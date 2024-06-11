export interface TableModel {
    search: string,
    sorting: Sorting,
    pageNumber: number,
    pageSize: number
  }

export interface Sorting{
  columnName: string,
  order: Order
}

export enum Order{
  Ascending = 1,
  Descending = 2
}