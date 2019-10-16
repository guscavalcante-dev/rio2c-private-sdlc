// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 06-28-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 10-16-2019
// ***********************************************************************
// <copyright file="ProjectsController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Application.Interfaces.Services;
using System.Web.Mvc;
using MediatR;
using PlataformaRio2C.Infra.CrossCutting.Identity.Service;
using System.Collections.Generic;
using PlataformaRio2C.Infra.CrossCutting.Identity.AuthorizeAttributes;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Helpers;
using PlataformaRio2C.Web.Site.Filters;
using Constants = PlataformaRio2C.Domain.Constants;

namespace PlataformaRio2C.Web.Site.Controllers
{
    /// <summary>ProjectsController</summary>
    [AjaxAuthorize(Order = 1)]
    [AuthorizeCollaboratorType(Order = 2, Types = Constants.CollaboratorType.ExecutiveAudiovisual + "," + Constants.CollaboratorType.Industry )]
    public class ProjectsController : BaseController
    {
        private readonly IProjectAppService _projectAppService;

        /// <summary>Initializes a new instance of the <see cref="ProjectsController"/> class.</summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="identityController">The identity controller.</param>
        /// <param name="projectAppService">The project application service.</param>
        public ProjectsController(IMediator commandBus, IdentityAutenticationService identityController, IProjectAppService projectAppService)
            : base(commandBus, identityController)
        {
            _projectAppService = projectAppService;
        }

        /// <summary>Indexes this instance.</summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
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

            #region Breadcrumb

                ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.AudiovisualProjects, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Labels.AudiovisualProjects, Url.Action("Index", "Projects", new { Area = "" }))
            });

            #endregion

            return View();
        }

        #region Industry

        /// <summary>Submiteds this instance.</summary>
        /// <returns></returns>
        [AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.Industry)]
        public ActionResult Submited()
        {
            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper("Submited Projects", new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper("Projects", Url.Action("Index", "Project", new { Area = "Player" }))
            });

            #endregion
            return View();
        }

        /// <summary>Submits this instance.</summary>
        /// <returns></returns>
        [AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.Industry)]
        public ActionResult Submit()
        {
            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper("Submit your projects", new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper("Projects", Url.Action("Index", "Project", new { Area = "Player" }))
            });

            #endregion
            return View();
        }

        #endregion

        #region Executive Audiovisual

        /// <summary>Reviews this instance.</summary>
        /// <returns></returns>
        [AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.ExecutiveAudiovisual)]
        public ActionResult Review()
        {
            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper("Projects for review", new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper("Projects", Url.Action("Index", "Project", new { Area = "Player" }))
            });

            #endregion
            return View();
        }

        #endregion

        //public ActionResult Detail(Guid uid)
        //{
        //    if (uid != Guid.Empty)
        //    {
        //        int userId = User.Identity.GetUserId<int>();
        //        var result = _projectAppService.GetForEvaluationPlayer(userId, uid);

        //        if (result != null)
        //        {
        //            return View(result);
        //        }
        //    }

        //    this.StatusMessage("Projeto não encotrado!", Infra.CrossCutting.Tools.Enums.StatusMessageType.Danger);

        //    return RedirectToAction("Index");
        //}
    }
}