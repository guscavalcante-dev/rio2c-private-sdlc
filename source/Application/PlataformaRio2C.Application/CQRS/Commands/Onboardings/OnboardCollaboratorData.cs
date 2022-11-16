// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 09-06-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 10-17-2022
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

        [Display(Name = "CollaboratorIndustry", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public Guid? CollaboratorIndustryUid { get; set; }

        public IEnumerable<CollaboratorIndustry> CollaboratorIndustries { get; set; }

        public bool CollaboratorIndustryAdditionalInfoRequired { get; set; }

        [Display(Name = "EnterYourIndustry", ResourceType = typeof(Labels))]
        [StringLength(300, MinimumLength = 0, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        [RequiredIf("CollaboratorIndustryAdditionalInfoRequired", "True", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public string CollaboratorIndustryAdditionalInfo { get; set; }

        [Display(Name = "Gender", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public Guid? CollaboratorGenderUid { get; set; }

        public IEnumerable<CollaboratorGender> CollaboratorGenders { get; set; }

        public bool CollaboratorGenderAdditionalInfoRequired { get; set; }

        [Display(Name = "AdditionalInfo", ResourceType = typeof(Labels))]
        [StringLength(300, MinimumLength = 0, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        [RequiredIf("CollaboratorGenderAdditionalInfoRequired", "True", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public string CollaboratorGenderAdditionalInfo { get; set; }

        [Display(Name = "Role", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public Guid? CollaboratorRoleUid { get; set; }

        public IEnumerable<CollaboratorRole> CollaboratorRoles { get; set; }

        public bool CollaboratorRoleAdditionalInfoRequired { get; set; }

        [Display(Name = "EnterYourRole", ResourceType = typeof(Labels))]
        [StringLength(300, MinimumLength = 0, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        [RequiredIf("CollaboratorRoleAdditionalInfoRequired", "True", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public string CollaboratorRoleAdditionalInfo { get; set; }

        [Display(Name = "HasAnySpecialNeeds", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public bool? HasAnySpecialNeeds { get; set; }

        [Display(Name = "WhichSpecialNeedsQ", ResourceType = typeof(Labels))]
        [RequiredIf("HasAnySpecialNeeds", "True", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [StringLength(300, MinimumLength = 0, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string SpecialNeedsDescription { get; set; }

        [Display(Name = "HaveYouBeenToRio2CBefore", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public bool? HaveYouBeenToRio2CBefore { get; set; }

        //[Display(Name = "PreviousEditions", ResourceType = typeof(Labels))]
        public IEnumerable<Guid> EditionsUids { get; set; }

        [RequiredIf("HaveYouBeenToRio2CBefore", "True", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "SelectAtLeastOneOption")]
        public bool? HasEditionSelected { get; set; }

        public IEnumerable<EditionDto> Editions { get; set; }

        public List<CollaboratorJobTitleBaseCommand> JobTitles { get; set; }
        public List<CollaboratorMiniBioBaseCommand> MiniBios { get; set; }
        public CropperImageBaseCommand CropperImage { get; set; }

        public Guid CollaboratorUid { get; set; }

        /// <summary></summary>
        /// <param name="collaborator"></param>
        /// <param name="genders"></param>
        /// <param name="industries"></param>
        /// <param name="roles"></param>
        /// <param name="languagesDtos"></param>
        /// <param name="editionsDtos"></param>
        /// <param name="currentEditionId"></param>
        /// <param name="isJobTitleRequired"></param>
        /// <param name="isMiniBioRequired"></param>
        /// <param name="isImageRequired"></param>
        /// <param name="userInterfaceLanguage"></param>
        public OnboardCollaboratorData(
            CollaboratorDto collaborator,
            List<CollaboratorGender> genders,
            List<CollaboratorIndustry> industries,
            List<CollaboratorRole> roles,
            List<LanguageDto> languagesDtos,
            List<EditionDto> editionsDtos,
            int currentEditionId,
            bool isJobTitleRequired,
            bool isMiniBioRequired,
            bool isImageRequired,
            string userInterfaceLanguage)
        {
            this.SharePublicEmail = !string.IsNullOrEmpty(collaborator.PublicEmail) ? (bool?)true : null;
            this.PublicEmail = collaborator?.PublicEmail;
            this.BirthDate = collaborator?.BirthDate;
            this.PublicEmail = collaborator?.PublicEmail;
            
            this.Website = collaborator?.Website;
            this.Linkedin = collaborator?.Linkedin;
            this.Twitter = collaborator?.Twitter;
            this.Instagram = collaborator?.Instagram;
            this.Youtube = collaborator?.Youtube;
            this.HasAnySpecialNeeds = collaborator?.HasAnySpecialNeeds;
            this.SpecialNeedsDescription = collaborator?.SpecialNeedsDescription;
            this.HaveYouBeenToRio2CBefore = collaborator?.EditionsUids?.Count > 0;
            this.EditionsUids = collaborator?.EditionsUids;

            this.UpdateEditions(editionsDtos, currentEditionId);

            this.UpdateGenders(genders, 
                userInterfaceLanguage, 
                collaborator?.Gender?.Uid, 
                collaborator?.CollaboratorGenderAdditionalInfo);

            this.UpdateIndustries(industries, 
                userInterfaceLanguage, 
                collaborator?.Industry?.Uid, 
                collaborator?.CollaboratorIndustryAdditionalInfo);

            this.UpdateRoles(roles, 
                userInterfaceLanguage, 
                collaborator?.CollaboratorRole?.Uid, 
                collaborator?.CollaboratorRoleAdditionalInfo);

            this.UpdateJobTitles(collaborator, languagesDtos, isJobTitleRequired);
            this.UpdateMiniBios(collaborator, languagesDtos, isMiniBioRequired);
            this.UpdateCropperImage(collaborator, isImageRequired);
        }

        /// <summary>Initializes a new instance of the <see cref="OnboardCollaboratorData"/> class.</summary>
        public OnboardCollaboratorData()
        {
        }

        /// <summary>Updates the data.</summary>
        /// <param name="genders">The genders.</param>
        /// <param name="industries">The industries.</param>
        /// <param name="roles">The roles.</param>
        /// <param name="editionsDtos">The editions dtos.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        public void UpdateData(List<CollaboratorGender> genders,
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

        /// <summary>Updates the pre send properties.</summary>
        /// <param name="collaboratorUid">The collaborator uid.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="userUid">The user uid.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="editionUid">The edition uid.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        public void UpdatePreSendProperties(
            Guid collaboratorUid,
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

        #endregion
    }
}