// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Renan Valentim
// Created          : 07-28-2021
//
// Last Modified By : Rafael Ribeiro 
// Last Modified On : 03-28-2025
// ***********************************************************************
// <copyright file="MeetingsController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Application.ViewModels;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Interfaces.Repositories;
using PlataformaRio2C.Infra.CrossCutting.Identity.AuthorizeAttributes;
using PlataformaRio2C.Infra.CrossCutting.Identity.Service;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using PlataformaRio2C.Infra.CrossCutting.Tools.Helpers;
using PlataformaRio2C.Web.Site.Controllers;
using PlataformaRio2C.Web.Site.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Constants = PlataformaRio2C.Domain.Constants;

namespace PlataformaRio2C.Web.Site.Areas.Music.Controllers
{
    /// <summary>
    /// MeetingsController
    /// </summary>
    [AjaxAuthorize(Order = 1)]
    [AuthorizeCollaboratorType(Order = 2, Types = Constants.CollaboratorType.PlayerExecutiveMusic + "," + Constants.CollaboratorType.Industry)]
    public class MeetingsController : BaseController
    {
        
        private readonly IMusicBusinessRoundNegotiationRepository musicBusinessRoundRepo;


        /// <summary>Initializes a new instance of the <see cref="MeetingsController" /> class.</summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="identityController">The identity controller.</param>
        /// <param name="musicBusinessRoundRepo">The negotiation repository.</param>
        public MeetingsController(
            IMediator commandBus,
            IdentityAutenticationService identityController,
            IMusicBusinessRoundNegotiationRepository musicBusinessRoundRepo)
            : base(commandBus, identityController)
        {
          
            this.musicBusinessRoundRepo = musicBusinessRoundRepo;
        }

        #region List

        /// <summary>Indexes this instance.</summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Index(Guid? collaboratorTypeUid)
        {
            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.BusinessRound, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Labels.Music, null),
                new BreadcrumbItemHelper(Labels.ScheduledNegotiations, Url.Action("Index", "Meetings", new { Area = "Music", CollaboratorTypeUid = collaboratorTypeUid }))
            });

            #endregion

            if (!collaboratorTypeUid.HasValue)
            {
                collaboratorTypeUid = this.UserAccessControlDto.EditionCollaboratorTypes.FirstOrDefault()?.Uid;
            }

            ViewBag.CollaboratorTypeUid = collaboratorTypeUid;

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
            if (DateTime.UtcNow < this.EditionDto.OneToOneMeetingsScheduleDate)
            {
                return new JsonResult()
                {
                    Data = new
                    {
                        status = "success",
                        pages = new List<dynamic>
                        {
                            new { page = this.RenderRazorViewToString("Widgets/ScheduledDataWidget", new List<MusicBusinessRoundNegotiationGroupedByDateDto>()), divIdOrClass = "#MusicMeetingsScheduledWidget" },
                        }
                    },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }

            var negotiationsDtos = await this.musicBusinessRoundRepo.FindCollaboratorScheduledWidgetDtoAsync(
                this.EditionDto.Id,
                searchViewModel.BuyerOrganizationUid,
                searchViewModel.SellerOrganizationUid,
                searchViewModel.ProjectKeywords,
                searchViewModel.Date,
                this.UserAccessControlDto?.EditionAttendeeCollaborator?.Uid);

            ViewBag.CollaboratorTypeUid = searchViewModel.CollaboratorTypeUid;

            return new JsonResult()
            {
                Data = new
                {
                    status = "success",
                    pages = new List<dynamic>
                    {
                        new { page = this.RenderRazorViewToString("Widgets/ScheduledDataWidget", negotiationsDtos), divIdOrClass = "#MusicMeetingsScheduledWidget" },
                    }
                },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                MaxJsonLength = Int32.MaxValue
            };
        }

        #endregion

    }
}