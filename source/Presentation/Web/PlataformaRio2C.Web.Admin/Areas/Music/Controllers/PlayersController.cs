// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Renan Valentim
// Created          : 12-21-2023
//
// Last Modified By : Renan Valentim
// Last Modified On : 12-21-2023
// ***********************************************************************
// <copyright file="PlayersController.cs" company="Softo">
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
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Statics;
using PlataformaRio2C.Infra.CrossCutting.Identity.AuthorizeAttributes;
using PlataformaRio2C.Infra.CrossCutting.Identity.Service;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions;
using PlataformaRio2C.Infra.CrossCutting.Tools.Helpers;
using PlataformaRio2C.Web.Admin.Controllers;
using PlataformaRio2C.Web.Admin.Filters;
using Constants = PlataformaRio2C.Domain.Constants;
using ClosedXML.Excel;
using PlataformaRio2C.Domain.ApiModels;
using PlataformaRio2C.Infra.CrossCutting.Tools.CustomActionResults;
using System.IO;

namespace PlataformaRio2C.Web.Admin.Areas.Music.Controllers
{
    /// <summary>PlayersController</summary>
    [AjaxAuthorize(Order = 1, Roles = Constants.Role.AnyAdmin)]
    [AuthorizeCollaboratorType(Order = 2, Types = Constants.CollaboratorType.AdminMusic)]
    public class PlayersController : BaseController
    {
        private readonly IOrganizationRepository organizationRepo;
        private readonly IAttendeeOrganizationRepository attendeeOrganizationRepo;
        private readonly IActivityRepository activityRepo;
        private readonly ITargetAudienceRepository targetAudienceRepo;
        private readonly IInterestRepository interestRepo;
        private readonly IFileRepository fileRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayersController"/> class.
        /// </summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="identityController">The identity controller.</param>
        /// <param name="organizationRepository">The organization repository.</param>
        /// <param name="attendeeOrganizationRepository">The attendee organization repository.</param>
        /// <param name="activityRepository">The activity repository.</param>
        /// <param name="targetAudienceRepository">The target audience repository.</param>
        /// <param name="interestRepository">The interest repository.</param>
        /// <param name="fileRepository">The file repository.</param>
        public PlayersController(
            IMediator commandBus,
            IdentityAutenticationService identityController,
            IOrganizationRepository organizationRepository,
            IAttendeeOrganizationRepository attendeeOrganizationRepository,
            IActivityRepository activityRepository,
            ITargetAudienceRepository targetAudienceRepository,
            IInterestRepository interestRepository,
            IFileRepository fileRepository)
            : base(commandBus, identityController)
        {
            this.organizationRepo = organizationRepository;
            this.attendeeOrganizationRepo = attendeeOrganizationRepository;
            this.activityRepo = activityRepository;
            this.targetAudienceRepo = targetAudienceRepository;
            this.interestRepo = interestRepository;
            this.fileRepo = fileRepository;
        }

        #region List

