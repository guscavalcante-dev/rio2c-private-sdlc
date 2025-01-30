// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 06-28-2019
//
// Last Modified By : Gilson Oliveira
// Last Modified On : 10-30-2024
// ***********************************************************************
// <copyright file="ProjectsController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using DataTables.AspNet.Core;
using DataTables.AspNet.Mvc5;
using MediatR;
using PlataformaRio2C.Application;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Application.CQRS.Queries;
using PlataformaRio2C.Application.TemplateDocuments;
using PlataformaRio2C.Application.ViewModels;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Statics;
using PlataformaRio2C.Infra.CrossCutting.Identity.AuthorizeAttributes;
using PlataformaRio2C.Infra.CrossCutting.Identity.Service;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using PlataformaRio2C.Infra.CrossCutting.Tools.Helpers;
using PlataformaRio2c.Infra.Data.FileRepository;
using PlataformaRio2C.Infra.Report.Models;
using PlataformaRio2C.Web.Admin.Controllers;
using PlataformaRio2C.Web.Admin.Filters;
using Constants = PlataformaRio2C.Domain.Constants;
using ClosedXML.Excel;
using PlataformaRio2C.Domain.ApiModels;
using PlataformaRio2C.Infra.CrossCutting.Tools.CustomActionResults;

namespace PlataformaRio2C.Web.Admin.Areas.Audiovisual.Controllers
{
    /// <summary>The Audiovisual Commissions ProjectsController</summary>
    [AjaxAuthorize(Order = 1, Roles = Constants.Role.AnyAdmin)]
    [AuthorizeCollaboratorType(Order = 2, Types = Constants.CollaboratorType.AdminAudiovisual)]
    public class ProjectsController : BaseController
    {
        private readonly IProjectRepository projectRepo;
        private readonly IInterestRepository interestRepo;
        private readonly ITargetAudienceRepository targetAudienceRepo;
        private readonly IAttendeeOrganizationRepository attendeeOrganizationRepo;
        private readonly IFileRepository fileRepo;
        private readonly IProjectEvaluationStatusRepository evaluationStatusRepo;
        private readonly IProjectModalityRepository projectModalityRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectsController"/> class.
        /// </summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="identityController">The identity controller.</param>
        /// <param name="projectRepository">The project repository.</param>
        /// <param name="interestRepository">The interest repository.</param>
        /// <param name="targetAudienceRepository">The target audience repository.</param>
        /// <param name="attendeeOrganizationRepository">The attendee organization repository.</param>
        /// <param name="fileRepository">The file repository.</param>
        /// <param name="evaluationStatusRepository">The evaluation status repository.</param>
        /// <param name="projectModalityRepository">The project modality repository.</param>
        public ProjectsController(
            IMediator commandBus,
            IdentityAutenticationService identityController,
            IProjectRepository projectRepository,
            IInterestRepository interestRepository,
            ITargetAudienceRepository targetAudienceRepository,
            IAttendeeOrganizationRepository attendeeOrganizationRepository,
            IFileRepository fileRepository,
            IProjectEvaluationStatusRepository evaluationStatusRepository,
            IProjectModalityRepository projectModalityRepository)
            : base(commandBus, identityController)
        {
            this.projectRepo = projectRepository;
            this.interestRepo = interestRepository;
            this.targetAudienceRepo = targetAudienceRepository;
            this.attendeeOrganizationRepo = attendeeOrganizationRepository;
            this.fileRepo = fileRepository;
            this.evaluationStatusRepo = evaluationStatusRepository;
            this.projectModalityRepository = projectModalityRepository;
        }

        #region List

