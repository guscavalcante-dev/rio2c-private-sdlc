// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 12-19-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 09-13-2021
// ***********************************************************************
// <copyright file="CompaniesController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using PlataformaRio2C.Infra.CrossCutting.Identity.Service;
using System.Threading.Tasks;
using System.Web.Mvc;
using MediatR;
using PlataformaRio2C.Infra.CrossCutting.Tools.Helpers;
using System.Collections.Generic;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Application.CQRS.Queries;
using PlataformaRio2C.Infra.CrossCutting.Identity.AuthorizeAttributes;
using PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Application;
using System.Linq;
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Web.Admin.Controllers
{
    /// <summary>CompaniesController</summary>
    [AjaxAuthorize(Order = 1, Roles = Domain.Constants.Role.AnyAdmin)]
    public class CompaniesController : BaseController
    {
        /// <summary>Initializes a new instance of the <see cref="CompaniesController"/> class.</summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="identityController">The identity controller.</param>
        public CompaniesController(
            IMediator commandBus, 
            IdentityAutenticationService identityController)
            : base(commandBus, identityController)
        {
        }

        #region Ticket Buyers Companies Autocomplete

        /// <summary>Shows the ticket buyer filled form.</summary>
        /// <param name="organizationUid">The organization uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowTicketBuyerFilledForm(Guid? organizationUid)
        {
            CreateTicketBuyerOrganizationData cmd;

            try
            {
                cmd = new CreateTicketBuyerOrganizationData(
                    Guid.Empty,
                    organizationUid.HasValue ? await this.CommandBus.Send(new FindOrganizationDtoByUidAsync(organizationUid, this.EditionDto.Id, this.UserInterfaceLanguage)) : null,
                    await this.CommandBus.Send(new FindAllLanguagesDtosAsync(this.UserInterfaceLanguage)),
                    await this.CommandBus.Send(new FindAllCountriesBaseDtosAsync(this.UserInterfaceLanguage)),
                    false,
                    false,
                    false);
            }
            catch (DomainException ex)
            {
                return Json(new { status = "error", message = ex.GetInnerMessage() }, JsonRequestBehavior.AllowGet);
            }

            ModelState.Clear();

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("/Views/Companies/Shared/_TicketBuyerCompanyInfoForm.cshtml", cmd), divIdOrClass = "#form-container" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}