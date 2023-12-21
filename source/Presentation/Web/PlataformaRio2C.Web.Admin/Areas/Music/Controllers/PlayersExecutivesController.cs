// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Elton Assunção
// Created          : 12-19-2023
//
// Last Modified By : Elton Assunção
// Last Modified On : 12-19-2023
// ***********************************************************************
// <copyright file="PlayersExecutivesController.cs" company="Softo">
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
using PlataformaRio2C.Web.Admin.Filters;
using Constants = PlataformaRio2C.Domain.Constants;
using PlataformaRio2C.Web.Admin.Controllers;
using ClosedXML.Excel;
using PlataformaRio2C.Domain.ApiModels;
using PlataformaRio2C.Infra.CrossCutting.Tools.CustomActionResults;
using System.IO;

namespace PlataformaRio2C.Web.Admin.Areas.Music.Controllers
{

    /// <summary>
    /// PlayersExecutivesController
    /// </summary>
    [AjaxAuthorize(Order = 1, Roles = Constants.Role.AnyAdmin)]
    [AuthorizeCollaboratorType(Order = 2, Types = Constants.CollaboratorType.AdminMusic)]
    public class PlayersExecutivesController : BaseController
    {
        private readonly ICollaboratorRepository collaboratorRepo;
        private readonly IAttendeeCollaboratorRepository attendeeCollaboratorRepo;
        private readonly IAttendeeOrganizationRepository attendeeOrganizationRepo;
        private readonly IFileRepository fileRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayersExecutivesController" /> class.
        /// </summary>
        /// <param name="commandBus"></param>
        /// <param name="identityControlle"></param>
        /// <param name="collaboratorRepository"></param>
        /// <param name="attendeeCollaboratorRepo"></param>
        /// <param name="attendeeOrganizationRepo"></param>
        /// <param name="fileRepo"></param>
        public PlayersExecutivesController(
            IMediator commandBus,
            IdentityAutenticationService identityControlle,
            ICollaboratorRepository collaboratorRepository,
            IAttendeeCollaboratorRepository attendeeCollaboratorRepo,
            IAttendeeOrganizationRepository attendeeOrganizationRepo,
            IFileRepository fileRepo
            )
            : base(commandBus, identityControlle)
        {
            this.collaboratorRepo = collaboratorRepository;
            this.attendeeCollaboratorRepo = attendeeCollaboratorRepo;
            this.attendeeOrganizationRepo = attendeeOrganizationRepo;
            this.fileRepo = fileRepo;
        }

        #region List

