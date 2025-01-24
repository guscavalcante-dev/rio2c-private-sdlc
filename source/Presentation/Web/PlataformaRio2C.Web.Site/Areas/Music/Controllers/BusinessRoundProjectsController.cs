// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Daniel Giese Rodrigues
// Created          : 01-08-2025
//
// Last Modified By : Renan Valentim
// Last Modified On : 01-13-2025
// ***********************************************************************
// <copyright file="BusinessRoundProjectsController.cs" company="Softo">
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
using PlataformaRio2C.Domain.Interfaces.Repositories.Music.Projects;

namespace PlataformaRio2C.Web.Site.Areas.Music.Controllers
{
    /// <summary>BusinessRoundProjectsController</summary>
    [AjaxAuthorize(Order = 1)]
    [AuthorizeCollaboratorType(Order = 2, Types = Constants.CollaboratorType.PlayerExecutiveMusic + "," + Constants.CollaboratorType.Industry + "," + Constants.CollaboratorType.Creator)]
    public class BusinessRoundProjectsController : BaseController
    {
        private readonly IMusicBusinessRoundProjectRepository musicBusinessRoundProjectRepo;
        private readonly IInterestRepository interestRepo;
        private readonly IActivityRepository activityRepo;
        private readonly ITargetAudienceRepository targetAudienceRepo;
        private readonly IAttendeeOrganizationRepository attendeeOrganizationRepo;
        private readonly IProjectEvaluationRefuseReasonRepository projectEvaluationRefuseReasonRepo;
        private readonly IProjectEvaluationStatusRepository evaluationStatusRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessRoundProjectsController" /> class.
        /// </summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="identityController">The identity controller.</param>
        /// <param name="musicBusinessRoundProjectRepository">The music business round project repository.</param>
        /// <param name="interestRepository">The interest repository.</param>
        /// <param name="activityRepository">The activity repository.</param>
        /// <param name="targetAudienceRepository">The target audience repository.</param>
        /// <param name="attendeeOrganizationRepository">The attendee organization repository.</param>
        /// <param name="projectEvaluationRefuseReasonRepo">The project evaluation refuse reason repo.</param>
        /// <param name="evaluationStatusRepository">The project evaluation status repository.</param>
        public BusinessRoundProjectsController(
            IMediator commandBus,
            IdentityAutenticationService identityController,
            IMusicBusinessRoundProjectRepository musicBusinessRoundProjectRepository,
            IInterestRepository interestRepository,
            IActivityRepository activityRepository,
            ITargetAudienceRepository targetAudienceRepository,
            IAttendeeOrganizationRepository attendeeOrganizationRepository,
            IProjectEvaluationRefuseReasonRepository projectEvaluationRefuseReasonRepo,
            IProjectEvaluationStatusRepository evaluationStatusRepository)
            : base(commandBus, identityController)
        {
            musicBusinessRoundProjectRepo = musicBusinessRoundProjectRepository;
            interestRepo = interestRepository;
            activityRepo = activityRepository;
            targetAudienceRepo = targetAudienceRepository;
            attendeeOrganizationRepo = attendeeOrganizationRepository;
            this.projectEvaluationRefuseReasonRepo = projectEvaluationRefuseReasonRepo;
            this.evaluationStatusRepository = evaluationStatusRepository;
        }

