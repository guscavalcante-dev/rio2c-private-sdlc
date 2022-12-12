// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Renan Valentim
// Created          : 07-28-2021
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 10-01-2021
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
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Constants = PlataformaRio2C.Domain.Constants;

namespace PlataformaRio2C.Web.Site.Areas.Audiovisual.Controllers
{
    /// <summary>
    /// MeetingsController
    /// </summary>
    [AjaxAuthorize(Order = 1)]
    [AuthorizeCollaboratorType(Order = 2, Types = Constants.CollaboratorType.AudiovisualPlayerExecutive + "," + Constants.CollaboratorType.Industry)]
    public class MeetingsController : BaseController
    {
        private readonly INegotiationRepository negotiationRepo;

        /// <summary>Initializes a new instance of the <see cref="MeetingsController" /> class.</summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="identityController">The identity controller.</param>
        /// <param name="negotiationRepository">The negotiation repository.</param>
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
        public async Task<ActionResult> Index(Guid? collaboratorTypeUid)
        {
            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.BusinessRound, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Labels.AudioVisual, null),
                new BreadcrumbItemHelper(Labels.ScheduledNegotiations, Url.Action("Index", "Meetings", new { Area = "Audiovisual", CollaboratorTypeUid = collaboratorTypeUid }))
            });

            #endregion

            if (!collaboratorTypeUid.HasValue)
            {
                collaboratorTypeUid = this.UserAccessControlDto.EditionCollaboratorTypes.FirstOrDefault().Uid;
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
            var negotiationsDtos = await this.negotiationRepo.FindCollaboratorScheduledWidgetDtoAsync(
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
                        new { page = this.RenderRazorViewToString("Widgets/ScheduledDataWidget", negotiationsDtos), divIdOrClass = "#AudiovisualMeetingsScheduledWidget" },
                    }
                },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                MaxJsonLength = Int32.MaxValue
            };
        }

        #endregion

        #region Details

        /// <summary>
        /// Detailses the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<ActionResult> Details(Guid? id, Guid? collaboratorTypeUid)
        {
            var negotiationDto = await this.negotiationRepo.FindDtoAsync(id ?? Guid.Empty);
            if (negotiationDto == null)
            {
                this.StatusMessageToastr(string.Format(Messages.EntityNotAction, Labels.OneToOneMeeting, Labels.FoundF.ToLowerInvariant()), Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);
                return RedirectToAction("Index", "Meetings", new { Area = "Audiovisual", CollaboratorTypeUid = collaboratorTypeUid });
            }

            if (this.EditionDto?.IsAudiovisualProjectNegotiationsStarted() != true)
            {
                return Json(new { status = "error", message = Messages.NegotiationPeriodClosed }, JsonRequestBehavior.AllowGet);
            }

            if (negotiationDto?.ProjectBuyerEvaluationDto?.BuyerAttendeeOrganizationDto?.Organization?.IsVirtualMeeting == false)
            {
                this.StatusMessageToastr(Messages.AccessDenied, Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);
                return RedirectToAction("Index", "Meetings", new { Area = "Audiovisual", CollaboratorTypeUid = collaboratorTypeUid });
            }

            if (negotiationDto?.ProjectBuyerEvaluationDto?.BuyerAttendeeOrganizationDto?.AttendeeCollaboratorDtos?
                    .Any(acDto => acDto?.AttendeeCollaborator?.Id == this.UserAccessControlDto?.EditionAttendeeCollaborator?.Id) == false &&
                negotiationDto?.ProjectBuyerEvaluationDto?.ProjectDto?.SellerAttendeeOrganizationDto?.AttendeeCollaboratorDtos?
                    .Any(acDto => acDto?.AttendeeCollaborator?.Id == this.UserAccessControlDto?.EditionAttendeeCollaborator?.Id) == false)
            {
                this.StatusMessageToastr(Messages.AccessDenied, Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);
                return RedirectToAction("Index", "Meetings", new { Area = "Audiovisual", CollaboratorTypeUid = collaboratorTypeUid });
            }

            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.OneToOneMeetings, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Labels.AudioVisual, null),
                new BreadcrumbItemHelper(Labels.ScheduledNegotiations, Url.Action("Index", "Meetings", new { Area = "Audiovisual" })),
                new BreadcrumbItemHelper(negotiationDto.RoomDto.GetRoomNameByLanguageCode(this.UserInterfaceLanguage)?.RoomName?.Value, Url.Action("Details", "Meetings", new { Area = "Audiovisual", id }))
            });

            #endregion

            return View(negotiationDto);
        }

        #region Main Information Widget

        /// <summary>
        /// Shows the main information widget.
        /// </summary>
        /// <param name="negotiationUid">The negotiation uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowMainInformationWidget(Guid? negotiationUid)
        {
            var negotiationDto = await this.negotiationRepo.FindMainInformationWidgetDtoAsync(negotiationUid ?? Guid.Empty);
            if (negotiationDto == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Negotiation, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            if (negotiationDto?.ProjectBuyerEvaluationDto?.BuyerAttendeeOrganizationDto?.AttendeeCollaboratorDtos?
                    .Any(acDto => acDto?.AttendeeCollaborator?.Id == this.UserAccessControlDto?.EditionAttendeeCollaborator?.Id) == false &&
                negotiationDto?.ProjectBuyerEvaluationDto?.ProjectDto?.SellerAttendeeOrganizationDto?.AttendeeCollaboratorDtos?
                    .Any(acDto => acDto?.AttendeeCollaborator?.Id == this.UserAccessControlDto?.EditionAttendeeCollaborator?.Id) == false)
            {
                this.StatusMessageToastr(Messages.AccessDenied, Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);
                return RedirectToAction("Index", "Meetings", new { Area = "Audiovisual" });
            }

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/MainInformationWidget", negotiationDto), divIdOrClass = "#AudiovisualMeetingsMainInformationWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Virtual Meeting Widget

        /// <summary>
        /// Shows the main information widget.
        /// </summary>
        /// <param name="negotiationUid">The negotiation uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowVirtualMeetingWidget(Guid? negotiationUid)
        {
            var negotiationDto = await this.negotiationRepo.FindVirtualMeetingWidgetDtoAsync(negotiationUid ?? Guid.Empty);
            if (negotiationDto == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Negotiation, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            if (negotiationDto?.ProjectBuyerEvaluationDto?.BuyerAttendeeOrganizationDto?.AttendeeCollaboratorDtos?
                   .Any(acDto => acDto?.AttendeeCollaborator?.Id == this.UserAccessControlDto?.EditionAttendeeCollaborator?.Id) == false &&
               negotiationDto?.ProjectBuyerEvaluationDto?.ProjectDto?.SellerAttendeeOrganizationDto?.AttendeeCollaboratorDtos?
                   .Any(acDto => acDto?.AttendeeCollaborator?.Id == this.UserAccessControlDto?.EditionAttendeeCollaborator?.Id) == false)
            {
                this.StatusMessageToastr(Messages.AccessDenied, Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);
                return RedirectToAction("Index", "Meetings", new { Area = "Audiovisual" });
            }

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/VirtualMeetingWidget", negotiationDto), divIdOrClass = "#AudiovisualMeetingsVirtualMeetingWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #endregion
    }
}