namespace Newsletter.Data {
    public class NewsletterData {
        public Guid? Id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public List<ArticleData> Articles { get; set; } = new List<ArticleData>();
        public static NewsletterData Map(Entities.Newsletter news) {
            return new NewsletterData() {
                Id = news.Id,
                Title = news.Title,
                Description = news.Description,
                Articles = news.Articles.Select(a => ArticleData.Map(a)).ToList()
            };
        }
    }
}
