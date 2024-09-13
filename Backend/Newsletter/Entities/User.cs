using System;
using System.Collections.Generic;

// auto-generated
namespace Newsletter.Entities;

public partial class User
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string DisplayName { get; set; } = null!;

    public int? OrganizationId { get; set; }

    public DateTime RegistratedAt { get; set; }

    public virtual ICollection<Article> ArticleCreatedBies { get; set; } = new List<Article>();

    public virtual ICollection<Article> ArticlePublishedBies { get; set; } = new List<Article>();

    public virtual Organization? Organization { get; set; }

    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
}
