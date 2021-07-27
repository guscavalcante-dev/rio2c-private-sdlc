// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Renan Valentim
// Created          : 07-24-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-24-2021
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

namespace PlataformaRio2C.Web.Admin.Areas.Innovation.Controllers
{
    /// <summary>ProjectsController</summary>
    [AjaxAuthorize(Order = 1, Roles = Constants.Role.AnyAdmin)]
    [AuthorizeCollaboratorType(Order = 2, Types = Constants.CollaboratorType.AdminInnovation)]
    public class ProjectsController : BaseController
    {
        private readonly IMusicProjectRepository musicProjectRepo;
        private readonly IProjectEvaluationStatusRepository evaluationStatusRepo;
        private readonly IAttendeeInnovationOrganizationRepository attendeeInnovationOrganizationRepo;
        private readonly IInnovationOrganizationTrackOptionRepository innovationOrganizationTrackOptionRepo;
        private readonly IInnovationOrganizationObjectivesOptionRepository innovationOrganizationObjectivesOptionRepo;
        private readonly IInnovationOrganizationTechnologyOptionRepository innovationOrganizationTechnologyOptionRepo;
        private readonly IInnovationOrganizationExperienceOptionRepository innovationOrganizationExperienceOptionRepo;

        public ProjectsController(
            IMediator commandBus,
            IdentityAutenticationService identityController,
            IMusicProjectRepository musicProjectRepository,
            IProjectEvaluationStatusRepository evaluationStatusRepository,
            IAttendeeInnovationOrganizationRepository attendeeInnovationOrganizationRepository,
            IInnovationOrganizationTrackOptionRepository innovationOrganizationTrackOptionRepository,
            IInnovationOrganizationObjectivesOptionRepository innovationOrganizationObjectivesOptionRepository,
            IInnovationOrganizationTechnologyOptionRepository innovationOrganizationTechnologyOptionRepository,
            IInnovationOrganizationExperienceOptionRepository innovationOrganizationExperienceOptionRepository
            )
            : base(commandBus, identityController)
        {
            this.musicProjectRepo = musicProjectRepository;
            this.evaluationStatusRepo = evaluationStatusRepository;
            this.attendeeInnovationOrganizationRepo = attendeeInnovationOrganizationRepository;
            this.innovationOrganizationTrackOptionRepo = innovationOrganizationTrackOptionRepository;
            this.innovationOrganizationObjectivesOptionRepo = innovationOrganizationObjectivesOptionRepository;
            this.innovationOrganizationTechnologyOptionRepo = innovationOrganizationTechnologyOptionRepository;
            this.innovationOrganizationExperienceOptionRepo = innovationOrganizationExperienceOptionRepository;
        }

        #region List

        /// <summary>
        /// Indexes the specified innovation organization track uid.
        /// </summary>
        /// <param name="innovationOrganizationTrackUid">The innovation organization track uid.</param>
        /// <param name="evaluationStatusUid">The evaluation status uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Index(Guid? innovationOrganizationTrackUid, Guid? evaluationStatusUid)
        {
            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.InnovationProjects, new List<BreadcrumbItemHelper>{
                new BreadcrumbItemHelper(Labels.Innovation, null),
                new BreadcrumbItemHelper(Labels.Projects, Url.Action("Index", "Projects", new { Area = "Innovation" }))
            });

            #endregion

            ViewBag.InnovationOrganizationTrackUid = innovationOrganizationTrackUid;
            ViewBag.EvaluationStatusUid = evaluationStatusUid;
            ViewBag.Page = 1;
            ViewBag.PageSize = 10;

            ViewBag.InnovationOrganizationTrackOptions = (await this.innovationOrganizationTrackOptionRepo.FindAllAsync()).GetSeparatorTranslation(m => m.Name, this.UserInterfaceLanguage, '|');
            ViewBag.ProjectEvaluationStatuses = (await this.evaluationStatusRepo.FindAllAsync()).GetSeparatorTranslation(m => m.Name, this.UserInterfaceLanguage, '|');

            return View();
        }

