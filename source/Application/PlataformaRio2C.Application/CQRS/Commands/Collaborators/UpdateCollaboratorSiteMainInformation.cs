// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 12-16-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 04-11-2023
// ***********************************************************************
// <copyright file="UpdateCollaboratorSiteMainInformation.cs" company="Softo">
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

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>UpdateCollaboratorSiteMainInformation</summary>
    public class UpdateCollaboratorSiteMainInformation : UpdateCollaboratorMainInformationBaseCommand
    {
        [Display(Name = "FirstName", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [StringLength(100, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string FirstName { get; set; }

        [Display(Name = "LastNames", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [StringLength(200, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string LastNames { get; set; }

        [Display(Name = "BadgeName", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [StringLength(50, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string Badge { get; set; }

        [Display(Name = "PhoneNumber", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [StringLength(50, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string PhoneNumber { get; set; }

        [Display(Name = "CellPhone", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [StringLength(50, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string CellPhone { get; set; }
        
        [Display(Name = "BirthDate", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]        
        public DateTime? BirthDate { get; set; }
        
        [Display(Name = "CollaboratorIndustry", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public Guid? CollaboratorIndustryUid { get; set; }

        public IEnumerable<CollaboratorIndustry> CollaboratorIndustries { get; set; }

        public bool CollaboratorIndustryAdditionalInfoRequired {get;set;}

        [Display(Name = "EnterYourIndustry", ResourceType = typeof(Labels))]
        [StringLength(300, MinimumLength = 0, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        [RequiredIf("CollaboratorIndustryAdditionalInfoRequired", "True", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public string CollaboratorIndustryAdditionalInfo  { get; set; }

        [Display(Name = "Gender", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public Guid? CollaboratorGenderUid  { get; set; }

        public IEnumerable<CollaboratorGender> CollaboratorGenders { get; set; }
        
        public bool CollaboratorGenderAdditionalInfoRequired {get;set;}

        [Display(Name = "AdditionalInfo", ResourceType = typeof(Labels))]
        [StringLength(300, MinimumLength = 0, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        [RequiredIf("CollaboratorGenderAdditionalInfoRequired", "True", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public string CollaboratorGenderAdditionalInfo  { get; set; }
        
        [Display(Name = "Role", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public Guid? CollaboratorRoleUid { get; set; }

        public IEnumerable<CollaboratorRole> CollaboratorRoles { get; set; }
        
        public bool CollaboratorRoleAdditionalInfoRequired {get;set;}

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
        
        [RequiredIf("HaveYouBeenToRio2CBefore", "True", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "SelectAtLeastOneOption")]
        public bool? HasEditionSelected { get;set; }
        
        public IEnumerable<Guid> EditionsUids { get; set; }

        public IEnumerable<EditionDto> Editions { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateCollaboratorSiteMainInformation" /> class.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="genders">The genders.</param>
        /// <param name="industries">The industries.</param>
        /// <param name="roles">The roles.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        /// <param name="editionsDtos">The editions dtos.</param>
        /// <param name="currentEditionId">The current edition identifier.</param>
        /// <param name="isJobTitleRequired">if set to <c>true</c> [is job title required].</param>
        /// <param name="isMiniBioRequired">if set to <c>true</c> [is mini bio required].</param>
        /// <param name="isImageRequired">if set to <c>true</c> [is image required].</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        public UpdateCollaboratorSiteMainInformation(
            AttendeeCollaboratorSiteMainInformationWidgetDto entity, 
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
            : base (entity, languagesDtos, isJobTitleRequired, isMiniBioRequired, isImageRequired)
        {
            this.UpdateGenders(genders, userInterfaceLanguage);
            this.UpdateIndustries(industries, userInterfaceLanguage);
            this.UpdateRoles(roles, userInterfaceLanguage);
            this.UpdateEditions(editionsDtos, entity.Collaborator, currentEditionId);

            this.FirstName = entity?.Collaborator?.FirstName;
            this.LastNames = entity?.Collaborator?.LastNames;
            this.Badge = entity?.Collaborator?.Badge;
            this.PhoneNumber = entity?.Collaborator?.PhoneNumber;
            this.CellPhone = entity?.Collaborator?.CellPhone;            
            this.BirthDate = entity?.Collaborator?.BirthDate;
            this.HasAnySpecialNeeds = entity?.Collaborator?.HasAnySpecialNeeds;
            this.SpecialNeedsDescription = entity?.Collaborator?.SpecialNeedsDescription;
            this.HaveYouBeenToRio2CBefore = entity?.EditionParticipationDtos?.Any() ?? false;
            this.CollaboratorGenderUid = entity?.Collaborator?.Gender?.Uid;
            this.CollaboratorGenderAdditionalInfo = entity?.Collaborator?.CollaboratorGenderAdditionalInfo;
            this.CollaboratorIndustryUid = entity?.Collaborator?.Industry?.Uid;
            this.CollaboratorIndustryAdditionalInfo = entity?.Collaborator?.CollaboratorIndustryAdditionalInfo;
            this.CollaboratorRoleUid = entity?.Collaborator?.Role?.Uid;
            this.CollaboratorRoleAdditionalInfo = entity?.Collaborator?.CollaboratorRoleAdditionalInfo;
        }
        
        /// <summary>Initializes a new instance of the <see cref="UpdateCollaboratorSiteMainInformation"/> class.</summary>
        public UpdateCollaboratorSiteMainInformation()
        {
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

        /// <summary>
        /// Updates the models and lists.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="genders">The genders.</param>
        /// <param name="industries">The industries.</param>
        /// <param name="roles">The roles.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        /// <param name="editionsDtos">The editions dtos.</param>
        /// <param name="currentEditionId">The current edition identifier.</param>
        /// <param name="">The .</param>
        /// <returns></returns>
        public void UpdateModelsAndLists(
            AttendeeCollaboratorSiteMainInformationWidgetDto entity,
            List<CollaboratorGender> genders,
            List<CollaboratorIndustry> industries,
            List<CollaboratorRole> roles,
            List<EditionDto> editionsDtos,
            int currentEditionId,
            string userInterfaceLanguage)
        {
            this.UpdateGenders(genders ?? new List<CollaboratorGender>(), userInterfaceLanguage);
            this.UpdateIndustries(industries ?? new List<CollaboratorIndustry>(), userInterfaceLanguage);
            this.UpdateRoles(roles ?? new List<CollaboratorRole>(), userInterfaceLanguage);
            this.UpdateEditions(editionsDtos ?? new List<EditionDto>(), entity.Collaborator, currentEditionId);
        }

        /// <summary>
        /// Updates the editions.
        /// </summary>
        /// <param name="editions">The editions.</param>
        /// <param name="collaborator">The collaborator.</param>
        /// <param name="currentEditionId">The current edition identifier.</param>
        private void UpdateEditions(IEnumerable<EditionDto> editions, Collaborator collaborator, int currentEditionId)
        {
            this.Editions = editions.Where(e => e.Id != currentEditionId).ToList();

            if (!collaborator.EditionParticipantions.Any(p => !p.IsDeleted))
            {
                this.EditionsUids = new List<Guid>();
                return;
            }

            this.HaveYouBeenToRio2CBefore = true;
            this.EditionsUids = editions.Where(e => collaborator.EditionParticipantions.Any(p => p.EditionId == e.Id && !p.IsDeleted)).Select(e => e.Uid).ToList();
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
        private void UpdateRoles(List<CollaboratorRole> roles, string userInterfaceLanguage)
        {
            roles.ForEach(g => g.Translate(userInterfaceLanguage));
            this.CollaboratorRoles = roles.OrderBy(e => e.HasAdditionalInfo).ThenBy(e => e.Name);
        }
    }
}