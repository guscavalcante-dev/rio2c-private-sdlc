// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 12-12-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 08-31-2021
// ***********************************************************************
// <copyright file="CollaboratorsController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
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
    /// <summary>CollaboratorsController</summary>
    [AjaxAuthorize(Order = 1, Roles = Constants.Role.AnyAdmin)]
    [RoutePrefix("{culture}/{edition}")]
    public class CollaboratorsController : BaseController
    {
        private readonly IAttendeeCollaboratorRepository attendeeCollaboratorRepo;
        private readonly IFileRepository fileRepo;
        private readonly ICollaboratorTypeRepository collaboratorTypeRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="CollaboratorsController"/> class.
        /// </summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="identityController">The identity controller.</param>
        /// <param name="attendeeCollaboratorRepository">The attendee collaborator repository.</param>
        /// <param name="fileRepository">The file repository.</param>
        /// <param name="collaboratorTypeRepository">The collaborator type repository.</param>
        public CollaboratorsController(
            IMediator commandBus,
            IdentityAutenticationService identityController,
            IAttendeeCollaboratorRepository attendeeCollaboratorRepository,
            IFileRepository fileRepository,
            ICollaboratorTypeRepository collaboratorTypeRepository)
            : base(commandBus, identityController)
        {
            this.attendeeCollaboratorRepo = attendeeCollaboratorRepository;
            this.fileRepo = fileRepository;
            this.collaboratorTypeRepo = collaboratorTypeRepository;
        }

        /// <summary>Indexes the specified search view model.</summary>
        /// <param name="searchViewModel">The search view model.</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index(CollaboratorSearchViewModel searchViewModel)
        {
            //return RedirectToAction(nameof(ManagersIndex));

            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.Editions, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Labels.Managers, Url.Action("Index", "Collaborators", new { Area = "" }))
            });

            #endregion

            return View(searchViewModel);
        }

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

        #region Main Information Widget (Admin)

        /// <summary>
        /// Shows the main information widget.
        /// </summary>
        /// <param name="collaboratorUid">The collaborator uid.</param>
        /// <param name="collaboratorType">Type of the collaborator.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowMainInformationWidget(Guid? collaboratorUid, string collaboratorType)
        {
            var mainInformationWidgetDto = await this.attendeeCollaboratorRepo.FindSiteMainInformationWidgetDtoByCollaboratorUidAndByEditionIdAsync(collaboratorUid ?? Guid.Empty, this.EditionDto.Id);
            if (mainInformationWidgetDto == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Member, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            ViewBag.CollaboratorType = collaboratorType;

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/MainInformationWidget", mainInformationWidgetDto), divIdOrClass = "#MainInformationWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #region Update

        /// <summary>Shows the update main information modal.</summary>
        /// <param name="collaboratorUid">The collaborator uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowUpdateMainInformationModal(Guid? collaboratorUid, string collaboratorType)
        {
            UpdateCollaboratorAdminMainInformation cmd;

            try
            {
                var mainInformationWidgetDto = await this.attendeeCollaboratorRepo.FindSiteMainInformationWidgetDtoByCollaboratorUidAndByEditionIdAsync(collaboratorUid ?? Guid.Empty, this.EditionDto.Id);
                if (mainInformationWidgetDto == null)
                {
                    throw new DomainException(string.Format(Messages.EntityNotAction, Labels.Member, Labels.FoundM.ToLowerInvariant()));
                }

                cmd = new UpdateCollaboratorAdminMainInformation(
                    mainInformationWidgetDto,
                    collaboratorType,
                    await this.CommandBus.Send(new FindAllCollaboratorGenderAsync(this.UserInterfaceLanguage)),
                    await this.CommandBus.Send(new FindAllCollaboratorIndustryAsync(this.UserInterfaceLanguage)),
                    await this.CommandBus.Send(new FindAllCollaboratorRoleAsync(this.UserInterfaceLanguage)),
                    await this.CommandBus.Send(new FindAllLanguagesDtosAsync(this.UserInterfaceLanguage)),
                    await this.CommandBus.Send(new FindAllEditionsDtosAsync(true)),
                    EditionDto.Id,
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
                    new { page = this.RenderRazorViewToString("Modals/UpdateMainInformationModal", cmd), divIdOrClass = "#GlobalModalContainer" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>Updates the main information.</summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> UpdateMainInformation(UpdateCollaboratorAdminMainInformation cmd)
        {
            var result = new AppValidationResult();

            try
            {
                // Commission members does not have public email
                if (ModelState.ContainsKey("SharePublicEmail"))
                {
                    ModelState["SharePublicEmail"].Errors.Clear();
                }

                if (!ModelState.IsValid)
                {
                    throw new DomainException(Messages.CorrectFormValues);
                }

                cmd.UpdatePreSendProperties(
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

                cmd.UpdateModelsAndLists(
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
                        new { page = this.RenderRazorViewToString("Modals/UpdateMainInformationForm", cmd), divIdOrClass = "#form-container" },
                    }
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return Json(new { status = "error", message = Messages.WeFoundAndError, }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Member, Labels.UpdatedM) });
        }

        #endregion

        #endregion

        #region Social Networks Widget

        /// <summary>
        /// Shows the social networks widget.
        /// </summary>
        /// <param name="collaboratorUid">The collaborator uid.</param>
        /// <param name="collaboratorType">Type of the collaborator.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowSocialNetworksWidget(Guid? collaboratorUid, string collaboratorType)
        {
            var socialNetworksWidgetDto = await this.attendeeCollaboratorRepo.FindSiteDetailstDtoByCollaboratorUidAndByEditionIdAsync(collaboratorUid ?? Guid.Empty, this.EditionDto.Id);
            if (socialNetworksWidgetDto == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Member, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            ViewBag.CollaboratorType = collaboratorType;

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/SocialNetworksWidget", socialNetworksWidgetDto), divIdOrClass = "#SocialNetworksWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #region Update

        /// <summary>Shows the update social networks modal.</summary>
        /// <param name="collaboratorUid">The collaborator uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowUpdateSocialNetworksModal(Guid? collaboratorUid, string collaboratorType)
        {
            UpdateCollaboratorSocialNetworks cmd;

            try
            {
                var socialNetworksWidgetDto = await this.attendeeCollaboratorRepo.FindSiteDetailstDtoByCollaboratorUidAndByEditionIdAsync(collaboratorUid ?? Guid.Empty, this.EditionDto.Id);
                if (socialNetworksWidgetDto == null)
                {
                    throw new DomainException(string.Format(Messages.EntityNotAction, Labels.Member, Labels.FoundM.ToLowerInvariant()));
                }

                cmd = new UpdateCollaboratorSocialNetworks(socialNetworksWidgetDto, collaboratorType);
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
                    new { page = this.RenderRazorViewToString("Modals/UpdateSocialNetworksModal", cmd), divIdOrClass = "#GlobalModalContainer" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>Updates the social networks.</summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> UpdateSocialNetworks(UpdateCollaboratorSocialNetworks cmd)
        {
            var result = new AppValidationResult();

            try
            {
                if (!ModelState.IsValid)
                {
                    throw new DomainException(Messages.CorrectFormValues);
                }

                cmd.UpdatePreSendProperties(
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
                        new { page = this.RenderRazorViewToString("/Views/Shared/Collaborators/Forms/_SocialNetworksForm.cshtml", cmd), divIdOrClass = "#form-container" },
                    }
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return Json(new { status = "error", message = Messages.WeFoundAndError, }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Member, Labels.UpdatedM) });
        }

        #endregion

        #endregion

        #region Onboarding Info Widget

        /// <summary>Shows the onboarding information widget.</summary>
        /// <param name="collaboratorUid">The collaborator uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowOnboardingInfoWidget(Guid? collaboratorUid, string collaboratorTypeName)
        {
            var collaboratorType = await collaboratorTypeRepo.FindByNameAsync(collaboratorTypeName);
            if(collaboratorType == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.CollaboratorType, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            var onboardingInfoWidgetDto = await this.attendeeCollaboratorRepo.FindOnboardingInfoWidgetDtoAsync(collaboratorUid ?? Guid.Empty, collaboratorType.Uid, this.EditionDto.Id);
            if (onboardingInfoWidgetDto == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Member, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            ViewBag.CollaboratorTypeName = collaboratorTypeName;

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/OnboardingInfoWidget", onboardingInfoWidgetDto), divIdOrClass = "#OnboardingInfoWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Company Widget

        /// <summary>Shows the company widget.</summary>
        /// <param name="collaboratorUid">The collaborator uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowCompanyWidget(Guid? collaboratorUid)
        {
            var companyWidgetDto = await this.attendeeCollaboratorRepo.FindSiteCompanyWidgetDtoByCollaboratorUidAndByEditionIdAsync(collaboratorUid ?? Guid.Empty, this.EditionDto.Id);
            if (companyWidgetDto == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Executive, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/CompanyWidget", companyWidgetDto), divIdOrClass = "#CollaboratorCompanyWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #region Update

        /// <summary>Shows the update company information modal.</summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowUpdateCompanyInfoModal(Guid? collaboratorUid, Guid? organizationUid)
        {
            CreateTicketBuyerOrganizationData cmd;

            try
            {
                if (!collaboratorUid.HasValue)
                {
                    throw new DomainException(string.Format(Messages.EntityNotAction, Labels.Speaker, Labels.FoundM.ToLowerInvariant()));
                }

                cmd = new CreateTicketBuyerOrganizationData(
                    collaboratorUid.Value,
                    organizationUid.HasValue ? await this.CommandBus.Send(new FindOrganizationDtoByUidAsync(organizationUid, this.EditionDto.Id, this.UserInterfaceLanguage)) : null,
                    await this.CommandBus.Send(new FindAllLanguagesDtosAsync(this.UserInterfaceLanguage)),
                    await this.CommandBus.Send(new FindAllCountriesBaseDtosAsync(this.UserInterfaceLanguage)),
                    false,
                    false,
                    false);
            }
            catch (DomainException ex)
            {
                return Json(new { status = "error", message = ex.GetInnerMessage() }, JsonRequestBehavior.AllowGet);
            }

            ModelState.Clear();

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Modals/UpdateCompanyInfoModal", cmd), divIdOrClass = "#GlobalModalContainer" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>Updates the company information.</summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> UpdateCompanyInformation(CreateTicketBuyerOrganizationData cmd)
        {
            var result = new AppValidationResult();

            try
            {
                if (!ModelState.IsValid)
                {
                    throw new DomainException(Messages.CorrectFormValues);
                }

                cmd.UpdatePreSendProperties(
                    cmd.CollaboratorUid,
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
                    await this.CommandBus.Send(new FindAllCountriesBaseDtosAsync(this.UserInterfaceLanguage)));

                return Json(new
                {
                    status = "error",
                    message = result.Errors?.FirstOrDefault(e => e.Target == "ToastrError")?.Message ?? ex.GetInnerMessage(),
                    pages = new List<dynamic>
                    {
                        new { page = this.RenderRazorViewToString("/Views/Companies/Shared/_TicketBuyerCompanyInfoForm.cshtml", cmd), divIdOrClass = "#form-container" },
                    }
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return Json(new { status = "error", message = Messages.WeFoundAndError, }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Company, Labels.UpdatedF) });
        }

        #endregion

        #region Delete

        /// <summary>Deletes the organization.</summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> DeleteOrganization(DeleteCollaboratorOrganization cmd)
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

        #endregion
    }
}