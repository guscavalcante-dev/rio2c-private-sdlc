// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 06-28-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 08-25-2023
// ***********************************************************************
// <copyright file="HomeController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Linq;
using PlataformaRio2C.Infra.CrossCutting.Identity.Service;
using System.Threading.Tasks;
using System.Web.Mvc;
using MediatR;
using PlataformaRio2C.Infra.CrossCutting.Resources.Helpers;
using PlataformaRio2C.Infra.CrossCutting.Tools.Helpers;
using PlataformaRio2C.Infra.CrossCutting.Identity.AuthorizeAttributes;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Web.Site.Filters;
using System.Text.RegularExpressions;
using PlataformaRio2C.Application.Common;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using PlataformaRio2C.Web.Site.Services;
using Constants = PlataformaRio2C.Domain.Constants;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Application.ViewModels;
using System.Collections.Generic;

namespace PlataformaRio2C.Web.Site.Controllers
{
    /// <summary>HomeController</summary>
    [AjaxAuthorize(Order = 1)]
    [AuthorizeCollaboratorType(Order = 2)]
    public class HomeController : BaseController
    {
        private readonly IWeConnectPublicationRepository weConnectPublicationRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController" /> class.
        /// </summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="identityController">The identity controller.</param>
        /// <param name="weConnectPublicationRepository">The we connect publication repository.</param>
        public HomeController(
            IMediator commandBus, 
            IdentityAutenticationService identityController,
            IWeConnectPublicationRepository weConnectPublicationRepository)
            : base(commandBus, identityController)
        {
            this.weConnectPublicationRepo = weConnectPublicationRepository;
        }

        #region Dashboard

        /// <summary>Indexes the specified show email settings.</summary>
        /// <param name="showEmailSettings">The show email settings.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Index(bool? showEmailSettings = false)
        {
            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.Dashboard, null);

            #endregion

            ViewBag.ShowEmailSettings = showEmailSettings;

            return View("Index");
        }

        #endregion

        #region Talents

        /// <summary>Talentses this instance.</summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Talents()
        {
            TalentAccessDto talentAccessDto;

            try
            {
                var talentPlatformService = new TalentPlatformService();
                talentAccessDto = talentPlatformService.Login(this.UserAccessControlDto, this.UserInterfaceLanguage);
                if (string.IsNullOrEmpty(talentAccessDto?.Url))
                {
                    this.StatusMessageToastr(Messages.CouldNotSignInTalentsPlatform, Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);
                    return RedirectToAction("Index", "Home", new { Area = "" });
                }
            }
            catch (DomainException ex)
            {
                this.StatusMessageToastr(ex.GetInnerMessage(), Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);
                return RedirectToAction("Index", "Home", new { Area = "" });
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                this.StatusMessageToastr(Messages.WeFoundAndError, Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);
                return RedirectToAction("Index", "Home", new { Area = "" });
            }

            return Redirect(talentAccessDto.Url);
        }

        #endregion

        #region Culture

        /// <summary>Sets the culture.</summary>
        /// <param name="culture">The culture.</param>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<ActionResult> SetCulture(string culture, string returnUrl = null)
        {
            // Validate input
            culture = CultureHelper.GetImplementedCulture(culture);
            RouteData.Values["culture"] = culture;  // set culture

            #region Create/Update cookie culture

            if (this.UserAccessControlDto != null)
            {
                var result = await this.CommandBus.Send(new UpdateUserInterfaceLanguage(
                    this.UserAccessControlDto.User.Uid,
                    culture));
            }

            var cookie = ApplicationCookieControl.SetCookie(culture, Response.Cookies[Constants.CookieName.MyRio2CCookie], Constants.CookieName.MyRio2CCookie);
            Response.Cookies.Add(cookie);

            #endregion

            if (returnUrl == null && Request.UrlReferrer != null)
            {
                returnUrl = Request.UrlReferrer.PathAndQuery;
            }

            if (!string.IsNullOrEmpty(returnUrl) && CultureHelper.Cultures?.Any() == true)
            {
                foreach (var configuredCulture in CultureHelper.Cultures)
                {
                    returnUrl = Regex.Replace(returnUrl, configuredCulture, culture, RegexOptions.IgnoreCase);
                }
            }

            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        }

        #endregion

        #region Edition

        /// <summary>
        /// Sets the edition.
        /// </summary>
        /// <param name="urlCode">The URL code.</param>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult SetEdition(string urlCode, string returnUrl = null)
        {
            var oldUrlCode = RouteData.Values["edition"].ToString();

            RouteData.Values["edition"] = urlCode;  // set urlCode from Edition

            #region Create/Update cookie culture

            //if (this.AdminAccessControlDto != null)
            //{
            //    var result = await this.CommandBus.Send(new UpdateUserInterfaceLanguage(
            //        this.AdminAccessControlDto.User.Uid,
            //        culture));
            //}

            //var cookie = ApplicationCookieControl.SetCookie(culture, Response.Cookies[Constants.CookieName.MyRio2CCookie], Constants.CookieName.MyRio2CCookie);
            //Response.Cookies.Add(cookie);

            #endregion

            if (returnUrl == null && Request.UrlReferrer != null)
            {
                returnUrl = Request.UrlReferrer.PathAndQuery;
            }

            if (!string.IsNullOrEmpty(returnUrl))
            {
                returnUrl = Regex.Replace(returnUrl, oldUrlCode, urlCode, RegexOptions.IgnoreCase);
            }

            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        }

        #endregion

        #region We Connect

        /// <summary>
        /// Shows the we connect widget.
        /// </summary>
        /// <param name="weConnectSearchViewModel">The we connect search view model.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowWeConnectWidget(WeConnectSearchViewModel weConnectSearchViewModel)
        {
            var weConnectPublicationsDtos = await this.weConnectPublicationRepo.FindAllDtosPagedAsync(
                weConnectSearchViewModel.Page.Value,
                weConnectSearchViewModel.PageSize.Value);

            return Json(new
            {
                status = "success",
                hasNextPage = weConnectPublicationsDtos.HasNextPage,
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/WeConnectedWidget", weConnectPublicationsDtos), divIdOrClass = "#WeConnectWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Wes the connect widget load more.
        /// </summary>
        /// <param name="weConnectSearchViewModel">The we connect search view model.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> WeConnectWidgetLoadMore(WeConnectSearchViewModel weConnectSearchViewModel)
        {
            var weConnectPublicationsDtos = await this.weConnectPublicationRepo.FindAllDtosPagedAsync(
                weConnectSearchViewModel.Page.Value,
                weConnectSearchViewModel.PageSize.Value);

            return Json(new
            {
                status = "success",
                hasNextPage = weConnectPublicationsDtos.HasNextPage,
                page = this.RenderRazorViewToString("Shared/_WeConnectPublications", weConnectPublicationsDtos.ToList())
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}