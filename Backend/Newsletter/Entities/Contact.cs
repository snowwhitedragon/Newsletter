using System;
using System.Collections.Generic;

namespace Newsletter.Entities;

public partial class Contact
{
    public Guid Id { get; set; }

    public string ReadableId { get; set; } = null!;

    public string Salutation { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public Guid StateId { get; set; }

    public Guid LanguageId { get; set; }

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

    public virtual State Language { get; set; } = null!;

    public virtual State State { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();

    public virtual ICollection<Newsletter> Newsletters { get; set; } = new List<Newsletter>();

    public virtual ICollection<Subcontractor> Subcontractors { get; set; } = new List<Subcontractor>();

    public virtual ICollection<Supplier> Suppliers { get; set; } = new List<Supplier>();
}
