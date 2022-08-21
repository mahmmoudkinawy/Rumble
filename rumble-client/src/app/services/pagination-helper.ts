import { HttpClient, HttpParams } from '@angular/common/http';
import { map } from 'rxjs';

import { PaginationResult } from '../models/pagination';

export function getPaginatedResult<T>(
  url: string,
  params: HttpParams,
  http: HttpClient
) {
  const paginatedResult: PaginationResult<T> = new PaginationResult<T>();

  return http.get<T>(url, { observe: 'response', params }).pipe(
    map((response) => {
      paginatedResult.result = response.body;
      if (response.headers.get('Pagination') !== null) {
        paginatedResult.pagination = JSON.parse(
          response.headers.get('Pagination') as string
        );
      }
      return paginatedResult;
    })
  );
}

export function getPaginationHeaders(pageNumber: number, pageSize: number) {
  let params = new HttpParams();

  params = params.append('pageNumber', pageNumber);
  params = params.append('pageSize', pageSize);

  return params;
}
