﻿// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Renan Valentim
// Created          : 01-31-2024
//
// Last Modified By : Renan Valentim
// Last Modified On : 01-31-2024
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

namespace PlataformaRio2C.Web.Admin.Areas.Creator.Controllers
{
    /// <summary>CommissionsController</summary>
    [AjaxAuthorize(Order = 1, Roles = Constants.Role.AnyAdmin)]
    [AuthorizeCollaboratorType(Order = 2, Types = Constants.CollaboratorType.AdminCreator)]
    public class CommissionsController : BaseController
    {
        private readonly ICollaboratorRepository collaboratorRepo;
        private readonly IAttendeeCollaboratorRepository attendeeCollaboratorRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommissionsController" /> class.
        /// </summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="identityController">The identity controller.</param>
        /// <param name="collaboratorRepository">The collaborator repository.</param>
        /// <param name="attendeeCollaboratorRepository">The attendee collaborator repository.</param>
        public CommissionsController(
            IMediator commandBus,
            IdentityAutenticationService identityController,
            ICollaboratorRepository collaboratorRepository,
            IAttendeeCollaboratorRepository attendeeCollaboratorRepository)
            : base(commandBus, identityController)
        {
            this.collaboratorRepo = collaboratorRepository;
            this.attendeeCollaboratorRepo = attendeeCollaboratorRepository;
        }

        #region List

        /// <summary>Indexes the specified search view model.</summary>
        /// <param name="searchViewModel">The search view model.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Index(CreatorCommissionSearchViewModel searchViewModel)
        {
            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.CreatorCommission, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Labels.Creator, null),
                new BreadcrumbItemHelper(Labels.Commission, Url.Action("Index", "Commissions", new { Area = "Creator" }))
            });

            #endregion

            return View(searchViewModel);
        }

        #region DataTable Widget

        /// <summary>
        /// Searches the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <param name="showAllParticipants">if set to <c>true</c> [show all participants].</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Search(IDataTablesRequest request, bool showAllEditions, bool showAllParticipants)
        {
            var members = await this.collaboratorRepo.FindAllCreatorCommissionsByDataTable(
                request.Start / request.Length,
                request.Length,
                request.Search?.Value,
                request.GetSortColumns(),
                new List<Guid>(),
                new string[] { Constants.CollaboratorType.CommissionCreator },
                showAllEditions,
                showAllParticipants,
                null,
                this.EditionDto?.Id
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
                return RedirectToAction("Index", "Commissions", new { Area = "Creator" });
            }

            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.CreatorCommission, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Labels.Creator, null),
                new BreadcrumbItemHelper(Labels.Commission, Url.Action("Index", "Commissions", new { Area = "Creator" })),
                new BreadcrumbItemHelper(attendeeCollaboratorDto.Collaborator.GetFullName(), Url.Action("Details", "Commissions", new { Area = "Creator", id }))
            });

            #endregion

            return View(attendeeCollaboratorDto);
        }

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
            throw new NotImplementedException();
            //var innovationEvaluationsWidgetDto = await this.attendeeCollaboratorRepo.FindCreatorEvaluationsWidgetDtoAsync(collaboratorUid ?? Guid.Empty, this.EditionDto.Id);
            //if (innovationEvaluationsWidgetDto == null)
            //{
            //    return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Member, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            //}

            //ViewBag.ApprovedAttendeeCreatorOrganizationsIds = await this.attendeeCreatorOrganizationRepo.FindAllApprovedAttendeeCreatorOrganizationsIdsAsync(this.EditionDto.Edition.Id);

            //return Json(new
            //{
            //    status = "success",
            //    pages = new List<dynamic>
            //    {
            //        new { page = this.RenderRazorViewToString("Widgets/EvaluationsWidget", innovationEvaluationsWidgetDto), divIdOrClass = "#CreatorCommissionEvaluationsWidget" },
            //    }
            //}, JsonRequestBehavior.AllowGet);
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
                #region Initial Validations

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

                #endregion

                List<string> errors = new List<string>();
                foreach (var collaboratorDto in collaboratorsDtos)
                {
                    var collaboratorLanguageCode = collaboratorDto.Language?.Code ?? this.UserInterfaceLanguage;

                    try
                    {
                        result = await this.CommandBus.Send(new SendCreatorCommissionWelcomeEmailAsync(
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
                Constants.CollaboratorType.CommissionCreator,
                null,
                true,
                this.EditionDto.Id);

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/TotalCountWidget", executivesCount), divIdOrClass = "#CreatorCommissionsTotalCountWidget" },
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
                Constants.CollaboratorType.CommissionCreator,
                null,
                false,
                this.EditionDto.Id);

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/EditionCountWidget", executivesCount), divIdOrClass = "#CreatorCommissionsEditionCountWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Create

        /// <summary>
        /// Shows the create modal.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowCreateModal()
        {
            CreateCreatorCommissionCollaborator cmd;

            try
            {
                cmd = new CreateCreatorCommissionCollaborator();
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
        public async Task<ActionResult> Create(CreateCreatorCommissionCollaborator cmd)
        {
            var result = new AppValidationResult();

            try
            {
                if (!ModelState.IsValid)
                {
                    throw new DomainException(Messages.CorrectFormValues);
                }

                cmd.UpdatePreSendProperties(
                    Constants.CollaboratorType.CommissionCreator,
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
            UpdateCreatorCommissionCollaborator cmd;

            try
            {
                cmd = new UpdateCreatorCommissionCollaborator(
                    await this.CommandBus.Send(new FindCollaboratorDtoByUidAndByEditionIdAsync(collaboratorUid, this.EditionDto.Id, this.UserInterfaceLanguage)),
                    isAddingToCurrentEdition);
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
        public async Task<ActionResult> Update(UpdateCreatorCommissionCollaborator cmd)
        {
            var result = new AppValidationResult();

            try
            {
                if (!ModelState.IsValid)
                {
                    throw new DomainException(Messages.CorrectFormValues);
                }

                cmd.UpdatePreSendProperties(
                    Constants.CollaboratorType.CommissionCreator,
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
                    Constants.CollaboratorType.CommissionCreator,
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