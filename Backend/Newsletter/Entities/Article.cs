using System;
using System.Collections.Generic;

// auto-generated
namespace Newsletter.Entities;

public partial class Article
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Summary { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Link { get; set; } = null!;

    public byte[] Picture { get; set; } = null!;

    public int NewsletterId { get; set; }

    public DateTime CreatedAt { get; set; }

    public int CreatedById { get; set; }

    public bool Published { get; set; }

    public DateTime? PublishedAt { get; set; }

    public int? PublishedById { get; set; }

    public virtual User CreatedBy { get; set; } = null!;

    public virtual Newsletter Newsletter { get; set; } = null!;

    public virtual User? PublishedBy { get; set; }
}
