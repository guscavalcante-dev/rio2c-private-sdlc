// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Renan Valentim
// Created          : 02-27-2024
//
// Last Modified By : Renan Valentim
// Last Modified On : 02-27-2024
// ***********************************************************************
// <copyright file="ProjectsController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Web.Mvc;
using MediatR;
using PlataformaRio2C.Infra.CrossCutting.Identity.Service;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlataformaRio2C.Application;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.CrossCutting.Identity.AuthorizeAttributes;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using PlataformaRio2C.Infra.CrossCutting.Tools.Helpers;
using PlataformaRio2C.Web.Site.Controllers;
using PlataformaRio2C.Web.Site.Filters;
using Constants = PlataformaRio2C.Domain.Constants;
using X.PagedList;
using PlataformaRio2C.Application.ViewModels;

namespace PlataformaRio2C.Web.Site.Areas.Creator.Controllers
{
    /// <summary>ProjectsController</summary>
    [AjaxAuthorize(Order = 1)]
    [AuthorizeCollaboratorType(Order = 2, Types = Constants.CollaboratorType.CommissionCreator)]
    public class ProjectsController : BaseController
    {
        private readonly IProjectEvaluationStatusRepository evaluationStatusRepo;
        private readonly IUserRepository userRepo;
        private readonly ICreatorProjectRepository creatorProjectRepo;
        private readonly IAttendeeCreatorProjectRepository attendeeCreatorProjectRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectsController" /> class.
        /// </summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="identityController">The identity controller.</param>
        /// <param name="evaluationStatusRepository">The evaluation status repository.</param>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="creatorProjectRepository">The creator project repository.</param>
        /// <param name="attendeeCreatorProjectRepository">The attendee creator project repository.</param>
        public ProjectsController(
            IMediator commandBus,
            IdentityAutenticationService identityController,
            IProjectEvaluationStatusRepository evaluationStatusRepository,
            IUserRepository userRepository,
            ICreatorProjectRepository creatorProjectRepository,
            IAttendeeCreatorProjectRepository attendeeCreatorProjectRepository)
            : base(commandBus, identityController)
        {
            this.evaluationStatusRepo = evaluationStatusRepository;
            this.userRepo = userRepository;
            this.creatorProjectRepo = creatorProjectRepository;
            this.attendeeCreatorProjectRepo = attendeeCreatorProjectRepository;
        }

