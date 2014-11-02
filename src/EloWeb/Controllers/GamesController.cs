using System.Linq;
using System.Web.Mvc;
using EloWeb.Models;

namespace EloWeb.Controllers
{
    public class GamesController : Controller
    {
        private readonly GamesRepository _gamesRepository;
        private readonly Players _players;
        public GamesController(GamesRepository gamesRepository, Players players)
        {
            _gamesRepository = gamesRepository;
            _players = players;
        }

        // GET: Games
        public ActionResult Index()
        {
            var leaderboard = _players.All().OrderByDescending(p => p.Rating);
            if (!leaderboard.Any())
                return Redirect("~/Players/NewLeague");

            ViewData.Model = _gamesRepository.MostRecent(20);
            return View();
        }

        // GET: Games/Create
        [HttpGet]
        public ActionResult Create()
        {
            var createGameView = new ViewModels.CreateGame
            {                
                Players = _players.Active().Select(p => p.Name).OrderBy(n => n), 
                RecentGames = _gamesRepository.MostRecent(10)
            };
            ViewData.Model = createGameView;
            return View();
        }

        // POST: Games/Create
        [HttpPost]
        public ActionResult Create(Game game)
        {
            if (game.Winner != game.Loser)
            { 
                _gamesRepository.Add(game);                
            }

            return Redirect("~/");
        }

    }
}