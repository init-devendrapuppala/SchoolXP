import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { Location } from '@angular/common';
import { Router, RouterLink } from '@angular/router';
import { ActiveSchoolService, UserSchool } from '../services/active-school-service';

interface SetupNavigationState {
  schools?: unknown;
  isPlatformAdmin?: unknown;
}

@Component({
  imports: [CommonModule, RouterLink],
  selector: 'app-setup-selection',
  styleUrl: './setup-selection.css',
  templateUrl: './setup-selection.html',
})
export class SetupSelection {
  private readonly router = inject(Router);
  private readonly location = inject(Location);
  private readonly activeSchoolService = inject(ActiveSchoolService);

  readonly schools: UserSchool[];
  readonly isPlatformAdmin: boolean;

  constructor() {
    const state = this.location.getState() as SetupNavigationState;
    this.schools = this.isUserSchoolList(state.schools) ? state.schools : [];
    this.isPlatformAdmin = state.isPlatformAdmin === true;
  }

  selectSchool(school: UserSchool): void {
    void this.openSchool(school);
  }

  private async openSchool(school: UserSchool): Promise<void> {
    const status = school.status.toLowerCase();
    if (status === 'inactive') {
      return;
    }
    if (!['active', 'draft', 'pending'].includes(status)) {
      throw new Error(`Unsupported membership status: ${school.status}`);
    }

    this.activeSchoolService.setActiveSchool(school);
    if (status === 'active') {
      await this.router.navigate(['/dashboard'], {
        queryParams: { schoolId: school.schoolId },
        state: { selectedSchool: school },
      });
      return;
    }

    const role = school.role.toLowerCase().replaceAll('_', '');
    const profilePath = role === 'student'
      ? '/add-edit-student/new'
      : ['administrativestaff', 'academicstaff', 'acedemicstaff'].includes(role)
        ? '/add-edit-staff/new'
        : null;

    if (!profilePath) {
      throw new Error(`Unsupported profile role: ${school.role}`);
    }

    await this.router.navigate([profilePath], {
      queryParams: { schoolId: school.schoolId },
      state: { selectedSchool: school },
    });
  }

  private isUserSchoolList(value: unknown): value is UserSchool[] {
    return Array.isArray(value) && value.every(
      (school): school is UserSchool =>
        typeof school === 'object' &&
        school !== null &&
        'schoolId' in school &&
        typeof school.schoolId === 'string' &&
        school.schoolId.length > 0 &&
        'name' in school &&
        typeof school.name === 'string' &&
        'role' in school &&
        typeof school.role === 'string' &&
        'status' in school &&
        typeof school.status === 'string'
    );
  }
}
