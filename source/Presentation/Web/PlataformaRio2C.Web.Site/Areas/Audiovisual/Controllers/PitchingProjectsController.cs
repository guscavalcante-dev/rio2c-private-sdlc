// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Gilson Oliveira
// Created          : 10-22-2024
//
// Last Modified By : Gilson Oliveira
// Last Modified On : 10-30-2024
// ***********************************************************************
// <copyright file="PitchingProjectsController.cs" company="Softo">
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
using PlataformaRio2C.Application.CQRS.Queries;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.CrossCutting.Identity.AuthorizeAttributes;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using PlataformaRio2C.Infra.CrossCutting.Tools.Helpers;
using PlataformaRio2C.Web.Site.Filters;
using Constants = PlataformaRio2C.Domain.Constants;
using PlataformaRio2C.Web.Site.Controllers;

namespace PlataformaRio2C.Web.Site.Areas.Audiovisual.Controllers
{
    /// <summary>PitchingProjectsController</summary>
    [AjaxAuthorize(Order = 1)]
    [AuthorizeCollaboratorType(Order = 2, Types = Constants.CollaboratorType.PlayerExecutiveAudiovisual + "," + Constants.CollaboratorType.Industry)]
    public class PitchingProjectsController : BaseController
    {
        private readonly IProjectRepository projectRepo;
        private readonly IInterestRepository interestRepo;
        private readonly IActivityRepository activityRepo;
        private readonly ITargetAudienceRepository targetAudienceRepo;
        private readonly IAttendeeOrganizationRepository attendeeOrganizationRepo;
        private readonly IProjectEvaluationRefuseReasonRepository projectEvaluationRefuseReasonRepo;
        private readonly IProjectEvaluationStatusRepository evaluationStatusRepository;
        private readonly IProjectModalityRepository projectModalityRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="PitchingProjectsController" /> class.
        /// </summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="identityController">The identity controller.</param>
        /// <param name="projectRepository">The project repository.</param>
        /// <param name="interestRepository">The interest repository.</param>
        /// <param name="activityRepository">The activity repository.</param>
        /// <param name="targetAudienceRepository">The target audience repository.</param>
        /// <param name="attendeeOrganizationRepository">The attendee organization repository.</param>
        /// <param name="projectEvaluationRefuseReasonRepo">The project evaluation refuse reason repo.</param>
        /// <param name="evaluationStatusRepository">The project evaluation status repository.</param>
        /// <param name="projectModalityRepository">The project modality repository.</param>
        public PitchingProjectsController(
            IMediator commandBus,
            IdentityAutenticationService identityController,
            IProjectRepository projectRepository,
            IInterestRepository interestRepository,
            IActivityRepository activityRepository,
            ITargetAudienceRepository targetAudienceRepository,
            IAttendeeOrganizationRepository attendeeOrganizationRepository,
            IProjectEvaluationRefuseReasonRepository projectEvaluationRefuseReasonRepo,
            IProjectEvaluationStatusRepository evaluationStatusRepository,
            IProjectModalityRepository projectModalityRepository)
            : base(commandBus, identityController)
        {
            this.projectRepo = projectRepository;
            this.interestRepo = interestRepository;
            this.activityRepo = activityRepository;
            this.targetAudienceRepo = targetAudienceRepository;
            this.attendeeOrganizationRepo = attendeeOrganizationRepository;
            this.projectEvaluationRefuseReasonRepo = projectEvaluationRefuseReasonRepo;
            this.evaluationStatusRepository = evaluationStatusRepository;
            this.projectModalityRepository = projectModalityRepository;
        }

        #region Submitted List

