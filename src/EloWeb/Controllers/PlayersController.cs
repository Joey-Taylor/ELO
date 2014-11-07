using System;
using System.Linq;
using System.Web.Mvc;
using EloWeb.Models;
using EloWeb.Services;
using EloWeb.Utils;
using EloWeb.ViewModels;

namespace EloWeb.Controllers
{
    public class PlayersController : Controller
    {
        private readonly Players _players;
        private readonly Ratings _ratings;

        public PlayersController(Players players, Ratings ratings)
        {
            _players = players;
            _ratings = ratings;
        }

        // GET: Players
        public ActionResult Index()
        {
            var leaderboard = _players.All().OrderByDescending(p => p.Name);
            if (!leaderboard.Any())
                return Redirect("~/Players/NewLeague");

            ViewData.Model = _players.All().OrderBy(p => p.Name);
            return View();
        }

        // GET: Players/Details?name=......
        public ActionResult Details(string name)
        {
            ViewData.Model = _players.PlayerByName(name);
            return View();
        }

        // GET: Players/Records
        public ActionResult Records()
        {
            var activePlayers = _players.Active();

            if (!activePlayers.Any())
                return Redirect("~/Players/NewLeague");

            var recordsView = new Records
            {
                CurrentTopRanked = activePlayers.MaxByAll(p => p.CurrentRating),
                MostRatingsPointsEver = activePlayers.MaxByAll(p => p.MaxRating),
                BestWinRate = activePlayers.MaxByAll(p => p.WinRate),
                LongestWinningStreak = activePlayers.MaxByAll(p => p.LongestWinningStreak),
                CurrentWinningStreak = activePlayers.MaxByAll(p => p.CurrentWinningStreak),
                LongestLosingStreak = activePlayers.MaxByAll(p => p.LongestLosingStreak),
                CurrentLosingStreak = activePlayers.MaxByAll(p => p.CurrentLosingStreak),
            };

            ViewData.Model = recordsView;
            return View();
        }

        public ActionResult NewLeague()
        {
            return View();
        }

        // GET: Players/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Players/Create
        [HttpPost]
        public ActionResult Create(CreatePlayerViewModel newPlayer)
        {
            var player = _players.Add(new Player(newPlayer.Name));
            _ratings.AddInitialRating(player);
            return Redirect("~/Players");
        }
    }
}