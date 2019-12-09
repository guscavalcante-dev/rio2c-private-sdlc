// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 06-28-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-09-2019
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

namespace PlataformaRio2C.Web.Site.Controllers
{
    /// <summary>ProjectsController</summary>
    [AjaxAuthorize(Order = 1)]
    [AuthorizeCollaboratorType(Order = 2, Types = Constants.CollaboratorType.ExecutiveAudiovisual + "," + Constants.CollaboratorType.Industry)]
    public class ProjectsController : BaseController
    {
        private readonly IProjectRepository projectRepo;
        private readonly IInterestRepository interestRepo;
        private readonly IActivityRepository activityRepo;
        private readonly ITargetAudienceRepository targetAudienceRepo;
        private readonly IAttendeeOrganizationRepository attendeeOrganizationRepo;

        /// <summary>Initializes a new instance of the <see cref="ProjectsController"/> class.</summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="identityController">The identity controller.</param>
        /// <param name="projectRepository">The project repository.</param>
        /// <param name="interestRepository">The interest repository.</param>
        /// <param name="activityRepository">The activity repository.</param>
        /// <param name="targetAudienceRepository">The target audience repository.</param>
        /// <param name="attendeeOrganizationRepository">The attendee organization repository.</param>
        /// <param name="languageRepo">The language repo.</param>
        public ProjectsController(
            IMediator commandBus,
            IdentityAutenticationService identityController,
            IProjectRepository projectRepository,
            IInterestRepository interestRepository,
            IActivityRepository activityRepository,
            ITargetAudienceRepository targetAudienceRepository,
            IAttendeeOrganizationRepository attendeeOrganizationRepository,
            ILanguageRepository languageRepo)
            : base(commandBus, identityController)
        {
            this.projectRepo = projectRepository;
            this.interestRepo = interestRepository;
            this.activityRepo = activityRepository;
            this.targetAudienceRepo = targetAudienceRepository;
            this.attendeeOrganizationRepo = attendeeOrganizationRepository;
        }

        #region Schedule

