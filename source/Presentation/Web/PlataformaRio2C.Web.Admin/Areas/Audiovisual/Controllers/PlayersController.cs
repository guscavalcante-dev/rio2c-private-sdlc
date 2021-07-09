// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 08-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 07-09-2021
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

namespace PlataformaRio2C.Web.Admin.Areas.Audiovisual.Controllers
{
    /// <summary>PlayersController</summary>
    [AjaxAuthorize(Order = 1, Roles = Constants.Role.AnyAdmin)]
    [AuthorizeCollaboratorType(Order = 2, Types = Constants.CollaboratorType.AdminAudiovisual)]
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
                new BreadcrumbItemHelper(Labels.AudioVisual, null),
                new BreadcrumbItemHelper(Labels.Players, Url.Action("Index", "Players", new { Area = "Audiovisual" }))
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
            var producers = await this.organizationRepo.FindAllByDataTable(
                request.Start / request.Length,
                request.Length,
                request.Search?.Value,
                request.GetSortColumns(),
                OrganizationType.Player.Uid,
                showAllEditions,
                showAllOrganizations,
                this.EditionDto.Id);

            var response = DataTablesResponse.Create(request, producers.TotalItemCount, producers.TotalItemCount, producers);

            return Json(new
            {
                status = "success",
                dataTable = response
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Total Count Widget

        /// <summary>Shows the total count widget.</summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowTotalCountWidget()
        {
            var playersCount = await this.organizationRepo.CountAllByDataTable(OrganizationType.Player.Uid, true, this.EditionDto.Id);

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/TotalCountWidget", playersCount), divIdOrClass = "#PlayersTotalCountWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Edition Count Widget

        /// <summary>Shows the edition count widget.</summary>
        /// <returns></returns>
        public async Task<ActionResult> ShowEditionCountWidget()
        {
            var playersCount = await this.organizationRepo.CountAllByDataTable(OrganizationType.Player.Uid, false, this.EditionDto.Id);

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/EditionCountWidget", playersCount), divIdOrClass = "#PlayersEditionCountWidget" },
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
            var attendeeOrganizationDto = await this.attendeeOrganizationRepo.FindDetailsDtoByOrganizationUidAndByEditionIdAsync(id ?? Guid.Empty, this.EditionDto.Id, true);
            if (attendeeOrganizationDto == null)
            {
                this.StatusMessageToastr(string.Format(Messages.EntityNotAction, Labels.Company, Labels.FoundM.ToLowerInvariant()), Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);
                return RedirectToAction("Index", "Players", new { Area = "Audiovisual" });
            }

            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.Players, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Labels.AudioVisual, null),
                new BreadcrumbItemHelper(Labels.Players, Url.Action("Index", "Players", new { Area = "Audiovisual" })),
                new BreadcrumbItemHelper(attendeeOrganizationDto.Organization.Name, Url.Action("Details", "Players", new { Area = "Audiovisual", id }))
            });

            #endregion

            ViewBag.OrganizationTypeUid = OrganizationType.Player.Uid; // It's the admin page accessed and not the organization type of the current organization

            return View("../Organizations/Details", attendeeOrganizationDto);
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
                OrganizationType.Player,
                await this.CommandBus.Send(new FindAllHoldingsBaseDtosAsync(null, this.UserInterfaceLanguage)),
                await this.CommandBus.Send(new FindAllLanguagesDtosAsync(this.UserInterfaceLanguage)),
                await this.CommandBus.Send(new FindAllCountriesBaseDtosAsync(this.UserInterfaceLanguage)),
                await this.activityRepo.FindAllAsync(),
                await this.targetAudienceRepo.FindAllByProjectTypeIdAsync(ProjectType.Audiovisual.Id),
                await this.interestRepo.FindAllDtosAsync(),
                false,
                false,
                false,
                false,
                true);

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
                    OrganizationType.Player,
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
                    await this.activityRepo.FindAllAsync(),
                    await this.targetAudienceRepo.FindAllByProjectTypeIdAsync(ProjectType.Audiovisual.Id));

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
                    OrganizationType.Player,
                    await this.CommandBus.Send(new FindAllHoldingsBaseDtosAsync(null, this.UserInterfaceLanguage)),
                    await this.CommandBus.Send(new FindAllLanguagesDtosAsync(this.UserInterfaceLanguage)),
                    await this.CommandBus.Send(new FindAllCountriesBaseDtosAsync(this.UserInterfaceLanguage)),
                    await this.activityRepo.FindAllAsync(),
                    await this.targetAudienceRepo.FindAllByProjectTypeIdAsync(ProjectType.Audiovisual.Id),
                    await this.interestRepo.FindAllDtosAsync(),
                    isAddingToCurrentEdition,
                    false,
                    false,
                    false,
                    false,
                    true);
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
                    OrganizationType.Player,
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
                    await this.activityRepo.FindAllAsync(),
                    await this.targetAudienceRepo.FindAllByProjectTypeIdAsync(ProjectType.Audiovisual.Id));

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
                    OrganizationType.Player,
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
                OrganizationType.Player.Uid,
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
                    TradeName = c.TradeName,
                    CompanyName = c.CompanyName,
                    Picture = c.ImageUploadDate.HasValue ? this.fileRepo.GetImageUrl(FileRepositoryPathType.UserImage, c.Uid, c.ImageUploadDate, true) : null
                })?.ToList()
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}