using System;
using System.Collections.Generic;

namespace TrelloBack;

public partial class Projet
{
    public int Id { get; set; }

    public string Nom { get; set; } = null!;

    public string? Description { get; set; }

    public DateOnly? DateCreation { get; set; }

    public virtual ICollection<Liste> Listes { get; set; } = new List<Liste>();
}
