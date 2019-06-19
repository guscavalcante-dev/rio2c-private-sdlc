using PlataformaRio2C.Web.Site.Controllers;
using System.Web.Mvc;

namespace PlataformaRio2C.Web.Site.Areas.ProducerArea.Controllers
{
    [TermFilter(Order = 2)]
    [Authorize(Order = 1, Roles = "Producer")]
    public class DashboardController : BaseController
    {
        // GET: ProducerArea/Dashboard
        public ActionResult Index()
        {
            return View();
        }
    }
}