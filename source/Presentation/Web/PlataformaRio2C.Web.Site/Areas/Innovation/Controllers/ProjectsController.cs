﻿// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Renan Valentim
// Created          : 07-28-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 01-27-2023
// ***********************************************************************
// <copyright file="ProjectsController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using PlataformaRio2C.Application;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Application.ViewModels;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.CrossCutting.Identity.AuthorizeAttributes;
using PlataformaRio2C.Infra.CrossCutting.Identity.Service;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using PlataformaRio2C.Infra.CrossCutting.Tools.Helpers;
using PlataformaRio2C.Web.Site.Controllers;
using PlataformaRio2C.Web.Site.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using X.PagedList;
using Constants = PlataformaRio2C.Domain.Constants;

namespace PlataformaRio2C.Web.Site.Areas.Innovation.Controllers
{
    /// <summary>ProjectsController</summary>
    [AjaxAuthorize(Order = 1)]
    [AuthorizeCollaboratorType(Order = 2, Types = Constants.CollaboratorType.CommissionInnovation)]
    public class ProjectsController : BaseController
    {
        private readonly IInnovationOrganizationRepository innovationOrganizationRepo;
        private readonly IAttendeeInnovationOrganizationRepository attendeeInnovationOrganizationRepo;
        private readonly IProjectEvaluationStatusRepository evaluationStatusRepo;
        private readonly IInnovationOrganizationTrackOptionRepository innovationOrganizationTrackOptionRepo;
        private readonly IInnovationOrganizationTrackOptionGroupRepository innovationOrganizationTrackOptionGroupRepo;
        private readonly IInnovationOrganizationObjectivesOptionRepository innovationOrganizationObjectivesOptionRepo;
        private readonly IInnovationOrganizationTechnologyOptionRepository innovationOrganizationTechnologyOptionRepo;
        private readonly IInnovationOrganizationExperienceOptionRepository innovationOrganizationExperienceOptionRepo;
        private readonly IUserRepository userRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectsController" /> class.
        /// </summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="identityController">The identity controller.</param>
        /// <param name="innovationOrganizationRepository">The innovation organization repository.</param>
        /// <param name="attendeeInnovationOrganizationRepository">The attendee innovation organization repository.</param>
        /// <param name="evaluationStatusRepository">The evaluation status repository.</param>
        /// <param name="innovationOrganizationTrackOptionRepository">The innovation organization track option repository.</param>
        /// <param name="innovationOrganizationTrackOptionGroupRepository">The innovation organization track option group repository.</param>
        /// <param name="innovationOrganizationObjectivesOptionRepository">The innovation organization objectives option repository.</param>
        /// <param name="innovationOrganizationTechnologyOptionRepository">The innovation organization technology option repository.</param>
        /// <param name="innovationOrganizationExperienceOptionRepository">The innovation organization experience option repository.</param>
        /// <param name="userRepository">The user repository.</param>
        public ProjectsController(
            IMediator commandBus,
            IdentityAutenticationService identityController,
            IInnovationOrganizationRepository innovationOrganizationRepository,
            IAttendeeInnovationOrganizationRepository attendeeInnovationOrganizationRepository,
            IProjectEvaluationStatusRepository evaluationStatusRepository,
            IInnovationOrganizationTrackOptionRepository innovationOrganizationTrackOptionRepository,
            IInnovationOrganizationTrackOptionGroupRepository innovationOrganizationTrackOptionGroupRepository,
            IInnovationOrganizationObjectivesOptionRepository innovationOrganizationObjectivesOptionRepository,
            IInnovationOrganizationTechnologyOptionRepository innovationOrganizationTechnologyOptionRepository,
            IInnovationOrganizationExperienceOptionRepository innovationOrganizationExperienceOptionRepository,
            IUserRepository userRepository
            )
            : base(commandBus, identityController)
        {
            this.innovationOrganizationRepo = innovationOrganizationRepository;
            this.attendeeInnovationOrganizationRepo = attendeeInnovationOrganizationRepository;
            this.evaluationStatusRepo = evaluationStatusRepository;
            this.innovationOrganizationTrackOptionRepo = innovationOrganizationTrackOptionRepository;
            this.innovationOrganizationTrackOptionGroupRepo = innovationOrganizationTrackOptionGroupRepository;
            this.innovationOrganizationObjectivesOptionRepo = innovationOrganizationObjectivesOptionRepository;
            this.innovationOrganizationTechnologyOptionRepo = innovationOrganizationTechnologyOptionRepository;
            this.innovationOrganizationExperienceOptionRepo = innovationOrganizationExperienceOptionRepository;
            this.userRepo = userRepository;
        }