        /// <summary>Indexes this instance.</summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.AudiovisualProjects, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Labels.Projects, Url.Action("Index", "BusinessRoundProjects", new { Area = "Music" }))
            });

            #endregion

            return View();
        }

        #region Seller (Industry or Creator)

        #region Submitted List

        /// <summary>Submitteds the list.</summary>
        /// <returns></returns>
        [AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.Industry + "," + Constants.CollaboratorType.Creator)]
        public async Task<ActionResult> SubmittedList()
        {
            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper($"{Labels.MusicProjects} - {Labels.BusinessRound}", new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper($"{Labels.Projects} {Labels.BusinessRound}", Url.Action("Index", "BusinessRoundProjects", new { Area = "Music" })),
            });

            #endregion

            if (EditionDto?.IsMusicBusinessRoundProjectSubmitStarted() != true)
            {
                return RedirectToAction("Index", "BusinessRoundProjects", new { Area = "Music" });
            }

            var projects = await musicBusinessRoundProjectRepo.FindAllMusicBusinessRoundProjectDtosToSellAsync(UserAccessControlDto?.EditionAttendeeCollaborator?.Uid ?? Guid.Empty);

            // Create fake projects in the list
            var projectMaxCount = EditionDto?.MusicBusinessRoundsMaximumProjectSubmissionsByCompany ?? 0;
            if (projects.Count < projectMaxCount)
            {
                var initialProject = projects.Count + 1;

                for (int i = initialProject; i < projectMaxCount + 1; i++)
                {
                    projects.Add(new MusicBusinessRoundProjectDto
                    {
                        IsFakeProject = true,

                        MusicBusinessRoundProjectExpectationsForMeetingDtos = new List<MusicBusinessRoundProjectExpectationsForMeetingDto>
                        {
                            new MusicBusinessRoundProjectExpectationsForMeetingDto
                            {
                                Value = Labels.Project + " " + i, //ProjectTitle = new ProjectTitle(Labels.Project + " " + i, new Language("", ViewBag.UserInterfaceLanguage), 0),
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

        ///// <summary>Submitteds the details.</summary>
        ///// <param name="id">The identifier.</param>
        ///// <returns></returns>
        //[AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.Industry + "," + Constants.CollaboratorType.Creator)]
        //public async Task<ActionResult> SubmittedDetails(Guid? id)
        //{
        //    if (EditionDto?.IsMusicProjectSubmitStarted() != true)
        //    {
        //        return RedirectToAction("Index", "BusinessRoundProjects", new { Area = "Music" });
        //    }

        //    var projectDto = await projectRepo.FindSiteDetailsDtoByProjectUidAsync(id ?? Guid.Empty, EditionDto.Id);
        //    if (projectDto == null)
        //    {
        //        this.StatusMessageToastr(string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()), Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);
        //        return RedirectToAction("Index", "BusinessRoundProjects", new { Area = "Music" });
        //    }

        //    if (UserAccessControlDto?.HasEditionAttendeeOrganization(projectDto.SellerAttendeeOrganizationDto.AttendeeOrganization.Uid) != true) // Is seller
        //    {
        //        this.StatusMessageToastr(Texts.ForbiddenErrorMessage, Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);
        //        return RedirectToAction("Index", "BusinessRoundProjects", new { Area = "Music" });
        //    }

        //    #region Breadcrumb

        //    ViewBag.Breadcrumb = new BreadcrumbHelper($"{Labels.MusicProjects} - {Labels.BusinessRound}", new List<BreadcrumbItemHelper> {
        //        new BreadcrumbItemHelper(Labels.Projects, Url.Action("Index", "BusinessRoundProjects", new { Area = "Music" })),
        //        new BreadcrumbItemHelper(projectDto.GetTitleDtoByLanguageCode(UserInterfaceLanguage)?.ProjectTitle?.Value ?? Labels.Project, Url.Action("SubmittedDetails", "BusinessRoundProjects", new { id }))
        //    });

        //    #endregion

        //    return View(projectDto);
        //}

        //#region Main Information Widget

        ///// <summary>Shows the main information widget.</summary>
        ///// <param name="projectUid">The project uid.</param>
        ///// <returns></returns>
        //[HttpGet]
        //public async Task<ActionResult> ShowMainInformationWidget(Guid? projectUid)
        //{
        //    var mainInformationWidgetDto = await projectRepo.FindSiteMainInformationWidgetDtoByProjectUidAsync(projectUid ?? Guid.Empty);
        //    if (mainInformationWidgetDto == null)
        //    {
        //        return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
        //    }

        //    if (UserAccessControlDto?.HasEditionAttendeeOrganization(mainInformationWidgetDto.SellerAttendeeOrganizationDto.AttendeeOrganization.Uid) != true                                                                    // Seller
        //        && (UserAccessControlDto?.HasAnyEditionAttendeeOrganization(mainInformationWidgetDto.ProjectBuyerEvaluationDtos?.Select(pbed => pbed.BuyerAttendeeOrganizationDto.AttendeeOrganization.Uid)?.ToList()) != true   // Buyer with project finished
        //            || mainInformationWidgetDto.Project?.IsFinished() != true))
        //    {
        //        return Json(new { status = "error", message = Texts.ForbiddenErrorMessage }, JsonRequestBehavior.AllowGet);
        //    }

        //    return Json(new
        //    {
        //        status = "success",
        //        pages = new List<dynamic>
        //        {
        //            new { page = this.RenderRazorViewToString("Widgets/MainInformationWidget", mainInformationWidgetDto), divIdOrClass = "#ProjectMainInformationWidget" },
        //        }
        //    }, JsonRequestBehavior.AllowGet);
        //}

        //#region Update

        ///// <summary>Shows the update main information modal.</summary>
        ///// <param name="projectUid">The project uid.</param>
        ///// <returns></returns>
        //[HttpGet]
        //public async Task<ActionResult> ShowUpdateMainInformationModal(Guid? projectUid)
        //{
        //    UpdateProjectMainInformation cmd;

        //    try
        //    {
        //        var mainInformationWidgetDto = await projectRepo.FindSiteMainInformationWidgetDtoByProjectUidAsync(projectUid ?? Guid.Empty);
        //        if (mainInformationWidgetDto == null)
        //        {
        //            throw new DomainException(string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()));
        //        }

        //        if (UserAccessControlDto?.HasEditionAttendeeOrganization(mainInformationWidgetDto.SellerAttendeeOrganizationDto.AttendeeOrganization.Uid) != true)
        //        {
        //            throw new DomainException(Texts.ForbiddenErrorMessage);
        //        }

        //        if (EditionDto?.IsMusicPitchingProjectSubmitOpen() != true)
        //        {
        //            throw new DomainException(Messages.ProjectSubmissionNotOpen);
        //        }

        //        if (mainInformationWidgetDto.Project.IsFinished())
        //        {
        //            throw new DomainException(Messages.ProjectIsFinishedCannotBeUpdated);
        //        }

        //        cmd = new UpdateProjectMainInformation(
        //            mainInformationWidgetDto,
        //            await CommandBus.Send(new FindAllLanguagesDtosAsync(UserInterfaceLanguage)),
        //            true,
        //            false,
        //            false,
        //            UserInterfaceLanguage,
        //            ProjectModality.BusinessRound.Uid
        //        );
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

        ///// <summary>Updates the main information.</summary>
        ///// <param name="cmd">The command.</param>
        ///// <returns></returns>
        //[HttpPost]
        //public async Task<ActionResult> UpdateMainInformation(UpdateProjectMainInformation cmd)
        //{
        //    var result = new AppValidationResult();

        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            throw new DomainException(Messages.CorrectFormValues);
        //        }

        //        cmd.UpdatePreSendProperties(
        //            UserAccessControlDto.GetFirstAttendeeOrganizationCreated()?.Uid,
        //            ProjectType.Music.Uid,
        //            UserAccessControlDto.User.Id,
        //            UserAccessControlDto.User.Uid,
        //            EditionDto.Id,
        //            EditionDto.Uid,
        //            UserInterfaceLanguage,
        //            ProjectModality.BusinessRound.Uid
        //        );
        //        result = await CommandBus.Send(cmd);
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

        //        //cmd.UpdateModelsAndLists(
        //        //    await this.interestRepo.FindAllGroupedByInterestGroupsAsync());

        //        return Json(new
        //        {
        //            status = "error",
        //            message = toastrError?.Message ?? ex.GetInnerMessage(),
        //            pages = new List<dynamic>
        //            {
        //                new { page = this.RenderRazorViewToString("Modals/UpdateMainInformationForm", cmd), divIdOrClass = "#form-container" },
        //            }
        //        }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        //        return Json(new { status = "error", message = Messages.WeFoundAndError, }, JsonRequestBehavior.AllowGet);
        //    }

        //    return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Project, Labels.UpdatedM) });
        //}

        //#endregion

        //#endregion

        //#region Interest Widget

        ///// <summary>Shows the interest widget.</summary>
        ///// <param name="projectUid">The project uid.</param>
        ///// <returns></returns>
        //[HttpGet]
        //public async Task<ActionResult> ShowInterestWidget(Guid? projectUid)
        //{
        //    var interestWidgetDto = await projectRepo.FindSiteInterestWidgetDtoByProjectUidAsync(projectUid ?? Guid.Empty);
        //    if (interestWidgetDto == null)
        //    {
        //        return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
        //    }

        //    if (UserAccessControlDto?.HasEditionAttendeeOrganization(interestWidgetDto.SellerAttendeeOrganizationDto.AttendeeOrganization.Uid) != true                                                                    // Seller
        //        && (UserAccessControlDto?.HasAnyEditionAttendeeOrganization(interestWidgetDto.ProjectBuyerEvaluationDtos?.Select(pbed => pbed.BuyerAttendeeOrganizationDto.AttendeeOrganization.Uid)?.ToList()) != true   // Buyer with project finished
        //            || interestWidgetDto.Project?.IsFinished() != true))
        //    {
        //        return Json(new { status = "error", message = Texts.ForbiddenErrorMessage }, JsonRequestBehavior.AllowGet);
        //    }

        //    ViewBag.GroupedInterests = await interestRepo.FindAllByProjectTypeIdAndGroupedByInterestGroupAsync(ProjectType.Music.Id);
        //    ViewBag.TargetAudiences = await targetAudienceRepo.FindAllByProjectTypeIdAsync(ProjectType.Music.Id);

        //    return Json(new
        //    {
        //        status = "success",
        //        pages = new List<dynamic>
        //        {
        //            new { page = this.RenderRazorViewToString("Widgets/InterestWidget", interestWidgetDto), divIdOrClass = "#ProjectInterestWidget" },
        //        }
        //    }, JsonRequestBehavior.AllowGet);
        //}

        //#region Update

        ///// <summary>Shows the update interest modal.</summary>
        ///// <param name="projectUid">The project uid.</param>
        ///// <returns></returns>
        //[HttpGet]
        //public async Task<ActionResult> ShowUpdateInterestModal(Guid? projectUid)
        //{
        //    UpdateProjectInterests cmd;

        //    try
        //    {
        //        var interestWidgetDto = await projectRepo.FindSiteInterestWidgetDtoByProjectUidAsync(projectUid ?? Guid.Empty);
        //        if (interestWidgetDto == null)
        //        {
        //            throw new DomainException(string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()));
        //        }

        //        if (UserAccessControlDto?.HasEditionAttendeeOrganization(interestWidgetDto.SellerAttendeeOrganizationDto.AttendeeOrganization.Uid) != true)
        //        {
        //            throw new DomainException(Texts.ForbiddenErrorMessage);
        //        }

        //        if (EditionDto?.IsMusicPitchingProjectSubmitOpen() != true)
        //        {
        //            throw new DomainException(Messages.ProjectSubmissionNotOpen);
        //        }

        //        if (interestWidgetDto.Project.IsFinished())
        //        {
        //            throw new DomainException(Messages.ProjectIsFinishedCannotBeUpdated);
        //        }

        //        cmd = new UpdateProjectInterests(
        //            interestWidgetDto,
        //            await interestRepo.FindAllDtosbyProjectTypeIdAsync(ProjectType.Music.Id),
        //            await targetAudienceRepo.FindAllByProjectTypeIdAsync(ProjectType.Music.Id));
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
        //            new { page = this.RenderRazorViewToString("Modals/UpdateInterestModal", cmd), divIdOrClass = "#GlobalModalContainer" },
        //        }
        //    }, JsonRequestBehavior.AllowGet);
        //}

        ///// <summary>Updates the interests.</summary>
        ///// <param name="cmd">The command.</param>
        ///// <returns></returns>
        //[HttpPost]
        //public async Task<ActionResult> UpdateInterests(UpdateProjectInterests cmd)
        //{
        //    var result = new AppValidationResult();

        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            throw new DomainException(Messages.CorrectFormValues);
        //        }

        //        cmd.UpdatePreSendProperties(
        //            UserAccessControlDto.User.Id,
        //            UserAccessControlDto.User.Uid,
        //            EditionDto.Id,
        //            EditionDto.Uid,
        //            UserInterfaceLanguage);
        //        result = await CommandBus.Send(cmd);
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

        //        cmd.UpdateDropdownProperties(
        //            await targetAudienceRepo.FindAllByProjectTypeIdAsync(ProjectType.Music.Id));

        //        return Json(new
        //        {
        //            status = "error",
        //            message = toastrError?.Message ?? ex.GetInnerMessage(),
        //            pages = new List<dynamic>
        //            {
        //                new { page = this.RenderRazorViewToString("Modals/UpdateInterestForm", cmd), divIdOrClass = "#form-container" },
        //            }
        //        }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        //        return Json(new { status = "error", message = Messages.WeFoundAndError, }, JsonRequestBehavior.AllowGet);
        //    }

        //    return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Project, Labels.UpdatedM) });
        //}

        //#endregion

        //#endregion

        //#region Links Widget

        ///// <summary>Shows the links widget.</summary>
        ///// <param name="projectUid">The project uid.</param>
        ///// <returns></returns>
        //[HttpGet]
        //public async Task<ActionResult> ShowLinksWidget(Guid? projectUid)
        //{
        //    var linksWidgetDto = await projectRepo.FindSiteLinksWidgetDtoByProjectUidAsync(projectUid ?? Guid.Empty);
        //    if (linksWidgetDto == null)
        //    {
        //        return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
        //    }

        //    if (UserAccessControlDto?.HasEditionAttendeeOrganization(linksWidgetDto.SellerAttendeeOrganizationDto.AttendeeOrganization.Uid) != true                                                                    // Seller
        //        && (UserAccessControlDto?.HasAnyEditionAttendeeOrganization(linksWidgetDto.ProjectBuyerEvaluationDtos?.Select(pbed => pbed.BuyerAttendeeOrganizationDto.AttendeeOrganization.Uid)?.ToList()) != true   // Buyer with project finished
        //            || linksWidgetDto.Project?.IsFinished() != true))
        //    {
        //        return Json(new { status = "error", message = Texts.ForbiddenErrorMessage }, JsonRequestBehavior.AllowGet);
        //    }

        //    return Json(new
        //    {
        //        status = "success",
        //        pages = new List<dynamic>
        //        {
        //            new { page = this.RenderRazorViewToString("Widgets/LinksWidget", linksWidgetDto), divIdOrClass = "#ProjectLinksWidget" },
        //        }
        //    }, JsonRequestBehavior.AllowGet);
        //}

        //#region Update

        ///// <summary>Shows the update links modal.</summary>
        ///// <param name="projectUid">The project uid.</param>
        ///// <returns></returns>
        //[HttpGet]
        //public async Task<ActionResult> ShowUpdateLinksModal(Guid? projectUid)
        //{
        //    UpdateProjectLinks cmd;

        //    try
        //    {
        //        var linksWidgetDto = await projectRepo.FindSiteLinksWidgetDtoByProjectUidAsync(projectUid ?? Guid.Empty);
        //        if (linksWidgetDto == null)
        //        {
        //            throw new DomainException(string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()));
        //        }

        //        if (UserAccessControlDto?.HasEditionAttendeeOrganization(linksWidgetDto.SellerAttendeeOrganizationDto.AttendeeOrganization.Uid) != true)
        //        {
        //            throw new DomainException(Texts.ForbiddenErrorMessage);
        //        }

        //        if (EditionDto?.IsMusicPitchingProjectSubmitOpen() != true)
        //        {
        //            throw new DomainException(Messages.ProjectSubmissionNotOpen);
        //        }

        //        if (linksWidgetDto.Project.IsFinished())
        //        {
        //            throw new DomainException(Messages.ProjectIsFinishedCannotBeUpdated);
        //        }

        //        cmd = new UpdateProjectLinks(linksWidgetDto);
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
        //            new { page = this.RenderRazorViewToString("Modals/UpdateLinksModal", cmd), divIdOrClass = "#GlobalModalContainer" },
        //        }
        //    }, JsonRequestBehavior.AllowGet);
        //}

        ///// <summary>Updates the links.</summary>
        ///// <param name="cmd">The command.</param>
        ///// <returns></returns>
        //[HttpPost]
        //public async Task<ActionResult> UpdateLinks(UpdateProjectLinks cmd)
        //{
        //    var result = new AppValidationResult();

        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            throw new DomainException(Messages.CorrectFormValues);
        //        }

        //        cmd.UpdatePreSendProperties(
        //            UserAccessControlDto.User.Id,
        //            UserAccessControlDto.User.Uid,
        //            EditionDto.Id,
        //            EditionDto.Uid,
        //            UserInterfaceLanguage);
        //        result = await CommandBus.Send(cmd);
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

        //        //cmd.UpdateModelsAndLists(
        //        //    await this.interestRepo.FindAllGroupedByInterestGroupsAsync());

        //        return Json(new
        //        {
        //            status = "error",
        //            message = toastrError?.Message ?? ex.GetInnerMessage(),
        //            pages = new List<dynamic>
        //            {
        //                new { page = this.RenderRazorViewToString("Modals/UpdateLinksForm", cmd), divIdOrClass = "#form-container" },
        //            }
        //        }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        //        return Json(new { status = "error", message = Messages.WeFoundAndError, }, JsonRequestBehavior.AllowGet);
        //    }

        //    return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Project, Labels.UpdatedM) });
        //}

        //#endregion

        //#endregion

        //#region Buyer Companies Widget

        ///// <summary>Shows the buyer company widget.</summary>
        ///// <param name="projectUid">The project uid.</param>
        ///// <returns></returns>
        //[HttpGet]
        //public async Task<ActionResult> ShowBuyerCompanyWidget(Guid? projectUid)
        //{
        //    var buyerCompanyWidgetDto = await projectRepo.FindSiteBuyerCompanyWidgetDtoByProjectUidAsync(projectUid ?? Guid.Empty);
        //    if (buyerCompanyWidgetDto == null)
        //    {
        //        return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
        //    }

        //    if (UserAccessControlDto?.HasEditionAttendeeOrganization(buyerCompanyWidgetDto.SellerAttendeeOrganizationDto.AttendeeOrganization.Uid) != true) // Seller
        //    {
        //        return Json(new { status = "error", message = Texts.ForbiddenErrorMessage }, JsonRequestBehavior.AllowGet);
        //    }

        //    return Json(new
        //    {
        //        status = "success",
        //        pages = new List<dynamic>
        //        {
        //            new { page = this.RenderRazorViewToString("Widgets/BuyerCompanyWidget", buyerCompanyWidgetDto), divIdOrClass = "#ProjectBuyercompanyWidget" },
        //        }
        //    }, JsonRequestBehavior.AllowGet);
        //}

        //#region Update

        ///// <summary>Shows the update buyer company modal.</summary>
        ///// <param name="projectUid">The project uid.</param>
        ///// <returns></returns>
        //[HttpGet]
        //public async Task<ActionResult> ShowUpdateBuyerCompanyModal(Guid? projectUid)
        //{
        //    ProjectDto buyerCompanyWidgetDto = null;

        //    try
        //    {
        //        buyerCompanyWidgetDto = await projectRepo.FindSiteBuyerCompanyWidgetDtoByProjectUidAsync(projectUid ?? Guid.Empty);
        //        if (buyerCompanyWidgetDto == null)
        //        {
        //            return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
        //        }

        //        if (UserAccessControlDto?.HasEditionAttendeeOrganization(buyerCompanyWidgetDto.SellerAttendeeOrganizationDto.AttendeeOrganization.Uid) != true)
        //        {
        //            throw new DomainException(Texts.ForbiddenErrorMessage);
        //        }

        //        if (EditionDto?.IsMusicPitchingProjectSubmitOpen() != true)
        //        {
        //            throw new DomainException(Messages.ProjectSubmissionNotOpen);
        //        }

        //        if (buyerCompanyWidgetDto.Project.IsFinished())
        //        {
        //            throw new DomainException(Messages.ProjectIsFinishedCannotBeUpdated);
        //        }
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
        //            new { page = this.RenderRazorViewToString("Modals/UpdateBuyerCompanyModal", buyerCompanyWidgetDto), divIdOrClass = "#GlobalModalContainer" },
        //        }
        //    }, JsonRequestBehavior.AllowGet);
        //}

        //#endregion

        //#endregion

        #endregion

        #region Submit

        /// <summary>Submits the specified identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.Industry + "," + Constants.CollaboratorType.Creator)]
        public async Task<ActionResult> Submit(Guid? id)
        {
            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.ProjectInfo, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper($"{Labels.Projects} {Labels.BusinessRound}", Url.Action("Index", "BusinessRoundProjects", new { Area = "Music" })),
                new BreadcrumbItemHelper(Labels.Subscription, Url.Action("Submit", "BusinessRoundProjects", new { Area = "Music" }))
            });

            #endregion

            if (EditionDto?.IsMusicBusinessRoundProjectSubmitOpen() != true)
            {
                this.StatusMessageToastr(Messages.ProjectSubmissionNotOpen, Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);
                return RedirectToAction("Index", "BusinessRoundProjects", new { Area = "Music" });
            }

            //if (UserAccessControlDto?.IsMusicProjectSubmissionOrganizationInformationPending() == true)
            //{
            //    return RedirectToAction("CompanyInfo", "BusinessRoundProjects", new { Area = "Music" });
            //}

            if (UserAccessControlDto?.IsMusicProducerBusinessRoundTermsAcceptanceDatePending() == true)
            {
                return RedirectToAction("TermsAcceptance", "BusinessRoundProjects", new { Area = "Music", id });
            }

            // Check if player submitted the max number of projects
            var editionAttendeeCollaborator = UserAccessControlDto.EditionAttendeeCollaborator;
            if (editionAttendeeCollaborator != null)
            {
                var projectsCount = GetSellerMusicBusinessRoundProjectsCount(UserAccessControlDto.EditionAttendeeCollaborator.Uid);
                var projectMaxCount = EditionDto?.MusicBusinessRoundsMaximumProjectSubmissionsByCompany ?? 0;
                if (projectsCount >= projectMaxCount)
                {
                    this.StatusMessageToastr(Messages.YouReachedProjectsLimit, Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);
                    return RedirectToAction("Index", "BusinessRoundProjects", new { Area = "Music" });
                }
            }
           
            // Duplicate project
            ProjectDto projectDto = null;
            if (id.HasValue)
            {
                //TODO: Enable the project duplication into RIO2CMY-1339 task
                //projectDto = await musicBusinessRoundProjectRepo.FindSiteDuplicateDtoByProjectUidAsync(id.Value);
                //if (projectDto != null)
                //{
                //    if (UserAccessControlDto?.HasEditionAttendeeOrganization(projectDto.SellerAttendeeOrganizationDto.AttendeeOrganization.Uid) != true)
                //    {
                //        this.StatusMessageToastr(Texts.ForbiddenErrorMessage, Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);
                //        return RedirectToAction("Index", "BusinessRoundProjects", new { Area = "Music" });
                //    }
                //}
            }

            var cmd = new CreateAudiovisualBusinessRoundProject(
                projectDto,
                await CommandBus.Send(new FindAllLanguagesDtosAsync(UserInterfaceLanguage)),
                await targetAudienceRepo.FindAllByProjectTypeIdAsync(ProjectType.Music.Id),
                await interestRepo.FindAllDtosbyProjectTypeIdAsync(ProjectType.Music.Id),
                true,
                false,
                false,
                UserInterfaceLanguage
            );

            return View(cmd);
        }

        #endregion

        //#region Producer Info

        ///// <summary>Companies the information.</summary>
        ///// <returns></returns>
        //[HttpGet]
        //public async Task<ActionResult> CompanyInfo()
        //{
        //    #region Breadcrumb

        //    ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.CompanyInfo, new List<BreadcrumbItemHelper> {
        //        new BreadcrumbItemHelper(Labels.Projects, Url.Action("Index", "BusinessRoundProjects", new { Area = "Music" })),
        //        new BreadcrumbItemHelper(Labels.Subscription, Url.Action("Submit", "BusinessRoundProjects", new { Area = "Music" }))
        //    });

        //    #endregion

        //    if (this.EditionDto?.IsMusicBusinessRoundProjectSubmitOpen() != true)
        //    {
        //        this.StatusMessageToastr(Messages.ProjectSubmissionNotOpen, Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);
        //        return RedirectToAction("Index", "BusinessRoundProjects", new { Area = "Music" });
        //    }

        //    if (this.UserAccessControlDto?.IsMusicProjectSubmissionOrganizationInformationPending() != true)
        //    {
        //        return RedirectToAction("Submit", "BusinessRoundProjects", new { Area = "Music" });
        //    }

        //    //this.SetViewBags();

        //    var currentOrganization = this.UserAccessControlDto?.EditionAttendeeOrganizations?.FirstOrDefault(eao => !eao.ProjectSubmissionOrganizationDate.HasValue)?.Organization;

        //    var cmd = new OnboardMusicProducerOrganizationData(
        //        currentOrganization != null ? await this.CommandBus.Send(new FindOrganizationDtoByUidAsync(currentOrganization.Uid, this.EditionDto.Id, this.UserInterfaceLanguage)) : null,
        //        await this.CommandBus.Send(new FindAllLanguagesDtosAsync(this.UserInterfaceLanguage)),
        //        await this.CommandBus.Send(new FindAllCountriesBaseDtosAsync(this.UserInterfaceLanguage)),
        //        await this.activityRepo.FindAllByProjectTypeIdAsync(ProjectType.Music.Id),
        //        await this.targetAudienceRepo.FindAllByProjectTypeIdAsync(ProjectType.Music.Id),
        //        true,
        //        true,
        //        true);

        //    return View(cmd);
        //}

        ///// <summary>Companies the information.</summary>
        ///// <param name="cmd">The command.</param>
        ///// <returns></returns>
        //[HttpPost]
        //public async Task<ActionResult> CompanyInfo(OnboardMusicProducerOrganizationData cmd)
        //{
        //    #region Breadcrumb

        //    ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.CompanyInfo, new List<BreadcrumbItemHelper> {
        //        new BreadcrumbItemHelper(Labels.Projects, Url.Action("Index", "BusinessRoundProjects", new { Area = "Music" })),
        //        new BreadcrumbItemHelper(Labels.Subscription, Url.Action("Submit", "BusinessRoundProjects", new { Area = "Music" }))
        //    });

        //    #endregion

        //    if (this.EditionDto?.IsMusicBusinessRoundProjectSubmitOpen() != true)
        //    {
        //        this.StatusMessageToastr(Messages.ProjectSubmissionNotOpen, Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);
        //        return RedirectToAction("Index", "BusinessRoundProjects", new { Area = "Music" });
        //    }

        //    if (this.UserAccessControlDto?.IsMusicProjectSubmissionOrganizationInformationPending() != true)
        //    {
        //        return RedirectToAction("Submit", "BusinessRoundProjects", new { Area = "Music" });
        //    }

        //    var result = new AppValidationResult();

        //    try
        //    {
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

        //        this.StatusMessageToastr(toastrError?.Message ?? ex.GetInnerMessage(), Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);

        //        cmd.UpdateDropdownProperties(
        //            await this.CommandBus.Send(new FindAllCountriesBaseDtosAsync(this.UserInterfaceLanguage)),
        //            await this.targetAudienceRepo.FindAllByProjectTypeIdAsync(ProjectType.Music.Id));

        //        return View(cmd);
        //    }
        //    catch (Exception ex)
        //    {
        //        Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        //        this.StatusMessageToastr(Messages.WeFoundAndError, Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);

        //        cmd.UpdateDropdownProperties(
        //            await this.CommandBus.Send(new FindAllCountriesBaseDtosAsync(this.UserInterfaceLanguage)),
        //            await this.targetAudienceRepo.FindAllByProjectTypeIdAsync(ProjectType.Music.Id));

        //        return View(cmd);
        //    }

        //    this.StatusMessageToastr(string.Format(Messages.EntityActionSuccessfull, Labels.Company, Labels.UpdatedF.ToLowerInvariant()), Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Success);

        //    return RedirectToAction("Submit", "BusinessRoundProjects", new { Area = "Music" });
        //}

        //#endregion

        #region Producer Terms Acceptance

        /// <summary>Termses the acceptance.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> TermsAcceptance(Guid? id)
        {
            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.ParticipantsTerms, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Labels.Projects, Url.Action("Index", "BusinessRoundProjects", new { Area = "Music" })),
                new BreadcrumbItemHelper(Labels.Subscription, Url.Action("Submit", "BusinessRoundProjects", new { Area = "Music" }))
            });

            #endregion

            if (EditionDto?.IsMusicBusinessRoundProjectSubmitOpen() != true)
            {
                this.StatusMessageToastr(Messages.ProjectSubmissionNotOpen, Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);
                return RedirectToAction("Index", "BusinessRoundProjects", new { Area = "Music" });
            }

            if (UserAccessControlDto?.IsMusicProducerBusinessRoundTermsAcceptanceDatePending() != true)
            {
                return RedirectToAction("Submit", "BusinessRoundProjects", new { Area = "Music" });
            }

            var cmd = new OnboardMusicProducerBusinessRoundTermsAcceptance(id);

            return View(cmd);
        }

        /// <summary>Termses the acceptance.</summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> TermsAcceptance(OnboardMusicProducerBusinessRoundTermsAcceptance cmd)
        {
            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.ParticipantsTerms, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Labels.Projects, Url.Action("Index", "BusinessRoundProjects", new { Area = "Music" })),
                new BreadcrumbItemHelper(Labels.Subscription, Url.Action("Submit", "BusinessRoundProjects", new { Area = "Music" }))
            });

            #endregion

            if (EditionDto?.IsMusicBusinessRoundProjectSubmitOpen() != true)
            {
                this.StatusMessageToastr(Messages.ProjectSubmissionNotOpen, Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);
                return RedirectToAction("Index", "BusinessRoundProjects", new { Area = "Music" });
            }

            if (UserAccessControlDto?.IsMusicProducerBusinessRoundTermsAcceptanceDatePending() != true)
            {
                return RedirectToAction("Submit", "BusinessRoundProjects", new { Area = "Music" });
            }

            var result = new AppValidationResult();

            try
            {
                if (!ModelState.IsValid)
                {
                    throw new DomainException(Messages.CorrectFormValues);
                }

                cmd.UpdatePreSendProperties(
                    UserAccessControlDto.Collaborator.Uid,
                    UserAccessControlDto.User.Id,
                    UserAccessControlDto.User.Uid,
                    EditionDto.Id,
                    EditionDto.Uid,
                    UserInterfaceLanguage);
                result = await CommandBus.Send(cmd);
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
            var editionAttendeeCollaborator = UserAccessControlDto.EditionAttendeeCollaborator;
            if (editionAttendeeCollaborator != null)
            {
                var projectsCount = GetSellerMusicBusinessRoundProjectsCount(UserAccessControlDto.EditionAttendeeCollaborator.Uid);
                var projectMaxCount = EditionDto?.MusicBusinessRoundsMaximumProjectSubmissionsByCompany ?? 0;
                if (projectsCount >= projectMaxCount)
                {
                    if (cmd.ProjectUid.HasValue)
                    {
                        return RedirectToAction("SendToPlayers", "BusinessRoundProjects", new { Area = "Music", id = cmd.ProjectUid });
                    }

                    return RedirectToAction("Index", "BusinessRoundProjects", new { Area = "Music" });
                }
            }

            return RedirectToAction("Submit", "BusinessRoundProjects", new { Area = "Music" });
        }

        /// <summary>
        /// Gets the seller music business round projects count.
        /// </summary>
        /// <param name="uid">The uid.</param>
        /// <returns></returns>
        private int GetSellerMusicBusinessRoundProjectsCount(Guid uid)
        {
            return musicBusinessRoundProjectRepo.Count(p => p.SellerAttendeeCollaborator.Uid == uid && !p.IsDeleted);
        }

        #endregion

        #endregion

        #region Buyer (Executive Music)

        #region Evaluation List

        /// <summary>Evaluations the list.</summary>
        /// <param name="searchKeywords">The search keywords.</param>
        /// <param name="interestUid">The interest uid.</param>
        /// <param name="evaluationStatusUid">The evaluation status uid.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        [AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.PlayerExecutiveMusic)]
        [HttpGet]
        public async Task<ActionResult> EvaluationList(string searchKeywords, Guid? interestUid, Guid? evaluationStatusUid, int? page = 1, int? pageSize = 10)
        {
            if (this.EditionDto?.IsMusicBusinessRoundProjectBuyerEvaluationStarted() != true)
            {
                return RedirectToAction("Index", "BusinessRoundProjects", new { Area = "Music" });
            }

            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper($"{Labels.MusicProjects} - {Labels.BusinessRound}", new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper($"{Labels.Projects} - {Labels.BusinessRound}", Url.Action("EvaluationList", "BusinessRoundProjects", new { Area = "Music" })),
            });

            #endregion

            ViewBag.SearchKeywords = searchKeywords;
            ViewBag.InterestUid = interestUid;
            ViewBag.EvaluationStatusUid = evaluationStatusUid;
            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;

            //ViewBag.GenreInterests = await this.interestRepo.FindAllDtosByInterestGroupUidAsync(InterestGroup.AudiovisualGenre.Uid);
            ViewBag.ProjectEvaluationStatuses = await this.evaluationStatusRepository.FindAllAsync();

            return View();
        }

        /// <summary>Shows the evaluation list widget.</summary>
        /// <param name="searchKeywords">The search keywords.</param>
        /// <param name="interestUid">The interest uid.</param>
        /// <param name="evaluationStatusUid">The evaluation status uid.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        [AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.PlayerExecutiveMusic)]
        [HttpGet]
        public async Task<ActionResult> ShowEvaluationListWidget(string searchKeywords, Guid? interestUid, Guid? evaluationStatusUid, int? page = 1, int? pageSize = 10)
        {
            if (this.EditionDto?.IsMusicBusinessRoundProjectBuyerEvaluationStarted() != true)
            {
                return Json(new { status = "error", message = Texts.ForbiddenErrorMessage }, JsonRequestBehavior.AllowGet);
            }

            var projects = await this.musicBusinessRoundProjectRepo.FindAllDtosToEvaluateAsync(
                this.UserAccessControlDto?.EditionAttendeeCollaborator?.Uid ?? Guid.Empty,
                searchKeywords,
                interestUid,
                evaluationStatusUid,
                page.Value,
                pageSize.Value);

            ViewBag.SearchKeywords = searchKeywords;
            ViewBag.InterestUid = interestUid;
            ViewBag.EvaluationStatusUid = evaluationStatusUid;
            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/EvaluationListWidget", projects), divIdOrClass = "#ProjectBuyerEvaluationListWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        ///// <summary>Shows the evaluation list item widget.</summary>
        ///// <param name="projectUid">The project uid.</param>
        ///// <returns></returns>
        //[AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.PlayerExecutiveAudiovisual)]
        //[HttpGet]
        //public async Task<ActionResult> ShowEvaluationListItemWidget(Guid? projectUid)
        //{
        //    if (this.EditionDto?.IsProjectBuyerEvaluationStarted() != true)
        //    {
        //        return Json(new { status = "error", message = Texts.ForbiddenErrorMessage }, JsonRequestBehavior.AllowGet);
        //    }

        //    if (!projectUid.HasValue)
        //    {
        //        return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM) }, JsonRequestBehavior.AllowGet);
        //    }

        //    var projects = await this.projectRepo.FindDtoToEvaluateAsync(
        //        this.UserAccessControlDto?.EditionAttendeeCollaborator?.Uid ?? Guid.Empty,
        //        projectUid.Value);

        //    return Json(new
        //    {
        //        status = "success",
        //        pages = new List<dynamic>
        //        {
        //            new { page = this.RenderRazorViewToString("Widgets/EvaluationListItemWidget", projects), divIdOrClass = $"#project-{projectUid}" },
        //        }
        //    }, JsonRequestBehavior.AllowGet);
        //}

        #endregion

        #region Evaluation Details

        ///// <summary>Evaluations the details.</summary>
        ///// <param name="id">The identifier.</param>
        ///// <returns></returns>
        //[AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.PlayerExecutiveAudiovisual)]
        //public async Task<ActionResult> EvaluationDetails(Guid? id)
        //{
        //    if (this.EditionDto?.IsProjectBuyerEvaluationStarted() != true)
        //    {
        //        return RedirectToAction("Index", "BusinessRoundProjects", new { Area = "Music" });
        //    }

        //    var projectDto = await this.projectRepo.FindSiteDetailsDtoByProjectUidAsync(id ?? Guid.Empty, this.EditionDto.Id);
        //    if (projectDto == null)
        //    {
        //        this.StatusMessageToastr(string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()), Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);
        //        return RedirectToAction("EvaluationList", "BusinessRoundProjects", new { Area = "Music" });
        //    }

        //    if (!projectDto.Project.IsFinished())
        //    {
        //        this.StatusMessageToastr(string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()), Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);
        //        return RedirectToAction("EvaluationList", "BusinessRoundProjects", new { Area = "Music" });
        //    }

        //    if (this.UserAccessControlDto?.HasAnyEditionAttendeeOrganization(projectDto.ProjectBuyerEvaluationDtos?.Select(pbed => pbed.BuyerAttendeeOrganizationDto.AttendeeOrganization.Uid)?.ToList()) != true) // Is buyer
        //    {
        //        this.StatusMessageToastr(Texts.ForbiddenErrorMessage, Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);
        //        return RedirectToAction("EvaluationList", "BusinessRoundProjects", new { Area = "Music" });
        //    }

        //    #region Breadcrumb

        //    ViewBag.Breadcrumb = new BreadcrumbHelper($"{Labels.AudiovisualProjects} - {Labels.BusinessRound}", new List<BreadcrumbItemHelper> {
        //        new BreadcrumbItemHelper($"{Labels.Projects} - {Labels.BusinessRound}", Url.Action("EvaluationList", "BusinessRoundProjects", new { Area = "Audiovisual" })),
        //        new BreadcrumbItemHelper(projectDto.GetTitleDtoByLanguageCode(this.UserInterfaceLanguage)?.ProjectTitle?.Value ?? Labels.Project, Url.Action("EvaluationDetails", "BusinessRoundProjects", new { id }))
        //    });

        //    #endregion

        //    return View(projectDto);
        //}

        ///// <summary>Shows the buyer evaluation widget.</summary>
        ///// <param name="projectUid">The project uid.</param>
        ///// <returns></returns>
        //[HttpGet]
        //public async Task<ActionResult> ShowBuyerEvaluationWidget(Guid? projectUid)
        //{
        //    if (this.EditionDto?.IsProjectBuyerEvaluationStarted() != true)
        //    {
        //        return Json(new { status = "error", message = Texts.ForbiddenErrorMessage }, JsonRequestBehavior.AllowGet);
        //    }

        //    var projectBuyerEvaluationDto = await this.projectRepo.FindSiteBuyerEvaluationWidgetDtoByProjectUidAsync(projectUid ?? Guid.Empty, this.UserAccessControlDto?.EditionAttendeeCollaborator?.Uid ?? Guid.Empty);
        //    if (projectBuyerEvaluationDto == null)
        //    {
        //        return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
        //    }

        //    if (this.UserAccessControlDto?.HasAnyEditionAttendeeOrganization(projectBuyerEvaluationDto.ProjectBuyerEvaluationDtos?.Select(pbed => pbed.BuyerAttendeeOrganizationDto.AttendeeOrganization.Uid)?.ToList()) != true   // Buyer with project finished
        //        || projectBuyerEvaluationDto.Project?.IsFinished() != true)
        //    {
        //        return Json(new { status = "error", message = Texts.ForbiddenErrorMessage }, JsonRequestBehavior.AllowGet);
        //    }

        //    return Json(new
        //    {
        //        status = "success",
        //        pages = new List<dynamic>
        //        {
        //            new { page = this.RenderRazorViewToString("Widgets/BuyerEvaluationWidget", projectBuyerEvaluationDto), divIdOrClass = "#ProjectBuyerEvaluationWidget" },
        //        }
        //    }, JsonRequestBehavior.AllowGet);
        //}

        ///// <summary>
        ///// Shows the accept evaluation modal.
        ///// </summary>
        ///// <param name="projectUid">The project uid.</param>
        ///// <param name="buyerAttendeeOrganizationUid">The buyer attendee organization uid.</param>
        ///// <returns></returns>
        ///// <exception cref="PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions.DomainException"></exception>
        //[HttpGet]
        //public async Task<ActionResult> ShowAcceptEvaluationModal(Guid? projectUid, Guid? buyerAttendeeOrganizationUid)
        //{
        //    AcceptProjectEvaluation cmd;

        //    try
        //    {
        //        if (this.EditionDto?.IsProjectBuyerEvaluationOpen() != true)
        //        {
        //            throw new DomainException(Texts.ForbiddenErrorMessage);
        //        }

        //        var projectDto = await this.projectRepo.FindSiteDetailsDtoByProjectUidAsync(projectUid ?? Guid.Empty, this.EditionDto.Id);
        //        if (projectDto == null)
        //        {
        //            throw new DomainException(string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()));
        //        }

        //        if (!projectDto.Project.IsFinished())
        //        {
        //            throw new DomainException(Texts.ForbiddenErrorMessage);
        //        }

        //        if (this.UserAccessControlDto?.HasAnyEditionAttendeeOrganization(projectDto.ProjectBuyerEvaluationDtos?.Select(pbed => pbed.BuyerAttendeeOrganizationDto.AttendeeOrganization.Uid)?.ToList()) != true) // Is buyer
        //        {
        //            throw new DomainException(Texts.ForbiddenErrorMessage);
        //        }

        //        var maximumAvailableSlotsByEditionIdResponseDto = await CommandBus.Send(new GetMaximumAvailableSlotsByEditionId(this.EditionDto.Id));
        //        var playerAcceptedProjectsCount = await CommandBus.Send(new CountPresentialNegotiationsAcceptedByBuyerAttendeeOrganizationUid(buyerAttendeeOrganizationUid ?? Guid.Empty));

        //        cmd = new AcceptProjectEvaluation(
        //            projectDto,
        //            this.UserAccessControlDto?.EditionAttendeeOrganizations?.ToList(),
        //            maximumAvailableSlotsByPlayer: maximumAvailableSlotsByEditionIdResponseDto.MaximumAvailableSlotsByPlayer,
        //            playerAcceptedProjectsCount: playerAcceptedProjectsCount);
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
        //[HttpPost]
        //public async Task<ActionResult> Accept(AcceptProjectEvaluation cmd)
        //{
        //    var result = new AppValidationResult();

        //    try
        //    {
        //        if (this.EditionDto?.IsProjectBuyerEvaluationOpen() != true)
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
        //            await this.projectRepo.FindSiteDetailsDtoByProjectUidAsync(cmd.ProjectUid ?? Guid.Empty, this.EditionDto.Id),
        //            this.UserAccessControlDto?.EditionAttendeeOrganizations?.ToList());

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

        ///// <summary>Shows the refuse evaluation modal.</summary>
        ///// <param name="projectUid">The project uid.</param>
        ///// <returns></returns>
        //[HttpGet]
        //public async Task<ActionResult> ShowRefuseEvaluationModal(Guid? projectUid)
        //{
        //    RefuseProjectEvaluation cmd;

        //    try
        //    {
        //        if (this.EditionDto?.IsProjectBuyerEvaluationOpen() != true)
        //        {
        //            throw new DomainException(Texts.ForbiddenErrorMessage);
        //        }

        //        var projectDto = await this.projectRepo.FindSiteDetailsDtoByProjectUidAsync(projectUid ?? Guid.Empty, this.EditionDto.Id);
        //        if (projectDto == null)
        //        {
        //            throw new DomainException(string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()));
        //        }

        //        if (!projectDto.Project.IsFinished())
        //        {
        //            throw new DomainException(Texts.ForbiddenErrorMessage);
        //        }

        //        if (this.UserAccessControlDto?.HasAnyEditionAttendeeOrganization(projectDto.ProjectBuyerEvaluationDtos?.Select(pbed => pbed.BuyerAttendeeOrganizationDto.AttendeeOrganization.Uid)?.ToList()) != true) // Is buyer
        //        {
        //            throw new DomainException(Texts.ForbiddenErrorMessage);
        //        }

        //        cmd = new RefuseProjectEvaluation(
        //            projectDto,
        //            this.UserAccessControlDto?.EditionAttendeeOrganizations?.ToList(),
        //            await this.projectEvaluationRefuseReasonRepo.FindAllByProjectTypeUidAsync(ProjectType.Audiovisual.Uid));
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

        ///// <summary>Refuses the specified project evaluation.</summary>
        ///// <param name="cmd">The command.</param>
        ///// <returns></returns>
        //[HttpPost]
        //public async Task<ActionResult> Refuse(RefuseProjectEvaluation cmd)
        //{
        //    var result = new AppValidationResult();

        //    try
        //    {
        //        if (this.EditionDto?.IsProjectBuyerEvaluationOpen() != true)
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
        //            await this.projectRepo.FindSiteDetailsDtoByProjectUidAsync(cmd.ProjectUid ?? Guid.Empty, this.EditionDto.Id),
        //            this.UserAccessControlDto?.EditionAttendeeOrganizations?.ToList(),
        //            await this.projectEvaluationRefuseReasonRepo.FindAllByProjectTypeUidAsync(ProjectType.Audiovisual.Uid));

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

        #endregion

        #endregion
    }
}