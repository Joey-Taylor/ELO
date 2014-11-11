using System;
using System.Collections.Generic;
using System.Web.Mvc;
using EloWeb.Models;
using Microsoft.Ajax.Utilities;

namespace EloWeb.ViewModels
{
    public class CreateGame
    {
        public IEnumerable<Game> RecentGames { get; set; }
        public IEnumerable<SelectListItem> Players { get; set; }
        public long WinnerId { get; set; }
        public long LoserId { get; set; }
    }
}