namespace Newsletter.Entities {

    public class Contact {
        public int Id { get; set; }

        public string ReadableId { get; set; } = null!;

        public string Salutation { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Country { get; set; } = null!;

        public string LanguageCode { get; set; } = null!;

        public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

        public virtual ICollection<Newsletter> Newsletters { get; set; } = new List<Newsletter>();

        public virtual ICollection<Subcontractor> Subcontractors { get; set; } = new List<Subcontractor>();

        public virtual ICollection<Supplier> Suppliers { get; set; } = new List<Supplier>();
    }
}