        /// <summary>Shows the list widget.</summary>
        /// <param name="request">The request.</param>
        /// <param name="innovationOrganizationTrackUid">The music genre uid.</param>
        /// <param name="evaluationStatusUid">The evaluation status uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowListWidget(IDataTablesRequest request, Guid? innovationOrganizationTrackUid, Guid? evaluationStatusUid)
        {
            int page = request.Start / request.Length;
            int pageSize = request.Length;
            page++; //Necessary because DataTable is zero index based.

            var attendeeInnovationOrganizationJsonDtos = await this.attendeeInnovationOrganizationRepo.FindAllJsonDtosPagedAsync(
                this.EditionDto.Id,
                request.Search?.Value,
                innovationOrganizationTrackUid,
                evaluationStatusUid,
                page,
                pageSize,
                request.GetSortColumns());

            var approvedAttendeeInnovationOrganizationIds = await this.attendeeInnovationOrganizationRepo.FindAllApprovedAttendeeInnovationOrganizationsIdsAsync(this.EditionDto.Id);

            StringBuilder sb = new StringBuilder();
            foreach (var attendeeInnovationOrganizationJsonDto in attendeeInnovationOrganizationJsonDtos)
            {
                #region Evaluation Column

                var icon = "fa-diagnoses";
                var color = "warning";
                var text = Labels.UnderEvaluation;
                bool isInnovationProjectEvaluationClosed = !this.EditionDto.IsInnovationProjectEvaluationOpen();

                if (isInnovationProjectEvaluationClosed)
                {
                    if (approvedAttendeeInnovationOrganizationIds.Contains(attendeeInnovationOrganizationJsonDto.AttendeeInnovationOrganizationId))
                    {
                        icon = "fa-thumbs-up";
                        color = "success";
                        text = Labels.ProjectAccepted;
                    }
                    else
                    {
                        icon = "fa-thumbs-down";
                        color = "danger";
                        text = Labels.ProjectRefused;
                    }
                }

                sb.Append($"<table class=\"image-side-text\">");
                sb.Append($"    <tr>");
                sb.Append($"        <td>");
                sb.Append($"            <div class=\"col-md-12 justify-content-center\">");
                sb.Append($"                <span class=\"kt-widget__button\" style=\"\" data-toggle=\"tooltip\" title=\"{text}\">");
                sb.Append($"                    <label class=\"btn btn-label-{color} btn-sm\">");
                sb.Append($"                        <i class=\"fa {icon}\"></i>");
                sb.Append($"                    </label>");
                sb.Append($"                </span>");
                if (isInnovationProjectEvaluationClosed)
                {
                    sb.Append($"            <span class=\"margin-left: 5px;\">");
                    sb.Append($"                <b>{attendeeInnovationOrganizationJsonDto.Grade?.ToString() ?? "-"}</b>");
                    sb.Append($"            </span>");
                    sb.Append("<br/>");
                }
                sb.Append($"                <span class=\"margin-left: 5px;\">");
                sb.Append($"                    ({attendeeInnovationOrganizationJsonDto.EvaluationsCount} {(attendeeInnovationOrganizationJsonDto.EvaluationsCount == 1 ? Labels.Vote : Labels.Votes)})");
                sb.Append($"                </span>");
                sb.Append($"            </div>");
                sb.Append($"        </td>");
                sb.Append($"    </tr>");
                sb.Append($"</table>");
                attendeeInnovationOrganizationJsonDto.EvaluationHtmlString = sb.ToString();
                sb.Clear();

                #endregion

                #region Menu Actions Column

                sb.Append($"<span class=\"dropdown\">");
                sb.Append($"     <a href = \"#\" class=\"btn btn-sm btn-clean btn-icon btn-icon-md\" data-toggle=\"dropdown\" aria-expanded=\"true\">");
                sb.Append($"         <i class=\"la la-ellipsis-h\"></i>");
                sb.Append($"     </a>");
                sb.Append($"     <div class=\"dropdown-menu dropdown-menu-right\">");
                sb.Append($"        <button class=\"dropdown-item\" onclick=\"InnovationProjectsDataTableWidget.showDetails({attendeeInnovationOrganizationJsonDto.AttendeeInnovationOrganizationId}, '', '{innovationOrganizationTrackUid}', '{evaluationStatusUid}', '{page}', '{pageSize}');\">");
                sb.Append($"            <i class=\"la la-eye\"></i> {@Labels.View}");
                sb.Append($"        </button>");
                sb.Append($"        <button class=\"dropdown-item\" onclick=\"InnovationProjectsDelete.showModal('{attendeeInnovationOrganizationJsonDto.AttendeeInnovationOrganizationUid}');\">");
                sb.Append($"            <i class=\"la la-remove\"></i> {Labels.Remove}");
                sb.Append($"        </button>");
                sb.Append($"    </div>");
                sb.Append($"</span>");
                attendeeInnovationOrganizationJsonDto.MenuActionsHtmlString = sb.ToString();
                sb.Clear();

                #endregion

                #region Translate InnovationOrganizationTracksNames

                for (int i = 0; i < attendeeInnovationOrganizationJsonDto.InnovationOrganizationTracksNames.Count(); i++)
                {
                    attendeeInnovationOrganizationJsonDto.InnovationOrganizationTracksNames[i] = attendeeInnovationOrganizationJsonDto.InnovationOrganizationTracksNames[i].GetSeparatorTranslation(this.UserInterfaceLanguage, '|');
                }

                #endregion
            }

            ViewBag.InnovationOrganizationTrackUid = innovationOrganizationTrackUid;
            ViewBag.EvaluationStatusUid = evaluationStatusUid;
            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;

            IDictionary<string, object> additionalParameters = new Dictionary<string, object>();
            if (attendeeInnovationOrganizationJsonDtos.TotalItemCount <= 0)
            {
                if (this.EditionDto.IsInnovationProjectEvaluationOpen() && (
                    evaluationStatusUid == ProjectEvaluationStatus.Accepted.Uid ||
                    evaluationStatusUid == ProjectEvaluationStatus.Refused.Uid))
                {
                    additionalParameters.Add("noRecordsFoundMessage", 
                        $"{string.Format(Messages.TheEvaluationPeriodRunsFrom, this.EditionDto.InnovationProjectEvaluationStartDate.ToBrazilTimeZone().ToShortDateString(), this.EditionDto.InnovationProjectEvaluationEndDate.ToBrazilTimeZone().ToShortDateString())}.</br>{Messages.TheProjectsWillReceiveFinalGradeAtPeriodEnds}");
                }
                else if (!this.EditionDto.IsInnovationProjectEvaluationOpen() && 
                    evaluationStatusUid == ProjectEvaluationStatus.UnderEvaluation.Uid)
                {
                    additionalParameters.Add("noRecordsFoundMessage", 
                        $"{Messages.EvaluationPeriodClosed}<br/>{string.Format(Messages.ProjectsNotFoundWithStatus, Labels.UnderEvaluation)}");
                }
            }

            var response = DataTablesResponse.Create(request, attendeeInnovationOrganizationJsonDtos.TotalItemCount, attendeeInnovationOrganizationJsonDtos.TotalItemCount, attendeeInnovationOrganizationJsonDtos, additionalParameters);

            return Json(new
            {
                status = "success",
                dataTable = response
            }, JsonRequestBehavior.AllowGet);
        }              

