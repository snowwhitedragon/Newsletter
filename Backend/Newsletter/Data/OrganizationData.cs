using Newsletter.Entities;

namespace Newsletter.Data {
    public class OrganizationData {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public List<NewsletterData> Newsletters { get; set; } = new List<NewsletterData>();
        public static OrganizationData Map(Organization organization) {
            return new OrganizationData() {
                Id = organization.Id,
                Title = organization.Title,
                Newsletters = organization.Newsletters.Select(n => NewsletterData.Map(n)).ToList()
            };
        }
    }
}
