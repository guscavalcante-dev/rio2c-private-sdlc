// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Renan Valentim
// Created          : 07-28-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 05-01-2025
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
using PlataformaRio2C.Domain.Entities;
using Newtonsoft.Json;
using Microsoft.Ajax.Utilities;
using X.PagedList;

namespace PlataformaRio2C.Web.Site.Areas.Audiovisual.Controllers
{
    /// <summary>ProjectsController</summary>
    [AjaxAuthorize(Order = 1)]
    [AuthorizeCollaboratorType(Order = 2, Types = Constants.CollaboratorType.CommissionAudiovisual)]
    public class ProjectsController : BaseController
    {
        private readonly IProjectRepository projectRepo;
        private readonly IProjectEvaluationStatusRepository evaluationStatusRepo;
        private readonly IUserRepository userRepo;
        private readonly IInterestRepository interestRepo;
        private readonly ITargetAudienceRepository targetAudienceRepo;
        private readonly IAttendeeOrganizationRepository attendeeOrganizationRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectsController"/> class.
        /// </summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="identityController">The identity controller.</param>
        /// <param name="projectRepository">The project repository.</param>
        /// <param name="evaluationStatusRepository">The evaluation status repository.</param>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="interestRepository">The interest repository.</param>
        /// <param name="targetAudienceRepository">The target audience repository.</param>
        /// <param name="attendeeOrganizationRepository">The attendeeOrganizationRepository repository.</param>
        public ProjectsController(
            IMediator commandBus,
            IdentityAutenticationService identityController,
            IProjectRepository projectRepository,
            IProjectEvaluationStatusRepository evaluationStatusRepository,
            IUserRepository userRepository,
            IInterestRepository interestRepository,
            ITargetAudienceRepository targetAudienceRepository,
            IAttendeeOrganizationRepository attendeeOrganizationRepository)
            : base(commandBus, identityController)
        {
            this.projectRepo = projectRepository;
            this.evaluationStatusRepo = evaluationStatusRepository;
            this.userRepo = userRepository;
            this.interestRepo = interestRepository;
            this.targetAudienceRepo = targetAudienceRepository;
            this.attendeeOrganizationRepo = attendeeOrganizationRepository;
        }

        #region List

