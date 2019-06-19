using PlataformaRio2C.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlataformaRio2C.Web.Site.Controllers
{
    [TermFilter(Order = 2)]
    [Authorize(Order = 1, Roles = "Player,Producer")]
    public class ScheduleController : BaseController
    {
        private readonly IScheduleAppService _scheduleAppService;
        public ScheduleController(IScheduleAppService scheduleAppService)
        {
            _scheduleAppService = scheduleAppService;
        }


        // GET: Schedule
        public ActionResult Index()
        {
            //if (!_scheduleAppService.ScheduleIsEnable())
            //{
            //    return View("Disabled");
            //}

            return View();
        }

        public ActionResult Print()
        {
            if (!_scheduleAppService.ScheduleIsEnable())
            {
                //return View("Disabled");
            }

            return View();
        }
    }
}