import { Component, inject } from '@angular/core';
import {
  StaffRegistrationRequest,
  StaffRegistrationService,
} from '../services/staff-registration-service';

@Component({
  selector: 'app-register-staff',
  templateUrl: './register-staff.html',
  styleUrl: './register-staff.css',
})
export class RegisterStaff {
  private readonly staffRegistrationService = inject(StaffRegistrationService);

  isSaving = false;
  statusMessage = '';

  async saveDraft(form: HTMLFormElement): Promise<void> {
    if (!form.reportValidity() || this.isSaving) {
      return;
    }

    const formData = new FormData(form);
    const value = (name: string): string => String(formData.get(name) ?? '').trim();
    const request: StaffRegistrationRequest = {
      firstName: value('firstName'),
      lastName: value('lastName'),
      email: value('email'),
      phone: value('phone'),
      aadhaarNumber: value('aadhaarNumber'),
      roleTypeId: Number(value('roleTypeId')),
      emergencyContactFirstName: value('emergencyContactFirstName'),
      emergencyContactLastName: value('emergencyContactLastName'),
      emergencyContactRelationship: value('emergencyContactRelationship'),
      emergencyContactPhone: value('emergencyContactPhone'),
      emergencyContactEmail: value('emergencyContactEmail'),
    };

    this.isSaving = true;
    this.statusMessage = '';
    try {
      const result = await this.staffRegistrationService.saveDraft(request);
      this.statusMessage = `Staff draft saved (${result.schoolMembershipId}).`;
    } catch {
      this.statusMessage = 'Unable to save staff registration. Check the SchoolService API and try again.';
    } finally {
      this.isSaving = false;
    }
  }
}