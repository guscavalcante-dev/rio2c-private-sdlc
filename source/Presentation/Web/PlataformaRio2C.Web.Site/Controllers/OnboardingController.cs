// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Fabio Seixas
// Created          : 08-29-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 12-21-2023
// ***********************************************************************
// <copyright file="OnboardingController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Infra.CrossCutting.Identity.Service;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using MediatR;
using PlataformaRio2C.Infra.CrossCutting.Tools.Helpers;
using System.Collections.Generic;
using System.Linq;
using PlataformaRio2C.Application;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Application.CQRS.Queries;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.CrossCutting.Identity.AuthorizeAttributes;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using PlataformaRio2C.Web.Site.Filters;
using Constants = PlataformaRio2C.Domain.Constants;

namespace PlataformaRio2C.Web.Site.Controllers
{
    /// <summary>OnboardingController</summary>
    [AjaxAuthorize(Order = 1)]
    [AuthorizeCollaboratorType(Order = 2)]
    public class OnboardingController : BaseController
    {
        private readonly IActivityRepository activityRepo;
        private readonly ITargetAudienceRepository targetAudienceRepo;
        private readonly IInterestRepository interestRepo;

        /// <summary>Initializes a new instance of the <see cref="OnboardingController"/> class.</summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="identityController">The identity controller.</param>
        /// <param name="activityRepository">The activity repository.</param>
        /// <param name="targetAudienceRepository">The target audience repository.</param>
        /// <param name="interestRepository">The interest repository.</param>
        public OnboardingController(
            IMediator commandBus, 
            IdentityAutenticationService identityController,
            IActivityRepository activityRepository,
            ITargetAudienceRepository targetAudienceRepository,
            IInterestRepository interestRepository)
            : base(commandBus, identityController)
        {
            this.activityRepo = activityRepository;
            this.targetAudienceRepo = targetAudienceRepository;
            this.interestRepo = interestRepository;
        }

        /// <summary>Indexes this instance.</summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            // Redirect if onboarding is not pending
            if (this.UserAccessControlDto?.IsOnboardingPending() != true)
            {
                return RedirectToAction("Index", "Home");
            }

            // Redirect to access data if not finished
            if (this.UserAccessControlDto?.IsUserOnboardingFinished() != true)
            {
                return RedirectToAction("AccessData", "Onboarding");
            }

            // Redirect if speaker terms acceptance pending
            if (this.UserAccessControlDto?.IsSpeakerTermsAcceptanceFinished() != true)
            {
                return RedirectToAction("SpeakerTermsAcceptance", "Onboarding");
            }

            // Redirect if player terms acceptance is pending
            if (this.UserAccessControlDto?.IsPlayerTermsAcceptanceFinished() != true)
            {
                return RedirectToAction("PlayerTermsAcceptance", "Onboarding");
            }

            // Redirect to collaborator data if not finished
            if (this.UserAccessControlDto?.IsCollaboratorOnboardingFinished() != true)
            {
                return RedirectToAction("CollaboratorData", "Onboarding");
            }

            // Redirect to organization data if not finished and there is no pending interests to do before
            if (this.UserAccessControlDto?.IsPlayerOrganizationsOnboardingFinished() != true
                && this.UserAccessControlDto?.IsPlayerOrganizationInterestsOnboardingPending() != true)
            {
                return RedirectToAction("PlayerInfo", "Onboarding");
            }

            // Redirect to organization interests if not finished
            if (this.UserAccessControlDto?.IsPlayerOrganizationInterestsOnboardingPending() == true)
            {
                return RedirectToAction("PlayerInterests", "Onboarding");
            }

            // Redirect to ticket buyer organization if not finished or speaker
            if (this.UserAccessControlDto?.IsTicketBuyerOrganizationOnboardingPending() == true)
            {
                return RedirectToAction("CompanyInfo", "Onboarding");
            }

            return RedirectToAction("Index", "Home");
        }

        #region Access Data

