using Newsletter.Entities;

namespace Newsletter.Data {
    public class ArticleData {
        public Guid? Id { get; set; }
        public required string Title { get; set; }
        public required string Summary { get; set; }
        public required string Description { get; set; }
        public byte[]? NewPicture { get; set; }
        public string? PreviewPicture { get; set; }
        public Guid NewsletterId { get; set; }
        public Guid OrganizationId { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedByName { get; set; }
        public bool? Published { get; set; }
        public DateTime? PublishedAt { get; set; }
        public string? PublishedByName { get; set; }

        public static ArticleData Map(Article article) {
            return new ArticleData {
                Id = article.Id,
                Title = article.Title,
                Summary = article.Summary,
                Description = article.Description,
                PreviewPicture = $"data:image/jpeg;base64,{Convert.ToBase64String(article.Picture)}",
                NewsletterId = article.NewsletterId,
                OrganizationId = article.OrganizationId,
                UpdatedAt = article.UpdatedAt,
                UpdatedByName = article.UpdatedBy?.DisplayName,
                Published = article.Published,
                PublishedAt = article.PublishedAt,
                PublishedByName = article.PublishedBy?.DisplayName
            };
        }

    }
}
