using Microsoft.AspNet.Identity;
using PlataformaRio2C.Application.Interfaces.Services;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlataformaRio2C.Web.Site.Controllers
{
    //[TermFilter(Order = 2)]
    //[Authorize(Order = 1, Roles = "Player")]
    public class ProjectController : BaseController
    {
        private readonly IProjectAppService _projectAppService;

        public ProjectController(IProjectAppService projectAppService)
        {
            _projectAppService = projectAppService;
        }

        // GET: Project
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Detail(Guid uid)
        {
            if (uid != Guid.Empty)
            {
                int userId = User.Identity.GetUserId<int>();
                var result = _projectAppService.GetForEvaluationPlayer(userId, uid);

                if (result != null)
                {
                    return View(result);
                }
            }

            this.StatusMessage("Projeto não encotrado!", Infra.CrossCutting.Tools.Enums.StatusMessageType.Danger);

            return RedirectToAction("Index");
        }
    }
}