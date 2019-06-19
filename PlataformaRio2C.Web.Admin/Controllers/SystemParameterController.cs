using PlataformaRio2C.Infra.CrossCutting.SystemParameter;
using PlataformaRio2C.Infra.CrossCutting.SystemParameter.ViewModels;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace PlataformaRio2C.Web.Admin.Controllers
{
    [Authorize(Roles = "Administrator")]
    //[Authorize(Roles = "Administrator", Users = "projeto.rio2c@marlin.com.br")]
    public class SystemParameterController : BaseController
    {
        private readonly ISystemParameterAppService _systemParameterAppService;

        public SystemParameterController(ISystemParameterAppService systemParameterAppService)
        {
            _systemParameterAppService = systemParameterAppService;
        }

        // GET: SystemParameter
        public ActionResult Index()
        {
            var systemParameters = _systemParameterAppService.All(true);
            return View(systemParameters);
        }

        public ActionResult ReIndex()
        {
            _systemParameterAppService.ReIndex();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(List<SystemParameterAppViewModel> systemParameterViewModels)
        {
            var result = _systemParameterAppService.UpdateAll(systemParameterViewModels);
            if (result.IsValid)
            {
                this.StatusMessage("Parametros atualizados com sucesso!", Infra.CrossCutting.Tools.Enums.StatusMessageType.Success);
            }
            else if (result.Errors != null && result.Errors.Any())
            {
                foreach (var error in result.Errors)
                {
                    this.StatusMessage(error.Message, Infra.CrossCutting.Tools.Enums.StatusMessageType.Danger);
                }
            }

            return RedirectToAction("Index");
        }
    }
}