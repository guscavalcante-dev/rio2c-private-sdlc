// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 10-08-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 10-08-2019
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
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.CrossCutting.Identity.AuthorizeAttributes;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using PlataformaRio2C.Web.Site.Filters;

namespace PlataformaRio2C.Web.Site.Controllers
{
    /// <summary>CompaniesController</summary>
    [AjaxAuthorize(Order = 1)]
    [AuthorizeCollaboratorType(Order = 2)]
    public class CompaniesController : BaseController
    {
        private readonly IAttendeeOrganizationRepository attendeeOrganizationRepo;
        private readonly IActivityRepository activityRepo;
        private readonly ITargetAudienceRepository targetAudienceRepo;
        private readonly IAttendeeCollaboratorTicketRepository attendeeCollaboratorTicketRepo;

        public CompaniesController(
            IMediator commandBus, 
            IdentityAutenticationService identityController,
            IAttendeeOrganizationRepository attendeeOrganizationRepository,
            IActivityRepository activityRepository,
            ITargetAudienceRepository targetAudienceRepository,
            IAttendeeCollaboratorTicketRepository attendeeCollaboratorTicketRepository)
            : base(commandBus, identityController)
        {
            this.attendeeOrganizationRepo = attendeeOrganizationRepository;
            this.activityRepo = activityRepository;
            this.targetAudienceRepo = targetAudienceRepository;
            this.attendeeCollaboratorTicketRepo = attendeeCollaboratorTicketRepository;
        }

        /// <summary>Detailses the specified identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Details(Guid? id)
        {
            var attendeeOrganizationDto = await this.attendeeOrganizationRepo.FindSiteMainInformationBaseDtoByOrganizationUidAndByEditionIdAsync(id ?? Guid.Empty, this.EditionDto.Id);
            if (attendeeOrganizationDto == null)
            {
                this.StatusMessageToastr(string.Format(Messages.EntityNotAction, Labels.Company, Labels.FoundM.ToLowerInvariant()), Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);
                return RedirectToAction("Index", "Home", new { Area = "" });
            }

            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.Company, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Labels.Dashboard, Url.Action("Index", "Home")),
                new BreadcrumbItemHelper(attendeeOrganizationDto.Organization.TradeName, Url.Action("Details", "Companies", new { id }))
            });

            #endregion

            ViewBag.TicketsDtos = await this.attendeeCollaboratorTicketRepo.FindAllDtoByEditionIdAndByCollaboratorId(this.EditionDto.Id, this.UserAccessControlDto.Collaborator?.Id ?? 0);

            return View(attendeeOrganizationDto.Organization.Uid);
        }

        #region Main Information Widget

        /// <summary>Shows the main information widget.</summary>
        /// <param name="organizationUid">The organization uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowMainInformationWidget(Guid? organizationUid)
        {
            var attendeeOrganizationDto = await this.attendeeOrganizationRepo.FindSiteMainInformationBaseDtoByOrganizationUidAndByEditionIdAsync(organizationUid ?? Guid.Empty, this.EditionDto.Id);
            if (attendeeOrganizationDto == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Company, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/MainInformationWidget", attendeeOrganizationDto), divIdOrClass = "#CompanyMainInformationWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Address Widget

        /// <summary>Shows the address widget.</summary>
        /// <param name="organizationUid">The organization uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowAddressWidget(Guid? organizationUid)
        {
            var addressDto = await this.attendeeOrganizationRepo.FindSiteAddressWidgetDtoByOrganizationUidAsync(organizationUid ?? Guid.Empty, this.EditionDto.Id);
            if (addressDto == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Address, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/AddressWidget", addressDto), divIdOrClass = "#CompanyAddressWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Activity Widget

        /// <summary>Shows the activity widget.</summary>
        /// <param name="organizationUid">The organization uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowActivityWidget(Guid? organizationUid)
        {
            var activitiesDtos = await this.attendeeOrganizationRepo.FindSiteActivityWidgetDtoByOrganizationUidAsync(organizationUid ?? Guid.Empty, this.EditionDto.Id);
            if (activitiesDtos == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Address, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            ViewBag.Activities = await this.activityRepo.FindAllAsync();

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/ActivityWidget", activitiesDtos), divIdOrClass = "#CompanyActivityWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Target Audience Widget

        /// <summary>Shows the activity widget.</summary>
        /// <param name="organizationUid">The organization uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowTargetAudienceWidget(Guid? organizationUid)
        {
            var targetAudiencesDtos = await this.attendeeOrganizationRepo.FindSiteTargetAudienceWidgetDtoByOrganizationUidAsync(organizationUid ?? Guid.Empty, this.EditionDto.Id);
            if (targetAudiencesDtos == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Address, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            ViewBag.TargetAudiences = await this.targetAudienceRepo.FindAllAsync();

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/TargetAudienceWidget", targetAudiencesDtos), divIdOrClass = "#CompanyTargetAudienceWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}