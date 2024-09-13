using System;
using System.Collections.Generic;

// auto-generated
namespace Newsletter.Entities;

public partial class Customer
{
    public int Id { get; set; }

    public string? ReadableId { get; set; }

    public int ContactId { get; set; }

    public virtual Contact Contact { get; set; } = null!;
}
