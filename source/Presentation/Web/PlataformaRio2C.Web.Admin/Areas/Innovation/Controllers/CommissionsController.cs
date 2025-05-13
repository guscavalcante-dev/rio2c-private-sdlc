// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Renan Valentim
// Created          : 07-08-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 12-30-2022
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

namespace PlataformaRio2C.Web.Admin.Areas.Innovation.Controllers
{
    /// <summary>CommissionsController</summary>
    [AjaxAuthorize(Order = 1, Roles = Constants.Role.AnyAdmin)]
    [AuthorizeCollaboratorType(Order = 2, Types = Constants.CollaboratorType.AdminInnovation)]
    public class CommissionsController : BaseController
    {
        private readonly ICollaboratorRepository collaboratorRepo;
        private readonly IAttendeeCollaboratorRepository attendeeCollaboratorRepo;
        private readonly IInnovationOrganizationTrackOptionRepository innovationOrganizationTrackOptionRepo;
        private readonly IInnovationOrganizationTrackOptionGroupRepository innovationOrganizationTrackOptionGroupRepo;
        private readonly IAttendeeInnovationOrganizationRepository attendeeInnovationOrganizationRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommissionsController" /> class.
        /// </summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="identityController">The identity controller.</param>
        /// <param name="collaboratorRepository">The collaborator repository.</param>
        /// <param name="attendeeCollaboratorRepository">The attendee collaborator repository.</param>
        /// <param name="innovationOrganizationTrackOptionRepository">The innovation organization track option repository.</param>
        /// <param name="attendeeInnovationOrganizationRepository">The attendee innovation organization repository.</param>
        /// <param name="innovationOrganizationTrackOptionGroupRepository">The innovation organization track option group repository.</param>
        public CommissionsController(
            IMediator commandBus,
            IdentityAutenticationService identityController,
            ICollaboratorRepository collaboratorRepository,
            IAttendeeCollaboratorRepository attendeeCollaboratorRepository,
            IInnovationOrganizationTrackOptionRepository innovationOrganizationTrackOptionRepository,
            IAttendeeInnovationOrganizationRepository attendeeInnovationOrganizationRepository,
            IInnovationOrganizationTrackOptionGroupRepository innovationOrganizationTrackOptionGroupRepository)
            : base(commandBus, identityController)
        {
            this.collaboratorRepo = collaboratorRepository;
            this.attendeeCollaboratorRepo = attendeeCollaboratorRepository;
            this.innovationOrganizationTrackOptionRepo = innovationOrganizationTrackOptionRepository;
            this.attendeeInnovationOrganizationRepo = attendeeInnovationOrganizationRepository;
            this.innovationOrganizationTrackOptionGroupRepo = innovationOrganizationTrackOptionGroupRepository;
        }

        #region List

