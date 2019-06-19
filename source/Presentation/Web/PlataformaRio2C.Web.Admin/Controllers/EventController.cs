using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlataformaRio2C.Web.Admin.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class EventController : BaseController
    {
        // GET: Event
        public ActionResult Index()
        {
            return View();
        }
    }
}