// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : William Sergio Almado Junior
// Created          : 01-13-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 02-15-2020
// ***********************************************************************
// <copyright file="ReportsController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.CrossCutting.Identity.AuthorizeAttributes;
using PlataformaRio2C.Infra.CrossCutting.Identity.Service;
using PlataformaRio2C.Web.Admin.Filters;
using Constants = PlataformaRio2C.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using PlataformaRio2C.Infra.CrossCutting.Tools.Helpers;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Application.ViewModels;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;
using System.IO;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;

namespace PlataformaRio2C.Web.Admin.Controllers
{
    /// <summary>ReportsController</summary>
    [AjaxAuthorize(Order = 1, Roles = Constants.Role.AnyAdmin)]
    [AuthorizeCollaboratorType(Order = 2, Types = Constants.CollaboratorType.AdminAudiovisual + "," + Constants.CollaboratorType.CommissionAudiovisual)]
    public class ReportsController : BaseController
    {
        private readonly IProjectRepository projectRepo;
        private readonly IInterestRepository interestRepo;
        private readonly ITargetAudienceRepository targetAudienceRepo;

        /// <summary>Initializes a new instance of the <see cref="ReportsController"/> class.</summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="identityController">The identity controller.</param>
        /// <param name="projectRepository">The project repository.</param>
        /// <param name="interestRepository">The interest repository.</param>
        /// <param name="targetAudienceRepository">The target audience repository.</param>
        public ReportsController(
            IMediator commandBus,
            IdentityAutenticationService identityController,
            IProjectRepository projectRepository,
            IInterestRepository interestRepository,
            ITargetAudienceRepository targetAudienceRepository)
            : base(commandBus, identityController)
        {
            this.projectRepo = projectRepository;
            this.interestRepo = interestRepository;
            this.targetAudienceRepo = targetAudienceRepository;
        }

        #region Audiovisual Projects

        #region Listing

        /// <summary>Audiovisuals the subscriptions.</summary>
        /// <param name="searchViewModel">The search view model.</param>
        /// <returns></returns>
        public async Task<ActionResult> AudiovisualSubscriptions(ReportsAudiovisualSearchViewModel searchViewModel)
        {
            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.Reports, new List<BreadcrumbItemHelper>{
                new BreadcrumbItemHelper(Labels.AudiovisualSubscriptionProjectReport, Url.Action("AudiovisualSubscriptions", "Reports", new { Area = "" }))
            });

            #endregion

            ViewBag.GenreInterests = await this.interestRepo.FindAllDtosByInterestGroupUidAsync(InterestGroup.Genre.Uid);
            ViewBag.TargetAudience = await this.targetAudienceRepo.FindAllByProjectTypeIdAsync(ProjectType.Audiovisual.Id);

            return View("Audiovisual/AudiovisualSubscriptions", searchViewModel);
        }

