using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EloWeb.Models;
using EloWeb.ViewModels;

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
            ViewData.Model =  new CreateGame
            {                
                Players = GetPlayerSelectList(),
                RecentGames = _gamesRepository.MostRecent(10)
            };
            return View();
        }

        private IEnumerable<SelectListItem> GetPlayerSelectList()
        {
            var players = _players.Active();

            var selectList = players.Select(p => 
                new SelectListItem
                {
                    Value = p.ID.ToString(),
                    Text = p.Name
                }
            );

            return new SelectList(selectList, "Value", "Text");
        }

        // POST: Games/Create
        [HttpPost]
        public ActionResult Create(GameOutcome gameOutcome)
        {
            if (gameOutcome.WinnerId != gameOutcome.LoserId)
            {                         
                var winner = _players.Get(gameOutcome.WinnerId);
                var loser = _players.Get(gameOutcome.LoserId);
                _gamesRepository.Add(new Game(winner, loser));
                _players.UpdateRatings(winner, loser);
            }

            return Redirect("~/");
        }

    }

    public class GameOutcome
    {
        public long WinnerId { get; set; }
        public long LoserId { get; set; }
    }
}