// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 12-26-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-02-2020
// ***********************************************************************
// <copyright file="ConferencesController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Application.ViewModels;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using System;
using System.Collections.Generic;
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
    /// <summary>SpeakersController</summary>
    [AjaxAuthorize(Order = 1, Roles = Constants.Role.AnyAdmin)]
    [AuthorizeCollaboratorType(Order = 2, Types = Constants.CollaboratorType.AdminAudiovisual + "," + Constants.CollaboratorType.CuratorshipAudiovisual)]
    public class ConferencesController : BaseController
    {
        private readonly IConferenceRepository conferenceRepo;
        private readonly ILanguageRepository languageRepo;

        /// <summary>Initializes a new instance of the <see cref="ConferencesController"/> class.</summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="identityController">The identity controller.</param>
        /// <param name="conferenceRepository">The conference repository.</param>
        /// <param name="languageRepository">The language repository.</param>
        public ConferencesController(
            IMediator commandBus, 
            IdentityAutenticationService identityController,
            IConferenceRepository conferenceRepository,
            ILanguageRepository languageRepository)
            : base(commandBus, identityController)
        {
            this.conferenceRepo = conferenceRepository;
            this.languageRepo = languageRepository;
        }

        #region List

        /// <summary>Indexes the specified search view model.</summary>
        /// <param name="searchViewModel">The search view model.</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index(ConferenceSearchViewModel searchViewModel)
        {
            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.Conferences, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Labels.Conferences, Url.Action("Index", "Conferences", new { Area = "" }))
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
            var conferences = await this.conferenceRepo.FindAllByDataTable(
                request.Start / request.Length,
                request.Length,
                request.Search?.Value,
                request.GetSortColumns(),
                this.GetCollaboratorsUids(null),
                this.EditionDto.Id,
                this.AdminAccessControlDto.Language.Id
            );

            var response = DataTablesResponse.Create(request, conferences.TotalItemCount, conferences.TotalItemCount, conferences);

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

        #region Total Count Widget

        /// <summary>Shows the total count widget.</summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowTotalCountWidget()
        {
            var conferencesCount = await this.conferenceRepo.CountAllByDataTable(true, this.EditionDto.Id);

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/TotalCountWidget", conferencesCount), divIdOrClass = "#ConferencesTotalCountWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Edition Count Widget

        /// <summary>Shows the edition count widget.</summary>
        /// <returns></returns>
        public async Task<ActionResult> ShowEditionCountWidget()
        {
            var conferencesCount = await this.conferenceRepo.CountAllByDataTable(true, this.EditionDto.Id);

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/EditionCountWidget", conferencesCount), divIdOrClass = "#ConferencesEditionCountWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        //#region Details

        ///// <summary>Detailses the specified identifier.</summary>
        ///// <param name="id">The identifier.</param>
        ///// <returns></returns>
        //[HttpGet]
        //public async Task<ActionResult> Details(Guid? id)
        //{
        //    var attendeeCollaboratorDto = await this.attendeeCollaboratorRepo.FindSiteDetailstDtoByCollaboratorUidAndByEditionIdAsync(id ?? Guid.Empty, this.EditionDto.Id);
        //    if (attendeeCollaboratorDto == null)
        //    {
        //        this.StatusMessageToastr(string.Format(Messages.EntityNotAction, Labels.Speaker, Labels.FoundM.ToLowerInvariant()), Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);
        //        return RedirectToAction("Index", "Home", new { Area = "" });
        //    }

        //    #region Breadcrumb

        //    ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.Speakers, new List<BreadcrumbItemHelper> {
        //        new BreadcrumbItemHelper(Labels.Speakers, Url.Action("Index", "Speakers", new { id })),
        //        new BreadcrumbItemHelper(attendeeCollaboratorDto.Collaborator.GetFullName(), Url.Action("Details", "Speakers", new { id }))
        //    });

        //    #endregion

        //    return View(attendeeCollaboratorDto);
        //}

        //#region Main Information Widget

        ///// <summary>Shows the main information widget.</summary>
        ///// <param name="collaboratorUid">The collaborator uid.</param>
        ///// <returns></returns>
        //[HttpGet]
        //public async Task<ActionResult> ShowMainInformationWidget(Guid? collaboratorUid)
        //{
        //    var mainInformationWidgetDto = await this.attendeeCollaboratorRepo.FindSiteMainInformationWidgetDtoByCollaboratorUidAndByEditionIdAsync(collaboratorUid ?? Guid.Empty, this.EditionDto.Id);
        //    if (mainInformationWidgetDto == null)
        //    {
        //        return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Company, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
        //    }

        //    return Json(new
        //    {
        //        status = "success",
        //        pages = new List<dynamic>
        //        {
        //            new { page = this.RenderRazorViewToString("Widgets/MainInformationWidget", mainInformationWidgetDto), divIdOrClass = "#SpeakerMainInformationWidget" },
        //        }
        //    }, JsonRequestBehavior.AllowGet);
        //}

        //#region Update

        ///// <summary>Shows the update main information modal.</summary>
        ///// <param name="collaboratorUid">The collaborator uid.</param>
        ///// <returns></returns>
        //[HttpGet]
        //public async Task<ActionResult> ShowUpdateMainInformationModal(Guid? collaboratorUid)
        //{
        //    UpdateCollaboratorAdminMainInformation cmd;

        //    try
        //    {
        //        var mainInformationWidgetDto = await this.attendeeCollaboratorRepo.FindSiteMainInformationWidgetDtoByCollaboratorUidAndByEditionIdAsync(collaboratorUid ?? Guid.Empty, this.EditionDto.Id);
        //        if (mainInformationWidgetDto == null)
        //        {
        //            throw new DomainException(string.Format(Messages.EntityNotAction, Labels.Speaker, Labels.FoundM.ToLowerInvariant()));
        //        }

        //        cmd = new UpdateCollaboratorAdminMainInformation(
        //            mainInformationWidgetDto,
        //            await this.CommandBus.Send(new FindAllLanguagesDtosAsync(this.UserInterfaceLanguage)));
        //    }
        //    catch (DomainException ex)
        //    {
        //        return Json(new { status = "error", message = ex.GetInnerMessage() }, JsonRequestBehavior.AllowGet);
        //    }

        //    return Json(new
        //    {
        //        status = "success",
        //        pages = new List<dynamic>
        //        {
        //            new { page = this.RenderRazorViewToString("Modals/UpdateMainInformationModal", cmd), divIdOrClass = "#GlobalModalContainer" },
        //        }
        //    }, JsonRequestBehavior.AllowGet);
        //}

        ///// <summary>Updates the main information.</summary>
        ///// <param name="cmd">The command.</param>
        ///// <returns></returns>
        //[HttpPost]
        //public async Task<ActionResult> UpdateMainInformation(UpdateCollaboratorAdminMainInformation cmd)
        //{
        //    var result = new AppValidationResult();

        //    try
        //    {
        //        // Speakers does not have public email
        //        if (ModelState.ContainsKey("SharePublicEmail"))
        //        {
        //            ModelState["SharePublicEmail"].Errors.Clear();
        //        }

        //        if (!ModelState.IsValid)
        //        {
        //            throw new DomainException(Messages.CorrectFormValues);
        //        }

        //        cmd.UpdatePreSendProperties(
        //            Domain.Constants.CollaboratorType.Speaker,
        //            this.AdminAccessControlDto.User.Id,
        //            this.AdminAccessControlDto.User.Uid,
        //            this.EditionDto.Id,
        //            this.EditionDto.Uid,
        //            this.UserInterfaceLanguage);
        //        result = await this.CommandBus.Send(cmd);
        //        if (!result.IsValid)
        //        {
        //            throw new DomainException(Messages.CorrectFormValues);
        //        }
        //    }
        //    catch (DomainException ex)
        //    {
        //        foreach (var error in result.Errors)
        //        {
        //            var target = error.Target ?? "";
        //            ModelState.AddModelError(target, error.Message);
        //        }

        //        return Json(new
        //        {
        //            status = "error",
        //            message = ex.GetInnerMessage(),
        //            pages = new List<dynamic>
        //            {
        //                new { page = this.RenderRazorViewToString("Modals/UpdateMainInformationForm", cmd), divIdOrClass = "#form-container" },
        //            }
        //        }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        //        return Json(new { status = "error", message = Messages.WeFoundAndError, }, JsonRequestBehavior.AllowGet);
        //    }

        //    return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Speaker, Labels.UpdatedM) });
        //}

        //#endregion

        //#endregion

        //#region Company Widget

        ///// <summary>Shows the company widget.</summary>
        ///// <param name="collaboratorUid">The collaborator uid.</param>
        ///// <returns></returns>
        //[HttpGet]
        //public async Task<ActionResult> ShowCompanyWidget(Guid? collaboratorUid)
        //{
        //    var companyWidgetDto = await this.attendeeCollaboratorRepo.FindSiteCompanyWidgetDtoByCollaboratorUidAndByEditionIdAsync(collaboratorUid ?? Guid.Empty, this.EditionDto.Id);
        //    if (companyWidgetDto == null)
        //    {
        //        return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Company, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
        //    }

        //    return Json(new
        //    {
        //        status = "success",
        //        pages = new List<dynamic>
        //        {
        //            new { page = this.RenderRazorViewToString("Widgets/CompanyWidget", companyWidgetDto), divIdOrClass = "#SpeakerCompanyWidget" },
        //        }
        //    }, JsonRequestBehavior.AllowGet);
        //}

        //#region Update

        ///// <summary>Shows the update company information modal.</summary>
        ///// <returns></returns>
        //[HttpGet]
        //public async Task<ActionResult> ShowUpdateCompanyInfoModal(Guid? collaboratorUid, Guid? organizationUid)
        //{
        //    CreateTicketBuyerOrganizationData cmd;

        //    try
        //    {
        //        if (!collaboratorUid.HasValue)
        //        {
        //            throw new DomainException(string.Format(Messages.EntityNotAction, Labels.Speaker, Labels.FoundM.ToLowerInvariant()));
        //        }

        //        cmd = new CreateTicketBuyerOrganizationData(
        //            collaboratorUid.Value,
        //            organizationUid.HasValue ? await this.CommandBus.Send(new FindOrganizationDtoByUidAsync(organizationUid, this.EditionDto.Id, this.UserInterfaceLanguage)) : null,
        //            await this.CommandBus.Send(new FindAllLanguagesDtosAsync(this.UserInterfaceLanguage)),
        //            await this.CommandBus.Send(new FindAllCountriesBaseDtosAsync(this.UserInterfaceLanguage)),
        //            false,
        //            false,
        //            false);
        //    }
        //    catch (DomainException ex)
        //    {
        //        return Json(new { status = "error", message = ex.GetInnerMessage() }, JsonRequestBehavior.AllowGet);
        //    }

        //    ModelState.Clear();

        //    return Json(new
        //    {
        //        status = "success",
        //        pages = new List<dynamic>
        //        {
        //            new { page = this.RenderRazorViewToString("Modals/UpdateCompanyInfoModal", cmd), divIdOrClass = "#GlobalModalContainer" },
        //        }
        //    }, JsonRequestBehavior.AllowGet);
        //}

        ///// <summary>Updates the company information.</summary>
        ///// <param name="cmd">The command.</param>
        ///// <returns></returns>
        //[HttpPost]
        //public async Task<ActionResult> UpdateCompanyInformation(CreateTicketBuyerOrganizationData cmd)
        //{
        //    var result = new AppValidationResult();

        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            throw new DomainException(Messages.CorrectFormValues);
        //        }

        //        cmd.UpdatePreSendProperties(
        //            cmd.CollaboratorUid,
        //            this.AdminAccessControlDto.User.Id,
        //            this.AdminAccessControlDto.User.Uid,
        //            this.EditionDto.Id,
        //            this.EditionDto.Uid,
        //            this.UserInterfaceLanguage);
        //        result = await this.CommandBus.Send(cmd);
        //        if (!result.IsValid)
        //        {
        //            throw new DomainException(Messages.CorrectFormValues);
        //        }
        //    }
        //    catch (DomainException ex)
        //    {
        //        foreach (var error in result.Errors)
        //        {
        //            var target = error.Target ?? "";
        //            ModelState.AddModelError(target, error.Message);
        //        }

        //        cmd.UpdateDropdownProperties(
        //            await this.CommandBus.Send(new FindAllCountriesBaseDtosAsync(this.UserInterfaceLanguage)));

        //        return Json(new
        //        {
        //            status = "error",
        //            message = ex.GetInnerMessage(),
        //            pages = new List<dynamic>
        //            {
        //                new { page = this.RenderRazorViewToString("/Views/Companies/Shared/_TicketBuyerCompanyInfoForm.cshtml", cmd), divIdOrClass = "#form-container" },
        //            }
        //        }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        //        return Json(new { status = "error", message = Messages.WeFoundAndError, }, JsonRequestBehavior.AllowGet);
        //    }

        //    return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Company, Labels.UpdatedF) });
        //}

        //#endregion

        //#region Delete

        ///// <summary>Deletes the organization.</summary>
        ///// <param name="cmd">The command.</param>
        ///// <returns></returns>
        //[HttpPost]
        //public async Task<ActionResult> DeleteOrganization(DeleteCollaboratorOrganization cmd)
        //{
        //    var result = new AppValidationResult();

        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            throw new DomainException(Messages.CorrectFormValues);
        //        }

        //        cmd.UpdatePreSendProperties(
        //            Constants.CollaboratorType.Speaker,
        //            this.AdminAccessControlDto.User.Id,
        //            this.AdminAccessControlDto.User.Uid,
        //            this.EditionDto.Id,
        //            this.EditionDto.Uid,
        //            this.UserInterfaceLanguage);

        //        result = await this.CommandBus.Send(cmd);
        //        if (!result.IsValid)
        //        {
        //            throw new DomainException(Messages.CorrectFormValues);
        //        }
        //    }
        //    catch (DomainException ex)
        //    {
        //        foreach (var error in result.Errors)
        //        {
        //            var target = error.Target ?? "";
        //            ModelState.AddModelError(target, error.Message);
        //        }

        //        return Json(new
        //        {
        //            status = "error",
        //            message = ex.GetInnerMessage(),
        //        }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        //        return Json(new { status = "error", message = Messages.WeFoundAndError, }, JsonRequestBehavior.AllowGet);
        //    }

        //    return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Speaker, Labels.DeletedM) });
        //}

        //#endregion

        //#endregion

        //#region Api Configuration Widget

        ///// <summary>Shows the API configuration widget.</summary>
        ///// <param name="collaboratorUid">The collaborator uid.</param>
        ///// <returns></returns>
        //[HttpGet]
        //public async Task<ActionResult> ShowApiConfigurationWidget(Guid? collaboratorUid)
        //{
        //    var apiConfigurationWidgetDto = await this.attendeeCollaboratorRepo.FindApiConfigurationWidgetDtoByCollaboratorUidAndByEditionIdAsync(collaboratorUid ?? Guid.Empty, this.EditionDto.Id);
        //    if (apiConfigurationWidgetDto == null)
        //    {
        //        return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Company, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
        //    }

        //    return Json(new
        //    {
        //        status = "success",
        //        pages = new List<dynamic>
        //        {
        //            new { page = this.RenderRazorViewToString("Widgets/ApiConfigurationWidget", apiConfigurationWidgetDto), divIdOrClass = "#SpeakerApiConfigurationWidget" },
        //        }
        //    }, JsonRequestBehavior.AllowGet);
        //}

        //#region Update

        ///// <summary>Shows the update API configuration modal.</summary>
        ///// <param name="collaboratorUid">The collaborator uid.</param>
        ///// <returns></returns>
        //[HttpGet]
        //public async Task<ActionResult> ShowUpdateApiConfigurationModal(Guid? collaboratorUid)
        //{
        //    UpdateCollaboratorApiConfiguration cmd;

        //    try
        //    {
        //        var apiConfigurationWidgetDto = await this.attendeeCollaboratorRepo.FindApiConfigurationWidgetDtoByCollaboratorUidAndByEditionIdAsync(collaboratorUid ?? Guid.Empty, this.EditionDto.Id);
        //        if (apiConfigurationWidgetDto == null)
        //        {
        //            throw new DomainException(string.Format(Messages.EntityNotAction, Labels.Speaker, Labels.FoundM.ToLowerInvariant()));
        //        }

        //        cmd = new UpdateCollaboratorApiConfiguration(
        //            apiConfigurationWidgetDto, 
        //            Constants.CollaboratorType.Speaker,
        //            await this.attendeeCollaboratorRepo.FindAllApiConfigurationWidgetDtoByHighlight(this.EditionDto.Id, Constants.CollaboratorType.Speaker));
        //    }
        //    catch (DomainException ex)
        //    {
        //        return Json(new { status = "error", message = ex.GetInnerMessage() }, JsonRequestBehavior.AllowGet);
        //    }

        //    return Json(new
        //    {
        //        status = "success",
        //        pages = new List<dynamic>
        //        {
        //            new { page = this.RenderRazorViewToString("Modals/UpdateApiConfigurationModal", cmd), divIdOrClass = "#GlobalModalContainer" },
        //        }
        //    }, JsonRequestBehavior.AllowGet);
        //}

        ///// <summary>Updates the API configuration.</summary>
        ///// <param name="cmd">The command.</param>
        ///// <returns></returns>
        ///// <exception cref="DomainException"></exception>
        //[HttpPost]
        //public async Task<ActionResult> UpdateApiConfiguration(UpdateCollaboratorApiConfiguration cmd)
        //{
        //    var result = new AppValidationResult();

        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            throw new DomainException(Messages.CorrectFormValues);
        //        }

        //        cmd.UpdatePreSendProperties(
        //            Constants.CollaboratorType.Speaker,
        //            this.AdminAccessControlDto.User.Id,
        //            this.AdminAccessControlDto.User.Uid,
        //            this.EditionDto.Id,
        //            this.EditionDto.Uid,
        //            this.UserInterfaceLanguage);
        //        result = await this.CommandBus.Send(cmd);
        //        if (!result.IsValid)
        //        {
        //            throw new DomainException(Messages.CorrectFormValues);
        //        }
        //    }
        //    catch (DomainException ex)
        //    {
        //        foreach (var error in result.Errors)
        //        {
        //            var target = error.Target ?? "";
        //            ModelState.AddModelError(target, error.Message);
        //        }

        //        cmd.UpdateBaseModels(
        //            await this.attendeeCollaboratorRepo.FindAllApiConfigurationWidgetDtoByHighlight(this.EditionDto.Id, Constants.CollaboratorType.Speaker));

        //        return Json(new
        //        {
        //            status = "error",
        //            message = ex.GetInnerMessage(),
        //            pages = new List<dynamic>
        //            {
        //                new { page = this.RenderRazorViewToString("Modals/UpdateApiConfigurationForm", cmd), divIdOrClass = "#form-container" },
        //            }
        //        }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        //        return Json(new { status = "error", message = Messages.WeFoundAndError, }, JsonRequestBehavior.AllowGet);
        //    }

        //    return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Speaker, Labels.UpdatedM) });
        //}

        //#endregion

        //#endregion

        //#endregion

        #region Create

        /// <summary>Shows the create modal.</summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowCreateModal()
        {
            var cmd = new CreateConference(
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

        /// <summary>Creates the specified command.</summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Create(CreateConference cmd)
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
                    message = ex.GetInnerMessage(),
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

            return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Conference, Labels.CreatedF) });
        }

        #endregion

        #region Delete

        /// <summary>Deletes the specified conference.</summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Delete(DeleteConference cmd)
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
                    message = ex.GetInnerMessage(),
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return Json(new { status = "error", message = Messages.WeFoundAndError, }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Conference, Labels.DeletedF) });
        }

        #endregion
    }
}