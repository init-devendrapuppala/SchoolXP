import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  GuardianType,
  StudentRegistrationRequest,
  StudentRegistrationService,
} from '../services/student-registration-service';

@Component({
  imports: [CommonModule],
  selector: 'app-register-student',
  templateUrl: './register-student.html',
  styleUrl: './register-student.css',
})
export class RegisterStudent {
  private readonly studentRegistrationService = inject(StudentRegistrationService);

  guardianTypes: GuardianType[] = [];

  async ngOnInit(): Promise<void> {
    this.guardianTypes = await this.studentRegistrationService.getPageRenderInfo();
  }

  isSaving = false;
  statusMessage = '';

  private async save(form: HTMLFormElement): Promise<void> {
    if (!form.reportValidity() || this.isSaving) {
      return;
    }

    const formData = new FormData(form);
    const value = (name: string): string => String(formData.get(name) ?? '').trim();
    const request: StudentRegistrationRequest = {
      firstName: value('firstName'),
      lastName: value('lastName'),
      email: value('email'),
      aadhaarNumber: value('aadhaarNumber'),
      guardianFirstName: value('guardianFirstName'),
      guardianLastName: value('guardianLastName'),
      guardianAadhaarNumber: value('guardianAadhaarNumber'),
      guardianTypeId: Number(value('guardianTypeId')),
      guardianPhone: value('guardianPhone'),
      guardianEmail: value('guardianEmail'),
    };

    this.isSaving = true;
    this.statusMessage = '';

    try {
      await this.studentRegistrationService.saveDraft(request);
      this.statusMessage = 'Draft saved.';
    } catch {
      this.statusMessage = 'Unable to save student registration. Check the SchoolService API and try again.';
    } finally {
      this.isSaving = false;
    }
  }

  saveDraft(form: HTMLFormElement): Promise<void> {
    return this.save(form);
  }
}