using System;
using System.Collections.Generic;

namespace Newsletter.Entities;

public partial class State
{
    public Guid Id { get; set; }

    public string Title { get; set; } = null!;

    public string Language { get; set; } = null!;

    public string LanguageCode { get; set; } = null!;

    public virtual ICollection<Contact> ContactLanguages { get; set; } = new List<Contact>();

    public virtual ICollection<Contact> ContactStates { get; set; } = new List<Contact>();
}
