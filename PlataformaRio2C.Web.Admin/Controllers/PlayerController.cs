using PlataformaRio2C.Application.Interfaces.Services;
using PlataformaRio2C.Application.ViewModels;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using System;
using System.Linq;
using System.Web.Mvc;

namespace PlataformaRio2C.Web.Admin.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class PlayerController : BaseController
    {
        private readonly IPlayerAppService _appService;
        private readonly ICollaboratorAppService _collaboratorAppService;

        public PlayerController(IPlayerAppService appService, ICollaboratorAppService collaboratorAppService)
        {
            _appService = appService;
            _collaboratorAppService = collaboratorAppService;
        }

        // GET: Player
        public ActionResult Index()
        {
            var viewModel = _appService.GetAllSimple();

            if (viewModel != null)
            {
                viewModel = viewModel.OrderBy(e => e.Name).ToList();
            }

            return View(viewModel);
        }

        // GET: Player/Create
        public ActionResult Create()
        {
            var viewModel = _appService.GetEditViewModel();            
            return View(viewModel);
        }

        // POST: Player/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PlayerEditAppViewModel viewModel)
        {            
            var result = _appService.Create(viewModel);

            if (result.IsValid)
            {
                this.StatusMessage("Player criado com sucesso!", Infra.CrossCutting.Tools.Enums.StatusMessageType.Success);

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

            UpdatePlayerViewModelDefaultValues(viewModel);

            return View(viewModel);
        }

        // GET: Player/Edit
        public ActionResult Edit(Guid Uid)
        {
            var viewModel = _appService.GetByEdit(Uid);         
            
            //var teste = viewModel.per   
            return View(viewModel);
        }

        // POST: Player/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PlayerEditAppViewModel viewModel)
        {            
            var result = _appService.Update(viewModel);

            if (result.IsValid)
            {
                this.StatusMessage("Player atualizado com sucesso!", Infra.CrossCutting.Tools.Enums.StatusMessageType.Success);
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

            UpdatePlayerViewModelDefaultValues(viewModel);

            return View(viewModel);
        }

        // GET: Player/Delete
        public ActionResult Delete(Guid Uid)
        {
            var result = _appService.Delete(Uid);

            if (result.IsValid)
            {
                this.StatusMessage("Player apagado com sucesso!", Infra.CrossCutting.Tools.Enums.StatusMessageType.Success);
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

        // GET: Player/Edit
        [HttpGet]
        public ActionResult EditInterests(Guid Uid)
        {
            var viewModel = _appService.GetEditIntersts(Uid);
            
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditInterests(PlayerEditInterstsAppViewModel viewModel)
        {
            var result = _appService.UpdateEditIntersts(viewModel);

            if (result.IsValid)
            {
                this.StatusMessage("Interesse atualizado com sucesso!", Infra.CrossCutting.Tools.Enums.StatusMessageType.Success);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Erro ao atualizar interesse! Verifique o preenchimento dos campos!");

                foreach (var error in result.Errors)
                {
                    var target = error.Target ?? "";
                    ModelState.AddModelError(target, error.Message);
                }
            }            

            return View(viewModel);
        }

        [HttpPost]
        public JsonResult ListStates(int code)
        {
            var states = _collaboratorAppService.listStates(code);

            return Json(states);
        }


        [HttpPost]
        public JsonResult ListCities(int code)
        {
            var cities = _collaboratorAppService.listCities(code);

            return Json(cities);
        }



        private void UpdatePlayerViewModelDefaultValues(PlayerEditAppViewModel viewModel)
        {
            viewModel.MergeWith<PlayerEditAppViewModel>(_appService.GetEditViewModel());
        }
    }
}