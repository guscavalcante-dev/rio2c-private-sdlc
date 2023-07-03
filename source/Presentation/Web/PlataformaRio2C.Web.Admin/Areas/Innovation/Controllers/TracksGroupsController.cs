// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Renan Valentim
// Created          : 07-03-2023
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-03-2023
// ***********************************************************************
// <copyright file="TracksGroupsController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using DataTables.AspNet.Core;
using DataTables.AspNet.Mvc5;
using MediatR;
using PlataformaRio2C.Application.ViewModels;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.CrossCutting.Identity.AuthorizeAttributes;
using PlataformaRio2C.Infra.CrossCutting.Identity.Service;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using PlataformaRio2C.Infra.CrossCutting.Tools.Helpers;
using PlataformaRio2C.Web.Admin.Controllers;
using PlataformaRio2C.Web.Admin.Filters;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using Constants = PlataformaRio2C.Domain.Constants;

namespace PlataformaRio2C.Web.Admin.Areas.Innovation.Controllers
{
    [AjaxAuthorize(Order = 1, Roles = Constants.Role.AnyAdmin)]
    [AuthorizeCollaboratorType(Order = 2, Types = Constants.CollaboratorType.AdminInnovation)]
    public class TracksGroupsController : BaseController
    {
        private readonly IInnovationOrganizationTrackOptionGroupRepository innovationOrganizationTrackOptionGroupRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="TracksGroupsController"/> class.
        /// </summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="identityControlle">The identity controlle.</param>
        /// <param name="innovationOrganizationTrackOptionGroupRepository">The innovation organization track option group repository.</param>
        public TracksGroupsController(
            IMediator commandBus, 
            IdentityAutenticationService identityControlle,
            IInnovationOrganizationTrackOptionGroupRepository innovationOrganizationTrackOptionGroupRepository) 
            : base(commandBus, identityControlle)
        {
            this.innovationOrganizationTrackOptionGroupRepo = innovationOrganizationTrackOptionGroupRepository;
        }

        #region List

        /// <summary>
        /// Indexes the specified search view model.
        /// </summary>
        /// <param name="searchViewModel">The search view model.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Index(InnovationTrackGroupSearchViewModel searchViewModel)
        {
            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.InnovationVerticals, new List<BreadcrumbItemHelper>{
                new BreadcrumbItemHelper(Labels.Innovation, null),
                new BreadcrumbItemHelper(Labels.Verticals, Url.Action("Index", "Tracks", new { Area = "Innovation" }))
            });

            #endregion

            return View(searchViewModel);
        }

        /// <summary>
        /// Searches the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="searchViewModel">The search view model.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Search(IDataTablesRequest request)
        {
            int page = request.Start / request.Length;
            int pageSize = request.Length;
            page++; //Necessary because DataTable is zero index based.

            var innovationOrganizationTrackOptionGroupDtos = await this.innovationOrganizationTrackOptionGroupRepo.FindAllByDataTable(
                page,
                pageSize,
                request.Search?.Value,
                request.GetSortColumns());

            var response = DataTablesResponse.Create(request, innovationOrganizationTrackOptionGroupDtos.TotalItemCount, innovationOrganizationTrackOptionGroupDtos.TotalItemCount, innovationOrganizationTrackOptionGroupDtos);

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
            var tracksCount = await this.innovationOrganizationTrackOptionGroupRepo.CountAllByDataTable(true, this.EditionDto.Id);

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/TotalCountWidget", tracksCount), divIdOrClass = "#InnovationTracksGroupsTotalCountWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Edition Count Widget

        /// <summary>Shows the edition count widget.</summary>
        /// <returns></returns>
        public async Task<ActionResult> ShowEditionCountWidget()
        {
            var tracksCount = await this.innovationOrganizationTrackOptionGroupRepo.CountAllByDataTable(false, this.EditionDto.Id);

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/EditionCountWidget", tracksCount), divIdOrClass = "#InnovationTracksGroupsEditionCountWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}