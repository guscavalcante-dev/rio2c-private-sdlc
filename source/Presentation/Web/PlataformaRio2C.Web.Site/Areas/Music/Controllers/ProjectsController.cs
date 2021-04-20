// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 02-26-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-02-2020
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
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.CrossCutting.Identity.AuthorizeAttributes;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using PlataformaRio2C.Infra.CrossCutting.Tools.Helpers;
using PlataformaRio2C.Web.Site.Controllers;
using PlataformaRio2C.Web.Site.Filters;
using Constants = PlataformaRio2C.Domain.Constants;

namespace PlataformaRio2C.Web.Site.Areas.Music.Controllers
{
    /// <summary>ProjectsController</summary>
    [AjaxAuthorize(Order = 1)]
    [AuthorizeCollaboratorType(Order = 2, Types = Constants.CollaboratorType.CommissionMusic)]
    public class ProjectsController : BaseController
    {
        private readonly IMusicProjectRepository musicProjectRepo;
        private readonly IMusicGenreRepository musicGenreRepo;
        private readonly IProjectEvaluationStatusRepository evaluationStatusRepo;
        private readonly IProjectEvaluationRefuseReasonRepository projectEvaluationRefuseReasonRepo;
        private readonly IMusicBandRepository musicBandRepo;

        /// <summary>Initializes a new instance of the <see cref="ProjectsController"/> class.</summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="identityController">The identity controller.</param>
        /// <param name="musicProjectRepository">The music project repository.</param>
        /// <param name="musicGenreRepository">The music genre repository.</param>
        /// <param name="evaluationStatusRepository">The evaluation status repository.</param>
        /// <param name="projectEvaluationRefuseReasonRepository">The project evaluation refuse reason repository.</param>
        public ProjectsController(
            IMediator commandBus,
            IdentityAutenticationService identityController,
            IMusicProjectRepository musicProjectRepository,
            IMusicGenreRepository musicGenreRepository,
            IProjectEvaluationStatusRepository evaluationStatusRepository,
            IProjectEvaluationRefuseReasonRepository projectEvaluationRefuseReasonRepository,
            IMusicBandRepository musicBandRepository
            )
            : base(commandBus, identityController)
        {
            this.musicProjectRepo = musicProjectRepository;
            this.musicGenreRepo = musicGenreRepository;
            this.evaluationStatusRepo = evaluationStatusRepository;
            this.projectEvaluationRefuseReasonRepo = projectEvaluationRefuseReasonRepository;
            this.musicBandRepo = musicBandRepository;
        }

        #region Schedule

