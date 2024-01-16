using System;
using System.Collections.Generic;

namespace TrelloBack.Models;

public partial class Projet
{
    public int? id { get; set; } = null!;

    public string nom { get; set; } = null!;

    public string? description { get; set; }

    public DateOnly? dateCreation { get; set; }

    public virtual ICollection<Liste> listes { get; set; } = new List<Liste>();
}
