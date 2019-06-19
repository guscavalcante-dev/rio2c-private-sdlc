using PlataformaRio2C.Application.Interfaces.Services;
using PlataformaRio2C.Application.ViewModels;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using System;
using System.Linq;
using System.Web.Mvc;

namespace PlataformaRio2C.Web.Admin.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class RoomController : BaseController
    {
        private readonly IRoomAppService _appService;

        public RoomController(IRoomAppService appService)
        {
            _appService = appService;
        }

        // GET: Holding
        public ActionResult Index()
        {
            var viewModel = _appService.GetAllSimple();

            if (viewModel != null)
            {
                viewModel = viewModel.OrderBy(e => e.Name).ToList();
            }

            return View(viewModel);
        }

        public ActionResult Create()
        {
            var viewModel = _appService.GetEditViewModel();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RoomEditAppViewModel viewModel)
        {
            var result = _appService.Create(viewModel);

            if (result.IsValid)
            {
                this.StatusMessage("Sala criada com sucesso!", Infra.CrossCutting.Tools.Enums.StatusMessageType.Success);

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

            return View(viewModel);
        }

        public ActionResult Edit(Guid Uid)
        {
            var result = _appService.GetByEdit(Uid);

            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RoomEditAppViewModel viewModel)
        {
            var result = _appService.Update(viewModel);

            if (result.IsValid)
            {
                this.StatusMessage("Sala atualizada com sucesso!", Infra.CrossCutting.Tools.Enums.StatusMessageType.Success);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Erro ao atualizar cadastro! Verifique o preenchimento dos campos!");

                foreach (var error in result.Errors)
                {
                    var target = error.Target ?? "";
                    ModelState.AddModelError(target, error.Message);
                }
            }

            return View(viewModel);
        }


        public ActionResult Delete(Guid Uid)
        {
            var result = _appService.Delete(Uid);

            if (result.IsValid)
            {
                this.StatusMessage("Sala apagada com sucesso!", Infra.CrossCutting.Tools.Enums.StatusMessageType.Success);
            }
            else
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