        /// <summary>Accesses the data.</summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> AccessData()
        {
            if (this.UserAccessControlDto?.IsUserOnboardingFinished() == true)
            {
                return RedirectToAction("Index", "Onboarding");
            }

            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.WelcomeTitle, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Messages.CompleteYourRegistration, Url.Action("Index", "Onboarding"))
            });

            #endregion

            await this.CommandBus.Send(new OnboardCollaborator(
                this.UserAccessControlDto.Collaborator.Uid,
                this.UserAccessControlDto.User.Id,
                this.UserAccessControlDto.User.Uid,
                this.EditionDto.Id,
                this.EditionDto.Uid,
                this.UserInterfaceLanguage));

            this.SetViewBags();

            var cmd = new OnboardCollaboratorAccessData(this.UserAccessControlDto?.Collaborator);

            return View(cmd);
        }

        /// <summary>Accesses the data.</summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> AccessData(OnboardCollaboratorAccessData cmd)
        {
            if (this.UserAccessControlDto?.IsUserOnboardingFinished() == true)
            {
                return RedirectToAction("Index", "Onboarding");
            }

            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.WelcomeTitle, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Messages.CompleteYourRegistration, Url.Action("Index", "Onboarding"))
            });

            #endregion

            this.SetViewBags();

            var result = new AppValidationResult();

            try
            {
                if (!ModelState.IsValid)
                {
                    throw new DomainException(Messages.CorrectFormValues);
                }

                cmd.UpdatePreSendProperties(
                    this.UserAccessControlDto.Collaborator.Uid,
                    this.IdentityController.HashPassword(cmd.Password),
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

                this.StatusMessageToastr(ex.GetInnerMessage(), Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);

                return View(cmd);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                this.StatusMessageToastr(Messages.WeFoundAndError, Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);

                return View(cmd);
            }

            this.StatusMessageToastr(string.Format(Messages.EntityActionSuccessfull, Labels.AccessData, Labels.UpdatedM.ToLowerInvariant()), Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Success);

            return RedirectToAction("Index", "Onboarding");
        }

        #endregion

        #region Speaker Terms Acceptance

        /// <summary>Speakers the terms acceptance.</summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> SpeakerTermsAcceptance()
        {
            if (this.UserAccessControlDto?.IsSpeakerTermsAcceptanceFinished() == true)
            {
                return RedirectToAction("Index", "Onboarding");
            }

            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.WelcomeTitle, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Messages.CompleteYourRegistration, Url.Action("Index", "Onboarding"))
            });

            #endregion

            this.SetViewBags();

            var cmd = new OnboardSpeakerTermsAcceptance();

            return View(cmd);
        }

        /// <summary>Speakers the terms acceptance.</summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> SpeakerTermsAcceptance(OnboardSpeakerTermsAcceptance cmd)
        {
            if (this.UserAccessControlDto?.IsSpeakerTermsAcceptanceFinished() == true)
            {
                return RedirectToAction("Index", "Onboarding");
            }

            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.WelcomeTitle, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Messages.CompleteYourRegistration, Url.Action("Index", "Onboarding"))
            });

            #endregion

            this.SetViewBags();

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

                this.StatusMessageToastr(ex.GetInnerMessage(), Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);

                return View(cmd);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                this.StatusMessageToastr(Messages.WeFoundAndError, Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);

                return View(cmd);
            }

            this.StatusMessageToastr(string.Format(Messages.EntityActionSuccessfull, Messages.ImageAuthorizationForm, Labels.Accepted.ToLowerInvariant()), Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Success);

            return RedirectToAction("Index", "Onboarding");
        }

        #endregion

        #region Player Terms Acceptance

        /// <summary>Players the terms acceptance.</summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> PlayerTermsAcceptance()
        {
            if (this.UserAccessControlDto?.IsPlayerTermsAcceptanceFinished() == true)
            {
                return RedirectToAction("Index", "Onboarding");
            }

            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.WelcomeTitle, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Messages.CompleteYourRegistration, Url.Action("Index", "Onboarding"))
            });

            #endregion

            this.SetViewBags();

            var cmd = new OnboardPlayerTermsAcceptance();

            return View(cmd);
        }

        /// <summary>Players the terms acceptance.</summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> PlayerTermsAcceptance(OnboardPlayerTermsAcceptance cmd)
        {
            if (this.UserAccessControlDto?.IsPlayerTermsAcceptanceFinished() == true)
            {
                return RedirectToAction("Index", "Onboarding");
            }

            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.WelcomeTitle, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Messages.CompleteYourRegistration, Url.Action("Index", "Onboarding"))
            });

            #endregion

            this.SetViewBags();

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

                this.StatusMessageToastr(ex.GetInnerMessage(), Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);

                return View(cmd);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                this.StatusMessageToastr(Messages.WeFoundAndError, Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);

                return View(cmd);
            }

            this.StatusMessageToastr(string.Format(Messages.EntityActionSuccessfull, Messages.PlayerTerms, Labels.Accepted.ToLowerInvariant()), Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Success);

            return RedirectToAction("Index", "Onboarding");
        }

        #endregion

        #region Collaborator Data

        /// <summary>Collaborators the data.</summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> CollaboratorData()
        {
            if (this.UserAccessControlDto?.IsCollaboratorOnboardingFinished() == true)
            {
                return RedirectToAction("Index", "Onboarding");
            }

            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.WelcomeTitle, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Messages.CompleteYourRegistration, Url.Action("Index", "Onboarding"))
            });

            #endregion

            this.SetViewBags();

            var cmd = new OnboardCollaboratorData(
                await this.CommandBus.Send(new FindCollaboratorDtoByUidAndByEditionIdAsync(this.UserAccessControlDto?.Collaborator?.Uid ?? Guid.Empty, this.EditionDto.Id, this.UserInterfaceLanguage)),
                await this.CommandBus.Send(new FindAllCollaboratorGenderAsync(this.UserInterfaceLanguage)),
                await this.CommandBus.Send(new FindAllCollaboratorIndustryAsync(this.UserInterfaceLanguage)),
                await this.CommandBus.Send(new FindAllCollaboratorRoleAsync(this.UserInterfaceLanguage)),
                await this.CommandBus.Send(new FindAllLanguagesDtosAsync(this.UserInterfaceLanguage)),
                await this.CommandBus.Send(new FindAllEditionsDtosAsync(true)),
                EditionDto.Id,
                true,
                true,
                true,
                UserInterfaceLanguage);

            return View(cmd);
        }

        /// <summary>Collaborators the data.</summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> CollaboratorData(OnboardCollaboratorData cmd)
        {
            if (this.UserAccessControlDto?.IsCollaboratorOnboardingFinished() == true)
            {
                return RedirectToAction("Index", "Onboarding");
            }

            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.WelcomeTitle, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Messages.CompleteYourRegistration, Url.Action("Index", "Onboarding"))
            });

            #endregion

            this.SetViewBags();

            var result = new AppValidationResult();

            try
            {
                var isExecutive = this.UserAccessControlDto?.HasAnyCollaboratorType(Constants.CollaboratorType.Executives) == true;
                var isIndustry = this.UserAccessControlDto?.HasCollaboratorType(Constants.CollaboratorType.Industry) == true;

                // Field SharePublicEmail does not exist for this types of users
                if (!isExecutive && !isIndustry)
                {
                    if (ModelState.ContainsKey("SharePublicEmail"))
                    {
                        ModelState["SharePublicEmail"].Errors.Clear();
                    }
                }

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

                this.StatusMessageToastr(ex.GetInnerMessage(), Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);                
                
                cmd.UpdateModelsAndLists(                
                    await this.CommandBus.Send(new FindAllCollaboratorGenderAsync(this.UserInterfaceLanguage)),
                    await this.CommandBus.Send(new FindAllCollaboratorIndustryAsync(this.UserInterfaceLanguage)),
                    await this.CommandBus.Send(new FindAllCollaboratorRoleAsync(this.UserInterfaceLanguage)),
                    await this.CommandBus.Send(new FindAllEditionsDtosAsync(true)),
                    this.EditionDto.Id,
                    this.UserInterfaceLanguage);

                return View(cmd);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                this.StatusMessageToastr(Messages.WeFoundAndError, Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);                
                cmd.UpdateModelsAndLists(                
                    await this.CommandBus.Send(new FindAllCollaboratorGenderAsync(this.UserInterfaceLanguage)),
                    await this.CommandBus.Send(new FindAllCollaboratorIndustryAsync(this.UserInterfaceLanguage)),
                    await this.CommandBus.Send(new FindAllCollaboratorRoleAsync(this.UserInterfaceLanguage)),
                    await this.CommandBus.Send(new FindAllEditionsDtosAsync(true)),
                    EditionDto.Id,
                    UserInterfaceLanguage);

                return View(cmd);
            }

            this.StatusMessageToastr(string.Format(Messages.EntityActionSuccessfull, Labels.PersonalInformation, Labels.UpdatedM.ToLowerInvariant()), Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Success);

            return RedirectToAction("Index", "Onboarding");
        }

        #endregion

        #region Player Info

        /// <summary>Players the information.</summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> PlayerInfo()
        {
            if (this.UserAccessControlDto?.IsPlayerOrganizationsOnboardingFinished() == true)
            {
                return RedirectToAction("Index", "Onboarding");
            }

            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.WelcomeTitle, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Messages.CompleteYourRegistration, Url.Action("Index", "Onboarding"))
            });

            #endregion

            this.SetViewBags();

            var currentOrganization = this.UserAccessControlDto?.EditionAttendeeOrganizations?.FirstOrDefault(eao => !eao.OnboardingOrganizationDate.HasValue
                                                                                                                     && eao.AttendeeOrganizationTypes.Any(aot => !aot.IsDeleted
                                                                                                                                                                 && aot.OrganizationType.Name == "Player"))?.Organization;
            if (currentOrganization == null)
            {
                return RedirectToAction("Index", "Onboarding");
            }

            var cmd = new OnboardPlayerOrganizationData(
                await this.CommandBus.Send(new FindOrganizationDtoByUidAsync(currentOrganization.Uid, this.EditionDto.Id, this.UserInterfaceLanguage)),
                await this.CommandBus.Send(new FindAllLanguagesDtosAsync(this.UserInterfaceLanguage)),
                await this.CommandBus.Send(new FindAllCountriesBaseDtosAsync(this.UserInterfaceLanguage)),
                await this.activityRepo.FindAllAsync(),
                await this.targetAudienceRepo.FindAllByProjectTypeIdAsync(ProjectType.Audiovisual.Id),
                true,
                true,
                true);

            return View(cmd);
        }

        /// <summary>Players the information.</summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> PlayerInfo(OnboardPlayerOrganizationData cmd)
        {
            if (this.UserAccessControlDto?.IsPlayerOrganizationsOnboardingFinished() == true)
            {
                return RedirectToAction("Index", "Onboarding");
            }

            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.WelcomeTitle, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Messages.CompleteYourRegistration, Url.Action("Index", "Onboarding"))
            });

            #endregion

            this.SetViewBags();

            var result = new AppValidationResult();

            try
            {
                if (!ModelState.IsValid)
                {
                    throw new DomainException(Messages.CorrectFormValues);
                }

                cmd.UpdatePreSendProperties(
                    OrganizationType.AudiovisualPlayer,
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

                this.StatusMessageToastr(ex.GetInnerMessage(), Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);

                cmd.UpdateDropdownProperties(
                    await this.CommandBus.Send(new FindAllCountriesBaseDtosAsync(this.UserInterfaceLanguage)),
                    await this.targetAudienceRepo.FindAllByProjectTypeIdAsync(ProjectType.Audiovisual.Id));

                return View(cmd);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                this.StatusMessageToastr(Messages.WeFoundAndError, Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);

                cmd.UpdateDropdownProperties(
                    await this.CommandBus.Send(new FindAllCountriesBaseDtosAsync(this.UserInterfaceLanguage)),
                    await this.targetAudienceRepo.FindAllByProjectTypeIdAsync(ProjectType.Audiovisual.Id));

                return View(cmd);
            }

            this.StatusMessageToastr(string.Format(Messages.EntityActionSuccessfull, Labels.PlayerInfo, Labels.UpdatedM.ToLowerInvariant()), Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Success);

            return RedirectToAction("Index", "Onboarding");
        }

        #endregion

        #region Player Interests

        /// <summary>Players the interests.</summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> PlayerInterests()
        {
            if (this.UserAccessControlDto?.IsPlayerOrganizationInterestsOnboardingPending() != true)
            {
                return RedirectToAction("Index", "Onboarding");
            }

            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.WelcomeTitle, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Messages.CompleteYourRegistration, Url.Action("Index", "Onboarding"))
            });

            #endregion

            this.SetViewBags();

            var currentOrganization = this.UserAccessControlDto?.EditionAttendeeOrganizations?.FirstOrDefault(eao => eao.OnboardingOrganizationDate.HasValue
                                                                                                                     && !eao.OnboardingInterestsDate.HasValue
                                                                                                                     && eao.AttendeeOrganizationTypes.Any(aot => !aot.IsDeleted
                                                                                                                                                                 && aot.OrganizationType.Name == OrganizationType.AudiovisualPlayer.Name))?.Organization;
            if (currentOrganization == null)
            {
                return RedirectToAction("Index", "Onboarding");
            }

            var cmd = new OnboardPlayerInterests(
                await this.CommandBus.Send(new FindOrganizationDtoByUidAsync(currentOrganization.Uid, this.EditionDto.Id, this.UserInterfaceLanguage)),
                await this.interestRepo.FindAllDtosAsync(),
                await this.CommandBus.Send(new FindAllLanguagesDtosAsync(this.UserInterfaceLanguage)),
                true);

            return View(cmd);
        }

        /// <summary>Players the interests.</summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> PlayerInterests(OnboardPlayerInterests cmd)
        {
            if (this.UserAccessControlDto?.IsPlayerOrganizationInterestsOnboardingPending() != true)
            {
                return RedirectToAction("Index", "Onboarding");
            }

            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.WelcomeTitle, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Messages.CompleteYourRegistration, Url.Action("Index", "Onboarding"))
            });

            #endregion

            this.SetViewBags();

            var result = new AppValidationResult();

            try
            {
                if (!ModelState.IsValid)
                {
                    throw new DomainException(Messages.CorrectFormValues);
                }

                cmd.UpdatePreSendProperties(
                    OrganizationType.AudiovisualPlayer,
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

                this.StatusMessageToastr(ex.GetInnerMessage(), Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);

                //cmd.UpdateDropdownProperties(
                //    await this.interestRepo.FindAllGroupedByInterestGroupsAsync());

                return View(cmd);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                this.StatusMessageToastr(Messages.WeFoundAndError, Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);

                //cmd.UpdateDropdownProperties(
                //    await this.interestRepo.FindAllGroupedByInterestGroupsAsync());

                return View(cmd);
            }

            this.StatusMessageToastr(string.Format(Messages.EntityActionSuccessfull, Labels.PlayerInterests, Labels.UpdatedM.ToLowerInvariant()), Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Success);

            return RedirectToAction("Index", "Onboarding");
        }

        #endregion

        #region Company Info

        /// <summary>Companies the information.</summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> CompanyInfo()
        {
            if (this.UserAccessControlDto?.IsTicketBuyerOrganizationOnboardingPending() != true)
            {
                return RedirectToAction("Index", "Onboarding");
            }

            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.WelcomeTitle, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Messages.CompleteYourRegistration, Url.Action("Index", "Onboarding"))
            });

            #endregion

            this.SetViewBags();

            var currentOrganization = this.UserAccessControlDto?.EditionAttendeeOrganizations?.FirstOrDefault(eao => !eao.OnboardingOrganizationDate.HasValue)?.Organization;

            var cmd = new CreateTicketBuyerOrganizationData(
                Guid.Empty,
                currentOrganization == null ? null : await this.CommandBus.Send(new FindOrganizationDtoByUidAsync(currentOrganization.Uid, this.EditionDto.Id, this.UserInterfaceLanguage)),
                await this.CommandBus.Send(new FindAllLanguagesDtosAsync(this.UserInterfaceLanguage)),
                await this.CommandBus.Send(new FindAllCountriesBaseDtosAsync(this.UserInterfaceLanguage)),
                false,
                false,
                true);

            return View(cmd);
        }

        /// <summary>Companies the information.</summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> CompanyInfo(CreateTicketBuyerOrganizationData cmd)
        {
            if (this.UserAccessControlDto?.IsTicketBuyerOrganizationOnboardingPending() != true)
            {
                return RedirectToAction("Index", "Onboarding");
            }

            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.WelcomeTitle, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Messages.CompleteYourRegistration, Url.Action("Index", "Onboarding"))
            });

            #endregion

            this.SetViewBags();

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

                this.StatusMessageToastr(ex.GetInnerMessage(), Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);

                cmd.UpdateDropdownProperties(
                    await this.CommandBus.Send(new FindAllCountriesBaseDtosAsync(this.UserInterfaceLanguage)));

                return View(cmd);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                this.StatusMessageToastr(Messages.WeFoundAndError, Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);

                cmd.UpdateDropdownProperties(
                    await this.CommandBus.Send(new FindAllCountriesBaseDtosAsync(this.UserInterfaceLanguage)));

                return View(cmd);
            }

            this.StatusMessageToastr(string.Format(Messages.EntityActionSuccessfull, Labels.Company, Labels.UpdatedF.ToLowerInvariant()), Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Success);

            return RedirectToAction("Index", "Onboarding");
        }

        /// <summary>Skips the company information.</summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> SkipCompanyInfo()
        {
            if (this.UserAccessControlDto?.IsTicketBuyerOrganizationOnboardingPending() != true)
            {
                return RedirectToAction("Index", "Onboarding");
            }

            var result = new AppValidationResult();

            try
            {
                result = await this.CommandBus.Send(new SkipOnboardTicketBuyerOrganizationData(
                    this.UserAccessControlDto.Collaborator.Uid,
                    this.UserAccessControlDto.User.Id,
                    this.UserAccessControlDto.User.Uid,
                    this.EditionDto.Id,
                    this.EditionDto.Uid,
                    this.UserInterfaceLanguage));
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

                this.StatusMessageToastr(ex.GetInnerMessage(), Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);

                return RedirectToAction("CompanyInfo", "Onboarding");
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                this.StatusMessageToastr(Messages.WeFoundAndError, Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);

                return RedirectToAction("CompanyInfo", "Onboarding");
            }

            this.StatusMessageToastr(string.Format(Messages.EntityActionSuccessfull, Labels.Company, Labels.UpdatedF.ToLowerInvariant()), Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Success);

            return RedirectToAction("Index", "Onboarding");
        }

        #endregion

        #region Private Methods

        /// <summary>Sets the view bags.</summary>
        private void SetViewBags()
        {
            bool isPlayer = this.UserAccessControlDto?.HasCollaboratorType(Constants.CollaboratorType.PlayerExecutiveAudiovisual) == true;
            ViewBag.IsPlayer = isPlayer;

            if (isPlayer)
            {
                ViewBag.PlayerAttendeeOrganizations = this.UserAccessControlDto?.EditionAttendeeOrganizations?
                                                                .Where(eao => !eao.IsDeleted
                                                                              && eao.AttendeeOrganizationTypes?
                                                                                  .Any(aot => !aot.IsDeleted
                                                                                              && aot.OrganizationType.Name == OrganizationType.AudiovisualPlayer.Name) == true)?
                                                                .ToList();
            }

            ViewBag.IsIndustry = this.UserAccessControlDto?.HasCollaboratorType(Constants.CollaboratorType.Industry) == true;
            ViewBag.IsTicketBuyer = this.UserAccessControlDto?.HasAnyCollaboratorType(Constants.CollaboratorType.TicketBuyers) == true;
            ViewBag.IsExecutive = this.UserAccessControlDto?.HasAnyCollaboratorType(Constants.CollaboratorType.Executives);
            ViewBag.IsSpeaker = this.UserAccessControlDto?.HasCollaboratorType(Constants.CollaboratorType.Speaker) == true;
        }

        #endregion
    }
}