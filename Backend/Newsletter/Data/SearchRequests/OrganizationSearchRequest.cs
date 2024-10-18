namespace Newsletter.Data.SearchRequests {
    public class OrganizationSearchRequest : SearchRequestBase {
        public bool? OnlyMine { get; set; } = true;
    }
}