        /// <summary>Submitteds the list.</summary>
        /// <returns></returns>
        [AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.Industry)]
        public async Task<ActionResult> Index()
        {
            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper($"{Labels.AudiovisualProjects} - {Labels.Pitching}", new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Labels.PitchingProjects, Url.Action("Index", "PitchingProjects", new { Area = "Audiovisual" })),
            });

            #endregion

            if (this.EditionDto?.IsAudiovisualProjectSubmitStarted() != true)
            {
                return RedirectToAction("Index", "PitchingProjects", new { Area = "Audiovisual" });
            }

            var projects = await this.projectRepo.FindAllDtosToSellAsync(
                this.UserAccessControlDto?.GetFirstAttendeeOrganizationCreated()?.Uid ?? Guid.Empty,
                false,
                new List<int> { ProjectModality.Both.Id, ProjectModality.Pitching.Id }
            );

            // Create fake projects in the list
            var projectMaxCount = this.EditionDto?.AttendeeOrganizationMaxSellProjectsCount ?? 0;
            if (projects.Count < projectMaxCount)
            {
                var initialProject = projects.Count + 1;

                for (int i = initialProject; i < projectMaxCount + 1; i++)
                {
                    projects.Add(new ProjectDto
                    {
                        IsFakeProject = true,
                        ProjectTitleDtos = new List<ProjectTitleDto>
                        {
                            new ProjectTitleDto
                            {
                                ProjectTitle = new ProjectTitle(Labels.Project + " " + i, new Language("", ViewBag.UserInterfaceLanguage), 0),
                                Language = new Language("", ViewBag.UserInterfaceLanguage)
                            }
                        }
                    });
                }
            }

            return View(projects);
        }

        #endregion

        #region Submitted Details

        /// <summary>Submitteds the details.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.Industry)]
        public async Task<ActionResult> SubmittedDetails(Guid? id)
        {
            if (this.EditionDto?.IsAudiovisualProjectSubmitStarted() != true)
            {
                return RedirectToAction("Index", "PitchingProjects", new { Area = "Audiovisual" });
            }

            var projectDto = await this.projectRepo.FindSiteDetailsDtoByProjectUidAsync(id ?? Guid.Empty, this.EditionDto.Id);
            if (projectDto == null)
            {
                this.StatusMessageToastr(string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()), Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);
                return RedirectToAction("Index", "PitchingProjects", new { Area = "Audiovisual" });
            }

            if (this.UserAccessControlDto?.HasEditionAttendeeOrganization(projectDto.SellerAttendeeOrganizationDto.AttendeeOrganization.Uid) != true) // Is seller
            {
                this.StatusMessageToastr(Texts.ForbiddenErrorMessage, Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);
                return RedirectToAction("Index", "PitchingProjects", new { Area = "Audiovisual" });
            }

            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper($"{Labels.AudiovisualProjects} - {Labels.Pitching}", new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Labels.PitchingProjects, Url.Action("Index", "PitchingProjects", new { Area = "Audiovisual" })),
                new BreadcrumbItemHelper(projectDto.GetTitleDtoByLanguageCode(this.UserInterfaceLanguage)?.ProjectTitle?.Value ?? Labels.Project, Url.Action("SubmittedDetails", "PitchingProjects", new { id }))
            });

            #endregion

            return View(projectDto);
        }

        #region Main Information Widget

        /// <summary>Shows the main information widget.</summary>
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

            if (this.UserAccessControlDto?.HasEditionAttendeeOrganization(mainInformationWidgetDto.SellerAttendeeOrganizationDto.AttendeeOrganization.Uid) != true                                                                    // Seller
                && (this.UserAccessControlDto?.HasAnyEditionAttendeeOrganization(mainInformationWidgetDto.ProjectBuyerEvaluationDtos?.Select(pbed => pbed.BuyerAttendeeOrganizationDto.AttendeeOrganization.Uid)?.ToList()) != true   // Buyer with project finished
                    || mainInformationWidgetDto.Project?.IsFinished() != true))
            {
                return Json(new { status = "error", message = Texts.ForbiddenErrorMessage }, JsonRequestBehavior.AllowGet);
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

        #region Update

        /// <summary>Shows the update main information modal.</summary>
        /// <param name="projectUid">The project uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowUpdateMainInformationModal(Guid? projectUid)
        {
            UpdatePitchingProjectMainInformation cmd;

            try
            {
                var mainInformationWidgetDto = await this.projectRepo.FindSiteMainInformationWidgetDtoByProjectUidAsync(projectUid ?? Guid.Empty);
                if (mainInformationWidgetDto == null)
                {
                    throw new DomainException(string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()));
                }

                if (this.UserAccessControlDto?.HasEditionAttendeeOrganization(mainInformationWidgetDto.SellerAttendeeOrganizationDto.AttendeeOrganization.Uid) != true)
                {
                    throw new DomainException(Texts.ForbiddenErrorMessage);
                }

                if (this.EditionDto?.IsAudiovisualProjectSubmitOpen() != true)
                {
                    throw new DomainException(Messages.ProjectSubmissionNotOpen);
                }

                if (mainInformationWidgetDto.Project.IsFinished())
                {
                    throw new DomainException(Messages.ProjectIsFinishedCannotBeUpdated);
                }

                cmd = new UpdatePitchingProjectMainInformation(
                    mainInformationWidgetDto,
                    await this.CommandBus.Send(new FindAllLanguagesDtosAsync(this.UserInterfaceLanguage)),
                    true,
                    false,
                    false,
                    this.UserInterfaceLanguage
                );
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
        public async Task<ActionResult> UpdateMainInformation(UpdatePitchingProjectMainInformation cmd)
        {
            var result = new AppValidationResult();

            try
            {
                if (!ModelState.IsValid)
                {
                    throw new DomainException(Messages.CorrectFormValues);
                }

                cmd.UpdatePreSendProperties(
                    this.UserAccessControlDto.GetFirstAttendeeOrganizationCreated()?.Uid,
                    ProjectType.AudiovisualBusinessRound.Uid,
                    this.UserAccessControlDto.User.Id,
                    this.UserAccessControlDto.User.Uid,
                    this.EditionDto.Id,
                    this.EditionDto.Uid,
                    this.UserInterfaceLanguage
                );
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
                var toastrError = result.Errors?.FirstOrDefault(e => e.Target == "ToastrError");

                //cmd.UpdateModelsAndLists(
                //    await this.interestRepo.FindAllGroupedByInterestGroupsAsync());

                return Json(new
                {
                    status = "error",
                    message = toastrError?.Message ?? ex.GetInnerMessage(),
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

            return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Project, Labels.UpdatedM) });
        }

        #endregion

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

            if (this.UserAccessControlDto?.HasEditionAttendeeOrganization(interestWidgetDto.SellerAttendeeOrganizationDto.AttendeeOrganization.Uid) != true                                                                    // Seller
                && (this.UserAccessControlDto?.HasAnyEditionAttendeeOrganization(interestWidgetDto.ProjectBuyerEvaluationDtos?.Select(pbed => pbed.BuyerAttendeeOrganizationDto.AttendeeOrganization.Uid)?.ToList()) != true   // Buyer with project finished
                    || interestWidgetDto.Project?.IsFinished() != true))
            {
                return Json(new { status = "error", message = Texts.ForbiddenErrorMessage }, JsonRequestBehavior.AllowGet);
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

        #region Update

        /// <summary>Shows the update interest modal.</summary>
        /// <param name="projectUid">The project uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowUpdateInterestModal(Guid? projectUid)
        {
            UpdateProjectInterests cmd;

            try
            {
                var interestWidgetDto = await this.projectRepo.FindSiteInterestWidgetDtoByProjectUidAsync(projectUid ?? Guid.Empty);
                if (interestWidgetDto == null)
                {
                    throw new DomainException(string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()));
                }

                if (this.UserAccessControlDto?.HasEditionAttendeeOrganization(interestWidgetDto.SellerAttendeeOrganizationDto.AttendeeOrganization.Uid) != true)
                {
                    throw new DomainException(Texts.ForbiddenErrorMessage);
                }

                if (this.EditionDto?.IsAudiovisualProjectSubmitOpen() != true)
                {
                    throw new DomainException(Messages.ProjectSubmissionNotOpen);
                }

                if (interestWidgetDto.Project.IsFinished())
                {
                    throw new DomainException(Messages.ProjectIsFinishedCannotBeUpdated);
                }

                cmd = new UpdateProjectInterests(
                    interestWidgetDto,
                    await this.interestRepo.FindAllDtosByProjectTypeIdAsync(ProjectType.AudiovisualBusinessRound.Id),
                    await this.targetAudienceRepo.FindAllByProjectTypeIdAsync(ProjectType.AudiovisualBusinessRound.Id));
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
                    new { page = this.RenderRazorViewToString("Modals/UpdateInterestModal", cmd), divIdOrClass = "#GlobalModalContainer" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>Updates the interests.</summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> UpdateInterests(UpdateProjectInterests cmd)
        {
            var result = new AppValidationResult();

            try
            {
                if (!ModelState.IsValid)
                {
                    throw new DomainException(Messages.CorrectFormValues);
                }

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
                foreach (var error in result.Errors)
                {
                    var target = error.Target ?? "";
                    ModelState.AddModelError(target, error.Message);
                }
                var toastrError = result.Errors?.FirstOrDefault(e => e.Target == "ToastrError");

                cmd.UpdateDropdownProperties(
                    await this.targetAudienceRepo.FindAllByProjectTypeIdAsync(ProjectType.AudiovisualBusinessRound.Id));

                return Json(new
                {
                    status = "error",
                    message = toastrError?.Message ?? ex.GetInnerMessage(),
                    pages = new List<dynamic>
                    {
                        new { page = this.RenderRazorViewToString("Modals/UpdateInterestForm", cmd), divIdOrClass = "#form-container" },
                    }
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return Json(new { status = "error", message = Messages.WeFoundAndError, }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Project, Labels.UpdatedM) });
        }

        #endregion

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

            if (this.UserAccessControlDto?.HasEditionAttendeeOrganization(linksWidgetDto.SellerAttendeeOrganizationDto.AttendeeOrganization.Uid) != true                                                                    // Seller
                && (this.UserAccessControlDto?.HasAnyEditionAttendeeOrganization(linksWidgetDto.ProjectBuyerEvaluationDtos?.Select(pbed => pbed.BuyerAttendeeOrganizationDto.AttendeeOrganization.Uid)?.ToList()) != true   // Buyer with project finished
                    || linksWidgetDto.Project?.IsFinished() != true))
            {
                return Json(new { status = "error", message = Texts.ForbiddenErrorMessage }, JsonRequestBehavior.AllowGet);
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

        #region Update

        /// <summary>Shows the update links modal.</summary>
        /// <param name="projectUid">The project uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowUpdateLinksModal(Guid? projectUid)
        {
            UpdateProjectLinks cmd;

            try
            {
                var linksWidgetDto = await this.projectRepo.FindSiteLinksWidgetDtoByProjectUidAsync(projectUid ?? Guid.Empty);
                if (linksWidgetDto == null)
                {
                    throw new DomainException(string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()));
                }

                if (this.UserAccessControlDto?.HasEditionAttendeeOrganization(linksWidgetDto.SellerAttendeeOrganizationDto.AttendeeOrganization.Uid) != true)
                {
                    throw new DomainException(Texts.ForbiddenErrorMessage);
                }

                if (this.EditionDto?.IsAudiovisualProjectSubmitOpen() != true)
                {
                    throw new DomainException(Messages.ProjectSubmissionNotOpen);
                }

                if (linksWidgetDto.Project.IsFinished())
                {
                    throw new DomainException(Messages.ProjectIsFinishedCannotBeUpdated);
                }

                cmd = new UpdateProjectLinks(linksWidgetDto);
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
                    new { page = this.RenderRazorViewToString("Modals/UpdateLinksModal", cmd), divIdOrClass = "#GlobalModalContainer" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>Updates the links.</summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> UpdateLinks(UpdateProjectLinks cmd)
        {
            var result = new AppValidationResult();

            try
            {
                if (!ModelState.IsValid)
                {
                    throw new DomainException(Messages.CorrectFormValues);
                }

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
                foreach (var error in result.Errors)
                {
                    var target = error.Target ?? "";
                    ModelState.AddModelError(target, error.Message);
                }
                var toastrError = result.Errors?.FirstOrDefault(e => e.Target == "ToastrError");

                //cmd.UpdateModelsAndLists(
                //    await this.interestRepo.FindAllGroupedByInterestGroupsAsync());

                return Json(new
                {
                    status = "error",
                    message = toastrError?.Message ?? ex.GetInnerMessage(),
                    pages = new List<dynamic>
                    {
                        new { page = this.RenderRazorViewToString("Modals/UpdateLinksForm", cmd), divIdOrClass = "#form-container" },
                    }
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return Json(new { status = "error", message = Messages.WeFoundAndError, }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Project, Labels.UpdatedM) });
        }

        #endregion

        #endregion

        #region Buyer Companies Widget

        /// <summary>Shows the buyer company widget.</summary>
        /// <param name="projectUid">The project uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowBuyerCompanyWidget(Guid? projectUid)
        {
            var buyerCompanyWidgetDto = await this.projectRepo.FindSiteBuyerCompanyWidgetDtoByProjectUidAsync(projectUid ?? Guid.Empty);
            if (buyerCompanyWidgetDto == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            if (this.UserAccessControlDto?.HasEditionAttendeeOrganization(buyerCompanyWidgetDto.SellerAttendeeOrganizationDto.AttendeeOrganization.Uid) != true) // Seller
            {
                return Json(new { status = "error", message = Texts.ForbiddenErrorMessage }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/BuyerCompanyWidget", buyerCompanyWidgetDto), divIdOrClass = "#ProjectBuyercompanyWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #region Update

        /// <summary>Shows the update buyer company modal.</summary>
        /// <param name="projectUid">The project uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowUpdateBuyerCompanyModal(Guid? projectUid)
        {
            ProjectDto buyerCompanyWidgetDto = null;

            try
            {
                buyerCompanyWidgetDto = await this.projectRepo.FindSiteBuyerCompanyWidgetDtoByProjectUidAsync(projectUid ?? Guid.Empty);
                if (buyerCompanyWidgetDto == null)
                {
                    return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
                }

                if (this.UserAccessControlDto?.HasEditionAttendeeOrganization(buyerCompanyWidgetDto.SellerAttendeeOrganizationDto.AttendeeOrganization.Uid) != true)
                {
                    throw new DomainException(Texts.ForbiddenErrorMessage);
                }

                if (this.EditionDto?.IsAudiovisualProjectSubmitOpen() != true)
                {
                    throw new DomainException(Messages.ProjectSubmissionNotOpen);
                }

                if (buyerCompanyWidgetDto.Project.IsFinished())
                {
                    throw new DomainException(Messages.ProjectIsFinishedCannotBeUpdated);
                }
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
                    new { page = this.RenderRazorViewToString("Modals/UpdateBuyerCompanyModal", buyerCompanyWidgetDto), divIdOrClass = "#GlobalModalContainer" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #endregion

        #endregion

        #region Submit

        /// <summary>Submits the specified identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.Industry)]
        public async Task<ActionResult> Submit(Guid? id)
        {
            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.ProjectInfo, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Labels.PitchingProjects, Url.Action("Index", "PitchingProjects", new { Area = "Audiovisual" })),
                new BreadcrumbItemHelper(Labels.Subscription, Url.Action("Submit", "PitchingProjects", new { Area = "Audiovisual" }))
            });

            #endregion

            if (this.EditionDto?.IsAudiovisualProjectSubmitOpen() != true)
            {
                this.StatusMessageToastr(Messages.ProjectSubmissionNotOpen, Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);
                return RedirectToAction("Index", "PitchingProjects");
            }

            if (this.UserAccessControlDto?.IsProjectSubmissionOrganizationInformationPending() == true)
            {
                return RedirectToAction("CompanyInfo", "PitchingProjects");
            }

            if (this.UserAccessControlDto?.IsAudiovisualProducerBusinessRoundTermsAcceptanceDatePending() == true)
            {
                return RedirectToAction("TermsAcceptance", "PitchingProjects", new { id });
            }

            // Check if producer submitted the max number of projects
            var firstAttendeeOrganizationCreated = this.UserAccessControlDto.GetFirstAttendeeOrganizationCreated();
            if (firstAttendeeOrganizationCreated != null)
            {
                var projectsCount = this.projectRepo.Count(p => p.SellerAttendeeOrganization.Uid == firstAttendeeOrganizationCreated.Uid && !p.IsDeleted && p.ProjectModalityId == ProjectModality.Pitching.Id);

                // TODO: Separate the AttendeeOrganizationMaxSellProjectsCount into AudiovisualBusinessRoundsMaxSellProjectsCount and AudiovisualPitchingMaxSellProjectsCount
                // today is using the same parameter value to both (Pitching and Business Rounds)
                var projectMaxCount = this.EditionDto?.AttendeeOrganizationMaxSellProjectsCount ?? 0;
                if (projectsCount >= projectMaxCount)
                {
                    this.StatusMessageToastr(Messages.YouReachedProjectsLimit, Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);
                    return RedirectToAction("Index", "PitchingProjects");
                }
            }

            // Duplicate project
            ProjectDto projectDto = null;
            if (id.HasValue)
            {
                projectDto = await this.projectRepo.FindSiteDuplicateDtoByProjectUidAsync(id.Value);
                if (projectDto != null)
                {
                    if (this.UserAccessControlDto?.HasEditionAttendeeOrganization(projectDto.SellerAttendeeOrganizationDto.AttendeeOrganization.Uid) != true)
                    {
                        this.StatusMessageToastr(Texts.ForbiddenErrorMessage, Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);
                        return RedirectToAction("Index", "PitchingProjects", new { Area = "Audiovisual" });
                    }
                }
            }

            var cmd = new CreatePitchingProject(
                projectDto,
                await this.CommandBus.Send(new FindAllLanguagesDtosAsync(this.UserInterfaceLanguage)),
                await this.targetAudienceRepo.FindAllByProjectTypeIdAsync(ProjectType.AudiovisualBusinessRound.Id),
                await this.interestRepo.FindAllDtosByProjectTypeIdAsync(ProjectType.AudiovisualBusinessRound.Id),
                true,
                false,
                false,
                this.UserInterfaceLanguage
            );

            return View(cmd);
        }

        /// <summary>Submits the specified create project.</summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Submit(CreatePitchingProject cmd)
        {
            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.ProjectInfo, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Labels.PitchingProjects, Url.Action("Index", "PitchingProjects", new { Area = "Audiovisual" })),
                new BreadcrumbItemHelper(Labels.Subscription, Url.Action("Submit", "PitchingProjects", new { Area = "Audiovisual" }))
            });

            #endregion

            if (this.EditionDto?.IsAudiovisualProjectSubmitOpen() != true)
            {
                this.StatusMessageToastr(Messages.ProjectSubmissionNotOpen, Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);
                return RedirectToAction("Index", "PitchingProjects");
            }

            if (this.UserAccessControlDto?.IsProjectSubmissionOrganizationInformationPending() == true
                || this.UserAccessControlDto?.IsAudiovisualProducerBusinessRoundTermsAcceptanceDatePending() == true)
            {
                return RedirectToAction("Submit", "PitchingProjects");
            }

            var result = new AppValidationResult();

            try
            {
                if (!ModelState.IsValid)
                {
                    throw new DomainException(Messages.CorrectFormValues);
                }
                cmd.UpdatePreSendProperties(
                    this.UserAccessControlDto.GetFirstAttendeeOrganizationCreated()?.Uid, //TODO: Change this
                    ProjectType.AudiovisualBusinessRound.Uid,
                    this.UserAccessControlDto.User.Id,
                    this.UserAccessControlDto.User.Uid,
                    this.EditionDto.Id,
                    this.EditionDto.Uid,
                    this.UserInterfaceLanguage,
                    ProjectModality.Pitching.Uid
                );
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
                var toastrError = result.Errors?.FirstOrDefault(e => e.Target == "ToastrError");

                this.StatusMessageToastr(toastrError?.Message ?? ex.GetInnerMessage(), Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);

                cmd.UpdateDropdownProperties(
                    await this.targetAudienceRepo.FindAllByProjectTypeIdAsync(ProjectType.AudiovisualBusinessRound.Id),
                    this.UserInterfaceLanguage
                );

                return View(cmd);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                this.StatusMessageToastr(Messages.WeFoundAndError, Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);

                cmd.UpdateDropdownProperties(
                    await this.targetAudienceRepo.FindAllByProjectTypeIdAsync(ProjectType.AudiovisualBusinessRound.Id),
                    this.UserInterfaceLanguage
                );

                return View(cmd);
            }

            this.StatusMessageToastr(string.Format(Messages.EntityActionSuccessfull, Labels.Project, Labels.CreatedM.ToLowerInvariant()), Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Success);

            try
            {
                var project = result.Data as Project;
                if (project != null)
                {
                    return RedirectToAction("SubmittedDetails", "PitchingProjects", new { id = project.Uid });
                }
            }
            catch
            {
                // ignored
            }

            return RedirectToAction("Index", "PitchingProjects");
        }

        /// <summary>Submitteds the list.</summary>
        /// <returns></returns>
        [AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.Industry)]
        public async Task<ActionResult> SubmittedListPitching()
        {
            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper($"{Labels.AudiovisualProjects} - {Labels.BusinessRound}", new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper($"{Labels.Projects} - {Labels.BusinessRound}", Url.Action("Index", "PitchingProjects", new { Area = "Audiovisual" })),
            });

            #endregion

            var view = new PitchingProjectDto() { AttendeeOrganizationUid = this.UserAccessControlDto?.GetFirstAttendeeOrganizationCreated()?.Uid , AttendeeCollaboratorUid = this.UserAccessControlDto?.EditionAttendeeCollaborator.Uid };

            return View(view);
        }

        #endregion

        #region Producer Info

        /// <summary>Companies the information.</summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> CompanyInfo()
        {
            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.CompanyInfo, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Labels.PitchingProjects, Url.Action("Index", "PitchingProjects", new { Area = "Audiovisual" })),
                new BreadcrumbItemHelper(Labels.Subscription, Url.Action("Submit", "PitchingProjects", new { Area = "Audiovisual" }))
            });

            #endregion

            if (this.EditionDto?.IsAudiovisualProjectSubmitOpen() != true)
            {
                this.StatusMessageToastr(Messages.ProjectSubmissionNotOpen, Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);
                return RedirectToAction("Index", "PitchingProjects");
            }

            if (this.UserAccessControlDto?.IsProjectSubmissionOrganizationInformationPending() != true)
            {
                return RedirectToAction("Submit", "PitchingProjects");
            }

            //this.SetViewBags();

            var currentOrganization = this.UserAccessControlDto?.EditionAttendeeOrganizations?.FirstOrDefault(eao => !eao.ProjectSubmissionOrganizationDate.HasValue)?.Organization;

            var cmd = new OnboardProducerOrganizationData(
                currentOrganization != null ? await this.CommandBus.Send(new FindOrganizationDtoByUidAsync(currentOrganization.Uid, this.EditionDto.Id, this.UserInterfaceLanguage)) : null,
                await this.CommandBus.Send(new FindAllLanguagesDtosAsync(this.UserInterfaceLanguage)),
                await this.CommandBus.Send(new FindAllCountriesBaseDtosAsync(this.UserInterfaceLanguage)),
                await this.activityRepo.FindAllByProjectTypeIdAsync(ProjectType.AudiovisualBusinessRound.Id),
                await this.targetAudienceRepo.FindAllByProjectTypeIdAsync(ProjectType.AudiovisualBusinessRound.Id),
                true,
                true,
                true);

            return View(cmd);
        }

        /// <summary>Companies the information.</summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> CompanyInfo(OnboardProducerOrganizationData cmd)
        {
            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.CompanyInfo, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Labels.PitchingProjects, Url.Action("Index", "PitchingProjects", new { Area = "Audiovisual" })),
                new BreadcrumbItemHelper(Labels.Subscription, Url.Action("Submit", "PitchingProjects", new { Area = "Audiovisual" }))
            });

            #endregion

            if (this.EditionDto?.IsAudiovisualProjectSubmitOpen() != true)
            {
                this.StatusMessageToastr(Messages.ProjectSubmissionNotOpen, Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);
                return RedirectToAction("Index", "PitchingProjects");
            }

            if (this.UserAccessControlDto?.IsProjectSubmissionOrganizationInformationPending() != true)
            {
                return RedirectToAction("Submit", "PitchingProjects");
            }

            var result = new AppValidationResult();

            try
            {
                if (!ModelState.IsValid)
                {
                    throw new DomainException(Messages.CorrectFormValues);
                }

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
                foreach (var error in result.Errors)
                {
                    var target = error.Target ?? "";
                    ModelState.AddModelError(target, error.Message);
                }
                var toastrError = result.Errors?.FirstOrDefault(e => e.Target == "ToastrError");

                this.StatusMessageToastr(toastrError?.Message ?? ex.GetInnerMessage(), Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);

                cmd.UpdateDropdownProperties(
                    await this.CommandBus.Send(new FindAllCountriesBaseDtosAsync(this.UserInterfaceLanguage)),
                    await this.targetAudienceRepo.FindAllByProjectTypeIdAsync(ProjectType.AudiovisualBusinessRound.Id));

                return View(cmd);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                this.StatusMessageToastr(Messages.WeFoundAndError, Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);

                cmd.UpdateDropdownProperties(
                    await this.CommandBus.Send(new FindAllCountriesBaseDtosAsync(this.UserInterfaceLanguage)),
                    await this.targetAudienceRepo.FindAllByProjectTypeIdAsync(ProjectType.AudiovisualBusinessRound.Id));

                return View(cmd);
            }

            this.StatusMessageToastr(string.Format(Messages.EntityActionSuccessfull, Labels.Company, Labels.UpdatedF.ToLowerInvariant()), Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Success);

            return RedirectToAction("Submit", "PitchingProjects");
        }

        #endregion

        #region Producer Terms Acceptance

        /// <summary>Termses the acceptance.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> TermsAcceptance(Guid? id)
        {
            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.ParticipantsTerms, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Labels.PitchingProjects, Url.Action("Index", "PitchingProjects", new { Area = "Audiovisual" })),
                new BreadcrumbItemHelper(Labels.Subscription, Url.Action("Submit", "PitchingProjects", new { Area = "Audiovisual" }))
            });

            #endregion

            if (this.EditionDto?.IsAudiovisualProjectSubmitOpen() != true)
            {
                this.StatusMessageToastr(Messages.ProjectSubmissionNotOpen, Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);
                return RedirectToAction("Index", "PitchingProjects");
            }

            if (this.UserAccessControlDto?.IsProjectSubmissionOrganizationInformationPending() == true
                || this.UserAccessControlDto?.IsAudiovisualProducerBusinessRoundTermsAcceptanceDatePending() != true)
            {
                return RedirectToAction("Submit", "PitchingProjects");
            }

            var cmd = new OnboardAudiovisualProducerPitchingTerms(id);

            return View(cmd);
        }

        /// <summary>Termses the acceptance.</summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> TermsAcceptance(OnboardAudiovisualProducerPitchingTerms cmd)
        {
            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.ParticipantsTerms, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Labels.PitchingProjects, Url.Action("Index", "PitchingProjects", new { Area = "Audiovisual" })),
                new BreadcrumbItemHelper(Labels.Subscription, Url.Action("Submit", "PitchingProjects", new { Area = "Audiovisual" }))
            });

            #endregion

            if (this.EditionDto?.IsAudiovisualProjectSubmitOpen() != true)
            {
                this.StatusMessageToastr(Messages.ProjectSubmissionNotOpen, Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);
                return RedirectToAction("Index", "PitchingProjects");
            }

            if (this.UserAccessControlDto?.IsProjectSubmissionOrganizationInformationPending() == true
                || this.UserAccessControlDto?.IsAudiovisualProducerBusinessRoundTermsAcceptanceDatePending() != true)
            {
                return RedirectToAction("Submit", "PitchingProjects");
            }

            var result = new AppValidationResult();

            try
            {
                if (!ModelState.IsValid)
                {
                    throw new DomainException(Messages.CorrectFormValues);
                }

                cmd.UpdatePreSendProperties(
                    this.UserAccessControlDto.Collaborator.Uid,
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
                foreach (var error in result.Errors)
                {
                    var target = error.Target ?? "";
                    ModelState.AddModelError(target, error.Message);
                }
                var toastrError = result.Errors?.FirstOrDefault(e => e.Target == "ToastrError");

                this.StatusMessageToastr(toastrError?.Message ?? ex.GetInnerMessage(), Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);

                return View(cmd);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                this.StatusMessageToastr(Messages.WeFoundAndError, Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);

                return View(cmd);
            }

            this.StatusMessageToastr(string.Format(Messages.EntityActionSuccessfull, Labels.ParticipantsTerms, Labels.Accepted.ToLowerInvariant()), Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Success);

            return RedirectToAction("Submit", "PitchingProjects");
        }

        #endregion

        #region Finish

        /// <summary>Finishes the specified command.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="originPage">The origin page.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Finish(FinishProject cmd, string originPage)
        {
            var result = new AppValidationResult();

            try
            {
                if (!ModelState.IsValid)
                {
                    throw new DomainException(Messages.CorrectFormValues);
                }

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
                foreach (var error in result.Errors)
                {
                    var target = error.Target ?? "";
                    ModelState.AddModelError(target, error.Message);
                }
                var toastrError = result.Errors?.FirstOrDefault(e => e.Target == "ToastrError");

                //cmd.UpdateModelsAndLists(
                //    await this.interestRepo.FindAllGroupedByInterestGroupsAsync());

                return Json(new { status = "error", message = toastrError?.Message ?? ex.GetInnerMessage() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return Json(new { status = "error", message = Messages.WeFoundAndError, }, JsonRequestBehavior.AllowGet);
            }

            var successMessage = string.Format(Messages.EntityActionSuccessfull, Labels.Project, Labels.Sent.ToLowerInvariant());

            if (originPage == "SendToPlayers")
            {
                this.StatusMessageToastr(successMessage, Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Success);
                return Json(new { status = "success", redirectOnly = Url.Action("SubmittedDetails", "PitchingProjects", new { id = cmd.ProjectUid }) });
            }

            return Json(new { status = "success", message = successMessage });
        }

        #endregion
    }
}