        #region List

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <param name="searchViewModel">The search view model.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Index(InnovationProjectSearchViewModel searchViewModel)
        {
            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.Startups, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Labels.Projects, Url.Action("Index", "Projects", new { Area = "Innovation" }))
            });

            #endregion

            searchViewModel.UpdateModelsAndLists(
               await this.innovationOrganizationTrackOptionGroupRepo.FindAllAsync(),
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
        [AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.CommissionInnovation)]
        public async Task<ActionResult> EvaluationList(InnovationProjectSearchViewModel searchViewModel)
        {
            if (this.EditionDto?.IsInnovationProjectEvaluationStarted() != true)
            {
                return RedirectToAction("Index", "Projects", new { Area = "Innovation" });
            }

            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.StartupsProjects, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Labels.Startups, Url.Action("EvaluationList", "Projects", new { Area = "Innovation" })),
            });

            #endregion

            #region Innovation Organization Track Options Dropdown

            List<InnovationOrganizationTrackOptionGroup> innovationOrganizationTrackOptionGroups = null;
            if (UserAccessControlDto?.User != null && this.EditionDto?.Edition != null)
            {
                var userDto = await this.userRepo.FindUserDtoByUserIdAsync(this.UserAccessControlDto.User.Id);
                var attendeeCollaborator = userDto.Collaborator?.GetAttendeeCollaboratorByEditionId(this.EditionDto.Edition.Id);

                if (attendeeCollaborator != null)
                {
                    innovationOrganizationTrackOptionGroups = await this.innovationOrganizationTrackOptionGroupRepo.FindAllByAttendeeCollaboratorIdAsync(attendeeCollaborator.Id);
                }
                else
                {
                    //Admin don't have Collaborator/AttendeeCollaborator, so, get all options to list in Dropdown.
                    innovationOrganizationTrackOptionGroups = await this.innovationOrganizationTrackOptionGroupRepo.FindAllAsync();
                }
            }
            else
            {
                innovationOrganizationTrackOptionGroups = new List<InnovationOrganizationTrackOptionGroup>();
            }

            #endregion

            searchViewModel.UpdateModelsAndLists(
               innovationOrganizationTrackOptionGroups,
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
        [AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.CommissionInnovation)]
        public async Task<ActionResult> ShowEvaluationListWidget(InnovationProjectSearchViewModel searchViewModel)
        {
            if (this.EditionDto?.IsInnovationProjectEvaluationStarted() != true)
            {
                return Json(new { status = "error", message = Texts.ForbiddenErrorMessage }, JsonRequestBehavior.AllowGet);
            }

            var attendeeInnovationOrganizationTrackOptionUids = await this.GetAttendeeCollaboratorInnovationOrganizationTrackOptionsGroupUids();
            if (attendeeInnovationOrganizationTrackOptionUids.Count <= 0)
            {
                return Json(new
                {
                    status = "success",
                    pages = new List<dynamic>
                    {
                        new { page = this.RenderRazorViewToString("Widgets/EvaluationListWidget", new List<AttendeeInnovationOrganizationDto>()), divIdOrClass = "#InnovationProjectEvaluationListWidget" },
                    }
                }, JsonRequestBehavior.AllowGet);
            }

            var innovationOrganizationTrackOptionGroupUids = await this.GetSearchInnovationOrganizationTrackOptionGroupUids(searchViewModel.InnovationOrganizationTrackOptionGroupUid);

            var projects = await this.attendeeInnovationOrganizationRepo.FindAllDtosPagedAsync(
                this.EditionDto.Id,
                searchViewModel.Search,
                innovationOrganizationTrackOptionGroupUids,
                searchViewModel.EvaluationStatusUid,
                searchViewModel.ShowBusinessRounds,
                searchViewModel.Page.Value,
                searchViewModel.PageSize.Value);

            ViewBag.InnovationProjectSearchViewModel = searchViewModel;
            ViewBag.ApprovedAttendeeInnovationOrganizationsIds = await this.attendeeInnovationOrganizationRepo.FindAllApprovedAttendeeInnovationOrganizationsIdsAsync(this.EditionDto.Edition.Id);

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/EvaluationListWidget", projects), divIdOrClass = "#InnovationProjectEvaluationListWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Shows the evaluation list item widget.
        /// </summary>
        /// <param name="projectUid">The project uid.</param>
        /// <returns></returns>
        [HttpGet]
        [AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.CommissionInnovation)]
        public async Task<ActionResult> ShowEvaluationListItemWidget(Guid? projectUid)
        {
            if (this.EditionDto?.IsInnovationProjectEvaluationStarted() != true)
            {
                return Json(new { status = "error", message = Texts.ForbiddenErrorMessage }, JsonRequestBehavior.AllowGet);
            }

            if (!projectUid.HasValue)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM) }, JsonRequestBehavior.AllowGet);
            }

            var projects = await this.attendeeInnovationOrganizationRepo.FindDtoToEvaluateAsync(projectUid.Value);

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
        /// <param name="searchViewModel">The search view model.</param>
        /// <returns></returns>
        [AuthorizeCollaboratorType(Types = Constants.CollaboratorType.CommissionInnovation)]
        public async Task<ActionResult> EvaluationDetails(InnovationProjectSearchViewModel searchViewModel)
        {
            if (this.EditionDto?.IsInnovationProjectEvaluationStarted() != true)
            {
                this.StatusMessageToastr(Messages.OutOfEvaluationPeriod, Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);
                return RedirectToAction("Index", "Projects", new { Area = "Innovation" });
            }

            var attendeeInnovationOrganizationDto = await this.attendeeInnovationOrganizationRepo.FindDtoToEvaluateAsync(searchViewModel.Id ?? 0);
            var attendeeCollaboratorInnovationOrganizationTrackOptionsGroupUids = await this.GetAttendeeCollaboratorInnovationOrganizationTrackOptionsGroupUids();

            if (attendeeInnovationOrganizationDto == null ||
                attendeeInnovationOrganizationDto.AttendeeInnovationOrganizationTrackDtos.Any(aiotDto => attendeeCollaboratorInnovationOrganizationTrackOptionsGroupUids.Contains(aiotDto.InnovationOrganizationTrackOptionGroup.Uid)) == false)
            {
                this.StatusMessageToastr(string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()), Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);
                return RedirectToAction("EvaluationList", "Projects", new { Area = "Innovation" });
            }

            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.StartupsProjects, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Labels.Startups, Url.Action("EvaluationList", "Projects", new { Area = "Innovation", searchViewModel.Search, searchViewModel.InnovationOrganizationTrackOptionGroupUid, searchViewModel.EvaluationStatusUid, searchViewModel.Page, searchViewModel.PageSize })),
                new BreadcrumbItemHelper(attendeeInnovationOrganizationDto?.InnovationOrganization?.Name ?? Labels.Project, Url.Action("EvaluationDetails", "Projects", new { Area = "Innovation", searchViewModel.Id }))
            });

            #endregion

            var innovationOrganizationTrackOptionGroupsUids = await this.GetSearchInnovationOrganizationTrackOptionGroupUids(searchViewModel.InnovationOrganizationTrackOptionGroupUid);

            var allInnovationOrganizationsIds = await this.attendeeInnovationOrganizationRepo.FindAllInnovationOrganizationsIdsPagedAsync(
                this.EditionDto.Edition.Id,
                searchViewModel.Search,
                innovationOrganizationTrackOptionGroupsUids,
                searchViewModel.EvaluationStatusUid,
                searchViewModel.ShowBusinessRounds,
                searchViewModel.Page.Value,
                searchViewModel.PageSize.Value);

            var currentInnovationProjectIdIndex = Array.IndexOf(allInnovationOrganizationsIds, searchViewModel.Id.Value) + 1; //Index start at 0, its a fix to "start at 1"

            ViewBag.InnovationProjectSearchViewModel = searchViewModel;
            ViewBag.CurrentInnovationProjectIndex = currentInnovationProjectIdIndex;
            ViewBag.ApprovedAttendeeInnovationOrganizationsIds = await this.attendeeInnovationOrganizationRepo.FindAllApprovedAttendeeInnovationOrganizationsIdsAsync(this.EditionDto.Edition.Id);
            ViewBag.InnovationOrganizationsTotalCount = await this.attendeeInnovationOrganizationRepo.CountPagedAsync(
                this.EditionDto.Edition.Id,
                searchViewModel.Search,
                innovationOrganizationTrackOptionGroupsUids,
                searchViewModel.EvaluationStatusUid,
                searchViewModel.ShowBusinessRounds,
                searchViewModel.Page.Value,
                searchViewModel.PageSize.Value);

            return View(attendeeInnovationOrganizationDto);
        }

        /// <summary>
        /// Previouses the evaluation details.
        /// </summary>
        /// <param name="searchViewModel">The search view model.</param>
        /// <returns></returns>
        [AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.CommissionInnovation)]
        public async Task<ActionResult> PreviousEvaluationDetails(InnovationProjectSearchViewModel searchViewModel)
        {
            var innovationOrganizationTrackOptionGroupsUids = await this.GetSearchInnovationOrganizationTrackOptionGroupUids(searchViewModel.InnovationOrganizationTrackOptionGroupUid);

            var allInnovationOrganizationsIds = await this.attendeeInnovationOrganizationRepo.FindAllInnovationOrganizationsIdsPagedAsync(
                this.EditionDto.Edition.Id,
                searchViewModel.Search,
                innovationOrganizationTrackOptionGroupsUids,
                searchViewModel.EvaluationStatusUid,
                searchViewModel.ShowBusinessRounds,
                searchViewModel.Page.Value,
                searchViewModel.PageSize.Value);

            var currentInnovationProjectIdIndex = Array.IndexOf(allInnovationOrganizationsIds, searchViewModel.Id.Value);
            var previousProjectId = allInnovationOrganizationsIds.ElementAtOrDefault(currentInnovationProjectIdIndex - 1);
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
        [AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.CommissionInnovation)]
        public async Task<ActionResult> NextEvaluationDetails(InnovationProjectSearchViewModel searchViewModel)
        {
            var innovationOrganizationTrackOptionsUids = await this.GetSearchInnovationOrganizationTrackOptionGroupUids(searchViewModel.InnovationOrganizationTrackOptionGroupUid);

            var allInnovationOrganizationsIds = await this.attendeeInnovationOrganizationRepo.FindAllInnovationOrganizationsIdsPagedAsync(
                this.EditionDto.Edition.Id,
                searchViewModel.Search,
                innovationOrganizationTrackOptionsUids,
                searchViewModel.EvaluationStatusUid,
                searchViewModel.ShowBusinessRounds,
                searchViewModel.Page.Value,
                searchViewModel.PageSize.Value);

            var currentInnovationProjectIdIndex = Array.IndexOf(allInnovationOrganizationsIds, searchViewModel.Id.Value);
            var nextProjectId = allInnovationOrganizationsIds.ElementAtOrDefault(currentInnovationProjectIdIndex + 1);
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
        /// <param name="attendeeInnovationOrganizationUid">The attendee innovation organization uid.</param>
        /// <returns></returns>
        [HttpGet]
        [AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.CommissionInnovation)]
        public async Task<ActionResult> ShowMainInformationWidget(Guid? attendeeInnovationOrganizationUid)
        {
            var attendeeInnovationOrganizationDto = await this.attendeeInnovationOrganizationRepo.FindMainInformationWidgetDtoAsync(attendeeInnovationOrganizationUid ?? Guid.Empty);
            if (attendeeInnovationOrganizationDto == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Startup, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            #region AttendeeCollaborator and AttendeeInnovationOrganization Tracks validation

            var attendeeCollaboratorInnovationOrganizationTrackOptionGroupsUids = await this.GetAttendeeCollaboratorInnovationOrganizationTrackOptionsGroupUids();
            if (attendeeInnovationOrganizationDto.AttendeeInnovationOrganizationTrackDtos.Any(aiotDto => attendeeCollaboratorInnovationOrganizationTrackOptionGroupsUids.Contains(aiotDto.InnovationOrganizationTrackOptionGroup?.Uid)) == false)
            {
                this.StatusMessageToastr(string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()), Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);
                return RedirectToAction("EvaluationList", "Projects", new { Area = "Innovation" });
            }

            #endregion

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/MainInformationWidget", attendeeInnovationOrganizationDto), divIdOrClass = "#ProjectMainInformationWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Business Information Widget

        /// <summary>
        /// Shows the main information widget.
        /// </summary>
        /// <param name="attendeeInnovationOrganizationUid">The attendee innovation organization uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowBusinessInformationWidget(Guid? attendeeInnovationOrganizationUid)
        {
            var attendeeInnovationOrganizationDto = await this.attendeeInnovationOrganizationRepo.FindBusinessInformationWidgetDtoAsync(attendeeInnovationOrganizationUid ?? Guid.Empty);
            if (attendeeInnovationOrganizationDto == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Startup, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            #region AttendeeCollaborator and AttendeeInnovationOrganization Tracks validation

            var attendeeCollaboratorInnovationOrganizationTrackOptionGroupsUids = await this.GetAttendeeCollaboratorInnovationOrganizationTrackOptionsGroupUids();
            if (attendeeInnovationOrganizationDto.AttendeeInnovationOrganizationTrackDtos.Any(aiotDto => attendeeCollaboratorInnovationOrganizationTrackOptionGroupsUids.Contains(aiotDto.InnovationOrganizationTrackOptionGroup?.Uid)) == false)
            {
                this.StatusMessageToastr(string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()), Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);
                return RedirectToAction("EvaluationList", "Projects", new { Area = "Innovation" });
            }

            #endregion

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/BusinessInformationWidget", attendeeInnovationOrganizationDto), divIdOrClass = "#ProjectBusinessInformationWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Tracks Widget

        /// <summary>
        /// Shows the tracks widget.
        /// </summary>
        /// <param name="attendeeInnovationOrganizationUid">The attendee innovation organization uid.</param>
        /// <returns></returns>
        [HttpGet]
        [AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.CommissionInnovation)]
        public async Task<ActionResult> ShowTracksWidget(Guid? attendeeInnovationOrganizationUid)
        {
            var attendeeInnovationOrganizationDto = await this.attendeeInnovationOrganizationRepo.FindTracksWidgetDtoAsync(attendeeInnovationOrganizationUid ?? Guid.Empty);
            if (attendeeInnovationOrganizationDto == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Startup, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            #region AttendeeCollaborator and AttendeeInnovationOrganization Tracks validation

            var attendeeCollaboratorInnovationOrganizationTrackOptionGroupsUids = await this.GetAttendeeCollaboratorInnovationOrganizationTrackOptionsGroupUids();
            if (attendeeInnovationOrganizationDto.AttendeeInnovationOrganizationTrackDtos.Any(aiotDto => attendeeCollaboratorInnovationOrganizationTrackOptionGroupsUids.Contains(aiotDto.InnovationOrganizationTrackOptionGroup?.Uid)) == false)
            {
                this.StatusMessageToastr(string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()), Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);
                return RedirectToAction("EvaluationList", "Projects", new { Area = "Innovation" });
            }

            #endregion

            ViewBag.InnovationOrganizationTrackOptionGroupDtos = await this.innovationOrganizationTrackOptionGroupRepo.FindAllDtoAsync();

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/TracksWidget", attendeeInnovationOrganizationDto), divIdOrClass = "#ProjectTracksWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Objectives Widget

        /// <summary>
        /// Shows the objectives widget.
        /// </summary>
        /// <param name="attendeeInnovationOrganizationUid">The attendee innovation organization uid.</param>
        /// <returns></returns>
        [HttpGet]
        [AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.CommissionInnovation)]
        public async Task<ActionResult> ShowObjectivesWidget(Guid? attendeeInnovationOrganizationUid)
        {
            var attendeeInnovationOrganizationDto = await this.attendeeInnovationOrganizationRepo.FindObjectivesWidgetDtoAsync(attendeeInnovationOrganizationUid ?? Guid.Empty);
            if (attendeeInnovationOrganizationDto == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Startup, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            #region AttendeeCollaborator and AttendeeInnovationOrganization Tracks validation

            var attendeeCollaboratorInnovationOrganizationTrackOptionGroupsUids = await this.GetAttendeeCollaboratorInnovationOrganizationTrackOptionsGroupUids();
            if (attendeeInnovationOrganizationDto.AttendeeInnovationOrganizationTrackDtos.Any(aiotDto => attendeeCollaboratorInnovationOrganizationTrackOptionGroupsUids.Contains(aiotDto.InnovationOrganizationTrackOptionGroup?.Uid)) == false)
            {
                this.StatusMessageToastr(string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()), Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);
                return RedirectToAction("EvaluationList", "Projects", new { Area = "Innovation" });
            }

            #endregion

            ViewBag.InnovationOrganizationObjectivesOptions = await this.innovationOrganizationObjectivesOptionRepo.FindAllAsync();

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/ObjectivesWidget", attendeeInnovationOrganizationDto), divIdOrClass = "#ProjectObjectivesWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Technologies Widget

        /// <summary>
        /// Shows the technologies widget.
        /// </summary>
        /// <param name="attendeeInnovationOrganizationUid">The attendee innovation organization uid.</param>
        /// <returns></returns>
        [HttpGet]
        [AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.CommissionInnovation)]
        public async Task<ActionResult> ShowTechnologiesWidget(Guid? attendeeInnovationOrganizationUid)
        {
            var attendeeInnovationOrganizationDto = await this.attendeeInnovationOrganizationRepo.FindTechnologiesWidgetDtoAsync(attendeeInnovationOrganizationUid ?? Guid.Empty);
            if (attendeeInnovationOrganizationDto == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Startup, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            #region AttendeeCollaborator and AttendeeInnovationOrganization Tracks validation

            var attendeeCollaboratorInnovationOrganizationTrackOptionGroupsUids = await this.GetAttendeeCollaboratorInnovationOrganizationTrackOptionsGroupUids();
            if (attendeeInnovationOrganizationDto.AttendeeInnovationOrganizationTrackDtos.Any(aiotDto => attendeeCollaboratorInnovationOrganizationTrackOptionGroupsUids.Contains(aiotDto.InnovationOrganizationTrackOptionGroup?.Uid)) == false)
            {
                this.StatusMessageToastr(string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()), Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);
                return RedirectToAction("EvaluationList", "Projects", new { Area = "Innovation" });
            }

            #endregion

            ViewBag.InnovationOrganizationTechnologiesOptions = await this.innovationOrganizationTechnologyOptionRepo.FindAllAsync();

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/TechnologiesWidget", attendeeInnovationOrganizationDto), divIdOrClass = "#ProjectTechnologiesWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Experiences Widget

        /// <summary>
        /// Shows the experiences widget.
        /// </summary>
        /// <param name="attendeeInnovationOrganizationUid">The attendee innovation organization uid.</param>
        /// <returns></returns>
        [HttpGet]
        [AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.CommissionInnovation)]
        public async Task<ActionResult> ShowExperiencesWidget(Guid? attendeeInnovationOrganizationUid)
        {
            var attendeeInnovationOrganizationDto = await this.attendeeInnovationOrganizationRepo.FindExperiencesWidgetDtoAsync(attendeeInnovationOrganizationUid ?? Guid.Empty);
            if (attendeeInnovationOrganizationDto == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Startup, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            #region AttendeeCollaborator and AttendeeInnovationOrganization Tracks validation

            var attendeeCollaboratorInnovationOrganizationTrackOptionGroupsUids = await this.GetAttendeeCollaboratorInnovationOrganizationTrackOptionsGroupUids();
            if (attendeeInnovationOrganizationDto.AttendeeInnovationOrganizationTrackDtos.Any(aiotDto => attendeeCollaboratorInnovationOrganizationTrackOptionGroupsUids.Contains(aiotDto.InnovationOrganizationTrackOptionGroup?.Uid)) == false)
            {
                this.StatusMessageToastr(string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()), Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);
                return RedirectToAction("EvaluationList", "Projects", new { Area = "Innovation" });
            }

            #endregion

            ViewBag.InnovationOrganizationExperiencesOptions = await this.innovationOrganizationExperienceOptionRepo.FindAllAsync();

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/ExperiencesWidget", attendeeInnovationOrganizationDto), divIdOrClass = "#ProjectExperiencesWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Evaluation Grade Widget 

        /// <summary>
        /// Shows the evaluation grade widget.
        /// </summary>
        /// <param name="projectUid">The project uid.</param>
        /// <returns></returns>
        [HttpGet]
        [AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.CommissionInnovation)]
        public async Task<ActionResult> ShowEvaluationGradeWidget(Guid? attendeeInnovationOrganizationUid)
        {
            if (this.EditionDto?.IsInnovationProjectEvaluationStarted() != true)
            {
                return Json(new { status = "error", message = Texts.ForbiddenErrorMessage }, JsonRequestBehavior.AllowGet);
            }

            var attendeeInnovationOrganizationDto = await this.attendeeInnovationOrganizationRepo.FindEvaluationGradeWidgetDtoAsync(attendeeInnovationOrganizationUid ?? Guid.Empty, this.UserAccessControlDto.User.Id);
            if (attendeeInnovationOrganizationDto == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            #region AttendeeCollaborator and AttendeeInnovationOrganization Tracks validation

            var attendeeCollaboratorInnovationOrganizationTrackOptionGroupsUids = await this.GetAttendeeCollaboratorInnovationOrganizationTrackOptionsGroupUids();
            if (attendeeInnovationOrganizationDto.AttendeeInnovationOrganizationTrackDtos.Any(aiotDto => attendeeCollaboratorInnovationOrganizationTrackOptionGroupsUids.Contains(aiotDto.InnovationOrganizationTrackOptionGroup?.Uid)) == false)
            {
                this.StatusMessageToastr(string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()), Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);
                return RedirectToAction("EvaluationList", "Projects", new { Area = "Innovation" });
            }

            #endregion

            ViewBag.ApprovedAttendeeInnovationOrganizationsIds = await this.attendeeInnovationOrganizationRepo.FindAllApprovedAttendeeInnovationOrganizationsIdsAsync(this.EditionDto.Edition.Id);

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/EvaluationGradeWidget", attendeeInnovationOrganizationDto), divIdOrClass = "#ProjectEvaluationWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Evaluates the specified innovation organization identifier.
        /// </summary>
        /// <param name="innovationOrganizationId">The innovation organization identifier.</param>
        /// <param name="grade">The grade.</param>
        /// <returns></returns>
        /// <exception cref="DomainException"></exception>
        [HttpPost]
        [AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.CommissionInnovation)]
        public async Task<ActionResult> Evaluate(int innovationOrganizationId, decimal? grade)
        {
            //if (this.EditionDto?.IsInnovationProjectEvaluationOpen() != true)
            //{
            //    return Json(new { status = "error", message = Messages.OutOfEvaluationPeriod }, JsonRequestBehavior.AllowGet);
            //}

            var result = new AppValidationResult();

            try
            {
                var cmd = new EvaluateInnovationOrganization(
                    await this.innovationOrganizationRepo.FindByIdAsync(innovationOrganizationId),
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
                //projectUid = cmd.InnovationBandId,
                message = string.Format(Messages.EntityActionSuccessfull, Labels.Startup, Labels.Evaluated.ToLowerInvariant())
            });
        }

        #endregion

        #region Evaluators Widget

        [HttpGet]
        [AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.CommissionInnovation)]
        public async Task<ActionResult> ShowEvaluatorsWidget(Guid? attendeeInnovationOrganizationUid)
        {
            if (this.EditionDto?.IsInnovationProjectEvaluationStarted() != true)
            {
                return Json(new { status = "error", message = Texts.ForbiddenErrorMessage }, JsonRequestBehavior.AllowGet);
            }

            var attendeeInnovationOrganizationDto = await this.attendeeInnovationOrganizationRepo.FindEvaluatorsWidgetDtoAsync(attendeeInnovationOrganizationUid ?? Guid.Empty);
            if (attendeeInnovationOrganizationDto == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            #region AttendeeCollaborator and AttendeeInnovationOrganization Tracks validation

            var attendeeCollaboratorInnovationOrganizationTrackOptionGroupsUids = await this.GetAttendeeCollaboratorInnovationOrganizationTrackOptionsGroupUids();
            if (attendeeInnovationOrganizationDto.AttendeeInnovationOrganizationTrackDtos.Any(aiotDto => attendeeCollaboratorInnovationOrganizationTrackOptionGroupsUids.Contains(aiotDto.InnovationOrganizationTrackOptionGroup?.Uid)) == false)
            {
                this.StatusMessageToastr(string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()), Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);
                return RedirectToAction("EvaluationList", "Projects", new { Area = "Innovation" });
            }

            #endregion

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/EvaluatorsWidget", attendeeInnovationOrganizationDto), divIdOrClass = "#ProjectEvaluatorsWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Founders Widget

        /// <summary>
        /// Shows the evaluators widget.
        /// </summary>
        /// <param name="attendeeInnovationOrganizationUid">The project uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowFoundersWidget(Guid? attendeeInnovationOrganizationUid)
        {
            var attendeeInnovationOrganizationDto = await this.attendeeInnovationOrganizationRepo.FindFoundersWidgetDtoAsync(attendeeInnovationOrganizationUid ?? Guid.Empty);
            if (attendeeInnovationOrganizationDto == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Startup, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            #region AttendeeCollaborator and AttendeeInnovationOrganization Tracks validation

            var attendeeCollaboratorInnovationOrganizationTrackOptionGroupsUids = await this.GetAttendeeCollaboratorInnovationOrganizationTrackOptionsGroupUids();
            if (attendeeInnovationOrganizationDto.AttendeeInnovationOrganizationTrackDtos.Any(aiotDto => attendeeCollaboratorInnovationOrganizationTrackOptionGroupsUids.Contains(aiotDto.InnovationOrganizationTrackOptionGroup?.Uid)) == false)
            {
                this.StatusMessageToastr(string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()), Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);
                return RedirectToAction("EvaluationList", "Projects", new { Area = "Innovation" });
            }

            #endregion

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/FoundersWidget", attendeeInnovationOrganizationDto), divIdOrClass = "#ProjectsFoundersWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Presentation Widget

        /// <summary>Shows the clipping widget.</summary>
        /// <param name="projectUid">The project uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowPresentationWidget(Guid? attendeeInnovationOrganizationUid)
        {
            var attendeeInnovationOrganizationDto = await this.attendeeInnovationOrganizationRepo.FindPresentationWidgetDtoAsync(attendeeInnovationOrganizationUid ?? Guid.Empty);
            if (attendeeInnovationOrganizationDto == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            #region AttendeeCollaborator and AttendeeInnovationOrganization Tracks validation

            var attendeeCollaboratorInnovationOrganizationTrackOptionGroupsUids = await this.GetAttendeeCollaboratorInnovationOrganizationTrackOptionsGroupUids();
            if (attendeeInnovationOrganizationDto.AttendeeInnovationOrganizationTrackDtos.Any(aiotDto => attendeeCollaboratorInnovationOrganizationTrackOptionGroupsUids.Contains(aiotDto.InnovationOrganizationTrackOptionGroup?.Uid)) == false)
            {
                this.StatusMessageToastr(string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()), Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);
                return RedirectToAction("EvaluationList", "Projects", new { Area = "Innovation" });
            }

            #endregion

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/PresentationWidget", attendeeInnovationOrganizationDto), divIdOrClass = "#ProjectsPresentationWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        /// <summary>
        /// Gets the attendee collaborator innovation organization track options group uids.
        /// </summary>
        /// <returns></returns>
        private async Task<List<Guid?>> GetAttendeeCollaboratorInnovationOrganizationTrackOptionsGroupUids()
        {
            List<Guid?> innovationOrganizationTrackOptionGroupUids = new List<Guid?>();

            var userDto = await this.userRepo.FindUserDtoByUserIdAsync(this.UserAccessControlDto.User.Id);
            var attendeeCollaborator = userDto.Collaborator?.GetAttendeeCollaboratorByEditionId(this.EditionDto.Edition.Id);
            if (attendeeCollaborator != null)
            {
                var innovationOrganizationTrackOptionGroups = await this.innovationOrganizationTrackOptionGroupRepo.FindAllByAttendeeCollaboratorIdAsync(attendeeCollaborator.Id);
                innovationOrganizationTrackOptionGroupUids = innovationOrganizationTrackOptionGroups.Select(ioto => ioto.Uid as Guid?).ToList();
            }
            else
            {
                //Admin don't have Collaborator/AttendeeCollaborator, so, list all options in Dropdown.
                var innovationOrganizationTrackOptionGroups = await this.innovationOrganizationTrackOptionGroupRepo.FindAllAsync();
                innovationOrganizationTrackOptionGroupUids = innovationOrganizationTrackOptionGroups.Select(ioto => ioto.Uid as Guid?).ToList();
            }

            return innovationOrganizationTrackOptionGroupUids;
        }

        /// <summary>
        /// Gets the search innovation organization track option uids.
        /// </summary>
        /// <param name="innovationOrganizationTrackOptionGroupUid">The InnovationOrganizationTrackOptionUid selected in dropdown filter</param>
        /// <returns></returns>
        private async Task<List<Guid?>> GetSearchInnovationOrganizationTrackOptionGroupUids(Guid? innovationOrganizationTrackOptionGroupUid)
        {
            var attendeeCollaboratorInnovationOrganizationTrackOptionGroupsUids = await this.GetAttendeeCollaboratorInnovationOrganizationTrackOptionsGroupUids();

            List<Guid?> innovationOrganizationTrackOptionGroups = new List<Guid?>();
            if (!innovationOrganizationTrackOptionGroupUid.HasValue)
            {
                //Search by "AttendeeCollaboratorInnovationOrganizationTrackOptionGroups" when have no "InnovationOrganizationTrackOptionGroupUid" selected in dropdown filter
                innovationOrganizationTrackOptionGroups.AddRange(attendeeCollaboratorInnovationOrganizationTrackOptionGroupsUids);
            }
            else
            {
                //Search by "InnovationOrganizationTrackOptionGroupUid" selected in dropdown filter
                innovationOrganizationTrackOptionGroups.Add(innovationOrganizationTrackOptionGroupUid);
            }

            return innovationOrganizationTrackOptionGroups;
        }
    }
}