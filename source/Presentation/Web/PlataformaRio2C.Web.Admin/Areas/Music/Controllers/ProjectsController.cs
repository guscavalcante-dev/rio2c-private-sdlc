// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 03-01-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-01-2020
// ***********************************************************************
// <copyright file="ProjectsController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using DataTables.AspNet.Core;
using DataTables.AspNet.Mvc5;
using MediatR;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.CrossCutting.Identity.AuthorizeAttributes;
using PlataformaRio2C.Infra.CrossCutting.Identity.Service;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using PlataformaRio2C.Infra.CrossCutting.Tools.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using PlataformaRio2C.Web.Admin.Controllers;
using Constants = PlataformaRio2C.Domain.Constants;

namespace PlataformaRio2C.Web.Admin.Areas.Music.Controllers
{
    /// <summary>ProjectsController</summary>
    [AjaxAuthorize(Order = 1, Roles = Constants.Role.Admin)]
    //[AuthorizeCollaboratorType(Order = 2, Types = Constants.CollaboratorType.Admin + "," + Constants.CollaboratorType.CommissionAudiovisual)]
    public class ProjectsController : BaseController
    {
        private readonly IMusicProjectRepository musicProjectRepo;
        private readonly IMusicGenreRepository musicGenreRepo;
        private readonly IProjectEvaluationStatusRepository evaluationStatusRepo;

        /// <summary>Initializes a new instance of the <see cref="ProjectsController"/> class.</summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="identityController">The identity controller.</param>
        /// <param name="musicProjectRepository">The music project repository.</param>
        /// <param name="musicGenreRepository">The music genre repository.</param>
        /// <param name="evaluationStatusRepository">The evaluation status repository.</param>
        public ProjectsController(
            IMediator commandBus,
            IdentityAutenticationService identityController,
            IMusicProjectRepository musicProjectRepository,
            IMusicGenreRepository musicGenreRepository,
            IProjectEvaluationStatusRepository evaluationStatusRepository)
            : base(commandBus, identityController)
        {
            this.musicProjectRepo = musicProjectRepository;
            this.musicGenreRepo = musicGenreRepository;
            this.evaluationStatusRepo = evaluationStatusRepository;
        }

        #region List

        /// <summary>Indexes the specified search keywords.</summary>
        /// <param name="musicGenreUid">The music genre uid.</param>
        /// <param name="evaluationStatusUid">The evaluation status uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Index(Guid? musicGenreUid, Guid? evaluationStatusUid)
        {
            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.PitchingShow, new List<BreadcrumbItemHelper>{
                new BreadcrumbItemHelper(Labels.Projects, Url.Action("Index", "Projects", new { Area = "Music" }))
            });

            #endregion

            ViewBag.MusicGenreUid = musicGenreUid;
            ViewBag.EvaluationStatusUid = evaluationStatusUid;
            ViewBag.Page = 1;
            ViewBag.PageSize = 10;

            ViewBag.MusicGenres = (await this.musicGenreRepo.FindAllAsync()).GetSeparatorTranslation(m => m.Name, this.UserInterfaceLanguage, '|');
            ViewBag.ProjectEvaluationStatuses = (await this.evaluationStatusRepo.FindAllAsync()).GetSeparatorTranslation(m => m.Name, this.UserInterfaceLanguage, '|');

            return View();
        }

        /// <summary>Shows the list widget.</summary>
        /// <param name="request">The request.</param>
        /// <param name="musicGenreUid">The music genre uid.</param>
        /// <param name="evaluationStatusUid">The evaluation status uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowListWidget(IDataTablesRequest request, Guid? musicGenreUid, Guid? evaluationStatusUid)
        {
            var musicProjects = await this.musicProjectRepo.FindAllJsonDtosPagedAsync(
                request.Start / request.Length,
                request.Length,
                request.GetSortColumns(),
                request.Search?.Value,
                musicGenreUid,
                evaluationStatusUid,
                this.UserInterfaceLanguage,
                this.EditionDto.Id);

            // Translate Target Audiences
            foreach (var musicProjectJsonDto in musicProjects)
            {
                musicProjectJsonDto.EvaluationStatusName = musicProjectJsonDto.EvaluationStatusName.GetSeparatorTranslation(this.UserInterfaceLanguage, '|');

                for (int i = 0; i < musicProjectJsonDto.MusicTargetAudiencesNames.Count(); i++)
                {
                    musicProjectJsonDto.MusicTargetAudiencesNames[i] = musicProjectJsonDto.MusicTargetAudiencesNames[i].GetSeparatorTranslation(this.UserInterfaceLanguage, '|');
                }
            }

            ViewBag.MusicGenreUid = musicGenreUid;
            ViewBag.EvaluationStatusUid = evaluationStatusUid;
            ViewBag.Page = request.Start / request.Length;
            ViewBag.PageSize = request.Length;

            var response = DataTablesResponse.Create(request, musicProjects.TotalItemCount, musicProjects.TotalItemCount, musicProjects);

            return Json(new
            {
                status = "success",
                dataTable = response
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Total Count Widget

        /// <summary>Shows the total count widget.</summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowTotalCountWidget()
        {
            var projectsCount = await this.musicProjectRepo.CountAsync(this.EditionDto.Id, true);

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/TotalCountWidget", projectsCount), divIdOrClass = "#MusicProjectsTotalCountWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Edition Count Widget

        /// <summary>Shows the edition count widget.</summary>
        /// <returns></returns>
        public async Task<ActionResult> ShowEditionCountWidget()
        {
            var projectsCount = await this.musicProjectRepo.CountAsync(this.EditionDto.Id);
            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/EditionCountWidget", projectsCount), divIdOrClass = "#MusicProjectsEditionCountWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}