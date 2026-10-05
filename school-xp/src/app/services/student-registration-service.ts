import { HttpClient } from '@angular/common/http';
import { inject, Service } from '@angular/core';
import { fetchAuthSession } from 'aws-amplify/auth';
import { firstValueFrom } from 'rxjs';
import { API_BASE_URLS } from '../config/api-base-urls';

export interface StudentRegistrationRequest {
  firstName: string;
  lastName: string;
  email: string;
  aadhaarNumber: string;
  guardianFirstName: string;
  guardianLastName: string;
  guardianAadhaarNumber: string;
  guardianTypeId: number;
  guardianPhone: string;
  guardianEmail: string;
}

export interface StudentRegistrationResponse {
  schoolMembershipId: string;
  status: 'draft';
}

export interface GuardianType {
  guardianTypeId: number;
  code: string;
  description: string;
}

@Service()
export class StudentRegistrationService {
  private readonly http = inject(HttpClient);

  async getPageRenderInfo(): Promise<GuardianType[]> {
    const accessToken = await this.getAccessToken();

    return firstValueFrom(
      this.http.get<GuardianType[]>(`${API_BASE_URLS.school}/api/students/registrations`, {
        headers: { Authorization: `Bearer ${accessToken}` },
      }),
    );
  }

  async saveDraft(request: StudentRegistrationRequest): Promise<StudentRegistrationResponse> {
    const accessToken = await this.getAccessToken();

    return firstValueFrom(
      this.http.post<StudentRegistrationResponse>(
        `${API_BASE_URLS.school}/api/students/registrations`,
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