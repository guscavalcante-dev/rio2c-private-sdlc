using PlataformaRio2C.Application.Interfaces.Services;
using PlataformaRio2C.Application.ViewModels;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace PlataformaRio2C.Web.Admin.Controllers
{
    public class SpeakerController : BaseController
    {
        private readonly ISpeakerService _service;
        private readonly ISpeakerAppService _appService;
        private readonly ICollaboratorAppService _collaboratorAppService;


        public SpeakerController(ISpeakerService speakerService, ISpeakerAppService appService, ICollaboratorAppService collaboratorAppService)
        {
            _service = speakerService;
            _appService = appService;
            _collaboratorAppService = collaboratorAppService;
        }

        public ActionResult Index()
        {
            var viewModel = _collaboratorAppService.GetAllSimple().ToList();
            List<CollaboratorItemListAppViewModel> entity = new List<CollaboratorItemListAppViewModel>();

            if (viewModel != null)
            {
                int i = 0;
                foreach(CollaboratorItemListAppViewModel collaborator in viewModel)
                {
                    if(collaborator.SpeakerId != null)
                    {
                        entity.Add(collaborator);
                    }

                    i++;
                }
            }

            return View(entity);
        }

        public ActionResult Create()
        {
            var viewModel = _collaboratorAppService.GetEditViewModel();
            return View(viewModel);
        }

        public ActionResult Edit(Guid uid)
        {
            var viewModel = _collaboratorAppService.GetByEdit(uid);
            return View(viewModel);
        }



        [HttpPost]
        public ActionResult Edit(CollaboratorEditAppViewModel viewModel)
        {
            var result = _collaboratorAppService.Update(viewModel);

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

        [HttpPost]
        public ActionResult Create(CollaboratorEditAppViewModel viewModel)
        {
            //var result = _collaboratorAppService.Create(viewModel);
            var result = _appService.Create(viewModel);


            if (result.IsValid)
            {
                //var resultSpeaker = _service.Create();
                //this.StatusMessage("Player criado com sucesso!", Infra.CrossCutting.Tools.Enums.StatusMessageType.Success);

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

        public ActionResult Delete(Guid uid)
        {
            var entity = _appService.Get(uid);
            var result = _collaboratorAppService.Delete(uid);

            var result2 = _appService.Delete(uid);
            bool message = result.IsValid;

            if (result.IsValid)
            {
                this.StatusMessage("Usuário apagado com sucesso!", Infra.CrossCutting.Tools.Enums.StatusMessageType.Success);
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    this.StatusMessage(error.Message, Infra.CrossCutting.Tools.Enums.StatusMessageType.Danger);
                }
            }

            return RedirectToAction("Index");

            //return message;
        }
    }
}