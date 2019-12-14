// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 12-12-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-14-2019
// ***********************************************************************
// <copyright file="SpeakersController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Application.ViewModels;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using DataTables.AspNet.Core;
using DataTables.AspNet.Mvc5;
using MediatR;
using Newtonsoft.Json;
using PlataformaRio2C.Application;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Application.CQRS.Queries;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.CrossCutting.Identity.AuthorizeAttributes;
using PlataformaRio2C.Infra.CrossCutting.Identity.Service;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.SalesPlatforms.Services.Eventbrite.Models;
using PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions;
using PlataformaRio2C.Infra.CrossCutting.Tools.Helpers;
using PlataformaRio2C.Web.Admin.Filters;
using Constants = PlataformaRio2C.Domain.Constants;

namespace PlataformaRio2C.Web.Admin.Controllers
{
    /// <summary>SpeakersController</summary>
    [AjaxAuthorize(Order = 1, Roles = Constants.Role.AnyAdmin)]
    [AuthorizeCollaboratorType(Order = 2, Types = Constants.CollaboratorType.AdminAudiovisual)]
    public class SpeakersController : BaseController
    {
        private readonly ICollaboratorRepository collaboratorRepo;
        private readonly IAttendeeSalesPlatformTicketTypeRepository attendeeSalesPlatformTicketTypeRepo;

        /// <summary>Initializes a new instance of the <see cref="PlayersExecutivesController"/> class.</summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="identityController">The identity controller.</param>
        /// <param name="collaboratorRepository">The collaborator repository.</param>
        /// <param name="attendeeSalesPlatformTicketTypeRepository">The attendee sales platform ticket type repository.</param>
        public SpeakersController(
            IMediator commandBus, 
            IdentityAutenticationService identityController,
            ICollaboratorRepository collaboratorRepository,
            IAttendeeSalesPlatformTicketTypeRepository attendeeSalesPlatformTicketTypeRepository)
            : base(commandBus, identityController)
        {
            this.collaboratorRepo = collaboratorRepository;
            this.attendeeSalesPlatformTicketTypeRepo = attendeeSalesPlatformTicketTypeRepository;
        }

        #region List

