using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Helpers;
using System.Web.Mvc;
using EloWeb.Models;

namespace EloWeb.Controllers
{
    public class GraphGeneratorController : Controller
    {
        private const int DefaultWidth = 500;
        private const int DefaultHeight = 400;

        public ActionResult SelectedEloByGames(string[] playerNames, int? width, int? height, bool? title)
        {
            var players = Players.All().Where(p => playerNames.Contains(p.Name));
            var chart = GenerateGameChart(players, width, height, title);
            chart.AddLegend();
            chart.Write("png");

            return new EmptyResult();
        }

        public ActionResult SelectedEloByTime(string[] playerNames, int? width, int? height, bool? title)
        {
            var players = Players.All().Where(p => playerNames.Contains(p.Name));
            var chart = GenerateDateChart(players, width, height, title);
            chart.AddLegend();
            chart.Write("png");

            return new EmptyResult();
        }

        public ActionResult PlayerEloByGames(string playerName, int? width, int? height, bool? title)
        {
            var players = new[] { Players.PlayerByName(playerName) };
            var chart = GenerateGameChart(players, width, height, title);
            chart.Write("png");

            return new EmptyResult();
        }

        public ActionResult PlayerEloByTime(string playerName, int? width, int? height, bool? title)
        {
            var players = new[] {Players.PlayerByName(playerName)};
            var chart = GenerateDateChart(players, width, height, title);
            chart.Write("png");

            return new EmptyResult();
        }

        private static Chart GenerateGameChart(IEnumerable<Player> players, int? width, int? height, bool? title)
        {
            var chart = new Chart(width: width ?? DefaultWidth, height: height ?? DefaultHeight);
            if (title ?? true) chart.AddTitle("ELO vs Games");

            var maxGames = int.MinValue;
            var minGames = int.MaxValue;
            var maxRating = int.MinValue;
            var minRating = int.MaxValue;

            foreach (var player in players.OrderBy(p => p.Name))
            {
                var ratings = player.Ratings.OrderBy(r => r.TimeFrom);

                var yValues = ratings.Select(r => r.Value).ToArray();
                var xValues = ratings.Select((x, i) => i).ToArray();

                maxGames = Math.Max(maxGames, xValues.Max());
                minGames = Math.Min(maxGames, xValues.Min());
                maxRating = Math.Max(maxRating, yValues.Max());
                minRating = Math.Min(minRating, yValues.Min());

                chart.AddSeries(
                    name: player.Name,
                    xValue: xValues,
                    yValues: yValues,
                    chartType: "Line");
            }

            chart.SetXAxis("Games", minGames, maxGames);
            chart.SetYAxis("ELO", minRating - 100, maxRating + 100);

            return chart;
        }

        private static Chart GenerateDateChart(IEnumerable<Player> players, int? width, int? height, bool? title)
        {
            var maxDate = double.MinValue;
            var minDate = double.MaxValue;
            var maxRating = int.MinValue;
            var minRating = int.MaxValue;

            var chart = new Chart(width: width ?? DefaultWidth, height: height ?? DefaultHeight);
            if (title ?? true) chart.AddTitle("ELO vs Time");
            var now = DateTime.UtcNow;

            foreach (var player in players.OrderBy(p => p.Name))
            {
                var ratings = player.Ratings.OrderBy(r => r.TimeFrom).ToList();
                ratings.Add(new Rating { Value = ratings.Last().Value, TimeFrom = now });

                var yValues = ratings.Select(r => r.Value).ToArray();
                var xValues = ratings.Select(r => r.TimeFrom).ToArray();

                maxDate = Math.Max(maxDate, xValues.Max().ToOADate());
                minDate = Math.Min(minDate, xValues.Min().ToOADate());
                maxRating = Math.Max(maxRating, yValues.Max());
                minRating = Math.Min(minRating, yValues.Min());

                chart.AddSeries(
                    name: player.Name,
                    xValue: xValues,
                    yValues: yValues,
                    chartType: "Line");
            }

            chart.SetXAxis("Time", minDate, maxDate);
            chart.SetYAxis("ELO", minRating - 100, maxRating + 100);

            return chart;
        }
	}
}