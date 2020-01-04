// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 06-28-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-03-2020
// ***********************************************************************
// <copyright file="ProjectsController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using DataTables.AspNet.Core;
using DataTables.AspNet.Mvc5;
using iTextSharp.text.pdf;
using MediatR;
using PlataformaRio2C.Application.TemplateDocuments;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.CrossCutting.Identity.AuthorizeAttributes;
using PlataformaRio2C.Infra.CrossCutting.Identity.Service;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using PlataformaRio2C.Infra.CrossCutting.Tools.Helpers;
using PlataformaRio2C.Infra.Report;
using PlataformaRio2C.Web.Admin.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Constants = PlataformaRio2C.Domain.Constants;

namespace PlataformaRio2C.Web.Admin.Controllers
{
    /// <summary>ProjectsController</summary>
    [AjaxAuthorize(Order = 1, Roles = Constants.Role.AnyAdmin)]
    [AuthorizeCollaboratorType(Order = 2, Types = Constants.CollaboratorType.AdminAudiovisual + "," + Constants.CollaboratorType.CommissionAudiovisual)]
    public class ProjectsController : BaseController
    {
        private readonly IProjectRepository projectRepo;
        private readonly IInterestRepository interestRepo;

        /// <summary>Initializes a new instance of the <see cref="ProjectsController"/> class.</summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="identityController">The identity controller.</param>
        /// <param name="projectRepository">The project repository.</param>
        /// <param name="interestRepository">The interest repository.</param>
        public ProjectsController(
            IMediator commandBus,
            IdentityAutenticationService identityController,
            IProjectRepository projectRepository,
            IInterestRepository interestRepository)
            : base(commandBus, identityController)
        {
            this.projectRepo = projectRepository;
            this.interestRepo = interestRepository;
        }

        #region Pitchings

        /// <summary>Pitchingses this instance.</summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Pitchings()
        {
            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.Producers, new List<BreadcrumbItemHelper>{
                new BreadcrumbItemHelper(Labels.ProjectsForPitching, Url.Action("Pitchings", "Projects", new { Area = "" }))
            });

            #endregion

            ViewBag.GenreInterests = await this.interestRepo.FindAllDtosByInterestGroupUidAsync(InterestGroup.Genre.Uid);
            ViewBag.Page = 1;
            ViewBag.PageSize = 10;

            return View();
        }

        /// <summary>Shows the pitching list widget.</summary>
        /// <param name="request">The request.</param>
        /// <param name="interestUid">The interest uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowPitchingListWidget(IDataTablesRequest request, Guid? interestUid)
        {
            var projects = await this.projectRepo.FindAllPitchingBaseDtosByFiltersAndByPageAsync(
                request.Start / request.Length,
                request.Length,
                request.GetSortColumns(),
                request.Search?.Value,
                interestUid,
                this.UserInterfaceLanguage,
                this.EditionDto.Id);
            foreach (var project in projects)
            {
                project.Genre = new List<string>();
                foreach (var item in project.Genres)
                {
                    project.Genre.Add(item.Interest.Name.GetSeparatorTranslation(this.UserInterfaceLanguage, '|'));
                }
            }

            ViewBag.GenreInterests = await this.interestRepo.FindAllDtosByInterestGroupUidAsync(InterestGroup.Genre.Uid);
            ViewBag.Page = request.Start / request.Length;
            ViewBag.PageSize = request.Length;

            var response = DataTablesResponse.Create(request, projects.TotalItemCount, projects.TotalItemCount, projects);

            return Json(new
            {
                status = "success",
                dataTable = response
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Download PDFs

        /// <summary>Downloads the PDFS.</summary>
        /// <param name="keyword">The keyword.</param>
        /// <param name="interestUid">The interest uid.</param>
        /// <param name="selectedProjectsUids">The selected projects uids.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<FileResult> DownloadPdfs(string keyword, Guid? interestUid, string selectedProjectsUids)
        {
            var projectsDtos = await this.projectRepo.FindAllPitchingDtosByFiltersAsync(
                keyword, 
                interestUid,
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
                var compressed = CompressPdf(pdf.GetStream().ToArray());

                return File(compressed, "application/pdf", Labels.Project + "_" + projectDto.Project.Id.ToString("D4") + "_" + projectDto.GetTitleDtoByLanguageCode(Constants.Culture.Portuguese).ProjectTitle.Value + ".pdf");
            }

            // Many projects returned
            var dictPdf = new Dictionary<string, MemoryStream>();

            foreach (var projectDto in projectsDtos)
            {
                var pdfDocument = new PlataformaRio2CDocument(new ProjectDocumentTemplate(projectDto));
                dictPdf.Add(Labels.Project + "_" + projectDto.Project.Id.ToString("D4") + "_" + projectDto.GetTitleDtoByLanguageCode(Constants.Culture.Portuguese).ProjectTitle.Value + ".pdf", pdfDocument.GetStream());
            }

            return ZipDocuments(dictPdf);
        }

        /// <summary>Zips the documents.</summary>
        /// <param name="pdfCollection">The PDF collection.</param>
        /// <returns></returns>
        private FileResult ZipDocuments(Dictionary<string, MemoryStream> pdfCollection)
        {
            string fileNameZip = Labels.Projects + "_" + DateTime.Now.ToString("yyyyMMdd") + ".zip";
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

        public byte[] CompressPdf(byte[] src)
        {
            PdfReader reader = new PdfReader(src);
            using (MemoryStream ms = new MemoryStream())
            {
                using (PdfStamper stamper =
                    new PdfStamper(reader, ms, PdfWriter.VERSION_1_5))
                {
                    stamper.Writer.CompressionLevel = 9;
                    int total = reader.NumberOfPages + 1;
                    for (int i = 1; i < total; i++)
                    {
                        reader.SetPageContent(i, reader.GetPageContent(i));
                    }
                    stamper.SetFullCompression();
                }
                return ms.ToArray();
            }
        }
        #endregion
    }
}