using System.Linq;
using System.Web.Mvc;
using EloWeb.Models;
using EloWeb.Services;

namespace EloWeb.Controllers
{
    public class GraphsController : Controller
    {        
        private readonly Players _players;

        public GraphsController(Players players)
        {
            _players = players;
        }


        public ActionResult Index(GraphsViewModel model)
        {
            if (model == null || model.Names == null)
            {
                var names = _players.All()
                    .OrderBy(p => p.Name)
                    .Select(p => new CheckBoxViewModel{Checked = true, Text = p.Name})
                    .ToList();

                model = new GraphsViewModel {Names = names, GraphType = GraphType.EloVsGames};
            }

            return View(model);
        }            
	}
}