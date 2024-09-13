using System;
using System.Collections.Generic;

// auto-generated
namespace Newsletter.Entities;

public partial class Role
{
    public int Id { get; set; }

    public string Code { get; set; } = null!;

    public string Title { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
