// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Renan Valentim
// Created          : 04-24-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 04-24-2021
// ***********************************************************************
// <copyright file="ManagersController.cs" company="Softo">
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
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
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
using PlataformaRio2C.Infra.CrossCutting.Identity.ViewModels;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using PlataformaRio2C.Infra.CrossCutting.Tools.Helpers;
using Constants = PlataformaRio2C.Domain.Constants;

namespace PlataformaRio2C.Web.Admin.Controllers
{
    /// <summary>ManagersController</summary>
    [AjaxAuthorize(Order = 1, Roles = Constants.Role.AnyAdmin)]
    [RoutePrefix("{culture}/{edition}/Managers")]
    public class ManagersController : BaseController
    {
        private readonly IdentityAutenticationService _identityController;
        private readonly ICollaboratorRepository collaboratorRepo;
        private readonly IAttendeeCollaboratorRepository attendeeCollaboratorRepo;
        private readonly IAttendeeOrganizationRepository attendeeOrganizationRepo;
        private readonly IRoleRepository roleRepo;
        private readonly ICollaboratorTypeRepository collaboratorTypeRepo;
        private readonly IFileRepository fileRepo;

        /// <summary>Initializes a new instance of the <see cref="ManagersController"/> class.</summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="identityController">The identity controller.</param>
        /// <param name="attendeeCollaboratorRepository">The attendee collaborator repository.</param>
        /// <param name="fileRepository">The file repository.</param>
        public ManagersController(
            IMediator commandBus,            
            IdentityAutenticationService identityController,
            ICollaboratorRepository collaboratorRepository,
            IAttendeeCollaboratorRepository attendeeCollaboratorRepository,
            IAttendeeOrganizationRepository attendeeOrganizationRepository,
            IRoleRepository roleRepository,
            ICollaboratorTypeRepository collaboratorTypeRepository,
            IFileRepository fileRepository)
            : base(commandBus, identityController)
        {
            _identityController = identityController;
            this.collaboratorRepo = collaboratorRepository;
            this.attendeeCollaboratorRepo = attendeeCollaboratorRepository;
            this.attendeeOrganizationRepo = attendeeOrganizationRepository;
            this.roleRepo = roleRepository;
            this.collaboratorTypeRepo = collaboratorTypeRepository;
            this.fileRepo = fileRepository;
        }

        #region List

