// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 03-06-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-08-2020
// ***********************************************************************
// <copyright file="MeetingsController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using MediatR;
using PlataformaRio2C.Application;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.CrossCutting.Identity.AuthorizeAttributes;
using PlataformaRio2C.Infra.CrossCutting.Identity.Service;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions;
using PlataformaRio2C.Infra.CrossCutting.Tools.Helpers;
using PlataformaRio2C.Web.Admin.Controllers;
using PlataformaRio2C.Web.Admin.Filters;
using Constants = PlataformaRio2C.Domain.Constants;

namespace PlataformaRio2C.Web.Admin.Areas.Audiovisual.Controllers
{
    /// <summary>MeetingsController</summary>
    [AjaxAuthorize(Order = 1, Roles = Constants.Role.AnyAdmin)]
    [AuthorizeCollaboratorType(Order = 2, Types = Constants.CollaboratorType.AdminAudiovisual)]
    public class MeetingsController : BaseController
    {
        private readonly INegotiationRepository negotiationRepo;
        private readonly IProjectBuyerEvaluationRepository projectBuyerEvaluationRepo;
        private readonly IRoomRepository roomRepo;

        /// <summary>Initializes a new instance of the <see cref="MeetingsController"/> class.</summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="identityController">The identity controller.</param>
        /// <param name="negotiationRepository">The negotiation repository.</param>
        /// <param name="projectBuyerEvaluationRepository">The project buyer evaluation repository.</param>
        /// <param name="roomRepository">The room repository.</param>
        public MeetingsController(
            IMediator commandBus,
            IdentityAutenticationService identityController,
            INegotiationRepository negotiationRepository,
            IProjectBuyerEvaluationRepository projectBuyerEvaluationRepository,
            IRoomRepository roomRepository)
            : base(commandBus, identityController)
        {
            this.negotiationRepo = negotiationRepository;
            this.projectBuyerEvaluationRepo = projectBuyerEvaluationRepository;
            this.roomRepo = roomRepository;
        }

        #region Generate Agenda

