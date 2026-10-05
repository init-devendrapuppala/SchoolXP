import { Location } from '@angular/common';
import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { signOut } from 'aws-amplify/auth';
import { ActiveSchoolService, UserSchool } from '../services/active-school-service';

interface DashboardNavigationState {
  selectedSchool?: unknown;
}

@Component({
  imports: [RouterLink],
  selector: 'app-dashboard',
  styleUrl: './dashboard.css',
  templateUrl: './dashboard.html',
})
export class Dashboard {
  readonly selectedSchool: UserSchool | null;

  constructor(location: Location, private readonly activeSchoolService: ActiveSchoolService) {
    const state = location.getState() as DashboardNavigationState;
    const navigationSchool = this.isUserSchool(state.selectedSchool) ? state.selectedSchool : null;
    this.selectedSchool = navigationSchool ?? this.activeSchoolService.getActiveSchool();
  }

  async logout(): Promise<void> {
    this.activeSchoolService.clearActiveSchool();
    await signOut();
  }

  private isUserSchool(value: unknown): value is UserSchool {
    return typeof value === 'object' &&
      value !== null &&
      'schoolId' in value &&
      typeof value.schoolId === 'string' &&
      'name' in value &&
      typeof value.name === 'string' &&
      'role' in value &&
      typeof value.role === 'string' &&
      'status' in value &&
      typeof value.status === 'string';
  }
}