        /// <summary>Export to Excel the evaluation list widget.</summary>
        /// <param name="searchKeywords">The search keywords.</param>
        /// <param name="innovationOrganizationTrackUid">The music genre uid.</param>
        /// <param name="evaluationStatusUid">The evaluation status uid.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ExportEvaluationListWidget(string searchKeywords, Guid? innovationOrganizationTrackUid, Guid? evaluationStatusUid, int? page = 1, int? pageSize = 1000)
        {
            StringBuilder data = new StringBuilder();
            data.AppendLine($"{Labels.Startup}; {Labels.Project}; {Labels.ProductsOrServices}; {Labels.Status}; {Labels.Votes}; {Labels.Average}");

            var attendeeInnovationOrganizationJsonDtos = await this.attendeeInnovationOrganizationRepo.FindAllJsonDtosPagedAsync(this.EditionDto.Id, searchKeywords, innovationOrganizationTrackUid, evaluationStatusUid, 1, 10000, new List<Tuple<string, string>>());
            var approvedAttendeeInnovationOrganizationIds = await this.attendeeInnovationOrganizationRepo.FindAllApprovedAttendeeInnovationOrganizationsIdsAsync(this.EditionDto.Id);

            foreach (var attendeeInnovationOrganizationJsonDto in attendeeInnovationOrganizationJsonDtos)
            {
                data.AppendLine(attendeeInnovationOrganizationJsonDto.InnovationOrganizationName + ";" +
                                attendeeInnovationOrganizationJsonDto.InnovationOrganizationServiceName + ";" +
                                string.Join(",", attendeeInnovationOrganizationJsonDto.InnovationOrganizationTracksNames) + ";" +
                                (approvedAttendeeInnovationOrganizationIds.Contains(attendeeInnovationOrganizationJsonDto.AttendeeInnovationOrganizationId) ? Labels.ProjectAccepted : Labels.ProjectRefused) + ";" +
                                attendeeInnovationOrganizationJsonDto.EvaluationsCount + ";" +
                                attendeeInnovationOrganizationJsonDto?.Grade ?? "-");
            }

            return Json(new
            {
                fileName = "InnovationProjects_"+ DateTime.Now.ToString("yyyyMMddHHmmss") + ".csv",
                fileContent = data.ToString()
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>Export to Excel the evaluators list widget.</summary>
        /// <param name="searchKeywords">The search keywords.</param>
        /// <param name="innovationOrganizationTrackUid">The music genre uid.</param>
        /// <param name="evaluationStatusUid">The evaluation status uid.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ExportEvaluatorsListWidget(string searchKeywords, Guid? innovationOrganizationTrackUid, Guid? evaluationStatusUid, int? page = 1, int? pageSize = 1000)
        {
            StringBuilder data = new StringBuilder();
            data.AppendLine($"{Labels.Startup}; {Labels.Project}; {Labels.Evaluation}; {Labels.Evaluator};");

            var attendeeInnovationOrganizationJsonDtos = await this.attendeeInnovationOrganizationRepo.FindAllJsonDtosPagedAsync(this.EditionDto.Id, searchKeywords, innovationOrganizationTrackUid, evaluationStatusUid, 1, 1000, new List<Tuple<string, string>>());
            foreach (var attendeeInnovationOrganizationJsonDto in attendeeInnovationOrganizationJsonDtos)
            {
                var attendeeInnovationEvaluationDto = await this.attendeeInnovationOrganizationRepo.FindEvaluatorsWidgetDtoAsync(attendeeInnovationOrganizationJsonDto.AttendeeInnovationOrganizationUid);
                foreach (var attendeeInnovationOrganizationEvaluationDto in attendeeInnovationEvaluationDto.AttendeeInnovationOrganizationEvaluationDtos)
                {
                    data.AppendLine(
                        attendeeInnovationOrganizationJsonDto.InnovationOrganizationName + ";" +
                        attendeeInnovationOrganizationJsonDto.InnovationOrganizationServiceName + ";" +
                        attendeeInnovationOrganizationEvaluationDto.AttendeeInnovationOrganizationEvaluation.Grade + ";" +
                        attendeeInnovationOrganizationEvaluationDto.EvaluatorUser.Name + ";"
                    );
                }
            }

            return Json(new
            {
                fileName = "MusicProjects_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".csv",
                fileContent = data.ToString()
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Total Count Widget

        /// <summary>Shows the total count widget.</summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowTotalCountWidget()
        {
            var projectsCount = await this.attendeeInnovationOrganizationRepo.CountAsync(this.EditionDto.Id, true);

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/TotalCountWidget", projectsCount), divIdOrClass = "#InnovationProjectsTotalCountWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Edition Count Widget

        /// <summary>Shows the edition count widget.</summary>
        /// <returns></returns>
        public async Task<ActionResult> ShowEditionCountWidget()
        {
            var projectsCount = await this.attendeeInnovationOrganizationRepo.CountAsync(this.EditionDto.Id);
            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/EditionCountWidget", projectsCount), divIdOrClass = "#InnovationProjectsEditionCountWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Details

        /// <summary>
        /// Evaluations the details.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="searchKeywords">The search keywords.</param>
        /// <param name="musicGenreUid">The music genre uid.</param>
        /// <param name="evaluationStatusUid">The evaluation status uid.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> EvaluationDetails(int? id, string searchKeywords = null, Guid? innovationOrganizationTrackUid = null, Guid? evaluationStatusUid = null, int? page = 1, int? pageSize = 12)
        {
            if (!page.HasValue || page <= 0)
            {
                page++;
            }

            var attendeeInnovationOrganizationDto = await this.attendeeInnovationOrganizationRepo.FindDtoToEvaluateAsync(id ?? 0);
            if (attendeeInnovationOrganizationDto == null)
            {
                this.StatusMessageToastr(string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()), Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);
                return RedirectToAction("Index", "Projects", new { Area = "Innovation" });
            }

            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.InnovationProjects, new List<BreadcrumbItemHelper>{
                new BreadcrumbItemHelper(Labels.Innovation, null),
                new BreadcrumbItemHelper(Labels.Projects, Url.Action("Index", "Projects", new { Area = "Innovation" })),
                new BreadcrumbItemHelper(attendeeInnovationOrganizationDto?.InnovationOrganization?.Name ?? Labels.Project, Url.Action("EvaluationDetails", "Projects", new { Area = "Innovation", id }))
            });

            #endregion

            var allInnovationProjectsIds = await this.attendeeInnovationOrganizationRepo.FindAllInnovationOrganizationsIdsPagedAsync(
                this.EditionDto.Edition.Id,
                searchKeywords,
                innovationOrganizationTrackUid,
                evaluationStatusUid,
                page.Value,
                pageSize.Value);
            var currentInnovationProjectIdIndex = Array.IndexOf(allInnovationProjectsIds, id.Value) + 1; //Index start at 0, its a fix to "start at 1"

            ViewBag.SearchKeywords = searchKeywords;
            ViewBag.InnovationOrganizationTrackUid = innovationOrganizationTrackUid;
            ViewBag.EvaluationStatusUid = evaluationStatusUid;
            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;
            ViewBag.CurrentInnovationProjectIndex = currentInnovationProjectIdIndex;

            ViewBag.InnovationProjectsTotalCount = await this.attendeeInnovationOrganizationRepo.CountPagedAsync(this.EditionDto.Edition.Id, searchKeywords, innovationOrganizationTrackUid, evaluationStatusUid, page.Value, pageSize.Value);
            ViewBag.ApprovedAttendeeInnovationOrganizationsIds = await this.attendeeInnovationOrganizationRepo.FindAllApprovedAttendeeInnovationOrganizationsIdsAsync(this.EditionDto.Edition.Id);

            return View(attendeeInnovationOrganizationDto);
        }

        /// <summary>
        /// Previouses the evaluation details.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="searchKeywords">The search keywords.</param>
        /// <param name="innovationOrganizationTrackUid">The music genre uid.</param>
        /// <param name="evaluationStatusUid">The evaluation status uid.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> PreviousEvaluationDetails(int? id, string searchKeywords = null, Guid? innovationOrganizationTrackUid = null, Guid? evaluationStatusUid = null, int? page = 1, int? pageSize = 12)
        {
            var allInnovationProjectsIds = await this.attendeeInnovationOrganizationRepo.FindAllInnovationOrganizationsIdsPagedAsync(
                this.EditionDto.Edition.Id,
                searchKeywords,
                innovationOrganizationTrackUid,
                evaluationStatusUid,
                page.Value,
                pageSize.Value);

            var currentInnovationProjectIdIndex = Array.IndexOf(allInnovationProjectsIds, id.Value);
            var previousProjectId = allInnovationProjectsIds.ElementAtOrDefault(currentInnovationProjectIdIndex - 1);
            if (previousProjectId == 0)
            {
                previousProjectId = id.Value;
            }

            return RedirectToAction("EvaluationDetails",
                new
                {
                    id = previousProjectId,
                    searchKeywords,
                    innovationOrganizationTrackUid,
                    evaluationStatusUid,
                    page,
                    pageSize
                });
        }

        /// <summary>
        /// Nexts the evaluation details.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="searchKeywords">The search keywords.</param>
        /// <param name="innovationOrganizationTrackUid">The music genre uid.</param>
        /// <param name="evaluationStatusUid">The evaluation status uid.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> NextEvaluationDetails(int? id, string searchKeywords = null, Guid? innovationOrganizationTrackUid = null, Guid? evaluationStatusUid = null, int? page = 1, int? pageSize = 12)
        {
            var allInnovationProjectsIds = await this.attendeeInnovationOrganizationRepo.FindAllInnovationOrganizationsIdsPagedAsync(
                this.EditionDto.Edition.Id,
                searchKeywords,
                innovationOrganizationTrackUid,
                evaluationStatusUid,
                page.Value,
                pageSize.Value);

            var currentInnovationProjectIdIndex = Array.IndexOf(allInnovationProjectsIds, id.Value);
            var nextProjectId = allInnovationProjectsIds.ElementAtOrDefault(currentInnovationProjectIdIndex + 1);
            if (nextProjectId == 0)
            {
                nextProjectId = id.Value;
            }

            return RedirectToAction("EvaluationDetails",
                new
                {
                    id = nextProjectId,
                    searchKeywords,
                    innovationOrganizationTrackUid,
                    evaluationStatusUid,
                    page,
                    pageSize
                });
        }

        #endregion

        #region Main Information Widget

        /// <summary>
        /// Shows the main information widget.
        /// </summary>
        /// <param name="attendeeInnovationOrganizationUid">The attendee innovation organization uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowMainInformationWidget(Guid? attendeeInnovationOrganizationUid)
        {
            var mainInformationWidgetDto = await this.attendeeInnovationOrganizationRepo.FindMainInformationWidgetDtoAsync(attendeeInnovationOrganizationUid ?? Guid.Empty);
            if (mainInformationWidgetDto == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Startup, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/MainInformationWidget", mainInformationWidgetDto), divIdOrClass = "#ProjectMainInformationWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Tracks Widget

        /// <summary>
        /// Shows the tracks widget.
        /// </summary>
        /// <param name="attendeeInnovationOrganizationUid">The attendee innovation organization uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowTracksWidget(Guid? attendeeInnovationOrganizationUid)
        {
            var tracksWidgetDto = await this.attendeeInnovationOrganizationRepo.FindTracksWidgetDtoAsync(attendeeInnovationOrganizationUid ?? Guid.Empty);
            if (tracksWidgetDto == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Startup, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            ViewBag.InnovationOrganizationTrackOptions = await this.innovationOrganizationTrackOptionRepo.FindAllAsync();

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/TracksWidget", tracksWidgetDto), divIdOrClass = "#ProjectTracksWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Objectives Widget

        /// <summary>
        /// Shows the objectives widget.
        /// </summary>
        /// <param name="attendeeInnovationOrganizationUid">The attendee innovation organization uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowObjectivesWidget(Guid? attendeeInnovationOrganizationUid)
        {
            var objectivesWidgetDto = await this.attendeeInnovationOrganizationRepo.FindObjectivesWidgetDtoAsync(attendeeInnovationOrganizationUid ?? Guid.Empty);
            if (objectivesWidgetDto == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Startup, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            ViewBag.InnovationOrganizationObjectivesOptions = await this.innovationOrganizationObjectivesOptionRepo.FindAllAsync();

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/ObjectivesWidget", objectivesWidgetDto), divIdOrClass = "#ProjectObjectivesWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Technologies Widget

        /// <summary>
        /// Shows the technologies widget.
        /// </summary>
        /// <param name="attendeeInnovationOrganizationUid">The attendee innovation organization uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowTechnologiesWidget(Guid? attendeeInnovationOrganizationUid)
        {
            var technologiesWidgetDto = await this.attendeeInnovationOrganizationRepo.FindTechnologiesWidgetDtoAsync(attendeeInnovationOrganizationUid ?? Guid.Empty);
            if (technologiesWidgetDto == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Startup, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            ViewBag.InnovationOrganizationTechnologiesOptions = await this.innovationOrganizationTechnologyOptionRepo.FindAllAsync();

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/TechnologiesWidget", technologiesWidgetDto), divIdOrClass = "#ProjectTechnologiesWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Experiences Widget

        /// <summary>
        /// Shows the experiences widget.
        /// </summary>
        /// <param name="attendeeInnovationOrganizationUid">The attendee innovation organization uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowExperiencesWidget(Guid? attendeeInnovationOrganizationUid)
        {
            var experiencesWidgetDto = await this.attendeeInnovationOrganizationRepo.FindExperiencesWidgetDtoAsync(attendeeInnovationOrganizationUid ?? Guid.Empty);
            if (experiencesWidgetDto == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Startup, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            ViewBag.InnovationOrganizationExperiencesOptions = await this.innovationOrganizationExperienceOptionRepo.FindAllAsync();

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/ExperiencesWidget", experiencesWidgetDto), divIdOrClass = "#ProjectExperiencesWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Evaluation Grade Widget 

        /// <summary>
        /// Shows the evaluation grade widget.
        /// </summary>
        /// <param name="attendeeInnovationOrganizationUid">The project uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowEvaluationGradeWidget(Guid? attendeeInnovationOrganizationUid)
        {
            var evaluationDto = await this.attendeeInnovationOrganizationRepo.FindEvaluationGradeWidgetDtoAsync(attendeeInnovationOrganizationUid ?? Guid.Empty, this.AdminAccessControlDto.User.Id);
            if (evaluationDto == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Startup, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            ViewBag.ApprovedAttendeeInnovationOrganizationsIds = await this.attendeeInnovationOrganizationRepo.FindAllApprovedAttendeeInnovationOrganizationsIdsAsync(this.EditionDto.Edition.Id);

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/EvaluationGradeWidget", evaluationDto), divIdOrClass = "#ProjectEvaluationWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Evaluates the specified music band identifier.
        /// </summary>
        /// <param name="musicBandId">The music band identifier.</param>
        /// <param name="grade">The grade.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Evaluate(int musicBandId, decimal? grade)
        {
            if (this.EditionDto?.IsMusicProjectEvaluationOpen() != true)
            {
                return Json(new { status = "error", message = Messages.EvaluationPeriodClosed }, JsonRequestBehavior.AllowGet);
            }

            var result = new AppValidationResult();

            try
            {
                //var cmd = new EvaluateMusicBand(
                //    await this.musicBandRepo.FindByIdAsync(musicBandId),
                //    grade);

                //cmd.UpdatePreSendProperties(
                //    this.AdminAccessControlDto.User.Id,
                //    this.AdminAccessControlDto.User.Uid,
                //    this.EditionDto.Id,
                //    this.EditionDto.Uid,
                //    this.UserInterfaceLanguage);
                //result = await this.CommandBus.Send(cmd);
                if (!result.IsValid)
                {
                    throw new DomainException(Messages.CorrectFormValues);
                }
            }
            catch (DomainException ex)
            {
                return Json(new
                {
                    status = "error",
                    message = result.Errors.Select(e => e = new AppValidationError(e.Message, "ToastrError", e.Code))?
                                            .FirstOrDefault(e => e.Target == "ToastrError")?.Message ?? ex.GetInnerMessage(),
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return Json(new { status = "error", message = Messages.WeFoundAndError, }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                status = "success",
                //projectUid = cmd.MusicBandId,
                message = string.Format(Messages.EntityActionSuccessfull, Labels.MusicBand, Labels.Evaluated.ToLowerInvariant())
            });
        }

        #endregion

        #region Evaluators Widget

        /// <summary>
        /// Shows the evaluators widget.
        /// </summary>
        /// <param name="attendeeInnovationOrganizationUid">The project uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowEvaluatorsWidget(Guid? attendeeInnovationOrganizationUid)
        {
            var evaluationDto = await this.attendeeInnovationOrganizationRepo.FindEvaluatorsWidgetDtoAsync(attendeeInnovationOrganizationUid ?? Guid.Empty);
            if (evaluationDto == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Startup, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/EvaluatorsWidget", evaluationDto), divIdOrClass = "#ProjectEvaluatorsWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Delete

        /// <summary>Deletes the specified delete music project.</summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Delete(DeleteAttendeeInnovationOrganization cmd)
        {
            var result = new AppValidationResult();

            try
            {
                if (!ModelState.IsValid)
                {
                    throw new DomainException(Messages.CorrectFormValues);
                }

                cmd.UpdatePreSendProperties(
                    this.AdminAccessControlDto.User.Id,
                    this.AdminAccessControlDto.User.Uid,
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

                return Json(new
                {
                    status = "error",
                    message = result.Errors?.FirstOrDefault(e => e.Target == "ToastrError")?.Message ?? ex.GetInnerMessage(),
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return Json(new { status = "error", message = Messages.WeFoundAndError, }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Project, Labels.DeletedM) });
        }

        #endregion
    }
}