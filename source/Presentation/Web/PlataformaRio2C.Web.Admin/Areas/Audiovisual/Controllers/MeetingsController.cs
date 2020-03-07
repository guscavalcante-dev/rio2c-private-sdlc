// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 03-06-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-07-2020
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
        private readonly INegotiationConfigRepository negotiationConfigRepo;
        private readonly INegotiationRoomConfigRepository negotiationRoomConfigRepo;
        private readonly IRoomRepository roomRepo;
        private readonly IProjectBuyerEvaluationRepository projectBuyerEvaluationRepo;

        /// <summary>Initializes a new instance of the <see cref="MeetingsController"/> class.</summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="identityController">The identity controller.</param>
        /// <param name="negotiationConfigRepository">The negotiation configuration repository.</param>
        /// <param name="negotiationRoomConfigRepository">The negotiation room configuration repository.</param>
        /// <param name="roomRepository">The room repository.</param>
        /// <param name="projectBuyerEvaluationRepository">The project buyer evaluation repository.</param>
        public MeetingsController(
            IMediator commandBus, 
            IdentityAutenticationService identityController,
            INegotiationConfigRepository negotiationConfigRepository,
            INegotiationRoomConfigRepository negotiationRoomConfigRepository,
            IRoomRepository roomRepository,
            IProjectBuyerEvaluationRepository projectBuyerEvaluationRepository)
            : base(commandBus, identityController)
        {
            this.negotiationConfigRepo = negotiationConfigRepository;
            this.negotiationRoomConfigRepo = negotiationRoomConfigRepository;
            this.roomRepo = roomRepository;
            this.projectBuyerEvaluationRepo = projectBuyerEvaluationRepository;
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

        #region Edition Not Scheduled Count Widget

        /// <summary>Shows the edition not scheduled count widget.</summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowEditionNotScheduledCountWidget()
        {
            var notScheduledCount = await this.projectBuyerEvaluationRepo.CountNegotiationNotScheduledAsync(this.EditionDto.Id, false);

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/EditionNotScheduledCountWidget", notScheduledCount), divIdOrClass = "#AudiovisualMeetingsEditionNotScheduledCountWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Status Widget

        /// <summary>Shows the status widget.</summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowStatusWidget()
        {
            //var executivesCount = await this.negotiationConfigRepo.CountAsync(this.EditionDto.Id, true);

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
    }
}