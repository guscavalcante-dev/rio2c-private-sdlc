// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 06-28-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 07-09-2021
// ***********************************************************************
// <copyright file="SystemParameterController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Infra.CrossCutting.SystemParameter.ViewModels;
using System.Collections.Generic;
using System.Web.Mvc;
using MediatR;
using PlataformaRio2C.Infra.CrossCutting.Identity.AuthorizeAttributes;
using PlataformaRio2C.Infra.CrossCutting.Identity.Service;

namespace PlataformaRio2C.Web.Admin.Controllers
{
    /// <summary>
    /// SystemParameterController
    /// </summary>
    //[Authorize(Roles = "Administrator")]
    [AjaxAuthorize(Order = 1, Roles = Domain.Constants.Role.Admin)]
    public class SystemParameterController : BaseController
    {
        //private readonly ISystemParameterAppService _systemParameterAppService;

        /// <summary>Initializes a new instance of the <see cref="SystemParameterController"/> class.</summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="identityController">The identity controller.</param>
        /// <param name="systemParameterAppService">The system parameter application service.</param>
        public SystemParameterController(IMediator commandBus, IdentityAutenticationService identityController/*, ISystemParameterAppService systemParameterAppService*/)
            : base(commandBus, identityController)
        {
            //_systemParameterAppService = systemParameterAppService;
        }

        // GET: SystemParameter
        public ActionResult Index()
        {
            //var systemParameters = _systemParameterAppService.All(true);
            //return View(systemParameters);
            return null;
        }

        public ActionResult ReIndex()
        {
            //_systemParameterAppService.ReIndex();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(List<SystemParameterAppViewModel> systemParameterViewModels)
        {
            //var result = _systemParameterAppService.UpdateAll(systemParameterViewModels);
            //if (result.IsValid)
            //{
            //    this.StatusMessage("Parametros atualizados com sucesso!", Infra.CrossCutting.Tools.Enums.StatusMessageType.Success);
            //}
            //else if (result.Errors != null && result.Errors.Any())
            //{
            //    foreach (var error in result.Errors)
            //    {
            //        this.StatusMessage(error.Message, Infra.CrossCutting.Tools.Enums.StatusMessageType.Danger);
            //    }
            //}

            return RedirectToAction("Index");
        }
    }
}