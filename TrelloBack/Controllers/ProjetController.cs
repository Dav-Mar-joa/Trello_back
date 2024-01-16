using Microsoft.AspNetCore.Mvc;
using TrelloBack.Models;

namespace TrelloBack.Controllers
{
    public class ProjetController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly DbTrelloContext _context;

        public ProjetController(ILogger<HomeController> logger, DbTrelloContext context)
        {
            _context = context;
            _logger = logger;
        }
        // -- requete pour les projets ---------

        /* (listes)=> listes.IdProjet == proj.Id*/

        /*public Boolean tri3(Liste listes)
        {
            return listes.id == proj.id;
        }*/

        /*       (Liste listes) =>listes.id == proj.id
        */




        // Lambda == Fonction flechée
        // fonction qui s'execute a l'endroit ou elle est appellé
        // 1) je supprime le nom de la fonction, et le type de retour
        // 2) je creer la fatArrow '=>' entre les arguments et le corps
        // si je n'ai qu'un argument je peux retirer les parentheses
        // si je n'ai qu'une ligne de code dans mon corps je peux retirer les accolades et de ce fait retirer le return

        [HttpGet]
        [Route("/projets")]
        public IActionResult GetProjets()
        {
            Console.WriteLine("-----getProjets-----");
            Console.WriteLine($"-----{DateTime.Now}-----");

            // Récupérer une liste de projets avec leurs listes et cartes associées
            var projets = _context.Projets
                .Select(proj => new Projet
                {
                    id = proj.id,
                    nom = proj.nom,
                    description = proj.description,
                    dateCreation = proj.dateCreation,
                    listes = _context.Listes
                        .Where(liste => liste.idProjet == proj.id)
                        .Select(liste => new Liste
                        {
                            id = liste.id,
                            idProjet = liste.idProjet,
                            nom = liste.nom,
                            cartes = _context.Cartes
                                .Where(carte => carte.idListe == liste.id)
                                .Select(carte => new Carte
                                {
                                    id = carte.id,
                                    idListe = carte.idListe,
                                    titre = carte.titre,
                                    description = carte.description,
                                    dateCreation = carte.dateCreation,
                                    commentaires = _context.Commentaires
                                        .Where(commentaire => commentaire.idCarte == carte.id)
                                        .Select(commentaire => new Commentaire
                                        {
                                            contenu = commentaire.contenu,
                                            id = commentaire.id,
                                            dateCreation = commentaire.dateCreation,
                                            idCarte = commentaire.idCarte
                                        }).ToList()
                                }).ToList()
                        }).ToList()
                }).ToList();

            // Renvoyer la liste en JSON
            return Json(projets);
        }


        /*          

                    foreach (var projet in projets)
                    {
                        projet.Listes = _context.Listes
                            .Where(liste => liste.IdProjet == projet.Id)
                            .Select(liste => new Liste
                            {
                                Id = liste.Id,
                                IdProjet = projet.Id,
                                IdProjetNavigation = null,
                                Nom = liste.Nom,

                                Cartes = _context.Cartes
                                    .Where(carte => carte.IdListe == liste.Id)
                                    .Select(carte => new Carte
                                    {
                                        Id = carte.Id,
                                        Titre = carte.Titre,
                                        Description = carte.Description,
                                        DateCreation = carte.DateCreation,
                                        IdListe = carte.IdListe,
                                        Commentaires = _context.Commentaires
                                            .Where(commentaire => commentaire.IdCarte == carte.Id)
                                            .ToList()
                                    })
                                    .ToList()
                            })
                            .ToList();
                    }

                    return Ok(projets);
        */
        [HttpPost]
        [Route("/projets")]
        public IActionResult createProjet(Projet newProjet)
        {
            Console.WriteLine($"---------newProjet id : {newProjet.id}--------");
            Console.WriteLine($"---------newProjet nom : {newProjet.nom}--------");
            try
            {
                if (newProjet == null)
                {
                    return BadRequest("Les donn�es du projet sont nulles.");
                }

                _context.Projets.Add(newProjet);
                _context.SaveChanges();

                // Retourne une r�ponse JSON avec le nouveau fil de discussion
                return Json(newProjet);
            }
            catch (Exception ex)
            {
                // G�rez l'exception de mani�re appropri�e (journalisation, renvoi d'un message d'erreur, etc.)
                return StatusCode(500, "Une erreur interne s'est produite lors de la cr�ation du projet.");
            }
        }

        [HttpPut]
        [Route("/projets")]
        // On en enlevé le /{id}
        public IActionResult updateProjet(int id, Projet updatedProjet)
        {
            Console.WriteLine($"------updatedProjet {id}--------");
            var existingProjet = _context.Projets.Find(id);

            existingProjet.nom= updatedProjet.nom;
            existingProjet.description = updatedProjet.description;
            existingProjet.dateCreation = updatedProjet.dateCreation;

            _context.Update(existingProjet);
            _context.SaveChanges();

            return Json(existingProjet);
        }

        [HttpDelete]
        [Route("projets")]
        // On en enlevé le /{id}
        public IActionResult DeleteProjet(int id)
        {
            Console.WriteLine($"------ Delete projet {id} --------");

            // Vérifiez si le projet avec l'ID spécifié existe
            var projet = _context.Projets.Find(id);

            if (projet == null)
            {
                // Retournez NotFound si le projet n'est pas trouvé
                return NotFound($"Projet avec l'ID {id} non trouvé.");
            }

            // Supprimez le projet de la base de données
            _context.Projets.Remove(projet);

            // Enregistrez les modifications dans la base de données
            _context.SaveChanges();

            // Retournez un résultat Ok pour indiquer que la suppression a réussi
            return Ok();
        }

        public IActionResult Index()
        {
            return View();

        }
    }
}
