// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 07-19-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-19-2021
// ***********************************************************************
// <copyright file="UpdateInnovationCollaboratorAdminMainInformation.cs" company="Softo">
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
    /// <summary>UpdateInnovationCollaboratorAdminMainInformation</summary>
    public class UpdateInnovationCollaboratorAdminMainInformation : UpdateCollaboratorMainInformationBaseCommand
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

        [Display(Name = "BadgeName", ResourceType = typeof(Labels))]
        [StringLength(50, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string Badge { get; set; }

        [Display(Name = "PhoneNumber", ResourceType = typeof(Labels))]
        [StringLength(50, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string PhoneNumber { get; set; }

        [Display(Name = "CellPhone", ResourceType = typeof(Labels))]
        [StringLength(50, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string CellPhone { get; set; }

        public string CollaboratorTypeName { get; private set; }
        
        [Display(Name = "BirthDate", ResourceType = typeof(Labels))]
        public DateTime? BirthDate { get; set; }
        
        [Display(Name = "CollaboratorIndustry", ResourceType = typeof(Labels))]
        public Guid? CollaboratorIndustryUid { get; set; }

        public IEnumerable<CollaboratorIndustry> CollaboratorIndustries { get; set; }

        public bool CollaboratorIndustryAdditionalInfoRequired {get;set;}

        [Display(Name = "EnterYourIndustry", ResourceType = typeof(Labels))]
        [StringLength(300, MinimumLength = 0, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        [RequiredIf("CollaboratorIndustryAdditionalInfoRequired", "True", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public string CollaboratorIndustryAdditionalInfo  { get; set; }

        [Display(Name = "Gender", ResourceType = typeof(Labels))]
        public Guid? CollaboratorGenderUid  { get; set; }

        public IEnumerable<CollaboratorGender> CollaboratorGenders { get; set; }
        
        public bool CollaboratorGenderAdditionalInfoRequired {get;set;}

        [Display(Name = "AdditionalInfo", ResourceType = typeof(Labels))]
        [StringLength(300, MinimumLength = 0, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        [RequiredIf("CollaboratorGenderAdditionalInfoRequired", "True", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public string CollaboratorGenderAdditionalInfo  { get; set; }
        
        [Display(Name = "Role", ResourceType = typeof(Labels))]
        public Guid? CollaboratorRoleUid { get; set; }

        public IEnumerable<CollaboratorRole> CollaboratorRoles { get; set; }
        
        public bool CollaboratorRoleAdditionalInfoRequired {get;set;}

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
        
        public IEnumerable<Guid> EditionsUids { get; set; }

        public IEnumerable<EditionDto> Editions { get; set; }

        [RequiredIf("HaveYouBeenToRio2CBefore", "True", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "SelectAtLeastOneOption")]
        public bool? HasEditionSelected { get;set; }

        /// <summary>Initializes a new instance of the <see cref="UpdateInnovationCollaboratorAdminMainInformation"/> class.</summary>
        /// <param name="entity">The entity.</param>
        /// <param name="genders">The genders.</param>
        /// <param name="industries">The industries.</param>
        /// <param name="roles">The roles.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        /// <param name="editionsDtos">The editions dtos.</param>
        /// <param name="currentEditionId">The current edition identifier.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        public UpdateInnovationCollaboratorAdminMainInformation(
            AttendeeCollaboratorSiteMainInformationWidgetDto entity, 
            List<CollaboratorGender> genders, 
            List<CollaboratorIndustry> industries, 
            List<CollaboratorRole> roles,
            List<LanguageDto> languagesDtos, 
            List<EditionDto> editionsDtos, 
            int currentEditionId,
            string userInterfaceLanguage)
            : base(entity, languagesDtos, false, false, false)
        {
            this.UpdateModelsAndLists(genders, industries, roles, editionsDtos, currentEditionId, userInterfaceLanguage);
            this.UpdateEditions(editionsDtos, entity.Collaborator, currentEditionId);

            this.FirstName = entity?.Collaborator?.FirstName;
            this.LastNames = entity?.Collaborator?.LastNames;
            this.Email = entity?.User?.Email;
            this.Badge = entity?.Collaborator?.Badge;
            this.PhoneNumber = entity?.Collaborator?.PhoneNumber;
            this.CellPhone = entity?.Collaborator?.CellPhone;   
            
            this.BirthDate = entity?.Collaborator?.BirthDate;
            this.HasAnySpecialNeeds = entity?.Collaborator?.HasAnySpecialNeeds;
            this.HaveYouBeenToRio2CBefore = entity?.EditionParticipationDtos?.Any();
            this.SpecialNeedsDescription = entity?.Collaborator?.SpecialNeedsDescription;
            this.CollaboratorGenderUid = entity?.Collaborator?.Gender?.Uid;
            this.CollaboratorGenderAdditionalInfo = entity?.Collaborator?.CollaboratorGenderAdditionalInfo;
            this.CollaboratorIndustryUid = entity?.Collaborator?.Industry?.Uid;
            this.CollaboratorIndustryAdditionalInfo = entity?.Collaborator?.CollaboratorIndustryAdditionalInfo;
            this.CollaboratorRoleUid = entity?.Collaborator?.Role?.Uid;
            this.CollaboratorRoleAdditionalInfo = entity?.Collaborator?.CollaboratorRoleAdditionalInfo;
        }

        /// <summary>Initializes a new instance of the <see cref="UpdateInnovationCollaboratorAdminMainInformation"/> class.</summary>
        public UpdateInnovationCollaboratorAdminMainInformation()
        {
        }

        /// <summary>Updates the models and lists.</summary>
        /// <param name="genders">The genders.</param>
        /// <param name="industries">The industries.</param>
        /// <param name="roles">The roles.</param>
        /// <param name="editionsDtos">The editions dtos.</param>
        /// <param name="currentEditionId">The current edition identifier.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        public void UpdateModelsAndLists(
            List<CollaboratorGender> genders,
            List<CollaboratorIndustry> industries,
            List<CollaboratorRole> roles,
            List<EditionDto> editionsDtos,
            int currentEditionId,
            string userInterfaceLanguage)
        {
            this.UpdateGenders(genders, userInterfaceLanguage);
            this.UpdateIndustries(industries, userInterfaceLanguage);
            this.UpdateRoles(roles, userInterfaceLanguage);
            this.Editions = editionsDtos.Where(e => e.Id != currentEditionId).ToList();
        }

        /// <summary>Updates the pre send properties.</summary>
        /// <param name="collaboratorTypeName">Name of the collaborator type.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="userUid">The user uid.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="editionUid">The edition uid.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        public void UpdatePreSendProperties(
            string collaboratorTypeName,
            int userId,
            Guid userUid,
            int? editionId,
            Guid? editionUid,
            string userInterfaceLanguage)
        {
            this.CollaboratorTypeName = collaboratorTypeName;
            base.UpdatePreSendProperties(userId, userUid, editionId, editionUid, userInterfaceLanguage);
        }

        #region Private Methods

        /// <summary>
        /// Updates the editions.
        /// </summary>
        /// <param name="editions">The editions.</param>
        /// <param name="collaborator">The collaborator.</param>
        /// <param name="currentEditionId">The current edition identifier.</param>
        private void UpdateEditions(IEnumerable<EditionDto> editions, Collaborator collaborator, int currentEditionId)
        {
            if (collaborator?.EditionParticipantions.Any(p => !p.IsDeleted) == false)
            {
                this.EditionsUids = new List<Guid>();
                return;
            }

            HaveYouBeenToRio2CBefore = true;
            this.EditionsUids = editions.Where(e => collaborator?.EditionParticipantions.Any(p => p.EditionId == e.Id && !p.IsDeleted) == true).Select(e => e.Uid).ToList();
        }

        /// <summary>
        /// Updates the genders.
        /// </summary>
        /// <param name="genders">The genders.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        private void UpdateGenders(List<CollaboratorGender> genders, string userInterfaceLanguage)
        {
            genders.ForEach(g => g.Translate(userInterfaceLanguage));
            this.CollaboratorGenders = genders.OrderBy(e => e.HasAdditionalInfo).ThenBy(e => e.Name);
        }

        /// <summary></summary>
        /// <param name="industries"></param>
        /// <param name="userInterfaceLanguage"></param>
        private void UpdateIndustries(List<CollaboratorIndustry> industries, string userInterfaceLanguage)
        {
            industries.ForEach(g => g.Translate(userInterfaceLanguage));
            this.CollaboratorIndustries = industries.OrderBy(e => e.HasAdditionalInfo).ThenBy(e => e.Name);
        }

        /// <summary></summary>
        /// <param name="roles"></param>
        /// <param name="userInterfaceLanguage"></param>
        private void UpdateRoles(List<CollaboratorRole> roles, string userInterfaceLanguage)
        {
            roles.ForEach(g => g.Translate(userInterfaceLanguage));
            this.CollaboratorRoles = roles.OrderBy(e => e.HasAdditionalInfo).ThenBy(e => e.Name);
        }

        #endregion
    }
}