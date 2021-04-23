// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 12-12-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-11-2020
// ***********************************************************************
// <copyright file="SpeakersController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
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
using PlataformaRio2C.Application.ViewModels;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Statics;
using PlataformaRio2C.Infra.CrossCutting.Identity.AuthorizeAttributes;
using PlataformaRio2C.Infra.CrossCutting.Identity.Service;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using PlataformaRio2C.Infra.CrossCutting.Tools.Helpers;
using Constants = PlataformaRio2C.Domain.Constants;

namespace PlataformaRio2C.Web.Admin.Controllers
{
    /// <summary>SpeakersController</summary>
    [AjaxAuthorize(Order = 1, Roles = Constants.Role.AnyAdmin)]
    [RoutePrefix("{culture}/{edition}")]
    public class CollaboratorsController : BaseController
    {
        private readonly ICollaboratorRepository collaboratorRepo;
        private readonly IAttendeeCollaboratorRepository attendeeCollaboratorRepo;
        private readonly IAttendeeOrganizationRepository attendeeOrganizationRepo;
        private readonly IFileRepository fileRepo;

        /// <summary>Initializes a new instance of the <see cref="SpeakersController"/> class.</summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="identityController">The identity controller.</param>
        /// <param name="attendeeCollaboratorRepository">The attendee collaborator repository.</param>
        /// <param name="fileRepository">The file repository.</param>
        public CollaboratorsController(
            IMediator commandBus, 
            IdentityAutenticationService identityController,
            ICollaboratorRepository collaboratorRepository,
            IAttendeeCollaboratorRepository attendeeCollaboratorRepository,
            IAttendeeOrganizationRepository attendeeOrganizationRepository,
            IFileRepository fileRepository)
            : base(commandBus, identityController)
        {
            this.collaboratorRepo = collaboratorRepository;
            this.attendeeCollaboratorRepo = attendeeCollaboratorRepository;
            this.attendeeOrganizationRepo = attendeeOrganizationRepository;
            this.fileRepo = fileRepository;
        }

        #region Collaborators

        /// <summary>Indexes the specified search view model.</summary>
        /// <param name="searchViewModel">The search view model.</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index(CollaboratorSearchViewModel searchViewModel)
        {
            return RedirectToAction(nameof(ManagersIndex));
            //#region Breadcrumb

            //ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.Editions, new List<BreadcrumbItemHelper> {
            //    new BreadcrumbItemHelper(Labels.Managers, Url.Action("Index", "Collaborators", new { Area = "" }))
            //});

            //#endregion

            //return View(searchViewModel);
        }

        #region List

        /// <summary>Finds all by filters.</summary>
        /// <param name="keywords">The keywords.</param>
        /// <param name="page">The page.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> FindAllByFilters(string keywords, int? page = 1)
        {
            var collaboratorsApiDtos = await this.attendeeCollaboratorRepo.FindAllDropdownApiListDtoPaged(
                this.EditionDto.Id,
                keywords,
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
                    Picture = c.ImageUploadDate.HasValue ? this.fileRepo.GetImageUrl(FileRepositoryPathType.UserImage, c.CollaboratorUid, c.ImageUploadDate, true) : null,
                    JobTitle = c.GetCollaboratorJobTitleBaseDtoByLanguageCode(this.UserInterfaceLanguage)?.Value?.Trim(),
                    Companies = c.OrganizationsDtos?.Select(od => new CollaboratorsDropdownOrganizationDto()
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

        #endregion

        #region Managers

        #region List

        /// <summary>Indexes the specified search view model.</summary>
        /// <param name="searchViewModel">The search view model.</param>
        /// <returns></returns>
        [HttpGet, Route("Collaborators/Managers")]
        public ActionResult ManagersIndex(ManagerSearchViewModel searchViewModel)
        {
            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.Managers, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Labels.Managers, Url.Action("Index", "Collaborators/Managers", new { Area = "" }))
            });

            #endregion

            return View(searchViewModel);
        }

        #region DataTable Widget

