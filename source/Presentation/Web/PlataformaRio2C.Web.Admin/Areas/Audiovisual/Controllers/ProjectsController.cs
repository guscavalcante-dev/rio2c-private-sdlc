// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 06-28-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 06-20-2021
// ***********************************************************************
// <copyright file="ProjectsController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using DataTables.AspNet.Core;
using DataTables.AspNet.Mvc5;
using MediatR;
using PlataformaRio2C.Application.TemplateDocuments;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.CrossCutting.Identity.AuthorizeAttributes;
using PlataformaRio2C.Infra.CrossCutting.Identity.Service;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using PlataformaRio2C.Infra.CrossCutting.Tools.Helpers;
using PlataformaRio2C.Web.Admin.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using PlataformaRio2C.Application.ViewModels;
using PlataformaRio2c.Infra.Data.FileRepository;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Statics;
using PlataformaRio2C.Infra.Report.Models;
using Constants = PlataformaRio2C.Domain.Constants;
using PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions;
using PlataformaRio2C.Web.Admin.Controllers;

namespace PlataformaRio2C.Web.Admin.Areas.Audiovisual.Controllers
{
    /// <summary>ProjectsController</summary>
    [AjaxAuthorize(Order = 1, Roles = Constants.Role.AnyAdmin)]
    [AuthorizeCollaboratorType(Order = 2, Types = Constants.CollaboratorType.AdminAudiovisual + "," + Constants.CollaboratorType.CommissionAudiovisual)]
    public class ProjectsController : BaseController
    {
        private readonly IProjectRepository projectRepo;
        private readonly IInterestRepository interestRepo;
        private readonly IFileRepository fileRepo;

        /// <summary>Initializes a new instance of the <see cref="ProjectsController"/> class.</summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="identityController">The identity controller.</param>
        /// <param name="projectRepository">The project repository.</param>
        /// <param name="interestRepository">The interest repository.</param>
        /// <param name="fileRepository">The file repository.</param>
        public ProjectsController(
            IMediator commandBus,
            IdentityAutenticationService identityController,
            IProjectRepository projectRepository,
            IInterestRepository interestRepository,
            IFileRepository fileRepository)
            : base(commandBus, identityController)
        {
            this.projectRepo = projectRepository;
            this.interestRepo = interestRepository;
            this.fileRepo = fileRepository;
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

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.Projects, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Labels.AudioVisual, null),
                new BreadcrumbItemHelper(Labels.Projects, Url.Action("Index", "Projects", new { Area = "Audiovisual" }))
            });

            #endregion

            ViewBag.GenreInterests = await this.interestRepo.FindAllDtosByInterestGroupUidAsync(InterestGroup.Genre.Uid);

            return View();
        }

        /// <summary>
        /// Searches the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="showPitchings">if set to <c>true</c> [show pitchings].</param>
        /// <param name="interestUid">The interest uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Search(IDataTablesRequest request, bool showPitchings, Guid? interestUid)
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
            string fileNameZip = Labels.Projects + "_" + DateTime.UtcNow.ToUserTimeZone().ToString("yyyyMMdd") + ".zip";
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

        #region Finds

        /// <summary>Finds all by filters.</summary>
        /// <param name="keywords">The keywords.</param>
        /// <param name="customFilter">The custom filter.</param>
        /// <param name="buyerOrganizationUid">The buyer organization uid.</param>
        /// <param name="page">The page.</param>
        /// <returns></returns>
        [HttpGet]
        //[AuthorizeCollaboratorType(Order = 2, Types = Constants.CollaboratorType.AdminAudiovisual + "," + Constants.CollaboratorType.CuratorshipAudiovisual + "," + Constants.CollaboratorType.CommissionAudiovisual)]
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