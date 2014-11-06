using System.Linq;
using System.Web.Mvc;
using EloWeb.Models;
using EloWeb.Services;

namespace EloWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly Players _players;
        public HomeController(Players players)
        {
            _players = players;
        }
        public ActionResult Index()
        {
            var leaderboard =
                _players.Active().Where(p => p.Games.Any()).OrderByDescending(player => player.CurrentRating);
            if (!leaderboard.Any())
                return Redirect("~/Players/NewLeague");

            ViewData.Model = leaderboard;
            return View();
        }
    }
}