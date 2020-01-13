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

namespace PlataformaRio2C.Web.Admin.Controllers
{
    [AjaxAuthorize(Order = 1, Roles = Constants.Role.AnyAdmin)]
    [AuthorizeCollaboratorType(Order = 2, Types = Constants.CollaboratorType.AdminAudiovisual + "," + Constants.CollaboratorType.CommissionAudiovisual)]
    public class ReportsController : BaseController
    {
        private readonly IProjectRepository projectRepo;
        private readonly IInterestRepository interestRepo;
        public ReportsController(
            IMediator commandBus,
            IdentityAutenticationService identityController,
            IProjectRepository projectRepository,
            IInterestRepository interestRepository)
            : base(commandBus, identityController)
        {
            this.projectRepo = projectRepository;
            this.interestRepo = interestRepository;
        }

        #region Audiovisual 
        #region Listing
        public async Task<ActionResult> AudiovisualSubscriptions()
        {

            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.Reports, new List<BreadcrumbItemHelper>{
                new BreadcrumbItemHelper(Labels.AudiovisualSubscriptionProjectReport, Url.Action("AudiovisualSubscriptions", "Reports", new { Area = "" }))
            });

            #endregion

            return View("Audiovisual/AudiovisualSubscriptions");
        }


        /// <summary>Shows the main information widget.</summary>
        /// <param name="roomUid">The room uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowAudiovisualSubscriptionsWidget(Guid? roomUid)
        {
            //var mainInformationWidgetDto = await this.projectRepo.FindDtoAsync(roomUid ?? Guid.Empty, this.EditionDto.Id);
            //if (mainInformationWidgetDto == null)
            //{
            //    return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Room, Labels.FoundF.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            //}

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Audiovisual/Widgets/AudiovisualSubscriptionsWidget", new object[]{ }), divIdOrClass = "#ReportAudiovisualSubscriptionWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion
        #endregion
    }
}