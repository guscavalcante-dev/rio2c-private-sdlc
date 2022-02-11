// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Franco
// Created          : 02-08-2022
//
// Last Modified By : Rafael Franco
// Last Modified On : 02-08-2022
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
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.CrossCutting.Identity.AuthorizeAttributes;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using PlataformaRio2C.Infra.CrossCutting.Tools.Helpers;
using PlataformaRio2C.Web.Site.Controllers;
using PlataformaRio2C.Web.Site.Filters;
using Constants = PlataformaRio2C.Domain.Constants;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Web.Site.Areas.Cartoon.Controllers
{
    /// <summary>ProjectsController</summary>
    [AjaxAuthorize(Order = 1)]
    [AuthorizeCollaboratorType(Order = 2, Types = Constants.CollaboratorType.CommissionCartoon)]
    public class ProjectsController : BaseController
    {
        private readonly ICartoonProjectRepository cartoonProjectRepo;
        private readonly IAttendeeCartoonProjectRepository attendeeCartoonProjectRepo;
        private readonly IProjectEvaluationStatusRepository evaluationStatusRepo;
        private readonly IUserRepository userRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectsController" /> class.
        /// </summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="identityController">The identity controller.</param>
        /// <param name="cartoonProjectRepository">The attendee cartoon organization repository.</param>
        /// <param name="attendeeCartoonProjectRepository">The attendee cartoon organization repository.</param>
        /// <param name="evaluationStatusRepository">The evaluation status repository.</param>
        /// <param name="userRepository">The user repository.</param>
        public ProjectsController(
            IMediator commandBus,
            IdentityAutenticationService identityController,
            ICartoonProjectRepository cartoonProjectRepository,
            IAttendeeCartoonProjectRepository attendeeCartoonProjectRepository,
            IProjectEvaluationStatusRepository evaluationStatusRepository,
            IUserRepository userRepository
            )
            : base(commandBus, identityController)
        {
            this.cartoonProjectRepo = cartoonProjectRepository;
            this.attendeeCartoonProjectRepo = attendeeCartoonProjectRepository;
            this.evaluationStatusRepo = evaluationStatusRepository;
            this.userRepo = userRepository;
        }

        #region List

        /// <summary>Indexes this instance.</summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Index(Guid? evaluationStatusUid)
        {
            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.Cartoonito, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Labels.Projects, Url.Action("Index", "Projects", new { Area = "Cartoon" }))
            });

            #endregion

            ViewBag.EvaluationStatusUid = evaluationStatusUid;
            ViewBag.Page = 1;
            ViewBag.PageSize = 10;

            ViewBag.ProjectEvaluationStatuses = (await this.evaluationStatusRepo.FindAllAsync()).GetSeparatorTranslation(m => m.Name, this.UserInterfaceLanguage, '|');

            return View();
        }

        /// <summary>
        /// Evaluations the list.
        /// </summary>
        /// <param name="searchKeywords">The search keywords.</param>
        /// <param name="evaluationStatusUid">The evaluation status uid.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        [HttpGet]
        [AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.CommissionCartoon)]
        public async Task<ActionResult> EvaluationList(string searchKeywords, Guid? evaluationStatusUid, int? page = 1, int? pageSize = 12)
        {
            if (this.EditionDto?.IsCartoonProjectEvaluationStarted() != true)
            {
                return RedirectToAction("Index", "Projects", new { Area = "Cartoon" });
            }

            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.CartoonProjects, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Labels.Cartoonito, Url.Action("EvaluationList", "Projects", new { Area = "Cartoon" })),
            });

            #endregion

            ViewBag.SearchKeywords = searchKeywords;
            ViewBag.EvaluationStatusUid = evaluationStatusUid;
            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;
            ViewBag.ProjectEvaluationStatuses = await this.evaluationStatusRepo.FindAllAsync();

            return View();
        }

        /// <summary>
        /// Shows the evaluation list widget.
        /// </summary>
        /// <param name="searchKeywords">The search keywords.</param>
        /// <param name="projectFormatUid">The project format uid.</param>
        /// <param name="evaluationStatusUid">The evaluation status uid.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        [HttpGet]
        [AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.CommissionCartoon)]
        public async Task<ActionResult> ShowEvaluationListWidget(string searchKeywords, Guid? projectFormatUid, Guid? evaluationStatusUid, int? page = 1, int? pageSize = 12)
        {
            if (this.EditionDto?.IsCartoonProjectEvaluationStarted() != true)
            {
                return Json(new { status = "error", message = Texts.ForbiddenErrorMessage }, JsonRequestBehavior.AllowGet);
            }

            var projects = await this.attendeeCartoonProjectRepo.FindAllDtosPagedAsync(
                this.EditionDto.Id,
                searchKeywords,
                new List<Guid?> { projectFormatUid },
                evaluationStatusUid,
                page.Value,
                pageSize.Value);

            ViewBag.SearchKeywords = searchKeywords;
            ViewBag.EvaluationStatusUid = evaluationStatusUid;
            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;
            ViewBag.ApprovedAttendeeCartoonProjectsIds = await this.attendeeCartoonProjectRepo.FindAllApprovedAttendeeCartoonProjectsIdsAsync(this.EditionDto.Edition.Id);

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/EvaluationListWidget", projects), divIdOrClass = "#CartoonProjectEvaluationListWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Shows the evaluation list item widget.
        /// </summary>
        /// <param name="projectUid">The project uid.</param>
        /// <returns></returns>
        [HttpGet]
        [AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.CommissionCartoon)]
        public async Task<ActionResult> ShowEvaluationListItemWidget(Guid? projectUid)
        {
            if (this.EditionDto?.IsCartoonProjectEvaluationStarted() != true)
            {
                return Json(new { status = "error", message = Texts.ForbiddenErrorMessage }, JsonRequestBehavior.AllowGet);
            }

            if (!projectUid.HasValue)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM) }, JsonRequestBehavior.AllowGet);
            }

            var projects = await this.attendeeCartoonProjectRepo.FindDtoToEvaluateAsync(projectUid.Value);

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/EvaluationListItemWidget", projects), divIdOrClass = $"#project-{projectUid}" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Details

        /// <summary>
        /// Evaluations the details.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="searchKeywords">The search keywords.</param>
        /// <param name="projectFormatUid">The project format uid.</param>
        /// <param name="evaluationStatusUid">The evaluation status uid.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        [AuthorizeCollaboratorType(Types = Constants.CollaboratorType.CommissionCartoon)]
        public async Task<ActionResult> EvaluationDetails(int? id, string searchKeywords = null, Guid? projectFormatUid = null, Guid? evaluationStatusUid = null, int? page = 1, int? pageSize = 12)
        {
            if (this.EditionDto?.IsCartoonProjectEvaluationStarted() != true)
            {
                this.StatusMessageToastr(Messages.OutOfEvaluationPeriod, Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);
                return RedirectToAction("Index", "Projects", new { Area = "Cartoon" });
            }

            var attendeeCartoonProjectDto = await this.attendeeCartoonProjectRepo.FindDtoToEvaluateAsync(id ?? 0);

            if(attendeeCartoonProjectDto == null)
            {
                this.StatusMessageToastr(string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()), Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);
                return RedirectToAction("EvaluationList", "Projects", new { Area = "Cartoon" });
            }        
            
            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.CartoonProjects, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Labels.Cartoonito, Url.Action("EvaluationList", "Projects", new { Area = "Cartoon", searchKeywords, evaluationStatusUid, page, pageSize })),
                new BreadcrumbItemHelper(attendeeCartoonProjectDto?.CartoonProject?.Title ?? Labels.Project, Url.Action("EvaluationDetails", "Projects", new { Area = "Cartoon", id }))
            });

            #endregion

            var allCartoonProjectsIds = await this.attendeeCartoonProjectRepo.FindAllCartoonProjectsIdsPagedAsync(
                this.EditionDto.Edition.Id,
                searchKeywords,
                new List<Guid?> { projectFormatUid },
                evaluationStatusUid,
                page.Value,
                pageSize.Value);
            var currentCartoonProjectIdIndex = Array.IndexOf(allCartoonProjectsIds, id.Value) + 1; //Index start at 0, its a fix to "start at 1"

            ViewBag.SearchKeywords = searchKeywords;
            ViewBag.EvaluationStatusUid = evaluationStatusUid;
            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;
            ViewBag.CurrentProjectIndex = currentCartoonProjectIdIndex;
            ViewBag.CartoonProjectsTotalCount = await this.attendeeCartoonProjectRepo.CountPagedAsync(this.EditionDto.Edition.Id, searchKeywords, new List<Guid?> { projectFormatUid }, evaluationStatusUid, page.Value, pageSize.Value);
            ViewBag.ApprovedAttendeeCartoonProjectsIds = await this.attendeeCartoonProjectRepo.FindAllApprovedAttendeeCartoonProjectsIdsAsync(this.EditionDto.Edition.Id);

            return View(attendeeCartoonProjectDto);
        }

        /// <summary>
        /// Previouses the evaluation details.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="searchKeywords">The search keywords.</param>
        /// <param name="projectFormatUid">The project format uid.</param>
        /// <param name="evaluationStatusUid">The evaluation status uid.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        [AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.CommissionCartoon)]
        public async Task<ActionResult> PreviousEvaluationDetails(int? id, string searchKeywords = null, Guid? projectFormatUid = null, Guid? evaluationStatusUid = null, int? page = 1, int? pageSize = 12)
        {
            var allCartoonProjectsIds = await this.attendeeCartoonProjectRepo.FindAllCartoonProjectsIdsPagedAsync(
                this.EditionDto.Edition.Id,
                searchKeywords,
                new List<Guid?> { projectFormatUid },
                evaluationStatusUid,
                page.Value,
                pageSize.Value);

            var currentCartoonProjectIdIndex = Array.IndexOf(allCartoonProjectsIds, id.Value);
            var previousProjectId = allCartoonProjectsIds.ElementAtOrDefault(currentCartoonProjectIdIndex - 1);
            if (previousProjectId == 0)
                previousProjectId = id.Value;

            return RedirectToAction("EvaluationDetails",
                new
                {
                    id = previousProjectId,
                    searchKeywords,
                    evaluationStatusUid,
                    page,
                    pageSize
                });
        }

        /// <summary>
        /// Nexts the evaluation details.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="searchKeywords">The search keywords.</param>
        /// <param name="projectFormatUid">The project format uid.</param>
        /// <param name="evaluationStatusUid">The evaluation status uid.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        [AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.CommissionCartoon)]
        public async Task<ActionResult> NextEvaluationDetails(int? id, string searchKeywords = null, Guid? projectFormatUid = null, Guid? evaluationStatusUid = null, int? page = 1, int? pageSize = 12)
        {
           
            var allCartoonProjectsIds = await this.attendeeCartoonProjectRepo.FindAllCartoonProjectsIdsPagedAsync(
                this.EditionDto.Edition.Id,
                searchKeywords,
                new List<Guid?> { projectFormatUid },
                evaluationStatusUid,
                page.Value,
                pageSize.Value);

            var currentCartoonProjectIdIndex = Array.IndexOf(allCartoonProjectsIds, id.Value);
            var nextProjectId = allCartoonProjectsIds.ElementAtOrDefault(currentCartoonProjectIdIndex + 1);
            if (nextProjectId == 0)
                nextProjectId = id.Value;

            return RedirectToAction("EvaluationDetails",
                new
                {
                    id = nextProjectId,
                    searchKeywords,
                    evaluationStatusUid,
                    page,
                    pageSize
                });
        }

        #endregion

        #region Main Information Widget

        /// <summary>
        /// Shows the main information widget.
        /// </summary>
        /// <param name="attendeeCartoonProjectUid">The attendee cartoon project uid.</param>
        /// <returns></returns>
        [HttpGet]
        [AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.CommissionCartoon)]
        public async Task<ActionResult> ShowMainInformationWidget(Guid? attendeeCartoonProjectUid)
        {
            var mainInformationWidgetDto = await this.attendeeCartoonProjectRepo.FindMainInformationWidgetDtoAsync(attendeeCartoonProjectUid ?? Guid.Empty);
            if (mainInformationWidgetDto == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Startup, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/MainInformationWidget", mainInformationWidgetDto), divIdOrClass = "#ProjectMainInformationWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Organization Widget

        /// <summary>
        /// Shows the organization widget.
        /// </summary>
        /// <param name="attendeeCartoonProjectUid">The attendee cartoon project uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowOrganizationWidget(Guid? attendeeCartoonProjectUid)
        {
            var mainInformationWidgetDto = await this.attendeeCartoonProjectRepo.FindOrganizationWidgetDtoAsync(attendeeCartoonProjectUid ?? Guid.Empty);
            if (mainInformationWidgetDto == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Startup, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            #region AttendeeCollaborator and AttendeeInnovationOrganization Tracks validation

           if (mainInformationWidgetDto == null)
                {
                this.StatusMessageToastr(string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()), Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);
                return RedirectToAction("EvaluationList", "Projects", new { Area = "Cartoon" });
            }

            #endregion

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/BusinessInformationWidget", mainInformationWidgetDto), divIdOrClass = "#ProjectBusinessInformationWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        //#endregion

        //#region Evaluation Grade Widget 

        /// <summary>
        /// Shows the evaluation grade widget.
        /// </summary>
        /// <param name="projectUid">The project uid.</param>
        /// <returns></returns>
        [HttpGet]
        [AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.CommissionCartoon)]
        public async Task<ActionResult> ShowEvaluationGradeWidget(Guid? attendeeCartoonProjectUid)
        {
            if (this.EditionDto?.IsCartoonProjectEvaluationStarted() != true)
            {
                return Json(new { status = "error", message = Texts.ForbiddenErrorMessage }, JsonRequestBehavior.AllowGet);
            }

            var evaluationDto = await this.attendeeCartoonProjectRepo.FindEvaluationGradeWidgetDtoAsync(attendeeCartoonProjectUid ?? Guid.Empty, this.UserAccessControlDto.User.Id);
            if (evaluationDto == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }


            ViewBag.ApprovedAttendeeCartoonProjectsIds = await this.attendeeCartoonProjectRepo.FindAllApprovedAttendeeCartoonProjectsIdsAsync(this.EditionDto.Edition.Id);

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/EvaluationGradeWidget", evaluationDto), divIdOrClass = "#ProjectEvaluationWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Evaluates the specified cartoon project identifier.
        /// </summary>
        /// <param name="cartoonProjectId">The cartoon project identifier.</param>
        /// <param name="grade">The grade.</param>
        /// <returns></returns>
        /// <exception cref="DomainException"></exception>
        [HttpPost]
        [AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.CommissionCartoon)]
        public async Task<ActionResult> Evaluate(int cartoonProjectId, decimal? grade)
        {
            if (this.EditionDto?.IsCartoonProjectEvaluationOpen() != true)
            {
                return Json(new { status = "error", message = Messages.OutOfEvaluationPeriod }, JsonRequestBehavior.AllowGet);
            }

            var result = new AppValidationResult();

            try
            {
                var cmd = new EvaluateCartoonProject(
                    await this.cartoonProjectRepo.FindByIdAsync(cartoonProjectId),
                    grade);

                cmd.UpdatePreSendProperties(
                    this.UserAccessControlDto.User.Id,
                    this.UserAccessControlDto.User.Uid,
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
                return Json(new
                {
                    status = "error",
                    message = result.Errors.Select(e => e = new AppValidationError(e.Message, "ToastrError", e.Code))?
                                            .FirstOrDefault(e => e.Target == "ToastrError")?.Message ?? ex.GetInnerMessage(),
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return Json(new { status = "error", message = Messages.WeFoundAndError, }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                status = "success",
                projectUid = cartoonProjectId,
                message = string.Format(Messages.EntityActionSuccessfull, Labels.CartoonProject, Labels.Evaluated.ToLowerInvariant())
            });
        }

        //#endregion

        #region Evaluators Widget

        [HttpGet]
        [AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.CommissionCartoon)]
        public async Task<ActionResult> ShowEvaluatorsWidget(Guid? attendeeCartoonProjectUid)
        {
            if (this.EditionDto?.IsCartoonProjectEvaluationStarted() != true)
            {
                return Json(new { status = "error", message = Texts.ForbiddenErrorMessage }, JsonRequestBehavior.AllowGet);
            }

            var evaluationDto = await this.attendeeCartoonProjectRepo.FindEvaluatorsWidgetDtoAsync(attendeeCartoonProjectUid ?? Guid.Empty);
            if (evaluationDto == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }


            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/EvaluatorsWidget", evaluationDto), divIdOrClass = "#ProjectEvaluatorsWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        //#region Founders Widget

        ///// <summary>
        ///// Shows the evaluators widget.
        ///// </summary>
        ///// <param name="attendeeInnovationOrganizationUid">The project uid.</param>
        ///// <returns></returns>
        //[HttpGet]
        //public async Task<ActionResult> ShowFoundersWidget(Guid? attendeeInnovationOrganizationUid)
        //{
        //    var attendeeInnovationOrganizationDto = await this.attendeeInnovationOrganizationRepo.FindFoundersWidgetDtoAsync(attendeeInnovationOrganizationUid ?? Guid.Empty);
        //    if (attendeeInnovationOrganizationDto == null)
        //    {
        //        return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Startup, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
        //    }

        //    #region AttendeeCollaborator and AttendeeInnovationOrganization Tracks validation

        //    var attendeeCollaboratorInnovationOrganizationTrackOptionsUids = await this.GetAttendeeCollaboratorInnovationOrganizationTrackOptionsUids();

        //    if (attendeeInnovationOrganizationDto == null ||
        //        attendeeInnovationOrganizationDto.AttendeeInnovationOrganizationTrackDtos.Any(aiotDto => attendeeCollaboratorInnovationOrganizationTrackOptionsUids.Contains(aiotDto.InnovationOrganizationTrackOption.Uid)) == false)
        //    {
        //        this.StatusMessageToastr(string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()), Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);
        //        return RedirectToAction("EvaluationList", "Projects", new { Area = "Innovation" });
        //    }

        //    #endregion

        //    return Json(new
        //    {
        //        status = "success",
        //        pages = new List<dynamic>
        //        {
        //            new { page = this.RenderRazorViewToString("Widgets/FoundersWidget", attendeeInnovationOrganizationDto), divIdOrClass = "#ProjectsFoundersWidget" },
        //        }
        //    }, JsonRequestBehavior.AllowGet);
        //}

        //#endregion

        //#region Presentation Widget

        ///// <summary>Shows the clipping widget.</summary>
        ///// <param name="projectUid">The project uid.</param>
        ///// <returns></returns>
        //[HttpGet]
        //public async Task<ActionResult> ShowPresentationWidget(Guid? attendeeInnovationOrganizationUid)
        //{
        //    var presentationWidgetDto = await this.attendeeInnovationOrganizationRepo.FindPresentationWidgetDtoAsync(attendeeInnovationOrganizationUid ?? Guid.Empty);
        //    if (presentationWidgetDto == null)
        //    {
        //        return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
        //    }

        //    #region AttendeeCollaborator and AttendeeInnovationOrganization Tracks validation

        //    var attendeeCollaboratorInnovationOrganizationTrackOptionsUids = await this.GetAttendeeCollaboratorInnovationOrganizationTrackOptionsUids();

        //    if (presentationWidgetDto == null ||
        //        presentationWidgetDto.AttendeeInnovationOrganizationTrackDtos.Any(aiotDto => attendeeCollaboratorInnovationOrganizationTrackOptionsUids.Contains(aiotDto.InnovationOrganizationTrackOption.Uid)) == false)
        //    {
        //        this.StatusMessageToastr(string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()), Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);
        //        return RedirectToAction("EvaluationList", "Projects", new { Area = "Innovation" });
        //    }

        //    #endregion

        //    return Json(new
        //    {
        //        status = "success",
        //        pages = new List<dynamic>
        //        {
        //            new { page = this.RenderRazorViewToString("Widgets/PresentationWidget", presentationWidgetDto), divIdOrClass = "#ProjectsPresentationWidget" },
        //        }
        //    }, JsonRequestBehavior.AllowGet);
        //}

        //#endregion

        ///// <summary>
        ///// Gets the attendee collaborator innovation organization track options uids.
        ///// </summary>
        ///// <param name="innovationOrganizationTrackOptionUid">The innovation organization track option uid.</param>
        ///// <returns></returns>
        //private async Task<List<Guid?>> GetAttendeeCollaboratorInnovationOrganizationTrackOptionsUids()
        //{
        //    List<Guid?> innovationOrganizationTrackOptionUids = new List<Guid?>();

        //    var userDto = await this.userRepo.FindUserDtoByUserIdAsync(this.UserAccessControlDto.User.Id);
        //    var attendeeCollaborator = userDto.Collaborator?.GetAttendeeCollaboratorByEditionId(this.EditionDto.Edition.Id);
        //    if (attendeeCollaborator != null)
        //    {
        //        var innovationOrganizationTrackOptions = await this.innovationOrganizationTrackOptionRepo.FindAllByAttendeeCollaboratorIdAsync(attendeeCollaborator.Id);
        //        innovationOrganizationTrackOptionUids = innovationOrganizationTrackOptions.Select(ioto => ioto.Uid as Guid?).ToList();
        //    }
        //    else
        //    {
        //        //Admin dont have Collaborator/AttendeeCollaborator, so, list all InnovationOrganizationTrackOptions in Dropdown.
        //        var innovationOrganizationTrackOptions = await this.innovationOrganizationTrackOptionRepo.FindAllAsync();
        //        innovationOrganizationTrackOptionUids = innovationOrganizationTrackOptions.Select(ioto => ioto.Uid as Guid?).ToList();
        //    }

        //    return innovationOrganizationTrackOptionUids;
        //}

        ///// <summary>
        ///// Gets the search innovation organization track option uids.
        ///// </summary>
        ///// <param name="innovationOrganizationTrackOptionUid">The InnovationOrganizationTrackOptionUid selected in dropdown filter</param>
        ///// <returns></returns>
        //private async Task<List<Guid?>> GetSearchInnovationOrganizationTrackOptionUids(Guid? innovationOrganizationTrackOptionUid)
        //{
        //    var attendeeCollaboratorInnovationOrganizationTrackOptionsUids = await this.GetAttendeeCollaboratorInnovationOrganizationTrackOptionsUids();

        //    List<Guid?> innovationOrganizationTrackOptions = new List<Guid?>();
        //    if (!innovationOrganizationTrackOptionUid.HasValue)
        //    {
        //        //Search by "AttendeeCollaboratorInnovationOrganizationTrackOptions" when have no "InnovationOrganizationTrackOptionUid" selected in dropdown filter
        //        innovationOrganizationTrackOptions.AddRange(attendeeCollaboratorInnovationOrganizationTrackOptionsUids);
        //    }
        //    else
        //    {
        //        //Search by "InnovationOrganizationTrackOptionUid" selected in dropdown filter
        //        innovationOrganizationTrackOptions.Add(innovationOrganizationTrackOptionUid);
        //    }

        //    return innovationOrganizationTrackOptions;
        //}

        //#endregion
    }
}