// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 12-12-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 01-31-2023
// ***********************************************************************
// <copyright file="CollaboratorDataBaseCommand.cs" company="Softo">
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
using PlataformaRio2C.Domain.Statics;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>CollaboratorDataBaseCommand</summary>
    public class CollaboratorDataBaseCommand : CollaboratorBaseCommand
    {
        [Display(Name = "BadgeName", ResourceType = typeof(Labels))]
        [StringLength(50, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string Badge { get; set; }

        [Display(Name = "PhoneNumber", ResourceType = typeof(Labels))]
        [StringLength(50, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string PhoneNumber { get; set; }

        [Display(Name = "CellPhone", ResourceType = typeof(Labels))]
        [StringLength(50, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string CellPhone { get; set; }

        public bool? SharePublicEmail { get; set; }

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

        #region CollaboratorIndustry

        [Display(Name = "CollaboratorIndustry", ResourceType = typeof(Labels))]
        public Guid? CollaboratorIndustryUid { get; set; }

        public bool CollaboratorIndustryAdditionalInfoRequired { get; set; }

        [Display(Name = "EnterYourIndustry", ResourceType = typeof(Labels))]
        [StringLength(300, MinimumLength = 0, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        [RequiredIf("CollaboratorIndustryAdditionalInfoRequired", "True", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public string CollaboratorIndustryAdditionalInfo { get; set; }

        #endregion

        #region CollaboratorGender

        [Display(Name = "Gender", ResourceType = typeof(Labels))]
        public Guid? CollaboratorGenderUid { get; set; }

        public bool CollaboratorGenderAdditionalInfoRequired { get; set; }

        [Display(Name = "AdditionalInfo", ResourceType = typeof(Labels))]
        [StringLength(300, MinimumLength = 0, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        [RequiredIf("CollaboratorGenderAdditionalInfoRequired", "True", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public string CollaboratorGenderAdditionalInfo { get; set; }

        #endregion

        #region CollaboratorRole

        [Display(Name = "Role", ResourceType = typeof(Labels))]
        public Guid? CollaboratorRoleUid { get; set; }

        public bool CollaboratorRoleAdditionalInfoRequired { get; set; }

        [Display(Name = "EnterYourRole", ResourceType = typeof(Labels))]
        [StringLength(300, MinimumLength = 0, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        [RequiredIf("CollaboratorRoleAdditionalInfoRequired", "True", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public string CollaboratorRoleAdditionalInfo { get; set; }

        #endregion

        #region SpecialNeeds

        [Display(Name = "HasAnySpecialNeeds", ResourceType = typeof(Labels))]
        public bool? HasAnySpecialNeeds { get; set; }

        [Display(Name = "WhichSpecialNeedsQ", ResourceType = typeof(Labels))]
        [RequiredIf("HasAnySpecialNeeds", "True", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [StringLength(300, MinimumLength = 0, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string SpecialNeedsDescription { get; set; }

        #endregion

        #region Editions

        [Display(Name = "HaveYouBeenToRio2CBefore", ResourceType = typeof(Labels))]
        public bool? HaveYouBeenToRio2CBefore { get; set; }

        public IEnumerable<Guid> EditionsUids { get; set; }

        [RequiredIf("HaveYouBeenToRio2CBefore", "True", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "SelectAtLeastOneOption")]
        public bool? HasEditionSelected { get; set; }

        #endregion

        public List<AttendeeOrganizationBaseCommand> AttendeeOrganizationBaseCommands { get; set; }
        public List<CollaboratorJobTitleBaseCommand> JobTitles { get; set; }
        public List<CollaboratorMiniBioBaseCommand> MiniBios { get; set; }
        public List<AttendeeCollaboratorActivityBaseCommand> AttendeeCollaboratorActivities { get; set; }
        public List<AttendeeCollaboratorInterestBaseCommand> AttendeeCollaboratorInterests { get; set; }
        //public List<AttendeeCollaboratorInnovationOrganizationTrackBaseCommand> AttendeeCollaboratorInnovationOrganizationTracks { get; set; }

        public CropperImageBaseCommand CropperImage { get; set; }
        public List<AttendeeOrganizationBaseCommand> TemplateAttendeeOrganizationBaseCommands { get; set; }

        #region Dropdowns Properties

        public IEnumerable<CollaboratorGender> CollaboratorGenders { get; set; }
        public IEnumerable<CollaboratorIndustry> CollaboratorIndustries { get; set; }
        public IEnumerable<CollaboratorRole> CollaboratorRoles { get; set; }
        public IEnumerable<EditionDto> Editions { get; set; }

        #endregion

        /// <summary>Initializes a new instance of the <see cref="CollaboratorDataBaseCommand"/> class.</summary>
        public CollaboratorDataBaseCommand()
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
            List<CollaboratorGender> genders,
            List<CollaboratorIndustry> industries,
            List<CollaboratorRole> collaboratorRoles,
            List<EditionDto> editionsDtos,
            List<Activity> activities,
            List<InterestDto> interestsDtos,
            List<InnovationOrganizationTrackOptionDto> innovationOrganizationTrackOptionDtos,
            int currentEditionId,
            bool isJobTitleRequired,
            bool isMiniBioRequired,
            bool isImageRequired,
            string userInterfaceLanguage)
        {
            base.UpdateBaseProperties(entity);
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
            this.UpdateOrganizations(entity, attendeeOrganizationsBaseDtos);
            this.UpdateJobTitles(entity, languagesDtos, isJobTitleRequired);
            this.UpdateMiniBios(entity, languagesDtos, isMiniBioRequired);
            this.UpdateCropperImage(entity, isImageRequired);
            this.UpdateActivities(entity, activities);
            this.UpdateInterests(entity, interestsDtos);
            this.UpdateInnovationOrganizationTrackOptions(entity, innovationOrganizationTrackOptionDtos);

            this.UpdateDropdownProperties(
                attendeeOrganizationsBaseDtos,
                genders,
                industries,
                collaboratorRoles,
                editionsDtos,
                activities,
                interestsDtos,
                innovationOrganizationTrackOptionDtos,
                currentEditionId,
                userInterfaceLanguage);
        }

        /// <summary>
        /// Updates the dropdown properties.
        /// </summary>
        /// <param name="attendeeOrganizationsBaseDtos">The attendee organizations base dtos.</param>
        /// <param name="genders">The genders.</param>
        /// <param name="industries">The industries.</param>
        /// <param name="collaboratorRoles">The roles.</param>
        /// <param name="editionsDtos">The editions dtos.</param>
        /// <param name="activities">The activities.</param>
        /// <param name="interestsDtos">The interests dtos.</param>
        /// <param name="currentEditionId">The current edition identifier.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        public void UpdateDropdownProperties(List<AttendeeOrganizationBaseDto> attendeeOrganizationsBaseDtos,
            List<CollaboratorGender> genders,
            List<CollaboratorIndustry> industries,
            List<CollaboratorRole> collaboratorRoles,
            List<EditionDto> editionsDtos,
            List<Activity> activities,
            List<InterestDto> interestsDtos,
            List<InnovationOrganizationTrackOptionDto> innovationOrganizationTrackOptionDtos,
            int currentEditionId,
            string userInterfaceLanguage)
        {
            // Attendee organizations
            foreach (var attendeeOrganizationBaseCommand in this.AttendeeOrganizationBaseCommands)
            {
                attendeeOrganizationBaseCommand.UpdateDropdownProperties(attendeeOrganizationsBaseDtos);
            }

            this.UpdateOrganizationTemplate(attendeeOrganizationsBaseDtos);
            this.UpdateGenders(genders, userInterfaceLanguage);
            this.UpdateIndustries(industries, userInterfaceLanguage);
            this.UpdateCollaboratorRoles(collaboratorRoles, userInterfaceLanguage);
            this.UpdateEditions(editionsDtos, currentEditionId);
        }

        #region Privates

        /// <summary>
        /// Updates the editions.
        /// </summary>
        /// <param name="editions">The editions.</param>
        /// <param name="collaborator">The collaborator.</param>
        /// <param name="currentEditionId">The current edition identifier.</param>
        private void UpdateEditions(IEnumerable<EditionDto> editions, int currentEditionId)
        {
            if (this.EditionsUids == null)
            {
                this.EditionsUids = new List<Guid>();
            }

            this.Editions = editions.Where(e => e.Id != currentEditionId).ToList();
        }

        /// <summary>
        /// Updates the genders.
        /// </summary>
        /// <param name="genders">The genders.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        /// <exception cref="NotImplementedException"></exception>
        private void UpdateGenders(List<CollaboratorGender> genders, string userInterfaceLanguage)
        {
            genders.ForEach(g => g.Translate(userInterfaceLanguage));
            this.CollaboratorGenders = genders.OrderBy(e => e.HasAdditionalInfo).ThenBy(e => e.Name);
        }

        /// <summary>
        /// Updates the genders.
        /// </summary>
        /// <param name="genders">The genders.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        /// <exception cref="NotImplementedException"></exception>
        private void UpdateIndustries(List<CollaboratorIndustry> industries, string userInterfaceLanguage)
        {
            industries.ForEach(g => g.Translate(userInterfaceLanguage));
            this.CollaboratorIndustries = industries.OrderBy(e => e.HasAdditionalInfo).ThenBy(e => e.Name);
        }

        /// <summary>
        /// Updates the genders.
        /// </summary>
        /// <param name="genders">The genders.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        /// <exception cref="NotImplementedException"></exception>
        private void UpdateCollaboratorRoles(List<CollaboratorRole> roles, string userInterfaceLanguage)
        {
            roles.ForEach(g => g.Translate(userInterfaceLanguage));
            this.CollaboratorRoles = roles.OrderBy(e => e.HasAdditionalInfo).ThenBy(e => e.Name);
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
            this.TemplateAttendeeOrganizationBaseCommands = new List<AttendeeOrganizationBaseCommand>
            {
                new AttendeeOrganizationBaseCommand(null, attendeeOrganizationsBaseDtos)
            };
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
                var jobTitle = entity?.JobTitleBaseDtos?.FirstOrDefault(d => d.LanguageDto.Code == languageDto.Code);
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
                var miniBio = entity?.MiniBioBaseDtos?.FirstOrDefault(d => d.LanguageDto.Code == languageDto.Code);
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

        /// <summary>
        /// Updates the roles.
        /// </summary>
        /// <param name="roles">The roles.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        /// <returns></returns>
        private void UpdateRoles(List<Role> roles, string userInterfaceLanguage)
        {
            //roles.ForEach(g => g.Translate(userInterfaceLanguage));
            this.Roles = roles;
        }

        /// <summary>
        /// Updates the genders.
        /// </summary>
        /// <param name="genders">The genders.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        /// <exception cref="NotImplementedException"></exception>
        private void UpdateCollaboratorTypes(List<CollaboratorType> collaboratorTypes, string userInterfaceLanguage)
        {
            //collaboratorTypes.ForEach(g => g.Translate(userInterfaceLanguage));
            this.CollaboratorTypes = collaboratorTypes;
        }

        /// <summary>
        /// Updates the activities.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="activities">The activities.</param>
        private void UpdateActivities(CollaboratorDto entity, List<Activity> activities)
        {
            this.AttendeeCollaboratorActivities = new List<AttendeeCollaboratorActivityBaseCommand>();
            if (activities?.Any() == true)
            {
                foreach (var activity in activities)
                {
                    var attendeeCollaboratorActivityDto = entity?.AttendeeCollaboratorActivityDtos?.FirstOrDefault(oad => oad.ActivityUid == activity.Uid);
                    this.AttendeeCollaboratorActivities.Add(attendeeCollaboratorActivityDto != null ? new AttendeeCollaboratorActivityBaseCommand(attendeeCollaboratorActivityDto) :
                                                                                                      new AttendeeCollaboratorActivityBaseCommand(activity));
                }
            }
        }

        /// <summary>
        /// Updates the interests.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="interestDtos">The interest dtos.</param>
        private void UpdateInterests(CollaboratorDto entity, List<InterestDto> interestDtos)
        {
            this.AttendeeCollaboratorInterests = new List<AttendeeCollaboratorInterestBaseCommand>();

            if (interestDtos?.Any() == true)
            {
                foreach (var interestDto in interestDtos)
                {
                    var attendeeCollaboratorInterestDto = entity?.AttendeeCollaboratorInterestDtos?.FirstOrDefault(oad => oad.InterestUid == interestDto.Interest.Uid);
                    this.AttendeeCollaboratorInterests.Add(attendeeCollaboratorInterestDto != null ? new AttendeeCollaboratorInterestBaseCommand(attendeeCollaboratorInterestDto) :
                                                                                                     new AttendeeCollaboratorInterestBaseCommand(interestDto));
                }
            }
        }

        /// <summary>
        /// Updates the innovation organization track options.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="innovationOrganizationTrackOptionDtos">The innovation organization track option dtos.</param>
        private void UpdateInnovationOrganizationTrackOptions(CollaboratorDto entity, List<InnovationOrganizationTrackOptionDto> innovationOrganizationTrackOptionDtos)
        {
            //this.AttendeeCollaboratorInnovationOrganizationTracks = new List<AttendeeCollaboratorInnovationOrganizationTrackBaseCommand>();
            //if (innovationOrganizationTrackOptionDtos?.Any() == true)
            //{
            //    foreach (var innovationOrganizationTrackOptionDto in innovationOrganizationTrackOptionDtos)
            //    {
            //        var attendeeCollaboratorInterestDto = entity?.AttendeeCollaboratorInnovationOrganizationTrackDtos?.FirstOrDefault(dto => dto.InnovationOrganizationTrackOption.Uid == innovationOrganizationTrackOptionDto.Uid);
            //        this.AttendeeCollaboratorInterests.Add(attendeeCollaboratorInterestDto != null ? new AttendeeCollaboratorInnovationOrganizationTrackBaseCommand(attendeeCollaboratorInterestDto) :
            //                                                                                         new AttendeeCollaboratorInnovationOrganizationTrackBaseCommand(innovationOrganizationTrackOptionDto));
            //    }
            //}
        }

        #endregion
    }
}