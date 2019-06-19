using PlataformaRio2C.Application.Interfaces.Services;
using PlataformaRio2C.Application.ViewModels.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlataformaRio2C.Web.Admin.Controllers
{
    public class PlayerProducerController : Controller
    {
        private readonly IUserAppService _userService;
        public PlayerProducerController(IUserAppService userService)
        {

            _userService = userService;
        }

        // GET: PlayerProducer
        public ActionResult Index()
        {
            //var data = _userService.getAllPlayerProducer();
            return View();
        }

        public ActionResult Create()
        {
            var viewmodel = new PlayerProducerEditAppViewModel();
            return View();
        }
    }
}