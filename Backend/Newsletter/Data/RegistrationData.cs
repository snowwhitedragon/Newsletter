using Newsletter.Entities;

namespace Newsletter.Data {
    public class RegistrationData {
        public required string Username { get; set; }
        public required string Password { get; set; }
        public required string DisplayName { get; set; }
        public List<Guid> Roles { get; set; } = new List<Guid>();
        public Guid? OrganizationId { get; set; }
        public Contact? Contact { get; set; }
    }
}
