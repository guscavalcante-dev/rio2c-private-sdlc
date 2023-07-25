// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Renan Valentim
// Created          : 07-04-2023
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-25-2023
// ***********************************************************************
// <copyright file="TrackOptionsController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using DataTables.AspNet.Core;
using DataTables.AspNet.Mvc5;
using MediatR;
using PlataformaRio2C.Application;
using PlataformaRio2C.Application.CQRS.Commands;
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
    [AjaxAuthorize(Order = 1, Roles = Constants.Role.AnyAdmin)]
    [AuthorizeCollaboratorType(Order = 2, Types = Constants.CollaboratorType.AdminInnovation)]
    public class TrackOptionsController : BaseController
    {
        private readonly IInnovationOrganizationTrackOptionRepository innovationOrganizationTrackOptionRepo;
        private readonly IInnovationOrganizationTrackOptionGroupRepository innovationOrganizationTrackOptionGroupRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="TrackOptionsController" /> class.
        /// </summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="identityControlle">The identity controlle.</param>
        /// <param name="innovationOrganizationTrackOptionRepository">The innovation organization track option repository.</param>
        /// <param name="innovationOrganizationTrackOptionGroupRepository">The innovation organization track option group repository.</param>
        public TrackOptionsController(
            IMediator commandBus, 
            IdentityAutenticationService identityControlle,
            IInnovationOrganizationTrackOptionRepository innovationOrganizationTrackOptionRepository,
            IInnovationOrganizationTrackOptionGroupRepository innovationOrganizationTrackOptionGroupRepository) 
            : base(commandBus, identityControlle)
        {
            this.innovationOrganizationTrackOptionRepo = innovationOrganizationTrackOptionRepository;
            this.innovationOrganizationTrackOptionGroupRepo = innovationOrganizationTrackOptionGroupRepository;
        }

        #region List

        /// <summary>
        /// Indexes the specified search view model.
        /// </summary>
        /// <param name="searchViewModel">The search view model.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Index(InnovationTrackOptionSearchViewModel searchViewModel)
        {
            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.CreativeEconomyThemes, new List<BreadcrumbItemHelper>{
                new BreadcrumbItemHelper(Labels.Innovation, null),
                new BreadcrumbItemHelper(Labels.CreativeEconomyThemes, Url.Action("Index", "TrackOptions", new { Area = "Innovation" }))
            });

            #endregion

            return View(searchViewModel);
        }

        /// <summary>
        /// Searches the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Search(IDataTablesRequest request)
        {
            int page = request.Start / request.Length;
            int pageSize = request.Length;
            page++; //Necessary because DataTable is zero index based.

            var innovationOrganizationTrackOptionDtos = await this.innovationOrganizationTrackOptionRepo.FindAllByDataTable(
                page,
                pageSize,
                request.Search?.Value,
                request.GetSortColumns());

            var response = DataTablesResponse.Create(request, innovationOrganizationTrackOptionDtos.TotalItemCount, innovationOrganizationTrackOptionDtos.TotalItemCount, innovationOrganizationTrackOptionDtos);

            return Json(new
            {
                status = "success",
                dataTable = response
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Total Count Widget

        /// <summary>
        /// Shows the total count widget.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowTotalCountWidget()
        {
            var tracksCount = await this.innovationOrganizationTrackOptionRepo.CountAllByDataTable(true, this.EditionDto.Id);

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/TotalCountWidget", tracksCount), divIdOrClass = "#InnovationTrackOptionsTotalCountWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Edition Count Widget

        /// <summary>
        /// Shows the edition count widget.
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> ShowEditionCountWidget()
        {
            var tracksCount = await this.innovationOrganizationTrackOptionRepo.CountAllByDataTable(false, this.EditionDto.Id);

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/EditionCountWidget", tracksCount), divIdOrClass = "#InnovationTrackOptionsEditionCountWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Details

        /// <summary>
        /// Detailses the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Details(Guid? id)
        {
            var innovationOrganizationTrackOptionDto = await this.innovationOrganizationTrackOptionRepo.FindMainInformationWidgetDtoAsync(id ?? Guid.Empty);
            if (innovationOrganizationTrackOptionDto == null)
            {
                this.StatusMessageToastr(string.Format(Messages.EntityNotAction, Labels.CreativeEconomyTheme, Labels.FoundF.ToLowerInvariant()), Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);
                return RedirectToAction("Index", "TrackOptions", new { Area = "Innovation" });
            }

            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.CreativeEconomyThemes, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Labels.Innovation, null),
                new BreadcrumbItemHelper(Labels.CreativeEconomyThemes, Url.Action("Index", "TrackOptions", new { Area = "Innovation" })),
                new BreadcrumbItemHelper(innovationOrganizationTrackOptionDto.Name, Url.Action("Details", "TrackOptions", new { id }))
            });

            #endregion

            return View(innovationOrganizationTrackOptionDto);
        }

        #region Main Information Widget

        /// <summary>
        /// Shows the main information widget.
        /// </summary>
        /// <param name="innovationOrganizationTrackOptionUid">The innovation organization track option uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowMainInformationWidget(Guid? innovationOrganizationTrackOptionUid)
        {
            var mainInformationWidgetDto = await this.innovationOrganizationTrackOptionRepo.FindMainInformationWidgetDtoAsync(innovationOrganizationTrackOptionUid ?? Guid.Empty);
            if (mainInformationWidgetDto == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.CreativeEconomyTheme, Labels.FoundF.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/MainInformationWidget", mainInformationWidgetDto), divIdOrClass = "#InnovationTrackOptionsMainInformationWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        //#region Update

        ///// <summary>
        ///// Shows the update main information modal.
        ///// </summary>
        ///// <param name="innovationOrganizationTrackOptionGroupUid">The innovation organization track option group uid.</param>
        ///// <returns></returns>
        //[HttpGet]
        //public async Task<ActionResult> ShowUpdateMainInformationModal(Guid? innovationOrganizationTrackOptionGroupUid)
        //{
        //    UpdateInnovationOrganizationTrackOptionGroupMainInformation cmd;

        //    try
        //    {
        //        var mainInformationWidgetDto = await this.innovationOrganizationTrackOptionGroupRepo.FindMainInformationWidgetDtoAsync(innovationOrganizationTrackOptionGroupUid ?? Guid.Empty);
        //        if (mainInformationWidgetDto == null)
        //        {
        //            return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Vertical, Labels.FoundF.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
        //        }

        //        cmd = new UpdateInnovationOrganizationTrackOptionGroupMainInformation(mainInformationWidgetDto);
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

        ///// <summary>
        ///// Updates the specified command.
        ///// </summary>
        ///// <param name="cmd">The command.</param>
        ///// <returns></returns>
        ///// <exception cref="PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions.DomainException"></exception>
        //[HttpPost]
        //public async Task<ActionResult> UpdateMainInformation(UpdateInnovationOrganizationTrackOptionGroupMainInformation cmd)
        //{
        //    var result = new AppValidationResult();

        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            throw new DomainException(Messages.CorrectFormValues);
        //        }

        //        cmd.UpdatePreSendProperties(
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
        //            message = result.Errors?.FirstOrDefault(e => e.Target == "ToastrError")?.Message ?? ex.GetInnerMessage(),
        //            pages = new List<dynamic>
        //            {
        //                new { page = this.RenderRazorViewToString("Modals/_Form", cmd), divIdOrClass = "#form-container" },
        //            }
        //        }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        //        return Json(new { status = "error", message = Messages.WeFoundAndError, }, JsonRequestBehavior.AllowGet);
        //    }

        //    return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Member, Labels.UpdatedM) });
        //}

        //#endregion

        #endregion

        #endregion

        #region Create

        /// <summary>Shows the create modal.</summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowCreateModal()
        {
            var cmd = new CreateInnovationOrganizationTrackOption(await this.innovationOrganizationTrackOptionGroupRepo.FindAllDtoAsync());

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Modals/CreateModal", cmd), divIdOrClass = "#GlobalModalContainer" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Creates the specified command.
        /// </summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        /// <exception cref="PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions.DomainException"></exception>
        [HttpPost]
        public async Task<ActionResult> Create(CreateInnovationOrganizationTrackOption cmd)
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

                cmd.UpdateDropdowns(await this.innovationOrganizationTrackOptionGroupRepo.FindAllDtoAsync());

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

            return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Vertical, Labels.CreatedF) });
        }

        #endregion
    }
}