        #region List

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <param name="searchViewModel">The search view model.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Index(CreatorProjectSearchViewModel searchViewModel)
        {
            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.Creator, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Labels.Projects, Url.Action("Index", "Projects", new { Area = "Creator" }))
            });

            #endregion

            searchViewModel.UpdateModelsAndLists(
               await this.evaluationStatusRepo.FindAllAsync(),
               this.UserInterfaceLanguage);

            return View(searchViewModel);
        }

        /// <summary>
        /// Evaluations the list.
        /// </summary>
        /// <param name="searchViewModel">The search view model.</param>
        /// <returns></returns>
        [HttpGet]
        [AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.CommissionCreator)]
        public async Task<ActionResult> EvaluationList(CreatorProjectSearchViewModel searchViewModel)
        {
            if (this.EditionDto?.IsCreatorProjectEvaluationStarted() != true)
            {
                return RedirectToAction("Index", "Projects", new { Area = "Creator" });
            }

            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.Creator, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Labels.Projects, Url.Action("EvaluationList", "Projects", new { Area = "Creator" })),
            });

            #endregion

            searchViewModel.UpdateModelsAndLists(
               await this.evaluationStatusRepo.FindAllAsync(),
               this.UserInterfaceLanguage);

            return View(searchViewModel);
        }

        /// <summary>
        /// Shows the evaluation list widget.
        /// </summary>
        /// <param name="searchViewModel">The search view model.</param>
        /// <returns></returns>
        [HttpGet]
        [AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.CommissionCreator)]
        public async Task<ActionResult> ShowEvaluationListWidget(CreatorProjectSearchViewModel searchViewModel)
        {
            if (this.EditionDto?.IsCreatorProjectEvaluationStarted() != true)
            {
                return Json(new { status = "error", message = Texts.ForbiddenErrorMessage }, JsonRequestBehavior.AllowGet);
            }

            var projects = await this.attendeeCreatorProjectRepo.FindAllDtosPagedAsync(
                this.EditionDto.Id,
                searchViewModel.SearchKeywords,
                searchViewModel.EvaluationStatusUid,
                searchViewModel.Page.Value,
                searchViewModel.PageSize.Value);

            ViewBag.CreatorProjectSearchViewModel = searchViewModel;
            ViewBag.ApprovedAttendeeCreatorProjectsIds = await this.attendeeCreatorProjectRepo.FindAllApprovedAttendeeCreatorProjectsIdsAsync(this.EditionDto.Edition.Id);

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/EvaluationListWidget", projects), divIdOrClass = "#CreatorProjectEvaluationListWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Shows the evaluation list item widget.
        /// </summary>
        /// <param name="projectUid">The project uid.</param>
        /// <returns></returns>
        [HttpGet]
        [AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.CommissionCreator)]
        public async Task<ActionResult> ShowEvaluationListItemWidget(Guid? projectUid)
        {
            if (this.EditionDto?.IsCreatorProjectEvaluationStarted() != true)
            {
                return Json(new { status = "error", message = Texts.ForbiddenErrorMessage }, JsonRequestBehavior.AllowGet);
            }

            if (!projectUid.HasValue)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM) }, JsonRequestBehavior.AllowGet);
            }

            var project = await this.attendeeCreatorProjectRepo.FindDtoToEvaluateAsync(projectUid.Value);

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/EvaluationListItemWidget", project), divIdOrClass = $"#project-{projectUid}" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Details

        /// <summary>
        /// Evaluations the details.
        /// </summary>
        /// <param name="searchViewModel">The search view model.</param>
        /// <returns></returns>
        [AuthorizeCollaboratorType(Types = Constants.CollaboratorType.CommissionCreator)]
        public async Task<ActionResult> EvaluationDetails(CreatorProjectSearchViewModel searchViewModel)
        {
            if (this.EditionDto?.IsCreatorProjectEvaluationStarted() != true)
            {
                this.StatusMessageToastr(Messages.OutOfEvaluationPeriod, Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);
                return RedirectToAction("Index", "Projects", new { Area = "Creator" });
            }

            var attendeeCreatorProjectDto = await this.attendeeCreatorProjectRepo.FindDtoToEvaluateAsync(searchViewModel.Id ?? 0);

            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.CreatorProjects, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Labels.Projects, Url.Action("EvaluationList", "Projects", new { Area = "Creator", searchViewModel.SearchKeywords, searchViewModel.EvaluationStatusUid, searchViewModel.Page, searchViewModel.PageSize })),
                new BreadcrumbItemHelper(attendeeCreatorProjectDto?.CreatorProjectDto?.Title ?? Labels.Project, Url.Action("EvaluationDetails", "Projects", new { Area = "Creator", searchViewModel.Id }))
            });

            #endregion

            var allCreatorProjectsIds = await this.attendeeCreatorProjectRepo.FindAllCreatorProjectsIdsPagedAsync(
                this.EditionDto.Edition.Id,
                searchViewModel.SearchKeywords,
                searchViewModel.EvaluationStatusUid,
                searchViewModel.Page.Value,
                searchViewModel.PageSize.Value);

            var currentCreatorProjectIdIndex = Array.IndexOf(allCreatorProjectsIds, searchViewModel.Id.Value) + 1; //Index start at 0, its a fix to "start at 1"

            ViewBag.CreatorProjectSearchViewModel = searchViewModel;
            ViewBag.CurrentProjectIndex = currentCreatorProjectIdIndex;
            ViewBag.ApprovedAttendeeCreatorProjectsIds = await this.attendeeCreatorProjectRepo.FindAllApprovedAttendeeCreatorProjectsIdsAsync(this.EditionDto.Edition.Id);
            ViewBag.TotalProjectsCount = await this.attendeeCreatorProjectRepo.CountPagedAsync(
                this.EditionDto.Edition.Id,
                searchViewModel.SearchKeywords,
                searchViewModel.EvaluationStatusUid,
                searchViewModel.Page.Value,
                searchViewModel.PageSize.Value);

            return View(attendeeCreatorProjectDto);
        }

        /// <summary>
        /// Previouses the evaluation details.
        /// </summary>
        /// <param name="searchViewModel">The search view model.</param>
        /// <returns></returns>
        [AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.CommissionCreator)]
        public async Task<ActionResult> PreviousEvaluationDetails(CreatorProjectSearchViewModel searchViewModel)
        {
            var allCreatorProjectsIds = await this.attendeeCreatorProjectRepo.FindAllCreatorProjectsIdsPagedAsync(
                this.EditionDto.Edition.Id,
                searchViewModel.SearchKeywords,
                searchViewModel.EvaluationStatusUid,
                searchViewModel.Page.Value,
                searchViewModel.PageSize.Value);

            var currentCreatorProjectIdIndex = Array.IndexOf(allCreatorProjectsIds, searchViewModel.Id.Value);
            var previousProjectId = allCreatorProjectsIds.ElementAtOrDefault(currentCreatorProjectIdIndex - 1);
            if (previousProjectId == 0)
            {
                previousProjectId = searchViewModel.Id.Value;
            }
            searchViewModel.Id = previousProjectId;

            return RedirectToAction("EvaluationDetails", searchViewModel);
        }

        /// <summary>
        /// Nexts the evaluation details.
        /// </summary>
        /// <param name="searchViewModel">The search view model.</param>
        /// <returns></returns>
        [AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.CommissionCreator)]
        public async Task<ActionResult> NextEvaluationDetails(CreatorProjectSearchViewModel searchViewModel)
        {
            var allCreatorProjectsIds = await this.attendeeCreatorProjectRepo.FindAllCreatorProjectsIdsPagedAsync(
                this.EditionDto.Edition.Id,
                searchViewModel.SearchKeywords,
                searchViewModel.EvaluationStatusUid,
                searchViewModel.Page.Value,
                searchViewModel.PageSize.Value);

            var currentCreatorProjectIdIndex = Array.IndexOf(allCreatorProjectsIds, searchViewModel.Id.Value);
            var nextProjectId = allCreatorProjectsIds.ElementAtOrDefault(currentCreatorProjectIdIndex + 1);
            if (nextProjectId == 0)
            {
                nextProjectId = searchViewModel.Id.Value;
            }
            searchViewModel.Id = nextProjectId;

            return RedirectToAction("EvaluationDetails", searchViewModel);
        }

        #endregion

        #region Main Information Widget

        /// <summary>
        /// Shows the main information widget.
        /// </summary>
        /// <param name="attendeeCreatorProjectUid">The attendee innovation organization uid.</param>
        /// <returns></returns>
        [HttpGet]
        [AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.CommissionCreator)]
        public async Task<ActionResult> ShowMainInformationWidget(Guid? attendeeCreatorProjectUid)
        {
            var attendeeCreatorProjectDto = await this.attendeeCreatorProjectRepo.FindMainInformationWidgetDtoAsync(attendeeCreatorProjectUid ?? Guid.Empty);
            if (attendeeCreatorProjectDto == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/MainInformationWidget", attendeeCreatorProjectDto), divIdOrClass = "#ProjectMainInformationWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Project Information Widget

        /// <summary>
        /// Shows the project information widget.
        /// </summary>
        /// <param name="attendeeCreatorProjectUid">The attendee creator project uid.</param>
        /// <returns></returns>
        [HttpGet]
        [AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.CommissionCreator)]
        public async Task<ActionResult> ShowProjectInformationWidget(Guid? attendeeCreatorProjectUid)
        {
            var attendeeCreatorProjectDto = await this.attendeeCreatorProjectRepo.FindProjectInformationWidgetDtoAsync(attendeeCreatorProjectUid ?? Guid.Empty);
            if (attendeeCreatorProjectDto == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/ProjectInformationWidget", attendeeCreatorProjectDto), divIdOrClass = "#ProjectInformationWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        //#region Evaluation Grade Widget 

        ///// <summary>
        ///// Shows the evaluation grade widget.
        ///// </summary>
        ///// <param name="projectUid">The project uid.</param>
        ///// <returns></returns>
        //[HttpGet]
        //[AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.CommissionCreator)]
        //public async Task<ActionResult> ShowEvaluationGradeWidget(Guid? attendeeCreatorOrganizationUid)
        //{
        //    if (this.EditionDto?.IsCreatorProjectEvaluationStarted() != true)
        //    {
        //        return Json(new { status = "error", message = Texts.ForbiddenErrorMessage }, JsonRequestBehavior.AllowGet);
        //    }

        //    var attendeeCreatorOrganizationDto = await this.attendeeCreatorOrganizationRepo.FindEvaluationGradeWidgetDtoAsync(attendeeCreatorOrganizationUid ?? Guid.Empty, this.UserAccessControlDto.User.Id);
        //    if (attendeeCreatorOrganizationDto == null)
        //    {
        //        return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
        //    }

        //    #region AttendeeCollaborator and AttendeeCreatorOrganization Tracks validation

        //    var attendeeCollaboratorCreatorOrganizationTrackOptionGroupsUids = await this.GetAttendeeCollaboratorCreatorOrganizationTrackOptionsGroupUids();
        //    if (attendeeCreatorOrganizationDto.AttendeeCreatorOrganizationTrackDtos.Any(aiotDto => attendeeCollaboratorCreatorOrganizationTrackOptionGroupsUids.Contains(aiotDto.CreatorOrganizationTrackOptionGroup?.Uid)) == false)
        //    {
        //        this.StatusMessageToastr(string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()), Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);
        //        return RedirectToAction("EvaluationList", "Projects", new { Area = "Creator" });
        //    }

        //    #endregion

        //    ViewBag.ApprovedAttendeeCreatorOrganizationsIds = await this.attendeeCreatorOrganizationRepo.FindAllApprovedAttendeeCreatorOrganizationsIdsAsync(this.EditionDto.Edition.Id);

        //    return Json(new
        //    {
        //        status = "success",
        //        pages = new List<dynamic>
        //        {
        //            new { page = this.RenderRazorViewToString("Widgets/EvaluationGradeWidget", attendeeCreatorOrganizationDto), divIdOrClass = "#ProjectEvaluationWidget" },
        //        }
        //    }, JsonRequestBehavior.AllowGet);
        //}

        ///// <summary>
        ///// Evaluates the specified innovation organization identifier.
        ///// </summary>
        ///// <param name="innovationOrganizationId">The innovation organization identifier.</param>
        ///// <param name="grade">The grade.</param>
        ///// <returns></returns>
        ///// <exception cref="DomainException"></exception>
        //[HttpPost]
        //[AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.CommissionCreator)]
        //public async Task<ActionResult> Evaluate(int innovationOrganizationId, decimal? grade)
        //{
        //    //if (this.EditionDto?.IsCreatorProjectEvaluationOpen() != true)
        //    //{
        //    //    return Json(new { status = "error", message = Messages.OutOfEvaluationPeriod }, JsonRequestBehavior.AllowGet);
        //    //}

        //    var result = new AppValidationResult();

        //    try
        //    {
        //        var cmd = new EvaluateCreatorOrganization(
        //            await this.innovationOrganizationRepo.FindByIdAsync(innovationOrganizationId),
        //            grade);

        //        cmd.UpdatePreSendProperties(
        //            this.UserAccessControlDto.User.Id,
        //            this.UserAccessControlDto.User.Uid,
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
        //        return Json(new
        //        {
        //            status = "error",
        //            message = result.Errors.Select(e => e = new AppValidationError(e.Message, "ToastrError", e.Code))?
        //                                    .FirstOrDefault(e => e.Target == "ToastrError")?.Message ?? ex.GetInnerMessage(),
        //        }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        //        return Json(new { status = "error", message = Messages.WeFoundAndError, }, JsonRequestBehavior.AllowGet);
        //    }

        //    return Json(new
        //    {
        //        status = "success",
        //        //projectUid = cmd.CreatorBandId,
        //        message = string.Format(Messages.EntityActionSuccessfull, Labels.Startup, Labels.Evaluated.ToLowerInvariant())
        //    });
        //}

        //#endregion

        //#region Evaluators Widget

        //[HttpGet]
        //[AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.CommissionCreator)]
        //public async Task<ActionResult> ShowEvaluatorsWidget(Guid? attendeeCreatorOrganizationUid)
        //{
        //    if (this.EditionDto?.IsCreatorProjectEvaluationStarted() != true)
        //    {
        //        return Json(new { status = "error", message = Texts.ForbiddenErrorMessage }, JsonRequestBehavior.AllowGet);
        //    }

        //    var attendeeCreatorOrganizationDto = await this.attendeeCreatorOrganizationRepo.FindEvaluatorsWidgetDtoAsync(attendeeCreatorOrganizationUid ?? Guid.Empty);
        //    if (attendeeCreatorOrganizationDto == null)
        //    {
        //        return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
        //    }

        //    #region AttendeeCollaborator and AttendeeCreatorOrganization Tracks validation

        //    var attendeeCollaboratorCreatorOrganizationTrackOptionGroupsUids = await this.GetAttendeeCollaboratorCreatorOrganizationTrackOptionsGroupUids();
        //    if (attendeeCreatorOrganizationDto.AttendeeCreatorOrganizationTrackDtos.Any(aiotDto => attendeeCollaboratorCreatorOrganizationTrackOptionGroupsUids.Contains(aiotDto.CreatorOrganizationTrackOptionGroup?.Uid)) == false)
        //    {
        //        this.StatusMessageToastr(string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()), Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);
        //        return RedirectToAction("EvaluationList", "Projects", new { Area = "Creator" });
        //    }

        //    #endregion

        //    return Json(new
        //    {
        //        status = "success",
        //        pages = new List<dynamic>
        //        {
        //            new { page = this.RenderRazorViewToString("Widgets/EvaluatorsWidget", attendeeCreatorOrganizationDto), divIdOrClass = "#ProjectEvaluatorsWidget" },
        //        }
        //    }, JsonRequestBehavior.AllowGet);
        //}

        //#endregion

        //#region Founders Widget

        ///// <summary>
        ///// Shows the evaluators widget.
        ///// </summary>
        ///// <param name="attendeeCreatorOrganizationUid">The project uid.</param>
        ///// <returns></returns>
        //[HttpGet]
        //public async Task<ActionResult> ShowFoundersWidget(Guid? attendeeCreatorOrganizationUid)
        //{
        //    var attendeeCreatorOrganizationDto = await this.attendeeCreatorOrganizationRepo.FindFoundersWidgetDtoAsync(attendeeCreatorOrganizationUid ?? Guid.Empty);
        //    if (attendeeCreatorOrganizationDto == null)
        //    {
        //        return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Startup, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
        //    }

        //    #region AttendeeCollaborator and AttendeeCreatorOrganization Tracks validation

        //    var attendeeCollaboratorCreatorOrganizationTrackOptionGroupsUids = await this.GetAttendeeCollaboratorCreatorOrganizationTrackOptionsGroupUids();
        //    if (attendeeCreatorOrganizationDto.AttendeeCreatorOrganizationTrackDtos.Any(aiotDto => attendeeCollaboratorCreatorOrganizationTrackOptionGroupsUids.Contains(aiotDto.CreatorOrganizationTrackOptionGroup?.Uid)) == false)
        //    {
        //        this.StatusMessageToastr(string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()), Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);
        //        return RedirectToAction("EvaluationList", "Projects", new { Area = "Creator" });
        //    }

        //    #endregion

        //    return Json(new
        //    {
        //        status = "success",
        //        pages = new List<dynamic>
        //        {
        //            new { page = this.RenderRazorViewToString("Widgets/FoundersWidget", attendeeCreatorOrganizationDto), divIdOrClass = "#ProjectsFoundersWidget" },
        //        }
        //    }, JsonRequestBehavior.AllowGet);
        //}

        //#endregion

        //#region Presentation Widget

        ///// <summary>Shows the clipping widget.</summary>
        ///// <param name="projectUid">The project uid.</param>
        ///// <returns></returns>
        //[HttpGet]
        //public async Task<ActionResult> ShowPresentationWidget(Guid? attendeeCreatorOrganizationUid)
        //{
        //    var attendeeCreatorOrganizationDto = await this.attendeeCreatorOrganizationRepo.FindPresentationWidgetDtoAsync(attendeeCreatorOrganizationUid ?? Guid.Empty);
        //    if (attendeeCreatorOrganizationDto == null)
        //    {
        //        return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
        //    }

        //    #region AttendeeCollaborator and AttendeeCreatorOrganization Tracks validation

        //    var attendeeCollaboratorCreatorOrganizationTrackOptionGroupsUids = await this.GetAttendeeCollaboratorCreatorOrganizationTrackOptionsGroupUids();
        //    if (attendeeCreatorOrganizationDto.AttendeeCreatorOrganizationTrackDtos.Any(aiotDto => attendeeCollaboratorCreatorOrganizationTrackOptionGroupsUids.Contains(aiotDto.CreatorOrganizationTrackOptionGroup?.Uid)) == false)
        //    {
        //        this.StatusMessageToastr(string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()), Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);
        //        return RedirectToAction("EvaluationList", "Projects", new { Area = "Creator" });
        //    }

        //    #endregion

        //    return Json(new
        //    {
        //        status = "success",
        //        pages = new List<dynamic>
        //        {
        //            new { page = this.RenderRazorViewToString("Widgets/PresentationWidget", attendeeCreatorOrganizationDto), divIdOrClass = "#ProjectsPresentationWidget" },
        //        }
        //    }, JsonRequestBehavior.AllowGet);
        //}

        //#endregion
    }
}