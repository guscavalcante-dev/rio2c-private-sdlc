﻿// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 03-01-2020
//
// Last Modified By : Daniel Giese Rodrigues
// Last Modified On : 04-16-2025
// ***********************************************************************
// <copyright file="ProjectsController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using ClosedXML.Excel;
using DataTables.AspNet.Core;
using DataTables.AspNet.Mvc5;
using MediatR;
using PlataformaRio2c.Infra.Data.FileRepository;
using PlataformaRio2C.Application;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Application.ViewModels;
using PlataformaRio2C.Domain.ApiModels;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Interfaces.Repositories.Music.Projects;
using PlataformaRio2C.Domain.Statics;
using PlataformaRio2C.Infra.CrossCutting.Identity.AuthorizeAttributes;
using PlataformaRio2C.Infra.CrossCutting.Identity.Service;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.CustomActionResults;
using PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using PlataformaRio2C.Infra.CrossCutting.Tools.Helpers;
using PlataformaRio2C.Web.Admin.Controllers;
using PlataformaRio2C.Web.Admin.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Constants = PlataformaRio2C.Domain.Constants;

namespace PlataformaRio2C.Web.Admin.Areas.Music.Controllers
{
    /// <summary>ProjectsController</summary>
    [AjaxAuthorize(Order = 1, Roles = Constants.Role.AnyAdmin)]
    [AuthorizeCollaboratorType(Order = 2, Types = Constants.CollaboratorType.AdminMusic)]
    public class ProjectsController : BaseController
    {
        private readonly IMusicProjectRepository musicProjectRepo;
        private readonly IMusicBusinessRoundProjectRepository musicBusinessRoundProjectRepo;
        private readonly IMusicGenreRepository musicGenreRepo;
        private readonly IProjectEvaluationStatusRepository evaluationStatusRepo;
        private readonly IMusicBandRepository musicBandRepo;
        private readonly IFileRepository fileRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectsController" /> class.
        /// </summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="identityController">The identity controller.</param>
        /// <param name="musicProjectRepository">The music project repository.</param>
        /// <param name="musicGenreRepository">The music genre repository.</param>
        /// <param name="evaluationStatusRepository">The evaluation status repository.</param>
        /// <param name="musicBandRepository">The music band repository.</param>
        /// <param name="fileRepository">The file repository.</param>
        public ProjectsController(
            IMediator commandBus,
            IdentityAutenticationService identityController,
            IMusicProjectRepository musicProjectRepository,
            IMusicGenreRepository musicGenreRepository,
            IProjectEvaluationStatusRepository evaluationStatusRepository,
            IMusicBandRepository musicBandRepository,
            IFileRepository fileRepository,
            IMusicBusinessRoundProjectRepository musicBusinessRoundProjectRepo)
            : base(commandBus, identityController)
        {
            this.musicProjectRepo = musicProjectRepository;
            this.musicGenreRepo = musicGenreRepository;
            this.evaluationStatusRepo = evaluationStatusRepository;
            this.musicBandRepo = musicBandRepository;
            this.fileRepo = fileRepository;
            this.musicBusinessRoundProjectRepo = musicBusinessRoundProjectRepo;
        }

        #region List

        /// <summary>
        /// Indexes the specified search view model.
        /// </summary>
        /// <param name="searchViewModel">The search view model.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Index(MusicProjectSearchViewModel searchViewModel)
        {
            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.MusicProjects, new List<BreadcrumbItemHelper>{
                new BreadcrumbItemHelper(Labels.Music, null),
                new BreadcrumbItemHelper(Labels.Projects, Url.Action("Index", "Projects", new { Area = "Music" }))
            });

            #endregion

