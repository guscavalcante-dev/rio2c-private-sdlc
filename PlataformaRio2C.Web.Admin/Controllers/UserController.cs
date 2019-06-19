using PlataformaRio2C.Application.Interfaces.Services;
using PlataformaRio2C.Application.ViewModels;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace PlataformaRio2C.Web.Admin.Controllers
{
    [Authorize(Roles = "Administrator")]
    //[Authorize(Roles = "Administrator", Users = "projeto.rio2c@marlin.com.br")]
    public class UserController : BaseController
    {
        private readonly IUserAppService _appService;

        public UserController(IUserAppService appService, IUserRoleRepository userRoleRepository)
        {
            _appService = appService;
        }

        // GET: User
        public ActionResult Index()
        {
            var viewModel = _appService.All();
            //var viewModel = _appService.listAdmin();

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
        public ActionResult Create(UserAppViewModel viewModel)
        {
            var result = _appService.Create(viewModel);

            if (result.IsValid)
            {
                this.StatusMessage("Usuário criado com sucesso!", Infra.CrossCutting.Tools.Enums.StatusMessageType.Success);

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
            var result = _appService.Get(Uid);

            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserAppViewModel viewModel)
        {
            var result = _appService.Update(viewModel);

            if (result.IsValid)
            {
                this.StatusMessage("Usuário atualizado com sucesso!", Infra.CrossCutting.Tools.Enums.StatusMessageType.Success);
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
        }
    }
}