import { ISearchRequestBase } from "./contracts/search-request-base.interface";

export class SearchRequestBase implements ISearchRequestBase {
    searchTerm?: string;
    skip: number = 0;
    take: number = 50;
    orderBy?: string;
    descending: boolean = false;

    constructor() {
    }

    public reset(): void {
        this.searchTerm = '';
        this.skip = 0;
        this.take = 50;
    }
}