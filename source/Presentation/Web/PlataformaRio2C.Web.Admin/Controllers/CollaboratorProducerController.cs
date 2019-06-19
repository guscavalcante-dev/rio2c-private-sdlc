using PlataformaRio2C.Application.Interfaces.Services;
using PlataformaRio2C.Application.ViewModels;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using System;
using System.Linq;
using System.Web.Mvc;

namespace PlataformaRio2C.Web.Admin.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class CollaboratorProducerController : BaseController
    {
        private readonly ICollaboratorProducerAppService _appService;

        public CollaboratorProducerController(ICollaboratorProducerAppService appService)
        {
            _appService = appService;
        }

        // GET: CollaboratorProducer
        public ActionResult Index()
        {
            //var viewModel = _appService.All();

            //if (viewModel != null)
            //{
            //    viewModel = viewModel.OrderBy(e => e.Name).ToList();
            //}        

            return View();
        }

        // GET: CollaboratorProducer/Create
        public ActionResult Create()
        {
            var viewModel = _appService.GetEditViewModel();
            return View(viewModel);
        }

        // POST: CollaboratorProducer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CollaboratorProducerEditAppViewModel viewModel)
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

        // GET: CollaboratorProducer/Edit
        public ActionResult Edit(Guid Uid)
        {
            var result = _appService.GetByEdit(Uid);

            return View(result);
        }

        // POST: CollaboratorProducer/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CollaboratorProducerEditAppViewModel viewModel)
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

        // GET: CollaboratorProducer/Delete
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
    }
}