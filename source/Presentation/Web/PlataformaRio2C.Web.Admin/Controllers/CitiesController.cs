// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 08-23-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-27-2019
// ***********************************************************************
// <copyright file="CitiesController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Threading.Tasks;
using System.Web.Mvc;
using MediatR;
using PlataformaRio2C.Application.CQRS.Queries;
using PlataformaRio2C.Infra.CrossCutting.Identity.AuthorizeAttributes;
using PlataformaRio2C.Infra.CrossCutting.Identity.Service;
using Constants = PlataformaRio2C.Domain.Constants;

namespace PlataformaRio2C.Web.Admin.Controllers
{
    /// <summary>CitiesController</summary>
    [AjaxAuthorize(Constants.Role.AnyAdmin)]
    public class CitiesController : BaseController
    {
        /// <summary>Initializes a new instance of the <see cref="CitiesController"/> class.</summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="identityController">The identity controller.</param>
        public CitiesController(IMediator commandBus, IdentityAutenticationService identityController)
            : base(commandBus, identityController)
        {
        }

        #region Finds

        /// <summary>Finds all by state uid.</summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> FindAllByStateUid(FindAllCitiesBaseDtosByStateUidAsync cmd)
        {
            cmd.UpdatePreSendProperties(this.UserInterfaceLanguage);

            var cities = await this.CommandBus.Send(cmd);

            return Json(new
            {
                status = "success",
                cities
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}