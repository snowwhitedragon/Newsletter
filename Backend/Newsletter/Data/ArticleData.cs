using System.Text.Json.Serialization;
using Newsletter.Entities;

namespace Newsletter.Data {
    public class ArticleData {
        public ArticleData(Article article) {
            this.Id = article.Id;
            this.Title = article.Title;
            this.Summary = article.Summary;
            this.Description = article.Description;
            this.PreviewPicture = $"data:image/jpeg;base64,{Convert.ToBase64String(article.Picture)}";
            this.NewsletterId = article.NewsletterId;
            this.UpdatedAt = article.UpdatedAt;
            this.UpdatedByName = article.UpdatedBy?.DisplayName;
            this.Published = article.Published;
            this.PublishedAt = article.PublishedAt;
            this.PublishedByName = article.PublishedBy?.DisplayName;
        }

        public Guid? Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public byte[]? NewPicture { get; set; }
        public string? PreviewPicture { get; set; }
        public Guid NewsletterId { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedByName { get; set; }
        public bool? Published { get; set; }
        public DateTime? PublishedAt { get; set; }
        public string? PublishedByName { get; set; }
    }
}
