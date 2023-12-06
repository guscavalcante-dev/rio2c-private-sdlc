// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Renan Valentim
// Created          : 07-24-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-27-2023
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
using PlataformaRio2C.Application.ViewModels;
using ClosedXML.Excel;
using PlataformaRio2C.Domain.ApiModels;
using PlataformaRio2C.Domain.Statics;
using PlataformaRio2C.Infra.CrossCutting.Tools.CustomActionResults;
using System.IO;
using PlataformaRio2c.Infra.Data.FileRepository;
using PlataformaRio2c.Infra.Data.FileRepository.Helpers;

namespace PlataformaRio2C.Web.Admin.Areas.Innovation.Controllers
{
    /// <summary>ProjectsController</summary>
    [AjaxAuthorize(Order = 1, Roles = Constants.Role.AnyAdmin)]
    [AuthorizeCollaboratorType(Order = 2, Types = Constants.CollaboratorType.AdminInnovation)]
    public class ProjectsController : BaseController
    {
        private readonly IInnovationOrganizationRepository innovationOrganizationRepo;
        private readonly IProjectEvaluationStatusRepository evaluationStatusRepo;
        private readonly IAttendeeInnovationOrganizationRepository attendeeInnovationOrganizationRepo;
        private readonly IInnovationOrganizationTrackOptionGroupRepository innovationOrganizationTrackOptionGroupRepo;
        private readonly IInnovationOrganizationObjectivesOptionRepository innovationOrganizationObjectivesOptionRepo;
        private readonly IInnovationOrganizationTechnologyOptionRepository innovationOrganizationTechnologyOptionRepo;
        private readonly IInnovationOrganizationExperienceOptionRepository innovationOrganizationExperienceOptionRepo;
        private readonly IInnovationOrganizationSustainableDevelopmentObjectivesOptionRepository innovationOrganizationSustainableDevelopmentObjectivesOptionRepo;
        private readonly IFileRepository fileRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectsController" /> class.
        /// </summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="identityController">The identity controller.</param>
        /// <param name="innovationOrganizationRepository">The innovation organization repository.</param>
        /// <param name="evaluationStatusRepository">The evaluation status repository.</param>
        /// <param name="attendeeInnovationOrganizationRepository">The attendee innovation organization repository.</param>
        /// <param name="innovationOrganizationTrackOptionGroupRepository">The innovation organization track option group repository.</param>
        /// <param name="innovationOrganizationObjectivesOptionRepository">The innovation organization objectives option repository.</param>
        /// <param name="innovationOrganizationTechnologyOptionRepository">The innovation organization technology option repository.</param>
        /// <param name="innovationOrganizationExperienceOptionRepository">The innovation organization experience option repository.</param>
        /// <param name="innovationOrganizationSustainableDevelopmentObjectivesOptionRepository">The innovation organization sustainable development objectives option repository.</param>
        /// <param name="fileRepository">The file repository.</param>
        public ProjectsController(
            IMediator commandBus,
            IdentityAutenticationService identityController,
            IInnovationOrganizationRepository innovationOrganizationRepository,
            IProjectEvaluationStatusRepository evaluationStatusRepository,
            IAttendeeInnovationOrganizationRepository attendeeInnovationOrganizationRepository,
            IInnovationOrganizationTrackOptionGroupRepository innovationOrganizationTrackOptionGroupRepository,
            IInnovationOrganizationObjectivesOptionRepository innovationOrganizationObjectivesOptionRepository,
            IInnovationOrganizationTechnologyOptionRepository innovationOrganizationTechnologyOptionRepository,
            IInnovationOrganizationExperienceOptionRepository innovationOrganizationExperienceOptionRepository,
            IInnovationOrganizationSustainableDevelopmentObjectivesOptionRepository innovationOrganizationSustainableDevelopmentObjectivesOptionRepository,
            IFileRepository fileRepository
            )
            : base(commandBus, identityController)
        {
            this.innovationOrganizationRepo = innovationOrganizationRepository;
            this.evaluationStatusRepo = evaluationStatusRepository;
            this.attendeeInnovationOrganizationRepo = attendeeInnovationOrganizationRepository;
            this.innovationOrganizationTrackOptionGroupRepo = innovationOrganizationTrackOptionGroupRepository;
            this.innovationOrganizationObjectivesOptionRepo = innovationOrganizationObjectivesOptionRepository;
            this.innovationOrganizationTechnologyOptionRepo = innovationOrganizationTechnologyOptionRepository;
            this.innovationOrganizationExperienceOptionRepo = innovationOrganizationExperienceOptionRepository;
            this.innovationOrganizationSustainableDevelopmentObjectivesOptionRepo = innovationOrganizationSustainableDevelopmentObjectivesOptionRepository;
            this.fileRepo = fileRepository;
        }