        /// <summary>Indexes the specified search view model.</summary>
        /// <param name="searchViewModel">The search view model.</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index(SpeakerSearchViewModel searchViewModel)
        {
            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.Players, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Labels.Speakers, Url.Action("Index", "Speakers", new { Area = "" }))
            });

            #endregion

            return View(searchViewModel);
        }

        #region DataTable Widget

        /// <summary>Searches the specified request.</summary>
        /// <param name="request">The request.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <param name="showAllParticipants">if set to <c>true</c> [show all participants].</param>
        /// <param name="showHighlights">if set to <c>true</c> [show highlights].</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Search(IDataTablesRequest request, bool showAllEditions, bool showAllParticipants, bool? showHighlights)
        {
            var speakers = await this.collaboratorRepo.FindAllByDataTable(
                request.Start / request.Length,
                request.Length,
                request.Search?.Value,
                request.GetSortColumns(),
                this.GetCollaboratorsUids(null),
                Constants.CollaboratorType.Speaker,
                showAllEditions,
                false,
                showAllParticipants,
                showHighlights,
                this.EditionDto?.Id
            );

            var response = DataTablesResponse.Create(request, speakers.TotalItemCount, speakers.TotalItemCount, speakers);

            return Json(new
            {
                status = "success",
                dataTable = response
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>Gets the collaborators uids.</summary>
        /// <param name="selectedCollaboratorsUids">The selected collaborators uids.</param>
        /// <returns></returns>
        private List<Guid> GetCollaboratorsUids(string selectedCollaboratorsUids)
        {
            var collaboratorsUids = new List<Guid>();

            if (string.IsNullOrEmpty(selectedCollaboratorsUids))
            {
                return collaboratorsUids;
            }

            var selectedCollaboratorsUidsSplit = selectedCollaboratorsUids.Split(',');
            foreach (var selectedCollaboratorUid in selectedCollaboratorsUidsSplit)
            {
                if (Guid.TryParse(selectedCollaboratorUid, out Guid collaboratorUid))
                {
                    collaboratorsUids.Add(collaboratorUid); ;
                }
            }

            return collaboratorsUids;
        }

        #endregion

        #endregion

        #region Export Eventbrite CSV

        /// <summary>Shows the export eventbrite CSV modal.</summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowExportEventbriteCsvModal()
        {
            var ticketTypes = await this.attendeeSalesPlatformTicketTypeRepo.FindAllByEditionIdAsync(this.EditionDto.Id);

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Modals/EventbriteCsvModal", ticketTypes), divIdOrClass = "#GlobalModalContainer" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>Exports the eventbrite CSV.</summary>
        /// <param name="request">The request.</param>
        /// <param name="selectedCollaboratorsUids">The selected collaborators uids.</param>
        /// <param name="ticketClassName">Name of the ticket class.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <param name="showAllParticipants">if set to <c>true</c> [show all participants].</param>
        /// <param name="showHighlights">The show highlights.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ExportEventbriteCsv(IDataTablesRequest request, string selectedCollaboratorsUids, string ticketClassName, bool showAllEditions, bool showAllParticipants, bool? showHighlights)
        {
            List<EventbriteCsv> eventbriteCsv = null;

            try
            {
                var speakers = await this.collaboratorRepo.FindAllByDataTable(
                    1,
                    10000,
                    request?.Search?.Value,
                    request?.GetSortColumns(),
                    this.GetCollaboratorsUids(selectedCollaboratorsUids),
                    Constants.CollaboratorType.Speaker,
                    showAllEditions,
                    false,
                    showAllParticipants,
                    showHighlights,
                    this.EditionDto?.Id
                );

                eventbriteCsv = speakers?.Select(s => new EventbriteCsv(
                    s.FirstName, 
                    s.LastNames, 
                    s.Email, 
                    ticketClassName, 1)).ToList();
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

            return Json(new { status = "success", data = JsonConvert.SerializeObject(eventbriteCsv) }, JsonRequestBehavior.AllowGet);
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

                var collaboratorsUids = this.GetCollaboratorsUids(selectedCollaboratorsUids);
                if (!collaboratorsUids.Any())
                {
                    throw new DomainException(Messages.SelectAtLeastOneOption);
                }

                var collaboratorsDtos = await this.collaboratorRepo.FindAllCollaboratorsByCollaboratorsUids(this.EditionDto.Id, collaboratorsUids);
                if (collaboratorsDtos?.Any() != true)
                {
                    throw new DomainException(Messages.SelectAtLeastOneOption);
                }

                foreach (var collaboratorDto in collaboratorsDtos)
                {
                    var collaboratorLanguageCode = collaboratorDto.Language?.Code ?? this.UserInterfaceLanguage;

                    try
                    {
                        result = await this.CommandBus.Send(new SendSpeakerWelcomeEmailAsync(
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
                    catch (DomainException ex)
                    {
                        //TODO: Check errors
                        //var errors = result?.Errors?.Select(e => e.Message)?.Join(", ");
                    }
                    catch (Exception ex)
                    {
                        Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                    }
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
            var executivesCount = await this.collaboratorRepo.CountAllByDataTable(Constants.CollaboratorType.Speaker, true, this.EditionDto.Id);

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/TotalCountWidget", executivesCount), divIdOrClass = "#SpeakersTotalCountWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Edition Count Widget

        /// <summary>Shows the edition count widget.</summary>
        /// <returns></returns>
        public async Task<ActionResult> ShowEditionCountWidget()
        {
            var executivesCount = await this.collaboratorRepo.CountAllByDataTable(Constants.CollaboratorType.Speaker, false, this.EditionDto.Id);

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/EditionCountWidget", executivesCount), divIdOrClass = "#SpeakersEditionCountWidget" },
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
            var cmd = new CreateTinyCollaborator();

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
        public async Task<ActionResult> Create(CreateTinyCollaborator cmd)
        {
            var result = new AppValidationResult();

            try
            {
                if (!ModelState.IsValid)
                {
                    throw new DomainException(Messages.CorrectFormValues);
                }

                cmd.UpdatePreSendProperties(
                    Constants.CollaboratorType.Speaker,
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
                    message = ex.GetInnerMessage(),
                    pages = new List<dynamic>
                    {
                        new { page = this.RenderRazorViewToString("Modals/_TinyForm", cmd), divIdOrClass = "#form-container" },
                    }
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return Json(new { status = "error", message = Messages.WeFoundAndError, }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Speaker, Labels.CreatedM) });
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
            UpdateTinyCollaborator cmd;

            try
            {
                cmd = new UpdateTinyCollaborator(
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
        public async Task<ActionResult> Update(UpdateTinyCollaborator cmd)
        {
            var result = new AppValidationResult();

            try
            {
                if (!ModelState.IsValid)
                {
                    throw new DomainException(Messages.CorrectFormValues);
                }

                cmd.UpdatePreSendProperties(
                    Constants.CollaboratorType.Speaker,
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
                    message = ex.GetInnerMessage(),
                    pages = new List<dynamic>
                    {
                        new { page = this.RenderRazorViewToString("Modals/_TinyForm", cmd), divIdOrClass = "#form-container" },
                    }
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return Json(new { status = "error", message = Messages.WeFoundAndError, }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Speaker, Labels.UpdatedM) });
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
                    Constants.CollaboratorType.Speaker,
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
                    message = ex.GetInnerMessage(),
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return Json(new { status = "error", message = Messages.WeFoundAndError, }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Speaker, Labels.DeletedM) });
        }

        #endregion
    }
}