using System;
using System.Collections.Generic;

namespace TrelloBack.Models;

public partial class Commentaire
{
    public int id { get; set; }

    public string? contenu { get; set; }

    public DateOnly? dateCreation { get; set; }

    public int? idCarte { get; set; }

    public virtual Carte? idCarteNavigation { get; set; }
}
