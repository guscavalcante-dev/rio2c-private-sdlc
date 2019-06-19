using PlataformaRio2C.Application.Interfaces.Services;
using System;
using System.Web.Mvc;

namespace PlataformaRio2C.Web.Site.Controllers
{
    [TermFilter(Order = 2)]
    [Authorize(Order = 1, Roles = "Player, Producer")]
    public class ConferenceController : BaseController
    {
        private readonly IConferenceAppService _appService;

        public ConferenceController(IConferenceAppService appService)
        {
            _appService = appService;
        }


        // GET: Lecture
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Detail(Guid uid)
        {
            var result = _appService.GetByDetails(uid);

            if (result != null)
            {
                return View( result);
            }

            return RedirectToAction("Index");            
        }
    }
}