        /// <summary>Shows the audiovisual subscriptions widget.</summary>
        /// <param name="searchViewModel">The search view model.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> ShowAudiovisualSubscriptionsWidget(ReportsAudiovisualSearchViewModel searchViewModel)
        {
            var audiovisualProjectSubscriptionDtos = await this.projectRepo.FindAudiovisualSubscribedProjectsDtosByFilterAndByPageAsync(
                searchViewModel.Search,
                searchViewModel.InterestUids.ToListGuid(','),
                this.EditionDto.Id,
                searchViewModel.IsPitching,
                searchViewModel.TargetAudienceUids.ToListGuid(','),
                searchViewModel.StartDate,
                searchViewModel.EndDate,
                searchViewModel.Page ?? 1,
                searchViewModel.PageSize ?? 10);
            if (audiovisualProjectSubscriptionDtos == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.AudiovisualSubscriptionProjectReport, Labels.FoundF.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            ViewBag.SearchKeywords = searchViewModel.Search;
            ViewBag.InterestUid = searchViewModel.InterestUids;
            ViewBag.IsPitching = searchViewModel.IsPitching;
            ViewBag.TargetAudienceUid = searchViewModel.TargetAudienceUids;
            ViewBag.Page = searchViewModel.Page ?? 1;
            ViewBag.PageSize = searchViewModel.PageSize ?? 10;

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Audiovisual/Widgets/AudiovisualSubscriptionsWidget", audiovisualProjectSubscriptionDtos), divIdOrClass = "#ReportAudiovisualSubscriptionWidget" },
                }
            }, JsonRequestBehavior.AllowGet);

        }

        #endregion

        #region Export Excel

        /// <summary>Generates the excel document asynchronous.</summary>
        /// <param name="search">The search.</param>
        /// <param name="interestUids">The interest uids.</param>
        /// <param name="isPitching">if set to <c>true</c> [is pitching].</param>
        /// <param name="targetAudienceUids">The target audience uids.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> GenerateExcelDocumentAsync(
            string search,
            string interestUids,
            bool isPitching,
            string targetAudienceUids,
            DateTime? startDate,
            DateTime? endDate
            )
        {
            ExcelPackage excelFile = new ExcelPackage();

            ExcelWorksheet worksheetAudiovisual = excelFile.Workbook.Worksheets.Add(Labels.AudiovisualSubscriptionProjectReport);

            var audiovisualProjectSubscriptionDtos = await this.projectRepo.FindAudiovisualSubscribedProjectsDtosByFilterAsync(
                search,
                interestUids.ToListGuid(','),
                this.EditionDto.Id,
                isPitching,
                targetAudienceUids.ToListGuid(','),
                startDate,
                endDate);
            if (audiovisualProjectSubscriptionDtos == null)
            {
                return null;
            }

            int row = 1;
            int column = 1;

            // Config reader row
            worksheetAudiovisual.Cells[row, column, row, column + 21].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
            worksheetAudiovisual.Cells[row, column, row, column + 21].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            worksheetAudiovisual.Cells[row, column, row, column + 21].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            worksheetAudiovisual.Cells[row, column, row, column + 21].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            worksheetAudiovisual.Cells[row, column, row, column + 21].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            worksheetAudiovisual.Cells[row, column, row, column + 21].Style.Fill.PatternType = ExcelFillStyle.Solid;
            worksheetAudiovisual.Cells[row, column, row, column + 21].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(255, 255, 255));
            worksheetAudiovisual.Cells[row, column, row, column + 21].Style.Font.Bold = true;

            worksheetAudiovisual.Column(1).Width = 20;
            worksheetAudiovisual.Column(2).Width = 20;
            worksheetAudiovisual.Column(3).Width = 40;
            worksheetAudiovisual.Column(4).Width = 40;
            worksheetAudiovisual.Column(5).Width = 40;
            worksheetAudiovisual.Column(6).Width = 40;
            worksheetAudiovisual.Column(7).Width = 40;
            worksheetAudiovisual.Column(8).Width = 40;
            worksheetAudiovisual.Column(9).Width = 40;
            worksheetAudiovisual.Column(10).Width = 40;
            worksheetAudiovisual.Column(11).Width = 40;
            worksheetAudiovisual.Column(12).Width = 40;
            worksheetAudiovisual.Column(13).Width = 40;
            worksheetAudiovisual.Column(14).Width = 40;
            worksheetAudiovisual.Column(15).Width = 40;
            worksheetAudiovisual.Column(16).Width = 40;
            worksheetAudiovisual.Column(17).Width = 40;
            worksheetAudiovisual.Column(18).Width = 40;
            worksheetAudiovisual.Column(19).Width = 40;
            worksheetAudiovisual.Column(20).Width = 40;
            worksheetAudiovisual.Column(21).Width = 40;
            worksheetAudiovisual.Column(22).Width = 40;
            worksheetAudiovisual.Column(23).Width = 40;

            worksheetAudiovisual.Cells[row, column++].Value = Labels.ProducerQty;
            worksheetAudiovisual.Cells[row, column++].Value = Labels.ProjectsPerProducerQty;
            worksheetAudiovisual.Cells[row, column++].Value = Labels.ProjectId;
            worksheetAudiovisual.Cells[row, column++].Value = Labels.Producer;
            worksheetAudiovisual.Cells[row, column++].Value = Labels.Name;
            worksheetAudiovisual.Cells[row, column++].Value = Labels.BadgeName;
            worksheetAudiovisual.Cells[row, column++].Value = Labels.Email;
            worksheetAudiovisual.Cells[row, column++].Value = string.Format("{0} - {1}", Labels.Title, Labels.Portuguese);
            worksheetAudiovisual.Cells[row, column++].Value = string.Format("{0} - {1}", Labels.Title, Labels.English);
            worksheetAudiovisual.Cells[row, column++].Value = string.Format("{0}{1}", Labels.Pitching, "?");
            worksheetAudiovisual.Cells[row, column++].Value = Labels.PlayersSelectedForEvaluation;
            worksheetAudiovisual.Cells[row, column++].Value = Labels.CreateDate;
            worksheetAudiovisual.Cells[row, column++].Value = Labels.SendDate;
            worksheetAudiovisual.Cells[row, column++].Value = Labels.Platforms;
            worksheetAudiovisual.Cells[row, column++].Value = Labels.ProjectStatus;
            worksheetAudiovisual.Cells[row, column++].Value = Labels.MarketLookingFor;
            worksheetAudiovisual.Cells[row, column++].Value = Labels.Format;
            worksheetAudiovisual.Cells[row, column++].Value = Labels.Genre;
            worksheetAudiovisual.Cells[row, column++].Value = Labels.SubGenre;
            worksheetAudiovisual.Cells[row, column++].Value = Labels.TargetAudience;
            worksheetAudiovisual.Cells[row, column++].Value = string.Format("{0} - {1}", Labels.Summary, Labels.Portuguese);
            worksheetAudiovisual.Cells[row, column++].Value = string.Format("{0} - {1}", Labels.Summary, Labels.English);

            row++;

            var projectsCount = 0;
            Guid? lastSellerAttendeeOrganizationUid = null;
            var projectPerProducerCount = 0;

            foreach (var projectDto in audiovisualProjectSubscriptionDtos)
            {
                projectsCount++;
                projectPerProducerCount = (!lastSellerAttendeeOrganizationUid.HasValue || lastSellerAttendeeOrganizationUid != projectDto.SellerAttendeeOrganizationDto?.AttendeeOrganization?.Uid)
                                                            ? 1 : projectPerProducerCount + 1;
                lastSellerAttendeeOrganizationUid = projectDto.SellerAttendeeOrganizationDto?.AttendeeOrganization?.Uid;

                var sellerCollaborator = projectDto.SellerAttendeeOrganizationDto.AttendeeOrganization.AttendeeOrganizationCollaborators;

                column = 1;

                worksheetAudiovisual.Cells[row, column++].Value = projectsCount;
                worksheetAudiovisual.Cells[row, column++].Value = projectPerProducerCount;
                worksheetAudiovisual.Cells[row, column++].Value = projectDto.Project.Id;
                worksheetAudiovisual.Cells[row, column++].Value = projectDto.SellerAttendeeOrganizationDto?.Organization?.Name;

                var sellerName = string.Empty;
                var badje = string.Empty;
                var publicMail = string.Empty;
                var firstLine = true;
                foreach (var item in sellerCollaborator)
                {
                    sellerName += !firstLine ? string.Format("{0}{1}", ", ", item.AttendeeCollaborator.Collaborator.GetFullName()) : item.AttendeeCollaborator.Collaborator.GetFullName();
                    badje += !firstLine ? string.Format("{0}{1}", ", ", item.AttendeeCollaborator.Collaborator.Badge ?? string.Empty) : item.AttendeeCollaborator.Collaborator.Badge ?? string.Empty;
                    publicMail += !firstLine ? string.Format("{0}{1}", ", ", item.AttendeeCollaborator.Collaborator.PublicEmail ?? string.Empty) : item.AttendeeCollaborator.Collaborator.PublicEmail ?? string.Empty;
                    firstLine = false;
                };
                worksheetAudiovisual.Cells[row, column++].Value = sellerName;
                worksheetAudiovisual.Cells[row, column++].Value = badje;
                worksheetAudiovisual.Cells[row, column++].Value = publicMail;
                worksheetAudiovisual.Cells[row, column++].Value = projectDto.GetTitleDtoByLanguageCode(Language.Portuguese.Code)?.ProjectTitle?.Value;
                worksheetAudiovisual.Cells[row, column++].Value = projectDto.GetTitleDtoByLanguageCode(Language.English.Code)?.ProjectTitle?.Value;
                worksheetAudiovisual.Cells[row, column++].Value = projectDto.Project.IsPitching ? Labels.Yes : Labels.No;

                var playersSelectedForEvaluation = string.Empty;
                firstLine = true;
                foreach (var item in projectDto.ProjectBuyerEvaluationDtos)
                {
                    playersSelectedForEvaluation += string.Format("{0}{1}",
                        string.Format("{0}{1}", firstLine ? "" : " / ", item.ProjectBuyerEvaluation.BuyerAttendeeOrganization.Organization.Name),
                        string.Format("{0}{1}", " | ", item.ProjectBuyerEvaluation.ProjectEvaluationStatus.Name.GetSeparatorTranslation(ViewBag.UserInterfaceLanguage as string, '|'))
                        );
                    firstLine = false;
                };
                worksheetAudiovisual.Cells[row, column++].Value = playersSelectedForEvaluation;
                worksheetAudiovisual.Cells[row, column++].Value = projectDto.Project.CreateDate.ToUserTimeZone().ToShortDateString();
                worksheetAudiovisual.Cells[row, column++].Value = projectDto.Project.FinishDate?.ToUserTimeZone().ToShortDateString();

                firstLine = true;
                var platforms = string.Empty;
                var projectPlatformsDtos = projectDto.GetAllInterestsByInterestGroupUid(InterestGroup.Platforms.Uid);
                foreach (var interestDto in projectPlatformsDtos)
                {
                    platforms += string.Format("{0}{1}", firstLine ? "" : " | ", interestDto.Interest.Name.GetSeparatorTranslation(UserInterfaceLanguage, '|'));
                    firstLine = false;
                }
                worksheetAudiovisual.Cells[row, column++].Value = platforms;

                firstLine = true;
                var status = string.Empty;
                var projectStatusDtos = projectDto.GetAllInterestsByInterestGroupUid(InterestGroup.ProjectStatus.Uid);
                foreach (var interestDto in projectStatusDtos)
                {
                    status += string.Format("{0}{1}", firstLine ? "" : " | ", interestDto.Interest.Name.GetSeparatorTranslation(UserInterfaceLanguage as string, '|'));
                    firstLine = false;
                }
                worksheetAudiovisual.Cells[row, column++].Value = status;

                firstLine = true;
                var lookingFor = string.Empty;
                var projectLookingForDtos = projectDto.GetAllInterestsByInterestGroupUid(InterestGroup.LookingFor.Uid);
                foreach (var interestDto in projectLookingForDtos)
                {
                    lookingFor += string.Format("{0}{1}", firstLine ? "" : " | ", interestDto.Interest.Name.GetSeparatorTranslation(UserInterfaceLanguage as string, '|'));
                    firstLine = false;
                }
                worksheetAudiovisual.Cells[row, column++].Value = lookingFor;

                firstLine = true;
                var format = string.Empty;
                var projectFormatDtos = projectDto.GetAllInterestsByInterestGroupUid(InterestGroup.Format.Uid);
                foreach (var interestDto in projectFormatDtos)
                {
                    format += string.Format("{0}{1}", firstLine ? "" : " | ", interestDto.Interest.Name.GetSeparatorTranslation(UserInterfaceLanguage as string, '|'));
                    firstLine = false;
                }
                worksheetAudiovisual.Cells[row, column++].Value = format;

                firstLine = true;
                var genre = string.Empty;
                var projectGenreDtos = projectDto.GetAllInterestsByInterestGroupUid(InterestGroup.Genre.Uid);
                foreach (var interestDto in projectGenreDtos)
                {
                    genre += string.Format("{0}{1}", firstLine ? "" : " | ", interestDto.Interest.Name.GetSeparatorTranslation(UserInterfaceLanguage as string, '|'));
                    firstLine = false;
                }
                worksheetAudiovisual.Cells[row, column++].Value = genre;

                firstLine = true;
                var subgenre = string.Empty;
                var projectSubgenreDtos = projectDto.GetAllInterestsByInterestGroupUid(InterestGroup.SubGenre.Uid);
                foreach (var interestDto in projectSubgenreDtos)
                {
                    subgenre += string.Format("{0}{1}", firstLine ? "" : " | ", interestDto.Interest.Name.GetSeparatorTranslation(UserInterfaceLanguage as string, '|'));
                    firstLine = false;
                }
                worksheetAudiovisual.Cells[row, column++].Value = subgenre;

                firstLine = true;
                var targetAudience = string.Empty;
                foreach (var item in projectDto.ProjectTargetAudienceDtos)
                {
                    targetAudience += string.Format("{0}{1}", firstLine ? "" : ", ", item.TargetAudience.Name);
                    firstLine = false;
                }
                worksheetAudiovisual.Cells[row, column++].Value = targetAudience;
                worksheetAudiovisual.Cells[row, column++].Value = projectDto.GetSummaryDtoByLanguageCode(Language.Portuguese.Code).ProjectSummary.Value;
                worksheetAudiovisual.Cells[row, column++].Value = projectDto.GetSummaryDtoByLanguageCode(Language.English.Code).ProjectSummary.Value;

                row++;
            }

            using (excelFile)
            {
                var stream = new MemoryStream();
                excelFile.SaveAs(stream);

                string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                string fileName = string.Format("{0} - {1}", Labels.AudiovisualSubscriptionProjectReport, DateTime.UtcNow.ToUserTimeZone().ToString("yyyyMMdd_HHmmss")) + ".xlsx";

                stream.Position = 0;

                return File(stream, contentType, fileName);
            }
        }

        #endregion

        #endregion
    }
}