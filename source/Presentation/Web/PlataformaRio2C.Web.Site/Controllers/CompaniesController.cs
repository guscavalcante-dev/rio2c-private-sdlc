// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 10-08-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 10-10-2019
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
using PlataformaRio2C.Application;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Application.CQRS.Queries;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.CrossCutting.Identity.AuthorizeAttributes;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions;
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
        private readonly IInterestRepository interestRepo;

        /// <summary>Initializes a new instance of the <see cref="CompaniesController"/> class.</summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="identityController">The identity controller.</param>
        /// <param name="attendeeOrganizationRepository">The attendee organization repository.</param>
        /// <param name="activityRepository">The activity repository.</param>
        /// <param name="targetAudienceRepository">The target audience repository.</param>
        /// <param name="interestRepository">The interest repository.</param>
        public CompaniesController(
            IMediator commandBus, 
            IdentityAutenticationService identityController,
            IAttendeeOrganizationRepository attendeeOrganizationRepository,
            IActivityRepository activityRepository,
            ITargetAudienceRepository targetAudienceRepository,
            IInterestRepository interestRepository)
            : base(commandBus, identityController)
        {
            this.attendeeOrganizationRepo = attendeeOrganizationRepository;
            this.activityRepo = activityRepository;
            this.targetAudienceRepo = targetAudienceRepository;
            this.interestRepo = interestRepository;
        }

        /// <summary>Detailses the specified identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Details(Guid? id)
        {
            var attendeeOrganizationDto = await this.attendeeOrganizationRepo.FindSiteDetailstDtoByOrganizationUidAndByEditionIdAsync(id ?? Guid.Empty, this.EditionDto.Id);
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

            return View(attendeeOrganizationDto);
        }

        #region Main Information Widget

        /// <summary>Shows the main information widget.</summary>
        /// <param name="organizationUid">The organization uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowMainInformationWidget(Guid? organizationUid)
        {
            var mainInformationWidgetDto = await this.attendeeOrganizationRepo.FindSiteMainInformationWidgetDtoByOrganizationUidAndByEditionIdAsync(organizationUid ?? Guid.Empty, this.EditionDto.Id);
            if (mainInformationWidgetDto == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Company, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/MainInformationWidget", mainInformationWidgetDto), divIdOrClass = "#CompanyMainInformationWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #region Update

        /// <summary>Shows the update main information modal.</summary>
        /// <param name="organizationUid">The organization uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowUpdateMainInformationModal(Guid? organizationUid)
        {
            UpdateOrganizationMainInformation cmd;

            try
            {
                var mainInformationWidgetDto = await this.attendeeOrganizationRepo.FindSiteMainInformationWidgetDtoByOrganizationUidAndByEditionIdAsync(organizationUid ?? Guid.Empty, this.EditionDto.Id);
                if (mainInformationWidgetDto == null)
                {
                    throw new DomainException(string.Format(Messages.EntityNotAction, Labels.Company, Labels.FoundM.ToLowerInvariant()));
                }

                if (this.UserAccessControlDto?.HasEditionAttendeeOrganization(mainInformationWidgetDto.AttendeeOrganization.Uid) != true)
                {
                    throw new DomainException(Texts.ForbiddenErrorMessage);
                }

                cmd = new UpdateOrganizationMainInformation(
                    mainInformationWidgetDto,
                    await this.CommandBus.Send(new FindAllLanguagesDtosAsync(this.UserInterfaceLanguage)),
                    true,
                    true);
            }
            catch (DomainException ex)
            {
                return Json(new { status = "error", message = ex.GetInnerMessage() }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Modals/UpdateMainInformationModal", cmd), divIdOrClass = "#GlobalModalContainer" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>Updates the main information.</summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> UpdateMainInformation(UpdateOrganizationMainInformation cmd)
        {
            var result = new AppValidationResult();

            try
            {
                if (!ModelState.IsValid)
                {
                    throw new DomainException(Messages.CorrectFormValues);
                }

                cmd.UpdatePreSendProperties(
                    this.UserAccessControlDto.User.Id,
                    this.UserAccessControlDto.User.Uid,
                    this.EditionDto.Id,
                    this.EditionDto.Uid,
                    this.UserInterfaceLanguage);
                result = await this.CommandBus.Send(cmd);
                if (!result.IsValid)
                {
                    throw new DomainException(Messages.CorrectFormValues);
                }
            }
            catch (DomainException ex)
            {
                foreach (var error in result.Errors)
                {
                    var target = error.Target ?? "";
                    ModelState.AddModelError(target, error.Message);
                }

                //cmd.UpdateModelsAndLists(
                //    await this.interestRepo.FindAllGroupedByInterestGroupsAsync());

                return Json(new
                {
                    status = "error",
                    message = ex.GetInnerMessage(),
                    pages = new List<dynamic>
                    {
                        new { page = this.RenderRazorViewToString("Modals/UpdateMainInformationForm", cmd), divIdOrClass = "#form-container" },
                    }
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Elmah.ErrorLog.GetDefault(System.Web.HttpContext.Current).Log(new Elmah.Error(ex));
                return Json(new { status = "error", message = Messages.WeFoundAndError, }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Company, Labels.UpdatedF) });
        }

        #endregion

        #endregion

        #region Activity Widget

        /// <summary>Shows the executive widget.</summary>
        /// <param name="organizationUid">The organization uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowExecutiveWidget(Guid? organizationUid)
        {
            var executiveWidget = await this.attendeeOrganizationRepo.FindSiteExecutiveWidgetDtoByOrganizationUidAndByEditionIdAsync(organizationUid ?? Guid.Empty, this.EditionDto.Id);
            if (executiveWidget == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Executive, Labels.FoundF.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/ExecutiveWidget", executiveWidget), divIdOrClass = "#CompanyExecutiveWidget" },
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
            var addressWidgetDto = await this.attendeeOrganizationRepo.FindSiteAddressWidgetDtoByOrganizationUidAndByEditionIdAsync(organizationUid ?? Guid.Empty, this.EditionDto.Id);
            if (addressWidgetDto == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Address, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            if (this.UserAccessControlDto?.HasEditionAttendeeOrganization(addressWidgetDto.AttendeeOrganization.Uid) != true)
            {
                return Json(new { status = "error", message = Texts.ForbiddenErrorMessage }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/AddressWidget", addressWidgetDto), divIdOrClass = "#CompanyAddressWidget" },
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
            var activityWidgetDto = await this.attendeeOrganizationRepo.FindSiteActivityWidgetDtoByOrganizationUidAndByEditionIdAsync(organizationUid ?? Guid.Empty, this.EditionDto.Id);
            if (activityWidgetDto == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Activity, Labels.FoundF.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            ViewBag.Activities = await this.activityRepo.FindAllAsync();

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/ActivityWidget", activityWidgetDto), divIdOrClass = "#CompanyActivityWidget" },
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
            var targetAudienceWidgetDto = await this.attendeeOrganizationRepo.FindSiteTargetAudienceWidgetDtoByOrganizationUidAndByEditionIdAsync(organizationUid ?? Guid.Empty, this.EditionDto.Id);
            if (targetAudienceWidgetDto == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.TargetAudience, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            ViewBag.TargetAudiences = await this.targetAudienceRepo.FindAllAsync();

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/TargetAudienceWidget", targetAudienceWidgetDto), divIdOrClass = "#CompanyTargetAudienceWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Interest Widget

        /// <summary>Shows the interest widget.</summary>
        /// <param name="organizationUid">The organization uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowInterestWidget(Guid? organizationUid)
        {
            var interestWidgetDto = await this.attendeeOrganizationRepo.FindSiteInterestWidgetDtoByOrganizationUidAndByEditionIdAsync(organizationUid ?? Guid.Empty, this.EditionDto.Id);
            if (interestWidgetDto == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Interests, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            ViewBag.GroupedInterests = await this.interestRepo.FindAllGroupedByInterestGroupsAsync();

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/InterestWidget", interestWidgetDto), divIdOrClass = "#CompanyInterestWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}