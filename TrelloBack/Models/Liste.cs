using System;
using System.Collections.Generic;

namespace TrelloBack.Models;

public partial class Liste
{
    public int id { get; set; }

    public string nom { get; set; } = null!;

    public int? idProjet { get; set; }

    public virtual ICollection<Carte> cartes { get; set; } = new List<Carte>();

    public virtual Projet? idProjetNavigation { get; set; }
}
