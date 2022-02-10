// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Renan Valentim
// Created          : 02-09-2022
//
// Last Modified By : Renan Valentim
// Last Modified On : 02-09-2022
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
using PlataformaRio2C.Application;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions;
using PlataformaRio2C.Web.Admin.Controllers;
using PlataformaRio2C.Web.Admin.Filters;
using Constants = PlataformaRio2C.Domain.Constants;
using System.Text;
using PlataformaRio2C.Domain.Dtos;

namespace PlataformaRio2C.Web.Admin.Areas.Cartoon.Controllers
{
    /// <summary>ProjectsController</summary>
    [AjaxAuthorize(Order = 1, Roles = Constants.Role.AnyAdmin)]
    [AuthorizeCollaboratorType(Order = 2, Types = Constants.CollaboratorType.AdminCartoon)]
    public class ProjectsController : BaseController
    {
        private readonly IProjectEvaluationStatusRepository evaluationStatusRepo;
        private readonly IAttendeeCartoonProjectRepository attendeeCartoonProjectRepo;
        private readonly ICartoonProjectFormatRepository cartoonProjectFormatRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectsController" /> class.
        /// </summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="identityController">The identity controller.</param>
        /// <param name="projectEvaluationStatusRepository">The project evaluation status repository.</param>
        /// <param name="attendeeCartoonProjectRepository">The attendee cartoon project repository.</param>
        /// <param name="cartoonProjectFormatRepository">The cartoon project format repository.</param>
        public ProjectsController(
            IMediator commandBus,
            IdentityAutenticationService identityController,
            IProjectEvaluationStatusRepository projectEvaluationStatusRepository,
            IAttendeeCartoonProjectRepository attendeeCartoonProjectRepository,
            ICartoonProjectFormatRepository cartoonProjectFormatRepository)
            : base(commandBus, identityController)
        {
            this.evaluationStatusRepo = projectEvaluationStatusRepository;
            this.attendeeCartoonProjectRepo = attendeeCartoonProjectRepository;
            this.cartoonProjectFormatRepo = cartoonProjectFormatRepository;
        }

        #region List

        /// <summary>
        /// Indexes the specified evaluation status uid.
        /// </summary>
        /// <param name="projectFormatUid">The project format uid.</param>
        /// <param name="evaluationStatusUid">The evaluation status uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Index(Guid? projectFormatUid, Guid? evaluationStatusUid)
        {
            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.CartoonProjects, new List<BreadcrumbItemHelper>{
                new BreadcrumbItemHelper(Labels.Cartoonito, null),
                new BreadcrumbItemHelper(Labels.Projects, Url.Action("Index", "Projects", new { Area = "Cartoon" }))
            });

            #endregion

            ViewBag.ProjectFormatUid = projectFormatUid;
            ViewBag.EvaluationStatusUid = evaluationStatusUid;
            ViewBag.Page = 1;
            ViewBag.PageSize = 10;

            ViewBag.ProjectFormats = (await this.cartoonProjectFormatRepo.FindAllAsync()).GetSeparatorTranslation(m => m.Name, this.UserInterfaceLanguage, '|');
            ViewBag.ProjectEvaluationStatuses = (await this.evaluationStatusRepo.FindAllAsync()).GetSeparatorTranslation(m => m.Name, this.UserInterfaceLanguage, '|');

            return View();
        }

        /// <summary>
        /// Shows the list widget.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="evaluationStatusUid">The evaluation status uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowListWidget(IDataTablesRequest request, Guid? projectFormatUid, Guid? evaluationStatusUid)
        {
            int page = request.Start / request.Length;
            int pageSize = request.Length;
            page++; //Necessary because DataTable is zero index based.

            var cartoonProjectJsonDtos = await this.attendeeCartoonProjectRepo.FindAllJsonDtosPagedAsync(
                this.EditionDto.Id,
                request.Search?.Value,
                new List<Guid?> { projectFormatUid },
                evaluationStatusUid,
                page,
                pageSize,
                request.GetSortColumns());

            var approvedAttendeeCartoonProjectsIds = await this.attendeeCartoonProjectRepo.FindAllApprovedAttendeeCartoonProjectsIdsAsync(this.EditionDto.Id);

            foreach (var cartoonProjectJsonDto in cartoonProjectJsonDtos)
            {
                cartoonProjectJsonDto.IsApproved = approvedAttendeeCartoonProjectsIds.Contains(cartoonProjectJsonDto.AttendeeCartoonProjectId);

                #region Translate CartoonProjectFormatName

                cartoonProjectJsonDto.CartoonProjectFormatName = cartoonProjectJsonDto.CartoonProjectFormatName.GetSeparatorTranslation(this.UserInterfaceLanguage, '|');

                #endregion
            }

            ViewBag.ProjectFormatUid = projectFormatUid;
            ViewBag.EvaluationStatusUid = evaluationStatusUid;
            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;

            IDictionary<string, object> additionalParameters = new Dictionary<string, object>();
            if (cartoonProjectJsonDtos.TotalItemCount <= 0)
            {
                if (this.EditionDto.IsCartoonProjectEvaluationOpen() && (
                    evaluationStatusUid == ProjectEvaluationStatus.Accepted.Uid ||
                    evaluationStatusUid == ProjectEvaluationStatus.Refused.Uid))
                {
                    additionalParameters.Add("noRecordsFoundMessage",
                        $"{string.Format(Messages.TheEvaluationPeriodRunsFrom, this.EditionDto.CartoonCommissionEvaluationStartDate.ToBrazilTimeZone().ToShortDateString(), this.EditionDto.CartoonCommissionEvaluationEndDate.ToBrazilTimeZone().ToShortDateString())}.</br>{Messages.TheProjectsWillReceiveFinalGradeAtPeriodEnds}");
                }
                else if (!this.EditionDto.IsCartoonProjectEvaluationOpen() &&
                    evaluationStatusUid == ProjectEvaluationStatus.UnderEvaluation.Uid)
                {
                    additionalParameters.Add("noRecordsFoundMessage",
                        $"{Messages.EvaluationPeriodClosed}<br/>{string.Format(Messages.ProjectsNotFoundWithStatus, Labels.UnderEvaluation)}");
                }
            }

            var response = DataTablesResponse.Create(request, cartoonProjectJsonDtos.TotalItemCount, cartoonProjectJsonDtos.TotalItemCount, cartoonProjectJsonDtos, additionalParameters);

            return Json(new
            {
                status = "success",
                dataTable = response,
                searchKeywords = request.Search?.Value,
                projectFormatUid,
                evaluationStatusUid,
                page,
                pageSize
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Total Count Widget

        /// <summary>Shows the total count widget.</summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowTotalCountWidget()
        {
            var projectsCount = await this.attendeeCartoonProjectRepo.CountAsync(this.EditionDto.Id, true);

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/TotalCountWidget", projectsCount), divIdOrClass = "#CartoonProjectsTotalCountWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Edition Count Widget

        /// <summary>Shows the edition count widget.</summary>
        /// <returns></returns>
        public async Task<ActionResult> ShowEditionCountWidget()
        {
            var projectsCount = await this.attendeeCartoonProjectRepo.CountAsync(this.EditionDto.Id);
            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/EditionCountWidget", projectsCount), divIdOrClass = "#CartoonProjectsEditionCountWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion     
    }
}