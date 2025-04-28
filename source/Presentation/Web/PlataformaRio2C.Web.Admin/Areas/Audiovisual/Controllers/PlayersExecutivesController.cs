// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 08-26-2019
//
// Last Modified By : Daniel Giese Rodrigues
// Last Modified On : 04-15-2025
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

namespace PlataformaRio2C.Web.Admin.Areas.Audiovisual.Controllers
{
    /// <summary>
    /// PlayersExecutivesController
    /// </summary>
    [AjaxAuthorize(Order = 1, Roles = Constants.Role.AnyAdmin)]
    [AuthorizeCollaboratorType(Order = 2, Types = Constants.CollaboratorType.AdminAudiovisual)]
    public class PlayersExecutivesController : BaseController
    {
        private readonly ICollaboratorRepository collaboratorRepo;
        private readonly IAttendeeOrganizationRepository attendeeOrganizationRepo;
        private readonly IAttendeeCollaboratorRepository attendeeCollaboratorRepo;
        private readonly IFileRepository fileRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayersExecutivesController" /> class.
        /// </summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="identityController">The identity controller.</param>
        /// <param name="collaboratorRepository">The collaborator repository.</param>
        /// <param name="attendeeOrganizationRepository">The attendee organization repository.</param>
        /// <param name="attendeeCollaboratorRepository">The attendee collaborator repository.</param>
        /// <param name="fileRepository">The file repository.</param>
        public PlayersExecutivesController(
            IMediator commandBus,
            IdentityAutenticationService identityController,
            ICollaboratorRepository collaboratorRepository,
            IAttendeeOrganizationRepository attendeeOrganizationRepository,
            IAttendeeCollaboratorRepository attendeeCollaboratorRepository,
            IFileRepository fileRepository
            )
            : base(commandBus, identityController)
        {
            this.collaboratorRepo = collaboratorRepository;
            this.attendeeOrganizationRepo = attendeeOrganizationRepository;
            this.attendeeCollaboratorRepo = attendeeCollaboratorRepository;
            this.fileRepo = fileRepository;
        }

        #region List

        /// <summary>Indexes the specified search view model.</summary>
        /// <param name="searchViewModel">The search view model.</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index(CollaboratorSearchViewModel searchViewModel)
        {
            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.Players, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Labels.AudioVisual, null),
                new BreadcrumbItemHelper(Labels.Players, Url.Action("Index", "Players", new { Area = "Audiovisual" })),
                new BreadcrumbItemHelper(Labels.Executives, Url.Action("Index", "PlayersExecutives", new { Area = "Audiovisual" }))
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
                new string[] { CollaboratorType.PlayerExecutiveAudiovisual.Name },
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
                    this.EditionDto?.Id,
                    CollaboratorType.PlayerExecutiveAudiovisual.Name
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
               CollaboratorType.PlayerExecutiveAudiovisual.Uid
           );

