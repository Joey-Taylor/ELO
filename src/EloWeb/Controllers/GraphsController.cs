using System.Linq;
using System.Web.Helpers;
using System.Web.Mvc;
using EloWeb.Models;

namespace EloWeb.Controllers
{
    public class GraphsController : Controller
    {
        public ActionResult PlayerEloByGames(string playerName, int? width, int? height)
        {
            var player = Players.PlayerByName(playerName);

            var ratings = player.Ratings.OrderBy(r => r.TimeFrom);

            var yValues = ratings.Select(r => r.Value).ToArray();
            var xValues = ratings.Select((x, i) => i).ToArray();

            new Chart(width: width ?? 500, height: height ?? 400)
                .AddSeries(
                    name: "ELO",
                    xValue: xValues,
                    yValues: yValues,
                    chartType: "Line")
                .AddTitle("ELO vs Games")
                .SetXAxis("Games", xValues.Min(), xValues.Max())
                .SetYAxis("ELO", yValues.Min()-100, yValues.Max()+100)
                .Write("png");

            return new EmptyResult();
        }

        public ActionResult PlayerEloByTime(string playerName, int? width, int? height)
        {
            var player = Players.PlayerByName(playerName);

            var ratings = player.Ratings.OrderBy(r => r.TimeFrom);

            var yValues = ratings.Select(r => r.Value).ToArray();
            var xValues = ratings.Select(r => r.TimeFrom).ToArray();

            new Chart(width: width ?? 500, height: height ?? 400)
                .AddSeries(
                    name: "ELO",
                    xValue: xValues,
                    yValues: yValues,
                    chartType: "Line")
                .AddTitle("ELO vs Time")
                .SetXAxis("Time", xValues.Min().ToOADate(), xValues.Max().ToOADate())
                .SetYAxis("ELO", yValues.Min() - 100, yValues.Max() + 100)
                .Write("png");

            return new EmptyResult();
        }
	}
}