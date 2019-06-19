using Microsoft.AspNet.Identity;
using PlataformaRio2C.Application.Interfaces.Services;
using PlataformaRio2C.Application.ViewModels;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using System.Web.Mvc;

namespace PlataformaRio2C.Web.Site.Controllers
{
    [Authorize]
    public class TermController : BaseController
    {
        private readonly IUserUseTermAppService _userUseTermAppService;

        public TermController(IUserUseTermAppService userUseTermAppService)
        {
            _userUseTermAppService = userUseTermAppService;
        }

        // GET: Term        
        public ActionResult Index()
        {
            if (User.IsInRole("Player"))
            {
                return RedirectToAction("Player");
            }

            if (User.IsInRole("Producer"))
            {
                return RedirectToAction("Producer");
            }

            this.StatusMessage(Messages.AccessDenied, Infra.CrossCutting.Tools.Enums.StatusMessageType.Danger);
            return RedirectToAction("LogOff", "Account");
        }

        [Authorize(Roles = "Player")]
        public ActionResult Player()
        {
            return View();
        }

        [Authorize(Roles = "Producer")]
        public ActionResult Producer()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AcceptTermPlayer(bool Accept)
        {
            if (Accept)
            {
                int userId = User.Identity.GetUserId<int>();

                var vm = new UserUseTermAppViewModel()
                {
                    UserId = userId,
                    Role = "Player"
                };

                var result = _userUseTermAppService.Create(vm);

                if (result.IsValid)
                {
                    return RedirectToAction("ProfileEdit", "Collaborator", new { area = "" });
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        this.StatusMessage(error.Message, Infra.CrossCutting.Tools.Enums.StatusMessageType.Danger);
                    }
                }
            }
            else
            {
                this.StatusMessage(Messages.ToProceedYouMustAcceptTheTerm, Infra.CrossCutting.Tools.Enums.StatusMessageType.Danger);
            }

            return RedirectToAction("Player");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AcceptTermProducer(bool Accept)
        {
            if (Accept)
            {
                int userId = User.Identity.GetUserId<int>();

                var vm = new UserUseTermAppViewModel()
                {
                    UserId = userId,
                    Role = "Producer"
                };

                var result = _userUseTermAppService.Create(vm);

                if (result.IsValid)
                {

                    return RedirectToAction("ProfileEdit", "Collaborator", new { area = "ProducerArea" });
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        this.StatusMessage(error.Message, Infra.CrossCutting.Tools.Enums.StatusMessageType.Danger);
                    }
                }
            }
            else
            {
                this.StatusMessage(Messages.ToProceedYouMustAcceptTheTerm, Infra.CrossCutting.Tools.Enums.StatusMessageType.Danger);
            }

            return RedirectToAction("Producer");
        }
    }
}