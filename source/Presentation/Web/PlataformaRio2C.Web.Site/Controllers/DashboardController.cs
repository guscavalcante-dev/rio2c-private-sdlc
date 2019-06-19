using System.Web.Mvc;

namespace PlataformaRio2C.Web.Site.Controllers
{

    [TermFilter(Order = 2)]
    [Authorize(Order = 1, Roles = "Player")]
    public class DashboardController : BaseController
    {
        // GET: Dashboard
        public ActionResult Index()
        {

            return View();
        }
    }
}