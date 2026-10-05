import { HttpClient } from '@angular/common/http';
import { Component, OnInit, inject } from '@angular/core';
import { Router } from '@angular/router';
import { fetchAuthSession, signOut } from 'aws-amplify/auth';
import { firstValueFrom } from 'rxjs';
import { API_BASE_URLS } from '../config/api-base-urls';
import { ActiveSchoolService, UserSchool, UserSchoolsResponse } from '../services/active-school-service';

interface ResolvedUser {
  id: string;
}

@Component({
  selector: 'app-auth-callback',
  standalone: true,
  template: `
    <main>
      <p>{{ message }}</p>
    </main>
  `,
})
export class AuthCallback implements OnInit {
  private readonly router = inject(Router);
  private readonly http = inject(HttpClient);
  private readonly activeSchoolService = inject(ActiveSchoolService);

  message = 'Completing sign in...';

  ngOnInit(): void {
    void this.completeSignIn();
  }

  private async completeSignIn(): Promise<void> {
    console.log('AuthCallback component initialized.');
    this.activeSchoolService.clearActiveSchool();

    try {
      const session = await fetchAuthSession();
      const idToken = session.tokens?.idToken;

      if (!idToken) {
        throw new Error('ID token is missing in the session.');
      }

      const user = await firstValueFrom(
        this.http.post<ResolvedUser>(
          `${API_BASE_URLS.identity}/api/auth/resolve-user`,
          { idToken: idToken.toString() }
        )
      );

      if (!user.id) {
        throw new Error('Identity service returned a user without an ID.');
      }

      await this.resolveNextPage();
    } catch (error: unknown) {
      console.error('Cognito callback failed:', error);
      this.message = 'Unable to complete sign in.';
      await signOut();
      await this.router.navigateByUrl('/login');
    }
  }

  private async resolveNextPage(): Promise<void> {
    const result = await firstValueFrom(
      this.http.get<UserSchoolsResponse>(`${API_BASE_URLS.school}/api/schools/mine`)
    );
    const schools = result.schools;

    if (result.isPlatformAdmin) {
      await this.router.navigate(['/setup'], {
        state: { schools, isPlatformAdmin: true },
      });
      return;
    }

    if (schools.length === 0) {
      throw new Error('No schools are linked to the signed-in user.');
    }

    if (schools.length === 1) {
      await this.openSchool(schools[0]);
      return;
    }

    await this.router.navigate(['/setup'], { state: { schools } });
  }

  async openSchool(selectedSchool: UserSchool): Promise<void> {
    const status = selectedSchool.status.toLowerCase();
    if (status === 'inactive') {
      throw new Error('Inactive school memberships cannot sign in.');
    }
    if (!['active', 'draft', 'pending'].includes(status)) {
      throw new Error(`Unsupported membership status: ${selectedSchool.status}`);
    }

    this.activeSchoolService.setActiveSchool(selectedSchool);

    if (status === 'active') {
      await this.router.navigate(['/dashboard'], {
        queryParams: { schoolId: selectedSchool.schoolId },
        state: { selectedSchool },
      });
      return;
    }

    const role = selectedSchool.role.toLowerCase().replaceAll('_', '');
    const profilePath = role === 'student'
      ? '/add-edit-student/new'
      : ['administrativestaff', 'academicstaff', 'acedemicstaff'].includes(role)
        ? '/add-edit-staff/new'
        : null;

    if (!profilePath) {
      throw new Error(`Unsupported profile role: ${selectedSchool.role}`);
    }

    await this.router.navigate([profilePath], {
      queryParams: { schoolId: selectedSchool.schoolId },
      state: { selectedSchool },
    });
  }
}
