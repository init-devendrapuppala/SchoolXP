import { Service } from '@angular/core';

export interface UserSchool {
  schoolId: string;
  name: string;
  role: string;
  status: string;
}

export interface UserSchoolsResponse {
  isPlatformAdmin: boolean;
  schools: UserSchool[];
}

const ACTIVE_SCHOOL_KEY = 'schoolxp.activeSchool';

@Service()
export class ActiveSchoolService {
  setActiveSchool(school: UserSchool): void {
    sessionStorage.setItem(ACTIVE_SCHOOL_KEY, JSON.stringify(school));
  }

  getActiveSchool(): UserSchool | null {
    const storedSchool = sessionStorage.getItem(ACTIVE_SCHOOL_KEY);
    if (!storedSchool) {
      return null;
    }

    try {
      const school: unknown = JSON.parse(storedSchool);
      if (this.isUserSchool(school)) {
        return school;
      }
    } catch {
      // Clear malformed session data and treat it as no active selection.
    }

    this.clearActiveSchool();
    return null;
  }

  clearActiveSchool(): void {
    sessionStorage.removeItem(ACTIVE_SCHOOL_KEY);
  }

  private isUserSchool(value: unknown): value is UserSchool {
    return typeof value === 'object' &&
      value !== null &&
      'schoolId' in value &&
      typeof value.schoolId === 'string' &&
      value.schoolId.length > 0 &&
      'name' in value &&
      typeof value.name === 'string' &&
      'role' in value &&
      typeof value.role === 'string' &&
      'status' in value &&
      typeof value.status === 'string';
  }
}