        /// <summary>Searches the specified request.</summary>
        /// <param name="request">The request.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <param name="showAllExecutives">if set to <c>true</c> [show all executives].</param>
        /// <param name="showAllParticipants">if set to <c>true</c> [show all participants].</param>
        /// <param name="showHighlights">The show highlights.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("Collaborators/Managers/Search")]
        public async Task<ActionResult> Search(IDataTablesRequest request, bool showAllEditions, string collaboratorType = "")
        {
            string[] collaboratorTypes = string.IsNullOrEmpty(collaboratorType) ? Constants.CollaboratorType.Admins : new string[] { collaboratorType };

            var playersExecutives = await this.collaboratorRepo.FindAllByDataTable(
                request.Start / request.Length,
                request.Length,
                request.Search?.Value,
                request.GetSortColumns(),
                new List<Guid>(),
                collaboratorTypes,
                showAllEditions,
                false,
                false,
                false,
                this.EditionDto?.Id);

            var response = DataTablesResponse.Create(request, playersExecutives.TotalItemCount, playersExecutives.TotalItemCount, playersExecutives);

            ViewBag.CollaboratorType = collaboratorType;

            return Json(new
            {
                status = "success",
                dataTable = response
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #endregion

        #region Create

        /// <summary>Shows the create modal.</summary>
        /// <returns></returns>
        [HttpGet, Route("Collaborators/Managers/ShowCreateModal")]
        public async Task<ActionResult> ShowCreateModal()
        {
            var cmd = new CreateCollaborator(
                await this.attendeeOrganizationRepo.FindAllBaseDtosByEditionUidAsync(this.EditionDto.Id, false, OrganizationType.Player.Uid),
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
                    new { page = this.RenderRazorViewToString("Managers/Modals/CreateModal", cmd), divIdOrClass = "#GlobalModalContainer" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>Creates the specified command.</summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        [HttpPost, Route("Collaborators/Managers/Create")]
        public async Task<ActionResult> Create(CreateCollaborator cmd)
        {
            var result = new AppValidationResult();

            try
            {
                if (!ModelState.IsValid)
                {
                    throw new DomainException(Messages.CorrectFormValues);
                }

                cmd.UpdatePreSendProperties(
                    Domain.Constants.CollaboratorType.ExecutiveAudiovisual,
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
                    await this.attendeeOrganizationRepo.FindAllBaseDtosByEditionUidAsync(this.EditionDto.Id, false, OrganizationType.Player.Uid),
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
        [HttpGet, Route("Collaborators/Managers/ShowUpdateModal")]
        public async Task<ActionResult> ShowUpdateModal(Guid? collaboratorUid, bool? isAddingToCurrentEdition)
        {
            UpdateCollaborator cmd;

            try
            {
                cmd = new UpdateCollaborator(
                    await this.CommandBus.Send(new FindCollaboratorDtoByUidAndByEditionIdAsync(collaboratorUid, this.EditionDto.Id, this.UserInterfaceLanguage)),
                    await this.attendeeOrganizationRepo.FindAllBaseDtosByEditionUidAsync(this.EditionDto.Id, false, Domain.Entities.OrganizationType.Player.Uid),
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
                    UserInterfaceLanguage);
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
                    new { page = this.RenderRazorViewToString("Managers/Modals/UpdateModal", cmd), divIdOrClass = "#GlobalModalContainer" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>Updates the specified command.</summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        [HttpPost, Route("Collaborators/Managers/Update")]
        public async Task<ActionResult> Update(UpdateCollaborator cmd)
        {
            var result = new AppValidationResult();

            try
            {
                if (!ModelState.IsValid)
                {
                    throw new DomainException(Messages.CorrectFormValues);
                }

                cmd.UpdatePreSendProperties(
                    cmd.CollaboratorTypeName,
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
                    await this.attendeeOrganizationRepo.FindAllBaseDtosByEditionUidAsync(this.EditionDto.Id, false, OrganizationType.Player.Uid),
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
        [HttpPost, Route("Collaborators/Managers/Delete")]
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
                    cmd.CollaboratorTypeName,
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

        #endregion
    }
}