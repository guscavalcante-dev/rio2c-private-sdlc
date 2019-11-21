// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 11-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 11-21-2019
// ***********************************************************************
// <copyright file="NetworksController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Web.Mvc;
using MediatR;
using PlataformaRio2C.Infra.CrossCutting.Identity.Service;
using System.Collections.Generic;
using System.Threading.Tasks;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.CrossCutting.Identity.AuthorizeAttributes;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Helpers;
using PlataformaRio2C.Web.Site.Filters;
using Constants = PlataformaRio2C.Domain.Constants;

namespace PlataformaRio2C.Web.Site.Controllers
{
    /// <summary>NetworksController</summary>
    [AjaxAuthorize(Order = 1)]
    [AuthorizeCollaboratorType(Order = 2, Types = Constants.CollaboratorType.ExecutiveAudiovisual + "," + Constants.CollaboratorType.Industry )]
    public class NetworksController : BaseController
    {
        private readonly IAttendeeCollaboratorRepository attendeeCollaboratorRepo;

        public NetworksController(
            IMediator commandBus, 
            IdentityAutenticationService identityController,
            IAttendeeCollaboratorRepository attendeeCollaboratorRepository)
            : base(commandBus, identityController)
        {
            this.attendeeCollaboratorRepo = attendeeCollaboratorRepository;
        }

        /// <summary>Indexes this instance.</summary>
        /// <returns></returns>
        [HttpGet]
        [AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.Industry)]
        public async Task<ActionResult> Index(int? page = 1, int? pageSize = 15)
        {
            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.NetworkRio2C, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Labels.NetworkRio2C, Url.Action("Index", "Networks", new { Area = "" }))
            });

            #endregion

            var attendeeCollaboratos = await this.attendeeCollaboratorRepo.FindAllNetworkDtoByEditionIdPagedAsync(this.EditionDto.Id, page.Value, pageSize.Value);

            ViewBag.PageSize = pageSize;

            return View(attendeeCollaboratos);
        }

        [AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.ExecutiveAudiovisual + "," + Constants.CollaboratorType.Industry)]
        public async Task<ActionResult> Messages()
        {
            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.NetworkRio2C, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Labels.Messages, Url.Action("Messages", "Networks", new { Area = "" }))
            });

            #endregion

            //var attendeeCollaboratos = await this.attendeeCollaboratorRepo.FindAllNetworkDtoByEditionIdPagedAsync(this.EditionDto.Id, page.Value, pageSize.Value);

            //ViewBag.PageSize = pageSize;

            return View();
        }
    }
}