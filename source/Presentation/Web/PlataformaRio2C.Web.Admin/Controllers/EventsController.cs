// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 01-05-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-04-2020
// ***********************************************************************
// <copyright file="EventsController.cs" company="Softo">
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
    /// <summary>EventsController</summary>
    [AjaxAuthorize(Order = 1, Roles = Constants.Role.AnyAdmin)]
    [AuthorizeCollaboratorType(Order = 2, Types = Constants.CollaboratorType.AdminAudiovisual + "," + Constants.CollaboratorType.CuratorshipAudiovisual)]
    public class EventsController : BaseController
    {
        private readonly IEditionEventRepository editionEventRepo;

        /// <summary>Initializes a new instance of the <see cref="EventsController"/> class.</summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="identityController">The identity controller.</param>
        /// <param name="editionEventRepository">The edition event repository.</param>
        public EventsController(
            IMediator commandBus, 
            IdentityAutenticationService identityController,
            IEditionEventRepository editionEventRepository)
            : base(commandBus, identityController)
        {
            this.editionEventRepo = editionEventRepository;
        }

        #region List

        /// <summary>Indexes the specified search view model.</summary>
        /// <param name="searchViewModel">The search view model.</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index(EditionEventSearchViewModel searchViewModel)
        {
            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.Conferences, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Labels.Events, Url.Action("Index", "Events", new { Area = "" }))
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
            var editionEvents = await this.editionEventRepo.FindAllByDataTable(
                request.Start / request.Length,
                request.Length,
                request.Search?.Value,
                request.GetSortColumns(),
                new List<Guid>(), 
                this.EditionDto.Id,
                this.AdminAccessControlDto.Language.Id
            );

            var response = DataTablesResponse.Create(request, editionEvents.TotalItemCount, editionEvents.TotalItemCount, editionEvents);

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
            var editionEventsCount = await this.editionEventRepo.CountAllByDataTable(true, this.EditionDto.Id);

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/TotalCountWidget", editionEventsCount), divIdOrClass = "#EventsTotalCountWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Edition Count Widget

        /// <summary>Shows the edition count widget.</summary>
        /// <returns></returns>
        public async Task<ActionResult> ShowEditionCountWidget()
        {
            var editionEventsCount = await this.editionEventRepo.CountAllByDataTable(false, this.EditionDto.Id);

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/EditionCountWidget", editionEventsCount), divIdOrClass = "#EventsEditionCountWidget" },
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
            var editionEventDto = await this.editionEventRepo.FindDtoAsync(id ?? Guid.Empty, this.EditionDto.Id);
            if (editionEventDto == null)
            {
                this.StatusMessageToastr(string.Format(Messages.EntityNotAction, Labels.Event, Labels.FoundM.ToLowerInvariant()), Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);
                return RedirectToAction("Index", "Events", new { Area = "" });
            }

            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.Conferences, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Labels.Events, Url.Action("Index", "Events", new { Area = "" })),
                new BreadcrumbItemHelper(editionEventDto.EditionEvent.Name, Url.Action("Details", "Events", new { id }))
            });

            #endregion

            return View(editionEventDto);
        }

        #region Main Information Widget

        /// <summary>Shows the main information widget.</summary>
        /// <param name="editionEventUid">The edition event uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowMainInformationWidget(Guid? editionEventUid)
        {
            var mainInformationWidgetDto = await this.editionEventRepo.FindDtoAsync(editionEventUid ?? Guid.Empty, this.EditionDto.Id);
            if (mainInformationWidgetDto == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Event, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/MainInformationWidget", mainInformationWidgetDto), divIdOrClass = "#EventMainInformationWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #region Update

        /// <summary>Shows the update main information modal.</summary>
        /// <param name="editionEventUid">The edition event uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowUpdateMainInformationModal(Guid? editionEventUid)
        {
            UpdateEditionEventMainInformation cmd;

            try
            {
                var mainInformationWidgetDto = await this.editionEventRepo.FindDtoAsync(editionEventUid ?? Guid.Empty, this.EditionDto.Id);
                if (mainInformationWidgetDto == null)
                {
                    throw new DomainException(string.Format(Messages.EntityNotAction, Labels.Event, Labels.FoundM.ToLowerInvariant()));
                }

                cmd = new UpdateEditionEventMainInformation(mainInformationWidgetDto);
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
        public async Task<ActionResult> UpdateMainInformation(UpdateEditionEventMainInformation cmd)
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

            return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Event, Labels.UpdatedM) });
        }

        #endregion

        #endregion

        #region Conferences Widget

        /// <summary>Shows the conferences widget.</summary>
        /// <param name="editionEventUid">The edition event uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowConferencesWidget(Guid? editionEventUid)
        {
            var conferencesWidgetDto = await this.editionEventRepo.FindConferenceWidgetDtoAsync(editionEventUid ?? Guid.Empty, this.EditionDto.Id);
            if (conferencesWidgetDto == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Event, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            ViewBag.StartDate = conferencesWidgetDto.EditionEvent.StartDate;
            ViewBag.EndDate = conferencesWidgetDto.EditionEvent.EndDate;

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("~/Views/Conferences/Widgets/RelatedConferencesWidget.cshtml", conferencesWidgetDto.ConferenceDtos), divIdOrClass = "#EventConferencesWidget" },
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
            var cmd = new CreateEditionEvent(null);

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Modals/CreateModal", cmd), divIdOrClass = "#GlobalModalContainer" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>Creates the specified edition event.</summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Create(CreateEditionEvent cmd)
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

            return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Event, Labels.CreatedM) });
        }

        #endregion

        #region Delete

        /// <summary>Deletes the specified delete edition event.</summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Delete(DeleteEditionEvent cmd)
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

            return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Event, Labels.DeletedM) });
        }

        #endregion
    }
}