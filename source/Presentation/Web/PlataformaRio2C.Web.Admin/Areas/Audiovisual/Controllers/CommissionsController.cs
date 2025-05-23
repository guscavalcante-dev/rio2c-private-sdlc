﻿// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Renan Valentim
// Created          : 08-14-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 21-03-2025
// ***********************************************************************
// <copyright file="CommissionsController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using DataTables.AspNet.Core;
using DataTables.AspNet.Mvc5;
using MediatR;
using PlataformaRio2C.Application;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Application.CQRS.Queries;
using PlataformaRio2C.Application.ViewModels;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
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

namespace PlataformaRio2C.Web.Admin.Areas.Audiovisual.Controllers
{
    /// <summary>CommissionsController</summary>
    [AjaxAuthorize(Order = 1, Roles = Constants.Role.AnyAdmin)]
    [AuthorizeCollaboratorType(Order = 2, Types = Constants.CollaboratorType.AdminAudiovisual)]
    public class CommissionsController : BaseController
    {
        private readonly ICollaboratorRepository collaboratorRepo;
        private readonly IAttendeeCollaboratorRepository attendeeCollaboratorRepo;
        private readonly IInterestRepository interestRepo;
        private readonly IProjectRepository projectRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommissionsController"/> class.
        /// </summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="identityController">The identity controller.</param>
        /// <param name="collaboratorRepository">The collaborator repository.</param>
        /// <param name="attendeeCollaboratorRepository">The attendee collaborator repository.</param>
        /// <param name="interestRepository">The interest repository.</param>
        /// <param name="projectRepository">The project repository.</param>
        public CommissionsController(
            IMediator commandBus,
            IdentityAutenticationService identityController,
            ICollaboratorRepository collaboratorRepository,
            IAttendeeCollaboratorRepository attendeeCollaboratorRepository,
            IInterestRepository interestRepository,
            IProjectRepository projectRepository
            )
            : base(commandBus, identityController)
        {
            this.collaboratorRepo = collaboratorRepository;
            this.attendeeCollaboratorRepo = attendeeCollaboratorRepository;
            this.interestRepo = interestRepository;
            this.projectRepo = projectRepository;
        }

        #region List

