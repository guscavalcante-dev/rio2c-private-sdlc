using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using MediatR;
using PlataformaRio2C.Application;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Application.CQRS.Queries;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.CrossCutting.Identity.AuthorizeAttributes;
using PlataformaRio2C.Infra.CrossCutting.Identity.Service;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using PlataformaRio2C.Infra.CrossCutting.Tools.Helpers;
using PlataformaRio2C.Web.Admin.Filters;
using Constants = PlataformaRio2C.Domain.Constants;

namespace PlataformaRio2C.Web.Admin.Controllers
{
    /// <summary>OrganizationsController</summary>
    [AjaxAuthorize(Order = 1, Roles = Constants.Role.AnyAdmin)]
    [AuthorizeCollaboratorType(Order = 2, Types = Constants.CollaboratorType.AdminAudiovisual + "," + Constants.CollaboratorType.AdminMusic)]
    public class OrganizationsController : BaseController
    {
        private readonly IOrganizationTypeRepository organizationTypeRepo;
        private readonly IAttendeeOrganizationRepository attendeeOrganizationRepo;
        private readonly IActivityRepository activityRepo;
        private readonly ITargetAudienceRepository targetAudienceRepo;
        private readonly IInterestRepository interestRepo;
        private readonly ICollaboratorTypeRepository collaboratorTypeRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrganizationsController"/> class.
        /// </summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="identityController">The identity controller.</param>
        /// <param name="organizationTypeRepository">The organization type repository.</param>
        /// <param name="attendeeOrganizationRepository">The attendee organization repository.</param>
        /// <param name="activityRepository">The activity repository.</param>
        /// <param name="targetAudienceRepository">The target audience repository.</param>
        /// <param name="interestRepository">The interest repository.</param>
        public OrganizationsController(
            IMediator commandBus,
            IdentityAutenticationService identityController,
            IOrganizationTypeRepository organizationTypeRepository,
            IAttendeeOrganizationRepository attendeeOrganizationRepository,
            IActivityRepository activityRepository,
            ITargetAudienceRepository targetAudienceRepository,
            IInterestRepository interestRepository,
            ICollaboratorTypeRepository collaboratorTypeRepository)
            : base(commandBus, identityController)
        {
            this.organizationTypeRepo = organizationTypeRepository;
            this.attendeeOrganizationRepo = attendeeOrganizationRepository;
            this.activityRepo = activityRepository;
            this.targetAudienceRepo = targetAudienceRepository;
            this.interestRepo = interestRepository;
            this.collaboratorTypeRepo = collaboratorTypeRepository;
        }

        #region Main Information Widget

