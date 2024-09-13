using System;
using System.Collections.Generic;

// auto-generated
namespace Newsletter.Entities;

public partial class Organization
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public int ResponsibilityType { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();

    public virtual ICollection<Newsletter> Newsletters { get; set; } = new List<Newsletter>();
}
