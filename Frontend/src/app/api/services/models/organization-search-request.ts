import { SearchRequestBase } from "./search-request-base";

export class OrganizationSearchRequest extends SearchRequestBase {
    onlyMine?: boolean = true;
    
    public override reset(): void {
        this.searchTerm = '';
        this.skip = 0;
        this.take = 50;
        this.onlyMine = true;
    }
}