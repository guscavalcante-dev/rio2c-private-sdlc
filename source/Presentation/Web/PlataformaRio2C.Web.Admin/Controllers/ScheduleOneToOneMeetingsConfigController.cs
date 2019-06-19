using PlataformaRio2C.Application.Interfaces.Services;
using PlataformaRio2C.Application.ViewModels;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using System.Collections.Generic;
using System.Web.Mvc;

namespace PlataformaRio2C.Web.Admin.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class ScheduleOneToOneMeetingsConfigController : BaseController
    {
        private readonly INegotiationConfigService _scheduleOneToOneMeetingsConfigService;
        public ScheduleOneToOneMeetingsConfigController(INegotiationConfigService scheduleOneToOneMeetingsConfigService)
        {
            _scheduleOneToOneMeetingsConfigService = scheduleOneToOneMeetingsConfigService;
        }

        // GET: ScheduleOneToOneMeetingsConfig
        public ActionResult Index()
        {
            var result = _scheduleOneToOneMeetingsConfigService.GetByEdit();

            if (result != null)
            {
                return View(result);
            }

            return View();
        }

        public ActionResult Update()
        {
            var result = _scheduleOneToOneMeetingsConfigService.GetByEdit();

            if (result != null)
            {
                return View("Index", result);
            }

            return View("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(IEnumerable<NegotiationConfigAppViewModel> datesViewModel)
        {
            var result = _scheduleOneToOneMeetingsConfigService.Update(datesViewModel);

            if (result.IsValid)
            {
                this.StatusMessage("Configurações da rodada de negócio atualizada com sucesso!", Infra.CrossCutting.Tools.Enums.StatusMessageType.Success);

                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Erro ao salvar cadastro! Verifique o preenchimento dos campos!");

                foreach (var error in result.Errors)
                {
                    var target = error.Target ?? "";
                    ModelState.AddModelError(target, error.Message);
                }
            }

            return View("Index", datesViewModel);
        }
    }
}