        /// <summary>Indexes the specified search view model.</summary>
        /// <param name="searchViewModel">The search view model.</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index(PlayerCompanySearchViewModel searchViewModel)
        {
            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.Players, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Labels.Music, null),
                new BreadcrumbItemHelper(Labels.Players, Url.Action("Index", "Players", new { Area = "Music" }))
            });

            #endregion

            return View(searchViewModel);
        }

        /// <summary>Searches the specified request.</summary>
        /// <param name="request">The request.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <param name="showAllOrganizations">if set to <c>true</c> [show all organizations].</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Search(IDataTablesRequest request, bool showAllEditions, bool showAllOrganizations)
        {
            var players = await this.organizationRepo.FindAllByDataTable(
                request.Start / request.Length,
                request.Length,
                request.Search?.Value,
                request.GetSortColumns(),
                OrganizationType.MusicPlayer.Uid,
                showAllEditions,
                showAllOrganizations,
                this.EditionDto.Id);

            var response = DataTablesResponse.Create(request, players.TotalItemCount, players.TotalItemCount, players);

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
        /// <param name="showAllOrganizations">if set to <c>true</c> [show all organizations].</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ExportToExcel(string searchKeywords, bool showAllEditions, bool showAllOrganizations)
        {
            string fileName = Labels.MusicPlayersReport + "_" + DateTime.UtcNow.ToStringFileNameTimestamp();
            string filePath = Path.Combine(Path.GetTempPath(), fileName + ".xlsx");

            try
            {
                var players = await this.organizationRepo.FindAllPlayersByDataTable(
                    1,
                    10000,
                    searchKeywords,
                    new List<Tuple<string, string>>(), //request.GetSortColumns(),
                    showAllEditions,
                    showAllOrganizations,
                    this.EditionDto.Id,
                    OrganizationType.MusicPlayer.Uid,
                    true
                );

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
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.CompanyName;
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.TradeName;
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.Document;
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.Website;
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.Instagram;
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.YouTube;
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.LinkedIn;
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.Twitter;
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.Description;
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.Interest + " - " + Labels.MarketLookingFor;
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.TargetAudience;
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.LogoOriginal;

                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.ShowOnWebsite;
                    skipFinalAdjustmentsColumnIndexes.Add(columnIndex);

                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.ReceivedProjects;
                    skipFinalAdjustmentsColumnIndexes.Add(columnIndex);

                    #endregion

                    if (players.Any())
                    {
                        #region Rows

                        foreach (var organizationDto in players)
                        {
                            lineIndex++;
                            columnIndex = 0;

                            worksheet.Cell(lineIndex, columnIndex += 1).Value = organizationDto.Id;
                            worksheet.Cell(lineIndex, columnIndex).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                            worksheet.Cell(lineIndex, columnIndex += 1).Value = organizationDto.Name ?? "-";
                            worksheet.Cell(lineIndex, columnIndex += 1).Value = organizationDto.CompanyName ?? "-";
                            worksheet.Cell(lineIndex, columnIndex += 1).Value = organizationDto.TradeName ?? "-";
                            worksheet.Cell(lineIndex, columnIndex += 1).Value = organizationDto.Document ?? "-";
                            worksheet.Cell(lineIndex, columnIndex += 1).Value = organizationDto.Website ?? "-";
                            worksheet.Cell(lineIndex, columnIndex += 1).Value = organizationDto.Instagram ?? "-";
                            worksheet.Cell(lineIndex, columnIndex += 1).Value = organizationDto.Youtube ?? "-";
                            worksheet.Cell(lineIndex, columnIndex += 1).Value = organizationDto.Linkedin ?? "-";
                            worksheet.Cell(lineIndex, columnIndex += 1).Value = organizationDto.Twitter ?? "-";

                            worksheet.Cell(lineIndex, columnIndex += 1).Value = organizationDto.GetOrganizationDescriptionBaseDtoByLanguageCode(this.UserInterfaceLanguage)?.Value ?? "-";
                            var musicLookingFor = organizationDto.GetAllInterestsNamesByInterestGroupUidAndCulture(
                                InterestGroup.MusicLookingFor.Uid,
                                this.UserInterfaceLanguage
                            ); 
                            worksheet.Cell(lineIndex, columnIndex += 1).Value = string.IsNullOrEmpty(musicLookingFor) ? "-" : musicLookingFor;

                            worksheet.Cell(lineIndex, columnIndex += 1).Value = organizationDto.OrganizationTargetAudiencesDtos
                                ?.Select(otaDto => otaDto.TargetAudienceName?.GetSeparatorTranslation(this.UserInterfaceLanguage, '|'))
                                ?.ToString("; ");

                            worksheet.Cell(lineIndex, columnIndex += 1).Value = organizationDto.ImageUploadDate.HasValue ?
                                this.fileRepo.GetImageUrl(FileRepositoryPathType.OrganizationImage, organizationDto.Uid, organizationDto.ImageUploadDate, true, "_500x500") 
                                    : "-";

                            worksheet.Cell(lineIndex, columnIndex += 1).Value = organizationDto.IsApiDisplayEnabled.ToYesOrNoString();
                            worksheet.Cell(lineIndex, columnIndex).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                            worksheet.Cell(lineIndex, columnIndex += 1).Value = organizationDto.ReceivedProjectsCount;
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

        #region Total Count Widget

        /// <summary>Shows the total count widget.</summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowTotalCountWidget()
        {
            var playersCount = await this.organizationRepo.CountAllByDataTable(OrganizationType.MusicPlayer.Uid, true, this.EditionDto.Id);

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/TotalCountWidget", playersCount), divIdOrClass = "#MusicPlayersTotalCountWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Edition Count Widget

        /// <summary>Shows the edition count widget.</summary>
        /// <returns></returns>
        public async Task<ActionResult> ShowEditionCountWidget()
        {
            var playersCount = await this.organizationRepo.CountAllByDataTable(OrganizationType.MusicPlayer.Uid, false, this.EditionDto.Id);

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/EditionCountWidget", playersCount), divIdOrClass = "#MusicPlayersEditionCountWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Edition Count Odometer Widget

        /// <summary>Shows the edition count widget.</summary>
        /// <returns></returns>
        public async Task<ActionResult> ShowEditionCountOdometerWidget()
        {
            var playersCount = await this.organizationRepo.CountAllByDataTable(OrganizationType.MusicPlayer.Uid, false, this.EditionDto.Id);
            return Json(new
            {
                status = "success",
                odometerCount = playersCount,
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/EditionCountOdometerWidget", playersCount), divIdOrClass = "#MusicPlayersEditionCountOdometerWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Details

        /// <summary>Detailses the specified identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Details(Guid? id)
        {
            var attendeeOrganizationDto = await this.attendeeOrganizationRepo.FindDetailsDtoByOrganizationUidAndByOrganizationTypeUidAsync(
                id ?? Guid.Empty, 
                OrganizationType.MusicPlayer.Uid,
                this.EditionDto.Id, 
                false);

            if (attendeeOrganizationDto == null)
            {
                this.StatusMessageToastr(string.Format(Messages.EntityNotAction, Labels.Player, Labels.FoundM.ToLowerInvariant()), Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);
                return RedirectToAction("Index", "Players", new { Area = "Music" });
            }

            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.Players, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Labels.Music, null),
                new BreadcrumbItemHelper(Labels.Players, Url.Action("Index", "Players", new { Area = "Music" })),
                new BreadcrumbItemHelper(attendeeOrganizationDto.Organization.Name, Url.Action("Details", "Players", new { Area = "Music", id }))
            });

            #endregion

            ViewBag.OrganizationTypeUid = OrganizationType.MusicPlayer.Uid; // It's the admin page accessed and not the organization type of the current organization

            return View(attendeeOrganizationDto);
        }

        #endregion

        #region Create

        /// <summary>
        /// Shows the create modal.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowCreateModal()
        {
            var cmd = new CreateOrganization(
                OrganizationType.MusicPlayer,
                null,
                await this.CommandBus.Send(new FindAllLanguagesDtosAsync(this.UserInterfaceLanguage)),
                await this.CommandBus.Send(new FindAllCountriesBaseDtosAsync(this.UserInterfaceLanguage)),
                await this.activityRepo.FindAllByProjectTypeIdAsync(ProjectType.Music.Id),
                await this.targetAudienceRepo.FindAllByProjectTypeIdAsync(ProjectType.Music.Id),
                await this.interestRepo.FindAllDtosByProjectTypeIdAsync(ProjectType.Music.Id),
                null,
                false,
                false,
                false,
                false,
                true,
                false,
                false);

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Modals/CreateModal", cmd), divIdOrClass = "#GlobalModalContainer" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>Creates the specified command.</summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Create(CreateOrganization cmd)
        {
            var result = new AppValidationResult();

            try
            {
                if (!ModelState.IsValid)
                {
                    throw new DomainException(Messages.CorrectFormValues);
                }

                cmd.UpdatePreSendProperties(
                    OrganizationType.MusicPlayer,
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

                cmd.UpdateDropdownProperties(
                    await this.CommandBus.Send(new FindAllHoldingsBaseDtosAsync(null, this.UserInterfaceLanguage)),
                    await this.CommandBus.Send(new FindAllCountriesBaseDtosAsync(this.UserInterfaceLanguage)),
                    await this.activityRepo.FindAllByProjectTypeIdAsync(ProjectType.Music.Id),
                    await this.targetAudienceRepo.FindAllByProjectTypeIdAsync(ProjectType.Music.Id),
                    null);

                return Json(new
                {
                    status = "error",
                    message = result.Errors?.FirstOrDefault(e => e.Target == "ToastrError")?.Message ?? ex.GetInnerMessage(),
                    pages = new List<dynamic>
                    {
                        new { page = this.RenderRazorViewToString("Modals/_Form", cmd), divIdOrClass = "#form-container" },
                    }
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return Json(new { status = "error", message = Messages.WeFoundAndError, }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Player, Labels.CreatedM) });
        }

        #endregion

        #region Update

        /// <summary>Shows the update modal.</summary>
        /// <param name="organizationUid">The organization uid.</param>
        /// <param name="isAddingToCurrentEdition">The is adding to current edition.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowUpdateModal(Guid? organizationUid, bool? isAddingToCurrentEdition)
        {
            UpdateOrganization cmd;

            try
            {
                cmd = new UpdateOrganization(
                    await this.CommandBus.Send(new FindOrganizationDtoByUidAsync(organizationUid, this.EditionDto.Id, this.UserInterfaceLanguage)),
                    OrganizationType.MusicPlayer,
                    await this.CommandBus.Send(new FindAllHoldingsBaseDtosAsync(null, this.UserInterfaceLanguage)),
                    await this.CommandBus.Send(new FindAllLanguagesDtosAsync(this.UserInterfaceLanguage)),
                    await this.CommandBus.Send(new FindAllCountriesBaseDtosAsync(this.UserInterfaceLanguage)),
                    await this.activityRepo.FindAllByProjectTypeIdAsync(ProjectType.Music.Id),
                    await this.targetAudienceRepo.FindAllByProjectTypeIdAsync(ProjectType.Music.Id),
                    await this.interestRepo.FindAllDtosByProjectTypeIdAsync(ProjectType.Music.Id),
                    null,
                    isAddingToCurrentEdition,
                    false,
                    false,
                    false,
                    false,
                    true,
                    false,
                    false);
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

        /// <summary>Updates the specified command.</summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Update(UpdateOrganization cmd)
        {
            var result = new AppValidationResult();

            try
            {
                if (!ModelState.IsValid)
                {
                    throw new DomainException(Messages.CorrectFormValues);
                }

                cmd.UpdatePreSendProperties(
                    OrganizationType.MusicPlayer,
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

                cmd.UpdateDropdownProperties(
                    await this.CommandBus.Send(new FindAllHoldingsBaseDtosAsync(null, this.UserInterfaceLanguage)),
                    await this.CommandBus.Send(new FindAllCountriesBaseDtosAsync(this.UserInterfaceLanguage)),
                    await this.activityRepo.FindAllByProjectTypeIdAsync(ProjectType.Music.Id),
                    await this.targetAudienceRepo.FindAllByProjectTypeIdAsync(ProjectType.Music.Id),
                    null);

                return Json(new
                {
                    status = "error",
                    message = result.Errors?.FirstOrDefault(e => e.Target == "ToastrError")?.Message ?? ex.GetInnerMessage(),
                    pages = new List<dynamic>
                    {
                        new { page = this.RenderRazorViewToString("Modals/_Form", cmd), divIdOrClass = "#form-container" },
                    }
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return Json(new { status = "error", message = Messages.WeFoundAndError, }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Player, Labels.UpdatedM) });
        }

        #endregion

        #region Delete

        /// <summary>Deletes the specified command.</summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Delete(DeleteOrganization cmd)
        {
            var result = new AppValidationResult();

            try
            {
                if (!ModelState.IsValid)
                {
                    throw new DomainException(Messages.CorrectFormValues);
                }

                cmd.UpdatePreSendProperties(
                    OrganizationType.MusicPlayer,
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

            return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Player, Labels.DeletedM) });
        }

        #endregion

        #region Finds

        /// <summary>Finds all by filters.</summary>
        /// <param name="keywords">The keywords.</param>
        /// <param name="customFilter">The custom filter.</param>
        /// <param name="page">The page.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> FindAllByFilters(string keywords, string customFilter, int? page = 1)
        {
            var collaboratorsApiDtos = await this.organizationRepo.FindAllDropdownApiListDtoPaged(
                this.EditionDto.Id,
                keywords,
                customFilter,
                OrganizationType.MusicPlayer.Uid,
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
                Organizations = collaboratorsApiDtos?.Select(c => new OrganizationDropdownDto
                {
                    Uid = c.Uid,
                    Name = c.Name,
                    TradeName = c.TradeName,
                    CompanyName = c.CompanyName,
                    Picture = c.ImageUploadDate.HasValue ? this.fileRepo.GetImageUrl(FileRepositoryPathType.OrganizationImage, c.Uid, c.ImageUploadDate, true) : null
                })?.ToList()
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}