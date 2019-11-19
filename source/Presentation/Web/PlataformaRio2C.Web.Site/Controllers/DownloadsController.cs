// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 11-18-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 11-19-2019
// ***********************************************************************
// <copyright file="DownloadsController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using ClosedXML.Excel;
using MediatR;
using PlataformaRio2C.Infra.CrossCutting.Identity.Service;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.CrossCutting.Identity.AuthorizeAttributes;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.CustomActionResults;
using PlataformaRio2C.Web.Site.Filters;
using Constants = PlataformaRio2C.Domain.Constants;

namespace PlataformaRio2C.Web.Site.Controllers
{
    /// <summary>DownloadsController</summary>
    [AjaxAuthorize(Order = 1)]
    [AuthorizeCollaboratorType(Order = 2, Types = Constants.CollaboratorType.Industry )]
    public class DownloadsController : BaseController
    {
        private readonly IAttendeeCollaboratorRepository attendeeCollaboratorRepo;

        /// <summary>Initializes a new instance of the <see cref="DownloadsController"/> class.</summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="identityController">The identity controller.</param>
        /// <param name="attendeeCollaboratorRepository">The attendee collaborator repository.</param>
        public DownloadsController(
            IMediator commandBus, 
            IdentityAutenticationService identityController,
            IAttendeeCollaboratorRepository attendeeCollaboratorRepository)
            : base(commandBus, identityController)
        {
            this.attendeeCollaboratorRepo = attendeeCollaboratorRepository;
        }

        #region Contacts

        /// <summary>Indexes this instance.</summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Contacts()
        {
            var attendeeCollaboratorsDtos = await this.attendeeCollaboratorRepo.FindAllExcelNetworkDtoByEditionIdAsync(this.EditionDto?.Id ?? 0);

            var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add(Labels.Contacts);

            if (attendeeCollaboratorsDtos.Any())
            {
                // Header
                var lineIndex = 1;
                var columnIndex = 0;
                worksheet.Cell(lineIndex, columnIndex = columnIndex + 1).Value = "#";
                worksheet.Cell(lineIndex, columnIndex = columnIndex + 1).Value = Labels.Company;
                worksheet.Cell(lineIndex, columnIndex = columnIndex + 1).Value = Labels.JobTitle;
                worksheet.Cell(lineIndex, columnIndex = columnIndex + 1).Value = Labels.Name;
                worksheet.Cell(lineIndex, columnIndex = columnIndex + 1).Value = Labels.Email;

                // Rows
                var counter = 0;
                foreach (var attendeeCollaboratorDto in attendeeCollaboratorsDtos)
                {
                    counter++;
                    lineIndex++;
                    columnIndex = 0;

                    worksheet.Cell(lineIndex, columnIndex = columnIndex + 1).Value = counter;
                    worksheet.Cell(lineIndex, columnIndex = columnIndex + 1).Value = attendeeCollaboratorDto.AttendeeOrganizationsDtos.FirstOrDefault()?.Organization?.TradeName;
                    worksheet.Cell(lineIndex, columnIndex = columnIndex + 1).Value = attendeeCollaboratorDto.GetJobTitleDtoByLanguageCode(ViewBag.UserInterfaceLanguage)?.Value;
                    worksheet.Cell(lineIndex, columnIndex = columnIndex + 1).Value = attendeeCollaboratorDto.Collaborator.GetFullName();
                    worksheet.Cell(lineIndex, columnIndex = columnIndex + 1).Value = attendeeCollaboratorDto.Collaborator.PublicEmail;
                }

                for (var adjustColumnIndex = 1; adjustColumnIndex <= columnIndex; adjustColumnIndex++)
                {
                    worksheet.Column(adjustColumnIndex).AdjustToContents();
                }
            }

            return new ExcelResult(workbook, Labels.Contacts + "_" + DateTime.Now.ToString("yyyyMMdd"));
        }

        #endregion
    }
}