        /// <summary>Indexes the specified search view model.</summary>
        /// <param name="searchViewModel">The search view model.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Index(AudiovisualCommissionSearchViewModel searchViewModel)
        {
            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.AudiovisualCommission, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Labels.AudioVisual, null),
                new BreadcrumbItemHelper(Labels.Commission, Url.Action("Index", "Commissions", new { Area = "Audiovisual" }))
            });

            #endregion

            searchViewModel.UpdateModelsAndLists(
                await this.interestRepo.FindAllByInterestGroupUidAsync(InterestGroup.AudiovisualGenre.Uid),
                this.UserInterfaceLanguage);

            return View(searchViewModel);
        }

        #region DataTable Widget

        /// <summary>
        /// Searches the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <param name="showAllParticipants">if set to <c>true</c> [show all participants].</param>
        /// <param name="interestUid">The interest uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Search(IDataTablesRequest request, bool showAllEditions, bool showAllParticipants, Guid? interestUid)
        {
            var members = await this.collaboratorRepo.FindAllAudiovisualCommissionMembersByDataTable(
                request.Start / request.Length,
                request.Length,
                request.Search?.Value,
                request.GetSortColumns(),
                new string[] { Constants.CollaboratorType.CommissionAudiovisual },
                showAllEditions,
                showAllParticipants,
                this.EditionDto?.Id,
                new List<Guid?>() { interestUid }
            );

            var response = DataTablesResponse.Create(request, members.TotalItemCount, members.TotalItemCount, members);

            return Json(new
            {
                status = "success",
                dataTable = response
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #endregion

        #region Details

        /// <summary>Detailses the specified identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Details(Guid? id)
        {
            var attendeeCollaboratorDto = await this.attendeeCollaboratorRepo.FindSiteDetailstDtoByCollaboratorUidAndByEditionIdAsync(id ?? Guid.Empty, this.EditionDto.Id);
            if (attendeeCollaboratorDto == null)
            {
                this.StatusMessageToastr(string.Format(Messages.EntityNotAction, Labels.Member, Labels.FoundM.ToLowerInvariant()), Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);
                return RedirectToAction("Index", "Commissions", new { Area = "Audiovisual" });
            }

            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.AudiovisualCommission, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Labels.AudioVisual, null),
                new BreadcrumbItemHelper(Labels.Commission, Url.Action("Index", "Commissions", new { Area = "Audiovisual" })),
                new BreadcrumbItemHelper(attendeeCollaboratorDto.Collaborator.GetFullName(), Url.Action("Details", "Commissions", new { Area = "Audiovisual", id }))
            });

            #endregion

            return View(attendeeCollaboratorDto);
        }

        #region Interests Widget

        [HttpGet]
        public async Task<ActionResult> ShowInterestsWidget(Guid? collaboratorUid)
        {
            var interestsWidgetDto = await this.attendeeCollaboratorRepo.FindInterestsWidgetDtoAsync(collaboratorUid ?? Guid.Empty, this.EditionDto.Id);
            if (interestsWidgetDto == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Member, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            ViewBag.InterestsDtos = await this.GetInterestsFromGenreInterestGroupAsync();

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/InterestsWidget", interestsWidgetDto), divIdOrClass = "#AudiovisualCommissionInterestsWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #region Update

        [HttpGet]
        public async Task<ActionResult> ShowUpdateInterestsModal(Guid? collaboratorUid)
        {
            UpdateAudiovisualCollaboratorInterests cmd;

            try
            {
                var attendeeCollaboratorInterestsWidgetDto = await this.attendeeCollaboratorRepo.FindInterestsWidgetDtoAsync(collaboratorUid ?? Guid.Empty, this.EditionDto.Id);
                if (attendeeCollaboratorInterestsWidgetDto == null)
                {
                    throw new DomainException(string.Format(Messages.EntityNotAction, Labels.Member, Labels.FoundM.ToLowerInvariant()));
                }

                cmd = new UpdateAudiovisualCollaboratorInterests(
                    attendeeCollaboratorInterestsWidgetDto,
                    await this.GetInterestsFromGenreInterestGroupAsync());
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
                    new { page = this.RenderRazorViewToString("Modals/UpdateInterestsModal", cmd), divIdOrClass = "#GlobalModalContainer" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>Updates the social networks.</summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> UpdateInterests(UpdateAudiovisualCollaboratorInterests cmd)
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
                    pages = new List<dynamic>
                    {
                        new { page = this.RenderRazorViewToString("Modals/UpdateInterestsForm", cmd), divIdOrClass = "#form-container" },
                    }
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return Json(new { status = "error", message = Messages.WeFoundAndError, }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Genres, Labels.UpdatedM) });
        }

        #endregion

        #endregion

        #region Commission Evaluations Widget

        [HttpGet]
        public async Task<ActionResult> ShowCommissionEvaluationsWidget(Guid? collaboratorUid)
        {
            var audiovisualCommissionEvaluationsWidgetDto = await this.attendeeCollaboratorRepo.FindAudiovisualCommissionEvaluationsWidgetDtoAsync(collaboratorUid ?? Guid.Empty, this.EditionDto.Id);
            if (audiovisualCommissionEvaluationsWidgetDto == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Member, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            ViewBag.ApprovedProjectsIds = await this.projectRepo.FindAllApprovedCommissionProjectsIdsAsync(this.EditionDto.Edition.Id);

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/CommissionEvaluationsWidget", audiovisualCommissionEvaluationsWidgetDto), divIdOrClass = "#AudiovisualCommissionEvaluationsWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #endregion

        #region Send Invitation Emails

        /// <summary>Sends the invitation emails.</summary>
        /// <param name="selectedCollaboratorsUids">The selected collaborators uids.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> SendInvitationEmails(string selectedCollaboratorsUids)
        {
            AppValidationResult result = null;

            try
            {
                if (string.IsNullOrEmpty(selectedCollaboratorsUids))
                {
                    throw new DomainException(Messages.SelectAtLeastOneOption);
                }

                var collaboratorsUids = selectedCollaboratorsUids?.ToListGuid(',');
                if (!collaboratorsUids.Any())
                {
                    throw new DomainException(Messages.SelectAtLeastOneOption);
                }

                var collaboratorsDtos = await this.collaboratorRepo.FindAllCollaboratorsByCollaboratorsUids(this.EditionDto.Id, collaboratorsUids);
                if (collaboratorsDtos?.Any() != true)
                {
                    throw new DomainException(Messages.SelectAtLeastOneOption);
                }

                List<string> errors = new List<string>();
                foreach (var collaboratorDto in collaboratorsDtos)
                {
                    var collaboratorLanguageCode = collaboratorDto.Language?.Code ?? this.UserInterfaceLanguage;

                    try
                    {
                        result = await this.CommandBus.Send(new SendAudiovisualCommissionWelcomeEmailAsync(
                            collaboratorDto.Collaborator.Uid,
                            collaboratorDto.User.SecurityStamp,
                            collaboratorDto.User.Id,
                            collaboratorDto.User.Uid,
                            collaboratorDto.GetFirstName(),
                            collaboratorDto.GetFullName(collaboratorLanguageCode),
                            collaboratorDto.User.Email,
                            this.EditionDto.Edition,
                            this.AdminAccessControlDto.User.Id,
                            collaboratorLanguageCode));
                        if (!result.IsValid)
                        {
                            throw new DomainException(Messages.CorrectFormValues);
                        }
                    }
                    catch (DomainException)
                    {
                        //Cannot stop sending email when exception occurs.
                        errors.AddRange(result.Errors.Select(e => e.Message));
                    }
                    catch (Exception ex)
                    {
                        Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                    }
                }

                if (errors.Any())
                {
                    throw new DomainException(string.Format(Messages.OneOrMoreEmailsNotSend, Labels.WelcomeEmail));
                }
            }
            catch (DomainException ex)
            {
                return Json(new { status = "error", message = ex.GetInnerMessage(), }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return Json(new { status = "error", message = Messages.WeFoundAndError, }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Email.ToLowerInvariant(), Labels.Sent.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Total Count Widget

        /// <summary>Shows the total count widget.</summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowTotalCountWidget()
        {
            var executivesCount = await this.collaboratorRepo.CountAllByDataTable(
                Constants.CollaboratorType.CommissionAudiovisual,
                null,
                true,
                this.EditionDto.Id);

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/TotalCountWidget", executivesCount), divIdOrClass = "#AudiovisualCommissionsTotalCountWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Edition Count Widget

        /// <summary>Shows the edition count widget.</summary>
        /// <returns></returns>
        public async Task<ActionResult> ShowEditionCountWidget()
        {
            var executivesCount = await this.collaboratorRepo.CountAllByDataTable(
                Constants.CollaboratorType.CommissionAudiovisual,
                null,
                false,
                this.EditionDto.Id);

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/EditionCountWidget", executivesCount), divIdOrClass = "#AudiovisualCommissionsEditionCountWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Create

        /// <summary>Shows the create modal.</summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowCreateModal()
        {
            CreateAudiovisualCommissionCollaborator cmd;

            try
            {
                cmd = new CreateAudiovisualCommissionCollaborator(await this.GetInterestsFromGenreInterestGroupAsync());
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
                    new { page = this.RenderRazorViewToString("Modals/CreateModal", cmd), divIdOrClass = "#GlobalModalContainer" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>Creates the specified collaborator.</summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Create(CreateAudiovisualCommissionCollaborator cmd)
        {
            var result = new AppValidationResult();

            try
            {
                if (!ModelState.IsValid)
                {
                    throw new DomainException(Messages.CorrectFormValues);
                }

                cmd.UpdatePreSendProperties(
                    Constants.CollaboratorType.CommissionAudiovisual,
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

                cmd.UpdateDropdownProperties(await this.GetInterestsFromGenreInterestGroupAsync());

                return Json(new
                {
                    status = "error",
                    message = result.Errors?.FirstOrDefault(e => e.Target == "ToastrError")?.Message ?? ex.GetInnerMessage(),
                    pages = new List<dynamic>
                    {
                        new { page = this.RenderRazorViewToString("Modals/_Form", cmd), divIdOrClass = "#form-container" },
                    }
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return Json(new { status = "error", message = Messages.WeFoundAndError, }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Member, Labels.CreatedM) });
        }

        #endregion

        #region Update

        /// <summary>Shows the update modal.</summary>
        /// <param name="collaboratorUid">The collaborator uid.</param>
        /// <param name="isAddingToCurrentEdition">The is adding to current edition.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowUpdateModal(Guid? collaboratorUid, bool? isAddingToCurrentEdition)
        {
            UpdateAudiovisualCommissionCollaborator cmd;

            try
            {
                cmd = new UpdateAudiovisualCommissionCollaborator(
                    await this.CommandBus.Send(new FindCollaboratorDtoByUidAndByEditionIdAsync(collaboratorUid, this.EditionDto.Id, this.UserInterfaceLanguage)),
                    isAddingToCurrentEdition,
                    await this.attendeeCollaboratorRepo.FindInterestsWidgetDtoAsync(collaboratorUid ?? Guid.Empty, this.EditionDto.Id),
                    await this.GetInterestsFromGenreInterestGroupAsync());
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
                    new { page = this.RenderRazorViewToString("Modals/UpdateModal", cmd), divIdOrClass = "#GlobalModalContainer" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>Updates the specified tiny collaborator.</summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Update(UpdateAudiovisualCommissionCollaborator cmd)
        {
            var result = new AppValidationResult();

            try
            {
                if (!ModelState.IsValid)
                {
                    throw new DomainException(Messages.CorrectFormValues);
                }

                cmd.UpdatePreSendProperties(
                    Constants.CollaboratorType.CommissionAudiovisual,
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

                cmd.UpdateDropdownProperties(await this.GetInterestsFromGenreInterestGroupAsync());

                return Json(new
                {
                    status = "error",
                    message = result.Errors?.FirstOrDefault(e => e.Target == "ToastrError")?.Message ?? ex.GetInnerMessage(),
                    pages = new List<dynamic>
                    {
                        new { page = this.RenderRazorViewToString("Modals/_Form", cmd), divIdOrClass = "#form-container" },
                    }
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return Json(new { status = "error", message = Messages.WeFoundAndError, }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Member, Labels.UpdatedM) });
        }

        #endregion

        #region Delete

        /// <summary>Deletes the specified collaborator.</summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Delete(DeleteCollaborator cmd)
        {
            var result = new AppValidationResult();

            try
            {
                if (!ModelState.IsValid)
                {
                    throw new DomainException(Messages.CorrectFormValues);
                }

                cmd.UpdatePreSendProperties(
                    Constants.CollaboratorType.CommissionAudiovisual,
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

            return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Member, Labels.DeletedM) });
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets the interests from genre interest group asynchronous.
        /// </summary>
        /// <returns></returns>
        private async Task<List<InterestDto>> GetInterestsFromGenreInterestGroupAsync()
        {
            return await this.interestRepo.FindAllDtosByInterestGroupUidAsync(InterestGroup.AudiovisualPitchingSubGenre.Uid);
        }

        #endregion
    }
}