import { ComponentFixture, TestBed } from '@angular/core/testing';
import { provideHttpClient } from '@angular/common/http';
import { provideHttpClientTesting, HttpTestingController } from '@angular/common/http/testing';
import { Router, provideRouter } from '@angular/router';
import { afterEach, vi } from 'vitest';
import { API_BASE_URLS } from '../config/api-base-urls';
import { RegisterSchool } from './register-school';

describe('RegisterSchool', () => {
  let component: RegisterSchool;
  let fixture: ComponentFixture<RegisterSchool>;
  let httpTesting: HttpTestingController;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RegisterSchool],
      providers: [provideHttpClient(), provideHttpClientTesting(), provideRouter([])],
    }).compileComponents();

    fixture = TestBed.createComponent(RegisterSchool);
    component = fixture.componentInstance;
    httpTesting = TestBed.inject(HttpTestingController);
    await fixture.whenStable();
  });

  afterEach(() => httpTesting.verify());

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('registers the school and administrator, then opens the setup page', async () => {
    component.schoolForm.setValue({
      schoolName: 'Northbridge Academy',
      schoolEmail: 'office@northbridge.edu',
      schoolPhone: '+1 555 0100',
      schoolAddress: '1 Academy Way',
      schoolCity: 'Northbridge',
      schoolState: 'State',
      schoolPostalCode: '10000',
      schoolCountry: 'Country',
      schoolWebsite: '',
      schoolAffliation: '',
      schoolEstablishedYear: '',
    });
    component.schoolAdmin.setValue({
      firstName: 'Alex',
      lastName: 'Admin',
      email: 'admin@northbridge.edu',
      phone: '+1 555 0101',
      gender: 'unspecified',
      dateOfBirth: '1990-01-01',
    });
    const router = TestBed.inject(Router);
    const navigate = vi.spyOn(router, 'navigate').mockResolvedValue(true);

    const submit = component.onSubmit();
    const request = httpTesting.expectOne(`${API_BASE_URLS.school}/api/schools`);
    expect(request.request.method).toBe('POST');
    expect(request.request.body.schoolName).toBe('Northbridge Academy');
    expect(request.request.body.schoolEstablishedYear).toBeNull();
    request.flush({ schoolId: 'school-id' });
    await submit;

    expect(navigate).toHaveBeenCalledWith(['/setup'], {
      state: {
        schools: [{
          schoolId: 'school-id',
          name: 'Northbridge Academy',
          role: 'platformadmin',
          status: 'active',
        }],
        isPlatformAdmin: true,
      },
    });
  });
});
