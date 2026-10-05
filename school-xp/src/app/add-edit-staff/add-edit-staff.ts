import { Component, inject } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormGroup, NonNullableFormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActiveSchoolService } from '../services/active-school-service';

@Component({
  imports: [CommonModule, ReactiveFormsModule],
  selector: 'app-add-edit-staff',
  templateUrl: './add-edit-staff.html',
  styleUrl: './add-edit-staff.css',
})
export class AddEditStaff {
  readonly membershipStatus = inject(ActiveSchoolService).getActiveSchool()?.status.toLowerCase() ?? '';
  operationAdd = true;
  staffForm: FormGroup;
  documentsForm: FormGroup;

  constructor(private route: ActivatedRoute, private formBuilder: NonNullableFormBuilder) {
    this.operationAdd = !this.route.snapshot.paramMap.get('id');

    this.staffForm = this.formBuilder.group({
      personal: this.formBuilder.group({
        title: ['', Validators.required],
        firstName: ['', Validators.required],
        middleName: [''],
        lastName: ['', Validators.required],
        dateOfBirth: ['', Validators.required],
        gender: ['', Validators.required],
        maritalStatus: [''],
        nationality: ['', Validators.required],
        religion: [''],
        bloodGroup: [''],
        aadharNumber: ['', [Validators.pattern('^[0-9]+$'), Validators.minLength(12), Validators.maxLength(12)]],
      }),
      contact: this.formBuilder.group({
        email: ['', [Validators.required, Validators.email]],
        phoneNumber: ['', [Validators.required, Validators.pattern('^[0-9]+$'), Validators.minLength(10), Validators.maxLength(10)]],
        alternatePhoneNumber: ['', [Validators.pattern('^[0-9]+$'), Validators.minLength(10), Validators.maxLength(10)]],
        address: ['', Validators.required],
        city: ['', Validators.required],
        state: ['', Validators.required],
        postalCode: ['', [Validators.required, Validators.pattern('^[0-9]+$')]],
      }),
      employment: this.formBuilder.group({
        employeeId: ['', Validators.required],
        joiningDate: ['', Validators.required],
        employmentType: ['', Validators.required],
        department: ['', Validators.required],
        designation: ['', Validators.required],
        qualification: ['', Validators.required],
        specialization: [''],
        experienceYears: ['', Validators.pattern('^[0-9]+$')],
        assignedClasses: [''],
      }),
      emergencyContact: this.formBuilder.group({
        name: ['', Validators.required],
        relationship: ['', Validators.required],
        phoneNumber: ['', [Validators.required, Validators.pattern('^[0-9]+$'), Validators.minLength(10), Validators.maxLength(10)]],
        address: ['', Validators.required],
      }),
      system: this.formBuilder.group({
        isActive: true,
        remarks: '',
      }),
    });

    this.documentsForm = this.formBuilder.group({
      photo: this.formBuilder.control<File | null>(null),
      identityProof: this.formBuilder.control<File | null>(null),
      qualificationCertificate: this.formBuilder.control<File | null>(null),
      resume: this.formBuilder.control<File | null>(null),
    });
  }

  initializeForm(): void {}

  loadStaff(): void {}

  submitStaff(): void {}

  cancel(): void {}
}
