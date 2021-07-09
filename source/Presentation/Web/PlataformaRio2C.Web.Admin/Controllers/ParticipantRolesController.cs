// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 01-07-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 07-09-2021
// ***********************************************************************
// <copyright file="ParticipantRolesController.cs" company="Softo">
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
using PlataformaRio2C.Application;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.CrossCutting.Identity.AuthorizeAttributes;
using PlataformaRio2C.Infra.CrossCutting.Identity.Service;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions;
using PlataformaRio2C.Infra.CrossCutting.Tools.Helpers;
using PlataformaRio2C.Web.Admin.Filters;
using Constants = PlataformaRio2C.Domain.Constants;

namespace PlataformaRio2C.Web.Admin.Controllers
{
    /// <summary>
    /// ParticipantRolesController
    /// </summary>
    [AjaxAuthorize(Order = 1, Roles = Constants.Role.AnyAdmin)]
    [AuthorizeCollaboratorType(Order = 2, Types = Constants.CollaboratorType.AdminConferences)]
    public class ParticipantRolesController : BaseController
    {
        private readonly IConferenceParticipantRoleRepository conferenceParticipantRoleRepo;
        private readonly ILanguageRepository languageRepo;

        /// <summary>Initializes a new instance of the <see cref="ParticipantRolesController"/> class.</summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="identityController">The identity controller.</param>
        /// <param name="conferenceParticipantRoleRepository">The conferenceParticipantRole repository.</param>
        /// <param name="languageRepository">The language repository.</param>
        public ParticipantRolesController(
            IMediator commandBus, 
            IdentityAutenticationService identityController,
            IConferenceParticipantRoleRepository conferenceParticipantRoleRepository,
            ILanguageRepository languageRepository)
            : base(commandBus, identityController)
        {
            this.conferenceParticipantRoleRepo = conferenceParticipantRoleRepository;
            this.languageRepo = languageRepository;
        }

        #region List

