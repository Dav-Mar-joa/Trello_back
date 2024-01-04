using Microsoft.AspNetCore.Mvc;
using TrelloBack.Models;

namespace TrelloBack.Controllers
{
    public class ListeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly DbTrelloContext _context;

        public ListeController(ILogger<HomeController> logger, DbTrelloContext context)
        {
            _context = context;
            _logger = logger;
        }
// -- requete pour les listes ---------

        [HttpGet]
        [Route("/listes")]
        public IActionResult getListes()
        {
            //recuperer une liste de users

            Console.WriteLine("-----getListes-----");
            var listes = _context.Listes;

            //renvoyer la liste en json

            return Json(listes);
        }

        [HttpPost]
        [Route("/listes")]
        public IActionResult createListe(Liste newListe)
        {
            Console.WriteLine($"---------newListe id : {newListe.Id}--------");
            Console.WriteLine($"---------newListe nom : {newListe.Nom}--------");
            try
            {
                if (newListe == null)
                {
                    return BadRequest("Les donn�es du liste sont nulles.");
                }

                _context.Listes.Add(newListe);
                _context.SaveChanges();

                // Retourne une r�ponse JSON avec le nouveau fil de discussion
                return Json(newListe);
            }
            catch (Exception ex)
            {
                // G�rez l'exception de mani�re appropri�e (journalisation, renvoi d'un message d'erreur, etc.)
                return StatusCode(500, "Une erreur interne s'est produite lors de la cr�ation du liste.");
            }
        }

        [HttpPut]
        [Route("/listes")]
        // On en enlevé le /{id}
        public IActionResult updateThread(int id, Liste updatedListe)
        {
            Console.WriteLine($"------updatedListe {id}--------");
            var existingListe = _context.Listes.Find(id);

            existingListe.Nom= updatedListe.Nom;
            existingListe.IdProjet = updatedListe.IdProjet;

            _context.Update(existingListe);
            _context.SaveChanges();

            return Json(existingListe);
        }

        [HttpDelete]
        [Route("listes")]
        // On en enlevé le /{id}
        public IActionResult DeleteListe(int id)
        {
            Console.WriteLine($"------ Delete liste {id} --------");

            // Vérifiez si le liste avec l'ID spécifié existe
            var liste = _context.Listes.Find(id);

            if (liste == null)
            {
                // Retournez NotFound si le liste n'est pas trouvé
                return NotFound($"Liste avec l'ID {id} non trouvé.");
            }

            // Supprimez le liste de la base de données
            _context.Listes.Remove(liste);

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