        /// <summary>
        /// Indexes the specified search view model.
        /// </summary>
        /// <param name="searchViewModel">The search view model.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Index(AudiovisualProjectSearchViewModel searchViewModel)
        {
            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.AudiovisualProjects, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Labels.AudioVisual, null),
                new BreadcrumbItemHelper(Labels.Projects, Url.Action("Index", "Projects", new { Area = "Audiovisual" }))
            });

            #endregion

            searchViewModel.UpdateModelsAndLists(
                await this.interestRepo.FindAllByInterestGroupUidAsync(InterestGroup.AudiovisualGenre.Uid),
                await this.evaluationStatusRepo.FindAllAsync(),
                this.UserInterfaceLanguage,
                await this.projectModalityRepository.FindAllAsync()
            );

            return View(searchViewModel);
        }

        /// <summary>
        /// Searches the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="projectModalityUid">The project modality uid.</param>
        /// <param name="interestUid">The interest uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Search(
            IDataTablesRequest request,
            Guid? projectModalityUid,
            Guid? interestUid,
            Guid? evaluationStatusUid
        )
        {
            int page = request.Start / request.Length;
            int pageSize = request.Length;
            page++; //Necessary because DataTable is zero index based.

            var projectsBaseDtos = await this.projectRepo.FindAllByDataTableAsync(
                page,
                pageSize,
                request.GetSortColumns(),
                request.Search?.Value,
                projectModalityUid,
                interestUid,
                evaluationStatusUid,
                this.UserInterfaceLanguage,
                this.EditionDto.Id);

            var approvedProjectsIds = await this.projectRepo.FindAllApprovedCommissionProjectsIdsAsync(this.EditionDto.Id);

            foreach (var projectBaseDto in projectsBaseDtos)
            {
                projectBaseDto.IsApproved = approvedProjectsIds.Contains(projectBaseDto.Id);

                #region Translate Project Genres

                projectBaseDto.Genre = new List<string>();
                foreach (var projectInterestDto in projectBaseDto.Genres)
                {
                    projectBaseDto.Genre.Add(projectInterestDto.Interest.Name.GetSeparatorTranslation(this.UserInterfaceLanguage, '|'));
                }

                #endregion
            }

            ViewBag.InterestUid = interestUid;
            ViewBag.EvaluationStatusUid = evaluationStatusUid;
            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;

            IDictionary<string, object> additionalParameters = new Dictionary<string, object>();
            if (projectsBaseDtos.TotalItemCount <= 0)
            {
                if (this.EditionDto.IsAudiovisualCommissionProjectEvaluationOpen() && (
                    evaluationStatusUid == ProjectEvaluationStatus.Accepted.Uid ||
                    evaluationStatusUid == ProjectEvaluationStatus.Refused.Uid))
                {
                    additionalParameters.Add("noRecordsFoundMessage",
                        $"{string.Format(Messages.TheEvaluationPeriodRunsFrom, this.EditionDto.AudiovisualCommissionEvaluationStartDate.ToBrazilTimeZone().ToShortDateString(), this.EditionDto.AudiovisualCommissionEvaluationEndDate.ToBrazilTimeZone().ToShortDateString())}.</br>{Messages.TheProjectsWillReceiveFinalGradeAtPeriodEnds}");
                }
                else if (!this.EditionDto.IsAudiovisualCommissionProjectEvaluationOpen() &&
                    evaluationStatusUid == ProjectEvaluationStatus.UnderEvaluation.Uid)
                {
                    additionalParameters.Add("noRecordsFoundMessage",
                        $"{Messages.EvaluationPeriodClosed}<br/>{string.Format(Messages.ProjectsNotFoundWithStatus, Labels.UnderEvaluation)}");
                }
            }

            var response = DataTablesResponse.Create(request, projectsBaseDtos.TotalItemCount, projectsBaseDtos.TotalItemCount, projectsBaseDtos, additionalParameters);

            return Json(new
            {
                status = "success",
                dataTable = response,
                searchKeywords = request.Search?.Value,
                interestUid,
                evaluationStatusUid,
                projectModalityUid,
                page,
                pageSize
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Exports the evaluators report to excel.
        /// </summary>
        /// <param name="searchViewModel">The search view model.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ExportEvaluatorsReportToExcel(AudiovisualProjectSearchViewModel searchViewModel)
        {
            string fileName = $@"{Labels.AudioVisual} - {Labels.EvaluationsByEvaluatorReport}_{DateTime.UtcNow.ToStringFileNameTimestamp()}";
            string filePath = Path.Combine(Path.GetTempPath(), fileName + ".xlsx");

            try
            {
                var projectsBaseDtos = await this.projectRepo.FindAllEvaluatorsReportByDataTableAsync(
                    1,
                    10000,
                    new List<Tuple<string, string>>(), //request.GetSortColumns(),
                    searchViewModel.Search,
                    searchViewModel.ProjectModalityUid,
                    searchViewModel.InterestUid,
                    searchViewModel.EvaluationStatusUid,
                    this.UserInterfaceLanguage,
                    this.EditionDto.Id);

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add(Labels.Contacts);

                    #region Header

                    var lineIndex = 1;
                    var columnIndex = 0;
                    var skipFinalAdjustmentsColumnIndexes = new List<int>();

                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.Producer;
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.Project;

                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.Evaluation;
                    worksheet.Column(columnIndex).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    skipFinalAdjustmentsColumnIndexes.Add(columnIndex);

                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.Evaluator;

                    #endregion

                    if (projectsBaseDtos.Any())
                    {
                        #region Rows

                        foreach (var projectBaseDto in projectsBaseDtos)
                        {
                            if (projectBaseDto?.ProjectCommissionEvaluationDtos != null)
                            {
                                foreach (var projectCommissionEvaluationDto in projectBaseDto.ProjectCommissionEvaluationDtos)
                                {
                                    lineIndex++;
                                    columnIndex = 0;

                                    worksheet.Cell(lineIndex, columnIndex += 1).Value = projectBaseDto.ProducerName;
                                    worksheet.Cell(lineIndex, columnIndex += 1).Value = projectBaseDto.ProjectName;

                                    worksheet.Cell(lineIndex, columnIndex += 1).Value = projectCommissionEvaluationDto.CommissionEvaluation.Grade;
                                    worksheet.Cell(lineIndex, columnIndex).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                                    worksheet.Cell(lineIndex, columnIndex += 1).Value = projectCommissionEvaluationDto.EvaluatorUser.Name;
                                }
                            }
                        }

                        for (var adjustColumnIndex = 1; adjustColumnIndex <= columnIndex; adjustColumnIndex++)
                        {
                            if (!skipFinalAdjustmentsColumnIndexes.Contains(adjustColumnIndex))
                            {
                                worksheet.Column(adjustColumnIndex).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            }

                            worksheet.Column(adjustColumnIndex).Style.Alignment.WrapText = false;
                            worksheet.Column(adjustColumnIndex).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                            worksheet.Column(adjustColumnIndex).AdjustToContents();
                        }

                        #endregion
                    }

                    workbook.SaveAs(filePath);
                }

                // It's necessary to save workbook to file to run "AdjustToContents()" correctly.
                // Without this, "AdjustToContents()" doesn't work and columns be with minimun width.
                var fileBytes = System.IO.File.ReadAllBytes(filePath);
                var workbookResult = new XLWorkbook(new MemoryStream(fileBytes));

                return new ExcelResult(workbookResult, fileName);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return Json(new { status = ApiStatus.Error, message = Messages.WeFoundAndError });
            }
            finally
            {
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }
        }

        /// <summary>
        /// Exports the detailed projects report to excel.
        /// </summary>
        /// <param name="searchViewModel">The search view model.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ExportDetailedProjectsReportToExcel(AudiovisualProjectSearchViewModel searchViewModel)
        {
            //TODO: This report was started in task [RIO2CMY-796] but after technical analysis it was verified that it was not a "project report", but a "Player Executives".
            //This information was not explicit in the task, it was only after we received the example .xlsx that we realized what the customer really needed.
            //Do not delete this code! It's working! Just remove the retun Json below and change the select fields that will be exported!
            return Json(new { status = ApiStatus.Error, message = Texts.NotFoundErrorMessage });

            #region Disabled but working!

#pragma warning disable CS0162 // Unreachable code detected
            string fileName = $@"{Labels.AudioVisual} - {Labels.DetailedProjectsReport}_{DateTime.UtcNow.ToStringFileNameTimestamp()}";

            string filePath = Path.Combine(Path.GetTempPath(), fileName + ".xlsx");

            try
            {
                var projectsBaseDtos = await this.projectRepo.FindAllProjectsReportByDataTableAsync(
                    1,
                    10000,
                    new List<Tuple<string, string>>(), //request.GetSortColumns(),
                    searchViewModel.Search,
                    searchViewModel.ProjectModalityUid,
                    searchViewModel.InterestUid,
                    searchViewModel.EvaluationStatusUid,
                    this.UserInterfaceLanguage,
                    this.EditionDto.Id);

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add(Labels.Contacts);

                    #region Header

                    var lineIndex = 1;
                    var columnIndex = 0;
                    var skipFinalAdjustmentsColumnIndexes = new List<int>();

                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.Producer;
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.Project;

                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.Evaluation;
                    worksheet.Column(columnIndex).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    skipFinalAdjustmentsColumnIndexes.Add(columnIndex);

                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.Evaluator;

                    #endregion

                    if (projectsBaseDtos.Any())
                    {
                        #region Rows

                        foreach (var projectBaseDto in projectsBaseDtos)
                        {
                            if (projectBaseDto?.ProjectCommissionEvaluationDtos != null)
                            {
                                foreach (var projectCommissionEvaluationDto in projectBaseDto.ProjectCommissionEvaluationDtos)
                                {
                                    lineIndex++;
                                    columnIndex = 0;

                                    worksheet.Cell(lineIndex, columnIndex += 1).Value = projectBaseDto.ProducerName;
                                    worksheet.Cell(lineIndex, columnIndex += 1).Value = projectBaseDto.ProjectName;

                                    worksheet.Cell(lineIndex, columnIndex += 1).Value = projectCommissionEvaluationDto.CommissionEvaluation.Grade;
                                    worksheet.Cell(lineIndex, columnIndex).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                                    worksheet.Cell(lineIndex, columnIndex += 1).Value = projectCommissionEvaluationDto.EvaluatorUser.Name;
                                }
                            }
                        }

                        for (var adjustColumnIndex = 1; adjustColumnIndex <= columnIndex; adjustColumnIndex++)
                        {
                            if (!skipFinalAdjustmentsColumnIndexes.Contains(adjustColumnIndex))
                            {
                                worksheet.Column(adjustColumnIndex).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            }

                            worksheet.Column(adjustColumnIndex).Style.Alignment.WrapText = false;
                            worksheet.Column(adjustColumnIndex).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                            worksheet.Column(adjustColumnIndex).AdjustToContents();
                        }

                        #endregion
                    }

                    workbook.SaveAs(filePath);
                }

                // It's necessary to save workbook to file to run "AdjustToContents()" correctly.
                // Without this, "AdjustToContents()" doesn't work and columns be with minimun width.
                var fileBytes = System.IO.File.ReadAllBytes(filePath);
                var workbookResult = new XLWorkbook(new MemoryStream(fileBytes));

                return new ExcelResult(workbookResult, fileName);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return Json(new { status = ApiStatus.Error, message = Messages.WeFoundAndError });
            }
            finally
            {
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }
#pragma warning restore CS0162 // Unreachable code detected

            #endregion
        }

        #endregion

        #region Download PDFs

        /// <summary>
        /// Downloads the PDFS.
        /// </summary>
        /// <param name="keyword">The keyword.</param>
        /// <param name="projectModalityUid">The project modality uid.</param>
        /// <param name="interestUid">The interest uid.</param>
        /// <param name="selectedProjectsUids">The selected projects uids.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<FileResult> DownloadPdfs(string keyword, Guid? projectModalityUid, Guid? interestUid, string selectedProjectsUids)
        {
            var projectsDtos = await this.projectRepo.FindAllDtosByFiltersAsync(
                keyword,
                null,
                projectModalityUid.HasValue ? new List<Guid?> { projectModalityUid } : new List<Guid?> { },
                new List<Guid?> { interestUid },
                selectedProjectsUids?.ToListGuid(','),
                this.UserInterfaceLanguage,
                this.EditionDto.Id);

            // No projects returned
            if (projectsDtos?.Any() != true)
            {
                return null;
            }

            // Just one project returned
            if (projectsDtos.Count == 1)
            {
                var projectDto = projectsDtos.First();
                var pdf = new PlataformaRio2CDocument(new ProjectDocumentTemplate(projectDto));

                return File(pdf.GetStream(), "application/pdf", Labels.Project +
                                                                "_" +
                                                                projectDto.Project.Id.ToString("D4") +
                                                                "_" +
                                                                projectDto.GetTitleDtoByLanguageCode(Language.Portuguese.Code).ProjectTitle.Value.RemoveFilenameInvalidChars() +
                                                                ".pdf");
            }

            // Many projects returned
            var dictPdf = new Dictionary<string, MemoryStream>();

            foreach (var projectDto in projectsDtos)
            {
                var pdfDocument = new PlataformaRio2CDocument(new ProjectDocumentTemplate(projectDto));
                dictPdf.Add(Labels.Project +
                            "_" +
                            projectDto.Project.Id.ToString("D4") +
                            "_" +
                            projectDto.GetTitleDtoByLanguageCode(Language.Portuguese.Code).ProjectTitle.Value.RemoveFilenameInvalidChars() +
                            ".pdf", pdfDocument.GetStream());
            }

            return ZipDocuments(dictPdf);
        }

        /// <summary>Zips the documents.</summary>
        /// <param name="pdfCollection">The PDF collection.</param>
        /// <returns></returns>
        private FileResult ZipDocuments(Dictionary<string, MemoryStream> pdfCollection)
        {
            string fileNameZip = Labels.Projects + "_" + DateTime.UtcNow.ToBrazilTimeZone().ToString("yyyyMMdd") + ".zip";
            byte[] compressedBytes;
            using (var outStream = new MemoryStream())
            {
                using (var archive = new ZipArchive(outStream, ZipArchiveMode.Create, true))
                {
                    foreach (var item in pdfCollection)
                    {

                        var fileInArchive = archive.CreateEntry(item.Key, CompressionLevel.Optimal);
                        using (var entryStream = fileInArchive.Open())
                        using (var fileToCompressStream = item.Value)
                        {
                            fileToCompressStream.CopyTo(entryStream);
                        }
                    }
                }
                compressedBytes = outStream.ToArray();
            }
            return File(compressedBytes, "application/zip", fileNameZip);
        }

        #endregion

        #region Total Count Widget

        /// <summary>Shows the total count widget.</summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowTotalCountWidget()
        {
            var projectsCount = await this.projectRepo.CountAllByDataTable(this.EditionDto.Id, true);

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/TotalCountWidget", projectsCount), divIdOrClass = "#AudiovisualProjectsTotalCountWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Edition Count Widget

        /// <summary>Shows the edition count widget.</summary>
        /// <returns></returns>
        public async Task<ActionResult> ShowEditionCountWidget()
        {
            var projectsCount = await this.projectRepo.CountAllByDataTable(this.EditionDto.Id);
            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/EditionCountWidget", projectsCount), divIdOrClass = "#AudiovisualProjectsEditionCountWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Edition Count Gauge Widget

        /// <summary>
        /// Shows the edition count gauge widget.
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> ShowEditionCountGaugeWidget()
        {
            var projectsCount = await this.projectRepo.CountAllByDataTable(this.EditionDto.Id);
            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/EditionCountGaugeWidget", projectsCount), divIdOrClass = "#AudiovisualProjectsEditionCountGaugeWidget" },
                },
                chartData = projectsCount
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Details

        /// <summary>
        /// Detailses the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="searchKeywords">The search keywords.</param>
        /// <param name="interestUid">The interest uid.</param>
        /// <param name="evaluationStatusUid">The evaluation status uid.</param>
        /// <param name="projectModalityUid">The project modality uid.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Details(int? id, string searchKeywords = null, Guid? interestUid = null, Guid? evaluationStatusUid = null, Guid? projectModalityUid = null, int? page = 1, int? pageSize = 10)
        {
            if (!page.HasValue || page <= 0)
            {
                page++;
            }

            var projectDto = await this.projectRepo.FindAdminDetailsDtoByProjectIdAndByEditionIdAsync(id ?? 0, this.EditionDto.Id);
            if (projectDto == null)
            {
                this.StatusMessageToastr(string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()), Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);
                return RedirectToAction("Index", "Projects", new { Area = "Audiovisual" });
            }

            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.AudiovisualProjects, new List<BreadcrumbItemHelper>{
                new BreadcrumbItemHelper(Labels.AudioVisual, null),
                new BreadcrumbItemHelper(Labels.Projects, Url.Action("Index", "Projects", new { Area = "Audiovisual" })),
                new BreadcrumbItemHelper(projectDto?.Project?.GetTitleByLanguageCode(this.UserInterfaceLanguage) ?? Labels.Project, Url.Action("Details", "Projects", new { Area = "Audiovisual", id }))
            });

            #endregion

            var allProjectsIds = await this.projectRepo.FindAllProjectsIdsPagedAsync(
                this.EditionDto.Edition.Id,
                searchKeywords,
                new List<Guid?> { interestUid },
                evaluationStatusUid,
                null,
                projectModalityUid.HasValue ? new List<Guid?> { projectModalityUid } : new List<Guid?> { },
                page.Value,
                pageSize.Value);
            var currentProjectIdIndex = Array.IndexOf(allProjectsIds, id.Value) + 1; //Index start at 0, its a fix to "start at 1"

            ViewBag.SearchKeywords = searchKeywords;
            ViewBag.InterestUid = interestUid;
            ViewBag.EvaluationStatusUid = evaluationStatusUid;
            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;
            ViewBag.CurrentProjectIndex = currentProjectIdIndex;

            ViewBag.ProjectsTotalCount = await this.projectRepo.CountPagedAsync(
                this.EditionDto.Edition.Id,
                searchKeywords,
                new List<Guid?> { interestUid },
                evaluationStatusUid,
                null,
                projectModalityUid.HasValue ? new List<Guid?> { projectModalityUid } : new List<Guid?> { },
                page.Value,
                pageSize.Value
            );
            ViewBag.ApprovedProjectsIds = await this.projectRepo.FindAllApprovedCommissionProjectsIdsAsync(this.EditionDto.Edition.Id);

            return View(projectDto);
        }

        /// <summary>
        /// Previouses the commission evaluation details.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="searchKeywords">The search keywords.</param>
        /// <param name="interestUid">The interest uid.</param>
        /// <param name="evaluationStatusUid">The evaluation status uid.</param>
        /// <param name="projectModalityUid">The project modality uid.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> PreviousCommissionEvaluationDetails(int? id, string searchKeywords = null, Guid? interestUid = null, Guid? evaluationStatusUid = null, Guid? projectModalityUid = null, int? page = 1, int? pageSize = 10)
        {
            var allProjectsIds = await this.projectRepo.FindAllProjectsIdsPagedAsync(
                this.EditionDto.Edition.Id,
                searchKeywords,
                new List<Guid?> { interestUid },
                evaluationStatusUid,
                null,
                projectModalityUid.HasValue ? new List<Guid?> { projectModalityUid } : new List<Guid?> { },
                page.Value,
                pageSize.Value);

            var currentProjectIdIndex = Array.IndexOf(allProjectsIds, id.Value);
            var previousProjectId = allProjectsIds.ElementAtOrDefault(currentProjectIdIndex - 1);
            if (previousProjectId == 0)
            {
                previousProjectId = id.Value;
            }

            return RedirectToAction("Details",
                new
                {
                    id = previousProjectId,
                    searchKeywords,
                    interestUid,
                    projectModalityUid,
                    page,
                    pageSize
                });
        }

        /// <summary>
        /// Nexts the commission evaluation details.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="searchKeywords">The search keywords.</param>
        /// <param name="interestUid">The interest uid.</param>
        /// <param name="evaluationStatusUid">The evaluation status uid.</param>
        /// <param name="projectModalityUid">The project modality uid.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> NextCommissionEvaluationDetails(int? id, string searchKeywords = null, Guid? interestUid = null, Guid? evaluationStatusUid = null, Guid? projectModalityUid = null, int? page = 1, int? pageSize = 10)
        {
            var allProjectsIds = await this.projectRepo.FindAllProjectsIdsPagedAsync(
                this.EditionDto.Edition.Id,
                searchKeywords,
                new List<Guid?> { interestUid },
                evaluationStatusUid,
                null,
                projectModalityUid.HasValue ? new List<Guid?> { projectModalityUid } : new List<Guid?> { },
                page.Value,
                pageSize.Value);

            var currentProjectIdIndex = Array.IndexOf(allProjectsIds, id.Value);
            var nextProjectId = allProjectsIds.ElementAtOrDefault(currentProjectIdIndex + 1);
            if (nextProjectId == 0)
            {
                nextProjectId = id.Value;
            }

            return RedirectToAction("Details",
                new
                {
                    id = nextProjectId,
                    searchKeywords,
                    interestUid,
                    projectModalityUid,
                    page,
                    pageSize
                });
        }

        #region Main Information Widget

        /// <summary>Shows the main information widget.</summary>
        /// <param name="projectUid">The project uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowMainInformationWidget(Guid? projectUid)
        {
            var mainInformationWidgetDto = await this.projectRepo.FindAdminMainInformationWidgetDtoByProjectUidAsync(projectUid ?? Guid.Empty);
            if (mainInformationWidgetDto == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            ViewBag.ApprovedProjectsIds = await this.projectRepo.FindAllApprovedCommissionProjectsIdsAsync(this.EditionDto.Edition.Id);

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/MainInformationWidget", mainInformationWidgetDto), divIdOrClass = "#ProjectMainInformationWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #region Update

        /// <summary>Shows the update main information modal.</summary>
        /// <param name="projectUid">The project uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowUpdateMainInformationModal(Guid? projectUid)
        {
            UpdateProjectMainInformation cmd;

            try
            {
                var mainInformationWidgetDto = await this.projectRepo.FindAdminMainInformationWidgetDtoByProjectUidAsync(projectUid ?? Guid.Empty);
                if (mainInformationWidgetDto == null)
                {
                    throw new DomainException(string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()));
                }

                cmd = new UpdateProjectMainInformation(
                    mainInformationWidgetDto,
                    await this.CommandBus.Send(new FindAllLanguagesDtosAsync(this.UserInterfaceLanguage)),
                    true,
                    false,
                    false,
                    this.UserInterfaceLanguage,
                    ProjectModality.BusinessRound.Uid
                );
            }
            catch (DomainException ex)
            {
                return Json(new { status = "error", message = ex.GetInnerMessage() }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Modals/UpdateMainInformationModal", cmd), divIdOrClass = "#GlobalModalContainer" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>Updates the main information.</summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> UpdateMainInformation(UpdateProjectMainInformation cmd)
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
                    this.UserInterfaceLanguage,
                    true);
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
                var toastrError = result.Errors?.FirstOrDefault(e => e.Target == "ToastrError");

                //cmd.UpdateModelsAndLists(
                //    await this.interestRepo.FindAllGroupedByInterestGroupsAsync());

                return Json(new
                {
                    status = "error",
                    message = toastrError?.Message ?? ex.GetInnerMessage(),
                    pages = new List<dynamic>
                    {
                        new { page = this.RenderRazorViewToString("Modals/UpdateMainInformationForm", cmd), divIdOrClass = "#form-container" },
                    }
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return Json(new { status = "error", message = Messages.WeFoundAndError, }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Project, Labels.UpdatedM) });
        }

        #endregion

        #endregion

        #region Interest Widget

        /// <summary>Shows the interest widget.</summary>
        /// <param name="projectUid">The project uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowInterestWidget(Guid? projectUid)
        {
            var interestWidgetDto = await this.projectRepo.FindAdminInterestWidgetDtoByProjectUidAsync(projectUid ?? Guid.Empty);
            if (interestWidgetDto == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            ViewBag.GroupedInterests = await this.interestRepo.FindAllByProjectTypeIdAndGroupedByInterestGroupAsync(ProjectType.Audiovisual.Id);
            ViewBag.TargetAudiences = await this.targetAudienceRepo.FindAllByProjectTypeIdAsync(ProjectType.Audiovisual.Id);

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/InterestWidget", interestWidgetDto), divIdOrClass = "#ProjectInterestWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #region Update

        /// <summary>Shows the update interest modal.</summary>
        /// <param name="projectUid">The project uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowUpdateInterestModal(Guid? projectUid)
        {
            UpdateProjectInterests cmd;

            try
            {
                var interestWidgetDto = await this.projectRepo.FindAdminInterestWidgetDtoByProjectUidAsync(projectUid ?? Guid.Empty);
                if (interestWidgetDto == null)
                {
                    throw new DomainException(string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()));
                }

                cmd = new UpdateProjectInterests(
                    interestWidgetDto,
                    await this.interestRepo.FindAllDtosbyProjectTypeIdAsync(ProjectType.Audiovisual.Id),
                    await this.targetAudienceRepo.FindAllByProjectTypeIdAsync(ProjectType.Audiovisual.Id));
            }
            catch (DomainException ex)
            {
                return Json(new { status = "error", message = ex.GetInnerMessage() }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Modals/UpdateInterestModal", cmd), divIdOrClass = "#GlobalModalContainer" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>Updates the interests.</summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> UpdateInterests(UpdateProjectInterests cmd)
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
                    this.UserInterfaceLanguage,
                    true);
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
                var toastrError = result.Errors?.FirstOrDefault(e => e.Target == "ToastrError");

                cmd.UpdateDropdownProperties(
                    await this.targetAudienceRepo.FindAllByProjectTypeIdAsync(ProjectType.Audiovisual.Id));

                return Json(new
                {
                    status = "error",
                    message = toastrError?.Message ?? ex.GetInnerMessage(),
                    pages = new List<dynamic>
                    {
                        new { page = this.RenderRazorViewToString("Modals/UpdateInterestForm", cmd), divIdOrClass = "#form-container" },
                    }
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return Json(new { status = "error", message = Messages.WeFoundAndError, }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Project, Labels.UpdatedM) });
        }

        #endregion

        #endregion

        #region Links Widget

        /// <summary>Shows the links widget.</summary>
        /// <param name="projectUid">The project uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowLinksWidget(Guid? projectUid)
        {
            var linksWidgetDto = await this.projectRepo.FindAdminLinksWidgetDtoByProjectUidAsync(projectUid ?? Guid.Empty);
            if (linksWidgetDto == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/LinksWidget", linksWidgetDto), divIdOrClass = "#ProjectLinksWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #region Update

        /// <summary>Shows the update links modal.</summary>
        /// <param name="projectUid">The project uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowUpdateLinksModal(Guid? projectUid)
        {
            UpdateProjectLinks cmd;

            try
            {
                var linksWidgetDto = await this.projectRepo.FindAdminLinksWidgetDtoByProjectUidAsync(projectUid ?? Guid.Empty);
                if (linksWidgetDto == null)
                {
                    throw new DomainException(string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()));
                }

                cmd = new UpdateProjectLinks(linksWidgetDto);
            }
            catch (DomainException ex)
            {
                return Json(new { status = "error", message = ex.GetInnerMessage() }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Modals/UpdateLinksModal", cmd), divIdOrClass = "#GlobalModalContainer" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>Updates the links.</summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> UpdateLinks(UpdateProjectLinks cmd)
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
                    this.UserInterfaceLanguage,
                    true);
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
                var toastrError = result.Errors?.FirstOrDefault(e => e.Target == "ToastrError");

                //cmd.UpdateModelsAndLists(
                //    await this.interestRepo.FindAllGroupedByInterestGroupsAsync());

                return Json(new
                {
                    status = "error",
                    message = toastrError?.Message ?? ex.GetInnerMessage(),
                    pages = new List<dynamic>
                    {
                        new { page = this.RenderRazorViewToString("Modals/UpdateLinksForm", cmd), divIdOrClass = "#form-container" },
                    }
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return Json(new { status = "error", message = Messages.WeFoundAndError, }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Project, Labels.UpdatedM) });
        }

        #endregion

        #endregion

        #region Buyer Companies Widget

        /// <summary>Shows the buyer company widget.</summary>
        /// <param name="projectUid">The project uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowBuyerCompanyWidget(Guid? projectUid)
        {
            var buyerCompanyWidgetDto = await this.projectRepo.FindAdminBuyerCompanyWidgetDtoByProjectUidAsync(projectUid ?? Guid.Empty);
            if (buyerCompanyWidgetDto == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/BuyerCompanyWidget", buyerCompanyWidgetDto), divIdOrClass = "#ProjectBuyercompanyWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #region Update

        /// <summary>Shows the update buyer company modal.</summary>
        /// <param name="projectUid">The project uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowUpdateBuyerCompanyModal(Guid? projectUid)
        {
            ProjectDto buyerCompanyWidgetDto = null;

            try
            {
                buyerCompanyWidgetDto = await this.projectRepo.FindAdminBuyerCompanyWidgetDtoByProjectUidAsync(projectUid ?? Guid.Empty);
                if (buyerCompanyWidgetDto == null)
                {
                    return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (DomainException ex)
            {
                return Json(new { status = "error", message = ex.GetInnerMessage() }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Modals/UpdateBuyerCompanyModal", buyerCompanyWidgetDto), divIdOrClass = "#GlobalModalContainer" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>Shows the buyer company selected widget.</summary>
        /// <param name="projectUid">The project uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowBuyerCompanySelectedWidget(Guid? projectUid)
        {
            var buyerCompanyWidgetDto = await this.projectRepo.FindSiteBuyerCompanyWidgetDtoByProjectUidAsync(projectUid ?? Guid.Empty);
            if (buyerCompanyWidgetDto == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/BuyerCompanySelectedWidget", buyerCompanyWidgetDto), divIdOrClass = "#ProjectBuyerCompanySelectedWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>Shows the project match buyer company widget.</summary>
        /// <param name="projectUid">The project uid.</param>
        /// <param name="searchKeywords">The search keywords.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowProjectMatchBuyerCompanyWidget(Guid? projectUid, string searchKeywords, int page = 1, int pageSize = 10)
        {
            var interestWidgetDto = await this.projectRepo.FindSiteInterestWidgetDtoByProjectUidAsync(projectUid ?? Guid.Empty);
            if (interestWidgetDto == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            var matchAttendeeOrganizationDtos = await this.attendeeOrganizationRepo.FindAllAudiovisualMatchingAttendeeOrganizationsDtosAsync(this.EditionDto.Id, interestWidgetDto, searchKeywords, page, pageSize);

            ViewBag.ShowProjectMatchBuyerCompanySearch = $"&projectUid={projectUid}&pageSize={pageSize}";
            ViewBag.SearchKeywords = searchKeywords;

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/ProjectMatchBuyerCompanyWidget", matchAttendeeOrganizationDtos), divIdOrClass = "#ProjectMatchBuyerCompanyWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>Shows the project all buyer company widget.</summary>
        /// <param name="projectUid">The project uid.</param>
        /// <param name="searchKeywords">The search keywords.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowProjectAllBuyerCompanyWidget(Guid? projectUid, string searchKeywords, int page = 1, int pageSize = 10)
        {
            var interestWidgetDto = await this.projectRepo.FindSiteInterestWidgetDtoByProjectUidAsync(projectUid ?? Guid.Empty);
            if (interestWidgetDto == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            var attendeeOrganizationDtos = await this.attendeeOrganizationRepo.FindAllDtoByProjectBuyerAsync(this.EditionDto.Id, interestWidgetDto, searchKeywords, page, pageSize);

            ViewBag.ShowProjectAllBuyerCompanySearch = $"&projectUid={projectUid}&pageSize={pageSize}";
            ViewBag.SearchKeywords = searchKeywords;

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/ProjectAllBuyerCompanyWidget", attendeeOrganizationDtos), divIdOrClass = "#ProjectAllBuyerCompanyWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>Creates the buyer evaluation.</summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> CreateBuyerEvaluation(CreateProjectBuyerEvaluation cmd)
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
                    this.UserInterfaceLanguage,
                    true);
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
                var toastrError = result.Errors?.FirstOrDefault(e => e.Target == "ToastrError");

                //cmd.UpdateModelsAndLists(
                //    await this.interestRepo.FindAllGroupedByInterestGroupsAsync());

                return Json(new { status = "error", message = toastrError?.Message ?? ex.GetInnerMessage() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return Json(new { status = "error", message = Messages.WeFoundAndError, }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Project, Labels.UpdatedM) });
        }

        /// <summary>Deletes the buyer evaluation.</summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> DeleteBuyerEvaluation(DeleteProjectBuyerEvaluation cmd)
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
                    this.UserInterfaceLanguage,
                    true);
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
                var toastrError = result.Errors?.FirstOrDefault(e => e.Target == "ToastrError");

                //cmd.UpdateModelsAndLists(
                //    await this.interestRepo.FindAllGroupedByInterestGroupsAsync());

                return Json(new { status = "error", message = toastrError?.Message ?? ex.GetInnerMessage() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return Json(new { status = "error", message = Messages.WeFoundAndError, }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Project, Labels.UpdatedM) });
        }

        #endregion

        #endregion

        #region Audiovisual Commission Evaluation Widget 

        /// <summary>
        /// Shows the audiovisual commission evaluation widget.
        /// </summary>
        /// <param name="projectUid">The project uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowAudiovisualCommissionEvaluationWidget(Guid? projectUid)
        {
            var evaluationDto = await this.projectRepo.FindAudiovisualCommissionEvaluationWidgetDtoAsync(projectUid ?? Guid.Empty, this.AdminAccessControlDto.User.Id);
            if (evaluationDto == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            ViewBag.ApprovedProjectsIds = await this.projectRepo.FindAllApprovedCommissionProjectsIdsAsync(this.EditionDto.Edition.Id);

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/AudiovisualCommissionEvaluationWidget", evaluationDto), divIdOrClass = "#ProjectEvaluationWidget" },
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
        public async Task<ActionResult> AudiovisualComissionEvaluateProject(int projectId, decimal? grade)
        {
            if (this.EditionDto?.IsAudiovisualCommissionProjectEvaluationOpen() != true)
            {
                return Json(new { status = "error", message = Messages.EvaluationPeriodClosed }, JsonRequestBehavior.AllowGet);
            }

            var result = new AppValidationResult();

            try
            {
                var cmd = new AudiovisualComissionEvaluateProject(
                    await this.projectRepo.FindByIdAsync(projectId),
                    grade);

                cmd.UpdatePreSendProperties(
                  this.AdminAccessControlDto.User.Id,
                  this.AdminAccessControlDto.User.Uid,
                  this.EditionDto.Id,
                  this.EditionDto.Uid,
                  this.UserInterfaceLanguage,
                  true);
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
                message = string.Format(Messages.EntityActionSuccessfull, Labels.Project, Labels.Evaluated.ToLowerInvariant())
            });
        }

        #endregion

        #region Audiovisual Evaluators Widget

        /// <summary>
        /// Shows the audiovisual commission evaluators widget.
        /// </summary>
        /// <param name="projectUid">The project uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowAudiovisualCommissionEvaluatorsWidget(Guid? projectUid)
        {
            var evaluationDto = await this.projectRepo.FindAudiovisualCommissionEvaluatorsWidgetDtoAsync(projectUid ?? Guid.Empty);
            if (evaluationDto == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/AudiovisualCommissionEvaluatorsWidget", evaluationDto), divIdOrClass = "#ProjectEvaluatorsWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #endregion

        #region Delete

        /// <summary>
        /// Deletes the specified project.
        /// </summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Delete(DeleteProject cmd)
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
                    this.UserInterfaceLanguage,
                    true);

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

        #region Finds

        /// <summary>Finds all by filters.</summary>
        /// <param name="keywords">The keywords.</param>
        /// <param name="customFilter">The custom filter.</param>
        /// <param name="buyerOrganizationUid">The buyer organization uid.</param>
        /// <param name="page">The page.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> FindAllByFilters(string keywords, string customFilter = "", Guid? buyerOrganizationUid = null, int? page = 1)
        {
            var projectDtos = await this.projectRepo.FindAllDropdownDtoPaged(
                this.EditionDto.Id,
                keywords,
                customFilter,
                buyerOrganizationUid,
                page.Value,
                10);

            return Json(new
            {
                status = "success",
                HasPreviousPage = projectDtos.HasPreviousPage,
                HasNextPage = projectDtos.HasNextPage,
                TotalItemCount = projectDtos.TotalItemCount,
                PageCount = projectDtos.PageCount,
                PageNumber = projectDtos.PageNumber,
                PageSize = projectDtos.PageSize,
                Projects = projectDtos?.Select(p => new ProjectDropdownDto
                {
                    Uid = p.Project.Uid,
                    ProjectTitle = p.GetTitleDtoByLanguageCode(this.UserInterfaceLanguage)?.ProjectTitle?.Value,
                    SellerTradeName = p.SellerAttendeeOrganizationDto.Organization.TradeName,
                    SellerCompanyName = p.SellerAttendeeOrganizationDto.Organization.CompanyName,
                    SellerPicture = p.SellerAttendeeOrganizationDto.Organization.ImageUploadDate.HasValue ? this.fileRepo.GetImageUrl(FileRepositoryPathType.UserImage, p.SellerAttendeeOrganizationDto.Organization.Uid, p.SellerAttendeeOrganizationDto.Organization.ImageUploadDate, true) : null,
                    SellerUid = p.SellerAttendeeOrganizationDto.Organization.Uid
                })?.ToList()
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}