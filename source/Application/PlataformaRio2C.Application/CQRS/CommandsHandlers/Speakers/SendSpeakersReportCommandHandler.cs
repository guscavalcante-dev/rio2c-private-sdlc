// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 01-27-2023
//
// Last Modified By : Renan Valentim
// Last Modified On : 01-11-2024
// ***********************************************************************
// <copyright file="SendSpeakersReportCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using ClosedXML.Excel;
using MediatR;
using PlataformaRio2c.Infra.Data.FileRepository;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Application.Interfaces;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Statics;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using PlataformaRio2C.Infra.Data.Context.Interfaces;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>SendSpeakersReportCommandHandler</summary>
    public class SendSpeakersReportCommandHandler : BaseCommandHandler, IRequestHandler<SendSpeakersReport, AppValidationResult>
    {
        private readonly IEditionRepository editionRepo;
        private readonly IFileRepository fileRepo;
        private readonly IMailerService mailerService;
        private readonly ICollaboratorRepository collaboratorRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="SendSpeakersReportCommandHandler"/> class.
        /// </summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="editionRepository">The edition repository.</param>
        /// <param name="fileRepository">The file repository.</param>
        /// <param name="mailerService">The mailer service.</param>
        /// <param name="collaboratorRepository">The collaborator repository.</param>
        public SendSpeakersReportCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            IEditionRepository editionRepository,
            IFileRepository fileRepository,
            IMailerService mailerService,
            ICollaboratorRepository collaboratorRepository)
            : base(eventBus, uow)
        {
            this.editionRepo = editionRepository;
            this.fileRepo = fileRepository;
            this.mailerService = mailerService;
            this.collaboratorRepo = collaboratorRepository;
        }

        /// <summary>Handles the specified delete music project.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(SendSpeakersReport cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var collaboratorDtos = await this.collaboratorRepo.FindAllSpeakersByDataTable(
                1,
                10000,
                "",   
                null, 
                false,
                false,
                false,
                cmd.EditionId ?? 0,
                false,
                null,
                exportToExcel:true);

            #region Initial validations

            if (!this.ValidationResult.IsValid)
            {
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            #endregion

            string filePath = Path.Combine(Path.GetTempPath(), Labels.SpeakersReport + "_" + DateTime.UtcNow.ToStringFileNameTimestamp() + ".xlsx");

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add(Labels.Contacts);

                #region Header

                var lineIndex = 1;
                var columnIndex = 0;
                var skipFinalAdjustmentsColumnIndexes = new List<int>();

                worksheet.Cell(lineIndex, columnIndex = columnIndex + 1).Value = Labels.Id;
                worksheet.Column(columnIndex).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                skipFinalAdjustmentsColumnIndexes.Add(columnIndex);

                worksheet.Cell(lineIndex, columnIndex = columnIndex + 1).Value = Labels.Name;
                worksheet.Cell(lineIndex, columnIndex = columnIndex + 1).Value = Labels.BadgeName;
                worksheet.Cell(lineIndex, columnIndex = columnIndex + 1).Value = Labels.Email;

                worksheet.Cell(lineIndex, columnIndex = columnIndex + 1).Value = Labels.CellPhone;
                worksheet.Column(columnIndex).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                skipFinalAdjustmentsColumnIndexes.Add(columnIndex);

                worksheet.Cell(lineIndex, columnIndex = columnIndex + 1).Value = Labels.PhoneNumber;
                worksheet.Column(columnIndex).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                skipFinalAdjustmentsColumnIndexes.Add(columnIndex);

                worksheet.Cell(lineIndex, columnIndex = columnIndex + 1).Value = Labels.JobTitle;
                worksheet.Cell(lineIndex, columnIndex = columnIndex + 1).Value = Labels.MiniBio;
                worksheet.Cell(lineIndex, columnIndex = columnIndex + 1).Value = Labels.Photo500x500;
                worksheet.Cell(lineIndex, columnIndex = columnIndex + 1).Value = Labels.PhotoOriginal;
                worksheet.Cell(lineIndex, columnIndex = columnIndex + 1).Value = Labels.Website;
                worksheet.Cell(lineIndex, columnIndex = columnIndex + 1).Value = Labels.LinkedIn;
                worksheet.Cell(lineIndex, columnIndex = columnIndex + 1).Value = Labels.Instagram;
                worksheet.Cell(lineIndex, columnIndex = columnIndex + 1).Value = Labels.YouTube;

                worksheet.Cell(lineIndex, columnIndex = columnIndex + 1).Value = Labels.Company + " - " + Labels.CompanyName;
                worksheet.Cell(lineIndex, columnIndex = columnIndex + 1).Value = Labels.Company + " - " + Labels.TradeName;
                worksheet.Cell(lineIndex, columnIndex = columnIndex + 1).Value = Labels.Company + " - " + Labels.CompanyResume;
                worksheet.Cell(lineIndex, columnIndex = columnIndex + 1).Value = Labels.Company + " - " + Labels.Logo500x500;
                worksheet.Cell(lineIndex, columnIndex = columnIndex + 1).Value = Labels.Company + " - " + Labels.LogoOriginal;
                worksheet.Cell(lineIndex, columnIndex = columnIndex + 1).Value = Labels.Company + " - " + Labels.Website;
                worksheet.Cell(lineIndex, columnIndex = columnIndex + 1).Value = Labels.Company + " - " + Labels.LinkedIn;
                worksheet.Cell(lineIndex, columnIndex = columnIndex + 1).Value = Labels.Company + " - " + Labels.Instagram;
                worksheet.Cell(lineIndex, columnIndex = columnIndex + 1).Value = Labels.Company + " - " + Labels.Twitter;
                worksheet.Cell(lineIndex, columnIndex = columnIndex + 1).Value = Labels.Company + " - " + Labels.YouTube;

                worksheet.Cell(lineIndex, columnIndex = columnIndex + 1).Value = Labels.AudiovisualTermsAcceptanceDate;
                worksheet.Column(columnIndex).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                skipFinalAdjustmentsColumnIndexes.Add(columnIndex);

                worksheet.Cell(lineIndex, columnIndex = columnIndex + 1).Value = Labels.OnboardingFinishDate;
                worksheet.Column(columnIndex).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                skipFinalAdjustmentsColumnIndexes.Add(columnIndex);

                worksheet.Cell(lineIndex, columnIndex = columnIndex + 1).Value = Labels.ShowOnWebsite;
                worksheet.Column(columnIndex).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                skipFinalAdjustmentsColumnIndexes.Add(columnIndex);

                worksheet.Cell(lineIndex, columnIndex = columnIndex + 1).Value = Labels.HighlightPosition;
                worksheet.Column(columnIndex).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                skipFinalAdjustmentsColumnIndexes.Add(columnIndex);

                worksheet.Cell(lineIndex, columnIndex = columnIndex + 1).Value = Labels.Creator;
                worksheet.Column(columnIndex).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                skipFinalAdjustmentsColumnIndexes.Add(columnIndex);

                #endregion

                if (collaboratorDtos.Any())
                {
                    #region Rows

                    foreach (var collaboratorDto in collaboratorDtos)
                    {
                        lineIndex++;
                        columnIndex = 0;

                        worksheet.Cell(lineIndex, columnIndex = columnIndex + 1).Value = collaboratorDto.Id;
                        worksheet.Cell(lineIndex, columnIndex).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                        worksheet.Cell(lineIndex, columnIndex = columnIndex + 1).Value = collaboratorDto.FullName ?? "-";
                        worksheet.Cell(lineIndex, columnIndex = columnIndex + 1).Value = collaboratorDto.Badge ?? "-";
                        worksheet.Cell(lineIndex, columnIndex = columnIndex + 1).Value = collaboratorDto.Email ?? "-";

                        worksheet.Cell(lineIndex, columnIndex = columnIndex + 1).Value = collaboratorDto.CellPhone ?? "-";
                        worksheet.Cell(lineIndex, columnIndex).Style.NumberFormat.Format = "00000";
                        worksheet.Cell(lineIndex, columnIndex).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                        worksheet.Cell(lineIndex, columnIndex = columnIndex + 1).Value = collaboratorDto.PhoneNumber ?? "-";
                        worksheet.Cell(lineIndex, columnIndex).Style.NumberFormat.Format = "00000";
                        worksheet.Cell(lineIndex, columnIndex).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                        worksheet.Cell(lineIndex, columnIndex = columnIndex + 1).Value = collaboratorDto.GetCollaboratorJobTitleBaseDtoByLanguageCode(cmd.UserInterfaceLanguage)?.Value ?? "-";
                        worksheet.Cell(lineIndex, columnIndex = columnIndex + 1).Value = collaboratorDto.GetMiniBioBaseDtoByLanguageCode(cmd.UserInterfaceLanguage)?.Value ?? "-";

                        worksheet.Cell(lineIndex, columnIndex = columnIndex + 1).Value = collaboratorDto.ImageUploadDate.HasValue ?
                            this.fileRepo.GetImageUrl(FileRepositoryPathType.UserImage, collaboratorDto.Uid, collaboratorDto.ImageUploadDate, true, "_500x500") : "-";

                        worksheet.Cell(lineIndex, columnIndex = columnIndex + 1).Value = collaboratorDto.ImageUploadDate.HasValue ?
                            this.fileRepo.GetImageUrl(FileRepositoryPathType.UserImage, collaboratorDto.Uid, collaboratorDto.ImageUploadDate, true) : "-";

                        worksheet.Cell(lineIndex, columnIndex = columnIndex + 1).Value = collaboratorDto.Website ?? "-";
                        worksheet.Cell(lineIndex, columnIndex = columnIndex + 1).Value = collaboratorDto.Linkedin ?? "-";
                        worksheet.Cell(lineIndex, columnIndex = columnIndex + 1).Value = collaboratorDto.Instagram ?? "-";
                        worksheet.Cell(lineIndex, columnIndex = columnIndex + 1).Value = collaboratorDto.Youtube ?? "-";

                        #region Organization

                        worksheet.Cell(lineIndex, columnIndex = columnIndex + 1).Value = collaboratorDto.AttendeeOrganizationBasesDtos.Select(ao => ao.OrganizationBaseDto.Name ?? "-")?.ToString(", ");
                        worksheet.Cell(lineIndex, columnIndex = columnIndex + 1).Value = collaboratorDto.AttendeeOrganizationBasesDtos.Select(ao => ao.OrganizationBaseDto.TradeName ?? "-")?.ToString(", ");
                        worksheet.Cell(lineIndex, columnIndex = columnIndex + 1).Value = collaboratorDto.AttendeeOrganizationBasesDtos.Select(ao => ao.OrganizationBaseDto.GetOrganizationDescriptionBaseDtoByLanguageCode(cmd.UserInterfaceLanguage)?.Value ?? "-")?.ToString(", ");

                        worksheet.Cell(lineIndex, columnIndex = columnIndex + 1).Value = collaboratorDto.AttendeeOrganizationBasesDtos.Select(ao => ao.OrganizationBaseDto.ImageUploadDate.HasValue ?
                            this.fileRepo.GetImageUrl(FileRepositoryPathType.OrganizationImage, ao.OrganizationBaseDto.Uid, ao.OrganizationBaseDto.ImageUploadDate, true, "_500x500") : "-")?.ToString(", ");

                        worksheet.Cell(lineIndex, columnIndex = columnIndex + 1).Value = collaboratorDto.AttendeeOrganizationBasesDtos.Select(ao => ao.OrganizationBaseDto.ImageUploadDate.HasValue ?
                            this.fileRepo.GetImageUrl(FileRepositoryPathType.OrganizationImage, ao.OrganizationBaseDto.Uid, ao.OrganizationBaseDto.ImageUploadDate, true) : "-")?.ToString(", ");

                        worksheet.Cell(lineIndex, columnIndex = columnIndex + 1).Value = collaboratorDto.AttendeeOrganizationBasesDtos.Select(ao => ao.OrganizationBaseDto.Website ?? "-")?.ToString(", ");
                        worksheet.Cell(lineIndex, columnIndex = columnIndex + 1).Value = collaboratorDto.AttendeeOrganizationBasesDtos.Select(ao => ao.OrganizationBaseDto.Linkedin ?? "-")?.ToString(", ");
                        worksheet.Cell(lineIndex, columnIndex = columnIndex + 1).Value = collaboratorDto.AttendeeOrganizationBasesDtos.Select(ao => ao.OrganizationBaseDto.Instagram ?? "-")?.ToString(", ");
                        worksheet.Cell(lineIndex, columnIndex = columnIndex + 1).Value = collaboratorDto.AttendeeOrganizationBasesDtos.Select(ao => ao.OrganizationBaseDto.Twitter ?? "-")?.ToString(", ");
                        worksheet.Cell(lineIndex, columnIndex = columnIndex + 1).Value = collaboratorDto.AttendeeOrganizationBasesDtos.Select(ao => ao.OrganizationBaseDto.Youtube ?? "-")?.ToString(", ");

                        #endregion

                        worksheet.Cell(lineIndex, columnIndex = columnIndex + 1).Value = collaboratorDto.EditionAttendeeCollaboratorBaseDto.SpeakerTermsAcceptanceDate?.ToStringHourMinute() ?? "-";
                        worksheet.Cell(lineIndex, columnIndex).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                        worksheet.Cell(lineIndex, columnIndex = columnIndex + 1).Value = collaboratorDto.EditionAttendeeCollaboratorBaseDto.OnboardingFinishDate?.ToStringHourMinute() ?? "-";
                        worksheet.Cell(lineIndex, columnIndex).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                        worksheet.Cell(lineIndex, columnIndex = columnIndex + 1).Value = collaboratorDto.EditionAttendeeCollaboratorBaseDto.AttendeeCollaboratorTypeDto.IsApiDisplayEnabled.ToYesOrNoString();
                        worksheet.Cell(lineIndex, columnIndex).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                        worksheet.Cell(lineIndex, columnIndex = columnIndex + 1).Value = collaboratorDto.EditionAttendeeCollaboratorBaseDto.AttendeeCollaboratorTypeDto.ApiHighlightPosition?.ToString() ?? "-";
                        worksheet.Cell(lineIndex, columnIndex).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                        worksheet.Cell(lineIndex, columnIndex = columnIndex + 1).Value = collaboratorDto.CreatorBaseDto.Email ?? "-";
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

            this.AppValidationResult.Data = File.ReadAllBytes(filePath);

            if (cmd.SendToEmails?.Any() == true)
            {
                try
                {
                    foreach (var email in cmd.SendToEmails)
                    {
                        using (var mailMessage = this.mailerService.SendSpeakersReportEmail(new SendSpeakersReportEmailAsync(
                            filePath,
                            email,
                            await this.editionRepo.GetAsync(cmd.EditionId ?? 0),
                            cmd.UserInterfaceLanguage)))
                        {
                            using (var smtpClient = new SmtpClient())
                            {
                                await smtpClient.SendMailAsync(mailMessage);
                            }
                        }
                    }
                }
                finally
                {
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                }
            }

            return this.AppValidationResult;
        }
    }
}