            if (attendeeCollaboratorDto == null)
            {
                this.StatusMessageToastr(string.Format(Messages.EntityNotAction, Labels.Executive, Labels.FoundM.ToLowerInvariant()), Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);
                return RedirectToAction("Index", "PlayersExecutives", new { Area = "Audiovisual" });
            }

            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.PlayersExecutives, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Labels.AudioVisual, null),
                new BreadcrumbItemHelper(Labels.Players, Url.Action("Index", "Players", new { Area = "Audiovisual" })),
                new BreadcrumbItemHelper(Labels.Executives, Url.Action("Index", "PlayersExecutives", new { Area = "Audiovisual" })),
                new BreadcrumbItemHelper(attendeeCollaboratorDto.Collaborator.GetFullName(), Url.Action("Details", "PlayersExecutives", new { Area = "Audiovisual", id }))
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
                        result = await this.CommandBus.Send(new SendPlayerWelcomeEmailAsync(
                            collaboratorDto.Collaborator.Uid,
                            CollaboratorType.PlayerExecutiveAudiovisual.Uid,
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

        #region Total Count Widget

        /// <summary>Shows the total count widget.</summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowTotalCountWidget()
        {
            var executivesCount = await this.collaboratorRepo.CountAllByDataTable(
                CollaboratorType.PlayerExecutiveAudiovisual.Name,
                OrganizationType.AudiovisualPlayer.Name,
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

        #endregion

        #region Edition Count Widget

        /// <summary>Shows the edition count widget.</summary>
        /// <returns></returns>
        public async Task<ActionResult> ShowEditionCountWidget()
        {
            var executivesCount = await this.collaboratorRepo.CountAllByDataTable(
                CollaboratorType.PlayerExecutiveAudiovisual.Name,
                OrganizationType.AudiovisualPlayer.Name,
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

        #region Create

        /// <summary>Shows the create modal.</summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowCreateModal()
        {
            CreateAudiovisualPlayerExecutiveCollaborator cmd = new CreateAudiovisualPlayerExecutiveCollaborator(
                    await this.attendeeOrganizationRepo.FindAllBaseDtosByEditionUidAsync(this.EditionDto.Id, false, OrganizationType.AudiovisualPlayer.Uid),
                    await this.CommandBus.Send(new FindAllLanguagesDtosAsync(this.UserInterfaceLanguage)),
                    await this.CommandBus.Send(new FindAllCollaboratorGenderAsync(this.UserInterfaceLanguage)),
                    await this.CommandBus.Send(new FindAllCollaboratorIndustryAsync(this.UserInterfaceLanguage)),
                    await this.CommandBus.Send(new FindAllCollaboratorRoleAsync(this.UserInterfaceLanguage)),
                    await this.CommandBus.Send(new FindAllEditionsDtosAsync(true)),
                    EditionDto.Id,
                    false,
                    false,
                    false,
                    true,
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

        /// <summary>Creates the specified command.</summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Create(CreateAudiovisualPlayerExecutiveCollaborator cmd)
        {
            var result = new AppValidationResult();

            try
            {
                if (!ModelState.IsValid)
                {
                    throw new DomainException(Messages.CorrectFormValues);
                }

                cmd.UpdatePreSendProperties(
                    Domain.Constants.CollaboratorType.PlayerExecutiveAudiovisual,
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
                    await this.attendeeOrganizationRepo.FindAllBaseDtosByEditionUidAsync(this.EditionDto.Id, false, OrganizationType.AudiovisualPlayer.Uid),
                    await this.CommandBus.Send(new FindAllCollaboratorGenderAsync(this.UserInterfaceLanguage)),
                    await this.CommandBus.Send(new FindAllCollaboratorIndustryAsync(this.UserInterfaceLanguage)),
                    await this.CommandBus.Send(new FindAllCollaboratorRoleAsync(this.UserInterfaceLanguage)),
                    await this.CommandBus.Send(new FindAllEditionsDtosAsync(true)),
                    EditionDto.Id,
                    UserInterfaceLanguage);

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

            return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Executive, Labels.CreatedM) });
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
            UpdateAudiovisualPlayerExecutiveCollaborator cmd;

            try
            {
                cmd = new UpdateAudiovisualPlayerExecutiveCollaborator(
                    await this.CommandBus.Send(new FindCollaboratorDtoByUidAndByEditionIdAsync(collaboratorUid, this.EditionDto.Id, this.UserInterfaceLanguage)),
                    await this.attendeeOrganizationRepo.FindAllBaseDtosByEditionUidAsync(this.EditionDto.Id, false, OrganizationType.AudiovisualPlayer.Uid),
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
                    true,
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

        /// <summary>Updates the specified command.</summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Update(UpdateAudiovisualPlayerExecutiveCollaborator cmd)
        {
            var result = new AppValidationResult();

            try
            {
                if (!ModelState.IsValid)
                {
                    throw new DomainException(Messages.CorrectFormValues);
                }

                cmd.UpdatePreSendProperties(
                    Domain.Constants.CollaboratorType.PlayerExecutiveAudiovisual,
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
                    await this.CommandBus.Send(new FindCollaboratorDtoByUidAndByEditionIdAsync(cmd.CollaboratorUid, this.EditionDto.Id, this.UserInterfaceLanguage)),
                    await this.attendeeOrganizationRepo.FindAllBaseDtosByEditionUidAsync(this.EditionDto.Id, false, OrganizationType.AudiovisualPlayer.Uid),
                    await this.CommandBus.Send(new FindAllCollaboratorGenderAsync(this.UserInterfaceLanguage)),
                    await this.CommandBus.Send(new FindAllCollaboratorIndustryAsync(this.UserInterfaceLanguage)),
                    await this.CommandBus.Send(new FindAllCollaboratorRoleAsync(this.UserInterfaceLanguage)),
                    await this.CommandBus.Send(new FindAllEditionsDtosAsync(true)),
                    EditionDto.Id,
                    UserInterfaceLanguage);

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

            return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Executive, Labels.UpdatedM) });
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
                    CollaboratorType.PlayerExecutiveAudiovisual.Name,
                    OrganizationType.AudiovisualPlayer.Name,
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

        #region Finds

        /// <summary>Finds all by filters.</summary>
        /// <param name="keywords">The keywords.</param>
        /// <param name="filterByProjectsInNegotiation">The filter by projects in negotiation.</param>
        /// <param name="page">The page.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> FindAllByFilters(string keywords, bool? filterByProjectsInNegotiation = false, int? page = 1)
        {
            var collaboratorsApiDtos = await this.collaboratorRepo.FindAllDropdownApiListDtoPaged(
                this.EditionDto.Id,
                keywords,
                filterByProjectsInNegotiation.Value,
                Constants.CollaboratorType.PlayerExecutiveAudiovisual,
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
                    Picture = c.ImageUploadDate.HasValue ? this.fileRepo.GetImageUrl(FileRepositoryPathType.UserImage, c.Uid, c.ImageUploadDate, true) : null,
                    JobTitle = c.GetCollaboratorJobTitleBaseDtoByLanguageCode(this.UserInterfaceLanguage)?.Value?.Trim(),
                    Companies = c.OrganizationsDtos?.Select(od => new CollaboratorsDropdownOrganizationDto
                    {
                        Uid = od.Uid,
                        TradeName = od.TradeName,
                        CompanyName = od.CompanyName,
                        Picture = od.ImageUploadDate.HasValue ? this.fileRepo.GetImageUrl(FileRepositoryPathType.OrganizationImage, od.Uid, od.ImageUploadDate, true) : null
                    })?.ToList()
                })?.ToList()
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}