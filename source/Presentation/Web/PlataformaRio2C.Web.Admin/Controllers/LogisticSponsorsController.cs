// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Arthur Souza
// Created          : 01-20-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-09-2020
// ***********************************************************************
// <copyright file="LogisticSponsorsController.cs" company="Softo">
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
using PlataformaRio2C.Application.CQRS.Queries;
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
    /// <summary>LogisticSponsorsController</summary>
    [AjaxAuthorize(Order = 1, Roles = Constants.Role.AnyAdmin)]
    [AuthorizeCollaboratorType(Order = 2, Types = Constants.CollaboratorType.AdminAudiovisual + "," + Constants.CollaboratorType.AdminLogistic + "," + Constants.CollaboratorType.CuratorshipAudiovisual)]
    public class LogisticSponsorsController : BaseController
    {
        private readonly IAttendeeLogisticSponsorRepository attendeeLogisticSponsorRepo;
        private readonly ILogisticSponsorRepository logisticSponsorRepo;
        private readonly ILanguageRepository languageRepo;

        /// <summary>Initializes a new instance of the <see cref="LogisticSponsorsController"/> class.</summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="identityController">The identity controller.</param>
        /// <param name="logisticSponsorRepo">The logistic sponsor repo.</param>
        /// <param name="attendeeLogisticSponsorRepo">The attendee logistic sponsor repo.</param>
        /// <param name="languageRepo">The language repo.</param>
        public LogisticSponsorsController(
            IMediator commandBus, 
            IdentityAutenticationService identityController,
            ILogisticSponsorRepository logisticSponsorRepo,
            IAttendeeLogisticSponsorRepository attendeeLogisticSponsorRepo,
            ILanguageRepository languageRepo)
            : base(commandBus, identityController)
        {
            this.logisticSponsorRepo = logisticSponsorRepo;
            this.languageRepo = languageRepo;
            this.attendeeLogisticSponsorRepo = attendeeLogisticSponsorRepo;
        }

        #region List

        /// <summary>Indexes the specified search view model.</summary>
        /// <param name="searchViewModel">The search view model.</param>
        /// <returns></returns>
        [HttpGet]
        [AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.AdminAudiovisual + "," + Constants.CollaboratorType.AdminLogistic)]
        public ActionResult Index(LogisticSponsorSearchViewModel searchViewModel)
        {
            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.Logistics, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Labels.Sponsors, Url.Action("Index", "LogisticSponsors", new { Area = "" }))
            });

            #endregion

            return View(searchViewModel);
        }

        #region DataTable Widget

        /// <summary>Searches the specified request.</summary>
        /// <param name="request">The request.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Search(IDataTablesRequest request, bool showAllEditions)
        {
            var sponsors = await this.logisticSponsorRepo.FindAllByDataTable(
                request.Start / request.Length,
                request.Length,
                request.Search?.Value,
                request.GetSortColumns(),
                showAllEditions,
                this.EditionDto?.Id);

            foreach (var item in sponsors){
                item.Name = item.Name.GetSeparatorTranslation(this.UserInterfaceLanguage, '|');
            }

            var response = DataTablesResponse.Create(request, sponsors.TotalItemCount, sponsors.TotalItemCount, sponsors);
            
            return Json(new
            {
                status = "success",
                dataTable = response
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #endregion
                       
        #region Create

        /// <summary>Shows the create modal.</summary>
        /// <returns></returns>
        [HttpGet]
        [AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.AdminAudiovisual + "," + Constants.CollaboratorType.AdminLogistic)]
        public async Task<ActionResult> ShowCreateModal()
        {
            var cmd = new CreateLogisticSponsors(
                await this.languageRepo.FindAllDtosAsync(),
                this.UserInterfaceLanguage);

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
        [AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.AdminAudiovisual + "," + Constants.CollaboratorType.AdminLogistic)]
        public async Task<ActionResult> Create(CreateLogisticSponsors cmd)
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
        /// <param name="sponsorUid">The sponsor uid.</param>
        /// <param name="isAddingToCurrentEdition">The is adding to current edition.</param>
        /// <returns></returns>
        [HttpGet]
        [AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.AdminAudiovisual + "," + Constants.CollaboratorType.AdminLogistic)]
        public async Task<ActionResult> ShowUpdateModal(Guid? sponsorUid, bool? isAddingToCurrentEdition)
        {
            UpdateLogisticSponsors cmd;

            try
            {
                cmd = new UpdateLogisticSponsors(
                    await this.CommandBus.Send(new FindLogisticSponsorDtoByUid(sponsorUid, this.UserInterfaceLanguage)),
                    await languageRepo.FindAllDtosAsync(),
                    UserInterfaceLanguage,
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
        [AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.AdminAudiovisual + "," + Constants.CollaboratorType.AdminLogistic)]
        public async Task<ActionResult> Update(UpdateLogisticSponsors cmd)
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

            return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Speaker, Labels.UpdatedM) });
        }

        #endregion

        #region Delete

        /// <summary>Deletes the specified command.</summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.AdminAudiovisual + "," + Constants.CollaboratorType.AdminLogistic)]
        public async Task<ActionResult> Delete(DeleteLogisticSponsors cmd)
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

            return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Speaker, Labels.DeletedM) });
        }

        #endregion

        #region Finds

        /// <summary>Finds all by is other.</summary>
        /// <returns></returns>
        [HttpGet]
        [AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.AdminAudiovisual + "," + Constants.CollaboratorType.AdminLogistic + "," + Constants.CollaboratorType.CuratorshipAudiovisual)]
        public async Task<ActionResult> FindAllByIsOther()
        {
            var list = await this.attendeeLogisticSponsorRepo.FindAllDtosByIsOther(this.EditionDto.Id, true);
            
            return Json(new
            {
                status = "success",
                list
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}