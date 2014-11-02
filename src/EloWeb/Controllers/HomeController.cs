using System.Linq;
using System.Web.Mvc;
using EloWeb.Models;

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
            var leaderboard = Players.Active().Where(p => p.GamesPlayed > 0).OrderByDescending(p => p.Rating);
            if (!leaderboard.Any())
                return Redirect("~/Players/NewLeague");

            ViewData.Model = leaderboard;
            return View();
        }
    }
}