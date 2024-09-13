namespace Newsletter.Data.SearchRequests {
    public class ArticleSearchRequest : SearchRequestBase {
        public int OrganizationId { get; set; }
        public int NewsletterId { get; set; }
        public int CreatedById { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}
