using PlataformaRio2C.Application.Interfaces.Services;
using PlataformaRio2C.Domain.Interfaces;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PlataformaRio2C.Web.Site.Areas.ProducerArea.Controllers
{
    [TermFilter(Order = 2)]
    [Authorize(Order = 1, Roles = "Producer")]
    public class PlayerController : PlataformaRio2C.Web.Site.Controllers.PlayerController
    {      
        public PlayerController(ICollaboratorAppService collaboratorAppService, IPlayerAppService playerAppService, IRepositoryFactory repositoryFactory)
            :base(collaboratorAppService, playerAppService, repositoryFactory)
        {

        }

        public ActionResult List()
        {
            return View();
        }


        public ActionResult Details(Guid uid)
        {
            var result = _playerAppService.GetByDetailsWithInterests(uid);

            if (result != null)
            {
                return View("Details", result);
            }

            return RedirectToAction("Index");
        }
    }
}