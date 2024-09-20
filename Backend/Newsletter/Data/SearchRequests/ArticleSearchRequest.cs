namespace Newsletter.Data.SearchRequests {
    public class ArticleSearchRequest : SearchRequestBase {
        public Guid? OrganizationId { get; set; }
        public Guid? NewsletterId { get; set; }
        public Guid? CreatedById { get; set; }
        public bool? Published { get; set; } = true;
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
    }
}
