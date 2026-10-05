import { Component, inject } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormGroup, NonNullableFormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActiveSchoolService } from '../services/active-school-service';
@Component({
  imports: [CommonModule, ReactiveFormsModule],
  selector: 'app-add-edit-student',
  styleUrl: './add-edit-student.css',
  templateUrl: './add-edit-student.html',
})
export class AddEditStudent {
  readonly membershipStatus = inject(ActiveSchoolService).getActiveSchool()?.status.toLowerCase() ?? '';
  operationAdd = true;
  admissionForm: FormGroup;
  documentsForm: FormGroup;

  constructor(private route: ActivatedRoute, private formBuilder: NonNullableFormBuilder) {
    this.operationAdd = !this.route.snapshot.paramMap.get('id');

    this.admissionForm = this.formBuilder.group({
      student: this.formBuilder.group({
        title: ['', Validators.required],
        firstName: ['', Validators.required],
        lastName: ['', Validators.required],
        email: ['', [Validators.required, Validators.email]],
        studentEnrollmentNumber: ['', [Validators.required, Validators.pattern('^[0-9]+$')]],
        dateOfBirth: ['', Validators.required],
        gender: ['', Validators.required],
        address: ['', Validators.required],
        phoneNumber: ['', [Validators.required, Validators.pattern('^[0-9]+$'), Validators.minLength(10), Validators.maxLength(10)]],
        aadharNumber: ['', [Validators.required, Validators.pattern('^[0-9]+$'), Validators.minLength(12), Validators.maxLength(12)]],
      }),
      guardian: this.formBuilder.group({
        guardianName: ['', Validators.required],
        guardianPhoneNumber: ['', [Validators.required, Validators.pattern('^[0-9]+$'), Validators.minLength(10), Validators.maxLength(10)]],
        guardianEmail: ['', [Validators.required, Validators.email]],
        guardianAddress: ['', Validators.required],
        guardianRelationship: ['', Validators.required],
      }),
      academic: this.formBuilder.group({
        class: ['', Validators.required],
        section: ['', Validators.required],
        rollNumber: ['', [Validators.required, Validators.pattern('^[0-9]+$')]],
        admissionDate: ['', Validators.required],
        previousSchool: ['', Validators.required],
        lastYearGrade: ['', Validators.required],
        lastYearResults: ['', Validators.required],
      }),
      administrative: this.formBuilder.group({
        nationality: ['', Validators.required],
        isDisabled: false,
        transportRequired: false,
        stayStatus: ['', Validators.required],
        bloodGroup: ['', Validators.required],
        religion: ['', Validators.required],
      }),
      system: this.formBuilder.group({
        isActive: true,
        Remarks: '',
      }),
    });

    this.documentsForm = this.formBuilder.group({
      photo: this.formBuilder.control<File | null>(null),
      birthCertificate: this.formBuilder.control<File | null>(null),
      aadharCard: this.formBuilder.control<File | null>(null),
      transferCertificate: this.formBuilder.control<File | null>(null),
      reportCard: this.formBuilder.control<File | null>(null),
    });
  }

  submitAdmission(): void {
    if (this.admissionForm.valid) {
      const admissionData = this.admissionForm.value;
      console.log('Admission Data:', admissionData);
    } else {
      console.log('Admission Form is invalid');
    }
  }
}
