// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Fabio Seixas
// Created          : 08-29-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-12-2019
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

namespace PlataformaRio2C.Web.Site.Controllers
{
    /// <summary>OnboardingController</summary>
    [AjaxAuthorize(Order = 1)]
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
            if (this.UserAccessControlDto?.IsOrganizatiosnOnboardingFinished() != true
                && this.UserAccessControlDto?.HasOrganizationInterestsOnboardingPending() != true)
            {
                return RedirectToAction("PlayerInfo", "Onboarding");
            }

            // Redirect to organization interests if not finished
            if (this.UserAccessControlDto?.HasOrganizationInterestsOnboardingPending() == true)
            {
                return RedirectToAction("PlayerInterests", "Onboarding");
            }

            //TODO: Producer onboarding

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
                    this.EditionId,
                    this.EditionUid,
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
                Elmah.ErrorLog.GetDefault(System.Web.HttpContext.Current).Log(new Elmah.Error(ex));
                this.StatusMessageToastr(Messages.WeFoundAndError, Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);

                return View(cmd);
            }

            this.StatusMessageToastr(string.Format(Messages.EntityActionSuccessfull, Labels.AccessData, Labels.UpdatedM.ToLowerInvariant()), Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Success);

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
                    this.EditionId,
                    this.EditionUid,
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
                Elmah.ErrorLog.GetDefault(System.Web.HttpContext.Current).Log(new Elmah.Error(ex));
                this.StatusMessageToastr(Messages.WeFoundAndError, Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);

                return View(cmd);
            }

            this.StatusMessageToastr(string.Format(Messages.EntityActionSuccessfull, Labels.AccessData, Labels.UpdatedM.ToLowerInvariant()), Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Success);

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
                await this.CommandBus.Send(new FindCollaboratorDtoByUidAndByEditionIdAsync(this.UserAccessControlDto?.Collaborator?.Uid ?? Guid.Empty, this.EditionId, this.UserInterfaceLanguage)),
                await this.CommandBus.Send(new FindAllLanguagesDtosAsync(this.UserInterfaceLanguage)),
                await this.CommandBus.Send(new FindAllCountriesBaseDtosAsync(this.UserInterfaceLanguage)));

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
                if (!ModelState.IsValid)
                {
                    throw new DomainException(Messages.CorrectFormValues);
                }

                cmd.UpdatePreSendProperties(
                    this.UserAccessControlDto.Collaborator.Uid,
                    this.UserAccessControlDto.User.Id,
                    this.UserAccessControlDto.User.Uid,
                    this.EditionId,
                    this.EditionUid,
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

                cmd.UpdateDropdownProperties(
                    await this.CommandBus.Send(new FindAllCountriesBaseDtosAsync(this.UserInterfaceLanguage)));

                this.StatusMessageToastr(ex.GetInnerMessage(), Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);

                return View(cmd);
            }
            catch (Exception ex)
            {
                Elmah.ErrorLog.GetDefault(System.Web.HttpContext.Current).Log(new Elmah.Error(ex));
                this.StatusMessageToastr(Messages.WeFoundAndError, Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);

                cmd.UpdateDropdownProperties(
                    await this.CommandBus.Send(new FindAllCountriesBaseDtosAsync(this.UserInterfaceLanguage)));

                return View(cmd);
            }

            this.StatusMessageToastr(string.Format(Messages.EntityActionSuccessfull, Labels.AccessData, Labels.UpdatedM.ToLowerInvariant()), Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Success);

            return RedirectToAction("Index", "Onboarding");
        }

        #endregion

        #region Player Data

        /// <summary>Players the information.</summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> PlayerInfo()
        {
            if (this.UserAccessControlDto?.IsOrganizatiosnOnboardingFinished() == true)
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

            var cmd = new OnboardOrganizationData(
                await this.CommandBus.Send(new FindOrganizationDtoByUidAsync(currentOrganization.Uid, this.UserInterfaceLanguage)),
                await this.CommandBus.Send(new FindAllHoldingsBaseDtosAsync(null, this.UserInterfaceLanguage)),
                await this.CommandBus.Send(new FindAllLanguagesDtosAsync(this.UserInterfaceLanguage)),
                await this.CommandBus.Send(new FindAllCountriesBaseDtosAsync(this.UserInterfaceLanguage)),
                await this.activityRepo.FindAllAsync(),
                await this.targetAudienceRepo.FindAllAsync());

            return View(cmd);
        }

        /// <summary>Players the information.</summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> PlayerInfo(OnboardOrganizationData cmd)
        {
            if (this.UserAccessControlDto?.IsOrganizatiosnOnboardingFinished() == true)
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
                    OrganizationType.Player,
                    this.UserAccessControlDto.User.Id,
                    this.UserAccessControlDto.User.Uid,
                    this.EditionId,
                    this.EditionUid,
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
                    await this.CommandBus.Send(new FindAllHoldingsBaseDtosAsync(null, this.UserInterfaceLanguage)),
                    await this.CommandBus.Send(new FindAllCountriesBaseDtosAsync(this.UserInterfaceLanguage)),
                    await this.activityRepo.FindAllAsync(),
                    await this.targetAudienceRepo.FindAllAsync());

                return View(cmd);
            }
            catch (Exception ex)
            {
                Elmah.ErrorLog.GetDefault(System.Web.HttpContext.Current).Log(new Elmah.Error(ex));
                this.StatusMessageToastr(Messages.WeFoundAndError, Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);

                cmd.UpdateDropdownProperties(
                    await this.CommandBus.Send(new FindAllHoldingsBaseDtosAsync(null, this.UserInterfaceLanguage)),
                    await this.CommandBus.Send(new FindAllCountriesBaseDtosAsync(this.UserInterfaceLanguage)),
                    await this.activityRepo.FindAllAsync(),
                    await this.targetAudienceRepo.FindAllAsync());

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
            if (this.UserAccessControlDto?.HasOrganizationInterestsOnboardingPending() != true)
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
                                                                                                                                                                 && aot.OrganizationType.Name == "Player"))?.Organization;
            if (currentOrganization == null)
            {
                return RedirectToAction("Index", "Onboarding");
            }

            var cmd = new OnboardPlayerInterests(
                await this.CommandBus.Send(new FindOrganizationDtoByUidAsync(currentOrganization.Uid, this.UserInterfaceLanguage)),
                await this.interestRepo.FindAllGroupedByInterestGroupsAsync());

            return View(cmd);
        }

        /// <summary>Players the interests.</summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> PlayerInterests(OnboardPlayerInterests cmd)
        {
            if (this.UserAccessControlDto?.HasOrganizationInterestsOnboardingPending() != true)
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
                    OrganizationType.Player,
                    this.UserAccessControlDto.User.Id,
                    this.UserAccessControlDto.User.Uid,
                    this.EditionId,
                    this.EditionUid,
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
                    await this.interestRepo.FindAllGroupedByInterestGroupsAsync());

                return View(cmd);
            }
            catch (Exception ex)
            {
                Elmah.ErrorLog.GetDefault(System.Web.HttpContext.Current).Log(new Elmah.Error(ex));
                this.StatusMessageToastr(Messages.WeFoundAndError, Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);

                cmd.UpdateDropdownProperties(
                    await this.interestRepo.FindAllGroupedByInterestGroupsAsync());

                return View(cmd);
            }

            this.StatusMessageToastr(string.Format(Messages.EntityActionSuccessfull, Labels.PlayerInfo, Labels.UpdatedM.ToLowerInvariant()), Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Success);

            return RedirectToAction("Index", "Onboarding");
        }

        #endregion

        #region Private Methods

        /// <summary>Sets the view bags.</summary>
        private void SetViewBags()
        {
            var playerAttendeeOrganizations = this.UserAccessControlDto?.EditionAttendeeOrganizations?
                                                                                    .Where(eao => eao.AttendeeOrganizationTypes?
                                                                                                            .Any(aot => !aot.IsDeleted 
                                                                                                                        && aot.OrganizationType.Name == "Player") == true)?.ToList();
            var producerAttendeeOrganizations = this.UserAccessControlDto?.EditionAttendeeOrganizations?
                                                                                    .Where(eao => eao.AttendeeOrganizationTypes?
                                                                                                            .Any(aot => !aot.IsDeleted 
                                                                                                                        && aot.OrganizationType.Name == "Producer") == true)?.ToList();

            ViewBag.PlayerAttendeeOrganizations = playerAttendeeOrganizations;
            ViewBag.ProducerAttendeeOrganizations = producerAttendeeOrganizations;
            ViewBag.IsPlayer = playerAttendeeOrganizations?.Any() == true;
            ViewBag.IsProducer = producerAttendeeOrganizations?.Any() == true;
        }

        #endregion
    }
}