using System;
using System.Collections.Generic;

namespace TrelloBack.Models;

public partial class Carte
{
    public int id { get; set; }

    public string titre { get; set; } = null!;

    public string? description { get; set; }

    public DateOnly? dateCreation { get; set; }

    public int? idListe { get; set; }

    public virtual ICollection<Commentaire> commentaires { get; set; } = new List<Commentaire>();

    public virtual Liste? idListeNavigation { get; set; }
}
