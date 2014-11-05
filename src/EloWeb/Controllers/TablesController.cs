using System.Linq;
using System.Web.Mvc;
using EloWeb.Models;

namespace EloWeb.Controllers
{
    public class TablesController : Controller
    {
        private readonly Players _players;

        public TablesController(Players players)
        {
            _players = players;
        }


        // GET: Tables
        public ActionResult Rating()
        {
            var table = _players.Active().OrderByDescending(p => p.Rating);
            if (!table.Any())
                return Redirect("~/Players/NewLeague");

            ViewData.Model = table;
            return View();
        }

        public ActionResult MaxRating()
        {
            var table = _players.All().OrderByDescending(p => p.MaxRating());
            if (!table.Any())
                return Redirect("~/Players/NewLeague");

            ViewData.Model = table;
            return View();
        }

        public ActionResult WinRate()
        {
            var table = _players.All().OrderByDescending(p => p.WinRate());
            if (!table.Any())
                return Redirect("~/Players/NewLeague");

            ViewData.Model = table;
            return View();
        }

        public ActionResult WinningStreak()
        {
            var table = _players.Active().OrderByDescending(p => p.CurrentWinningStreak());
            if (!table.Any())
                return Redirect("~/Players/NewLeague");

            ViewData.Model = table;
            return View();
        }

        public ActionResult BestEverWinningStreak()
        {
            var table = _players.All().OrderByDescending(p => p.LongestWinningStreak());
            if (!table.Any())
                return Redirect("~/Players/NewLeague");

            ViewData.Model = table;
            return View();
        }

        public ActionResult LosingStreak()
        {
            var table = _players.Active().OrderByDescending(p => p.CurrentLosingStreak());
            if (!table.Any())
                return Redirect("~/Players/NewLeague");

            ViewData.Model = table;
            return View();
        }

        public ActionResult WorstEverLosingStreak()
        {
            var table = _players.All().OrderByDescending(p => p.LongestLosingStreak());
            if (!table.Any())
                return Redirect("~/Players/NewLeague");

            ViewData.Model = table;
            return View();
        }
    }
}