        #region List

        /// <summary>
        /// Indexes the specified search view model.
        /// </summary>
        /// <param name="searchViewModel">The search view model.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Index(InnovationProjectSearchViewModel searchViewModel)
        {
            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.StartupsProjects, new List<BreadcrumbItemHelper>{
                new BreadcrumbItemHelper(Labels.Startups, null),
                new BreadcrumbItemHelper(Labels.Projects, Url.Action("Index", "Projects", new { Area = "Innovation" }))
            });

            #endregion

            searchViewModel.UpdateModelsAndLists(
                await this.innovationOrganizationTrackOptionGroupRepo.FindAllAsync(),
                await this.evaluationStatusRepo.FindAllAsync(),
                this.UserInterfaceLanguage);

            return View(searchViewModel);
        }

        /// <summary>
        /// Shows the list widget.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="searchViewModel">The search view model.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowListWidget(IDataTablesRequest request, InnovationProjectSearchViewModel searchViewModel)
        {
            int page = request.Start / request.Length;
            int pageSize = request.Length;
            page++; //Necessary because DataTable is zero index based.

            var attendeeInnovationOrganizationJsonDtos = await this.attendeeInnovationOrganizationRepo.FindAllByDataTableAsync(
                this.EditionDto.Id,
                request.Search?.Value,
                new List<Guid?> { searchViewModel.InnovationOrganizationTrackOptionGroupUid },
                searchViewModel.EvaluationStatusUid,
                searchViewModel.ShowBusinessRounds,
                page,
                pageSize,
                request.GetSortColumns());

            var approvedAttendeeInnovationOrganizationIds = await this.attendeeInnovationOrganizationRepo.FindAllApprovedAttendeeInnovationOrganizationsIdsAsync(this.EditionDto.Id);

            foreach (var attendeeInnovationOrganizationJsonDto in attendeeInnovationOrganizationJsonDtos)
            {
                attendeeInnovationOrganizationJsonDto.IsApproved = approvedAttendeeInnovationOrganizationIds.Contains(attendeeInnovationOrganizationJsonDto.AttendeeInnovationOrganizationId);

                #region Translate InnovationOrganizationTracksNames

                for (int i = 0; i < attendeeInnovationOrganizationJsonDto.InnovationOrganizationTracksNames.Count(); i++)
                {
                    attendeeInnovationOrganizationJsonDto.InnovationOrganizationTracksNames[i] = attendeeInnovationOrganizationJsonDto.InnovationOrganizationTracksNames[i].GetSeparatorTranslation(this.UserInterfaceLanguage, '|');
                }

                #endregion
            }

            IDictionary<string, object> additionalParameters = new Dictionary<string, object>();
            if (attendeeInnovationOrganizationJsonDtos.TotalItemCount <= 0)
            {
                if (this.EditionDto.IsInnovationProjectEvaluationOpen() && (
                    searchViewModel.EvaluationStatusUid == ProjectEvaluationStatus.Accepted.Uid ||
                    searchViewModel.EvaluationStatusUid == ProjectEvaluationStatus.Refused.Uid))
                {
                    additionalParameters.Add("noRecordsFoundMessage", 
                        $"{string.Format(Messages.TheEvaluationPeriodRunsFrom, this.EditionDto.InnovationCommissionEvaluationStartDate.ToBrazilTimeZone().ToShortDateString(), this.EditionDto.InnovationCommissionEvaluationEndDate.ToBrazilTimeZone().ToShortDateString())}.</br>{Messages.TheProjectsWillReceiveFinalGradeAtPeriodEnds}");
                }
                else if (!this.EditionDto.IsInnovationProjectEvaluationOpen() &&
                    searchViewModel.EvaluationStatusUid == ProjectEvaluationStatus.UnderEvaluation.Uid)
                {
                    additionalParameters.Add("noRecordsFoundMessage", 
                        $"{Messages.EvaluationPeriodClosed}<br/>{string.Format(Messages.ProjectsNotFoundWithStatus, Labels.UnderEvaluation)}");
                }
            }

            var response = DataTablesResponse.Create(request, attendeeInnovationOrganizationJsonDtos.TotalItemCount, attendeeInnovationOrganizationJsonDtos.TotalItemCount, attendeeInnovationOrganizationJsonDtos, additionalParameters);

            return Json(new
            {
                status = "success",
                dataTable = response,
                searchKeywords = request.Search?.Value,
                searchViewModel.InnovationOrganizationTrackOptionGroupUid,
                searchViewModel.EvaluationStatusUid,
                searchViewModel.ShowBusinessRounds,
                page,
                pageSize
            }, JsonRequestBehavior.AllowGet);
        }