        /// <summary>Indexes this instance.</summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.AudiovisualProjects, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Labels.Projects, Url.Action("Index", "Projects", new { Area = "" }))
            });

            #endregion

            //TODO: Enable this projects redirection when projects is implemented
            //// If the user is not player and industry, redirect to the correct page
            //if (this.UserAccessControlDto?.HasCollaboratorType(Constants.CollaboratorType.ExecutiveAudiovisual) == true
            //    && this.UserAccessControlDto?.HasCollaboratorType(Constants.CollaboratorType.Industry) != true)
            //{
            //    return RedirectToAction("Review", "Projects", new { Area = "" });
            //}
            //if (this.UserAccessControlDto?.HasCollaboratorType(Constants.CollaboratorType.ExecutiveAudiovisual) != true
            //    && this.UserAccessControlDto?.HasCollaboratorType(Constants.CollaboratorType.Industry) == true)
            //{
            //    return RedirectToAction("Submited", "Projects", new { Area = "" });
            //}

            return View();
        }

        #endregion

        #region Industry

        #region Submitted List

        /// <summary>Submitteds the list.</summary>
        /// <returns></returns>
        [AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.Industry)]
        public async Task<ActionResult> SubmittedList()
        {
            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.AudiovisualProjects, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Labels.Projects, Url.Action("SubmittedList", "Projects", new { Area = "" })),
            });

            #endregion

            if (this.EditionDto?.IsProjectSubmitStarted() != true)
            {
                return RedirectToAction("Index", "Projects", new { Area = "" });
            }

            var projects = await this.projectRepo.FindAllDtosToSellAsync(
                this.UserAccessControlDto?.GetFirstAttendeeOrganizationCreated()?.Uid ?? Guid.Empty,
                false);

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
            if (this.EditionDto?.IsProjectSubmitStarted() != true)
            {
                return RedirectToAction("Index", "Projects", new { Area = "" });
            }

            var projectDto = await this.projectRepo.FindSiteDetailsDtoByProjectUidAsync(id ?? Guid.Empty);
            if (projectDto == null)
            {
                this.StatusMessageToastr(string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()), Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);
                return RedirectToAction("SubmittedList", "Projects", new { Area = "" });
            }

            if (!projectDto.Project.IsFinished())
            {
                if (this.UserAccessControlDto?.HasCollaboratorType(Constants.CollaboratorType.ExecutiveAudiovisual) == true)
                {
                    if (this.UserAccessControlDto?.HasCollaboratorType(Constants.CollaboratorType.Industry) != true)
                    {
                        this.StatusMessageToastr(string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()), Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);
                        return RedirectToAction("SubmittedList", "Projects", new { Area = "" });
                    }
                }
            }

            if (!this.HasEdition(projectDto.SellerAttendeeOrganizationDto.AttendeeOrganization.Uid))
            {
                this.StatusMessageToastr(Texts.ForbiddenErrorMessage, Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);
                return RedirectToAction("SubmittedList", "Projects", new { Area = "" });
            }

            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.AudiovisualProjects, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Labels.Projects, Url.Action("SubmittedList", "Projects", new { Area = "" })),
                new BreadcrumbItemHelper(projectDto.GetTitleDtoByLanguageCode(this.UserInterfaceLanguage)?.ProjectTitle?.Value ?? Labels.Project, Url.Action("SubmittedDetails", "Projects", new { id }))
            });

            #endregion

            return View(projectDto);
        }

        /// <summary>Submitteds the details.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.ExecutiveAudiovisual)]
        public async Task<ActionResult> SubmittedDetailsEvaluation(Guid? id)
        {
            if (this.EditionDto?.IsProjectSubmitStarted() != true)
            {
                return RedirectToAction("Index", "Projects", new { Area = "" });
            }

            var projectDto = await this.projectRepo.FindSiteDetailsDtoByProjectUidAsync(id ?? Guid.Empty);
            if (projectDto == null)
            {
                this.StatusMessageToastr(string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()), Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);
                return RedirectToAction("SubmittedList", "Projects", new { Area = "" });
            }

            if (!projectDto.Project.IsFinished())
            {
                if (this.UserAccessControlDto?.HasCollaboratorType(Constants.CollaboratorType.ExecutiveAudiovisual) == true)
                {
                    if (this.UserAccessControlDto?.HasCollaboratorType(Constants.CollaboratorType.Industry) != true)
                    {
                        this.StatusMessageToastr(string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()), Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);
                        return RedirectToAction("SubmittedList", "Projects", new { Area = "" });
                    }
                }
            }

            if (!this.HasEdition(projectDto.SellerAttendeeOrganizationDto.AttendeeOrganization.Uid))
            {
                this.StatusMessageToastr(Texts.ForbiddenErrorMessage, Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);
                return RedirectToAction("SubmittedList", "Projects", new { Area = "" });
            }

            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.AudiovisualProjects, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Labels.Projects, Url.Action("EvaluationList", "Projects", new { Area = "" })),
                new BreadcrumbItemHelper(projectDto.GetTitleDtoByLanguageCode(this.UserInterfaceLanguage)?.ProjectTitle?.Value ?? Labels.Project, Url.Action("SubmittedDetailsEvaluation", "Projects", new { id }))
            });

            #endregion

            return View("SubmittedDetails", projectDto);
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

            if (!this.HasEdition(mainInformationWidgetDto.SellerAttendeeOrganizationDto.AttendeeOrganization.Uid))
            {
                this.StatusMessageToastr(Texts.ForbiddenErrorMessage, Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);
                return RedirectToAction("SubmittedList", "Projects", new { Area = "" });
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
            UpdateProjectMainInformation cmd;

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


                if (this.EditionDto?.IsProjectSubmitOpen() != true)
                {
                    throw new DomainException(Messages.ProjectSubmissionNotOpen);
                }

                if (mainInformationWidgetDto.Project.IsFinished())
                {
                    throw new DomainException(Messages.ProjectIsFinishedCannotBeUpdated);
                }

                cmd = new UpdateProjectMainInformation(
                    mainInformationWidgetDto,
                    await this.CommandBus.Send(new FindAllLanguagesDtosAsync(this.UserInterfaceLanguage)),
                    true,
                    false,
                    false);
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
        public async Task<ActionResult> UpdateMainInformation(UpdateProjectMainInformation cmd)
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

            if (!this.HasEdition(interestWidgetDto.SellerAttendeeOrganizationDto.AttendeeOrganization.Uid))
            {
                return Json(new { status = "error", message = Texts.ForbiddenErrorMessage }, JsonRequestBehavior.AllowGet);
            }

            ViewBag.GroupedInterests = await this.interestRepo.FindAllGroupedByInterestGroupsAsync();
            ViewBag.TargetAudiences = await this.targetAudienceRepo.FindAllAsync();

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

                if (this.EditionDto?.IsProjectSubmitOpen() != true)
                {
                    throw new DomainException(Messages.ProjectSubmissionNotOpen);
                }

                if (interestWidgetDto.Project.IsFinished())
                {
                    throw new DomainException(Messages.ProjectIsFinishedCannotBeUpdated);
                }

                cmd = new UpdateProjectInterests(
                    interestWidgetDto,
                    await this.interestRepo.FindAllDtosAsync(),
                    await this.targetAudienceRepo.FindAllAsync());
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
                    await this.targetAudienceRepo.FindAllAsync());

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

            if (!this.HasEdition(linksWidgetDto.SellerAttendeeOrganizationDto.AttendeeOrganization.Uid))
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

                if (this.EditionDto?.IsProjectSubmitOpen() != true)
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

            if (!this.HasEdition(buyerCompanyWidgetDto.SellerAttendeeOrganizationDto.AttendeeOrganization.Uid))
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

                if (this.EditionDto?.IsProjectSubmitOpen() != true)
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
                new BreadcrumbItemHelper(Labels.Projects, Url.Action("SubmittedList", "Projects", new { Area = "" })),
                new BreadcrumbItemHelper(Labels.Subscription, Url.Action("Submit", "Projects", new { Area = "" }))
            });

            #endregion

            if (this.EditionDto?.IsProjectSubmitOpen() != true)
            {
                this.StatusMessageToastr(Messages.ProjectSubmissionNotOpen, Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);
                return RedirectToAction("Index", "Projects");
            }

            if (this.UserAccessControlDto?.IsProjectSubmissionOrganizationInformationPending() == true)
            {
                return RedirectToAction("CompanyInfo", "Projects");
            }

            if (this.UserAccessControlDto?.IsProjectSubmissionTermsAcceptancePending() == true)
            {
                return RedirectToAction("TermsAcceptance", "Projects", new { id });
            }

            // Check if player submitted the max number of projects
            var firstAttendeeOrganizationCreated = this.UserAccessControlDto.GetFirstAttendeeOrganizationCreated();
            if (firstAttendeeOrganizationCreated != null)
            {
                var projectsCount = this.projectRepo.Count(p => p.SellerAttendeeOrganization.Uid == firstAttendeeOrganizationCreated.Uid
                                                                && !p.IsDeleted);
                var projectMaxCount = this.EditionDto?.AttendeeOrganizationMaxSellProjectsCount ?? 0;
                if (projectsCount >= projectMaxCount)
                {
                    this.StatusMessageToastr(Messages.YouReachedProjectsLimit, Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);
                    return RedirectToAction("SubmittedList", "Projects");
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
                        return RedirectToAction("SubmittedList", "Projects", new { Area = "" });
                    }
                }
            }

            var cmd = new CreateProject(
                projectDto,
                await this.CommandBus.Send(new FindAllLanguagesDtosAsync(this.UserInterfaceLanguage)),
                await this.targetAudienceRepo.FindAllAsync(),
                await this.interestRepo.FindAllDtosAsync(),
                true,
                false,
                false);

            return View(cmd);
        }

        /// <summary>Submits the specified create project.</summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Submit(CreateProject cmd)
        {
            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.ProjectInfo, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Labels.Projects, Url.Action("SubmittedList", "Projects", new { Area = "" })),
                new BreadcrumbItemHelper(Labels.Subscription, Url.Action("Submit", "Projects", new { Area = "" }))
            });

            #endregion

            if (this.EditionDto?.IsProjectSubmitOpen() != true)
            {
                this.StatusMessageToastr(Messages.ProjectSubmissionNotOpen, Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);
                return RedirectToAction("Index", "Projects");
            }

            if (this.UserAccessControlDto?.IsProjectSubmissionOrganizationInformationPending() == true
                || this.UserAccessControlDto?.IsProjectSubmissionTermsAcceptancePending() == true)
            {
                return RedirectToAction("Submit", "Projects");
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
                    ProjectType.Audiovisual.Uid,
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
                    await this.targetAudienceRepo.FindAllAsync());

                return View(cmd);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                this.StatusMessageToastr(Messages.WeFoundAndError, Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);

                cmd.UpdateDropdownProperties(
                    await this.targetAudienceRepo.FindAllAsync());

                return View(cmd);
            }

            this.StatusMessageToastr(string.Format(Messages.EntityActionSuccessfull, Labels.Project, Labels.CreatedM.ToLowerInvariant()), Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Success);

            try
            {
                var project = result.Data as Project;
                if (project != null)
                {
                    return RedirectToAction("SendToPlayers", "Projects", new { id = project.Uid });
                }
            }
            catch
            {
                // ignored
            }

            return RedirectToAction("Index", "Projects");
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
                new BreadcrumbItemHelper(Labels.Projects, Url.Action("SubmittedList", "Projects", new { Area = "" })),
                new BreadcrumbItemHelper(Labels.Subscription, Url.Action("Submit", "Projects", new { Area = "" }))
            });

            #endregion

            if (this.EditionDto?.IsProjectSubmitOpen() != true)
            {
                this.StatusMessageToastr(Messages.ProjectSubmissionNotOpen, Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);
                return RedirectToAction("Index", "Projects");
            }

            if (this.UserAccessControlDto?.IsProjectSubmissionOrganizationInformationPending() != true)
            {
                return RedirectToAction("Submit", "Projects");
            }

            //this.SetViewBags();

            var currentOrganization = this.UserAccessControlDto?.EditionAttendeeOrganizations?.FirstOrDefault(eao => !eao.ProjectSubmissionOrganizationDate.HasValue)?.Organization;

            var cmd = new OnboardProducerOrganizationData(
                currentOrganization != null ? await this.CommandBus.Send(new FindOrganizationDtoByUidAsync(currentOrganization.Uid, this.EditionDto.Id, this.UserInterfaceLanguage)) : null,
                await this.CommandBus.Send(new FindAllLanguagesDtosAsync(this.UserInterfaceLanguage)),
                await this.CommandBus.Send(new FindAllCountriesBaseDtosAsync(this.UserInterfaceLanguage)),
                await this.activityRepo.FindAllAsync(),
                await this.targetAudienceRepo.FindAllAsync(),
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
                new BreadcrumbItemHelper(Labels.Projects, Url.Action("SubmittedList", "Projects", new { Area = "" })),
                new BreadcrumbItemHelper(Labels.Subscription, Url.Action("Submit", "Projects", new { Area = "" }))
            });

            #endregion

            if (this.EditionDto?.IsProjectSubmitOpen() != true)
            {
                this.StatusMessageToastr(Messages.ProjectSubmissionNotOpen, Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);
                return RedirectToAction("Index", "Projects");
            }

            if (this.UserAccessControlDto?.IsProjectSubmissionOrganizationInformationPending() != true)
            {
                return RedirectToAction("Submit", "Projects");
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
                    await this.targetAudienceRepo.FindAllAsync());

                return View(cmd);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                this.StatusMessageToastr(Messages.WeFoundAndError, Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);

                cmd.UpdateDropdownProperties(
                    await this.CommandBus.Send(new FindAllCountriesBaseDtosAsync(this.UserInterfaceLanguage)),
                    await this.targetAudienceRepo.FindAllAsync());

                return View(cmd);
            }

            this.StatusMessageToastr(string.Format(Messages.EntityActionSuccessfull, Labels.Company, Labels.UpdatedF.ToLowerInvariant()), Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Success);

            return RedirectToAction("Submit", "Projects");
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
                new BreadcrumbItemHelper(Labels.Projects, Url.Action("SubmittedList", "Projects", new { Area = "" })),
                new BreadcrumbItemHelper(Labels.Subscription, Url.Action("Submit", "Projects", new { Area = "" }))
            });

            #endregion

            if (this.EditionDto?.IsProjectSubmitOpen() != true)
            {
                this.StatusMessageToastr(Messages.ProjectSubmissionNotOpen, Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);
                return RedirectToAction("Index", "Projects");
            }

            if (this.UserAccessControlDto?.IsProjectSubmissionOrganizationInformationPending() == true
                || this.UserAccessControlDto?.IsProjectSubmissionTermsAcceptancePending() != true)
            {
                return RedirectToAction("Submit", "Projects");
            }

            var cmd = new OnboardProducerTermsAcceptance(id);

            return View(cmd);
        }

        /// <summary>Termses the acceptance.</summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> TermsAcceptance(OnboardProducerTermsAcceptance cmd)
        {
            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.ParticipantsTerms, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Labels.Projects, Url.Action("SubmittedList", "Projects", new { Area = "" })),
                new BreadcrumbItemHelper(Labels.Subscription, Url.Action("Submit", "Projects", new { Area = "" }))
            });

            #endregion

            if (this.EditionDto?.IsProjectSubmitOpen() != true)
            {
                this.StatusMessageToastr(Messages.ProjectSubmissionNotOpen, Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);
                return RedirectToAction("Index", "Projects");
            }

            if (this.UserAccessControlDto?.IsProjectSubmissionOrganizationInformationPending() == true
                || this.UserAccessControlDto?.IsProjectSubmissionTermsAcceptancePending() != true)
            {
                return RedirectToAction("Submit", "Projects");
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

            // Check if player submitted the max number of projects
            var firstAttendeeOrganizationCreated = this.UserAccessControlDto.GetFirstAttendeeOrganizationCreated();
            if (firstAttendeeOrganizationCreated != null)
            {
                var projectsCount = this.projectRepo.Count(p => p.SellerAttendeeOrganization.Uid == firstAttendeeOrganizationCreated.Uid
                                                                && !p.IsDeleted);
                var projectMaxCount = this.EditionDto?.AttendeeOrganizationMaxSellProjectsCount ?? 0;
                if (projectsCount >= projectMaxCount)
                {
                    if (cmd.ProjectUid.HasValue)
                    {
                        return RedirectToAction("SendToPlayers", "Projects", new { id = cmd.ProjectUid });
                    }

                    return RedirectToAction("SubmittedList", "Projects");
                }
            }

            return RedirectToAction("Submit", "Projects");
        }

        #endregion

        #region Send to Players

        /// <summary>Sends to players.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> SendToPlayers(Guid? id)
        {
            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.ParticipantsTerms, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Labels.Projects, Url.Action("SubmittedList", "Projects", new { Area = "" })),
                new BreadcrumbItemHelper(Labels.Subscription, Url.Action("Submit", "Projects", new { Area = "" }))
            });

            #endregion

            if (this.EditionDto?.IsProjectSubmitOpen() != true)
            {
                this.StatusMessageToastr(Messages.ProjectSubmissionNotOpen, Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);
                return RedirectToAction("Index", "Projects");
            }

            if (this.UserAccessControlDto?.IsProjectSubmissionOrganizationInformationPending() == true
                || this.UserAccessControlDto?.IsProjectSubmissionTermsAcceptancePending() == true)
            {
                return RedirectToAction("Submit", "Projects", new { id });
            }

            var buyerCompanyWidgetDto = await this.projectRepo.FindSiteBuyerCompanyWidgetDtoByProjectUidAsync(id ?? Guid.Empty);
            if (buyerCompanyWidgetDto == null)
            {
                this.StatusMessageToastr(string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM), Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);
                return RedirectToAction("SubmittedList", "Projects");
            }

            if (buyerCompanyWidgetDto.Project.IsFinished())
            {
                this.StatusMessageToastr(Messages.ProjectIsFinishedCannotBeUpdated, Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);
                return RedirectToAction("SubmittedList", "Projects");
            }

            return View(buyerCompanyWidgetDto);
        }

        /// <summary>Saves the specified identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Save(Guid? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("SubmittedList", "Projects");
            }

            this.StatusMessageToastr(Messages.ProjectSavedButNotSentToPlayers, Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Warning);
            return RedirectToAction("SubmittedDetails", "Projects", new { id });
        }

        #region Shared (project creation and details)

        /// <summary>Shows the buyer company selected widget.</summary>
        /// <param name="projectUid">The project uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowBuyerCompanySelectedWidget(Guid? projectUid)
        {
            var buyerCompanyWidgetDto = await this.projectRepo.FindSiteBuyerCompanyWidgetDtoByProjectUidAsync(projectUid ?? Guid.Empty);
            if (buyerCompanyWidgetDto == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            if (this.UserAccessControlDto?.HasEditionAttendeeOrganization(buyerCompanyWidgetDto.SellerAttendeeOrganizationDto.AttendeeOrganization.Uid) != true)
            {
                return Json(new { status = "error", message = Texts.ForbiddenErrorMessage }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/BuyerCompanySelectedWidget", buyerCompanyWidgetDto), divIdOrClass = "#ProjectBuyerCompanySelectedWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>Shows the project match buyer company widget.</summary>
        /// <param name="projectUid">The project uid.</param>
        /// <param name="searchKeywords">The search keywords.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowProjectMatchBuyerCompanyWidget(Guid? projectUid, string searchKeywords, int page = 1, int pageSize = 10)
        {
            var interestWidgetDto = await this.projectRepo.FindSiteInterestWidgetDtoByProjectUidAsync(projectUid ?? Guid.Empty);
            if (interestWidgetDto == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            if (this.UserAccessControlDto?.HasEditionAttendeeOrganization(interestWidgetDto.SellerAttendeeOrganizationDto.AttendeeOrganization.Uid) != true)
            {
                return Json(new { status = "error", message = Texts.ForbiddenErrorMessage }, JsonRequestBehavior.AllowGet);
            }

            var matchAttendeeOrganizationDtos = await this.attendeeOrganizationRepo.FindAllDtoByMatchingProjectBuyerAsync(this.EditionDto.Id, interestWidgetDto, searchKeywords, page, pageSize);

            ViewBag.ShowProjectMatchBuyerCompanySearch = $"&projectUid={projectUid}&pageSize={pageSize}";
            ViewBag.SearchKeywords = searchKeywords;

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/ProjectMatchBuyerCompanyWidget", matchAttendeeOrganizationDtos), divIdOrClass = "#ProjectMatchBuyerCompanyWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>Shows the project all buyer company widget.</summary>
        /// <param name="projectUid">The project uid.</param>
        /// <param name="searchKeywords">The search keywords.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowProjectAllBuyerCompanyWidget(Guid? projectUid, string searchKeywords, int page = 1, int pageSize = 10)
        {
            var interestWidgetDto = await this.projectRepo.FindSiteInterestWidgetDtoByProjectUidAsync(projectUid ?? Guid.Empty);
            if (interestWidgetDto == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            if (this.UserAccessControlDto?.HasEditionAttendeeOrganization(interestWidgetDto.SellerAttendeeOrganizationDto.AttendeeOrganization.Uid) != true)
            {
                return Json(new { status = "error", message = Texts.ForbiddenErrorMessage }, JsonRequestBehavior.AllowGet);
            }

            var attendeeOrganizationDtos = await this.attendeeOrganizationRepo.FindAllDtoByProjectBuyerAsync(this.EditionDto.Id, interestWidgetDto, searchKeywords, page, pageSize);

            ViewBag.ShowProjectAllBuyerCompanySearch = $"&projectUid={projectUid}&pageSize={pageSize}";
            ViewBag.SearchKeywords = searchKeywords;

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/ProjectAllBuyerCompanyWidget", attendeeOrganizationDtos), divIdOrClass = "#ProjectAllBuyerCompanyWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>Creates the buyer evaluation.</summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> CreateBuyerEvaluation(CreateProjectBuyerEvaluation cmd)
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

            return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Project, Labels.UpdatedM) });
        }

        /// <summary>Deletes the buyer evaluation.</summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> DeleteBuyerEvaluation(DeleteProjectBuyerEvaluation cmd)
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

            return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Project, Labels.UpdatedM) });
        }

        #endregion

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
                return Json(new { status = "success", redirectOnly = Url.Action("SubmittedDetails", "Projects", new { id = cmd.ProjectUid }) });
            }

            return Json(new { status = "success", message = successMessage });
        }

        #endregion

        #endregion

        #region Executive Audiovisual

        ///// <summary>Reviews this instance.</summary>
        ///// <returns></returns>
        //[AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.ExecutiveAudiovisual)]
        //public ActionResult Review()
        //{
        //    #region Breadcrumb

        //    ViewBag.Breadcrumb = new BreadcrumbHelper("Projects for review", new List<BreadcrumbItemHelper> {
        //        new BreadcrumbItemHelper("Projects", Url.Action("Index", "Projects", new { Area = "Player" }))
        //    });

        //    #endregion

        //    return View();
        //}

        #region Evaluation List

        /// <summary>Evaluations the list.</summary>
        /// <returns></returns>
        [AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.ExecutiveAudiovisual)]
        [HttpGet]
        public async Task<ActionResult> EvaluationList()
        {
            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.AudiovisualProjects, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Labels.Projects, Url.Action("EvaluationList", "Projects", new { Area = "" })),
            });

            #endregion

            var projectInterests = await this.interestRepo.FindAllDtosAsync();

            return View(projectInterests);
        }

        /// <summary>Shows the evaluation list widget.</summary>
        /// <param name="searchKeywords">The search keywords.</param>
        /// <param name="interestUid">The interest uid.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        [AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.ExecutiveAudiovisual)]
        [HttpGet]
        public async Task<ActionResult> ShowEvaluationListWidget(string searchKeywords, Guid? interestUid, int page = 1, int pageSize = 10)
        {
            var projects = await this.projectRepo.FindAllDtosToEvaluateAsync(
                this.UserAccessControlDto?.Collaborator?.Uid ?? Guid.Empty, 
                searchKeywords, 
                interestUid, 
                page, 
                pageSize);
            if (projects == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            ViewBag.SubmittedListBuyerEvaluation = $"&pageSize={pageSize}";
            ViewBag.SearchKeywords = searchKeywords;

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/EvaluationListWidget", projects), divIdOrClass = "#ProjectBuyerEvaluationWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #region Update

        /// <summary>Approves the specified identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Approve(Guid? id)
        {
            var result = new AppValidationResult();

            try
            {
                if (!ModelState.IsValid)
                {
                    throw new DomainException(Messages.CorrectFormValues);
                }

                var projectDto = await this.projectRepo.FindSiteDetailsDtoByProjectUidAsync(id ?? Guid.Empty);
                if (projectDto == null)
                {
                    return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
                }

                var projectBuyerEvaluation = projectDto.Project.ProjectBuyerEvaluations
                                                    .FirstOrDefault(p => p.BuyerAttendeeOrganization.AttendeeOrganizationCollaborators
                                                                            .Any(ao => ao.AttendeeCollaborator.Collaborator.Uid == this.UserAccessControlDto.Collaborator.Uid)); //TODO: Substituir pela empresa selecionada no modal

                var projectBuyerEvaluationData = new ProjectBuyerEvaluationData()
                {
                    Uid = projectBuyerEvaluation.Uid,
                    BuyerAttendeeOrganizationId = projectBuyerEvaluation.BuyerAttendeeOrganizationId,
                    BuyerEvaluationUserId = this.UserAccessControlDto.User.Id,
                    EvaluationDate = DateTime.Now,
                    ProjectEvaluationStatusId = 2,
                };

                var cmd = new ApproveProjectBuyerEvaluation(projectBuyerEvaluationData);
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

                //return RedirectToAction("SubmittedListBuyerEvaluation", "Projects");
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

            return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Project, Labels.UpdatedM) });
        }

        /// <summary>Refuses the specified identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <param name="reason">The reason.</param>
        /// <returns></returns>
        public async Task<ActionResult> Refuse(Guid? id, string reason)
        {
            var result = new AppValidationResult();

            try
            {
                if (!ModelState.IsValid)
                {
                    throw new DomainException(Messages.CorrectFormValues);
                }

                var projectDto = await this.projectRepo.FindSiteDetailsDtoByProjectUidAsync(id ?? Guid.Empty);
                if (projectDto == null)
                {
                    return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
                }

                var projectBuyerEvaluation = projectDto.Project.ProjectBuyerEvaluations
                                                    .FirstOrDefault(p => p.BuyerAttendeeOrganization.AttendeeOrganizationCollaborators
                                                                            .Any(ao => ao.AttendeeCollaborator.Collaborator.Uid == this.UserAccessControlDto.Collaborator.Uid)); //TODO: Substituir pela empresa selecionada no modal

                var projectBuyerEvaluationData = new ProjectBuyerEvaluationData()
                {
                    Uid = projectBuyerEvaluation.Uid,
                    BuyerAttendeeOrganizationId = projectBuyerEvaluation.BuyerAttendeeOrganizationId,
                    BuyerEvaluationUserId = this.UserAccessControlDto.User.Id,
                    EvaluationDate = DateTime.Now,
                    ProjectEvaluationStatusId = 3,
                    Reason = reason
                };

                var cmd = new RejectProjectBuyerEvaluation(projectBuyerEvaluationData);
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

                //return RedirectToAction("SubmittedListBuyerEvaluation", "Projects");
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

            return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Project, Labels.UpdatedM) });
        }

        #endregion

        #endregion

        #endregion

        internal bool HasEdition(Guid idAtendeeOrganization)
        {
            if (this.UserAccessControlDto?.HasCollaboratorType(Constants.CollaboratorType.ExecutiveAudiovisual) != true)
            {
                if (this.UserAccessControlDto?.HasEditionAttendeeOrganization(idAtendeeOrganization) != true)
                {
                    return false;
                }
                return true;
            }
            else
            {
                foreach (var item in this.UserAccessControlDto?.EditionAttendeeCollaborator.AttendeeOrganizationCollaborators)
                {
                    if (this.UserAccessControlDto?.HasEditionAttendeeOrganization(item.AttendeeOrganization.Uid) == true)
                    {
                        return true;
                    }
                }
                return false;
            }
        }
    }
}