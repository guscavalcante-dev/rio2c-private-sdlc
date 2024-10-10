// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 12-12-2019
//
// Last Modified By : Gilson Oliveira
// Last Modified On : 10-03-2024
// ***********************************************************************
// <copyright file="SpeakersController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Application.ViewModels;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using DataTables.AspNet.Core;
using DataTables.AspNet.Mvc5;
using MediatR;
using PlataformaRio2c.Infra.Data.FileRepository;
using PlataformaRio2C.Application;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Application.CQRS.Queries;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Statics;
using PlataformaRio2C.Infra.CrossCutting.Identity.AuthorizeAttributes;
using PlataformaRio2C.Infra.CrossCutting.Identity.Service;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions;
using PlataformaRio2C.Infra.CrossCutting.Tools.Helpers;
using PlataformaRio2C.Web.Admin.Filters;
using Constants = PlataformaRio2C.Domain.Constants;
using PlataformaRio2C.Infra.CrossCutting.Tools.CustomActionResults;
using PlataformaRio2C.Domain.ApiModels;
using ClosedXML.Excel;
using System.IO;
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Web.Admin.Controllers
{
    /// <summary>SpeakersController</summary>
    [AjaxAuthorize(Order = 1, Roles = Constants.Role.AnyAdmin)]
    [AuthorizeCollaboratorType(Order = 2, Types = Constants.CollaboratorType.SpeakersReadString)]
    public class SpeakersController : BaseController
    {
        private readonly ICollaboratorRepository collaboratorRepo;
        private readonly IAttendeeCollaboratorRepository attendeeCollaboratorRepo;
        private readonly IFileRepository fileRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="SpeakersController" /> class.
        /// </summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="identityController">The identity controller.</param>
        /// <param name="collaboratorRepository">The collaborator repository.</param>
        /// <param name="attendeeCollaboratorRepository">The attendee collaborator repository.</param>
        /// <param name="fileRepository">The file repository.</param>
        public SpeakersController(
            IMediator commandBus,
            IdentityAutenticationService identityController,
            ICollaboratorRepository collaboratorRepository,
            IAttendeeCollaboratorRepository attendeeCollaboratorRepository,
            IFileRepository fileRepository)
            : base(commandBus, identityController)
        {
            this.collaboratorRepo = collaboratorRepository;
            this.attendeeCollaboratorRepo = attendeeCollaboratorRepository;
            this.fileRepo = fileRepository;
        }

        #region List

        /// <summary>Indexes the specified search view model.</summary>
        /// <param name="searchViewModel">The search view model.</param>
        /// <returns></returns>
        [HttpGet]
        [AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.SpeakersWriteString)]
        public ActionResult Index(SpeakerSearchViewModel searchViewModel)
        {
            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.Speakers, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Labels.Speakers, Url.Action("Index", "Speakers", new { Area = "" }))
            });

            #endregion

            return View(searchViewModel);
        }

        #region DataTable Widget

        /// <summary>Searches the specified request.</summary>
        /// <param name="request">The request.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <param name="showAllParticipants">if set to <c>true</c> [show all participants].</param>
        /// <param name="showHighlights">if set to <c>true</c> [show highlights].</param>
        /// <returns></returns>
        [HttpGet]
        [AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.SpeakersWriteString)]
        public async Task<ActionResult> Search(IDataTablesRequest request, bool showAllEditions, bool showAllParticipants, bool? showHighlights)
        {
            var speakers = await this.collaboratorRepo.FindAllSpeakersByDataTable(
                request.Start / request.Length,
                request.Length,
                request.Search?.Value,
                request.GetSortColumns(),
                showAllEditions,
                showAllParticipants,
                showHighlights,
                this.EditionDto?.Id,
                false);

            foreach(var speaker in speakers)
            {
                var collaborator = new Collaborator(
                    speaker.FirstName,
                    speaker.LastNames,
                    speaker.Badge,
                    speaker.ImageUploadDate,
                    speaker.JobTitleBaseDtos,
                    speaker.MiniBioBaseDtos,
                    speaker.EditionAttendeeCollaboratorBaseDto
                );
                collaborator.FillRequiredFieldsToPublishToApi();
                speaker.RequiredFieldsToPublish = collaborator.RequiredFieldsToPublish;
                speaker.JobTitleBaseDtos = Enumerable.Empty<CollaboratorJobTitleBaseDto>();
                speaker.MiniBioBaseDtos = Enumerable.Empty<CollaboratorMiniBioBaseDto>();
            }

            var response = DataTablesResponse.Create(request, speakers.TotalItemCount, speakers.TotalItemCount, speakers);

            return Json(new
            {
                status = "success",
                dataTable = response
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Exports to excel.
        /// </summary>
        /// <param name="searchKeywords">The search keywords.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <param name="showAllParticipants">if set to <c>true</c> [show all participants].</param>
        /// <param name="showHighlights">The show highlights.</param>
        /// <returns></returns>
        [HttpGet]
        [AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.SpeakersWriteString)]
        public async Task<ActionResult> ExportToExcel(string searchKeywords, bool showAllEditions, bool showAllParticipants, bool? showHighlights)
        {
            string fileName = Labels.SpeakersReport + "_" + DateTime.UtcNow.ToStringFileNameTimestamp();
            string filePath = Path.Combine(Path.GetTempPath(), fileName + ".xlsx");

            try
            {
                var speakers = await this.collaboratorRepo.FindAllSpeakersByDataTable(
                     1,
                     10000,
                     searchKeywords,
                     new List<Tuple<string, string>>(), //request.GetSortColumns(),
                     showAllEditions,
                     showAllParticipants,
                     showHighlights,
                     this.EditionDto?.Id,
                     true);

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add(Labels.Contacts);

                    #region Header

                    var lineIndex = 1;
                    var columnIndex = 0;
                    var skipFinalAdjustmentsColumnIndexes = new List<int>();

                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.Id;
                    worksheet.Column(columnIndex).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    skipFinalAdjustmentsColumnIndexes.Add(columnIndex);

                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.Name;
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.BadgeName;
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.Email;

                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.CellPhone;
                    worksheet.Column(columnIndex).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    skipFinalAdjustmentsColumnIndexes.Add(columnIndex);

                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.PhoneNumber;
                    worksheet.Column(columnIndex).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    skipFinalAdjustmentsColumnIndexes.Add(columnIndex);

                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.JobTitle;
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.Conferences;
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.MiniBio;
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.Photo500x500;
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.PhotoOriginal;
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.Website;
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.LinkedIn;
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.Instagram;
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.YouTube;

                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.Company + " - " + Labels.CompanyName;
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.Company + " - " + Labels.TradeName;
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.Company + " - " + Labels.CompanyResume;
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.Company + " - " + Labels.Logo500x500;
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.Company + " - " + Labels.LogoOriginal;
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.Company + " - " + Labels.Website;
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.Company + " - " + Labels.LinkedIn;
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.Company + " - " + Labels.Instagram;
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.Company + " - " + Labels.Twitter;
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.Company + " - " + Labels.YouTube;

                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.TermsAcceptanceDate;
                    worksheet.Column(columnIndex).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    skipFinalAdjustmentsColumnIndexes.Add(columnIndex);

                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.OnboardingFinishDate;
                    worksheet.Column(columnIndex).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    skipFinalAdjustmentsColumnIndexes.Add(columnIndex);

                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.ShowOnWebsite;
                    worksheet.Column(columnIndex).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    skipFinalAdjustmentsColumnIndexes.Add(columnIndex);

                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.HighlightPosition;
                    worksheet.Column(columnIndex).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    skipFinalAdjustmentsColumnIndexes.Add(columnIndex);

                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.Creator;
                    worksheet.Column(columnIndex).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    skipFinalAdjustmentsColumnIndexes.Add(columnIndex);

                    #endregion

                    if (speakers.Any())
                    {
                        #region Rows

                        foreach (var collaboratorDto in speakers)
                        {
                            lineIndex++;
                            columnIndex = 0;

                            worksheet.Cell(lineIndex, columnIndex += 1).Value = collaboratorDto.Id;
                            worksheet.Cell(lineIndex, columnIndex).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                            worksheet.Cell(lineIndex, columnIndex += 1).Value = collaboratorDto.FullName ?? "-";
                            worksheet.Cell(lineIndex, columnIndex += 1).Value = collaboratorDto.Badge ?? "-";
                            worksheet.Cell(lineIndex, columnIndex += 1).Value = collaboratorDto.Email ?? "-";

                            worksheet.Cell(lineIndex, columnIndex += 1).Value = collaboratorDto.CellPhone ?? "-";
                            worksheet.Cell(lineIndex, columnIndex).Style.NumberFormat.Format = "00000";
                            worksheet.Cell(lineIndex, columnIndex).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                            worksheet.Cell(lineIndex, columnIndex += 1).Value = collaboratorDto.PhoneNumber ?? "-";
                            worksheet.Cell(lineIndex, columnIndex).Style.NumberFormat.Format = "00000";
                            worksheet.Cell(lineIndex, columnIndex).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                            worksheet.Cell(lineIndex, columnIndex += 1).Value = collaboratorDto.GetCollaboratorJobTitleBaseDtoByLanguageCode(this.UserInterfaceLanguage)?.Value ?? "-";
                            worksheet.Cell(lineIndex, columnIndex += 1).Value = collaboratorDto.GetConferencesTitlesWithRoomAndDateString(this.UserInterfaceLanguage) ?? "-";
                            worksheet.Cell(lineIndex, columnIndex += 1).Value = collaboratorDto.GetMiniBioBaseDtoByLanguageCode(this.UserInterfaceLanguage)?.Value ?? "-";

                            worksheet.Cell(lineIndex, columnIndex += 1).Value = collaboratorDto.ImageUploadDate.HasValue ?
                                this.fileRepo.GetImageUrl(FileRepositoryPathType.UserImage, collaboratorDto.Uid, collaboratorDto.ImageUploadDate, true, "_500x500") : "-";

                            worksheet.Cell(lineIndex, columnIndex += 1).Value = collaboratorDto.ImageUploadDate.HasValue ?
                                this.fileRepo.GetImageUrl(FileRepositoryPathType.UserImage, collaboratorDto.Uid, collaboratorDto.ImageUploadDate, true) : "-";

                            worksheet.Cell(lineIndex, columnIndex += 1).Value = collaboratorDto.Website ?? "-";
                            worksheet.Cell(lineIndex, columnIndex += 1).Value = collaboratorDto.Linkedin ?? "-";
                            worksheet.Cell(lineIndex, columnIndex += 1).Value = collaboratorDto.Instagram ?? "-";
                            worksheet.Cell(lineIndex, columnIndex += 1).Value = collaboratorDto.Youtube ?? "-";

                            #region Organization

                            worksheet.Cell(lineIndex, columnIndex += 1).Value = collaboratorDto.AttendeeOrganizationBasesDtos.Select(ao => ao.OrganizationBaseDto.Name ?? "-")?.ToString(", ");
                            worksheet.Cell(lineIndex, columnIndex += 1).Value = collaboratorDto.AttendeeOrganizationBasesDtos.Select(ao => ao.OrganizationBaseDto.TradeName ?? "-")?.ToString(", ");
                            worksheet.Cell(lineIndex, columnIndex += 1).Value = collaboratorDto.AttendeeOrganizationBasesDtos.Select(ao => ao.OrganizationBaseDto.GetOrganizationDescriptionBaseDtoByLanguageCode(this.UserInterfaceLanguage)?.Value ?? "-")?.ToString(", ");

                            worksheet.Cell(lineIndex, columnIndex += 1).Value = collaboratorDto.AttendeeOrganizationBasesDtos.Select(ao => ao.OrganizationBaseDto.ImageUploadDate.HasValue ?
                                this.fileRepo.GetImageUrl(FileRepositoryPathType.OrganizationImage, ao.OrganizationBaseDto.Uid, ao.OrganizationBaseDto.ImageUploadDate, true, "_500x500") : "-")?.ToString(", ");

                            worksheet.Cell(lineIndex, columnIndex += 1).Value = collaboratorDto.AttendeeOrganizationBasesDtos.Select(ao => ao.OrganizationBaseDto.ImageUploadDate.HasValue ?
                                this.fileRepo.GetImageUrl(FileRepositoryPathType.OrganizationImage, ao.OrganizationBaseDto.Uid, ao.OrganizationBaseDto.ImageUploadDate, true) : "-")?.ToString(", ");

                            worksheet.Cell(lineIndex, columnIndex += 1).Value = collaboratorDto.AttendeeOrganizationBasesDtos.Select(ao => ao.OrganizationBaseDto.Website ?? "-")?.ToString(", ");
                            worksheet.Cell(lineIndex, columnIndex += 1).Value = collaboratorDto.AttendeeOrganizationBasesDtos.Select(ao => ao.OrganizationBaseDto.Linkedin ?? "-")?.ToString(", ");
                            worksheet.Cell(lineIndex, columnIndex += 1).Value = collaboratorDto.AttendeeOrganizationBasesDtos.Select(ao => ao.OrganizationBaseDto.Instagram ?? "-")?.ToString(", ");
                            worksheet.Cell(lineIndex, columnIndex += 1).Value = collaboratorDto.AttendeeOrganizationBasesDtos.Select(ao => ao.OrganizationBaseDto.Twitter ?? "-")?.ToString(", ");
                            worksheet.Cell(lineIndex, columnIndex += 1).Value = collaboratorDto.AttendeeOrganizationBasesDtos.Select(ao => ao.OrganizationBaseDto.Youtube ?? "-")?.ToString(", ");

                            #endregion

                            worksheet.Cell(lineIndex, columnIndex += 1).Value = collaboratorDto?.EditionAttendeeCollaboratorBaseDto?.SpeakerTermsAcceptanceDate?.ToStringHourMinute() ?? "-";
                            worksheet.Cell(lineIndex, columnIndex).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                            worksheet.Cell(lineIndex, columnIndex += 1).Value = collaboratorDto?.EditionAttendeeCollaboratorBaseDto?.OnboardingFinishDate?.ToStringHourMinute() ?? "-";
                            worksheet.Cell(lineIndex, columnIndex).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                            worksheet.Cell(lineIndex, columnIndex += 1).Value = collaboratorDto?.EditionAttendeeCollaboratorBaseDto?.AttendeeCollaboratorTypeDto?.IsApiDisplayEnabled.ToYesOrNoString();
                            worksheet.Cell(lineIndex, columnIndex).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                            worksheet.Cell(lineIndex, columnIndex += 1).Value = collaboratorDto?.EditionAttendeeCollaboratorBaseDto?.AttendeeCollaboratorTypeDto?.ApiHighlightPosition?.ToString() ?? "-";
                            worksheet.Cell(lineIndex, columnIndex).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                            worksheet.Cell(lineIndex, columnIndex += 1).Value = collaboratorDto?.CreatorBaseDto?.Email ?? "-";
                            worksheet.Cell(lineIndex, columnIndex).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
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

        #endregion

        #endregion

        #region Details

        /// <summary>Detailses the specified identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet]
        [AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.SpeakersWriteString)]
        public async Task<ActionResult> Details(Guid? id)
        {
            var attendeeCollaboratorDto = await this.attendeeCollaboratorRepo.FindSiteDetailstDtoByCollaboratorUidAndByEditionIdAsync(id ?? Guid.Empty, this.EditionDto.Id);
            if (attendeeCollaboratorDto == null)
            {
                this.StatusMessageToastr(string.Format(Messages.EntityNotAction, Labels.Speaker, Labels.FoundM.ToLowerInvariant()), Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);
                return RedirectToAction("Index", "Home", new { Area = "" });
            }

            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.Speakers, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Labels.Speakers, Url.Action("Index", "Speakers", new { id })),
                new BreadcrumbItemHelper(attendeeCollaboratorDto.Collaborator.GetFullName(), Url.Action("Details", "Speakers", new { id }))
            });

            #endregion

            return View(attendeeCollaboratorDto);
        }

        #region Participants Widget

        /// <summary>Shows the participants widget.</summary>
        /// <param name="collaboratorUid">The collaborator uid.</param>
        /// <returns></returns>
        [HttpGet]
        [AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.SpeakersReadString)]
        public async Task<ActionResult> ShowParticipantsWidget(Guid? collaboratorUid)
        {
            var participantsWidgetDto = await this.attendeeCollaboratorRepo.FindParticipantsWidgetDtoAsync(collaboratorUid ?? Guid.Empty, this.EditionDto.Id);
            if (participantsWidgetDto == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Speaker, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/RelatedParticipantsWidget", participantsWidgetDto), divIdOrClass = "#SpeakerParticipantsWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Conferences Widget

        /// <summary>Shows the conferences widget.</summary>
        /// <param name="collaboratorUid">The collaborator uid.</param>
        /// <returns></returns>
        [HttpGet]
        [AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.SpeakersReadString)]
        public async Task<ActionResult> ShowConferencesWidget(Guid? collaboratorUid)
        {
            var conferencesWidgetDto = await this.attendeeCollaboratorRepo.FindConferenceWidgetDtoAsync(collaboratorUid ?? Guid.Empty, this.EditionDto.Id);
            if (conferencesWidgetDto == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Speaker, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("~/Views/Conferences/Widgets/RelatedConferencesWidget.cshtml", conferencesWidgetDto.ConferenceDtos), divIdOrClass = "#SpeakerConferencesWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Api Configuration Widget

        /// <summary>Shows the API configuration widget.</summary>
        /// <param name="collaboratorUid">The collaborator uid.</param>
        /// <returns></returns>
        [HttpGet]
        [AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.SpeakersWriteString)]
        public async Task<ActionResult> ShowApiConfigurationWidget(Guid? collaboratorUid)
        {
            var apiConfigurationWidgetDto = await this.attendeeCollaboratorRepo.FindApiConfigurationWidgetDtoByCollaboratorUidAndByEditionIdAsync(collaboratorUid ?? Guid.Empty, this.EditionDto.Id);
            if (apiConfigurationWidgetDto == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Speaker, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            apiConfigurationWidgetDto.Collaborator.FillRequiredFieldsToPublishToApi();

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/ApiConfigurationWidget", apiConfigurationWidgetDto), divIdOrClass = "#SpeakerApiConfigurationWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #region Update

        /// <summary>Shows the update API configuration modal.</summary>
        /// <param name="collaboratorUid">The collaborator uid.</param>
        /// <returns></returns>
        [HttpGet]
        [AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.SpeakersWriteString)]
        public async Task<ActionResult> ShowUpdateApiConfigurationModal(Guid? collaboratorUid)
        {
            UpdateCollaboratorApiConfiguration cmd;

            try
            {
                var apiConfigurationWidgetDto = await this.attendeeCollaboratorRepo.FindApiConfigurationWidgetDtoByCollaboratorUidAndByEditionIdAsync(collaboratorUid ?? Guid.Empty, this.EditionDto.Id);
                if (apiConfigurationWidgetDto == null)
                {
                    throw new DomainException(string.Format(Messages.EntityNotAction, Labels.Speaker, Labels.FoundM.ToLowerInvariant()));
                }

                cmd = new UpdateCollaboratorApiConfiguration(
                    apiConfigurationWidgetDto,
                    Constants.CollaboratorType.Speaker,
                    await this.attendeeCollaboratorRepo.FindAllApiConfigurationWidgetDtoByHighlight(this.EditionDto.Id, Constants.CollaboratorType.Speaker),
                    this.EditionDto.SpeakersApiHighlightPositionsCount);

                if (!apiConfigurationWidgetDto.Collaborator.IsAbleToPublishToApi)
                {
                    throw new DomainException(Messages.PendingFieldsToPublish);
                }
            }
            catch (DomainException ex)
            {
                string message = null;
                if (ex.Message == Messages.PendingFieldsToPublish)
                {
                    message = Messages.PendingFieldsToPublish;
                }
                return Json(new { status = "error", message = message ?? ex.GetInnerMessage() }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Modals/UpdateApiConfigurationModal", cmd), divIdOrClass = "#GlobalModalContainer" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>Updates the API configuration.</summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.SpeakersWriteString)]
        public async Task<ActionResult> UpdateApiConfiguration(UpdateCollaboratorApiConfiguration cmd)
        {
            var result = new AppValidationResult();

            try
            {
                if (!ModelState.IsValid)
                {
                    throw new DomainException(Messages.CorrectFormValues);
                }

                cmd.UpdatePreSendProperties(
                    Constants.CollaboratorType.Speaker,
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

                cmd.UpdateBaseModels(
                    await this.attendeeCollaboratorRepo.FindAllApiConfigurationWidgetDtoByHighlight(this.EditionDto.Id, Constants.CollaboratorType.Speaker));
                cmd.GenerateCountSpeakersApiHighlightPositions(this.EditionDto.SpeakersApiHighlightPositionsCount);

                return Json(new
                {
                    status = "error",
                    message = result.Errors?.FirstOrDefault(e => e.Target == "ToastrError")?.Message ?? ex.GetInnerMessage(),
                    pages = new List<dynamic>
                    {
                        new { page = this.RenderRazorViewToString("Modals/UpdateApiConfigurationForm", cmd), divIdOrClass = "#form-container" },
                    }
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return Json(new { status = "error", message = Messages.WeFoundAndError, }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Speaker, Labels.UpdatedM) });
        }

        #endregion

        #endregion

        #endregion

        #region Send Invitation Emails

        /// <summary>Sends the invitation emails.</summary>
        /// <param name="selectedCollaboratorsUids">The selected collaborators uids.</param>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.SpeakersWriteString)]
        public async Task<ActionResult> SendInvitationEmails(string selectedCollaboratorsUids)
        {
            AppValidationResult result = null;

            try
            {
                if (string.IsNullOrEmpty(selectedCollaboratorsUids))
                {
                    throw new DomainException(Messages.SelectAtLeastOneOption);
                }

                var collaboratorsUids = selectedCollaboratorsUids?.ToListGuid(',');
                if (!collaboratorsUids.Any())
                {
                    throw new DomainException(Messages.SelectAtLeastOneOption);
                }

                var collaboratorsDtos = await this.collaboratorRepo.FindAllCollaboratorsByCollaboratorsUids(this.EditionDto.Id, collaboratorsUids);
                if (collaboratorsDtos?.Any() != true)
                {
                    throw new DomainException(Messages.SelectAtLeastOneOption);
                }

                List<string> errors = new List<string>();
                foreach (var collaboratorDto in collaboratorsDtos)
                {
                    var collaboratorLanguageCode = collaboratorDto.Language?.Code ?? this.UserInterfaceLanguage;

                    try
                    {
                        result = await this.CommandBus.Send(new SendSpeakerWelcomeEmailAsync(
                            collaboratorDto.Collaborator.Uid,
                            collaboratorDto.User.SecurityStamp,
                            collaboratorDto.User.Id,
                            collaboratorDto.User.Uid,
                            collaboratorDto.GetFirstName(),
                            collaboratorDto.GetFullName(collaboratorLanguageCode),
                            collaboratorDto.User.Email,
                            this.EditionDto.Edition,
                            this.AdminAccessControlDto.User.Id,
                            collaboratorLanguageCode));
                        if (!result.IsValid)
                        {
                            throw new DomainException(Messages.CorrectFormValues);
                        }
                    }
                    catch (DomainException)
                    {
                        //Cannot stop sending email when exception occurs.
                        errors.AddRange(result.Errors.Select(e => e.Message));
                    }
                    catch (Exception ex)
                    {
                        Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                    }
                }

                if (errors.Any())
                {
                    throw new DomainException(string.Format(Messages.OneOrMoreEmailsNotSend, Labels.WelcomeEmail));
                }
            }
            catch (DomainException ex)
            {
                return Json(new { status = "error", message = ex.GetInnerMessage(), }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return Json(new { status = "error", message = Messages.WeFoundAndError, }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Email.ToLowerInvariant(), Labels.Sent.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Total Count Widget

        /// <summary>Shows the total count widget.</summary>
        /// <returns></returns>
        [HttpGet]
        [AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.SpeakersReadString)]
        public async Task<ActionResult> ShowTotalCountWidget()
        {
            var executivesCount = await this.collaboratorRepo.CountAllByDataTable(
                Constants.CollaboratorType.Speaker,
                null,
                true,
                this.EditionDto.Id);

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/TotalCountWidget", executivesCount), divIdOrClass = "#SpeakersTotalCountWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Edition Count Widget

        /// <summary>Shows the edition count widget.</summary>
        /// <returns></returns>
        [HttpGet]
        [AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.SpeakersReadString)]
        public async Task<ActionResult> ShowEditionCountWidget()
        {
            var executivesCount = await this.collaboratorRepo.CountAllByDataTable(
                Constants.CollaboratorType.Speaker,
                null,
                false,
                this.EditionDto.Id);

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/EditionCountWidget", executivesCount), divIdOrClass = "#SpeakersEditionCountWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Create

        /// <summary>Shows the create modal.</summary>
        /// <returns></returns>
        [HttpGet]
        [AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.SpeakersWriteString)]
        public async Task<ActionResult> ShowCreateModal()
        {
            var cmd = new CreateTinyCollaborator();

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Modals/CreateModal", cmd), divIdOrClass = "#GlobalModalContainer" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>Creates the specified collaborator.</summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.SpeakersWriteString)]
        public async Task<ActionResult> Create(CreateTinyCollaborator cmd)
        {
            var result = new AppValidationResult();

            try
            {
                if (!ModelState.IsValid)
                {
                    throw new DomainException(Messages.CorrectFormValues);
                }

                cmd.UpdatePreSendProperties(
                    Constants.CollaboratorType.Speaker,
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
                    pages = new List<dynamic>
                    {
                        new { page = this.RenderRazorViewToString("/Views/Collaborators/Forms/_TinyForm.cshtml", cmd), divIdOrClass = "#form-container" },
                    }
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return Json(new { status = "error", message = Messages.WeFoundAndError, }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Speaker, Labels.CreatedM) });
        }

        #endregion

        #region Update

        /// <summary>Shows the update modal.</summary>
        /// <param name="collaboratorUid">The collaborator uid.</param>
        /// <param name="isAddingToCurrentEdition">The is adding to current edition.</param>
        /// <returns></returns>
        [HttpGet]
        [AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.SpeakersWriteString)]
        public async Task<ActionResult> ShowUpdateModal(Guid? collaboratorUid, bool? isAddingToCurrentEdition)
        {
            UpdateTinyCollaborator cmd;

            try
            {
                cmd = new UpdateTinyCollaborator(
                    await this.CommandBus.Send(new FindCollaboratorDtoByUidAndByEditionIdAsync(collaboratorUid, this.EditionDto.Id, this.UserInterfaceLanguage)),
                    isAddingToCurrentEdition);
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
                    new { page = this.RenderRazorViewToString("Modals/UpdateModal", cmd), divIdOrClass = "#GlobalModalContainer" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>Updates the specified tiny collaborator.</summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.SpeakersWriteString)]
        public async Task<ActionResult> Update(UpdateTinyCollaborator cmd)
        {
            var result = new AppValidationResult();

            try
            {
                if (!ModelState.IsValid)
                {
                    throw new DomainException(Messages.CorrectFormValues);
                }

                cmd.UpdatePreSendProperties(
                    Constants.CollaboratorType.Speaker,
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
                    pages = new List<dynamic>
                    {
                        new { page = this.RenderRazorViewToString("/Views/Collaborators/Forms/_TinyForm.cshtml", cmd), divIdOrClass = "#form-container" },
                    }
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return Json(new { status = "error", message = Messages.WeFoundAndError, }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Speaker, Labels.UpdatedM) });
        }

        #endregion

        #region Delete

        /// <summary>Deletes the specified collaborator.</summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.SpeakersWriteString)]
        public async Task<ActionResult> Delete(DeleteCollaborator cmd)
        {
            var result = new AppValidationResult();

            try
            {
                if (!ModelState.IsValid)
                {
                    throw new DomainException(Messages.CorrectFormValues);
                }

                cmd.UpdatePreSendProperties(
                    Constants.CollaboratorType.Speaker,
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

            return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Speaker, Labels.DeletedM) });
        }

        #endregion

        #region Finds

        /// <summary>Finds all by filters.</summary>
        /// <param name="keywords">The keywords.</param>
        /// <param name="page">The page.</param>
        /// <returns></returns>
        [HttpGet]
        [AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.SpeakersReadString)]
        public async Task<ActionResult> FindAllByFilters(string keywords, int? page = 1)
        {
            var collaboratorsApiDtos = await this.collaboratorRepo.FindAllSpeakersApiListDtoPaged(
                this.EditionDto.Id,
                keywords,
                false,
                Constants.CollaboratorType.Speaker,
                false,
                page.Value,
                10);

            return Json(new
            {
                status = "success",
                HasPreviousPage = collaboratorsApiDtos.HasPreviousPage,
                HasNextPage = collaboratorsApiDtos.HasNextPage,
                TotalItemCount = collaboratorsApiDtos.TotalItemCount,
                PageCount = collaboratorsApiDtos.PageCount,
                PageNumber = collaboratorsApiDtos.PageNumber,
                PageSize = collaboratorsApiDtos.PageSize,
                Collaborators = collaboratorsApiDtos?.Select(c => new CollaboratorsDropdownDto
                {
                    Uid = c.Uid,
                    BadgeName = c.BadgeName?.Trim(),
                    Name = c.Name?.Trim(),
                    Picture = c.ImageUploadDate.HasValue ? this.fileRepo.GetImageUrl(FileRepositoryPathType.UserImage, c.Uid, c.ImageUploadDate, true) : null
                })?.ToList()
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}