            searchViewModel.UpdateModelsAndLists(
                await this.musicGenreRepo.FindAllAsync(),
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
        public async Task<ActionResult> ShowListWidget(IDataTablesRequest request, MusicProjectSearchViewModel searchViewModel)
        {
            int page = request.Start / request.Length;
            int pageSize = request.Length;
            page++; //Necessary because DataTable is zero index based.

            var musicProjectJsonDtos = await this.musicProjectRepo.FindAllByDataTableAsync(
                this.EditionDto.Id,
                request.Search?.Value,
                searchViewModel.MusicGenreUid,
                ProjectEvaluationStatus.GetId(searchViewModel.EvaluationStatusUid),
                searchViewModel.ShowBusinessRounds,
                page,
                pageSize,
                request.GetSortColumns(),
                this.AdminAccessControlDto.User.Id);

            var approvedAttendeeMusicBandsIds = await this.musicProjectRepo.FindAllApprovedAttendeeMusicBandsIdsAsync(this.EditionDto.Id, this.AdminAccessControlDto.User.Id);

            foreach (var musicProjectJsonDto in musicProjectJsonDtos)
            {
                musicProjectJsonDto.IsApproved = approvedAttendeeMusicBandsIds.Contains(musicProjectJsonDto.AttendeeMusicBandId);

                #region Translate MusicBandTypeName

                musicProjectJsonDto.MusicBandTypeName = musicProjectJsonDto.MusicBandTypeName.GetSeparatorTranslation(this.UserInterfaceLanguage, '|');

                #endregion

                #region Translate MusicTargetAudiencesNames

                for (int i = 0; i < musicProjectJsonDto.MusicTargetAudiencesNames.Count(); i++)
                {
                    musicProjectJsonDto.MusicTargetAudiencesNames[i] = musicProjectJsonDto.MusicTargetAudiencesNames[i].GetSeparatorTranslation(this.UserInterfaceLanguage, '|');
                }

                #endregion

                #region Translate MusicGenreNames

                for (int i = 0; i < musicProjectJsonDto.MusicGenreNames.Count(); i++)
                {
                    musicProjectJsonDto.MusicGenreNames[i] = musicProjectJsonDto.MusicGenreNames[i].GetSeparatorTranslation(this.UserInterfaceLanguage, '|');
                }

                #endregion
            }

            IDictionary<string, object> additionalParameters = new Dictionary<string, object>();
            if (musicProjectJsonDtos.TotalItemCount <= 0)
            {
                if (this.EditionDto.IsMusicPitchingComissionEvaluationOpen() && (
                    searchViewModel.EvaluationStatusUid == ProjectEvaluationStatus.Accepted.Uid ||
                    searchViewModel.EvaluationStatusUid == ProjectEvaluationStatus.Refused.Uid))
                {
                    additionalParameters.Add("noRecordsFoundMessage",
                        $"{string.Format(Messages.TheEvaluationPeriodRunsFrom, this.EditionDto.MusicCommissionEvaluationStartDate.ToBrazilTimeZone().ToShortDateString(), this.EditionDto.MusicCommissionEvaluationEndDate.ToBrazilTimeZone().ToShortDateString())}.</br>{Messages.TheProjectsWillReceiveFinalGradeAtPeriodEnds}");
                }
                else if (!this.EditionDto.IsMusicPitchingComissionEvaluationOpen() &&
                    searchViewModel.EvaluationStatusUid == ProjectEvaluationStatus.UnderEvaluation.Uid)
                {
                    additionalParameters.Add("noRecordsFoundMessage",
                        $"{Messages.EvaluationPeriodClosed}<br/>{string.Format(Messages.ProjectsNotFoundWithStatus, Labels.UnderEvaluation)}");
                }
            }

            var response = DataTablesResponse.Create(request, musicProjectJsonDtos.TotalItemCount, musicProjectJsonDtos.TotalItemCount, musicProjectJsonDtos, additionalParameters);

            return Json(new
            {
                status = "success",
                dataTable = response,
                searchKeywords = request.Search?.Value,
                searchViewModel.MusicGenreUid,
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
        public async Task<ActionResult> ExportEvaluationsByProjectReportToExcel(MusicProjectSearchViewModel searchViewModel)
        {
            StringBuilder data = new StringBuilder();
            bool ptBR = this.UserInterfaceLanguage == "pt-br";
            if (ptBR)
                data.AppendLine("Banda; Tipo de artista; Estilo musical; Público-Alvo; Data de Criação; Qtd. Votos; Status;");
            else
                data.AppendLine("Music Band; Participant profile; Musical style; Target Audience; Create Date; Qty. Evaluation; Status;");

            var musicProjectJsonDtos = await this.musicProjectRepo.FindAllByDataTableAsync(
                this.EditionDto.Id,
                searchViewModel.Search,
                searchViewModel.MusicGenreUid,
                ProjectEvaluationStatus.GetId(searchViewModel.EvaluationStatusUid),
                searchViewModel.ShowBusinessRounds,
                1,
                10000,
                new List<Tuple<string, string>>(),
                this.AdminAccessControlDto.User.Id);

            var approvedAttendeeMusicBandsIds = await this.musicProjectRepo.FindAllApprovedAttendeeMusicBandsIdsAsync(this.EditionDto.Id, this.AdminAccessControlDto.User.Id);

            foreach (var item in musicProjectJsonDtos)
            {
                var audiences = string.Join(",", item.MusicTargetAudiencesNames);
                var genre = string.Join("|", item.MusicGenreNames);
                var createDate = ptBR ? item.CreateDate.ToString("dd/MM/yy") : item.CreateDate.ToString("MM/dd/yy");
                var status = ptBR ?
                    approvedAttendeeMusicBandsIds.Contains(item.AttendeeMusicBandId) ? "Aprovado" : "Reprovado" :
                    approvedAttendeeMusicBandsIds.Contains(item.AttendeeMusicBandId) ? "Accepted" : "Refused";

                data.AppendLine(item.MusicBandName + ";" +
                                item.MusicBandTypeName + ";" +
                                audiences + ";" +
                                genre + ";" +
                                createDate + ";" +
                                item.EvaluationsCount + ";" +
                                status);
            }

            var dtFileName = ptBR ? DateTime.Now.ToString("yyMMddHHmmss") : DateTime.Now.ToString("yyddMMHHmmss");
            return Json(new
            {
                fileName = $@"{Labels.Music} - {Labels.EvaluationsByProjectReport}_{DateTime.UtcNow.ToStringFileNameTimestamp()}.csv",
                fileContent = data.ToString()
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Exports the evaluations by evaluators report to excel.
        /// </summary>
        /// <param name="searchViewModel">The search view model.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ExportEvaluationsByEvaluatorsReportToExcel(MusicProjectSearchViewModel searchViewModel)
        {
            // TODO: This method needs refactor. RIO2CMY-1533
            // The evaluation logic has changed. Before it was via Grade, and now it is via approve/reject. This method still using Grade!

            StringBuilder data = new StringBuilder();
            bool ptBR = this.UserInterfaceLanguage == "pt-br";
            if (ptBR)
                data.AppendLine("Banda; Jurado; Status;");
            else
                data.AppendLine("Music Band; Evaluator; Status;");

            var musicProjectJsonDtos = await this.musicProjectRepo.FindAllByDataTableAsync(
                this.EditionDto.Id,
                searchViewModel.Search,
                searchViewModel.MusicGenreUid,
                ProjectEvaluationStatus.GetId(searchViewModel.EvaluationStatusUid),
                searchViewModel.ShowBusinessRounds,
                1,
                10000,
                new List<Tuple<string, string>>(),
                this.AdminAccessControlDto.User.Id);

            var approvedAttendeeMusicBandsIds = await this.musicProjectRepo.FindAllApprovedAttendeeMusicBandsIdsAsync(this.EditionDto.Id, this.AdminAccessControlDto.User.Id);

            foreach (var item in musicProjectJsonDtos)
            {
                var evaluationDto = await this.musicProjectRepo.FindEvaluatorsWidgetDtoAsync(item.MusicProjectUid);
                var createDate = ptBR ? item.CreateDate.ToString("dd/MM/yy") : item.CreateDate.ToString("MM/dd/yy");
                var status = ptBR ?
                    approvedAttendeeMusicBandsIds.Contains(item.AttendeeMusicBandId) ? "Aprovado" : "Reprovado" :
                    approvedAttendeeMusicBandsIds.Contains(item.AttendeeMusicBandId) ? "Accepted" : "Refused";

                foreach (var eval in evaluationDto.AttendeeMusicBandDto.AttendeeMusicBandEvaluationsDtos)
                {
                    data.AppendLine(
                        item.MusicBandName + ";" +
                        eval.EvaluatorUser.Name + ";" +
                        status
                    );
                }
            }

            return Json(new
            {
                fileName = $@"{Labels.Music} - {Labels.EvaluationsByEvaluatorReport}_{DateTime.UtcNow.ToStringFileNameTimestamp()}.csv",
                fileContent = data.ToString()
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Exports the projects report to excel.
        /// </summary>
        /// <param name="searchViewModel">The search view model.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ExportProjectsReportToExcel(MusicProjectSearchViewModel searchViewModel)
        {
            string fileName = $@"{Labels.MusicBandsReport}_{DateTime.UtcNow.ToStringFileNameTimestamp()}";
            string filePath = Path.Combine(Path.GetTempPath(), fileName + ".xlsx");

            try
            {
                var musicProjectReportDtos = await this.musicProjectRepo.FindAllMusicProjectsReportByDataTable(
                    this.EditionDto.Id,
                    searchViewModel.Search,
                    searchViewModel.MusicGenreUid,
                    ProjectEvaluationStatus.GetId(searchViewModel.EvaluationStatusUid),
                    searchViewModel.ShowBusinessRounds,
                    1,
                    10000,
                    new List<Tuple<string, string>>(),
                    this.AdminAccessControlDto.User.Id
                );

                var approvedAttendeeMusicBandsIds = await this.musicProjectRepo.FindAllApprovedAttendeeMusicBandsIdsAsync(this.EditionDto.Id, this.AdminAccessControlDto.User.Id);

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add(Labels.MusicProjects);

                    #region Header

                    var lineIndex = 1;
                    var columnIndex = 0;
                    var skipFinalAdjustmentsColumnIndexes = new List<int>();

                    // DataTable Columns
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.MusicBand;
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.BandType;
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.MusicGenre;
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.TargetAudience;
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.Evaluation;
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.CreateDate;
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.UpdateDate;

                    // Extra Columns
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.MusicBandFormationYear;
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.BusinessRound;
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.Facebook;
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.Instagram;
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.Twitter;
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.YouTube;
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.VideoClip;
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = $@"{Labels.Music} 1";
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = $@"{Labels.Music} 2";
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.Clipping;
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.Release;
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = $@"{Labels.Responsible} - {Labels.Name}";
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = $@"{Labels.Responsible} - {Labels.Email}";
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = $@"{Labels.Responsible} - {Labels.PhoneNumber}";
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = $@"{Labels.Responsible} - {Labels.Document}";
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = $@"{Labels.Responsible} - {Labels.Address}";
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = $@"{Labels.Responsible} - {Labels.Country}";
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = $@"{Labels.Responsible} - {Labels.State}";
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = $@"{Labels.Responsible} - {Labels.City}";
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = $@"{Labels.Responsible} - {Labels.ZipCode}";
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.MusicBandMembers;
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.MusicBandTeamMember;
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.ProjectsReleased;
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.MainMusicInfluences;
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.Photo;

                    skipFinalAdjustmentsColumnIndexes.Add(columnIndex);

                    #endregion

                    if (musicProjectReportDtos.Any())
                    {
                        #region Rows

                        foreach (var musicProjectReportDto in musicProjectReportDtos)
                        {
                            lineIndex++;
                            columnIndex = 0;

                            // DataTable Columns
                            worksheet.Cell(lineIndex, columnIndex += 1).Value = musicProjectReportDto.MusicBandName;//AttendeeOrganizationBasesDtos?.Select(ao => ao.OrganizationBaseDto.Name)?.ToString(", ");
                            worksheet.Cell(lineIndex, columnIndex += 1).Value = musicProjectReportDto.MusicBandType.GetNameTranslation(this.UserInterfaceLanguage);
                            worksheet.Cell(lineIndex, columnIndex += 1).Value = musicProjectReportDto.MusicGenresApiDtos?.Select(mg => $"{mg.MusicGenre.GetNameTranslation(this.UserInterfaceLanguage)}" + (mg.MusicGenre.HasAdditionalInfo ? $" ({mg.AdditionalInfo})" : ""))?.ToString("; ");
                            worksheet.Cell(lineIndex, columnIndex += 1).Value = musicProjectReportDto.TargetAudiencesApiDtos?.Select(ta => $"{ta.TargetAudience.GetNameTranslation(this.UserInterfaceLanguage)}")?.ToString("; ");
                            worksheet.Cell(lineIndex, columnIndex += 1).Value = this.EditionDto.IsMusicPitchingComissionEvaluationOpen() ? Labels.UnderEvaluation :
                                (approvedAttendeeMusicBandsIds.Contains(musicProjectReportDto.MusicBandId) ? Labels.ProjectAccepted : Labels.ProjectRefused);
                            worksheet.Cell(lineIndex, columnIndex += 1).Value = musicProjectReportDto.CreateDate.ToStringHourMinute();
                            worksheet.Cell(lineIndex, columnIndex += 1).Value = musicProjectReportDto.UpdateDate.ToStringHourMinute();

                            // Extra Columns
                            worksheet.Cell(lineIndex, columnIndex += 1).Value = musicProjectReportDto.FormationDate;
                            worksheet.Cell(lineIndex, columnIndex += 1).Value = musicProjectReportDto.WouldYouLikeParticipateBusinessRound.ToYesOrNoString();
                            worksheet.Cell(lineIndex, columnIndex += 1).Value = musicProjectReportDto.Facebook;
                            worksheet.Cell(lineIndex, columnIndex += 1).Value = musicProjectReportDto.Instagram;
                            worksheet.Cell(lineIndex, columnIndex += 1).Value = musicProjectReportDto.Twitter;
                            worksheet.Cell(lineIndex, columnIndex += 1).Value = musicProjectReportDto.YouTube;
                            worksheet.Cell(lineIndex, columnIndex += 1).Value = musicProjectReportDto.MusicProjectApiDto.VideoUrl;
                            worksheet.Cell(lineIndex, columnIndex += 1).Value = musicProjectReportDto.MusicProjectApiDto.Music1Url;
                            worksheet.Cell(lineIndex, columnIndex += 1).Value = musicProjectReportDto.MusicProjectApiDto.Music2Url;
                            worksheet.Cell(lineIndex, columnIndex += 1).Value = musicProjectReportDto.MusicProjectApiDto.Clipping;
                            worksheet.Cell(lineIndex, columnIndex += 1).Value = musicProjectReportDto.MusicProjectApiDto.Release;
                            worksheet.Cell(lineIndex, columnIndex += 1).Value = musicProjectReportDto.MusicBandResponsibleApiDto?.Name;
                            worksheet.Cell(lineIndex, columnIndex += 1).Value = musicProjectReportDto.MusicBandResponsibleApiDto?.Email;
                            worksheet.Cell(lineIndex, columnIndex += 1).Value = musicProjectReportDto.MusicBandResponsibleApiDto?.PhoneNumber;
                            worksheet.Cell(lineIndex, columnIndex).Style.NumberFormat.Format = "00000";
                            worksheet.Cell(lineIndex, columnIndex).Style.NumberFormat.Format = "00000";
                            worksheet.Cell(lineIndex, columnIndex += 1).Value = musicProjectReportDto.MusicBandResponsibleApiDto?.Document;
                            worksheet.Cell(lineIndex, columnIndex += 1).Value = musicProjectReportDto.MusicBandResponsibleApiDto?.Address;
                            worksheet.Cell(lineIndex, columnIndex += 1).Value = musicProjectReportDto.MusicBandResponsibleApiDto?.Country;
                            worksheet.Cell(lineIndex, columnIndex += 1).Value = musicProjectReportDto.MusicBandResponsibleApiDto?.State;
                            worksheet.Cell(lineIndex, columnIndex += 1).Value = musicProjectReportDto.MusicBandResponsibleApiDto?.City;
                            worksheet.Cell(lineIndex, columnIndex += 1).Value = musicProjectReportDto.MusicBandResponsibleApiDto?.ZipCode;
                            worksheet.Cell(lineIndex, columnIndex += 1).Value = musicProjectReportDto.MusicBandMembersApiDtos?.Select(mbm => $"{mbm.Name} ({mbm.MusicInstrumentName})")?.ToString("; ");
                            worksheet.Cell(lineIndex, columnIndex += 1).Value = musicProjectReportDto.MusicBandTeamMembersApiDtos?.Select(mbtm => $"{mbtm.Name} ({mbtm.Role})")?.ToString("; ");
                            worksheet.Cell(lineIndex, columnIndex += 1).Value = musicProjectReportDto.ReleasedMusicProjectsApiDtos?.Select(rmp => $"{rmp.Name} ({rmp.Year})")?.ToString("; ");
                            worksheet.Cell(lineIndex, columnIndex += 1).Value = musicProjectReportDto.MainMusicInfluences;
                            worksheet.Cell(lineIndex, columnIndex += 1).Value = musicProjectReportDto.ImageUploadDate.HasValue ?
                                this.fileRepo.GetImageUrl(FileRepositoryPathType.MusicBandImage, musicProjectReportDto.MusicBandUid, musicProjectReportDto.ImageUploadDate, true) : "";
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

        #region Edition Count Pie Widget

        /// <summary>Shows the edition count pie widget.</summary>
        /// <returns></returns>
        public async Task<ActionResult> ShowEditionCountPieWidget()
        {
            var musicBandGroupedByGenreDtos = await this.musicProjectRepo.FindEditionCountPieWidgetDto(this.EditionDto.Id);
            var projectsCount = await this.musicProjectRepo.CountAsync(this.EditionDto.Id);

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/EditionCountPieWidget", musicBandGroupedByGenreDtos), divIdOrClass = "#MusicProjectsEditionCountPieWidget" },
                },
                musicBandGroupedByGenreDtos = musicBandGroupedByGenreDtos.Select(i => new MusicBandGroupedByGenreDto
                {
                    MusicBandsTotalCount = i.MusicBandsTotalCount,
                    MusicGenreName = i.MusicGenreName.GetSeparatorTranslation(ViewBag.UserInterfaceLanguage as string, '|')
                }),
                musicBandsTotalCount = projectsCount
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
        /// <param name="showBusinessRounds">The show business rounds.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> EvaluationDetails(
            int? id,
            string searchKeywords = null,
            Guid? musicGenreUid = null,
            Guid? evaluationStatusUid = null,
            bool? showBusinessRounds = false,
            int? page = 1,
            int? pageSize = 12)
        {
            if (!page.HasValue || page <= 0)
            {
                page++;
            }

            var musicProjectDto = await this.musicProjectRepo.FindDtoToEvaluateAsync(id ?? 0);
            if (musicProjectDto == null)
            {
                this.StatusMessageToastr(string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()), Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);
                return RedirectToAction("Index", "Projects", new { Area = "Music" });
            }

            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.MusicProjects, new List<BreadcrumbItemHelper>{
                new BreadcrumbItemHelper(Labels.Music, null),
                new BreadcrumbItemHelper(Labels.Projects, Url.Action("Index", "Projects", new { Area = "Music" })),
                new BreadcrumbItemHelper(musicProjectDto.AttendeeMusicBandDto?.MusicBand?.Name ?? Labels.Project, Url.Action("EvaluationDetails", "Projects", new { Area = "Music", id }))
            });

            #endregion

            var allMusicProjectsIds = await this.musicProjectRepo.FindAllMusicProjectsIdsPagedAsync(
                this.EditionDto.Edition.Id,
                searchKeywords,
                musicGenreUid,
                ProjectEvaluationStatus.GetId(evaluationStatusUid),
                showBusinessRounds ?? false,
                page.Value,
                pageSize.Value,
                this.AdminAccessControlDto.User.Id);
            var currentMusicProjectIdIndex = Array.IndexOf(allMusicProjectsIds, id.Value) + 1; //Index start at 0, its a fix to "start at 1"

            ViewBag.SearchKeywords = searchKeywords;
            ViewBag.MusicGenreUid = musicGenreUid;
            ViewBag.EvaluationStatusUid = evaluationStatusUid;
            ViewBag.ShowBusinessRounds = showBusinessRounds;

            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;
            ViewBag.CurrentMusicProjectIndex = currentMusicProjectIdIndex;

            ViewBag.MusicProjectsTotalCount = await this.musicProjectRepo.CountPagedAsync(
                this.EditionDto.Edition.Id,
                searchKeywords,
                musicGenreUid,
                ProjectEvaluationStatus.GetId(evaluationStatusUid),
                showBusinessRounds ?? false,
                page.Value,
                pageSize.Value,
                this.AdminAccessControlDto.User.Id);

            ViewBag.ApprovedAttendeeMusicBandsIds = await this.musicProjectRepo.FindAllApprovedAttendeeMusicBandsIdsAsync(this.EditionDto.Edition.Id, this.AdminAccessControlDto.User.Id);

            return View(musicProjectDto);
        }

        /// <summary>
        /// Previouses the evaluation details.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="searchKeywords">The search keywords.</param>
        /// <param name="musicGenreUid">The music genre uid.</param>
        /// <param name="evaluationStatusUid">The evaluation status uid.</param>
        /// <param name="showBusinessRounds">The show business rounds.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> PreviousEvaluationDetails(
            int? id,
            string searchKeywords = null,
            Guid? musicGenreUid = null,
            Guid? evaluationStatusUid = null,
            bool? showBusinessRounds = false,
            int? page = 1,
            int? pageSize = 12)
        {
            var allMusicProjectsIds = await this.musicProjectRepo.FindAllMusicProjectsIdsPagedAsync(
                this.EditionDto.Edition.Id,
                searchKeywords,
                musicGenreUid,
                ProjectEvaluationStatus.GetId(evaluationStatusUid),
                showBusinessRounds ?? false,
                page.Value,
                pageSize.Value,
                this.AdminAccessControlDto.User.Id
                );

            var currentMusicProjectIdIndex = Array.IndexOf(allMusicProjectsIds, id.Value);
            var previousProjectId = allMusicProjectsIds.ElementAtOrDefault(currentMusicProjectIdIndex - 1);
            if (previousProjectId == 0)
            {
                previousProjectId = id.Value;
            }

            return RedirectToAction("EvaluationDetails",
                new
                {
                    id = previousProjectId,
                    searchKeywords,
                    musicGenreUid,
                    evaluationStatusUid,
                    showBusinessRounds,
                    page,
                    pageSize
                });
        }

        /// <summary>
        /// Nexts the evaluation details.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="searchKeywords">The search keywords.</param>
        /// <param name="musicGenreUid">The music genre uid.</param>
        /// <param name="evaluationStatusUid">The evaluation status uid.</param>
        /// <param name="showBusinessRounds">The show business rounds.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> NextEvaluationDetails(int? id, string searchKeywords = null, Guid? musicGenreUid = null, Guid? evaluationStatusUid = null, bool? showBusinessRounds = false, int? page = 1, int? pageSize = 12)
        {
            var allMusicProjectsIds = await this.musicProjectRepo.FindAllMusicProjectsIdsPagedAsync(
                this.EditionDto.Edition.Id,
                searchKeywords,
                musicGenreUid,
                ProjectEvaluationStatus.GetId(evaluationStatusUid),
                showBusinessRounds ?? false,
                page.Value,
                pageSize.Value,
                this.AdminAccessControlDto.User.Id);

            var currentMusicProjectIdIndex = Array.IndexOf(allMusicProjectsIds, id.Value);
            var nextProjectId = allMusicProjectsIds.ElementAtOrDefault(currentMusicProjectIdIndex + 1);
            if (nextProjectId == 0)
            {
                nextProjectId = id.Value;
            }

            return RedirectToAction("EvaluationDetails",
                new
                {
                    id = nextProjectId,
                    searchKeywords,
                    musicGenreUid,
                    evaluationStatusUid,
                    showBusinessRounds,
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
            var mainInformationWidgetDto = await this.musicProjectRepo.FindMainInformationWidgetDtoAsync(projectUid ?? Guid.Empty);
            if (mainInformationWidgetDto == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
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

        #region Members Widget

        /// <summary>Shows the members widget.</summary>
        /// <param name="projectUid">The project uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowMembersWidget(Guid? projectUid)
        {
            var membersWidgetDto = await this.musicProjectRepo.FindMembersWidgetDtoAsync(projectUid ?? Guid.Empty);
            if (membersWidgetDto == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/MembersWidget", membersWidgetDto), divIdOrClass = "#ProjectMembersWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Team Members Widget

        /// <summary>Shows the team members widget.</summary>
        /// <param name="projectUid">The project uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowTeamMembersWidget(Guid? projectUid)
        {
            var teamMembersWidgetDto = await this.musicProjectRepo.FindTeamMembersWidgetDtoAsync(projectUid ?? Guid.Empty);
            if (teamMembersWidgetDto == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/TeamMembersWidget", teamMembersWidgetDto), divIdOrClass = "#ProjectTeamMembersWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Released Music Projects Widget

        /// <summary>Shows the released projects widget.</summary>
        /// <param name="projectUid">The project uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowReleasedProjectsWidget(Guid? projectUid)
        {
            var releasedProjectsWidgetDto = await this.musicProjectRepo.FindReleasedProjectsWidgetDtoAsync(projectUid ?? Guid.Empty);
            if (releasedProjectsWidgetDto == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/ReleasedProjectsWidget", releasedProjectsWidgetDto), divIdOrClass = "#ReleasedProjectsWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Responsible Widget

        /// <summary>Shows the responsible widget.</summary>
        /// <param name="projectUid">The project uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowResponsibleWidget(Guid? projectUid)
        {
            var projectResponsibleWidgetDto = await this.musicProjectRepo.FindProjectResponsibleWidgetDtoAsync(projectUid ?? Guid.Empty);
            if (projectResponsibleWidgetDto == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/ResponsibleWidget", projectResponsibleWidgetDto), divIdOrClass = "#ProjectResponsibleWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Video and Music Widget

        /// <summary>Shows the video and music widget.</summary>
        /// <param name="projectUid">The project uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowVideoAndMusicWidget(Guid? projectUid)
        {
            var videoAndMusicWidgetDto = await this.musicProjectRepo.FindVideoAndMusicWidgetDtoAsync(projectUid ?? Guid.Empty);
            if (videoAndMusicWidgetDto == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/VideoAndMusicWidget", videoAndMusicWidgetDto), divIdOrClass = "#ProjectVideoAndMusicWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Cilpping Widget

        /// <summary>Shows the clipping widget.</summary>
        /// <param name="projectUid">The project uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowClippingWidget(Guid? projectUid)
        {
            var clippingWidgetDto = await this.musicProjectRepo.FindClippingWidgetDtoAsync(projectUid ?? Guid.Empty);
            if (clippingWidgetDto == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/ClippingWidget", clippingWidgetDto), divIdOrClass = "#ProjectClippingWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Social Networks Widget

        /// <summary>Shows the social networks widget.</summary>
        /// <param name="projectUid">The project uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowSocialNetworksWidget(Guid? projectUid)
        {
            var socialNetworksWidget = await this.musicProjectRepo.FindSocialNetworksWidgetDtoAsync(projectUid ?? Guid.Empty);
            if (socialNetworksWidget == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/SocialNetworksWidget", socialNetworksWidget), divIdOrClass = "#ProjectSocialNetworksWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Evaluation Grade Widget 

        /// <summary>
        /// Shows the evaluation grade widget.
        /// </summary>
        /// <param name="projectUid">The project uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowEvaluationGradeWidget(Guid? projectUid)
        {
            var evaluationDto = await this.musicProjectRepo.FindEvaluationGradeWidgetDtoAsync(projectUid ?? Guid.Empty, this.AdminAccessControlDto.User.Id);
            if (evaluationDto == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            ViewBag.ApprovedAttendeeMusicBandsIds = await this.musicProjectRepo.FindAllApprovedAttendeeMusicBandsIdsAsync(this.EditionDto.Edition.Id, this.AdminAccessControlDto.User.Id);

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
            if (this.EditionDto?.IsMusicPitchingComissionEvaluationOpen() != true)
            {
                return Json(new { status = "error", message = Messages.EvaluationPeriodClosed }, JsonRequestBehavior.AllowGet);
            }

            var result = new AppValidationResult();

            try
            {
                var cmd = new EvaluateMusicBand(
                    await this.musicBandRepo.FindByIdAsync(musicBandId),
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
                message = string.Format(Messages.EntityActionSuccessfull, Labels.MusicBand, Labels.Evaluated.ToLowerInvariant())
            });
        }

        #endregion

        #region Evaluators Widget

        /// <summary>
        /// Shows the evaluators widget.
        /// </summary>
        /// <param name="projectUid">The project uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowEvaluatorsWidget(Guid? projectUid)
        {
            var evaluationDto = await this.musicProjectRepo.FindEvaluatorsWidgetDtoAsync(projectUid ?? Guid.Empty);
            if (evaluationDto == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
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

        #endregion

        #region Delete

        /// <summary>Deletes the specified delete music project.</summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Delete(DeleteMusicProject cmd)
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
            var projectDtos = await this.musicBusinessRoundProjectRepo.FindAllDropdownDtoPaged(
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
                    Uid = p.Uid,
                    ProjectTitle = p.SellerAttendeeCollaboratorDto.Collaborator.GetStageNameOrBadgeOrFullName(),
                    SellerTradeName = p.SellerAttendeeCollaboratorDto.Collaborator.GetNameAbbreviation(),
                    SellerCompanyName = p.SellerAttendeeCollaboratorDto.Collaborator.GetDisplayName(),
                    SellerPicture = p.SellerAttendeeCollaboratorDto.Collaborator.ImageUploadDate.HasValue ? this.fileRepo.GetImageUrl(FileRepositoryPathType.UserImage, p.SellerAttendeeCollaboratorDto.Collaborator.Uid, p.SellerAttendeeCollaboratorDto.Collaborator.ImageUploadDate, true) : null,
                    SellerUid = p.SellerAttendeeCollaboratorDto.Collaborator.Uid
                })?.ToList()
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}