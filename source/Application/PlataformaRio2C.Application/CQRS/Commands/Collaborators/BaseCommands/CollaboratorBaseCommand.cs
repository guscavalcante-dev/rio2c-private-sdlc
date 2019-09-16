// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-26-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-16-2019
// ***********************************************************************
// <copyright file="CollaboratorBaseCommand.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Statics;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>CollaboratorBaseCommand</summary>
    public class CollaboratorBaseCommand : BaseCommand
    {
        [Display(Name = "FirstName", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [StringLength(100, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string FirstName { get; set; }

        [Display(Name = "LastNames", ResourceType = typeof(Labels))]
        [StringLength(200, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string LastNames { get; set; }

        [Display(Name = "BadgeName", ResourceType = typeof(Labels))]
        [StringLength(50, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string Badge { get; set; }

        [Display(Name = "Email", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [EmailAddress(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "EmailISInvalid")]
        [StringLength(256, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "PhoneNumber", ResourceType = typeof(Labels))]
        [StringLength(50, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string PhoneNumber { get; set; }

        [Display(Name = "CellPhone", ResourceType = typeof(Labels))]
        [StringLength(50, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string CellPhone { get; set; }

        [Display(Name = "Email", ResourceType = typeof(Labels))]
        [EmailAddress(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "EmailISInvalid")]
        [StringLength(256, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        [DataType(DataType.EmailAddress)]
        public string PublicEmail { get; set; }

        public List<AttendeeOrganizationBaseCommand> AttendeeOrganizationBaseCommands { get; set; }
        public List<CollaboratorJobTitleBaseCommand> JobTitles { get; set; }
        public List<CollaboratorMiniBioBaseCommand> MiniBios { get; set; }
        public CropperImageBaseCommand CropperImage { get; set; }

        public List<AttendeeOrganizationBaseCommand> TemplateAttendeeOrganizationBaseCommands { get; set; }
        public OrganizationType OrganizationType { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="CollaboratorBaseCommand"/> class.</summary>
        public CollaboratorBaseCommand()
        {
        }

        /// <summary>Updates the base properties.</summary>
        /// <param name="entity">The entity.</param>
        /// <param name="attendeeOrganizationsBaseDtos">The attendee organizations base dtos.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        /// <param name="countriesBaseDtos">The countries base dtos.</param>
        /// <param name="isJobTitleRequired">if set to <c>true</c> [is job title required].</param>
        /// <param name="isMiniBioRequired">if set to <c>true</c> [is mini bio required].</param>
        /// <param name="isImageRequired">if set to <c>true</c> [is image required].</param>
        public void UpdateBaseProperties(
            CollaboratorDto entity, 
            List<AttendeeOrganizationBaseDto> attendeeOrganizationsBaseDtos, 
            List<LanguageDto> languagesDtos, 
            List<CountryBaseDto> countriesBaseDtos, 
            bool isJobTitleRequired, 
            bool isMiniBioRequired, 
            bool isImageRequired)
        {
            this.FirstName = entity?.FirstName;
            this.LastNames = entity?.LastNames;
            this.Badge = entity?.Badge;
            this.Email = entity?.Email;
            this.PhoneNumber = entity?.PhoneNumber;
            this.CellPhone = entity?.CellPhone;
            this.PublicEmail = entity?.PublicEmail;
            this.UpdateOrganizations(entity, attendeeOrganizationsBaseDtos);
            this.UpdateJobTitles(entity, languagesDtos, isJobTitleRequired);
            this.UpdateMiniBios(entity, languagesDtos, isMiniBioRequired);
            this.UpdateCropperImage(entity, isImageRequired);
            this.UpdateDropdownProperties(attendeeOrganizationsBaseDtos, countriesBaseDtos);
        }

        /// <summary>Updates the organizations.</summary>
        /// <param name="entity">The entity.</param>
        /// <param name="attendeeOrganizationsBaseDtos">The attendee organizations base dtos.</param>
        private void UpdateOrganizations(CollaboratorDto entity, List<AttendeeOrganizationBaseDto> attendeeOrganizationsBaseDtos)
        {
            if (this.AttendeeOrganizationBaseCommands == null)
            {
                this.AttendeeOrganizationBaseCommands = new List<AttendeeOrganizationBaseCommand>();
            }

            if (entity?.AttendeeOrganizationBasesDtos?.Any() != true)
            {
                this.AttendeeOrganizationBaseCommands.Add(new AttendeeOrganizationBaseCommand(null, attendeeOrganizationsBaseDtos));
            }
            else
            {
                this.AttendeeOrganizationBaseCommands = entity?.AttendeeOrganizationBasesDtos?.Select(aobd => new AttendeeOrganizationBaseCommand(aobd, attendeeOrganizationsBaseDtos))?.ToList();
            }
        }

        /// <summary>Updates the organization template.</summary>
        /// <param name="attendeeOrganizationsBaseDtos">The attendee organizations base dtos.</param>
        private void UpdateOrganizationTemplate(List<AttendeeOrganizationBaseDto> attendeeOrganizationsBaseDtos)
        {
            if (this.TemplateAttendeeOrganizationBaseCommands == null)
            {
                this.TemplateAttendeeOrganizationBaseCommands = new List<AttendeeOrganizationBaseCommand>();
            }

            this.TemplateAttendeeOrganizationBaseCommands.Add(new AttendeeOrganizationBaseCommand(null, attendeeOrganizationsBaseDtos));
        }

        /// <summary>Updates the job titles.</summary>
        /// <param name="entity">The entity.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        /// <param name="isJobTitleRequired">if set to <c>true</c> [is job title required].</param>
        private void UpdateJobTitles(CollaboratorDto entity, List<LanguageDto> languagesDtos, bool isJobTitleRequired)
        {
            this.JobTitles = new List<CollaboratorJobTitleBaseCommand>();
            foreach (var languageDto in languagesDtos)
            {
                var jobTitle = entity?.JobTitlesDtos?.FirstOrDefault(d => d.LanguageDto.Code == languageDto.Code);
                this.JobTitles.Add(jobTitle != null ? new CollaboratorJobTitleBaseCommand(jobTitle, isJobTitleRequired) :
                                                      new CollaboratorJobTitleBaseCommand(languageDto, isJobTitleRequired));
            }
        }

        /// <summary>Updates the mini bios.</summary>
        /// <param name="entity">The entity.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        /// <param name="isMiniBioRequired">if set to <c>true</c> [is mini bio required].</param>
        private void UpdateMiniBios(CollaboratorDto entity, List<LanguageDto> languagesDtos, bool isMiniBioRequired)
        {
            this.MiniBios = new List<CollaboratorMiniBioBaseCommand>();
            foreach (var languageDto in languagesDtos)
            {
                var miniBio = entity?.MiniBiosDtos?.FirstOrDefault(d => d.LanguageDto.Code == languageDto.Code);
                this.MiniBios.Add(miniBio != null ? new CollaboratorMiniBioBaseCommand(miniBio, isMiniBioRequired) :
                                                    new CollaboratorMiniBioBaseCommand(languageDto, isMiniBioRequired));
            }
        }

        /// <summary>Updates the cropper image.</summary>
        /// <param name="entity">The entity.</param>
        /// <param name="isImageRequired">if set to <c>true</c> [is image required].</param>
        private void UpdateCropperImage(CollaboratorDto entity, bool isImageRequired)
        {
            this.CropperImage = new CropperImageBaseCommand(entity?.ImageUploadDate, entity?.Uid, FileRepositoryPathType.UserImage, isImageRequired);
        }

        /// <summary>Updates the dropdown properties.</summary>
        /// <param name="attendeeOrganizationsBaseDtos">The attendee organizations base dtos.</param>
        /// <param name="countriesBaseDtos">The countries base dtos.</param>
        public void UpdateDropdownProperties(List<AttendeeOrganizationBaseDto> attendeeOrganizationsBaseDtos, List<CountryBaseDto> countriesBaseDtos)
        {
            // Attendee organizations
            foreach (var attendeeOrganizationBaseCommand in this.AttendeeOrganizationBaseCommands)
            {
                attendeeOrganizationBaseCommand.UpdateDropdownProperties(attendeeOrganizationsBaseDtos);
            }

            this.UpdateOrganizationTemplate(attendeeOrganizationsBaseDtos);
        }

        /// <summary>Updates the pre send properties.</summary>
        /// <param name="organizationType">Type of the organization.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="userUid">The user uid.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="editionUid">The edition uid.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        public void UpdatePreSendProperties(
            OrganizationType organizationType,
            int userId,
            Guid userUid,
            int? editionId,
            Guid? editionUid,
            string userInterfaceLanguage)
        {
            this.OrganizationType = organizationType;
            this.UpdatePreSendProperties(userId, userUid, editionId, editionUid, UserInterfaceLanguage);
        }
    }
}