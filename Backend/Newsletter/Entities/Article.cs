using System;
using System.Collections.Generic;

namespace Newsletter.Entities;

public partial class Article
{
    public Guid Id { get; set; }

    public string Title { get; set; } = null!;

    public string Summary { get; set; } = null!;

    public string Description { get; set; } = null!;

    public byte[] Picture { get; set; } = null!;

    public Guid NewsletterId { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid CreatedById { get; set; }

    public DateTime UpdatedAt { get; set; }

    public Guid UpdatedById { get; set; }

    public bool Published { get; set; }

    public DateTime? PublishedAt { get; set; }

    public Guid? PublishedById { get; set; }

    public virtual User CreatedBy { get; set; } = null!;

    public virtual Newsletter Newsletter { get; set; } = null!;

    public virtual User? PublishedBy { get; set; }

    public virtual User UpdatedBy { get; set; } = null!;
}