        /// <summary>
        /// Shows the main information widget.
        /// </summary>
        /// <param name="organizationUid">The organization uid.</param>
        /// <param name="organizationTypeUid">The organization type uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowMainInformationWidget(Guid? organizationUid, Guid? organizationTypeUid)
        {
            var mainInformationWidgetDto = await this.attendeeOrganizationRepo.FindAdminMainInformationWidgetDtoByOrganizationUidAndByEditionIdAsync(
                organizationUid ?? Guid.Empty,
                organizationTypeUid ?? Guid.Empty,
                this.EditionDto.Id);
            if (mainInformationWidgetDto == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Company, Labels.FoundF.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            ViewBag.OrganizationTypeUid = organizationTypeUid; // It's the admin page accessed and not the organization type of the current organization

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/MainInformationWidget", mainInformationWidgetDto), divIdOrClass = "#CompanyMainInformationWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #region Update

        /// <summary>
        /// Shows the update main information modal.
        /// </summary>
        /// <param name="organizationUid">The organization uid.</param>
        /// <param name="organizationTypeUid">The organization type uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowUpdateMainInformationModal(Guid? organizationUid, Guid? organizationTypeUid)
        {
            UpdateOrganizationAdminMainInformation cmd;

            try
            {
                var mainInformationWidgetDto = await this.attendeeOrganizationRepo.FindAdminMainInformationWidgetDtoByOrganizationUidAndByEditionIdAsync(
                    organizationUid ?? Guid.Empty,
                    organizationTypeUid ?? Guid.Empty,
                    this.EditionDto.Id);
                if (mainInformationWidgetDto == null)
                {
                    throw new DomainException(string.Format(Messages.EntityNotAction, Labels.Company, Labels.FoundF.ToLowerInvariant()));
                }

                cmd = new UpdateOrganizationAdminMainInformation(
                    mainInformationWidgetDto,
                    await this.organizationTypeRepo.FindByUidAsync(organizationTypeUid ?? Guid.Empty),
                    await this.CommandBus.Send(new FindAllHoldingsBaseDtosAsync(null, this.UserInterfaceLanguage)),
                    await this.CommandBus.Send(new FindAllLanguagesDtosAsync(this.UserInterfaceLanguage)));
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

        /// <summary>
        /// Updates the main information.
        /// </summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> UpdateMainInformation(UpdateOrganizationAdminMainInformation cmd)
        {
            var result = new AppValidationResult();

            try
            {
                if (!ModelState.IsValid)
                {
                    throw new DomainException(Messages.CorrectFormValues);
                }

                cmd.UpdatePreSendProperties(
                    await this.organizationTypeRepo.FindByUidAsync(cmd.OrganizationTypeUid ?? Guid.Empty),
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
                    await this.CommandBus.Send(new FindAllHoldingsBaseDtosAsync(null, this.UserInterfaceLanguage)));

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

            return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Company, Labels.UpdatedF) });
        }

        #endregion

        #endregion

        #region Social Networks Widget

        /// <summary>Shows the social networks widget.</summary>
        /// <param name="organizationUid">The organization uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowSocialNetworksWidget(Guid? organizationUid)
        {
            var socialNetworksWidgetDto = await this.attendeeOrganizationRepo.FindDetailsDtoByOrganizationUidAndByEditionIdAsync(organizationUid ?? Guid.Empty, this.EditionDto.Id, true);
            if (socialNetworksWidgetDto == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Company, Labels.FoundF.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/SocialNetworksWidget", socialNetworksWidgetDto), divIdOrClass = "#CompanySocialNetworksWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #region Update

        /// <summary>Shows the update social networks modal.</summary>
        /// <param name="organizationUid">The organization uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowUpdateSocialNetworksModal(Guid? organizationUid)
        {
            UpdateOrganizationSocialNetworks cmd;

            try
            {
                var socialNetworksWidgetDto = await this.attendeeOrganizationRepo.FindDetailsDtoByOrganizationUidAndByEditionIdAsync(organizationUid ?? Guid.Empty, this.EditionDto.Id, true);
                if (socialNetworksWidgetDto == null)
                {
                    throw new DomainException(string.Format(Messages.EntityNotAction, Labels.Company, Labels.FoundF.ToLowerInvariant()));
                }

                //if (this.UserAccessControlDto?.HasEditionAttendeeOrganization(socialNetworksWidgetDto.AttendeeOrganization.Uid) != true)
                //{
                //    throw new DomainException(Texts.ForbiddenErrorMessage);
                //}

                cmd = new UpdateOrganizationSocialNetworks(socialNetworksWidgetDto);
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
        public async Task<ActionResult> UpdateSocialNetworks(UpdateOrganizationSocialNetworks cmd)
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
                        new { page = this.RenderRazorViewToString("Modals/UpdateSocialNetworksForm", cmd), divIdOrClass = "#form-container" },
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

        #endregion

        #region Address Widget

        /// <summary>Shows the address widget.</summary>
        /// <param name="organizationUid">The organization uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowAddressWidget(Guid? organizationUid)
        {
            var addressWidgetDto = await this.attendeeOrganizationRepo.FindAddressWidgetDtoByOrganizationUidAndByEditionIdAsync(organizationUid ?? Guid.Empty, this.EditionDto.Id, true);
            if (addressWidgetDto == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Address, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            //if (this.UserAccessControlDto?.HasEditionAttendeeOrganization(addressWidgetDto.AttendeeOrganization.Uid) != true)
            //{
            //    return Json(new { status = "error", message = Texts.ForbiddenErrorMessage }, JsonRequestBehavior.AllowGet);
            //}

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/AddressWidget", addressWidgetDto), divIdOrClass = "#CompanyAddressWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #region Update

        /// <summary>Shows the update address modal.</summary>
        /// <param name="organizationUid">The organization uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowUpdateAddressModal(Guid? organizationUid)
        {
            UpdateOrganizationAddress cmd;

            try
            {
                var addressWidgetDto = await this.attendeeOrganizationRepo.FindAddressWidgetDtoByOrganizationUidAndByEditionIdAsync(organizationUid ?? Guid.Empty, this.EditionDto.Id, true);
                if (addressWidgetDto == null)
                {
                    throw new DomainException(string.Format(Messages.EntityNotAction, Labels.Company, Labels.FoundF.ToLowerInvariant()));
                }

                //if (this.UserAccessControlDto?.HasEditionAttendeeOrganization(addressWidgetDto.AttendeeOrganization.Uid) != true)
                //{
                //    throw new DomainException(Texts.ForbiddenErrorMessage);
                //}

                cmd = new UpdateOrganizationAddress(
                    addressWidgetDto,
                    await this.CommandBus.Send(new FindAllCountriesBaseDtosAsync(this.UserInterfaceLanguage)),
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
                    new { page = this.RenderRazorViewToString("Modals/UpdateAddressModal", cmd), divIdOrClass = "#GlobalModalContainer" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>Updates the address.</summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> UpdateAddress(UpdateOrganizationAddress cmd)
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

                cmd.UpdateModelsAndLists(
                    await this.CommandBus.Send(new FindAllCountriesBaseDtosAsync(this.UserInterfaceLanguage)));

                return Json(new
                {
                    status = "error",
                    message = result.Errors?.FirstOrDefault(e => e.Target == "ToastrError")?.Message ?? ex.GetInnerMessage(),
                    pages = new List<dynamic>
                    {
                        new { page = this.RenderRazorViewToString("Modals/UpdateAddressForm", cmd), divIdOrClass = "#form-container" },
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

        #endregion

        #region Activity Widget

        /// <summary>Shows the activity widget.</summary>
        /// <param name="organizationUid">The organization uid.</param>
        /// <param name="projectTypeId">Project type id</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowActivityWidget(Guid? organizationUid, Guid? projectTypeUid)
        {
            var activityWidgetDto = await this.attendeeOrganizationRepo.FindActivityWidgetDtoByOrganizationUidAndByEditionIdAsync(organizationUid ?? Guid.Empty, this.EditionDto.Id, true);
            if (activityWidgetDto == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Activity, Labels.FoundF.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            ViewBag.Activities = await this.activityRepo.FindAllByProjectTypeUidAsync(projectTypeUid ?? ProjectType.Audiovisual.Uid);

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/ActivityWidget", activityWidgetDto), divIdOrClass = "#CompanyActivityWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #region Update

        /// <summary>Shows the update activity modal.</summary>
        /// <param name="organizationUid">The organization uid.</param>
        /// <param name="projectTypeId">Project type id</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowUpdateActivityModal(Guid? organizationUid, Guid? projectTypeUid)
        {
            UpdateOrganizationActivities cmd;

            try
            {
                var activityWidgetDto = await this.attendeeOrganizationRepo.FindActivityWidgetDtoByOrganizationUidAndByEditionIdAsync(organizationUid ?? Guid.Empty, this.EditionDto.Id, true);
                if (activityWidgetDto == null)
                {
                    throw new DomainException(string.Format(Messages.EntityNotAction, Labels.Company, Labels.FoundF.ToLowerInvariant()));
                }

                cmd = new UpdateOrganizationActivities(
                    activityWidgetDto,
                    await this.activityRepo.FindAllByProjectTypeUidAsync(projectTypeUid ?? ProjectType.Audiovisual.Uid));
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
                    new { page = this.RenderRazorViewToString("Modals/UpdateActivityModal", cmd), divIdOrClass = "#GlobalModalContainer" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>Updates the activities.</summary>
        /// <param name="cmd">The command</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> UpdateActivities(UpdateOrganizationActivities cmd)
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
                        new { page = this.RenderRazorViewToString("Modals/UpdateActivityForm", cmd), divIdOrClass = "#form-container" },
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

        #endregion

        #region Target Audience Widget

        /// <summary>Shows the activity widget.</summary>
        /// <param name="organizationUid">The organization uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowTargetAudienceWidget(Guid? organizationUid)
        {
            var targetAudienceWidgetDto = await this.attendeeOrganizationRepo.FindTargetAudienceWidgetDtoByOrganizationUidAndByEditionIdAsync(organizationUid ?? Guid.Empty, this.EditionDto.Id, true);
            if (targetAudienceWidgetDto == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.TargetAudience, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            ViewBag.TargetAudiences = await this.targetAudienceRepo.FindAllByProjectTypeIdAsync(ProjectType.Audiovisual.Id);

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/TargetAudienceWidget", targetAudienceWidgetDto), divIdOrClass = "#CompanyTargetAudienceWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #region Update

        /// <summary>Shows the update target audience modal.</summary>
        /// <param name="organizationUid">The organization uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowUpdateTargetAudienceModal(Guid? organizationUid)
        {
            UpdateOrganizationTargetAudiences cmd;

            try
            {
                var targetAudienceWidgetDto = await this.attendeeOrganizationRepo.FindTargetAudienceWidgetDtoByOrganizationUidAndByEditionIdAsync(organizationUid ?? Guid.Empty, this.EditionDto.Id, true);
                if (targetAudienceWidgetDto == null)
                {
                    throw new DomainException(string.Format(Messages.EntityNotAction, Labels.Company, Labels.FoundF.ToLowerInvariant()));
                }

                cmd = new UpdateOrganizationTargetAudiences(
                    targetAudienceWidgetDto,
                    await this.targetAudienceRepo.FindAllByProjectTypeIdAsync(ProjectType.Audiovisual.Id));
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
                    new { page = this.RenderRazorViewToString("Modals/UpdateTargetAudienceModal", cmd), divIdOrClass = "#GlobalModalContainer" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>Updates the target audiences.</summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> UpdateTargetAudiences(UpdateOrganizationTargetAudiences cmd)
        {
            var result = new AppValidationResult();

            try
            {
                if (!ModelState.IsValid)
                {
                    throw new DomainException(Messages.CorrectFormValues);
                }

                cmd.UpdatePreSendProperties(
                    ProjectType.Audiovisual.Id,
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

                cmd.UpdateTargetAudiences(null, await this.targetAudienceRepo.FindAllByProjectTypeIdAsync(ProjectType.Audiovisual.Id));

                return Json(new
                {
                    status = "error",
                    message = result.Errors?.FirstOrDefault(e => e.Target == "ToastrError")?.Message ?? ex.GetInnerMessage(),
                    pages = new List<dynamic>
                    {
                        new { page = this.RenderRazorViewToString("Modals/UpdateTargetAudienceForm", cmd), divIdOrClass = "#form-container" },
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

        #endregion

        #region Interest Widget

        /// <summary>Shows the interest widget.</summary>
        /// <param name="organizationUid">The organization uid.</param>
        /// <param name="projectTypeId">Project type uid</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowInterestWidget(Guid? organizationUid, int? projectTypeId)
        {
            var interestWidgetDto = await this.attendeeOrganizationRepo.FindInterestWidgetDtoByOrganizationUidAndByEditionIdAsync(organizationUid ?? Guid.Empty, this.EditionDto.Id, true);
            if (interestWidgetDto == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Interests, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            ViewBag.GroupedInterests = await this.interestRepo.FindAllByProjectTypeIdAndGroupedByInterestGroupAsync(projectTypeId ?? ProjectType.Audiovisual.Id);

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/InterestWidget", interestWidgetDto), divIdOrClass = "#CompanyInterestWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #region Update

        /// <summary>Shows the update interest modal.</summary>
        /// <param name="organizationUid">The organization uid.</param>
        /// <param name="projectTypeId">Project type uid</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowUpdateInterestModal(Guid? organizationUid, int? projectTypeId)
        {
            UpdateOrganizationInterests cmd;

            try
            {
                var interestWidgetDto = await this.attendeeOrganizationRepo.FindInterestWidgetDtoByOrganizationUidAndByEditionIdAsync(organizationUid ?? Guid.Empty, this.EditionDto.Id, true);
                if (interestWidgetDto == null)
                {
                    throw new DomainException(string.Format(Messages.EntityNotAction, Labels.Company, Labels.FoundF.ToLowerInvariant()));
                }

                //if (this.UserAccessControlDto?.HasEditionAttendeeOrganization(interestWidgetDto.AttendeeOrganization.Uid) != true)
                //{
                //    throw new DomainException(Texts.ForbiddenErrorMessage);
                //}

                cmd = new UpdateOrganizationInterests(
                    interestWidgetDto,
                    await this.interestRepo.FindAllDtosbyProjectTypeIdAsync(projectTypeId ?? ProjectType.Audiovisual.Id),
                    await this.CommandBus.Send(new FindAllLanguagesDtosAsync(this.UserInterfaceLanguage)),
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
                    new { page = this.RenderRazorViewToString("Modals/UpdateInterestModal", cmd), divIdOrClass = "#GlobalModalContainer" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>Updates the interests.</summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> UpdateInterests(UpdateOrganizationInterests cmd)
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

                //cmd.UpdateModelsAndLists(await this.interestRepo.FindAllGroupedByInterestGroupsAsync());

                return Json(new
                {
                    status = "error",
                    message = result.Errors?.FirstOrDefault(e => e.Target == "ToastrError")?.Message ?? ex.GetInnerMessage(),
                    pages = new List<dynamic>
                    {
                        new { page = this.RenderRazorViewToString("Modals/UpdateInterestForm", cmd), divIdOrClass = "#form-container" },
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

        #endregion

        #region Executives Widget

        /// <summary>
        /// Shows the executive widget.
        /// </summary>
        /// <param name="organizationUid">The organization uid.</param>
        /// <param name="organizationTypeUid">The organization type uid.</param>
        /// <returns></returns>
        [HttpGet]
        //TODO: This route isn't more specific to Audiovisual area. Move this route out from Audiovisual Area!
        //Or make it specific and create this same route at Innovation/OrganizationsController and Music/OrganizationsCrontroller.
        public async Task<ActionResult> ShowExecutivesWidget(Guid? organizationUid, Guid? organizationTypeUid)
        {
            ViewBag.OrganizationTypeUid = organizationTypeUid;
            ViewBag.CollaboratorTypeForDropdownSearch = organizationTypeUid == OrganizationType.AudiovisualPlayer.Uid ? "Audiovisual/PlayersExecutives" :
                                                        organizationTypeUid == OrganizationType.StartupPlayer.Uid ? "Innovation/PlayersExecutives" :
                                                        organizationTypeUid == OrganizationType.MusicPlayer.Uid ? "Music/PlayersExecutives" :
                                                        "Audiovisual/ProducersExecutives";


            Guid collaboratorTypeUid = organizationTypeUid == OrganizationType.AudiovisualPlayer.Uid ? CollaboratorType.PlayerExecutiveAudiovisual.Uid :
                                            organizationTypeUid == OrganizationType.Producer.Uid ? CollaboratorType.Industry.Uid :
                                                Guid.Empty;

            var executiveWidgetDto = await this.attendeeOrganizationRepo.FindAdminExecutiveWidgetDtoByOrganizationUidAndByEditionIdAsync(
                organizationUid ?? Guid.Empty,
                organizationTypeUid ?? Guid.Empty,
                collaboratorTypeUid,
                this.EditionDto.Id);
            if (executiveWidgetDto == null)
            {
                this.StatusMessageToastr(string.Format(Messages.EntityNotAction, Labels.Executive, Labels.FoundM.ToLowerInvariant()), Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);
                return RedirectToAction("Index", $"{ViewBag.CollaboratorTypeForDropdownSearch}", new { Area = "Audiovisual" });
            }


            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/ExecutivesWidget", executiveWidgetDto), divIdOrClass = "#OrganizationExecutivesWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #region Associate

        /// <summary>
        /// Shows the create player executive modal.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowAssociateExecutiveModal(Guid? organizationUid, Guid? organizationTypeUid)
        {
            AssociateOrganizationCollaborator cmd;

            try
            {
                if (!organizationUid.HasValue)
                {
                    throw new DomainException(string.Format(Messages.EntityNotAction, Labels.Company, Labels.FoundM.ToLowerInvariant()));
                }

                cmd = new AssociateOrganizationCollaborator(organizationUid, null, organizationTypeUid);
            }
            catch (DomainException ex)
            {
                return Json(new { status = "error", message = ex.GetInnerMessage() }, JsonRequestBehavior.AllowGet);
            }

            ViewBag.OrganizationTypeUid = organizationTypeUid;

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Modals/AssociateExecutiveModal", cmd), divIdOrClass = "#GlobalModalContainer" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>Creates the specified command.</summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> AssociateExecutive(AssociateOrganizationCollaborator cmd)
        {
            var result = new AppValidationResult();

            try
            {
                if (!ModelState.IsValid)
                {
                    throw new DomainException(Messages.CorrectFormValues);
                }

                if (cmd.OrganizationTypeUid == OrganizationType.AudiovisualPlayer.Uid)
                {
                    cmd.CollaboratorTypeName = CollaboratorType.PlayerExecutiveAudiovisual.Name;
                }
                else if (cmd.OrganizationTypeUid == OrganizationType.Producer.Uid)
                {
                    cmd.CollaboratorTypeName = CollaboratorType.Industry.Name;
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
                    pages = new List<dynamic>
                    {
                        new { page = this.RenderRazorViewToString("Modals/AssociateExecutiveForm", cmd), divIdOrClass = "#form-container" },
                    }
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return Json(new { status = "error", message = Messages.WeFoundAndError, }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Executive, Labels.Associated.ToLowerInvariant()) });
        }

        #endregion

        #region Disassociate

        /// <summary>Deletes the organization.</summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> DisassociateExecutive(DisassociateOrganizationCollaborator cmd)
        {
            var result = new AppValidationResult();

            try
            {
                if (!ModelState.IsValid)
                {
                    throw new DomainException(Messages.CorrectFormValues);
                }

                if (cmd.OrganizationTypeUid == OrganizationType.AudiovisualPlayer.Uid)
                {
                    cmd.CollaboratorTypeName = CollaboratorType.PlayerExecutiveAudiovisual.Name;
                }
                else if (cmd.OrganizationTypeUid == OrganizationType.Producer.Uid)
                {
                    cmd.CollaboratorTypeName = CollaboratorType.Industry.Name;
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

            return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Executive, Labels.Disassociated.ToLowerInvariant()) });
        }

        #endregion

        #endregion

        #region Api Configuration Widget

        /// <summary>
        /// Shows the API configuration widget.
        /// </summary>
        /// <param name="organizationUid">The organization uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowApiConfigurationWidget(Guid? organizationUid, Guid? organizationTypeUid)
        {
            var apiConfigurationWidgetDto = await this.attendeeOrganizationRepo.FindApiConfigurationWidgetDtoByOrganizationUidAndByEditionIdAsync(organizationUid ?? Guid.Empty, this.EditionDto.Id);
            if (apiConfigurationWidgetDto == null)
            {
                return Json(new { status = "error", message = string.Format(Messages.EntityNotAction, Labels.Company, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            ViewBag.OrganizationTypeUid = organizationTypeUid;

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/ApiConfigurationWidget", apiConfigurationWidgetDto), divIdOrClass = "#OrganizationApiConfigurationWidget" },
                }

            }, JsonRequestBehavior.AllowGet);
        }

        #region Update

        [HttpGet]
        public async Task<ActionResult> ShowUpdateApiConfigurationModal(Guid? organizationUid, Guid? organizationTypeUid)
        {
            UpdateOrganizationApiConfiguration cmd;

            try
            {
                var apiConfigurationWidgetDto = await this.attendeeOrganizationRepo.FindApiConfigurationWidgetDtoByOrganizationUidAndByEditionIdAsync(organizationUid ?? Guid.Empty, this.EditionDto.Id);
                if (apiConfigurationWidgetDto == null)
                {
                    throw new DomainException(string.Format(Messages.EntityNotAction, Labels.Company, Labels.FoundM.ToLowerInvariant()));
                }

                cmd = new UpdateOrganizationApiConfiguration(
                    apiConfigurationWidgetDto,
                    organizationTypeUid ?? Guid.Empty,
                    await this.attendeeOrganizationRepo.FindAllApiConfigurationWidgetDtoByHighlight(this.EditionDto.Id, organizationTypeUid ?? Guid.Empty));
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
                    new { page = this.RenderRazorViewToString("Modals/UpdateApiConfigurationModal", cmd), divIdOrClass = "#GlobalModalContainer" },
                }
            }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public async Task<ActionResult> UpdateApiConfiguration(UpdateOrganizationApiConfiguration cmd)
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

                cmd.UpdateBaseModels(
                    await this.attendeeOrganizationRepo.FindAllApiConfigurationWidgetDtoByHighlight(this.EditionDto.Id, cmd.OrganizationTypeUid),
                    cmd.OrganizationTypeUid);

                return Json(new
                {
                    status = "error",
                    message = result.Errors?.FirstOrDefault(e => e.Target == "ToastrError")?.Message ?? ex.GetInnerMessage(),
                    pages = new List<dynamic>
                    {
                        new { page = this.RenderRazorViewToString("~/Areas/Audiovisual/Views/Organizations/Modals/UpdateApiConfigurationForm.cshtml", cmd), divIdOrClass = "#form-container" },
                    }
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return Json(new { status = "error", message = Messages.WeFoundAndError, }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Company, Labels.UpdatedM) });
        }

        #endregion

        #endregion
    }
}