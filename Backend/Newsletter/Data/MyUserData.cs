using Newsletter.Entities;

namespace Newsletter.Data {
    public class MyUserData {
        public MyUserData(User user) {
            this.Username = user.Username;
            this.DisplayName = user.DisplayName;
            this.Roles = user.Roles.Select(r => new HeaderData() { 
                Id = r.Id,
                Title = r.Title
            }).ToList();
            this.OrganizationName = user.Organization?.Title;
            this.OrganizationId = user.OrganizationId;
        }

        public string Username { get; set; }
        public string DisplayName { get; set; }
        public List<HeaderData> Roles { get; set; } = new List<HeaderData>();
        public Guid? OrganizationId { get; set; }
        public string? OrganizationName { get; set; }
    }
}