        /// <summary>Indexes this instance.</summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Index(Guid? subgenreInterestUid, Guid? segmentInterestUid, Guid? evaluationStatusUid)
        {
            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.AudioVisual, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Labels.Projects, Url.Action("Index", "Projects", new { Area = "Audiovisual" }))
            });

            #endregion

            ViewBag.SelectedSubgenreInterestUid = subgenreInterestUid;
            ViewBag.SelectedSegmentInterestUid = segmentInterestUid;
            ViewBag.EvaluationStatusUid = evaluationStatusUid;
            ViewBag.Page = 1;
            ViewBag.PageSize = 10;

            ViewBag.Interests = (await this.interestRepo.FindAllByInterestGroupUidAsync(InterestGroup.AudiovisualGenre.Uid)).GetSeparatorTranslation(m => m.Name, this.UserInterfaceLanguage, '|');
            ViewBag.ProjectEvaluationStatuses = (await this.evaluationStatusRepo.FindAllAsync()).GetSeparatorTranslation(m => m.Name, this.UserInterfaceLanguage, '|');

            return View();
        }

        /// <summary>
        /// Evaluations the list.
        /// </summary>
        /// <param name="searchKeywords">The search keywords.</param>
        /// <param name="subgenreInterestUid">The interest uid.</param>
        /// <param name="segmentInterestUid">The segment interest uid.</param>
        /// <param name="evaluationStatusUid">The evaluation status uid.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        [HttpGet]
        [AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.CommissionAudiovisual)]
        public async Task<ActionResult> CommissionEvaluationList(string searchKeywords, Guid? subgenreInterestUid, Guid? segmentInterestUid, Guid? evaluationStatusUid, int? page = 1, int? pageSize = 12)
        {
            if (this.EditionDto?.IsAudiovisualCommissionProjectEvaluationStarted() != true)
            {
                return RedirectToAction("Index", "Projects", new { Area = "Audiovisual" });
            }

            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.AudiovisualProjects, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Labels.AudioVisual, Url.Action("CommissionEvaluationList", "Projects", new { Area = "Audiovisual" })),
            });

            #endregion

            #region Interests Dropdown

            List<Interest> subgenreInterests = null;
            if (UserAccessControlDto?.User != null && this.EditionDto?.Edition != null)
            {
                var userDto = await this.userRepo.FindUserDtoByUserIdAsync(this.UserAccessControlDto.User.Id);
                var attendeeCollaborator = userDto.Collaborator?.GetAttendeeCollaboratorByEditionId(this.EditionDto.Edition.Id);

                if (attendeeCollaborator != null)
                {
                    subgenreInterests = await this.interestRepo.FindAllByAttendeeCollaboratorIdAsync(attendeeCollaborator.Id, InterestGroup.AudiovisualPitchingSubGenre.Uid);
                }
                else
                {
                    //Admin don't have Collaborator/AttendeeCollaborator, so, get all Interests to list in Dropdown.
                    subgenreInterests = await this.interestRepo.FindAllByInterestGroupUidAsync(InterestGroup.AudiovisualPitchingSubGenre.Uid);
                }
            }
            else
            {
                subgenreInterests = new List<Interest>();
            }

            #endregion

            ViewBag.SearchKeywords = searchKeywords;
            ViewBag.EvaluationStatusUid = evaluationStatusUid;
            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;
            ViewBag.SubgenreInterests = subgenreInterests;
            ViewBag.SelectedSubgenreInterestUid = subgenreInterestUid;
            ViewBag.SegmentInterests = await this.interestRepo.FindAllByInterestGroupUidAsync(InterestGroup.AudiovisualPitchingSegment.Uid);
            ViewBag.SelectedSegmentInterestUid = segmentInterestUid;
            ViewBag.ProjectEvaluationStatuses = await this.evaluationStatusRepo.FindAllAsync();

            return View();
        }

        /// <summary>
        /// Shows the evaluation list widget.
        /// </summary>
        /// <param name="searchKeywords">The search keywords.</param>
        /// <param name="subgenreInterestUid">The subgenre interest uid.</param>
        /// <param name="segmentInterestUid">The segment interest uid.</param>
        /// <param name="evaluationStatusUid">The evaluation status uid.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        [HttpGet]
        [AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.CommissionAudiovisual)]
        public async Task<ActionResult> ShowEvaluationListWidget(string searchKeywords, Guid? subgenreInterestUid, Guid? segmentInterestUid, Guid? evaluationStatusUid, int? page = 1, int? pageSize = 12)
        {
            if (this.EditionDto?.IsAudiovisualCommissionProjectEvaluationStarted() != true)
            {
                return Json(new { status = "error", message = Texts.ForbiddenErrorMessage }, JsonRequestBehavior.AllowGet);
            }

            var attendeeCollaboratorInterestsUids = await this.GetAttendeeCollaboratorInterestsUidsByInterestGroupUid(InterestGroup.AudiovisualPitchingSubGenre.Uid);
            if (attendeeCollaboratorInterestsUids.Count <= 0)
            {
                return Json(new
                {
                    status = "success",
                    pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/EvaluationListWidget", null), divIdOrClass = "#ProjectEvaluationListWidget" },
                }
                }, JsonRequestBehavior.AllowGet);
            }

            var subgenreInterestUids = await this.GetSubgenreInterestUids(subgenreInterestUid);

            var projects = await this.projectRepo.FindAllDtosPagedAsync(
                 this.EditionDto.Id,
                 null, //searchKeywords, //TODO: Re-enable this when we start writing project title into ProjectTitles table instead of writing to the PitchingJsonPayload column
                 subgenreInterestUids,
                 segmentInterestUid,
                 evaluationStatusUid,
                 true,
                 page.Value,
                 pageSize.Value);

            //Convert PitchingJsonPayload to the main object when PitchingJsonPayload is not null.
            foreach (var projectDto in projects)
            {
                if (!String.IsNullOrEmpty(projectDto.Project.PitchingJsonPayload))
                {
                    var pitchingJsonPayload = JsonConvert.DeserializeObject<PitchingJsonPayload>(projectDto.Project.PitchingJsonPayload);
                    if (pitchingJsonPayload != null)
                    {
                        if (projectDto.Project.ProjectTitles.Count > 0)
                            projectDto.Project.ProjectTitles.ForEach(p => p.UpdateValue(pitchingJsonPayload.Title));
                        else
                        {
                            projectDto.Project.ProjectTitles.Add(new ProjectTitle(pitchingJsonPayload.Title, Language.Portuguese, 0));
                            projectDto.Project.ProjectTitles.Add(new ProjectTitle(pitchingJsonPayload.Title, Language.English, 0));

                        }

                        projectDto.SellerAttendeeOrganizationDto = await attendeeOrganizationRepo.FindDtoByAttendeeOrganizationUid(new Guid(pitchingJsonPayload.SellerAttendeeOrganizationId));
                    }
                }
            }

            //TODO: Remove this when we start writing project title into ProjectTitles table instead of writing to the PitchingJsonPayload column
            // We know, this is not a best practice, but we need to do it! :(
            if (!string.IsNullOrEmpty(searchKeywords))
            {
                var filteredProjects = projects
                    .Where(p => p.Project.ProjectTitles.Any(t => t.Value.IndexOf(searchKeywords, StringComparison.OrdinalIgnoreCase) >= 0)
                                || p.SellerAttendeeOrganizationDto.Organization.TradeName.IndexOf(searchKeywords, StringComparison.OrdinalIgnoreCase) >= 0)
                    .ToList();

                projects = filteredProjects.ToPagedList(page.Value, pageSize.Value);
            }

            ViewBag.SearchKeywords = searchKeywords;
            ViewBag.SelectedSubgenreInterestUid = subgenreInterestUid;
            ViewBag.SelectedSegmentInterestUid = segmentInterestUid;
            ViewBag.EvaluationStatusUid = evaluationStatusUid;
            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;
            ViewBag.ApprovedProjectsIds = await this.projectRepo.FindAllApprovedCommissionProjectsIdsAsync(this.EditionDto.Edition.Id);

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/EvaluationListWidget", projects), divIdOrClass = "#ProjectEvaluationListWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Shows the evaluation list item widget.
        /// </summary>
        /// <param name="projectUid">The project uid.</param>
        /// <returns></returns>
        [HttpGet]
        [AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.CommissionAudiovisual)]
        public async Task<ActionResult> ShowEvaluationListItemWidget(Guid? projectUid)
        {
            if (this.EditionDto?.IsAudiovisualCommissionProjectEvaluationStarted() != true)
            {
                return Json(new { status = "error", message = Texts.ForbiddenErrorMessage }, JsonRequestBehavior.AllowGet);
            }

            if (!projectUid.HasValue)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM) }, JsonRequestBehavior.AllowGet);
            }

            var projects = await this.projectRepo.FindDtoToEvaluateAsync(Guid.Empty, projectUid ?? Guid.Empty);

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
        /// <param name="subgenreInterestUid">The interest uid.</param>
        /// <param name="segmentInterestUid">The segment interest uid.</param>
        /// <param name="evaluationStatusUid">The evaluation status uid.</param>
        /// <param name="showPitchings">The show pitchings.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        [AuthorizeCollaboratorType(Types = Constants.CollaboratorType.CommissionAudiovisual)]
        public async Task<ActionResult> EvaluationDetails(int? id, string searchKeywords = null, Guid? subgenreInterestUid = null, Guid? segmentInterestUid = null, Guid? evaluationStatusUid = null, bool showPitchings = true, int? page = 1, int? pageSize = 12)
        {
            if (this.EditionDto?.IsAudiovisualCommissionProjectEvaluationStarted() != true)
            {
                this.StatusMessageToastr(Messages.OutOfEvaluationPeriod, Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);
                return RedirectToAction("Index", "Projects", new { Area = "Audiovisual" });
            }

            var projectDto = await this.projectRepo.FindDtoToEvaluateAsync(id ?? 0);
            var attendeeCollaboratorInterestsUids = await this.GetAttendeeCollaboratorInterestsUidsByInterestGroupUid(InterestGroup.AudiovisualPitchingSubGenre.Uid);

            if (projectDto == null ||
                projectDto.ProjectInterestDtos.Any(dto => attendeeCollaboratorInterestsUids.Contains(dto.Interest.Uid)) == false)
            {
                this.StatusMessageToastr(string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()), Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);
                return RedirectToAction("CommissionEvaluationList", "Projects", new { Area = "Audiovisual" });
            }
            
            var subgenreInterestUids = await this.GetSubgenreInterestUids(subgenreInterestUid);

            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.AudiovisualProjects, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Labels.AudioVisual, Url.Action("CommissionEvaluationList", "Projects", new 
                { 
                    Area = "Audiovisual", 
                    searchKeywords, 
                    subgenreInterestUid, 
                    segmentInterestUid, 
                    evaluationStatusUid, 
                    page, 
                    pageSize 
                })),
                new BreadcrumbItemHelper(projectDto?.GetTitleDtoByLanguageCode(this.UserInterfaceLanguage)?.ProjectTitle?.Value ?? Labels.Project, Url.Action("EvaluationDetails", "Projects", new { Area = "Audiovisual", id }))
            });

            #endregion

            var allProjectsIds = await this.projectRepo.FindAllProjectsIdsPagedAsync(
                this.EditionDto.Edition.Id,
                searchKeywords,
                subgenreInterestUids,
                segmentInterestUid,
                evaluationStatusUid,
                showPitchings,
                new List<Guid?> { },
                page.Value,
                pageSize.Value);
            var currentProjectIdIndex = Array.IndexOf(allProjectsIds, id.Value) + 1; //Index start at 0, its a fix to "start at 1"

            ViewBag.SearchKeywords = searchKeywords;
            ViewBag.SelectedSubgenreInterestUid = subgenreInterestUid;
            ViewBag.SelectedSegmentInterestUid = segmentInterestUid;
            ViewBag.EvaluationStatusUid = evaluationStatusUid;
            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;
            ViewBag.CurrentProjectIndex = currentProjectIdIndex;
            ViewBag.ProjectsTotalCount = allProjectsIds.Length;
            ViewBag.ApprovedProjectsIds = await this.projectRepo.FindAllApprovedCommissionPitchingProjectsIdsAsync(this.EditionDto.Edition.Id);

            return View(projectDto);
        }

        /// <summary>
        /// Previouses the evaluation details.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="searchKeywords">The search keywords.</param>
        /// <param name="subgenreInterestUid">The interest uid.</param>
        /// <param name="segmentInterestUid">The segment interest uid.</param>
        /// <param name="evaluationStatusUid">The evaluation status uid.</param>
        /// <param name="showPitchings">The show pitchings.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        [AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.CommissionAudiovisual)]
        public async Task<ActionResult> PreviousEvaluationDetails(int? id, string searchKeywords = null, Guid? subgenreInterestUid = null, Guid? segmentInterestUid = null, Guid? evaluationStatusUid = null, bool showPitchings = true, int? page = 1, int? pageSize = 12)
        {
            var subgenreInterestUids = await this.GetSubgenreInterestUids(subgenreInterestUid);

            var allProjectsIds = await this.projectRepo.FindAllProjectsIdsPagedAsync(
                this.EditionDto.Edition.Id,
                searchKeywords,
                subgenreInterestUids,
                segmentInterestUid,
                evaluationStatusUid,
                showPitchings,
                new List<Guid?> { },
                page.Value,
                pageSize.Value);

            var currentProjectIdIndex = Array.IndexOf(allProjectsIds, id.Value);
            var previousProjectId = allProjectsIds.ElementAtOrDefault(currentProjectIdIndex - 1);
            if (previousProjectId == 0)
                previousProjectId = id.Value;

            return RedirectToAction("EvaluationDetails",
                new
                {
                    id = previousProjectId,
                    searchKeywords,
                    subgenreInterestUid,
                    segmentInterestUid,
                    evaluationStatusUid,
                    showPitchings,
                    page,
                    pageSize
                });
        }

        /// <summary>
        /// Nexts the evaluation details.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="searchKeywords">The search keywords.</param>
        /// <param name="subgenreInterestUid">The interest uid.</param>
        /// <param name="segmentInterestUid">The segment interest uid.</param>
        /// <param name="evaluationStatusUid">The evaluation status uid.</param>
        /// <param name="showPitchings">The show pitchings.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        [AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.CommissionAudiovisual)]
        public async Task<ActionResult> NextEvaluationDetails(int? id, string searchKeywords = null, Guid? subgenreInterestUid = null, Guid? segmentInterestUid = null, Guid? evaluationStatusUid = null, bool showPitchings = true, int? page = 1, int? pageSize = 12)
        {
            var interestsUids = await this.GetSubgenreInterestUids(subgenreInterestUid);

            var allProjectsIds = await this.projectRepo.FindAllProjectsIdsPagedAsync(
                this.EditionDto.Edition.Id,
                searchKeywords,
                interestsUids,
                segmentInterestUid,
                evaluationStatusUid,
                showPitchings,
                new List<Guid?> { },
                page.Value,
                pageSize.Value);

            var currentProjectIdIndex = Array.IndexOf(allProjectsIds, id.Value);
            var nextProjectId = allProjectsIds.ElementAtOrDefault(currentProjectIdIndex + 1);
            if (nextProjectId == 0)
                nextProjectId = id.Value;

            return RedirectToAction("EvaluationDetails",
                new
                {
                    id = nextProjectId,
                    searchKeywords,
                    subgenreInterestUid,
                    segmentInterestUid,
                    evaluationStatusUid,
                    showPitchings,
                    page,
                    pageSize
                });
        }

        #endregion

        #region Main Information Widget

        /// <summary>
        /// Shows the main information widget.
        /// </summary>
        /// <param name="projectUid">The project uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowMainInformationWidget(Guid? projectUid)
        {
            var mainInformationWidgetDto = await this.projectRepo.FindSiteMainInformationWidgetDtoByProjectUidAsync(projectUid ?? Guid.Empty);
            if (mainInformationWidgetDto == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            #region Attendee Collaborator Interest validation

            var attendeeCollaboratorInterestUids = await this.GetAttendeeCollaboratorInterestsUidsByInterestGroupUid(InterestGroup.AudiovisualPitchingSubGenre.Uid);

            if (mainInformationWidgetDto == null ||
                mainInformationWidgetDto.ProjectInterestDtos?.Any(dto => attendeeCollaboratorInterestUids.Contains(dto.Interest.Uid)) == false)
            {
                this.StatusMessageToastr(string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()), Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);
                return RedirectToAction("CommissionEvaluationList", "Projects", new { Area = "Audiovisual" });
            }

            //Convert PitchingJsonPayload field  to the object PitchingJsonPayload.
            if (!String.IsNullOrEmpty(mainInformationWidgetDto.Project.PitchingJsonPayload))
            {
                var pitchingJsonPayload = JsonConvert.DeserializeObject<PitchingJsonPayload>(mainInformationWidgetDto.Project.PitchingJsonPayload);
                if (pitchingJsonPayload != null)
                    mainInformationWidgetDto.PitchingJsonPayload = pitchingJsonPayload;
            }

            #endregion

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

        #region Interest Widget

        /// <summary>Shows the interest widget.</summary>
        /// <param name="projectUid">The project uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowInterestWidget(Guid? projectUid)
        {
            var interestWidgetDto = await this.projectRepo.FindSiteInterestWidgetDtoByProjectUidAsync(projectUid ?? Guid.Empty);
            if (interestWidgetDto == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            ViewBag.GroupedInterests = await this.interestRepo.FindAllByProjectTypeIdAndGroupedByInterestGroupAsync(ProjectType.AudiovisualBusinessRound.Id);
            ViewBag.TargetAudiences = await this.targetAudienceRepo.FindAllByProjectTypeIdAsync(ProjectType.AudiovisualBusinessRound.Id);

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/InterestWidget", interestWidgetDto), divIdOrClass = "#ProjectInterestWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Links Widget

        /// <summary>Shows the links widget.</summary>
        /// <param name="projectUid">The project uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowLinksWidget(Guid? projectUid)
        {
            var linksWidgetDto = await this.projectRepo.FindSiteLinksWidgetDtoByProjectUidAsync(projectUid ?? Guid.Empty);
            if (linksWidgetDto == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/LinksWidget", linksWidgetDto), divIdOrClass = "#ProjectLinksWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Audiovisual Commission Evaluation Widget 

        /// <summary>
        /// Shows the audiovisual commission evaluation widget.
        /// </summary>
        /// <param name="projectUid">The project uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowAudiovisualCommissionEvaluationWidget(Guid? projectUid)
        {
            var evaluationDto = await this.projectRepo.FindAudiovisualCommissionEvaluationWidgetDtoAsync(projectUid ?? Guid.Empty, this.UserAccessControlDto.User.Id);
            if (evaluationDto == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            ViewBag.ApprovedProjectsIds = await this.projectRepo.FindAllApprovedCommissionProjectsIdsAsync(this.EditionDto.Edition.Id);

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/AudiovisualCommissionEvaluationWidget", evaluationDto), divIdOrClass = "#ProjectEvaluationWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Evaluates the specified music band identifier.
        /// </summary>
        /// <param name="musicBandId">The music band identifier.</param>
        /// <param name="grade">The grade.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> AudiovisualComissionEvaluateProject(int projectId, decimal? grade)
        {
            if (this.EditionDto?.IsAudiovisualCommissionProjectEvaluationOpen() != true)
            {
                return Json(new { status = "error", message = Messages.EvaluationPeriodClosed }, JsonRequestBehavior.AllowGet);
            }

            var result = new AppValidationResult();

            try
            {
                var cmd = new AudiovisualComissionEvaluateProject(
                    await this.projectRepo.FindByIdAsync(projectId),
                    grade);

                cmd.UpdatePreSendProperties(
                  this.UserAccessControlDto.User.Id,
                  this.UserAccessControlDto.User.Uid,
                  this.EditionDto.Id,
                  this.EditionDto.Uid,
                  this.UserInterfaceLanguage,
                  true);
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
                message = string.Format(Messages.EntityActionSuccessfull, Labels.Project, Labels.Evaluated.ToLowerInvariant())
            });
        }

        #endregion

        #region Audiovisual Evaluators Widget

        /// <summary>
        /// Shows the audiovisual commission evaluators widget.
        /// </summary>
        /// <param name="projectUid">The project uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowAudiovisualCommissionEvaluatorsWidget(Guid? projectUid)
        {
            var evaluationDto = await this.projectRepo.FindAudiovisualCommissionEvaluatorsWidgetDtoAsync(projectUid ?? Guid.Empty);
            if (evaluationDto == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/AudiovisualCommissionEvaluatorsWidget", evaluationDto), divIdOrClass = "#ProjectEvaluatorsWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        /// <summary>
        /// Gets the attendee collaborator interests uids by interest group.
        /// </summary>
        /// <param name="interestGroupUid">The interest group uid.</param>
        /// <returns></returns>
        private async Task<List<Guid?>> GetAttendeeCollaboratorInterestsUidsByInterestGroupUid(Guid interestGroupUid)
        {
            List<Guid?> interestsUids = new List<Guid?>();

            var userDto = await this.userRepo.FindUserDtoByUserIdAsync(this.UserAccessControlDto.User.Id);
            var attendeeCollaborator = userDto.Collaborator?.GetAttendeeCollaboratorByEditionId(this.EditionDto.Edition.Id);
            if (attendeeCollaborator != null)
            {
                var interests = await this.interestRepo.FindAllByAttendeeCollaboratorIdAsync(attendeeCollaborator.Id, interestGroupUid);
                interestsUids = interests.Select(i => i.Uid as Guid?).ToList();
            }
            else
            {
                //Admin dont have Collaborator/AttendeeCollaborator, so, list all Interests in Dropdown.
                var interests = await this.interestRepo.FindAllByInterestGroupUidAsync(interestGroupUid);
                interestsUids = interests.Select(i => i.Uid as Guid?).ToList();
            }

            return interestsUids;
        }

        /// <summary>
        /// Gets the search interests uids.
        /// </summary>
        /// <param name="subgenreInterestUid">The interest uid.</param>
        /// <returns></returns>
        private async Task<List<Guid?>> GetSubgenreInterestUids(Guid? subgenreInterestUid)
        {
            var attendeeCollaboratorInterestsUids = await this.GetAttendeeCollaboratorInterestsUidsByInterestGroupUid(InterestGroup.AudiovisualPitchingSubGenre.Uid);

            List<Guid?> subgenreInterestUids = new List<Guid?>();
            if (!subgenreInterestUid.HasValue)
            {
                //Search by "Attendee Collaborator Interests" when have no "InterestUid" selected in dropdown filter
                subgenreInterestUids.AddRange(attendeeCollaboratorInterestsUids);
            }
            else
            {
                //Search by "InterestUid" selected in dropdown filter
                subgenreInterestUids.Add(subgenreInterestUid);
            }

            return subgenreInterestUids;
        }
    }
}