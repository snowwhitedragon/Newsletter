namespace Newsletter.Entities {
    public class Role {
        public int Id { get; set; }

        public string Code { get; set; } = null!;

        public string Title { get; set; } = null!;

        public virtual ICollection<User> Users { get; set; } = new List<User>();
    }
}