        /// <summary>Indexes the specified search view model.</summary>
        /// <param name="searchViewModel">The search view model.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Index(InnovationCommissionSearchViewModel searchViewModel)
        {
            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.StartupsCommission, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Labels.Startups, null),
                new BreadcrumbItemHelper(Labels.Commission, Url.Action("Index", "Commissions", new { Area = "Innovation" }))
            });

            #endregion

            searchViewModel.UpdateModelsAndLists(
                await this.innovationOrganizationTrackOptionGroupRepo.FindAllAsync(),
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
        /// <param name="innovationOrganizationTrackOptionGroupUid">The innovation organization track option uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Search(IDataTablesRequest request, bool showAllEditions, bool showAllParticipants, Guid? innovationOrganizationTrackOptionGroupUid)
        {
            var members = await this.collaboratorRepo.FindAllInnovationCommissionsByDataTable(
                request.Start / request.Length,
                request.Length,
                request.Search?.Value,
                request.GetSortColumns(),
                new List<Guid>(),
                new string[] { Constants.CollaboratorType.CommissionInnovation },
                showAllEditions,
                showAllParticipants,
                null,
                this.EditionDto?.Id,
                new List<Guid?>() { innovationOrganizationTrackOptionGroupUid }
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
                return RedirectToAction("Index", "Commissions", new { Area = "Innovation" });
            }

            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.StartupsCommission, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Labels.Startups, null),
                new BreadcrumbItemHelper(Labels.Commission, Url.Action("Index", "Commissions", new { Area = "Innovation" })),
                new BreadcrumbItemHelper(attendeeCollaboratorDto.Collaborator.GetFullName(), Url.Action("Details", "Commissions", new { Area = "Innovation", id }))
            });

            #endregion

            return View(attendeeCollaboratorDto);
        }

        #endregion

        #region Tracks Widget

        /// <summary>
        /// Shows the tracks widget.
        /// </summary>
        /// <param name="collaboratorUid">The collaborator uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowTracksWidget(Guid? collaboratorUid)
        {
            var tracksWidgetDto = await this.attendeeCollaboratorRepo.FindTracksWidgetDtoAsync(collaboratorUid ?? Guid.Empty, this.EditionDto.Id);
            if (tracksWidgetDto == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Member, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            ViewBag.InnovationOrganizationTrackOptionGroupDtos = await this.innovationOrganizationTrackOptionGroupRepo.FindAllDtoAsync();

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/TracksWidget", tracksWidgetDto), divIdOrClass = "#InnovationCommissionTracksInfoWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #region Update

        /// <summary>
        /// Shows the update tracks modal.
        /// </summary>
        /// <param name="collaboratorUid">The collaborator uid.</param>
        /// <returns></returns>
        /// <exception cref="PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions.DomainException"></exception>
        [HttpGet]
        public async Task<ActionResult> ShowUpdateTracksModal(Guid? collaboratorUid)
        {
            UpdateInnovationCollaboratorTracks cmd;

            try
            {
                var attendeeCollaboratorTracksWidgetDto = await this.attendeeCollaboratorRepo.FindTracksWidgetDtoAsync(collaboratorUid ?? Guid.Empty, this.EditionDto.Id);
                if (attendeeCollaboratorTracksWidgetDto == null)
                {
                    throw new DomainException(string.Format(Messages.EntityNotAction, Labels.Member, Labels.FoundM.ToLowerInvariant()));
                }

                cmd = new UpdateInnovationCollaboratorTracks(
                    attendeeCollaboratorTracksWidgetDto,
                    await this.innovationOrganizationTrackOptionRepo.FindAllDtoAsync());
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
                    new { page = this.RenderRazorViewToString("Modals/UpdateTracksModal", cmd), divIdOrClass = "#GlobalModalContainer" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Updates the tracks.
        /// </summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        /// <exception cref="PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions.DomainException"></exception>
        [HttpPost]
        public async Task<ActionResult> UpdateTracks(UpdateInnovationCollaboratorTracks cmd)
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
                        new { page = this.RenderRazorViewToString("Modals/UpdateTracksForm", cmd), divIdOrClass = "#form-container" },
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

        #endregion

        #region Evaluations Widget

        /// <summary>
        /// Shows the evaluations widget.
        /// </summary>
        /// <param name="collaboratorUid">The collaborator uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowEvaluationsWidget(Guid? collaboratorUid)
        {
            var innovationEvaluationsWidgetDto = await this.attendeeCollaboratorRepo.FindInnovationEvaluationsWidgetDtoAsync(collaboratorUid ?? Guid.Empty, this.EditionDto.Id);
            if (innovationEvaluationsWidgetDto == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Member, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            ViewBag.ApprovedAttendeeInnovationOrganizationsIds = await this.attendeeInnovationOrganizationRepo.FindAllApprovedAttendeeInnovationOrganizationsIdsAsync(this.EditionDto.Edition.Id);

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/EvaluationsWidget", innovationEvaluationsWidgetDto), divIdOrClass = "#InnovationCommissionEvaluationsWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

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
                        result = await this.CommandBus.Send(new SendInnovationCommissionWelcomeEmailAsync(
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
                Constants.CollaboratorType.CommissionInnovation,
                null,
                true,
                this.EditionDto.Id);

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/TotalCountWidget", executivesCount), divIdOrClass = "#InnovationCommissionsTotalCountWidget" },
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
                Constants.CollaboratorType.CommissionInnovation,
                null,
                false,
                this.EditionDto.Id);

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/EditionCountWidget", executivesCount), divIdOrClass = "#InnovationCommissionsEditionCountWidget" },
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
            CreateInnovationCommissionCollaborator cmd;

            try
            {
                cmd = new CreateInnovationCommissionCollaborator(await this.innovationOrganizationTrackOptionRepo.FindAllDtoAsync());
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
        public async Task<ActionResult> Create(CreateInnovationCommissionCollaborator cmd)
        {
            var result = new AppValidationResult();

            try
            {
                if (!ModelState.IsValid)
                {
                    throw new DomainException(Messages.CorrectFormValues);
                }

                cmd.UpdatePreSendProperties(
                    Constants.CollaboratorType.CommissionInnovation,
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

                cmd.UpdateDropdownProperties(await this.innovationOrganizationTrackOptionRepo.FindAllDtoAsync());

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
            UpdateInnovationCommissionCollaborator cmd;

            try
            {
                cmd = new UpdateInnovationCommissionCollaborator(
                    await this.CommandBus.Send(new FindCollaboratorDtoByUidAndByEditionIdAsync(collaboratorUid, this.EditionDto.Id, this.UserInterfaceLanguage)),
                    isAddingToCurrentEdition,
                    await this.attendeeCollaboratorRepo.FindTracksWidgetDtoAsync(collaboratorUid ?? Guid.Empty, this.EditionDto.Id),
                    await this.innovationOrganizationTrackOptionRepo.FindAllDtoAsync());
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
        public async Task<ActionResult> Update(UpdateInnovationCommissionCollaborator cmd)
        {
            var result = new AppValidationResult();

            try
            {
                if (!ModelState.IsValid)
                {
                    throw new DomainException(Messages.CorrectFormValues);
                }

                cmd.UpdatePreSendProperties(
                    Constants.CollaboratorType.CommissionInnovation,
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

                cmd.UpdateDropdownProperties(await this.innovationOrganizationTrackOptionRepo.FindAllDtoAsync());

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
                    Constants.CollaboratorType.CommissionInnovation,
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
    }
}