namespace Newsletter.Entities {
    public class Newsletter {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        public virtual ICollection<Article> Articles { get; set; } = new List<Article>();

        public virtual ICollection<Contact> Contacts { get; set; } = new List<Contact>();

        public virtual ICollection<Organization> Organizations { get; set; } = new List<Organization>();
    }
}