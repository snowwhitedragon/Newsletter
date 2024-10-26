import { SearchRequestBase } from './search-request-base';

export class ArticleSearchRequest extends SearchRequestBase {
  organizationId?: string;
  newsletterId?: string;
  createdById?: string;
  published?: boolean = false;
  from?: Date;
  to?: Date;

  public override reset(): void {
    this.searchTerm = '';
    this.skip = 0;
    this.take = 50;
    this.organizationId = undefined;
    this.newsletterId = undefined;
    this.createdById = undefined;
    this.published = true;
    this.from = undefined;
    this.to = undefined;
  }
}