        /// <summary>Indexes the specified search view model.</summary>
        /// <param name="searchViewModel">The search view model.</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index(ManagerSearchViewModel searchViewModel)
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
        public async Task<ActionResult> Search(IDataTablesRequest request, bool showAllEditions, string collaboratorType = "", string roleName = "")
        {
            //Show only Admin Users
            string[] collaboratorTypes = string.IsNullOrEmpty(collaboratorType) ? Constants.CollaboratorType.Admins : new string[] { collaboratorType };
            string[] rolesNames = string.IsNullOrEmpty(roleName) ? Constants.Role.AnyAdminArray : new string[] { roleName };

            var playersExecutives = await this.collaboratorRepo.FindAllAminsByDataTable(
                request.Start / request.Length,
                request.Length,
                request.Search?.Value,
                request.GetSortColumns(),
                new List<Guid>(),
                collaboratorTypes,// new string[] { collaboratorType }, // //
                rolesNames,//new string[] { roleName }, // //
                showAllEditions,
                false,
                this.EditionDto?.Id);

            var response = DataTablesResponse.Create(request, playersExecutives.TotalItemCount, playersExecutives.TotalItemCount, playersExecutives);

            ViewBag.CollaboratorType = collaboratorType;
            ViewBag.Role = roleName;

            return Json(new
            {
                status = "success",
                dataTable = response
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #endregion

        #region Details

        /// <summary>Shows the main information widget.</summary>
        /// <param name="collaboratorUid">The edition event uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowMainInformationWidget(Guid collaboratorUid)
        {
            var mainInformationWidgetDto = await this.collaboratorRepo.FindDtoByUidAndByEditionIdAsync(collaboratorUid, this.EditionDto.Id);
            if (mainInformationWidgetDto == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Edition, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
        {
            new { page = this.RenderRazorViewToString("Widgets/MainInformationWidget", mainInformationWidgetDto), divIdOrClass = "#ManagersMainInformationWidget" },
        }
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>Detailses the specified identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet, Route("Details/{collaboratorUid}")]
        public async Task<ActionResult> Details(Guid? collaboratorUid)
        {
            var collaboratorDto = await this.CommandBus.Send(new FindCollaboratorDtoByUidAndByEditionIdAsync(collaboratorUid, this.EditionDto.Id, this.UserInterfaceLanguage));

            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper("Administradores", new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper("Administradores", Url.Action("Index", "Managers", new { Area = "" })),
		        //new BreadcrumbItemHelper(editionDto.Edition.Name, Url.Action("Details", "Collaborators", new { id }))
	        });

            //ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.Editions, null);

            #endregion

            return View(collaboratorDto);
        }
     

        #endregion

        #region Create

        /// <summary>Shows the create modal.</summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowCreateModal()
        {
            var cmd = new CreateManager(
                await this.roleRepo.FindAllAdminRolesAsync(),
                await this.collaboratorTypeRepo.FindAllAdminCollaboratorTypesAsync(),
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
        public async Task<ActionResult> Create(CreateManager cmd)
        {
            var result = new AppValidationResult();

            try
            {
                if (!ModelState.IsValid)
                {
                    throw new DomainException(Messages.CorrectFormValues);
                }

                cmd.UpdatePreSendProperties(
                    this.IdentityController.HashPassword(cmd.Password),
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

                //Send invitation email (Disabled)
                //var createdCollaborator = result.Data as Collaborator;
                //if (createdCollaborator != null)
                //{
                //    var emailSendResult = await this.SendAdminInvitationEmails(createdCollaborator.Uid.ToString());
                //}
            }
            catch (DomainException ex)
            {
                foreach (var error in result.Errors)
                {
                    var target = error.Target ?? "";
                    ModelState.AddModelError(target, error.Message);
                }

                cmd.UpdateDropdownProperties(
                    await this.roleRepo.FindAllAdminRolesAsync(),
                    await this.collaboratorTypeRepo.FindAllAdminCollaboratorTypesAsync(),
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
            UpdateManager cmd;

            try
            {
                cmd = new UpdateManager(
                    await this.CommandBus.Send(new FindCollaboratorDtoByUidAndByEditionIdAsync(collaboratorUid, this.EditionDto.Id, this.UserInterfaceLanguage)),
                    await this.roleRepo.FindAllAdminRolesAsync(),
                    await this.collaboratorTypeRepo.FindAllAdminCollaboratorTypesAsync(),
                    isAddingToCurrentEdition ?? false,
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
                    new { page = this.RenderRazorViewToString("Modals/UpdateModal", cmd), divIdOrClass = "#GlobalModalContainer" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>Updates the specified command.</summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Update(UpdateManager cmd)
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

                cmd.UpdateDropdownProperties(
                    await this.roleRepo.FindAllAdminRolesAsync(),
                    await this.collaboratorTypeRepo.FindAllAdminCollaboratorTypesAsync(),
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

            return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Manager, Labels.UpdatedM) });
        }

        [HttpPost]
        public async Task<ActionResult> UpdateManagerStatus(Guid userUid, bool active)
        {
            var result = new AppValidationResult();
            UpdateManagerStatus cmd = new UpdateManagerStatus(userUid, active);

            try
            {                               
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

                cmd.UpdateDropdownProperties(
                    await this.roleRepo.FindAllAdminRolesAsync(),
                    await this.collaboratorTypeRepo.FindAllAdminCollaboratorTypesAsync(),
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

            return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Manager, Labels.UpdatedM) });
        }

        [HttpPost]
        public async Task<ActionResult> UpdatePassword(int userId, string newPassword)
        {
            IdentityResult result = null;
            try
            {

                //result = await _identityController.AddPasswordAsync(userId, newPassword);
                //.ChangePasswordAsync(this.UserAccessControlDto.User.Id, cmd.OldPassword, cmd.NewPassword);

                //var user = await _identityController.FindByIdAsync(userId);
                var token = await _identityController.GeneratePasswordResetTokenAsync(userId);
                result = await _identityController.ResetPasswordAsync(userId, token, newPassword);

                if (!result.Succeeded)
                {
                    throw new DomainException(Messages.ErrorUpdatingPassword);
                }
            }            
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return Json(new { status = "error", message = Messages.WeFoundAndError, }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Password, Labels.UpdatedF) });
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

        /// <summary>Sends the admin invitation emails.</summary>
        /// <param name="selectedCollaboratorsUids">The selected collaborators uids.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> SendAdminInvitationEmails(string selectedCollaboratorsUids)
        {
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

                foreach (var collaboratorDto in collaboratorsDtos)
                {
                    var collaboratorLanguageCode = collaboratorDto.Language?.Code ?? this.UserInterfaceLanguage;

                    try
                    {
                        var result = await this.CommandBus.Send(new SendAdminWelcomeEmailAsync(
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
                        //TODO: Check errors
                        //var errors = result?.Errors?.Select(e => e.Message)?.Join(", ");
                    }
                    catch (Exception ex)
                    {
                        Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                    }
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
    }
}