import { CommonModule } from '@angular/common';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Component, inject } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { firstValueFrom } from 'rxjs';
import { API_BASE_URLS } from '../config/api-base-urls';
import { UserSchool } from '../services/active-school-service';

@Component({
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  selector: 'app-register-school',
  styleUrl: './register-school.css',
  templateUrl: './register-school.html',
})
export class RegisterSchool {
  private readonly http = inject(HttpClient);
  private readonly router = inject(Router);

  schoolForm: FormGroup;
  schoolAdmin: FormGroup;
  isSubmitted = false;
  isSubmitting = false;
  errorMessage = '';
  currentSection: 'school' | 'admin' | 'review' = 'school';

  constructor(private fb: FormBuilder) {
    this.schoolForm = this.fb.group({
      schoolName: ['', Validators.required],
      schoolEmail: ['', [Validators.required, Validators.email]],
      schoolPhone: ['', Validators.required],
      schoolAddress: ['', Validators.required],
      schoolCity: ['', Validators.required],
      schoolState: ['', Validators.required],
      schoolPostalCode: ['', Validators.required],
      schoolCountry: ['', Validators.required],
      schoolWebsite: [''],
      schoolAffliation: [''],
      schoolEstablishedYear: ['']
    });
    this.schoolAdmin = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email, Validators.maxLength(50)]],
      phone: ['', Validators.required],
      gender: ['', Validators.required],
      dateOfBirth: ['', Validators.required]
    });
  }

  isFieldInvalid(form: FormGroup, field: string): boolean {
    const control = form.get(field);
    return !!(control && control.invalid && (control.dirty || control.touched));
  }

  goToAdmin(): void {
    this.schoolForm.markAllAsTouched();

    if (this.schoolForm.valid) {
      this.currentSection = 'admin';
    }
  }

  goToReview(): void {
    this.schoolAdmin.markAllAsTouched();

    if (this.schoolAdmin.valid) {
      this.currentSection = 'review';
    }
  }

  goToPreviousSection(): void {
    this.currentSection = this.currentSection === 'review' ? 'admin' : 'school';
  }

  async onSubmit(): Promise<void> {
    this.schoolForm.markAllAsTouched();
    this.schoolAdmin.markAllAsTouched();

    if (!this.schoolForm.valid || !this.schoolAdmin.valid || this.isSubmitting) {
      return;
    }

    this.isSubmitting = true;
    this.errorMessage = '';
    const school = this.schoolForm.getRawValue();
    const admin = this.schoolAdmin.getRawValue();

    let createdSchoolId: string;
    try {
      const result = await firstValueFrom(
        this.http.post<{ schoolId: string }>(
          `${API_BASE_URLS.school}/api/schools`,
          {
            schoolName: school.schoolName,
            schoolEmail: school.schoolEmail,
            schoolPhone: school.schoolPhone,
            schoolAddress: school.schoolAddress,
            schoolCity: school.schoolCity,
            schoolState: school.schoolState,
            schoolPostalCode: school.schoolPostalCode,
            schoolCountry: school.schoolCountry,
            schoolWebsite: school.schoolWebsite,
            schoolAffiliation: school.schoolAffliation,
            schoolEstablishedYear: school.schoolEstablishedYear
              ? Number(school.schoolEstablishedYear)
              : null,
            firstName: admin.firstName,
            lastName: admin.lastName,
            email: admin.email,
            phone: admin.phone,
            gender: admin.gender,
            dateOfBirth: admin.dateOfBirth,
          }
        )
      );
      createdSchoolId = result.schoolId;
    } catch (error: unknown) {
      console.error('School registration failed:', error);
      this.errorMessage = error instanceof HttpErrorResponse &&
        typeof error.error?.message === 'string'
        ? error.error.message
        : 'School registration failed. Please check your connection and try again.';
      this.isSubmitting = false;
      return;
    }

    const selectedSchool: UserSchool = {
      schoolId: createdSchoolId,
      name: school.schoolName.trim(),
      role: 'platformadmin',
      status: 'active',
    };
    this.isSubmitted = true;
    await this.router.navigate(['/setup'], {
      state: { schools: [selectedSchool], isPlatformAdmin: true },
    });
  }
}