        #region Reports

        /// <summary>
        /// Exports the evaluations by project report to excel.
        /// </summary>
        /// <param name="searchViewModel">The search view model.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ExportEvaluationsByProjectReportToExcel(InnovationProjectSearchViewModel searchViewModel)
        {
            StringBuilder data = new StringBuilder();
            data.AppendLine($"{Labels.Startup}; {Labels.Project}; {Labels.ProductsOrServices}; {Labels.Status}; {Labels.Votes}; {Labels.Average}");

            var attendeeInnovationOrganizationJsonDtos = await this.attendeeInnovationOrganizationRepo.FindAllByDataTableAsync(
                this.EditionDto.Id,
                searchViewModel.Search, 
                new List<Guid?> { searchViewModel.InnovationOrganizationTrackOptionGroupUid },
                searchViewModel.EvaluationStatusUid,
                searchViewModel.ShowBusinessRounds,
                1, 
                10000, 
                new List<Tuple<string, string>>());

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
                fileName = $@"{Labels.Startups} - {Labels.EvaluationsByProjectReport}_{DateTime.UtcNow.ToStringFileNameTimestamp()}.csv",
                fileContent = data.ToString()
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Exports the evaluations by evaluators report to excel.
        /// </summary>
        /// <param name="searchViewModel">The search view model.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ExportEvaluationsByEvaluatorsReportToExcel(InnovationProjectSearchViewModel searchViewModel)
        {
            StringBuilder data = new StringBuilder();
            data.AppendLine($"{Labels.Startup}; {Labels.Project}; {Labels.Evaluation}; {Labels.Evaluator};");

            var attendeeInnovationOrganizationJsonDtos = await this.attendeeInnovationOrganizationRepo.FindAllByDataTableAsync(
                this.EditionDto.Id,
                searchViewModel.Search, 
                new List<Guid?> { searchViewModel.InnovationOrganizationTrackOptionGroupUid },
                searchViewModel.EvaluationStatusUid,
                searchViewModel.ShowBusinessRounds,
                1, 
                10000, 
                new List<Tuple<string, string>>());

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
                fileName = $@"{Labels.Startups} - {Labels.EvaluationsByEvaluatorReport}_{DateTime.UtcNow.ToStringFileNameTimestamp()}.csv",
                fileContent = data.ToString()
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Exports the projects report to excel.
        /// </summary>
        /// <param name="searchViewModel">The search view model.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ExportProjectsReportToExcel(InnovationProjectSearchViewModel searchViewModel)
        {
            string fileName = $@"{Labels.StartupsProjects}_{DateTime.UtcNow.ToStringFileNameTimestamp()}";
            string filePath = Path.Combine(Path.GetTempPath(), fileName + ".xlsx");

            try
            {
                var attendeeInnovationOrganizationReportDtos = await this.attendeeInnovationOrganizationRepo.FindAllInnovationProjectsReportByDataTable(
                    this.EditionDto.Id,
                    searchViewModel.Search,
                    new List<Guid?> { searchViewModel.InnovationOrganizationTrackOptionGroupUid },
                    searchViewModel.EvaluationStatusUid,
                    searchViewModel.ShowBusinessRounds,
                    1,
                    10000,
                    new List<Tuple<string, string>>()
                );

                var approvedAttendeeInnovationOrganizationsIds = await this.attendeeInnovationOrganizationRepo.FindAllApprovedAttendeeInnovationOrganizationsIdsAsync(this.EditionDto.Id);

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add(Labels.StartupsProjects);

                    #region Header

                    var lineIndex = 1;
                    var columnIndex = 0;
                    var skipFinalAdjustmentsColumnIndexes = new List<int>();

                    // DataTable Columns
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.Company;
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.ProductOrServiceName;
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.Description;
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.Verticals;
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.CreativeEconomyThemes;
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.BusinessRound;
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.CreateDate;
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.UpdateDate;

                    // Extra Columns
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.Document;
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.AgentName;
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.Email;
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.PhoneNumber;
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.CellPhone;
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.FoundationYear;
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.AccumulatedRevenueForLast3Months;
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.Website;
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.BusinessDefinition;
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.BusinessFocus;
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.BusinessDifferentials;
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.BusinessStage;
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.BusinessEconomicModel;
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.BusinessOperationalModel;
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.Competitors;
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.UsedTechnologies;
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.ExperiencesThatCompanyHasParticipatedIn;
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.PitchingParticipationObjectives;
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.SustainableDevelopmentObjectives;
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = $"{Labels.Founders} - {Labels.Name}";
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = $"{Labels.Founders} - {Labels.Curriculum}";
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.PresentationVideo;
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.PresentationFile;
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.Photo;

                    skipFinalAdjustmentsColumnIndexes.Add(columnIndex);

                    #endregion

                    if (attendeeInnovationOrganizationReportDtos.Any())
                    {
                        #region Rows

                        foreach (var attendeeInnovationOrganizationReportDto in attendeeInnovationOrganizationReportDtos)
                        {
                            lineIndex++;
                            columnIndex = 0;

                            // DataTable Columns
                            worksheet.Cell(lineIndex, columnIndex += 1).Value = attendeeInnovationOrganizationReportDto.CompanyName;
                            worksheet.Cell(lineIndex, columnIndex += 1).Value = attendeeInnovationOrganizationReportDto.ServiceName;
                            worksheet.Cell(lineIndex, columnIndex += 1).Value = attendeeInnovationOrganizationReportDto.Description;
                            worksheet.Cell(lineIndex, columnIndex += 1).Value = attendeeInnovationOrganizationReportDto.AttendeeInnovationOrganizationTrackDtos.Select(aiotDto => aiotDto.GetNameTranslation(this.UserInterfaceLanguage)).ToString("; ");
                            worksheet.Cell(lineIndex, columnIndex += 1).Value = attendeeInnovationOrganizationReportDto.AttendeeInnovationOrganizationTrackDtos.Select(aiotDto => aiotDto.GetGroupNameTranslation(this.UserInterfaceLanguage)).ToString("; ");
                            worksheet.Cell(lineIndex, columnIndex += 1).Value = attendeeInnovationOrganizationReportDto.WouldYouLikeParticipateBusinessRound?.ToYesOrNoString();
                            worksheet.Cell(lineIndex, columnIndex += 1).Value = attendeeInnovationOrganizationReportDto.CreateDate?.ToStringHourMinute();
                            worksheet.Cell(lineIndex, columnIndex += 1).Value = attendeeInnovationOrganizationReportDto.UpdateDate?.ToStringHourMinute();

                            // Extra Columns
                            worksheet.Cell(lineIndex, columnIndex += 1).Value = attendeeInnovationOrganizationReportDto.Document;
                            worksheet.Cell(lineIndex, columnIndex).Style.NumberFormat.Format = "00000";

                            worksheet.Cell(lineIndex, columnIndex += 1).Value = attendeeInnovationOrganizationReportDto.ResponsibleCollaboratorDto.FullName;
                            worksheet.Cell(lineIndex, columnIndex += 1).Value = attendeeInnovationOrganizationReportDto.ResponsibleCollaboratorDto.Email;

                            worksheet.Cell(lineIndex, columnIndex += 1).Value = attendeeInnovationOrganizationReportDto.ResponsibleCollaboratorDto.PhoneNumber;
                            worksheet.Cell(lineIndex, columnIndex).Style.NumberFormat.Format = "00000";

                            worksheet.Cell(lineIndex, columnIndex += 1).Value = attendeeInnovationOrganizationReportDto.ResponsibleCollaboratorDto.CellPhone;
                            worksheet.Cell(lineIndex, columnIndex).Style.NumberFormat.Format = "00000";

                            worksheet.Cell(lineIndex, columnIndex += 1).Value = attendeeInnovationOrganizationReportDto.FoundationYear;

                            worksheet.Cell(lineIndex, columnIndex += 1).Value = attendeeInnovationOrganizationReportDto.AccumulatedRevenue;
                            worksheet.Cell(lineIndex, columnIndex).Style.NumberFormat.Format = "R$0.00";

                            worksheet.Cell(lineIndex, columnIndex += 1).Value = attendeeInnovationOrganizationReportDto.Website;
                            worksheet.Cell(lineIndex, columnIndex += 1).Value = attendeeInnovationOrganizationReportDto.BusinessDefinition;
                            worksheet.Cell(lineIndex, columnIndex += 1).Value = attendeeInnovationOrganizationReportDto.BusinessFocus;
                            worksheet.Cell(lineIndex, columnIndex += 1).Value = attendeeInnovationOrganizationReportDto.BusinessDifferentials;
                            worksheet.Cell(lineIndex, columnIndex += 1).Value = attendeeInnovationOrganizationReportDto.BusinessStage;
                            worksheet.Cell(lineIndex, columnIndex += 1).Value = attendeeInnovationOrganizationReportDto.BusinessEconomicModel;
                            worksheet.Cell(lineIndex, columnIndex += 1).Value = attendeeInnovationOrganizationReportDto.BusinessOperationalModel;

                            worksheet.Cell(lineIndex, columnIndex += 1).Value = attendeeInnovationOrganizationReportDto.AttendeeInnovationOrganizationCompetitorDtos.Select(dto => dto.Name);
                            worksheet.Cell(lineIndex, columnIndex += 1).Value = attendeeInnovationOrganizationReportDto.AttendeeInnovationOrganizationTechnologyDtos.Select(dto => $"{dto.GetNameTranslation(this.UserInterfaceLanguage)}" + (!string.IsNullOrEmpty(dto.AdditionalInfo) ? $" ({dto.AdditionalInfo})" : "")).ToString("; ");
                            worksheet.Cell(lineIndex, columnIndex += 1).Value = attendeeInnovationOrganizationReportDto.AttendeeInnovationOrganizationExperienceDtos.Select(dto => $"{dto.GetNameTranslation(this.UserInterfaceLanguage)}" + (!string.IsNullOrEmpty(dto.AdditionalInfo) ? $" ({dto.AdditionalInfo})" : "")).ToString("; ");
                            worksheet.Cell(lineIndex, columnIndex += 1).Value = attendeeInnovationOrganizationReportDto.AttendeeInnovationOrganizationObjectiveDtos.Select(dto => $"{dto.GetNameTranslation(this.UserInterfaceLanguage)}" + (!string.IsNullOrEmpty(dto.AdditionalInfo) ? $" ({dto.AdditionalInfo})" : "")).ToString("; ");
                            worksheet.Cell(lineIndex, columnIndex += 1).Value = attendeeInnovationOrganizationReportDto.AttendeeInnovationOrganizationSustainableDevelopmentObjectiveDtos.Select(dto => $"{dto.GetNameTranslation(this.UserInterfaceLanguage)}" + (!string.IsNullOrEmpty(dto.AdditionalInfo) ? $" ({dto.AdditionalInfo})" : "")).ToString("; ");

                            worksheet.Cell(lineIndex, columnIndex += 1).Value = attendeeInnovationOrganizationReportDto.AttendeeInnovationOrganizationFounderDtos.Select(dto => $"{dto.Name} ({dto.GetWorkDedicationNameTranslation(this.UserInterfaceLanguage)})").ToString("; ");
                            worksheet.Cell(lineIndex, columnIndex += 1).Value = attendeeInnovationOrganizationReportDto.AttendeeInnovationOrganizationFounderDtos.Select(dto => dto.Curriculum).ToString("; ");

                            worksheet.Cell(lineIndex, columnIndex += 1).Value = attendeeInnovationOrganizationReportDto.VideoUrl;
                            
                            worksheet.Cell(lineIndex, columnIndex += 1).Value = attendeeInnovationOrganizationReportDto.PresentationUploadDate.HasValue ?
                                FileHelper.GetFileUrl(
                                    FileRepositoryPathType.InnovationOrganizationPresentationFile, 
                                    attendeeInnovationOrganizationReportDto.AttendeeInnovationOrganizationUid, 
                                    attendeeInnovationOrganizationReportDto.PresentationUploadDate, 
                                    attendeeInnovationOrganizationReportDto.PresentationFileExtension) 
                                : "";

                            worksheet.Cell(lineIndex, columnIndex += 1).Value = attendeeInnovationOrganizationReportDto.ImageUploadDate.HasValue ?
                                this.fileRepo.GetImageUrl(FileRepositoryPathType.OrganizationImage, attendeeInnovationOrganizationReportDto.InnovationOrganizationUid, attendeeInnovationOrganizationReportDto.ImageUploadDate, true) : "";
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

                    var range = worksheet.Range(worksheet.FirstCellUsed().Address, worksheet.LastCellUsed().Address);
                    var table = range.CreateTable();
                    table.Theme = XLTableTheme.TableStyleMedium9;

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

        #endregion

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

        #region Edition Count Pie Widget

        /// <summary>Shows the edition count pie widget.</summary>
        /// <returns></returns>
        public async Task<ActionResult> ShowEditionCountPieWidget()
        {
            var innovationOrganizationGroupedByTrackDtos = await this.attendeeInnovationOrganizationRepo.FindEditionCountPieWidgetDto(this.EditionDto.Id);
            var projectsCount = await this.attendeeInnovationOrganizationRepo.CountAsync(this.EditionDto.Id);

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/EditionCountPieWidget", innovationOrganizationGroupedByTrackDtos), divIdOrClass = "#InnovationProjectsEditionCountPieWidget" },
                },
                innovationProjectsGroupedByTrackDtos = innovationOrganizationGroupedByTrackDtos.Select(i => new InnovationOrganizationGroupedByTrackDto
                {
                    InnovationProjectsTotalCount = i.InnovationProjectsTotalCount,
                    TrackName = i.TrackName.GetSeparatorTranslation(ViewBag.UserInterfaceLanguage as string, '|')
                }),
                innovationProjectsTotalCount = projectsCount
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Details

        /// <summary>
        /// Evaluations the details.
        /// </summary>
        /// <param name="searchViewModel">The search form.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> EvaluationDetails(InnovationProjectSearchViewModel searchViewModel)
        {
            if (!searchViewModel.Page.HasValue || searchViewModel.Page <= 0)
            {
                searchViewModel.Page++;
            }

            var attendeeInnovationOrganizationDto = await this.attendeeInnovationOrganizationRepo.FindDtoToEvaluateAsync(searchViewModel.Id ?? 0);
            if (attendeeInnovationOrganizationDto == null)
            {
                this.StatusMessageToastr(string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()), Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);
                return RedirectToAction("Index", "Projects", new { Area = "Innovation" });
            }

            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.StartupsProjects, new List<BreadcrumbItemHelper>{
                new BreadcrumbItemHelper(Labels.Projects, Url.Action("Index", "Projects", new { Area = "Innovation" })),
                new BreadcrumbItemHelper(attendeeInnovationOrganizationDto?.InnovationOrganization?.Name ?? Labels.Project, Url.Action("EvaluationDetails", "Projects", new { Area = "Innovation", searchViewModel.Id }))
            });

            #endregion

            var allInnovationOrganizationsIds = await this.attendeeInnovationOrganizationRepo.FindAllInnovationOrganizationsIdsPagedAsync(
                this.EditionDto.Edition.Id,
                searchViewModel.Search,
                new List<Guid?> { searchViewModel.InnovationOrganizationTrackOptionGroupUid },
                searchViewModel.EvaluationStatusUid,
                searchViewModel.ShowBusinessRounds,
                searchViewModel.Page.Value,
                searchViewModel.PageSize.Value);

            var currentProjectIdIndex = Array.IndexOf(allInnovationOrganizationsIds, searchViewModel.Id.Value) + 1; //Index start at 0, its a fix to "start at 1"

            ViewBag.InnovationProjectSearchViewModel = searchViewModel;
            ViewBag.CurrentInnovationProjectIndex = currentProjectIdIndex;
            ViewBag.ApprovedAttendeeInnovationOrganizationsIds = await this.attendeeInnovationOrganizationRepo.FindAllApprovedAttendeeInnovationOrganizationsIdsAsync(this.EditionDto.Edition.Id);
            ViewBag.InnovationProjectsTotalCount = await this.attendeeInnovationOrganizationRepo.CountPagedAsync(
                this.EditionDto.Edition.Id,
                searchViewModel.Search, 
                new List<Guid?> { searchViewModel.InnovationOrganizationTrackOptionGroupUid },
                searchViewModel.EvaluationStatusUid,
                searchViewModel.ShowBusinessRounds,
                searchViewModel.Page.Value,
                searchViewModel.PageSize.Value);

            return View(attendeeInnovationOrganizationDto);
        }

        /// <summary>
        /// Previouses the evaluation details.
        /// </summary>
        /// <param name="searchViewModel">The search form.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> PreviousEvaluationDetails(InnovationProjectSearchViewModel searchViewModel)
        {
            var allInnovationProjectsIds = await this.attendeeInnovationOrganizationRepo.FindAllInnovationOrganizationsIdsPagedAsync(
                this.EditionDto.Edition.Id,
                searchViewModel.Search,
                new List<Guid?> { searchViewModel.InnovationOrganizationTrackOptionGroupUid },
                searchViewModel.EvaluationStatusUid,
                searchViewModel.ShowBusinessRounds,
                searchViewModel.Page.Value,
                searchViewModel.PageSize.Value);

            var currentInnovationProjectIdIndex = Array.IndexOf(allInnovationProjectsIds, searchViewModel.Id.Value);
            var previousProjectId = allInnovationProjectsIds.ElementAtOrDefault(currentInnovationProjectIdIndex - 1);
            if (previousProjectId == 0)
            {
                previousProjectId = searchViewModel.Id.Value;
            }
            searchViewModel.Id = previousProjectId;

            return RedirectToAction("EvaluationDetails", searchViewModel);
        }

        /// <summary>
        /// Nexts the evaluation details.
        /// </summary>
        /// <param name="searchViewModel">The search form.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> NextEvaluationDetails(InnovationProjectSearchViewModel searchViewModel)
        {
            var allInnovationProjectsIds = await this.attendeeInnovationOrganizationRepo.FindAllInnovationOrganizationsIdsPagedAsync(
                this.EditionDto.Edition.Id,
                searchViewModel.Search,
                new List<Guid?> { searchViewModel.InnovationOrganizationTrackOptionGroupUid },
                searchViewModel.EvaluationStatusUid,
                searchViewModel.ShowBusinessRounds,
                searchViewModel.Page.Value,
                searchViewModel.PageSize.Value);

            var currentInnovationProjectIdIndex = Array.IndexOf(allInnovationProjectsIds, searchViewModel.Id.Value);
            var nextProjectId = allInnovationProjectsIds.ElementAtOrDefault(currentInnovationProjectIdIndex + 1);
            if (nextProjectId == 0)
            {
                nextProjectId = searchViewModel.Id.Value;
            }
            searchViewModel.Id = nextProjectId;

            return RedirectToAction("EvaluationDetails", searchViewModel);
        }

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

        #region Business Information Widget

        /// <summary>
        /// Shows the main information widget.
        /// </summary>
        /// <param name="attendeeInnovationOrganizationUid">The attendee innovation organization uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowBusinessInformationWidget(Guid? attendeeInnovationOrganizationUid)
        {
            var mainInformationWidgetDto = await this.attendeeInnovationOrganizationRepo.FindBusinessInformationWidgetDtoAsync(attendeeInnovationOrganizationUid ?? Guid.Empty);
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

            ViewBag.InnovationOrganizationTrackOptionGroupDtos = await this.innovationOrganizationTrackOptionGroupRepo.FindAllDtoAsync();

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

        #region Sustainable Development Objectives Widget

        /// <summary>
        /// Shows the sustainable development objectives widget.
        /// </summary>
        /// <param name="attendeeInnovationOrganizationUid">The attendee innovation organization uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowSustainableDevelopmentWidget(Guid? attendeeInnovationOrganizationUid)
        {
            var sustainableDevelopmentWidgetDto = await this.attendeeInnovationOrganizationRepo.FindSustainableDevelopmentWidgetDtoAsync(attendeeInnovationOrganizationUid ?? Guid.Empty);
            if (sustainableDevelopmentWidgetDto == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Startup, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }
            
            ViewBag.InnovationOrganizationSustainableDevelopmentObjectivesOptions = await this.innovationOrganizationSustainableDevelopmentObjectivesOptionRepo.FindAllAsync();

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/SustainableDevelopmentWidget", sustainableDevelopmentWidgetDto), divIdOrClass = "#ProjectsSustainableDevelopmentWidget" },
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
        public async Task<ActionResult> Evaluate(int innovationOrganizationId, decimal? grade)
        {
            if (this.EditionDto?.IsInnovationProjectEvaluationOpen() != true)
            {
                return Json(new { status = "error", message = Messages.EvaluationPeriodClosed }, JsonRequestBehavior.AllowGet);
            }

            var result = new AppValidationResult();

            try
            {
                var cmd = new EvaluateInnovationOrganization(
                    await this.innovationOrganizationRepo.FindByIdAsync(innovationOrganizationId),
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

        #region Founders Widget

        /// <summary>
        /// Shows the evaluators widget.
        /// </summary>
        /// <param name="attendeeInnovationOrganizationUid">The project uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowFoundersWidget(Guid? attendeeInnovationOrganizationUid)
        {
            var attendeeInnovationOrganizationDto = await this.attendeeInnovationOrganizationRepo.FindFoundersWidgetDtoAsync(attendeeInnovationOrganizationUid ?? Guid.Empty);
            if (attendeeInnovationOrganizationDto == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Startup, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/FoundersWidget", attendeeInnovationOrganizationDto), divIdOrClass = "#ProjectsFoundersWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Presentation Widget

        /// <summary>Shows the clipping widget.</summary>
        /// <param name="projectUid">The project uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowPresentationWidget(Guid? attendeeInnovationOrganizationUid)
        {
            var presentationWidgetDto = await this.attendeeInnovationOrganizationRepo.FindPresentationWidgetDtoAsync(attendeeInnovationOrganizationUid ?? Guid.Empty);
            if (presentationWidgetDto == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/PresentationWidget", presentationWidgetDto), divIdOrClass = "#ProjectsPresentationWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #endregion

        #region Delete

        /// <summary>Deletes the specified delete music project.</summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Delete(DeleteInnovationOrganization cmd)
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