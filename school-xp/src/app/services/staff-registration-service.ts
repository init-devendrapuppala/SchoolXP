import { HttpClient } from '@angular/common/http';
import { inject, Service } from '@angular/core';
import { fetchAuthSession } from 'aws-amplify/auth';
import { firstValueFrom } from 'rxjs';
import { API_BASE_URLS } from '../config/api-base-urls';

export interface StaffRegistrationRequest {
  firstName: string;
  lastName: string;
  email: string;
  phone: string;
  aadhaarNumber: string;
  roleTypeId: number;
  emergencyContactFirstName: string;
  emergencyContactLastName: string;
  emergencyContactRelationship: string;
  emergencyContactPhone: string;
  emergencyContactEmail: string;
}

export interface StaffRegistrationResponse {
  schoolMembershipId: string;
  status: 'draft';
}

@Service()
export class StaffRegistrationService {
  private readonly http = inject(HttpClient);

  async saveDraft(request: StaffRegistrationRequest): Promise<StaffRegistrationResponse> {
    const accessToken = await this.getAccessToken();
    return firstValueFrom(
      this.http.post<StaffRegistrationResponse>(
        `${API_BASE_URLS.school}/api/staff/registrations`,
        request,
        { headers: { Authorization: `Bearer ${accessToken}` } },
      ),
    );
  }

  private async getAccessToken(): Promise<string> {
    const session = await fetchAuthSession();
    const accessToken = session.tokens?.accessToken?.toString();
    if (!accessToken) {
      throw new Error('An authenticated session is required.');
    }

    return accessToken;
  }
}