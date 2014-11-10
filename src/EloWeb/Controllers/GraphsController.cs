using System.Linq;
using System.Web.Mvc;
using EloWeb.Models;

namespace EloWeb.Controllers
{
    public class GraphsController : Controller
    {
        public ActionResult Index(GraphsViewModel model)
        {
            if (model == null || model.Names == null)
            {
                var names = Players.All()
                    .OrderBy(p => p.Name)
                    .Select(p => new CheckBoxViewModel{Checked = true, Text = p.Name})
                    .ToList();

                model = new GraphsViewModel {Names = names, GraphType = GraphType.EloVsGames};
            }

            return View(model);
        }
	}
}