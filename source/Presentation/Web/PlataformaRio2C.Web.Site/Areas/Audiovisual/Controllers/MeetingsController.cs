// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Renan Valentim
// Created          : 07-28-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 08-31-2021
// ***********************************************************************
// <copyright file="MeetingsController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using PlataformaRio2C.Application.ViewModels;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.CrossCutting.Identity.AuthorizeAttributes;
using PlataformaRio2C.Infra.CrossCutting.Identity.Service;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using PlataformaRio2C.Infra.CrossCutting.Tools.Helpers;
using PlataformaRio2C.Web.Site.Controllers;
using PlataformaRio2C.Web.Site.Filters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using Constants = PlataformaRio2C.Domain.Constants;

namespace PlataformaRio2C.Web.Site.Areas.Audiovisual.Controllers
{
    /// <summary>MeetingsController</summary>
    [AjaxAuthorize(Order = 1)]
    [AuthorizeCollaboratorType(Order = 2, Types = Constants.CollaboratorType.AudiovisualPlayerExecutive)]
    public class MeetingsController : BaseController
    {
        private readonly INegotiationRepository negotiationRepo;

        public MeetingsController(
            IMediator commandBus,
            IdentityAutenticationService identityController,
            INegotiationRepository negotiationRepository)
            : base(commandBus, identityController)
        {
            this.negotiationRepo = negotiationRepository;
        }

        #region List

        /// <summary>Indexes this instance.</summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.BusinessRound, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Labels.ScheduledNegotiations, Url.Action("Index", "Meetings", new { Area = "Audiovisual" }))
            });

            #endregion

            return View();
        }

        /// <summary>
        /// Shows the scheduled data widget.
        /// </summary>
        /// <param name="searchViewModel">The search view model.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowScheduledDataWidget(ScheduledSearchViewModel searchViewModel)
        {
            var negotiationsDtos = await this.negotiationRepo.FindCollaboratorScheduledWidgetDtoAsync(
                this.EditionDto.Id,
                searchViewModel.BuyerOrganizationUid,
                searchViewModel.SellerOrganizationUid,
                searchViewModel.ProjectKeywords,
                searchViewModel.Date,
                this.UserAccessControlDto?.EditionAttendeeCollaborator?.Uid);

            return new JsonResult()
            {
                Data = new
                {
                    status = "success",
                    pages = new List<dynamic>
                    {
                        new { page = this.RenderRazorViewToString("Widgets/ScheduledDataWidget", negotiationsDtos), divIdOrClass = "#AudiovisualMeetingsScheduledWidget" },
                    }
                },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                MaxJsonLength = Int32.MaxValue
            };
        }

        #endregion
    }
}