        /// <summary>Indexes the specified search view model.</summary>
        /// <param name="searchViewModel">The search view model.</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index(CollaboratorSearchViewModel searchViewModel)
        {
            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.MusicCommission, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Labels.Music, null),
                new BreadcrumbItemHelper(Labels.Players, Url.Action("Index", "Players", new { Area = "Music" })),
                new BreadcrumbItemHelper(Labels.Executives, Url.Action("Index", "PlayersExecutives", new { Area = "Music" }))
            });

            #endregion

            return View(searchViewModel);
        }

        /// <summary>Searches the specified request.</summary>
        /// <param name="request">The request.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <param name="showAllParticipants">if set to <c>true</c> [show all participants].</param>
        /// <param name="showHighlights">The show highlights.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Search(IDataTablesRequest request, bool showAllEditions, bool showAllParticipants, bool? showHighlights)
        {
            var audiovisualExecutives = await this.collaboratorRepo.FindAllByDataTable(
                request.Start / request.Length,
                request.Length,
                request.Search?.Value,
                request.GetSortColumns(),
                new List<Guid>(),
                new string[] { CollaboratorType.PlayerExecutiveMusic.Name },
                new string[] { OrganizationType.MusicPlayer.Name },
                showAllEditions,
                showAllParticipants,
                showHighlights,
                this.EditionDto?.Id
            );

            var response = DataTablesResponse.Create(request, audiovisualExecutives.TotalItemCount, audiovisualExecutives.TotalItemCount, audiovisualExecutives);

            return Json(new
            {
                status = "success",
                dataTable = response
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Exports the evaluators report to excel.
        /// </summary>
        /// <param name="searchViewModel">The search view model.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ExportPlayersExecutivesReportToExcel(CollaboratorSearchViewModel searchViewModel)
        {
            string fileName = $@"{Labels.PlayersExecutivesReport}_{DateTime.UtcNow.ToStringFileNameTimestamp()}";
            string filePath = Path.Combine(Path.GetTempPath(), fileName + ".xlsx");

            try
            {
                var playerExecutiveReportDtos = await this.collaboratorRepo.FindAllPlayersExecutivesReportByDataTable(
                    1,
                    10000,
                    searchViewModel.Search,
                    new List<Tuple<string, string>>(), //request.GetSortColumns(),
                    searchViewModel.ShowAllEditions,
                    searchViewModel.ShowAllParticipants,
                    false,
                    this.EditionDto?.Id
                );

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add(Labels.PlayersExecutives);

                    #region Header

                    var lineIndex = 1;
                    var columnIndex = 0;
                    var skipFinalAdjustmentsColumnIndexes = new List<int>();

                    worksheet.Cell(lineIndex, columnIndex += 1).Value = $@"{Labels.Player} - {Labels.Name}";
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = $@"{Labels.Player} - {Labels.TradeName}";
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.FirstName;
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.LastNames;
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.BadgeName;
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.CellPhone;
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.PhoneNumber;
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.AccessEmail;
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.PublicEmail;
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.Website;
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.LinkedIn;
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.Twitter;
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.Instagram;
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.YouTube;
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.BirthDate;
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.CollaboratorIndustry;
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.Role;
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.Gender;
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.JobTitles;
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.MiniBio;
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.PastEditions;
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.HasAnySpecialNeeds;
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.WhichSpecialNeedsQ;
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.OnboardingStartDate;
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.OnboardingFinishDate;
                    worksheet.Cell(lineIndex, columnIndex += 1).Value = Labels.Photo;

                    skipFinalAdjustmentsColumnIndexes.Add(columnIndex);

                    #endregion

                    if (playerExecutiveReportDtos.Any())
                    {
                        #region Rows

                        foreach (var playerExecutiveReportDto in playerExecutiveReportDtos)
                        {
                            lineIndex++;
                            columnIndex = 0;

                            worksheet.Cell(lineIndex, columnIndex += 1).Value = playerExecutiveReportDto.AttendeeOrganizationBasesDtos?.Select(ao => ao.OrganizationBaseDto.Name)?.ToString(", ");
                            worksheet.Cell(lineIndex, columnIndex += 1).Value = playerExecutiveReportDto.AttendeeOrganizationBasesDtos?.Select(ao => ao.OrganizationBaseDto.TradeName)?.ToString(", ");
                            worksheet.Cell(lineIndex, columnIndex += 1).Value = playerExecutiveReportDto.FirstName;
                            worksheet.Cell(lineIndex, columnIndex += 1).Value = playerExecutiveReportDto.LastNames;
                            worksheet.Cell(lineIndex, columnIndex += 1).Value = playerExecutiveReportDto.Badge;
                            worksheet.Cell(lineIndex, columnIndex += 1).Value = playerExecutiveReportDto.CellPhone;
                            worksheet.Cell(lineIndex, columnIndex).Style.NumberFormat.Format = "00000";
                            worksheet.Cell(lineIndex, columnIndex += 1).Value = playerExecutiveReportDto.PhoneNumber;
                            worksheet.Cell(lineIndex, columnIndex).Style.NumberFormat.Format = "00000";
                            worksheet.Cell(lineIndex, columnIndex += 1).Value = playerExecutiveReportDto.Email;
                            worksheet.Cell(lineIndex, columnIndex += 1).Value = playerExecutiveReportDto.PublicEmail;
                            worksheet.Cell(lineIndex, columnIndex += 1).Value = playerExecutiveReportDto.Website;
                            worksheet.Cell(lineIndex, columnIndex += 1).Value = playerExecutiveReportDto.Linkedin;
                            worksheet.Cell(lineIndex, columnIndex += 1).Value = playerExecutiveReportDto.Twitter;
                            worksheet.Cell(lineIndex, columnIndex += 1).Value = playerExecutiveReportDto.Instagram;
                            worksheet.Cell(lineIndex, columnIndex += 1).Value = playerExecutiveReportDto.Youtube;
                            worksheet.Cell(lineIndex, columnIndex += 1).Value = playerExecutiveReportDto.BirthDate?.ToShortDateString();
                            worksheet.Cell(lineIndex, columnIndex += 1).Value = playerExecutiveReportDto.Industry?.GetTranslatedName(this.UserInterfaceLanguage);
                            worksheet.Cell(lineIndex, columnIndex += 1).Value = playerExecutiveReportDto.Role?.GetTranslatedName(this.UserInterfaceLanguage);
                            worksheet.Cell(lineIndex, columnIndex += 1).Value = playerExecutiveReportDto.Gender?.GetTranslatedName(this.UserInterfaceLanguage);
                            worksheet.Cell(lineIndex, columnIndex += 1).Value = playerExecutiveReportDto.GetJobTitleBaseDtoByLanguageCode(this.UserInterfaceLanguage)?.Value;
                            worksheet.Cell(lineIndex, columnIndex += 1).Value = playerExecutiveReportDto.GetMiniBioBaseDtoByLanguageCode(this.UserInterfaceLanguage)?.Value;
                            worksheet.Cell(lineIndex, columnIndex += 1).Value = playerExecutiveReportDto.EditionParticipationBaseDtos?.Select(cep => cep.EditionUrlCode.ToString())?.ToString(", ");
                            worksheet.Cell(lineIndex, columnIndex += 1).Value = playerExecutiveReportDto.HasAnySpecialNeeds?.ToYesOrNoString();
                            worksheet.Cell(lineIndex, columnIndex += 1).Value = playerExecutiveReportDto.SpecialNeedsDescription;
                            worksheet.Cell(lineIndex, columnIndex += 1).Value = playerExecutiveReportDto.OnboardingStartDate?.ToStringHourMinute();
                            worksheet.Cell(lineIndex, columnIndex += 1).Value = playerExecutiveReportDto.OnboardingFinishDate?.ToStringHourMinute();
                            worksheet.Cell(lineIndex, columnIndex += 1).Value = playerExecutiveReportDto.ImageUploadDate.HasValue ?
                                this.fileRepo.GetImageUrl(FileRepositoryPathType.UserImage, playerExecutiveReportDto.Uid, playerExecutiveReportDto.ImageUploadDate, true) : "";
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

        /// <summary>Shows the total count widget.</summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowTotalCountWidget()
        {
            var executivesCount = await this.collaboratorRepo.CountAllByDataTable(
                CollaboratorType.PlayerExecutiveMusic.Name,
                OrganizationType.MusicPlayer.Name,
                true,
                this.EditionDto.Id);

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/TotalCountWidget", executivesCount), divIdOrClass = "#PlayersExecutivesTotalCountWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>Shows the edition count widget.</summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowEditionCountWidget()
        {
            var executivesCount = await this.collaboratorRepo.CountAllByDataTable(
                CollaboratorType.PlayerExecutiveMusic.Name,
                OrganizationType.MusicPlayer.Name,
                false,
                this.EditionDto.Id);

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/EditionCountWidget", executivesCount), divIdOrClass = "#PlayersExecutivesEditionCountWidget" },
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
            var attendeeCollaboratorDto = await this.attendeeCollaboratorRepo.FindSiteDetailstDtoByCollaboratorUidAndByCollaboratorTypeUidAsync(
                id ?? Guid.Empty,
                CollaboratorType.PlayerExecutiveMusic.Uid,
                OrganizationType.MusicPlayer.Uid);

            if (attendeeCollaboratorDto == null)
            {
                this.StatusMessageToastr(string.Format(Messages.EntityNotAction, Labels.Executive, Labels.FoundM.ToLowerInvariant()), Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);
                return RedirectToAction("Index", "PlayersExecutives", new { Area = "Music" });
            }

            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.MusicCommission, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Labels.Music, null),
                new BreadcrumbItemHelper(Labels.Players, Url.Action("Index", "Players", new { Area = "Music" })),
                new BreadcrumbItemHelper(Labels.Executives, Url.Action("Index", "PlayersExecutives", new { Area = "Music" })),
                new BreadcrumbItemHelper(attendeeCollaboratorDto.Collaborator.GetFullName(), Url.Action("Details", "PlayersExecutives", new { Area = "Music", id }))
            });

            #endregion

            return View(attendeeCollaboratorDto);
        }

        #endregion

        #region Send Invitation Emails

        /// <summary>Sends the invitation emails.</summary>
        /// <param name="selectedCollaboratorsUids">The selected collaborators uids.</param>
        /// <returns></returns>
        [HttpPost]
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
                        //TODO: 866 - verify type SendPlayerWelcomeEmailAsync to change
                        result = await this.CommandBus.Send(new SendPlayerWelcomeEmailAsync(
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
                    catch (DomainException ex)
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

        #region Create

        /// <summary>Shows the create modal.</summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowCreateModal()
        {
            CreateCollaborator cmd = new CreateCollaborator(
                    //TODO: 866 - verify type CollaboratorType and  OrganizationType
                    await this.attendeeOrganizationRepo.FindAllBaseDtosByEditionUidAsync(this.EditionDto.Id, false, OrganizationType.MusicPlayer.Uid),
                    await this.CommandBus.Send(new FindAllLanguagesDtosAsync(this.UserInterfaceLanguage)),
                    await this.CommandBus.Send(new FindAllCollaboratorGenderAsync(this.UserInterfaceLanguage)),
                    await this.CommandBus.Send(new FindAllCollaboratorIndustryAsync(this.UserInterfaceLanguage)),
                    await this.CommandBus.Send(new FindAllCollaboratorRoleAsync(this.UserInterfaceLanguage)),
                    await this.CommandBus.Send(new FindAllEditionsDtosAsync(true)),
                    EditionDto.Id,
                    false,
                    false,
                    false,
                    UserInterfaceLanguage);

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Modals/CreateModal", cmd), divIdOrClass = "#GlobalModalContainer" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Delete

        /// <summary>Deletes the specified command.</summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        [HttpPost]
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
                    CollaboratorType.PlayerExecutiveMusic.Name,
                    OrganizationType.MusicPlayer.Name,
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

            return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Executive, Labels.DeletedM) });
        }

        #endregion

        #region Update

        /// <summary>Shows the update modal.</summary>
        /// <param name="collaboratorUid">The collaborator uid.</param>
        /// <param name="isAddingToCurrentEdition">The is adding to current edition.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowUpdateModal(Guid? collaboratorUid, bool? isAddingToCurrentEdition)
        {
            UpdateCollaborator cmd;

            try
            {
                cmd = new UpdateCollaborator(
                    await this.CommandBus.Send(new FindCollaboratorDtoByUidAndByEditionIdAsync(collaboratorUid, this.EditionDto.Id, this.UserInterfaceLanguage)),
                    //TODO: 866 - verify type CollaboratorType and  OrganizationType
                    await this.attendeeOrganizationRepo.FindAllBaseDtosByEditionUidAsync(this.EditionDto.Id, false, OrganizationType.MusicPlayer.Uid),
                    await this.CommandBus.Send(new FindAllLanguagesDtosAsync(this.UserInterfaceLanguage)),
                    await this.CommandBus.Send(new FindAllCountriesBaseDtosAsync(this.UserInterfaceLanguage)),
                    await this.CommandBus.Send(new FindAllCollaboratorGenderAsync(this.UserInterfaceLanguage)),
                    await this.CommandBus.Send(new FindAllCollaboratorIndustryAsync(this.UserInterfaceLanguage)),
                    await this.CommandBus.Send(new FindAllCollaboratorRoleAsync(this.UserInterfaceLanguage)),
                    await this.CommandBus.Send(new FindAllEditionsDtosAsync(true)),
                    EditionDto.Id,
                    isAddingToCurrentEdition,
                    false,
                    false,
                    false,
                    this.UserInterfaceLanguage);
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

        #endregion
    }
}