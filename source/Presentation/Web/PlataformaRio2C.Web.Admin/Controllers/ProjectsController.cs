//// ***********************************************************************
//// Assembly         : PlataformaRio2C.Web.Admin
//// Author           : Rafael Dantas Ruiz
//// Created          : 06-28-2019
////
//// Last Modified By : William Sergio Almado Junior
//// Last Modified On : 12-12-2019
//// ***********************************************************************
//// <copyright file="ProjectsController.cs" company="Softo">
////     Copyright (c) Softo. All rights reserved.
//// </copyright>
//// <summary></summary>
//// ***********************************************************************
using DataTables.AspNet.Core;
using DataTables.AspNet.Mvc5;
using MediatR;
using Newtonsoft.Json;
using PlataformaRio2C.Application.ViewModels;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.CrossCutting.Identity.AuthorizeAttributes;
using PlataformaRio2C.Infra.CrossCutting.Identity.Service;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Helpers;
using PlataformaRio2C.Web.Admin.Filters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using Constants = PlataformaRio2C.Domain.Constants;

namespace PlataformaRio2C.Web.Admin.Controllers
{
    /// <summary>ProjectsController</summary>
    [AjaxAuthorize(Order = 1, Roles = Constants.Role.AnyAdmin)]
    [AuthorizeCollaboratorType(Order = 2, Types = Constants.CollaboratorType.AdminAudiovisual)] //TODO: Definir roles
    public class ProjectsController : BaseController
    {

        private readonly IProjectRepository projectRepo;
        private readonly IInterestRepository interestRepo;
        private readonly IActivityRepository activityRepo;

        /// <summary>Initializes a new instance of the <see cref="ProjectsController"/> class.</summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="identityController">The identity controller.</param>
        /// <param name="projectRepository">The project repository.</param>
        /// <param name="interestRepository">The interest repository.</param>
        public ProjectsController(
            IMediator commandBus,
            IdentityAutenticationService identityController,
            IProjectRepository projectRepository,
            IInterestRepository interestRepository)
            : base(commandBus, identityController)
        {
            this.projectRepo = projectRepository;
            this.interestRepo = interestRepository;
        }

        #region List

        /// <summary>Indexes the specified search view model.</summary>
        /// <param name="searchViewModel">The search view model.</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.Producers, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Labels.Project, Url.Action("Index", "Project", new { Area = "" })),
                new BreadcrumbItemHelper(Labels.ProjectsForPitching, Url.Action("Index", "ProjectsPitchingList", new { Area = "" }))
            });

            #endregion

            return View();
        }

        #endregion


        #region Pitching
        /// <summary>Pitching project list</summary>
        /// <param name="searchKeywords"></param>
        /// <param name="interestUid"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ProjectsPitchingList(string searchKeywords, Guid? interestUid, int? page = 1, int? pageSize = 10)
        {
            #region Breadcrumb
            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.Producers, new List<BreadcrumbItemHelper>{
                new BreadcrumbItemHelper(Labels.ProjectsForPitching, Url.Action("ProjectsPitchingList", "Projects", new { Area = "" }))
            });

            #endregion

            ViewBag.SearchKeywords = searchKeywords;
            ViewBag.InterestUid = interestUid;
            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;

            ViewBag.GenreInterests = await this.interestRepo.FindAllDtosByInterestGroupUidAsync(InterestGroup.Genre.Uid);

            return View();
        }


        /// <summary>Shows the pitching project list.</summary>
        /// <param name="searchKeywords">The search keywords.</param>
        /// <param name="interestUid">The interest uid.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowPitchingListWidget(IDataTablesRequest request, Guid? interestUid, int? page = 1, int? pageSize = 10)
        {
            var projects = await this.projectRepo.FindAllPitchingProjectsDtoAsync(request.Search?.Value, interestUid, this.UserInterfaceLanguage, page.Value, pageSize.Value);

            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;

            var response = DataTablesResponse.Create(request, projects.TotalItemCount, projects.TotalItemCount, projects);

            return Json(new
            {
                status = "success",
                dataTable = response
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}