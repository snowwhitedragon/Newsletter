namespace Newsletter.Entities {
    public class Supplier {
        public int Id { get; set; }

        public string CompanyName { get; set; } = null!;

        public virtual ICollection<Contact> Contacts { get; set; } = new List<Contact>();
    }
}