        /// <summary>Indexes this instance.</summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.PitchingShow, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Labels.Projects, Url.Action("Index", "Projects", new { Area = "Music" }))
            });

            #endregion

            return View();
        }

        #endregion

        #region Music Commission

        #region Evaluation List

        /// <summary>Evaluations the list.</summary>
        /// <param name="searchKeywords">The search keywords.</param>
        /// <param name="musicGenreUid">The music genre uid.</param>
        /// <param name="evaluationStatusUid">The evaluation status uid.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        [AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.CommissionMusic)]
        [HttpGet]
        public async Task<ActionResult> EvaluationList(string searchKeywords, Guid? musicGenreUid, Guid? evaluationStatusUid, int? page = 1, int? pageSize = 12)
        {
            if (this.EditionDto?.IsMusicProjectEvaluationStarted() != true)
            {
                return RedirectToAction("Index", "Projects", new { Area = "Music" });
            }

            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.PitchingShow, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Labels.ProjectsEvaluation, Url.Action("EvaluationList", "Projects", new { Area = "Music" })),
            });

            #endregion

            ViewBag.SearchKeywords = searchKeywords;
            ViewBag.MusicGenreUid = musicGenreUid;
            ViewBag.EvaluationStatusUid = evaluationStatusUid;
            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;

            ViewBag.MusicGenres = await this.musicGenreRepo.FindAllAsync();
            ViewBag.ProjectEvaluationStatuses = await this.evaluationStatusRepo.FindAllAsync();

            return View();
        }

        /// <summary>Shows the evaluation list widget.</summary>
        /// <param name="searchKeywords">The search keywords.</param>
        /// <param name="musicGenreUid">The music genre uid.</param>
        /// <param name="evaluationStatusUid">The evaluation status uid.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        [AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.CommissionMusic)]
        [HttpGet]
        public async Task<ActionResult> ShowEvaluationListWidget(string searchKeywords, Guid? musicGenreUid, Guid? evaluationStatusUid, int? page = 1, int? pageSize = 12)
        {
            if (this.EditionDto?.IsMusicProjectEvaluationStarted() != true)
            {
                return Json(new { status = "error", message = Texts.ForbiddenErrorMessage }, JsonRequestBehavior.AllowGet);
            }

            var projects = await this.musicProjectRepo.FindAllMusicProjectDtosPagedAsync(
                this.EditionDto.Id,
                searchKeywords,
                musicGenreUid,
                evaluationStatusUid,
                page.Value,
                pageSize.Value);

            ViewBag.SearchKeywords = searchKeywords;
            ViewBag.MusicGenreUid = musicGenreUid;
            ViewBag.EvaluationStatusUid = evaluationStatusUid;
            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;
            ViewBag.ApprovedAttendeeMusicBandsIds = await this.musicProjectRepo.FindAllApprovedAttendeeMusicBandsIdsAsync(this.EditionDto.Edition.Id);

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/EvaluationListWidget", projects), divIdOrClass = "#MusicProjectEvaluationListWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>Shows the evaluation list item widget.</summary>
        /// <param name="projectUid">The project uid.</param>
        /// <returns></returns>
        [AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.CommissionMusic)]
        [HttpGet]
        public async Task<ActionResult> ShowEvaluationListItemWidget(Guid? projectUid)
        {
            if (this.EditionDto?.IsMusicProjectEvaluationStarted() != true)
            {
                return Json(new { status = "error", message = Texts.ForbiddenErrorMessage }, JsonRequestBehavior.AllowGet);
            }

            if (!projectUid.HasValue)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM) }, JsonRequestBehavior.AllowGet);
            }

            var projects = await this.musicProjectRepo.FindDtoToEvaluateAsync(projectUid.Value);

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

        #region Evaluation Details

        /// <summary>
        /// Previouses the evaluation details.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="searchKeywords">The search keywords.</param>
        /// <param name="musicGenreUid">The music genre uid.</param>
        /// <param name="evaluationStatusUid">The evaluation status uid.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        [AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.CommissionMusic)]
        public async Task<ActionResult> PreviousEvaluationDetails(int? id, string searchKeywords = null, Guid? musicGenreUid = null, Guid? evaluationStatusUid = null, int? page = 1, int? pageSize = 12)
        {
            var allMusicProjectsIds = await this.musicProjectRepo.FindAllMusicProjectsIdsPagedAsync(
                this.EditionDto.Edition.Id,
                searchKeywords,
                musicGenreUid,
                evaluationStatusUid,
                page.Value,
                pageSize.Value);

            var currentMusicProjectIdIndex = Array.IndexOf(allMusicProjectsIds, id.Value);
            var previousProjectId = allMusicProjectsIds.ElementAtOrDefault(currentMusicProjectIdIndex - 1);
            if (previousProjectId == 0)
                previousProjectId = id.Value;

            return RedirectToAction("EvaluationDetails", 
                new { 
                    id = previousProjectId, 
                    searchKeywords, 
                    musicGenreUid, 
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
        /// <param name="musicGenreUid">The music genre uid.</param>
        /// <param name="evaluationStatusUid">The evaluation status uid.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        [AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.CommissionMusic)]
        public async Task<ActionResult> NextEvaluationDetails(int? id, string searchKeywords = null, Guid? musicGenreUid = null, Guid? evaluationStatusUid = null, int? page = 1, int? pageSize = 12)
        {
            var allMusicProjectsIds = await this.musicProjectRepo.FindAllMusicProjectsIdsPagedAsync(
                this.EditionDto.Edition.Id,
                searchKeywords,
                musicGenreUid,
                evaluationStatusUid,
                page.Value,
                pageSize.Value);

            var currentMusicProjectIdIndex = Array.IndexOf(allMusicProjectsIds, id.Value);
            var nextProjectId = allMusicProjectsIds.ElementAtOrDefault(currentMusicProjectIdIndex + 1);
            if (nextProjectId == 0)
                nextProjectId = id.Value;

            return RedirectToAction("EvaluationDetails", 
                new { 
                    id = nextProjectId, 
                    searchKeywords, 
                    musicGenreUid, 
                    evaluationStatusUid, 
                    page, 
                    pageSize
                });
        }

        /// <summary>
        /// Evaluations the details.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="step">
        /// This parameter is responsible by next and previous pagination in EvaluationDetails.cshtml.
        /// Pass any value > 0 to step next.
        /// Pass any value < 0 to step previous.
        /// Pass 0 to dont execute any step. (default)</param>
        /// <returns></returns>
        /// [AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.CommissionMusic)]
        public async Task<ActionResult> EvaluationDetails(int? id, string searchKeywords = null, Guid? musicGenreUid = null, Guid? evaluationStatusUid = null, int? page = 1, int? pageSize = 12)
        {
            if (this.EditionDto?.IsMusicProjectEvaluationStarted() != true)
            {
                return RedirectToAction("Index", "Projects", new { Area = "Music" });
            }

            var musicProjectDto = await this.musicProjectRepo.FindDtoToEvaluateAsync(id ?? 0);
            if (musicProjectDto == null)
            {
                this.StatusMessageToastr(string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()), Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);
                return RedirectToAction("EvaluationList", "Projects", new { Area = "Music" });
            }

            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.PitchingShow, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Labels.Projects, Url.Action("EvaluationList", "Projects", new { Area = "Music" })),
                new BreadcrumbItemHelper(musicProjectDto.AttendeeMusicBandDto?.MusicBand?.Name ?? Labels.Project, Url.Action("EvaluationDetails", "Projects", new { Area = "Music", id }))
            });

            #endregion

            var allMusicProjectsIds = await this.musicProjectRepo.FindAllMusicProjectsIdsPagedAsync(
                this.EditionDto.Edition.Id,
                searchKeywords,
                musicGenreUid,
                evaluationStatusUid,
                page.Value,
                pageSize.Value);
            var currentMusicProjectIdIndex = Array.IndexOf(allMusicProjectsIds, id.Value) + 1; //Index start at 0, its a fix to "start at 1"

            ViewBag.SearchKeywords = searchKeywords;
            ViewBag.MusicGenreUid = musicGenreUid;
            ViewBag.EvaluationStatusUid = evaluationStatusUid;
            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;
            ViewBag.CurrentMusicProjectIndex = currentMusicProjectIdIndex;

            ViewBag.MusicProjectsTotalCount = await this.musicProjectRepo.CountPagedAsync(this.EditionDto.Edition.Id, searchKeywords, musicGenreUid, evaluationStatusUid, page.Value, pageSize.Value);
            ViewBag.ApprovedAttendeeMusicBandsIds = await this.musicProjectRepo.FindAllApprovedAttendeeMusicBandsIdsAsync(this.EditionDto.Edition.Id);

            return View(musicProjectDto);
        }

        #endregion

        #region Main Information Widget

        /// <summary>Shows the main information widget.</summary>
        /// <param name="projectUid">The project uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowMainInformationWidget(Guid? projectUid)
        {
            var mainInformationWidgetDto = await this.musicProjectRepo.FindMainInformationWidgetDtoAsync(projectUid ?? Guid.Empty);
            if (mainInformationWidgetDto == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            ViewBag.ApprovedAttendeeMusicBandsIds = await this.musicProjectRepo.FindAllApprovedAttendeeMusicBandsIdsAsync(this.EditionDto.Edition.Id);

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

        #region Members Widget

        /// <summary>Shows the members widget.</summary>
        /// <param name="projectUid">The project uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowMembersWidget(Guid? projectUid)
        {
            var membersWidgetDto = await this.musicProjectRepo.FindMembersWidgetDtoAsync(projectUid ?? Guid.Empty);
            if (membersWidgetDto == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/MembersWidget", membersWidgetDto), divIdOrClass = "#ProjectMembersWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Team Members Widget

        /// <summary>Shows the team members widget.</summary>
        /// <param name="projectUid">The project uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowTeamMembersWidget(Guid? projectUid)
        {
            var teamMembersWidgetDto = await this.musicProjectRepo.FindTeamMembersWidgetDtoAsync(projectUid ?? Guid.Empty);
            if (teamMembersWidgetDto == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/TeamMembersWidget", teamMembersWidgetDto), divIdOrClass = "#ProjectTeamMembersWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Released Music Projects Widget

        /// <summary>Shows the released projects widget.</summary>
        /// <param name="projectUid">The project uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowReleasedProjectsWidget(Guid? projectUid)
        {
            var releasedProjectsWidgetDto = await this.musicProjectRepo.FindReleasedProjectsWidgetDtoAsync(projectUid ?? Guid.Empty);
            if (releasedProjectsWidgetDto == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/ReleasedProjectsWidget", releasedProjectsWidgetDto), divIdOrClass = "#ReleasedProjectsWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Responsible Widget

        /// <summary>Shows the responsible widget.</summary>
        /// <param name="projectUid">The project uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowResponsibleWidget(Guid? projectUid)
        {
            var projectResponsibleWidgetDto = await this.musicProjectRepo.FindProjectResponsibleWidgetDtoAsync(projectUid ?? Guid.Empty);
            if (projectResponsibleWidgetDto == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/ResponsibleWidget", projectResponsibleWidgetDto), divIdOrClass = "#ProjectResponsibleWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Video and Music Widget

        /// <summary>Shows the video and music widget.</summary>
        /// <param name="projectUid">The project uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowVideoAndMusicWidget(Guid? projectUid)
        {
            var videoAndMusicWidgetDto = await this.musicProjectRepo.FindVideoAndMusicWidgetDtoAsync(projectUid ?? Guid.Empty);
            if (videoAndMusicWidgetDto == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/VideoAndMusicWidget", videoAndMusicWidgetDto), divIdOrClass = "#ProjectVideoAndMusicWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Cilpping Widget

        /// <summary>Shows the clipping widget.</summary>
        /// <param name="projectUid">The project uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowClippingWidget(Guid? projectUid)
        {
            var clippingWidgetDto = await this.musicProjectRepo.FindClippingWidgetDtoAsync(projectUid ?? Guid.Empty);
            if (clippingWidgetDto == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/ClippingWidget", clippingWidgetDto), divIdOrClass = "#ProjectClippingWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Social Networks Widget

        /// <summary>Shows the social networks widget.</summary>
        /// <param name="projectUid">The project uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowSocialNetworksWidget(Guid? projectUid)
        {
            var socialNetworksWidget = await this.musicProjectRepo.FindSocialNetworksWidgetDtoAsync(projectUid ?? Guid.Empty);
            if (socialNetworksWidget == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/SocialNetworksWidget", socialNetworksWidget), divIdOrClass = "#ProjectSocialNetworksWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Evaluation Widget Old (Disabled 01/04/2021)

        ///// <summary>Shows the evaluation widget.</summary>
        ///// <param name="projectUid">The project uid.</param>
        ///// <returns></returns>
        //[Obsolete(@"Please, use the 'ShowEvaluationGradeWidget'")]
        //[HttpGet]
        //public async Task<ActionResult> ShowEvaluationWidget(Guid? projectUid)
        //{
        //    if (this.EditionDto?.IsMusicProjectEvaluationStarted() != true)
        //    {
        //        return Json(new { status = "error", message = Texts.ForbiddenErrorMessage }, JsonRequestBehavior.AllowGet);
        //    }

        //    var evaluationDto = await this.musicProjectRepo.FindEvaluationWidgetDtoAsync(projectUid ?? Guid.Empty);
        //    if (evaluationDto == null)
        //    {
        //        return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
        //    }

        //    return Json(new
        //    {
        //        status = "success",
        //        pages = new List<dynamic>
        //        {
        //            new { page = this.RenderRazorViewToString("Widgets/EvaluationWidget", evaluationDto), divIdOrClass = "#ProjectEvaluationWidget" },
        //        }
        //    }, JsonRequestBehavior.AllowGet);
        //}

        //#region Accept (Obsolete 01/04/2021)

        ///// <summary>Shows the accept evaluation modal.</summary>
        ///// <param name="projectUid">The project uid.</param>
        ///// <returns></returns>
        //[Obsolete(@"Please, use the 'ShowEvaluationGradeWidget' method.")]
        //[HttpGet]
        //[AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.CommissionMusic)]
        //public async Task<ActionResult> ShowAcceptEvaluationModal(Guid? projectUid)
        //{
        //    AcceptMusicProjectEvaluation cmd;

        //    try
        //    {
        //        if (this.EditionDto?.IsMusicProjectEvaluationOpen() != true)
        //        {
        //            throw new DomainException(Texts.ForbiddenErrorMessage);
        //        }

        //        var projectDto = await this.musicProjectRepo.FindDtoToEvaluateAsync(projectUid ?? Guid.Empty);
        //        if (projectDto == null)
        //        {
        //            throw new DomainException(string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()));
        //        }

        //        cmd = new AcceptMusicProjectEvaluation(projectDto);
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
        //            new { page = this.RenderRazorViewToString("Modals/AcceptEvaluationModal", cmd), divIdOrClass = "#GlobalModalContainer" },
        //        }
        //    }, JsonRequestBehavior.AllowGet);
        //}

        ///// <summary>Accepts the specified project evaluation.</summary>
        ///// <param name="cmd">The command.</param>
        ///// <returns></returns>
        //[Obsolete(@"Please, use the 'Evaluate' method.")]
        //[HttpPost]
        //[AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.CommissionMusic)]
        //public async Task<ActionResult> Accept(AcceptMusicProjectEvaluation cmd)
        //{
        //    var result = new AppValidationResult();

        //    try
        //    {
        //        if (this.EditionDto?.IsMusicProjectEvaluationOpen() != true)
        //        {
        //            throw new DomainException(Texts.ForbiddenErrorMessage);
        //        }

        //        if (!ModelState.IsValid)
        //        {
        //            throw new DomainException(Messages.CorrectFormValues);
        //        }

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
        //        foreach (var error in result.Errors)
        //        {
        //            var target = error.Target ?? "";
        //            ModelState.AddModelError(target, error.Message);
        //        }
        //        var toastrError = result.Errors?.FirstOrDefault(e => e.Target == "ToastrError");

        //        cmd.UpdateModelsAndLists(await this.musicProjectRepo.FindDtoToEvaluateAsync(cmd.ProjectUid ?? Guid.Empty));

        //        return Json(new
        //        {
        //            status = "error",
        //            message = toastrError?.Message ?? ex.GetInnerMessage(),
        //            pages = new List<dynamic>
        //            {
        //                new { page = this.RenderRazorViewToString("Modals/AcceptEvaluationForm", cmd), divIdOrClass = "#form-container" },
        //            }
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
        //        projectUid = cmd.ProjectUid,
        //        message = string.Format(Messages.EntityActionSuccessfull, Labels.Project, Labels.ProjectAccepted.ToLowerInvariant())
        //    });
        //}

        //#endregion

        //#region Refuse (Obsolete 01/04/2021)

        ///// <summary>Shows the refuse evaluation modal.</summary>
        ///// <param name="projectUid">The project uid.</param>
        ///// <returns></returns>
        //[Obsolete("Please, use the 'ShowEvaluationGradeWidget' endpoint.")]
        //[HttpGet]
        //[AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.CommissionMusic)]
        //public async Task<ActionResult> ShowRefuseEvaluationModal(Guid? projectUid)
        //{
        //    RefuseMusicProjectEvaluation cmd;

        //    try
        //    {
        //        if (this.EditionDto?.IsMusicProjectEvaluationOpen() != true)
        //        {
        //            throw new DomainException(Texts.ForbiddenErrorMessage);
        //        }

        //        var projectDto = await this.musicProjectRepo.FindDtoToEvaluateAsync(projectUid ?? Guid.Empty);
        //        if (projectDto == null)
        //        {
        //            throw new DomainException(string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()));
        //        }

        //        cmd = new RefuseMusicProjectEvaluation(
        //            projectDto,
        //            await this.projectEvaluationRefuseReasonRepo.FindAllByProjectTypeUidAsync(ProjectType.Music.Uid));
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
        //            new { page = this.RenderRazorViewToString("Modals/RefuseEvaluationModal", cmd), divIdOrClass = "#GlobalModalContainer" },
        //        }
        //    }, JsonRequestBehavior.AllowGet);
        //}

        ///// <summary>Refuses the specified music project evaluation.</summary>
        ///// <param name="cmd">The command</param>
        ///// <returns></returns>
        //[Obsolete(@"Please, use the 'Evaluate' method.")]
        //[HttpPost]
        //[AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.CommissionMusic)]
        //public async Task<ActionResult> Refuse(RefuseMusicProjectEvaluation cmd)
        //{
        //    var result = new AppValidationResult();

        //    try
        //    {
        //        if (this.EditionDto?.IsMusicProjectEvaluationOpen() != true)
        //        {
        //            throw new DomainException(Texts.ForbiddenErrorMessage);
        //        }

        //        if (!ModelState.IsValid)
        //        {
        //            throw new DomainException(Messages.CorrectFormValues);
        //        }

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
        //        foreach (var error in result.Errors)
        //        {
        //            var target = error.Target ?? "";
        //            ModelState.AddModelError(target, error.Message);
        //        }
        //        var toastrError = result.Errors?.FirstOrDefault(e => e.Target == "ToastrError");

        //        cmd.UpdateModelsAndLists(
        //            await this.musicProjectRepo.FindDtoToEvaluateAsync(cmd.ProjectUid ?? Guid.Empty),
        //            await this.projectEvaluationRefuseReasonRepo.FindAllByProjectTypeUidAsync(ProjectType.Music.Uid));

        //        return Json(new
        //        {
        //            status = "error",
        //            message = toastrError?.Message ?? ex.GetInnerMessage(),
        //            pages = new List<dynamic>
        //            {
        //                new { page = this.RenderRazorViewToString("Modals/RefuseEvaluationForm", cmd), divIdOrClass = "#form-container" },
        //            }
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
        //        projectUid = cmd.ProjectUid,
        //        message = string.Format(Messages.EntityActionSuccessfull, Labels.Project, Labels.ProjectRefused.ToLowerInvariant())
        //    });
        //}

        //#endregion

        #endregion

        #region Evaluation Grade Widget 

        /// <summary>
        /// Shows the evaluation grade widget.
        /// </summary>
        /// <param name="projectUid">The project uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowEvaluationGradeWidget(Guid? projectUid)
        {
            if (this.EditionDto?.IsMusicProjectEvaluationStarted() != true)
            {
                return Json(new { status = "error", message = Texts.ForbiddenErrorMessage }, JsonRequestBehavior.AllowGet);
            }

            var evaluationDto = await this.musicProjectRepo.FindEvaluationGradeWidgetDtoAsync(projectUid ?? Guid.Empty, this.UserAccessControlDto.User.Id);
            if (evaluationDto == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            ViewBag.ApprovedAttendeeMusicBandsIds = await this.musicProjectRepo.FindAllApprovedAttendeeMusicBandsIdsAsync(this.EditionDto.Edition.Id);

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/EvaluationGradeWidget", evaluationDto), divIdOrClass = "#ProjectEvaluationWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>Accepts the specified project evaluation.</summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.CommissionMusic)]
        public async Task<ActionResult> Evaluate(int musicBandId, decimal? grade)
        {
            var result = new AppValidationResult();

            try
            {
                if (this.EditionDto?.IsMusicProjectEvaluationOpen() != true)
                {
                    throw new DomainException(Texts.ForbiddenErrorMessage);
                }

                var cmd = new EvaluateMusicBand(
                    await this.musicBandRepo.FindByIdAsync(musicBandId),
                    grade.Value);

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
                //projectUid = cmd.MusicBandId,
                message = string.Format(Messages.EntityActionSuccessfull, Labels.MusicBand, Labels.Evaluated.ToLowerInvariant())
            });
        }

        #endregion

        #region Evaluators Widget

        [HttpGet]
        public async Task<ActionResult> ShowEvaluatorsWidget(Guid? projectUid)
        {
            if (this.EditionDto?.IsMusicProjectEvaluationStarted() != true)
            {
                return Json(new { status = "error", message = Texts.ForbiddenErrorMessage }, JsonRequestBehavior.AllowGet);
            }

            var evaluationDto = await this.musicProjectRepo.FindEvaluatorsWidgetDtoAsync(projectUid ?? Guid.Empty);
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

        #endregion
    }
}