using System.Web.Mvc;

namespace Fatec.RD.WebApi.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("ui/index", "Swagger");
        }
    }
}