        /// <summary>Indexes the specified search view model.</summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.OneToOneMeetings, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Labels.GenerateCalendar, Url.Action("Index", "Meetings", new { Area = "Audiovisual" }))
            });

            #endregion

            return View();
        }

        #region Status Widget

        /// <summary>Shows the status widget.</summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowStatusWidget()
        {
            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/StatusWidget", null), divIdOrClass = "#AudiovisualMeetingsStatusWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #region Generate

        /// <summary>Generates the specified command.</summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Generate(CreateNegotiations cmd)
        {
            var result = new AppValidationResult();

            try
            {
                if (!ModelState.IsValid)
                {
                    throw new DomainException(Messages.CorrectFormValues);
                }

                cmd.UpdatePreSendProperties(
                    this.AdminAccessControlDto.User.Id,
                    this.AdminAccessControlDto.User.Uid,
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

                return Json(new
                {
                    status = "error",
                    message = result.Errors?.FirstOrDefault(e => e.Target == "ToastrError")?.Message ?? ex.GetInnerMessage(),
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return Json(new { status = "error", message = Messages.WeFoundAndError, }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Agenda, Labels.GeneratedF.ToLowerInvariant()) });
        }

        #endregion

        #endregion

        #endregion

        #region Edition Scheduled Count Widget

        /// <summary>Shows the edition scheduled count widget.</summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowEditionScheduledCountWidget()
        {
            var scheduledCount = await this.projectBuyerEvaluationRepo.CountNegotiationScheduledAsync(this.EditionDto.Id, false);

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/EditionScheduledCountWidget", scheduledCount), divIdOrClass = "#AudiovisualMeetingsEditionScheduledCountWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Edition Unscheduled Count Widget

        /// <summary>Shows the edition unscheduled count widget.</summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowEditionUnscheduledCountWidget()
        {
            var notScheduledCount = await this.projectBuyerEvaluationRepo.CountNegotiationNotScheduledAsync(this.EditionDto.Id, false);

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/EditionUnscheduledCountWidget", notScheduledCount), divIdOrClass = "#AudiovisualMeetingsEditionUnscheduledCountWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Scheduled

        /// <summary>Scheduleds this instance.</summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Scheduled()
        {
            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.OneToOneMeetings, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Labels.ScheduledNegotiations, Url.Action("Scheduled", "Meetings", new { Area = "Audiovisual" }))
            });

            #endregion

            ViewBag.Rooms = (await this.roomRepo.FindAllDtoByEditionIdAsync(this.EditionDto.Id))?.Select(r => new RoomJsonDto
            {
                Id = r.Room.Id,
                Uid = r.Room.Uid,
                Name = r.GetRoomNameByLanguageCode(this.UserInterfaceLanguage)?.RoomName?.Value
            })?.ToList();

            return View();
        }

        #region Scheduled Widget

        /// <summary>Shows the scheduled data widget.</summary>
        /// <param name="buyerOrganizationUid">The buyer organization uid.</param>
        /// <param name="sellerOrganizationUid">The seller organization uid.</param>
        /// <param name="projectKeywords">The project keywords.</param>
        /// <param name="date">The date.</param>
        /// <param name="roomUid">The room uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowScheduledDataWidget(Guid? buyerOrganizationUid, Guid? sellerOrganizationUid, string projectKeywords, DateTime? date, Guid? roomUid)
        {
            var negotiations = await this.negotiationRepo.FindScheduledWidgetDtoAsync(
                this.EditionDto.Id,
                buyerOrganizationUid,
                sellerOrganizationUid,
                projectKeywords,
                date,
                roomUid);

            return new JsonResult()
            {
                Data = new
                {
                    status = "success",
                    pages = new List<dynamic>
                    {
                        new { page = this.RenderRazorViewToString("Widgets/ScheduledDataWidget", negotiations), divIdOrClass = "#AudiovisualMeetingsScheduledWidget" },
                    }
                },
                //ContentType = contentType,
                //ContentEncoding = contentEncoding,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                MaxJsonLength = Int32.MaxValue
            };
        }

        #endregion

        #region Delete

        /// <summary>Deletes the specified delete negotiation.</summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Delete(DeleteNegotiation cmd)
        {
            var result = new AppValidationResult();

            try
            {
                if (!ModelState.IsValid)
                {
                    throw new DomainException(Messages.CorrectFormValues);
                }

                cmd.UpdatePreSendProperties(
                    this.AdminAccessControlDto.User.Id,
                    this.AdminAccessControlDto.User.Uid,
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

                return Json(new
                {
                    status = "error",
                    message = result.Errors?.FirstOrDefault(e => e.Target == "ToastrError")?.Message ?? ex.GetInnerMessage(),
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return Json(new { status = "error", message = Messages.WeFoundAndError, }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Negotiation, Labels.DeletedF) });
        }

        #endregion

        #endregion

        #region Unscheduled

        /// <summary>Unscheduleds this instance.</summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Unscheduled()
        {
            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.OneToOneMeetings, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Labels.UnscheduledNegotiations, Url.Action("Unscheduled", "Meetings", new { Area = "Audiovisual" }))
            });

            #endregion

            return View();
        }

        #region Unscheduled Widget

        /// <summary>Shows the unscheduled widget.</summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowUnscheduledWidget()
        {
            var negotiations = await this.projectBuyerEvaluationRepo.FindUnscheduledWidgetDtoAsync(this.EditionDto.Id);

            return new JsonResult()
            {
                Data = new
                {
                    status = "success",
                    pages = new List<dynamic>
                    {
                        new { page = this.RenderRazorViewToString("Widgets/UnscheduledWidget", negotiations), divIdOrClass = "#AudiovisualMeetingsUnscheduledWidget" },
                    }
                },
                //ContentType = contentType,
                //ContentEncoding = contentEncoding,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                MaxJsonLength = Int32.MaxValue
            };
        }

        #endregion

        #endregion
    }
}