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

        [HttpGet]
        [Route("/projets")]
        public IActionResult getProjets()
        {
            //recuperer une liste de users

            Console.WriteLine("-----getProjets-----");
            var projets = _context.Projets;

            //renvoyer la liste en json

            return Json(projets);
        }

        [HttpPost]
        [Route("/projets")]
        public IActionResult createProjet(Projet newProjet)
        {
            Console.WriteLine($"---------newProjet id : {newProjet.Id}--------");
            Console.WriteLine($"---------newProjet nom : {newProjet.Nom}--------");
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

            existingProjet.Nom= updatedProjet.Nom;
            existingProjet.Description = updatedProjet.Description;
            existingProjet.DateCreation = updatedProjet.DateCreation;

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
