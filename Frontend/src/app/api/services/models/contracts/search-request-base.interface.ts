export interface ISearchRequestBase {
    searchTerm?: string;
    skip: number;
    take: number;
    orderBy?: string;
    descending: boolean;
}