// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 09-06-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 01-17-2024
// ***********************************************************************
// <copyright file="OnboardCollaboratorData.cs" company="Softo">
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
    /// <summary>OnboardCollaboratorData</summary>
    public class OnboardCollaboratorData : BaseCommand
    {
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "SelectAnOption")]
        public bool? SharePublicEmail { get; set; }

        [Display(Name = "Email", ResourceType = typeof(Labels))]
        [RequiredIf("SharePublicEmail", "True", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [EmailAddress(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "EmailISInvalid")]
        [StringLength(256, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        [DataType(DataType.EmailAddress)]
        public string PublicEmail { get; set; }

        [Display(Name = "BirthDate", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public DateTime? BirthDate { get; set; }

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

        #region CollaboratorIndustry

        [Display(Name = "CollaboratorIndustry", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public Guid? CollaboratorIndustryUid { get; set; }

        public IEnumerable<CollaboratorIndustry> CollaboratorIndustries { get; set; }

        public bool CollaboratorIndustryAdditionalInfoRequired { get; set; }

        [Display(Name = "EnterYourIndustry", ResourceType = typeof(Labels))]
        [StringLength(300, MinimumLength = 0, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        [RequiredIf("CollaboratorIndustryAdditionalInfoRequired", "True", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public string CollaboratorIndustryAdditionalInfo { get; set; }

        #endregion

        #region CollaboratorGender

        [Display(Name = "Gender", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public Guid? CollaboratorGenderUid { get; set; }

        public IEnumerable<CollaboratorGender> CollaboratorGenders { get; set; }

        public bool CollaboratorGenderAdditionalInfoRequired { get; set; }

        [Display(Name = "AdditionalInfo", ResourceType = typeof(Labels))]
        [StringLength(300, MinimumLength = 0, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        [RequiredIf("CollaboratorGenderAdditionalInfoRequired", "True", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public string CollaboratorGenderAdditionalInfo { get; set; }

        #endregion

        #region CollaboratorRole

        [Display(Name = "Role", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public Guid? CollaboratorRoleUid { get; set; }

        public IEnumerable<CollaboratorRole> CollaboratorRoles { get; set; }

        public bool CollaboratorRoleAdditionalInfoRequired { get; set; }

        [Display(Name = "EnterYourRole", ResourceType = typeof(Labels))]
        [StringLength(300, MinimumLength = 0, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        [RequiredIf("CollaboratorRoleAdditionalInfoRequired", "True", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public string CollaboratorRoleAdditionalInfo { get; set; }

        #endregion

        #region SpecialNeeds

        [Display(Name = "HasAnySpecialNeeds", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public bool? HasAnySpecialNeeds { get; set; }

        [Display(Name = "WhichSpecialNeedsQ", ResourceType = typeof(Labels))]
        [RequiredIf("HasAnySpecialNeeds", "True", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [StringLength(300, MinimumLength = 0, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string SpecialNeedsDescription { get; set; }

        #endregion

        #region Editions

        [Display(Name = "HaveYouBeenToRio2CBefore", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public bool? HaveYouBeenToRio2CBefore { get; set; }

        //[Display(Name = "PreviousEditions", ResourceType = typeof(Labels))]
        public IEnumerable<Guid> EditionsUids { get; set; }

        [RequiredIf("HaveYouBeenToRio2CBefore", "True", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "SelectAtLeastOneOption")]
        public bool? HasEditionSelected { get; set; }

        #endregion

        public List<CollaboratorJobTitleBaseCommand> JobTitles { get; set; }
        public List<CollaboratorMiniBioBaseCommand> MiniBios { get; set; }
        public CropperImageBaseCommand CropperImage { get; set; }

        public Guid CollaboratorUid { get; set; }
        public UserAccessControlDto UserAccessControlDto { get; set; }

        #region Music Player Executive

        [Display(Name = "Activities", ResourceType = typeof(Labels))]
        public List<AttendeeCollaboratorActivityBaseCommand> MusicAttendeeCollaboratorActivities { get; set; }

        [Display(Name = "TargetAudiences", ResourceType = typeof(Labels))]
        public List<AttendeeCollaboratorTargetAudienceBaseCommand> MusicAttendeeCollaboratorTargetAudiences { get; set; }

        [Display(Name = "Interests", ResourceType = typeof(Labels))]
        public InterestBaseCommand[][] MusicInterests { get; set; }

        #endregion

        #region Innovation Player Executive

        [Display(Name = "Verticals", ResourceType = typeof(Labels))]
        public List<InnovationOrganizationTrackOptionBaseCommand> InnovationOrganizationTrackGroups { get; set; }

        [Display(Name = "Activities", ResourceType = typeof(Labels))]
        public List<AttendeeCollaboratorActivityBaseCommand> InnovationAttendeeCollaboratorActivities { get; set; }

        [Display(Name = "Interests", ResourceType = typeof(Labels))]
        public InterestBaseCommand[][] InnovationInterests { get; set; }

        #endregion


        #region Dropdowns Properties

        public IEnumerable<EditionDto> Editions { get; set; }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="OnboardCollaboratorData" /> class.
        /// </summary>
        /// <param name="entity">The collaborator.</param>
        /// <param name="genders">The genders.</param>
        /// <param name="industries">The industries.</param>
        /// <param name="roles">The roles.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        /// <param name="editionsDtos">The editions dtos.</param>
        /// <param name="musicAttendeeCollaboratorActivities">The activities.</param>
        /// <param name="musicAttendeeCollaboratorTargetAudiences">The target audiences.</param>
        /// <param name="musicInterestsDtos">The music interests dtos.</param>
        /// <param name="innovationOrganizationTrackOptionDtos">The innovation organization track option dtos.</param>
        /// <param name="innovationAttendeeCollaboratorActivities">The innovation attendee collaborator activities.</param>
        /// <param name="innovationInterestsDtos">The innovation interests dtos.</param>
        /// <param name="currentEditionId">The current edition identifier.</param>
        /// <param name="isJobTitleRequired">if set to <c>true</c> [is job title required].</param>
        /// <param name="isMiniBioRequired">if set to <c>true</c> [is mini bio required].</param>
        /// <param name="isImageRequired">if set to <c>true</c> [is image required].</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        public OnboardCollaboratorData(
            CollaboratorDto entity,
            List<CollaboratorGender> genders,
            List<CollaboratorIndustry> industries,
            List<CollaboratorRole> roles,
            List<LanguageDto> languagesDtos,
            List<EditionDto> editionsDtos,
            List<Activity> musicAttendeeCollaboratorActivities,
            List<TargetAudience> musicAttendeeCollaboratorTargetAudiences,
            List<InterestDto> musicInterestsDtos,
            List<InnovationOrganizationTrackOptionDto> innovationOrganizationTrackOptionDtos,
            List<Activity> innovationAttendeeCollaboratorActivities,
            List<InterestDto> innovationInterestsDtos,
            int currentEditionId,
            bool isJobTitleRequired,
            bool isMiniBioRequired,
            bool isImageRequired,
            string userInterfaceLanguage)
        {
            this.SharePublicEmail = !string.IsNullOrEmpty(entity.PublicEmail) ? (bool?)true : null;
            this.PublicEmail = entity?.PublicEmail;
            this.BirthDate = entity?.BirthDate;

            this.Website = entity?.Website;
            this.Linkedin = entity?.Linkedin;
            this.Twitter = entity?.Twitter;
            this.Instagram = entity?.Instagram;
            this.Youtube = entity?.Youtube;
            this.HasAnySpecialNeeds = entity?.HasAnySpecialNeeds;
            this.SpecialNeedsDescription = entity?.SpecialNeedsDescription;
            this.HaveYouBeenToRio2CBefore = entity?.EditionsUids?.Count > 0;
            this.EditionsUids = entity?.EditionsUids;

            this.UpdateEditions(editionsDtos, currentEditionId);

            this.UpdateGenders(genders,
                userInterfaceLanguage,
                entity?.Gender?.Uid,
                entity?.CollaboratorGenderAdditionalInfo);

            this.UpdateIndustries(industries,
                userInterfaceLanguage,
                entity?.Industry?.Uid,
                entity?.CollaboratorIndustryAdditionalInfo);

            this.UpdateRoles(roles,
                userInterfaceLanguage,
                entity?.CollaboratorRole?.Uid,
                entity?.CollaboratorRoleAdditionalInfo);

            this.UpdateJobTitles(entity, languagesDtos, isJobTitleRequired);
            this.UpdateMiniBios(entity, languagesDtos, isMiniBioRequired);
            this.UpdateMusicAttendeeCollaboratorActivities(entity, musicAttendeeCollaboratorActivities);
            this.UpdateMusicTargetAudiences(entity, musicAttendeeCollaboratorTargetAudiences);
            this.UpdateMusicInterests(entity, musicInterestsDtos);
            this.UpdateInnovationOrganizationTrackOptionGroups(entity, innovationOrganizationTrackOptionDtos);
            this.UpdateInnovationAttendeeCollaboratorActivities(entity, innovationAttendeeCollaboratorActivities);
            this.UpdateInnovationInterests(entity, innovationInterestsDtos);
            this.UpdateCropperImage(entity, isImageRequired);
        }

        /// <summary>Initializes a new instance of the <see cref="OnboardCollaboratorData"/> class.</summary>
        public OnboardCollaboratorData()
        {
        }

        /// <summary>
        /// Updates the models and lists.
        /// </summary>
        /// <param name="genders">The genders.</param>
        /// <param name="industries">The industries.</param>
        /// <param name="roles">The roles.</param>
        /// <param name="editionsDtos">The editions dtos.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        public void UpdateModelsAndLists(
            List<CollaboratorGender> genders,
            List<CollaboratorIndustry> industries,
            List<CollaboratorRole> roles,
            List<EditionDto> editionsDtos,
            int editionId,
            string userInterfaceLanguage)
        {
            this.UpdateEditions(editionsDtos, editionId);
            this.UpdateGenders(genders, userInterfaceLanguage, null, null);
            this.UpdateIndustries(industries, userInterfaceLanguage, null, null);
            this.UpdateRoles(roles, userInterfaceLanguage, null, null);
        }

        /// <summary>
        /// Updates the pre send properties.
        /// </summary>
        /// <param name="collaboratorUid">The collaborator uid.</param>
        /// <param name="userAccessControlDto">The user access control dto.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="userUid">The user uid.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="editionUid">The edition uid.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        public void UpdatePreSendProperties(
            Guid collaboratorUid,
            UserAccessControlDto userAccessControlDto,
            int userId,
            Guid userUid,
            int? editionId,
            Guid? editionUid,
            string userInterfaceLanguage)
        {
            if (!HaveYouBeenToRio2CBefore ?? false)
            {
                EditionsUids = new List<Guid>();
            }

            this.CollaboratorUid = collaboratorUid;
            this.UserAccessControlDto = userAccessControlDto;

            this.UpdatePreSendProperties(userId, userUid, editionId, editionUid, userInterfaceLanguage);
        }

        #region Private Methods

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

        /// <summary>Updates the editions.</summary>
        /// <param name="editionsDtos">The editions dtos.</param>
        /// <param name="currentEditionId">The current edition identifier.</param>
        private void UpdateEditions(List<EditionDto> editionsDtos, int currentEditionId)
        {
            if (this.EditionsUids == null)
                this.EditionsUids = new List<Guid>();

            this.Editions = editionsDtos.Where(e => e.Id != currentEditionId).ToList();
        }

        /// <summary>
        /// Updates the genders.
        /// </summary>
        /// <param name="genders">The genders.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        /// <param name="collaboratorGenderUid">The collaborator gender uid.</param>
        /// <param name="collaboratorGenderAdditionalInfo">The collaborator gender additional information.</param>
        private void UpdateGenders(List<CollaboratorGender> genders, string userInterfaceLanguage, Guid? collaboratorGenderUid, string collaboratorGenderAdditionalInfo)
        {
            genders.ForEach(g => g.Translate(userInterfaceLanguage));
            this.CollaboratorGenders = genders.OrderBy(e => e.HasAdditionalInfo).ThenBy(e => e.Name);
            this.CollaboratorGenderUid = collaboratorGenderUid;
            this.CollaboratorGenderAdditionalInfo = collaboratorGenderAdditionalInfo;
        }

        /// <summary>
        /// Updates the industries.
        /// </summary>
        /// <param name="industries">The industries.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        /// <param name="collaboratorIndustryUid">The collaborator industry uid.</param>
        /// <param name="collaboratorIndustryAdditionalInfo">The collaborator industry additional information.</param>
        private void UpdateIndustries(List<CollaboratorIndustry> industries, string userInterfaceLanguage, Guid? collaboratorIndustryUid, string collaboratorIndustryAdditionalInfo)
        {
            industries.ForEach(g => g.Translate(userInterfaceLanguage));
            this.CollaboratorIndustries = industries.OrderBy(e => e.HasAdditionalInfo).ThenBy(e => e.Name);
            this.CollaboratorIndustryUid = collaboratorIndustryUid;
            this.CollaboratorIndustryAdditionalInfo = collaboratorIndustryAdditionalInfo;
        }

        /// <summary>
        /// Updates the roles.
        /// </summary>
        /// <param name="roles">The roles.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        /// <param name="collaboratorRoleUid">The collaborator role uid.</param>
        /// <param name="collaboratorRoleAdditionalInfo">The collaborator role additional information.</param>
        private void UpdateRoles(List<CollaboratorRole> roles, string userInterfaceLanguage, Guid? collaboratorRoleUid, string collaboratorRoleAdditionalInfo)
        {
            roles.ForEach(g => g.Translate(userInterfaceLanguage));
            this.CollaboratorRoles = roles.OrderBy(e => e.HasAdditionalInfo).ThenBy(e => e.Name);
            this.CollaboratorRoleUid = collaboratorRoleUid;
            this.CollaboratorRoleAdditionalInfo = collaboratorRoleAdditionalInfo;
        }

        #region Music Player Executive

        /// <summary>
        /// Updates the music attendee collaborator activities.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="activities">The activities.</param>
        /// <param name="isActivitiesRequired">if set to <c>true</c> [is activities required].</param>
        private void UpdateMusicAttendeeCollaboratorActivities(CollaboratorDto entity, List<Activity> activities)
        {
            if (activities?.Any() == true)
            {
                this.MusicAttendeeCollaboratorActivities = new List<AttendeeCollaboratorActivityBaseCommand>();
                foreach (var activity in activities)
                {
                    var attendeeCollaboratorActivityDto = entity?.AttendeeCollaboratorActivityDtos?.FirstOrDefault(oad => oad.ActivityUid == activity.Uid);
                    this.MusicAttendeeCollaboratorActivities.Add(attendeeCollaboratorActivityDto != null ? new AttendeeCollaboratorActivityBaseCommand(attendeeCollaboratorActivityDto) :
                                                                                                           new AttendeeCollaboratorActivityBaseCommand(activity));
                }
            }
        }

        /// <summary>
        /// Updates the target audiences.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="targetAudiences">The target audiences.</param>
        private void UpdateMusicTargetAudiences(CollaboratorDto entity, List<TargetAudience> targetAudiences)
        {
            if (targetAudiences?.Any() == true)
            {
                this.MusicAttendeeCollaboratorTargetAudiences = new List<AttendeeCollaboratorTargetAudienceBaseCommand>();
                foreach (var targetAudience in targetAudiences)
                {
                    var attendeeCollaboratorTargetAudience = entity?.AttendeeCollaboratorTargetAudiencesDtos?.FirstOrDefault(ota => ota.TargetAudienceUid == targetAudience.Uid);
                    this.MusicAttendeeCollaboratorTargetAudiences.Add(attendeeCollaboratorTargetAudience != null ? new AttendeeCollaboratorTargetAudienceBaseCommand(attendeeCollaboratorTargetAudience) :
                                                                                                                   new AttendeeCollaboratorTargetAudienceBaseCommand(targetAudience));
                }
            }
        }

        /// <summary>
        /// Updates the music interests.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="interestsDtos">The interests dtos.</param>
        private void UpdateMusicInterests(CollaboratorDto entity, List<InterestDto> interestsDtos)
        {
            if (interestsDtos?.Any() == true)
            {
                var interestsBaseCommands = new List<InterestBaseCommand>();
                foreach (var interestDto in interestsDtos)
                {
                    var attendeeCollaboratorInterestDto = entity?.AttendeeCollaboratorInterestDtos?.FirstOrDefault(oad => oad.Interest.Uid == interestDto.Interest.Uid);
                    interestsBaseCommands.Add(attendeeCollaboratorInterestDto != null ? new InterestBaseCommand(attendeeCollaboratorInterestDto) :
                                                                                        new InterestBaseCommand(interestDto));
                }

                var groupedInterestsDtos = interestsBaseCommands?
                                                .GroupBy(i => new { i.InterestGroupUid, i.InterestGroupName, i.InterestGroupDisplayOrder })?
                                                .OrderBy(g => g.Key.InterestGroupDisplayOrder)?
                                                .ToList();

                if (groupedInterestsDtos?.Any() == true)
                {
                    this.MusicInterests = new InterestBaseCommand[groupedInterestsDtos.Count][];
                    for (int i = 0; i < groupedInterestsDtos.Count; i++)
                    {
                        this.MusicInterests[i] = groupedInterestsDtos[i].ToArray();
                    }
                }
            }
        }

        #endregion

        #region Innovation Player Executive

        /// <summary>
        /// Updates the innovation organization track option groups.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="innovationOrganizationTrackOptionDtos">The innovation organization track option dtos.</param>
        private void UpdateInnovationOrganizationTrackOptionGroups(CollaboratorDto entity, List<InnovationOrganizationTrackOptionDto> innovationOrganizationTrackOptionDtos)
        {
            if (innovationOrganizationTrackOptionDtos?.Any() == true)
            {
                this.InnovationOrganizationTrackGroups = new List<InnovationOrganizationTrackOptionBaseCommand>();

                var selectedInnovationOrganizationTrackOptionGroupsUids = entity?.InnovationOrganizationTrackOptionGroupDtos
                                                                                    ?.GroupBy(aciotDto => aciotDto.InnovationOrganizationTrackOptionGroup?.Uid)
                                                                                    ?.Select(group => group.Key);

                foreach (var innovationOrganizationTrackOptionGroup in innovationOrganizationTrackOptionDtos.GroupBy(dto => dto.InnovationOrganizationTrackOptionGroup))
                {
                    this.InnovationOrganizationTrackGroups.Add(
                        new InnovationOrganizationTrackOptionBaseCommand(
                            innovationOrganizationTrackOptionGroup.Key,
                            selectedInnovationOrganizationTrackOptionGroupsUids?.Contains(innovationOrganizationTrackOptionGroup.Key?.Uid) == true));
                }
            }
        }

        /// <summary>
        /// Updates the innovation attendee collaborator activities.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="activities">The activities.</param>
        /// <param name="isActivitiesRequired">if set to <c>true</c> [is activities required].</param>
        private void UpdateInnovationAttendeeCollaboratorActivities(CollaboratorDto entity, List<Activity> activities)
        {
            if (activities?.Any() == true)
            {
                this.InnovationAttendeeCollaboratorActivities = new List<AttendeeCollaboratorActivityBaseCommand>();
                foreach (var activity in activities)
                {
                    var attendeeCollaboratorActivityDto = entity?.AttendeeCollaboratorActivityDtos?.FirstOrDefault(oad => oad.ActivityUid == activity.Uid);
                    this.InnovationAttendeeCollaboratorActivities.Add(attendeeCollaboratorActivityDto != null ? new AttendeeCollaboratorActivityBaseCommand(attendeeCollaboratorActivityDto) :
                                                                                                                new AttendeeCollaboratorActivityBaseCommand(activity));
                }
            }
        }

        /// <summary>
        /// Updates the music interests.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="interestsDtos">The interests dtos.</param>
        private void UpdateInnovationInterests(CollaboratorDto entity, List<InterestDto> interestsDtos)
        {
            if (interestsDtos?.Any() == true)
            {
                var interestsBaseCommands = new List<InterestBaseCommand>();

                foreach (var interestDto in interestsDtos)
                {
                    var attendeeCollaboratorInterestDto = entity?.AttendeeCollaboratorInterestDtos?.FirstOrDefault(oad => oad?.Interest?.Uid == interestDto?.Interest?.Uid);
                    interestsBaseCommands.Add(attendeeCollaboratorInterestDto != null ? new InterestBaseCommand(attendeeCollaboratorInterestDto) :
                                                                                        new InterestBaseCommand(interestDto));
                }

                var groupedInterestsDtos = interestsBaseCommands?
                                                .GroupBy(i => new { i.InterestGroupUid, i.InterestGroupName, i.InterestGroupDisplayOrder })?
                                                .OrderBy(g => g.Key.InterestGroupDisplayOrder)?
                                                .ToList();

                if (groupedInterestsDtos?.Any() == true)
                {
                    this.InnovationInterests = new InterestBaseCommand[groupedInterestsDtos.Count][];
                    for (int i = 0; i < groupedInterestsDtos.Count; i++)
                    {
                        this.InnovationInterests[i] = groupedInterestsDtos[i].ToArray();
                    }
                }
            }
        }

        #endregion

        #endregion
    }
}