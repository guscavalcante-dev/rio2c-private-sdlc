// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 09-06-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-06-2019
// ***********************************************************************
// <copyright file="StatesController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using PlataformaRio2C.Application.CQRS.Queries;
using PlataformaRio2C.Infra.CrossCutting.Identity.AuthorizeAttributes;
using PlataformaRio2C.Infra.CrossCutting.Identity.Service;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PlataformaRio2C.Web.Site.Controllers
{
    /// <summary>StatesController</summary>
    [AjaxAuthorize]
    public class StatesController : BaseController
    {
        /// <summary>Initializes a new instance of the <see cref="StatesController"/> class.</summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="identityController">The identity controller.</param>
        public StatesController(IMediator commandBus, IdentityAutenticationService identityController)
            : base(commandBus, identityController)
        {
        }

        #region Finds

        /// <summary>Finds all by country uid.</summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> FindAllByCountryUid(FindAllStatesBaseDtosByCountryUidAsync cmd)
        {
            cmd.UpdatePreSendProperties(this.UserInterfaceLanguage);

            var states = await this.CommandBus.Send(cmd);

            return Json(new
            {
                status = "success",
                states
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}