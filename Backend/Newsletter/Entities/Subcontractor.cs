using System;
using System.Collections.Generic;

// auto-generated
namespace Newsletter.Entities;

public partial class Subcontractor
{
    public int Id { get; set; }

    public string CompanyName { get; set; } = null!;

    public virtual ICollection<Contact> Contacts { get; set; } = new List<Contact>();
}
