import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { fetchAuthSession } from 'aws-amplify/auth';
import { from, switchMap, throwError } from 'rxjs';
import { API_BASE_URLS } from '../config/api-base-urls';
import { ActiveSchoolService } from './active-school-service';

export const authInterceptor: HttpInterceptorFn = (request, next) => {
  const activeSchoolService = inject(ActiveSchoolService);

  if (!isConfiguredApiUrl(request.url)) {
    return next(request);
  }

  return from(fetchAuthSession()).pipe(
    switchMap((session) => {
      const accessToken = session.tokens?.accessToken?.toString();
      if (!accessToken) {
        return throwError(() => new Error('Access token is missing from the auth session.'));
      }

      let headers = request.headers.set('Authorization', `Bearer ${accessToken}`);
      if (isSchoolServiceUrl(request.url)) {
        const activeSchool = activeSchoolService.getActiveSchool();
        if (activeSchool) {
          headers = headers.set('X-School-Id', activeSchool.schoolId);
        }
      }

      return next(request.clone({ headers }));
    })
  );
};

function isConfiguredApiUrl(url: string): boolean {
  return Object.values(API_BASE_URLS).some((baseUrl) => isUrlWithinBase(url, baseUrl));
}

function isSchoolServiceUrl(url: string): boolean {
  return isUrlWithinBase(url, API_BASE_URLS.school);
}

function isUrlWithinBase(url: string, baseUrl: string): boolean {
  return url === baseUrl ||
    url.startsWith(`${baseUrl}/`) ||
    url.startsWith(`${baseUrl}?`);
}
