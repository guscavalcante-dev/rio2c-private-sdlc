// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : William Sergio Almado Junior
// Created          : 01-13-2019
//
// Last Modified By : William Sergio Almado Junior
// Last Modified On : 01-13-2019
// ***********************************************************************
// <copyright file="ReportsController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.CrossCutting.Identity.AuthorizeAttributes;
using PlataformaRio2C.Infra.CrossCutting.Identity.Service;
using PlataformaRio2C.Web.Admin.Filters;
using Constants = PlataformaRio2C.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Web.Mvc;
using PlataformaRio2C.Infra.CrossCutting.Tools.Helpers;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Application.ViewModels;

namespace PlataformaRio2C.Web.Admin.Controllers
{
    [AjaxAuthorize(Order = 1, Roles = Constants.Role.AnyAdmin)]
    [AuthorizeCollaboratorType(Order = 2, Types = Constants.CollaboratorType.AdminAudiovisual + "," + Constants.CollaboratorType.CommissionAudiovisual)]
    public class ReportsController : BaseController
    {
        private readonly IProjectRepository projectRepo;
        private readonly IInterestRepository interestRepo;
        private readonly ITargetAudienceRepository targetAudienceRepo;

        public ReportsController(
            IMediator commandBus,
            IdentityAutenticationService identityController,
            IProjectRepository projectRepository,
            IInterestRepository interestRepository,
            ITargetAudienceRepository targetAudienceRepository)
            : base(commandBus, identityController)
        {
            this.projectRepo = projectRepository;
            this.interestRepo = interestRepository;
            this.targetAudienceRepo = targetAudienceRepository;
        }

        #region Audiovisual 
        #region Listing
        /// <summary>Return the audiovisual subscription projects list</summary>
        /// <param name="searchViewModel"></param>
        /// <returns></returns>
        public async Task<ActionResult> AudiovisualSubscriptions(ReportsAudiovisualSearchViewModel searchViewModel)
        {
            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.Reports, new List<BreadcrumbItemHelper>{
                new BreadcrumbItemHelper(Labels.AudiovisualSubscriptionProjectReport, Url.Action("AudiovisualSubscriptions", "Reports", new { Area = "" }))
            });

            #endregion

            ViewBag.GenreInterests = await this.interestRepo.FindAllDtosByInterestGroupUidAsync(InterestGroup.Genre.Uid);
            ViewBag.TargetAudience = await this.targetAudienceRepo.FindAllAsync();

            return View("Audiovisual/AudiovisualSubscriptions", searchViewModel);
        }


        /// <summary>Return the audiovisual subscription projects list</summary>
        /// <param name="searchViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> ShowAudiovisualSubscriptionsWidget(ReportsAudiovisualSearchViewModel searchViewModel)
        {
            var audiovisualProjectSubscriptionDtos = await this.projectRepo.FindAudiovisualSubscribedProjectsDtosByFilterAndByPageAsync(
                                                                searchViewModel.Search,
                                                                searchViewModel.InterestUid,
                                                                this.EditionDto.Id,
                                                                searchViewModel.IsPitching,
                                                                searchViewModel.TargetAudienceUid,
                                                                searchViewModel.StartDate,
                                                                searchViewModel.EndDate,
                                                                searchViewModel.Page ?? 1,
                                                                searchViewModel.PageSize ?? 10);
            if (audiovisualProjectSubscriptionDtos == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.AudiovisualSubscriptionProjectReport, Labels.FoundF.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            var producer = string.Empty;
            var projectPerProducerCountnt = 0;
            foreach (var item in audiovisualProjectSubscriptionDtos)
            {
                projectPerProducerCountnt = (string.IsNullOrEmpty(producer) || !item.SellerAttendeeOrganizationDto?.Organization?.Name.Equals(producer) == true)
                                                            ? 1 : projectPerProducerCountnt + 1;
                item.ProjectPerProducerCount = projectPerProducerCountnt;
                producer = item.SellerAttendeeOrganizationDto?.Organization?.Name;
            }

            ViewBag.SearchKeywords = searchViewModel.Search;
            ViewBag.InterestUid = searchViewModel.InterestUid;
            ViewBag.IsPitching = searchViewModel.IsPitching;
            ViewBag.TargetAudienceUid = searchViewModel.TargetAudienceUid;
            ViewBag.Page = searchViewModel.Page ?? 1;
            ViewBag.PageSize = searchViewModel.PageSize ?? 10;

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Audiovisual/Widgets/AudiovisualSubscriptionsWidget", audiovisualProjectSubscriptionDtos), divIdOrClass = "#ReportAudiovisualSubscriptionWidget" },
                }
            }, JsonRequestBehavior.AllowGet);

        }
        #endregion
        #endregion
    }
}