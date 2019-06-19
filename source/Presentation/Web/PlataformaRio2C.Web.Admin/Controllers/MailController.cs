using PlataformaRio2C.Application.Interfaces.Services;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace PlataformaRio2C.Web.Admin.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class MailController : BaseController
    {
        private readonly IMailAppService _appService;

        public MailController(IMailAppService appService)
        {
            _appService = appService;
        }

        public ActionResult Index()
        {
            var model = _appService.GetAll();

            if (model != null)
            {
                model = model.ToList();
            }

            return View(model);
        }

    }
}