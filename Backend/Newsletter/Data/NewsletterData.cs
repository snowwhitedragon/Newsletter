namespace Newsletter.Data {
    public class NewsletterData {
        public NewsletterData(Entities.Newsletter news) {
            this.Id = news.Id;
            this.Title = news.Title;
            this.Description = news.Description;

            if (news.Articles != null && news.Articles.Any()) {
                this.Articles = news.Articles.Select(a => new ArticleData(a)).ToList();
            }
        }

        public Guid? Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public List<ArticleData> Articles { get; set; } = new List<ArticleData>();
    }
}