        /// <summary>Indexes the specified search view model.</summary>
        /// <param name="searchViewModel">The search view model.</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index(ConferenceParticipantRoleSearchViewModel searchViewModel)
        {
            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.Conferences, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Labels.SpeakersRoles, Url.Action("Index", "ParticipantRoles", new { Area = "" }))
            });

            #endregion

            return View(searchViewModel);
        }

        #region DataTable Widget

        /// <summary>Searches the specified request.</summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Search(IDataTablesRequest request)
        {
            var conferenceParticipantRoles = await this.conferenceParticipantRoleRepo.FindAllByDataTable(
                request.Start / request.Length,
                request.Length,
                request.Search?.Value,
                request.GetSortColumns(),
                new List<Guid>(), 
                this.EditionDto.Id,
                this.AdminAccessControlDto.Language.Id
            );

            var response = DataTablesResponse.Create(request, conferenceParticipantRoles.TotalItemCount, conferenceParticipantRoles.TotalItemCount, conferenceParticipantRoles);

            return Json(new
            {
                status = "success",
                dataTable = response
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #endregion

        #region Total Count Widget

        /// <summary>Shows the total count widget.</summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowTotalCountWidget()
        {
            var conferenceParticipantRolesCount = await this.conferenceParticipantRoleRepo.CountAllByDataTable(true, this.EditionDto.Id);

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/TotalCountWidget", conferenceParticipantRolesCount), divIdOrClass = "#ConferenceParticipantRolesTotalCountWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Edition Count Widget

        /// <summary>Shows the edition count widget.</summary>
        /// <returns></returns>
        public async Task<ActionResult> ShowEditionCountWidget()
        {
            var conferenceParticipantRolesCount = await this.conferenceParticipantRoleRepo.CountAllByDataTable(false, this.EditionDto.Id);

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/EditionCountWidget", conferenceParticipantRolesCount), divIdOrClass = "#ConferenceParticipantRolesEditionCountWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Details

        /// <summary>Detailses the specified identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Details(Guid? id)
        {
            var conferenceParticipantRoleDto = await this.conferenceParticipantRoleRepo.FindDtoAsync(id ?? Guid.Empty, this.EditionDto.Id);
            if (conferenceParticipantRoleDto == null)
            {
                this.StatusMessageToastr(string.Format(Messages.EntityNotAction, Labels.SpeakerRole, Labels.FoundF.ToLowerInvariant()), Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);
                return RedirectToAction("Index", "ParticipantRoles", new { Area = "" });
            }

            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.Conferences, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Labels.SpeakersRoles, Url.Action("Index", "ParticipantRoles", new { Area = "" })),
                new BreadcrumbItemHelper(conferenceParticipantRoleDto.GetConferenceParticipantRoleTitleDtoByLanguageCode(ViewBag.UserInterfaceLanguage)?.ConferenceParticipantRoleTitle?.Value, Url.Action("Details", "ParticipantRoles", new { id }))
            });

            #endregion

            return View(conferenceParticipantRoleDto);
        }

        #region Main Information Widget

        /// <summary>Shows the main information widget.</summary>
        /// <param name="conferenceParticipantRoleUid">The conferenceParticipantRole uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowMainInformationWidget(Guid? conferenceParticipantRoleUid)
        {
            var mainInformationWidgetDto = await this.conferenceParticipantRoleRepo.FindDtoAsync(conferenceParticipantRoleUid ?? Guid.Empty, this.EditionDto.Id);
            if (mainInformationWidgetDto == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.SpeakerRole, Labels.FoundF.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/MainInformationWidget", mainInformationWidgetDto), divIdOrClass = "#ConferenceParticipantRoleMainInformationWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #region Update

        /// <summary>Shows the update main information modal.</summary>
        /// <param name="conferenceParticipantRoleUid">The conferenceParticipantRole uid.</param>
        /// <returns></returns>
        /// <exception cref="DomainException"></exception>
        [HttpGet]
        public async Task<ActionResult> ShowUpdateMainInformationModal(Guid? conferenceParticipantRoleUid)
        {
            UpdateConferenceParticipantRoleMainInformation cmd;

            try
            {
                var mainInformationWidgetDto = await this.conferenceParticipantRoleRepo.FindDtoAsync(conferenceParticipantRoleUid ?? Guid.Empty, this.EditionDto.Id);
                if (mainInformationWidgetDto == null)
                {
                    throw new DomainException(string.Format(Messages.EntityNotAction, Labels.SpeakerRole, Labels.FoundF.ToLowerInvariant()));
                }

                cmd = new UpdateConferenceParticipantRoleMainInformation(
                    mainInformationWidgetDto,
                    await this.languageRepo.FindAllDtosAsync());
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
        public async Task<ActionResult> UpdateMainInformation(UpdateConferenceParticipantRoleMainInformation cmd)
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
                        new { page = this.RenderRazorViewToString("Modals/UpdateMainInformationForm", cmd), divIdOrClass = "#form-container" },
                    }
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return Json(new { status = "error", message = Messages.WeFoundAndError, }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.SpeakerRole, Labels.UpdatedF) });
        }

        #endregion

        #endregion

        #region Participants Widget

        /// <summary>Shows the participants widget.</summary>
        /// <param name="conferenceParticipantRoleUid">The conference participant role uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowParticipantsWidget(Guid? conferenceParticipantRoleUid)
        {
            var participantsWidgetDto = await this.conferenceParticipantRoleRepo.FindParticipantsWidgetDtoAsync(conferenceParticipantRoleUid ?? Guid.Empty, this.EditionDto.Id);
            if (participantsWidgetDto == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.SpeakerRole, Labels.FoundF.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/RelatedParticipantsWidget", participantsWidgetDto), divIdOrClass = "#ConferenceParticipantRoleParticipantsWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Conferences Widget

        /// <summary>Shows the conferences widget.</summary>
        /// <param name="conferenceParticipantRoleUid">The conferenceParticipantRole uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowConferencesWidget(Guid? conferenceParticipantRoleUid)
        {
            var conferencesWidgetDto = await this.conferenceParticipantRoleRepo.FindConferenceWidgetDtoAsync(conferenceParticipantRoleUid ?? Guid.Empty, this.EditionDto.Id);
            if (conferencesWidgetDto == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.SpeakerRole, Labels.FoundF.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("~/Views/Conferences/Widgets/RelatedConferencesWidget.cshtml", conferencesWidgetDto.ConferenceDtos), divIdOrClass = "#ConferenceParticipantRoleConferencesWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #endregion

        #region Create

        /// <summary>Shows the create modal.</summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowCreateModal()
        {
            var cmd = new CreateConferenceParticipantRole(
                null,
                await this.languageRepo.FindAllDtosAsync());

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Modals/CreateModal", cmd), divIdOrClass = "#GlobalModalContainer" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>Creates the specified conferenceParticipantRole.</summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Create(CreateConferenceParticipantRole cmd)
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
                        new { page = this.RenderRazorViewToString("Modals/_CreateForm", cmd), divIdOrClass = "#form-container" },
                    }
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return Json(new { status = "error", message = Messages.WeFoundAndError, }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.SpeakerRole, Labels.CreatedF) });
        }

        #endregion

        #region Delete

        /// <summary>Deletes the specified delete conferenceParticipantRole.</summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Delete(DeleteConferenceParticipantRole cmd)
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

            return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.SpeakerRole, Labels.DeletedF) });
        }

        #endregion
    }
}