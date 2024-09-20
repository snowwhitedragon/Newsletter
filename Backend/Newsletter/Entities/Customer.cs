using System;
using System.Collections.Generic;

namespace Newsletter.Entities;

public partial class Customer
{
    public Guid Id { get; set; }

    public string? ReadableId { get; set; }

    public Guid ContactId { get; set; }

    public virtual Contact Contact { get; set; } = null!;
}
