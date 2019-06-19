using PlataformaRio2C.Application.Interfaces.Services;
using PlataformaRio2C.Application.ViewModels;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using System;
using System.Linq;
using System.Web.Mvc;

namespace PlataformaRio2C.Web.Admin.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class HoldingController : BaseController
    {
        private readonly IHoldingAppService _appService;

        public HoldingController(IHoldingAppService appService)
        {
            _appService = appService;
        }

        // GET: Holding
        public ActionResult Index()
        {
            //var viewModel = _appService.GetAllSimple();

            //if (viewModel != null)
            //{
            //    viewModel = viewModel.OrderBy(e => e.Name).ToList();
            //}

            return View();
        }

        public ActionResult Create()
        {
            var viewModel = _appService.GetEditViewModel();            
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HoldingAppViewModel viewModel)
        {            
            var result = _appService.Create(viewModel);

            if (result.IsValid)
            {
                this.StatusMessage("Holding criado com sucesso!", Infra.CrossCutting.Tools.Enums.StatusMessageType.Success);

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

            UpdateHoldingViewModelDefaultValues(viewModel);

            return View(viewModel);
        }

        public ActionResult Edit(Guid Uid)
        {
            var result = _appService.Get(Uid);

            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(HoldingAppViewModel viewModel)
        {            
            var result = _appService.Update(viewModel);

            if (result.IsValid)
            {
                this.StatusMessage("Holding atualizado com sucesso!", Infra.CrossCutting.Tools.Enums.StatusMessageType.Success);
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

            UpdateHoldingViewModelDefaultValues(viewModel);

            return View(viewModel);
        }


        public ActionResult Delete(Guid Uid)
        {
            var result = _appService.Delete(Uid);

            if (result.IsValid)
            {
                this.StatusMessage("Holding apagado com sucesso!", Infra.CrossCutting.Tools.Enums.StatusMessageType.Success);
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

        private void UpdateHoldingViewModelDefaultValues(HoldingAppViewModel viewModel)
        {            
            viewModel.MergeWith<HoldingAppViewModel>(_appService.GetEditViewModel());
        }
    }
}