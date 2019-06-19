using PlataformaRio2C.Application.Interfaces.Services;
using PlataformaRio2C.Application.ViewModels;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using System;
using System.Linq;
using System.Web.Mvc;

namespace PlataformaRio2C.Web.Admin.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class CollaboratorPlayerController : BaseController
    {
        private readonly ICollaboratorPlayerAppService _appService;
        private readonly ICollaboratorAppService _collaboratorAppService;

        public CollaboratorPlayerController(ICollaboratorPlayerAppService appService, ICollaboratorAppService collaboratorAppService)
        {
            _appService = appService;
            _collaboratorAppService = collaboratorAppService;
        }

        // GET: CollaboratorPlayer
        public ActionResult Index()
        {
            var viewModel = _appService.All();

            if (viewModel != null)
            {
                viewModel = viewModel.OrderBy(e => e.Name).ToList();
            }

            return View(viewModel);
        }

        // GET: CollaboratorPlayer/Create
        public ActionResult Create()
        {
            var viewModel = _appService.GetEditViewModel();
            return View(viewModel);
        }

        // POST: CollaboratorPlayer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CollaboratorPlayerEditAppViewModel viewModel)
        {
            var result = _appService.Create(viewModel);

            if (result.IsValid)
            {
                this.StatusMessage(string.Format("Colaborador '{0}' criado com sucesso!", viewModel.Name), Infra.CrossCutting.Tools.Enums.StatusMessageType.Success);

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

        // GET: CollaboratorPlayer/Edit
        public ActionResult Edit(Guid Uid)
        {
            var result = _appService.GetByEdit(Uid);

            return View(result);
        }

        // POST: CollaboratorPlayer/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CollaboratorPlayerEditAppViewModel viewModel)
        {
            var result = _appService.Update(viewModel);

            if (result.IsValid)
            {
                this.StatusMessage(string.Format("Colaborador '{0}' atualizado com sucesso!", viewModel.Name), Infra.CrossCutting.Tools.Enums.StatusMessageType.Success);
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

        // GET: CollaboratorPlayer/Delete
        public ActionResult Delete(Guid Uid)
        {
            var viewModel = _appService.Get(Uid);

            var result = _appService.Delete(Uid);

            if (result.IsValid)
            {
                this.StatusMessage(string.Format("Colaborador '{0}' apagado com sucesso!", viewModel.Name), Infra.CrossCutting.Tools.Enums.StatusMessageType.Success);
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

        [HttpGet]
        public ActionResult Schedule(Guid Uid)
        {
            var colllaboratorViewModel = _appService.Get(Uid);

            if (colllaboratorViewModel != null)
            {

            }

            return View(colllaboratorViewModel);
        }

        [HttpGet]
        public ActionResult Export()
        {
            var Player = Index();

            return View(Player);
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
    }
}