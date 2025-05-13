// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Gilson Oliveira
// Created          : 11-22-2024
//
// Last Modified By : Gilson Oliveira
// Last Modified On : 11-22-2024
// ***********************************************************************
// <copyright file="PitchingController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using PlataformaRio2C.Application;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Application.ViewModels;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.CrossCutting.Identity.AuthorizeAttributes;
using PlataformaRio2C.Infra.CrossCutting.Identity.Service;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using PlataformaRio2C.Infra.CrossCutting.Tools.Helpers;
using PlataformaRio2C.Web.Admin.Controllers;
using PlataformaRio2C.Web.Admin.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Constants = PlataformaRio2C.Domain.Constants;

namespace PlataformaRio2C.Web.Admin.Areas.Music.Controllers
{
    /// <summary>PitchingController</summary>
    [AjaxAuthorize(Order = 1, Roles = Constants.Role.AnyAdmin)]
    [AuthorizeCollaboratorType(Order = 2, Types = Constants.CollaboratorType.AdminMusic)]
    public class PitchingController : BaseController
    {
        private readonly ICollaboratorRepository collaboratorRepo;
        private readonly IAttendeeCollaboratorRepository attendeeCollaboratorRepo;
        private readonly IMusicProjectRepository musicProjectRepo;
        private readonly INegotiationConfigRepository negotiationConfigRepo;
        private readonly IProjectBuyerEvaluationRepository projectBuyerEvaluationRepo;
        private readonly IAttendeeMusicBandRepository attendeeMusicBandRepo;

        /// <summary>Initializes a new instance of the <see cref="PitchingController"/> class.</summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="identityController">The identity controller.</param>
        /// <param name="collaboratorRepository">The collaborator repository.</param>
        /// <param name="attendeeCollaboratorRepository">The attendee collaborator repository.</param>
        public PitchingController(
            IMediator commandBus,
            IdentityAutenticationService identityController,
            ICollaboratorRepository collaboratorRepository,
            IAttendeeCollaboratorRepository attendeeCollaboratorRepository,
            IMusicProjectRepository musicProjectRepository,
            INegotiationConfigRepository negotiationConfigRepo,
            IProjectBuyerEvaluationRepository projectBuyerEvaluationRepository,
            IAttendeeMusicBandRepository attendeeMusicBandRepo
        )
            : base(commandBus, identityController)
        {
            this.collaboratorRepo = collaboratorRepository;
            this.attendeeCollaboratorRepo = attendeeCollaboratorRepository;
            this.musicProjectRepo = musicProjectRepository;
            this.negotiationConfigRepo = negotiationConfigRepo;
            this.projectBuyerEvaluationRepo = projectBuyerEvaluationRepository;
            this.attendeeMusicBandRepo = attendeeMusicBandRepo;
        }

        #region List

        /// <summary>Indexes the specified search view model.</summary>
        /// <param name="searchViewModel">The search view model.</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index(MusicCommissionSearchViewModel searchViewModel)
        {
            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.MusicCommission, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Labels.Music, null),
                new BreadcrumbItemHelper(Labels.Commission, Url.Action("Index", "Pitching", new { Area = "Music" }))
            });

            #endregion

            return View(searchViewModel);
        }

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
                    new { page = this.RenderRazorViewToString("Widgets/EditionScheduledCountWidget", scheduledCount), divIdOrClass = "#MusicPitchingEditionScheduledCountWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

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
                    new { page = this.RenderRazorViewToString("Widgets/EditionUnscheduledCountWidget", notScheduledCount), divIdOrClass = "#MusicPitchingEditionUnscheduledCountWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>Shows the status widget.</summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowStatusWidget()
        {
            var musicPitchingStatusWidgetDto = new MusicPitchingStatusWidgetDto(
                await this.attendeeMusicBandRepo.CountByEvaluatorUsersAsync(this.EditionDto.Id)
            );

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/StatusWidget", musicPitchingStatusWidgetDto), divIdOrClass = "#MusicPitchingStatusWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        /// <summary>Updates the specified tiny collaborator.</summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> DistributeMembers(DistributeMembersMusicPitching cmd)
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
                    this.UserInterfaceLanguage
                );
                result = await this.CommandBus.Send(cmd);
                if (!result.IsValid)
                {
                    throw new DomainException(Messages.CorrectFormValues);
                }
            }
            catch (DomainException ex)
            {
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

            return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Member, Labels.Distributed) });
        }
    }
}