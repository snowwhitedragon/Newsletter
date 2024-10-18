using Newsletter.Entities;

namespace Newsletter.Data {
    public class OrganizationData {
        public OrganizationData(Organization organization) {
            this.Id = organization.Id;
            this.Title = organization.Title;

            if (organization.Newsletters != null && organization.Newsletters.Any()) {
                this.Newsletters = organization.Newsletters.Select(n => new NewsletterData(n)).ToList();
            }
        }

        public Guid Id { get; set; }
        public string Title { get; set; }
        public List<NewsletterData> Newsletters { get; set; } = new List<NewsletterData>();
    }
}
