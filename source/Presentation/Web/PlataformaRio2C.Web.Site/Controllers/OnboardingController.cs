// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Fabio Seixas
// Created          : 08-29-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-06-2019
// ***********************************************************************
// <copyright file="OnboardingController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Microsoft.AspNet.Identity;
using PlataformaRio2C.Infra.CrossCutting.Identity.Service;
using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MediatR;
using PlataformaRio2C.Infra.CrossCutting.Resources.Helpers;
using PlataformaRio2C.Infra.CrossCutting.Tools.Helpers;
using System.Collections.Generic;
using PlataformaRio2C.Application;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Application.CQRS.Queries;
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
        /// <summary>Initializes a new instance of the <see cref="HomeController"/> class.</summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="identityController">The identity controller.</param>
        public OnboardingController(IMediator commandBus, IdentityAutenticationService identityController)
            : base(commandBus, identityController)
        {
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

            // Redirect to collaborator data if not finished
            if (this.UserAccessControlDto?.IsAttendeeCollaboratorOnboardingFinished() != true)
            {
                return RedirectToAction("CollaboratorData", "Onboarding");
            }
            
            // Redirect to player data if not finished
            if (this.UserAccessControlDto?.IsAttendeeOrganizationsOnboardingFinished() != true)
            {
                return RedirectToAction("PlayerData", "Onboarding");
            }

            return View("Index");
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

        #region Collaborator Data

        /// <summary>Collaborators the data.</summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> CollaboratorData()
        {
            if (this.UserAccessControlDto?.IsAttendeeCollaboratorOnboardingFinished() == true)
            {
                return RedirectToAction("Index", "Onboarding");
            }

            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.WelcomeTitle, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Messages.CompleteYourRegistration, Url.Action("Index", "Onboarding"))
            });

            #endregion

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
            if (this.UserAccessControlDto?.IsAttendeeCollaboratorOnboardingFinished() == true)
            {
                return RedirectToAction("Index", "Onboarding");
            }

            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.WelcomeTitle, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Messages.CompleteYourRegistration, Url.Action("Index", "Onboarding"))
            });

            #endregion

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

        #region Player Data

        [HttpGet]
        public async Task<ActionResult> PlayerData()
        {
            if (this.UserAccessControlDto?.IsAttendeeOrganizationsOnboardingFinished() == true)
            {
                return RedirectToAction("Index", "Onboarding");
            }

            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.WelcomeTitle, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Messages.CompleteYourRegistration, Url.Action("Index", "Onboarding"))
            });

            #endregion

            var cmd = new OnboardCollaboratorData(
                await this.CommandBus.Send(new FindCollaboratorDtoByUidAndByEditionIdAsync(this.UserAccessControlDto?.Collaborator?.Uid ?? Guid.Empty, this.EditionId, this.UserInterfaceLanguage)),
                await this.CommandBus.Send(new FindAllLanguagesDtosAsync(this.UserInterfaceLanguage)),
                await this.CommandBus.Send(new FindAllCountriesBaseDtosAsync(this.UserInterfaceLanguage)));

            return View(cmd);
        }

        /// <summary>Collaborators the data.</summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        /// <exception cref="DomainException"></exception>
        [HttpPost]
        public async Task<ActionResult> PlayerData(OnboardCollaboratorAccessData cmd)
        {
            if (this.UserAccessControlDto?.IsAttendeeCollaboratorOnboardingFinished() == true)
            {
                return RedirectToAction("Index", "Onboarding");
            }

            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.WelcomeTitle, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Messages.CompleteYourRegistration, Url.Action("Index", "Onboarding"))
            });

            #endregion

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
    }
}