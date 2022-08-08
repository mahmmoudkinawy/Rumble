export interface Pagination {
  currentPage: number;
  itemsPerPage: number;
  totalPages: number;
  totalItems: number;
}

export class PaginationResult<T> {
  result: T | null = null;
  pagination: Pagination | null = null;
}
