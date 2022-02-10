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
        private readonly ICartoonProjectRepository cartoonProjectRepo;
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
            ICartoonProjectRepository cartoonProjectRepository,
            IProjectEvaluationStatusRepository evaluationStatusRepository,
            IAttendeeCartoonProjectRepository attendeeCartoonProjectRepository
            )
            : base(commandBus, identityController)
        {
            this.cartoonProjectRepo = cartoonProjectRepository;
            this.evaluationStatusRepo = evaluationStatusRepository;
            this.evaluationStatusRepo = projectEvaluationStatusRepository;
            this.attendeeCartoonProjectRepo = attendeeCartoonProjectRepository;
            this.cartoonProjectFormatRepo = cartoonProjectFormatRepository;
        }

        #region List

        /// <param name="evaluationStatusUid">The evaluation status uid.</param>
        /// <returns></returns>
        [HttpGet]
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

        /// <summary>Export to Excel the evaluation list widget.</summary>
        /// <param name="searchKeywords">The search keywords.</param>
        /// <param name="evaluationStatusUid">The evaluation status uid.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ExportEvaluationListWidget(string searchKeywords, Guid? evaluationStatusUid, int? page = 1, int? pageSize = 1000)
        {
            StringBuilder data = new StringBuilder();
            data.AppendLine($"{Labels.Startup}; {Labels.Project}; {Labels.ProductsOrServices}; {Labels.Status}; {Labels.Votes}; {Labels.Average}");

            var attendeeCartoonProjectJsonDtos = await this.attendeeCartoonProjectRepo.FindAllJsonDtosPagedAsync(this.EditionDto.Id, searchKeywords, evaluationStatusUid, 1, 10000, new List<Tuple<string, string>>());
            var approvedAttendeeCartoonProjectIds = await this.attendeeCartoonProjectRepo.FindAllApprovedAttendeeCartoonProjectsIdsAsync(this.EditionDto.Id);

            foreach (var attendeeCartoonProjectJsonDto in attendeeCartoonProjectJsonDtos)
            {
                data.AppendLine(attendeeCartoonProjectJsonDto.CartoonProjectName + ";" +
                                attendeeCartoonProjectJsonDto.CartoonProjectServiceName + ";" +
                                (approvedAttendeeCartoonProjectIds.Contains(attendeeCartoonProjectJsonDto.AttendeeCartoonProjectId) ? Labels.ProjectAccepted : Labels.ProjectRefused) + ";" +
                                attendeeCartoonProjectJsonDto.EvaluationsCount + ";" +
                                attendeeCartoonProjectJsonDto?.Grade ?? "-");
            }

            return Json(new
            {
                fileName = "CartoonProjects_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".csv",
                fileContent = data.ToString()
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>Export to Excel the evaluators list widget.</summary>
        /// <param name="searchKeywords">The search keywords.</param>
        /// <param name="evaluationStatusUid">The evaluation status uid.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ExportEvaluatorsListWidget(string searchKeywords, Guid? evaluationStatusUid, int? page = 1, int? pageSize = 1000)
        {
            StringBuilder data = new StringBuilder();
            data.AppendLine($"{Labels.Startup}; {Labels.Project}; {Labels.Evaluation}; {Labels.Evaluator};");

            var attendeeCartoonProjectJsonDtos = await this.attendeeCartoonProjectRepo.FindAllJsonDtosPagedAsync(this.EditionDto.Id, searchKeywords, evaluationStatusUid, 1, 1000, new List<Tuple<string, string>>());
            foreach (var attendeeCartoonProjectJsonDto in attendeeCartoonProjectJsonDtos)
            {
                var attendeeCartoonEvaluationDto = await this.attendeeCartoonProjectRepo.FindEvaluatorsWidgetDtoAsync(attendeeCartoonProjectJsonDto.AttendeeCartoonProjectUid);
                foreach (var attendeeCartoonProjectEvaluationDto in attendeeCartoonEvaluationDto.AttendeeCartoonProjectEvaluationDtos)
                {
                    data.AppendLine(
                        attendeeCartoonProjectJsonDto.CartoonProjectName + ";" +
                        attendeeCartoonProjectJsonDto.CartoonProjectServiceName + ";" +
                        attendeeCartoonProjectEvaluationDto.AttendeeCartoonProjectEvaluation.Grade + ";" +
                        attendeeCartoonProjectEvaluationDto.EvaluatorUser.Name + ";"
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

        //#region Edition Count Pie Widget

        ///// <summary>Shows the edition count pie widget.</summary>
        ///// <returns></returns>
        //public async Task<ActionResult> ShowEditionCountPieWidget()
        //{
        //   var projectsCount = await this.attendeeCartoonProjectRepo.CountAsync(this.EditionDto.Id);

        //    return Json(new
        //    {
        //        status = "success",
        //        pages = new List<dynamic>
        //        {
        //            new { page = this.RenderRazorViewToString("Widgets/EditionCountPieWidget"), divIdOrClass = "#InnovationProjectsEditionCountPieWidget" },
        //        },
        //        innovationProjectsGroupedByTrackDtos = innovationOrganizationGroupedByTrackDtos.Select(i => new InnovationOrganizationGroupedByTrackDto
        //        {
        //            InnovationProjectsTotalCount = i.InnovationProjectsTotalCount,
        //            TrackName = i.TrackName.GetSeparatorTranslation(ViewBag.UserInterfaceLanguage as string, '|')
        //        }),
        //        innovationProjectsTotalCount = projectsCount
        //    }, JsonRequestBehavior.AllowGet);
        //}

        //#endregion

        #region Details

        /// <summary>
        /// Evaluations the details.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="searchKeywords">The search keywords.</param>=
        /// <param name="evaluationStatusUid">The evaluation status uid.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> EvaluationDetails(int? id, string searchKeywords = null, Guid? evaluationStatusUid = null, int? page = 1, int? pageSize = 10)
        {
            if (!page.HasValue || page <= 0)
            {
                page++;
            }

            var attendeeCartoonProjectDto = await this.attendeeCartoonProjectRepo.FindDtoToEvaluateAsync(id ?? 0);
            if (attendeeCartoonProjectDto == null)
            {
                this.StatusMessageToastr(string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()), Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);
                return RedirectToAction("Index", "Projects", new { Area = "Cartoon" });
            }

            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.CartoonProjects, new List<BreadcrumbItemHelper>{
                new BreadcrumbItemHelper(Labels.Projects, Url.Action("Index", "Projects", new { Area = "Cartoon" })),
                new BreadcrumbItemHelper(attendeeCartoonProjectDto?.CartoonProject?.Title ?? Labels.Project, Url.Action("EvaluationDetails", "Projects", new { Area = "Cartoon", id }))
            });

            #endregion

            var allProjectsIds = await this.attendeeCartoonProjectRepo.FindAllCartoonProjectsIdsPagedAsync(
                this.EditionDto.Edition.Id,
                searchKeywords,
                evaluationStatusUid,
                page.Value,
                pageSize.Value);
            var currentProjectIdIndex = Array.IndexOf(allProjectsIds, id.Value) + 1; //Index start at 0, its a fix to "start at 1"

            ViewBag.SearchKeywords = searchKeywords;
            ViewBag.EvaluationStatusUid = evaluationStatusUid;
            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;
            ViewBag.CurrentCartoonProjectIndex = currentProjectIdIndex;

            ViewBag.CartoonProjectsTotalCount = await this.attendeeCartoonProjectRepo.CountPagedAsync(this.EditionDto.Edition.Id, searchKeywords, evaluationStatusUid, page.Value, pageSize.Value);
            ViewBag.ApprovedAttendeeCartoonProjectsIds = await this.attendeeCartoonProjectRepo.FindAllApprovedAttendeeCartoonProjectsIdsAsync(this.EditionDto.Edition.Id);

            return View(attendeeCartoonProjectDto);
        }

        /// <summary>
        /// Previouses the evaluation details.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="searchKeywords">The search keywords.</param>
        /// <param name="evaluationStatusUid">The evaluation status uid.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> PreviousEvaluationDetails(int? id, string searchKeywords = null, Guid? evaluationStatusUid = null, int? page = 1, int? pageSize = 10)
        {
            var allCartoonProjectsIds = await this.attendeeCartoonProjectRepo.FindAllCartoonProjectsIdsPagedAsync(
                this.EditionDto.Edition.Id,
                searchKeywords,
                evaluationStatusUid,
                page.Value,
                pageSize.Value);

            var currentCartoonProjectIdIndex = Array.IndexOf(allCartoonProjectsIds, id.Value);
            var previousProjectId = allCartoonProjectsIds.ElementAtOrDefault(currentCartoonProjectIdIndex - 1);
            if (previousProjectId == 0)
            {
                previousProjectId = id.Value;
            }

            return RedirectToAction("EvaluationDetails",
                new
                {
                    id = previousProjectId,
                    searchKeywords,
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
        /// <param name="evaluationStatusUid">The evaluation status uid.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> NextEvaluationDetails(int? id, string searchKeywords = null, Guid? evaluationStatusUid = null, int? page = 1, int? pageSize = 10)
        {
            var allCartoonProjectsIds = await this.attendeeCartoonProjectRepo.FindAllCartoonProjectsIdsPagedAsync(
                this.EditionDto.Edition.Id,
                searchKeywords,
                evaluationStatusUid,
                page.Value,
                pageSize.Value);

            var currentCartoonProjectIdIndex = Array.IndexOf(allCartoonProjectsIds, id.Value);
            var nextProjectId = allCartoonProjectsIds.ElementAtOrDefault(currentCartoonProjectIdIndex + 1);
            if (nextProjectId == 0)
            {
                nextProjectId = id.Value;
            }

            return RedirectToAction("EvaluationDetails",
                new
                {
                    id = nextProjectId,
                    searchKeywords,
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
        /// <param name="attendeeCartoonProjectUid">The attendee cartoon project uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowMainInformationWidget(Guid? attendeeCartoonProjectUid)
        {
            var mainInformationWidgetDto = await this.attendeeCartoonProjectRepo.FindMainInformationWidgetDtoAsync(attendeeCartoonProjectUid ?? Guid.Empty);
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

        #region Business Information Widget

        /// <summary>
        /// Shows the main information widget.
        /// </summary>
        /// <param name="attendeeCartoonProjectUid">The attendee cartoon project uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowBusinessInformationWidget(Guid? attendeeCartoonProjectUid)
        {
            var mainInformationWidgetDto = await this.attendeeCartoonProjectRepo.FindBusinessInformationWidgetDtoAsync(attendeeCartoonProjectUid ?? Guid.Empty);
            if (mainInformationWidgetDto == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Startup, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/BusinessInformationWidget", mainInformationWidgetDto), divIdOrClass = "#ProjectBusinessInformationWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Evaluation Grade Widget 

        /// <summary>
        /// Shows the evaluation grade widget.
        /// </summary>
        /// <param name="attendeeCartoonProjectUid">The project uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowEvaluationGradeWidget(Guid? attendeeCartoonProjectUid)
        {
            var evaluationDto = await this.attendeeCartoonProjectRepo.FindEvaluationGradeWidgetDtoAsync(attendeeCartoonProjectUid ?? Guid.Empty, this.AdminAccessControlDto.User.Id);
            if (evaluationDto == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Startup, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            ViewBag.ApprovedAttendeeCartoonProjectsIds = await this.attendeeCartoonProjectRepo.FindAllApprovedAttendeeCartoonProjectsIdsAsync(this.EditionDto.Edition.Id);

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
        public async Task<ActionResult> Evaluate(int cartoonProjectId, decimal? grade)
        {
            if (this.EditionDto?.IsCartoonProjectEvaluationOpen() != true)
            {
                return Json(new { status = "error", message = Messages.EvaluationPeriodClosed }, JsonRequestBehavior.AllowGet);
            }

            var result = new AppValidationResult();

            try
            {
                var cmd = new EvaluateCartoonProject(
                    await this.cartoonProjectRepo.FindByIdAsync(cartoonProjectId),
                    grade);

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
                message = string.Format(Messages.EntityActionSuccessfull, Labels.Startup, Labels.Evaluated.ToLowerInvariant())
            });
        }

        #endregion

        #region Evaluators Widget

        /// <summary>
        /// Shows the evaluators widget.
        /// </summary>
        /// <param name="attendeeCartoonProjectUid">The project uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowEvaluatorsWidget(Guid? attendeeCartoonProjectUid)
        {
            var evaluationDto = await this.attendeeCartoonProjectRepo.FindEvaluatorsWidgetDtoAsync(attendeeCartoonProjectUid ?? Guid.Empty);
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

        //#region Presentation Widget

        ///// <summary>Shows the clipping widget.</summary>
        ///// <param name="projectUid">The project uid.</param>
        ///// <returns></returns>
        //[HttpGet]
        //public async Task<ActionResult> ShowPresentationWidget(Guid? attendeeCartoonProjectUid)
        //{
        //    var presentationWidgetDto = await this.attendeeCartoonProjectRepo.FindPresentationWidgetDtoAsync(attendeeCartoonProjectUid ?? Guid.Empty);
        //    if (presentationWidgetDto == null)
        //    {
        //        return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
        //    }

        //    return Json(new
        //    {
        //        status = "success",
        //        pages = new List<dynamic>
        //        {
        //            new { page = this.RenderRazorViewToString("Widgets/PresentationWidget", presentationWidgetDto), divIdOrClass = "#ProjectsPresentationWidget" },
        //        }
        //    }, JsonRequestBehavior.AllowGet);
        //}

        //#endregion

        //#region Delete

        ///// <summary>Deletes the specified delete music project.</summary>
        ///// <param name="cmd">The command.</param>
        ///// <returns></returns>
        //[HttpPost]
        //public async Task<ActionResult> Delete(DeleteAttendeeCartoonProject cmd)
        //{
        //    var result = new AppValidationResult();

        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            throw new DomainException(Messages.CorrectFormValues);
        //        }

        //        cmd.UpdatePreSendProperties(
        //            this.AdminAccessControlDto.User.Id,
        //            this.AdminAccessControlDto.User.Uid,
        //            this.EditionDto.Id,
        //            this.EditionDto.Uid,
        //            this.UserInterfaceLanguage);

        //        result = await this.CommandBus.Send(cmd);
        //        if (!result.IsValid)
        //        {
        //            throw new DomainException(Messages.CorrectFormValues);
        //        }
        //    }
        //    catch (DomainException ex)
        //    {
        //        foreach (var error in result.Errors)
        //        {
        //            var target = error.Target ?? "";
        //            ModelState.AddModelError(target, error.Message);
        //        }

        //        return Json(new
        //        {
        //            status = "error",
        //            message = result.Errors?.FirstOrDefault(e => e.Target == "ToastrError")?.Message ?? ex.GetInnerMessage(),
        //        }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        //        return Json(new { status = "error", message = Messages.WeFoundAndError, }, JsonRequestBehavior.AllowGet);
        //    }

        //    return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Project, Labels.DeletedM) });
        //}

        //#endregion
    }
}