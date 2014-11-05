using System.Linq;
using System.Web.Mvc;
using EloWeb.Models;
using EloWeb.Persist;
using EloWeb.Utils;
using EloWeb.ViewModels;

namespace EloWeb.Controllers
{
    public class PlayersController : Controller
    {
        // GET: Players
        public ActionResult Index()
        {
            var leaderboard = Players.All().OrderByDescending(p => p.Rating);
            if (!leaderboard.Any())
                return Redirect("~/Players/NewLeague");

            var players = Players.All();
            ViewData.Model = players.OrderBy(p => p.Name);
            return View();
        }

        // GET: Players/Details?name=......
        public ActionResult Details(string name)
        {
            ViewData.Model = Players.PlayerByName(name);
            return View();
        }

        // GET: Players/Records
        public ActionResult Records()
        {
            var activePlayers = Players.Active();

            if (!activePlayers.Any())
                return Redirect("~/Players/NewLeague");

            var recordsView = new Records
            {
                CurrentTopRanked = activePlayers.MaxByAll(p => p.Rating),
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
        public ActionResult Create(Player player)
        {
            Players.Add(Player.CreateInitial(player.Name)); 
            PlayersData.PersistPlayer(player.Name);         
            return Redirect("~/Players");
        }
    }
}