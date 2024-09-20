using System;
using System.Collections.Generic;

namespace Newsletter.Entities;

public partial class Subcontractor
{
    public Guid Id { get; set; }

    public string CompanyName { get; set; } = null!;

    public virtual ICollection<Contact> Contacts { get; set; } = new List<Contact>();
}
