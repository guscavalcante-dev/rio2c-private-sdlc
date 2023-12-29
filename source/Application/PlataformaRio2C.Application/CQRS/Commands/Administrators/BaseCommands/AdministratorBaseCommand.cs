// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 04-24-2021
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 07-23-2021
// ***********************************************************************
// <copyright file="AdministratorBaseCommand.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Foolproof;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using Constants = PlataformaRio2C.Domain.Constants;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>AdministratorBaseCommand</summary>
    public class AdministratorBaseCommand : BaseCommand
    {
        [Display(Name = "FirstName", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [StringLength(100, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string FirstName { get; set; }

        [Display(Name = "LastNames", ResourceType = typeof(Labels))]
        [StringLength(200, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string LastNames { get; set; }

        [Display(Name = "Email", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [EmailAddress(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "EmailISInvalid")]
        [StringLength(256, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public bool? IsCreatingNewManager { get; set; }
        [RequiredIf("IsCreatingNewManager", "True", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [StringLength(100, MinimumLength = 6, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(Labels))]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPassword", ResourceType = typeof(Labels))]
        [Compare("Password", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PasswordConfirmationDoesNotMatch")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "PhoneNumber", ResourceType = typeof(Labels))]
        [StringLength(50, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string PhoneNumber { get; set; }

        [Display(Name = "CellPhone", ResourceType = typeof(Labels))]
        [StringLength(50, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string CellPhone { get; set; }

        [Display(Name = "Document", ResourceType = typeof(Labels))]
        [StringLength(100, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string Document { get; set; }

        [Display(Name = "BadgeName", ResourceType = typeof(Labels))]
        [StringLength(50, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string Badge { get; set; }

        [Display(Name = "Email", ResourceType = typeof(Labels))]
        [RequiredIf("SharePublicEmail", "True", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [EmailAddress(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "EmailISInvalid")]
        [StringLength(256, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        [DataType(DataType.EmailAddress)]
        public string PublicEmail { get; set; }

        [Display(Name = "Website", ResourceType = typeof(Labels))]
        [StringLength(300, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string Website { get; set; }

        [Display(Name = "LinkedIn")]
        [StringLength(100, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string Linkedin { get; set; }

        [Display(Name = "Twitter")]
        [StringLength(100, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string Twitter { get; set; }

        [Display(Name = "Instagram")]
        [StringLength(100, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string Instagram { get; set; }

        [Display(Name = "YouTube")]
        [StringLength(300, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string Youtube { get; set; }

        [Display(Name = "BirthDate", ResourceType = typeof(Labels))]
        public DateTime? BirthDate { get; set; }

        [Display(Name = "CollaboratorIndustry", ResourceType = typeof(Labels))]
        public Guid? CollaboratorIndustryUid { get; set; }

        [Display(Name = "EnterYourIndustry", ResourceType = typeof(Labels))]
        [StringLength(300, MinimumLength = 0, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        [RequiredIf("CollaboratorIndustryAdditionalInfoRequired", "True", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public string CollaboratorIndustryAdditionalInfo { get; set; }

        [Display(Name = "Gender", ResourceType = typeof(Labels))]
        public Guid? CollaboratorGenderUid { get; set; }

        [Display(Name = "AdditionalInfo", ResourceType = typeof(Labels))]
        [StringLength(300, MinimumLength = 0, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        [RequiredIf("CollaboratorGenderAdditionalInfoRequired", "True", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public string CollaboratorGenderAdditionalInfo { get; set; }

        [Display(Name = "Role", ResourceType = typeof(Labels))]
        public Guid? CollaboratorRoleUid { get; set; }

        [Display(Name = "EnterYourRole", ResourceType = typeof(Labels))]
        [StringLength(300, MinimumLength = 0, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        [RequiredIf("CollaboratorRoleAdditionalInfoRequired", "True", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public string CollaboratorRoleAdditionalInfo { get; set; }

        [Display(Name = "HasAnySpecialNeeds", ResourceType = typeof(Labels))]
        public bool? HasAnySpecialNeeds { get; set; }

        [Display(Name = "WhichSpecialNeedsQ", ResourceType = typeof(Labels))]
        [RequiredIf("HasAnySpecialNeeds", "True", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [StringLength(300, MinimumLength = 0, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string SpecialNeedsDescription { get; set; }

        [Display(Name = "HaveYouBeenToRio2CBefore", ResourceType = typeof(Labels))]
        public bool? HaveYouBeenToRio2CBefore { get; set; }

        [RequiredIf("HaveYouBeenToRio2CBefore", "True", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "SelectAtLeastOneOption")]
        public bool? HasEditionSelected { get; set; }

        [Display(Name = "Profile", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public string RoleName { get; set; }

        [Display(Name = "CollaboratorType", ResourceType = typeof(Labels))]
        [RequiredIf(nameof(RoleName), Constants.Role.AdminPartial, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public string[] CollaboratorTypeNames { get; set; }

        public bool? IsAdminFull => RoleName == Constants.Role.Admin;

        public bool? SharePublicEmail { get; set; }
        public bool CollaboratorIndustryAdditionalInfoRequired { get; set; }
        public bool CollaboratorGenderAdditionalInfoRequired { get; set; }
        public bool CollaboratorRoleAdditionalInfoRequired { get; set; }
          
        public string PasswordHash { get; private set; }

        public IEnumerable<Role> Roles { get; set; }
        public IEnumerable<CollaboratorType> CollaboratorTypes { get; set; }
        public IEnumerable<CollaboratorIndustry> CollaboratorIndustries { get; set; }
        public IEnumerable<CollaboratorGender> CollaboratorGenders { get; set; }
        public IEnumerable<CollaboratorRole> CollaboratorRoles { get; set; }
        public IEnumerable<Guid> EditionsUids { get; set; }
        public IEnumerable<EditionDto> Editions { get; set; }
        public List<AttendeeOrganizationBaseCommand> AttendeeOrganizationBaseCommands { get; set; }
        public List<CollaboratorJobTitleBaseCommand> JobTitles { get; set; }
        public List<CollaboratorMiniBioBaseCommand> MiniBios { get; set; }
        public CropperImageBaseCommand CropperImage { get; set; }
        public List<AttendeeOrganizationBaseCommand> TemplateAttendeeOrganizationBaseCommands { get; set; }

        /// <summary>Initializes a new instance of the <see cref="AdministratorBaseCommand"/> class.</summary>
        public AdministratorBaseCommand()
        {
        }

        #region Public Methods

        /// <summary>
        /// Updates the base properties.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="roles">The roles.</param>
        /// <param name="collaboratorTypes">The collaborator types.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        public void UpdateBaseProperties(
            CollaboratorDto entity,
            List<Role> roles,
            List<CollaboratorType> collaboratorTypes,
            string userInterfaceLanguage)
        {
            this.UpdateBaseProperties(entity);
            this.Badge = entity?.Badge;
            this.PhoneNumber = entity?.PhoneNumber;
            this.CellPhone = entity?.CellPhone;
            this.SharePublicEmail = !string.IsNullOrEmpty(entity?.PublicEmail);
            this.PublicEmail = entity?.PublicEmail;
            this.Website = entity?.Website;
            this.Linkedin = entity?.Linkedin;
            this.Twitter = entity?.Twitter;
            this.Instagram = entity?.Instagram;
            this.Youtube = entity?.Youtube;
            this.EditionsUids = entity?.EditionsUids;
            this.HaveYouBeenToRio2CBefore = entity?.EditionsUids?.Any();
            this.RoleName = entity?.Roles?.FirstOrDefault(r => Constants.Role.AnyAdminArray.Contains(r.Name))?.Name;
            this.CollaboratorTypeNames = entity?.AttendeeCollaboratorTypeDtos?.Select(s => s.CollaboratorType.Name)?.ToArray();

            this.UpdateDropdownProperties(roles, collaboratorTypes, userInterfaceLanguage);
        }

        /// <summary>
        /// Updates the base properties.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void UpdateBaseProperties(CollaboratorDto entity)
        {
            this.FirstName = entity?.FirstName;
            this.LastNames = entity?.LastNames;
            this.Email = entity?.Email;
        }

        /// <summary>
        /// Updates the pre send properties.
        /// </summary>
        /// <param name="passwordHash">The password hash.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="userUid">The user uid.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="editionUid">The edition uid.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        public void UpdatePreSendProperties(
            string passwordHash,
            int userId,
            Guid userUid,
            int? editionId,
            Guid? editionUid,
            string userInterfaceLanguage)
        {
            this.PasswordHash = passwordHash;
            this.UpdatePreSendProperties(userId, userUid, editionId, editionUid, userInterfaceLanguage);
        }

        /// <summary>
        /// Updates the dropdown properties.
        /// </summary>
        /// <param name="roles">The roles.</param>
        /// <param name="collaboratorTypes">The collaborator types.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        public void UpdateDropdownProperties(
            List<Role> roles,
            List<CollaboratorType> collaboratorTypes,
            string userInterfaceLanguage)
        {
            this.UpdateRoles(roles, userInterfaceLanguage);
            this.UpdateCollaboratorTypes(collaboratorTypes, userInterfaceLanguage);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Updates the roles.
        /// </summary>
        /// <param name="roles">The roles.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        /// <returns></returns>
        private void UpdateRoles(List<Role> roles, string userInterfaceLanguage)
        {
            this.Roles = roles?
                            .GetSeparatorTranslation(r => r.Description, userInterfaceLanguage, '|')?
                            .OrderBy(r => r.Description);
        }

        /// <summary>
        /// Updates the collaborator types.
        /// </summary>
        /// <param name="collaboratorTypes">The collaborator types.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        private void UpdateCollaboratorTypes(List<CollaboratorType> collaboratorTypes, string userInterfaceLanguage)
        {
            this.CollaboratorTypes = collaboratorTypes?
                            .GetSeparatorTranslation(ct => ct.Description, userInterfaceLanguage, '|')?
                            .OrderBy(ct => ct.Description);
        }

        #endregion
    }
}