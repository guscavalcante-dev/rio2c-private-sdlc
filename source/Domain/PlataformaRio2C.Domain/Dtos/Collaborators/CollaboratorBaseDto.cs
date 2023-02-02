// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-26-2019
//
// Last Modified By : Elton Assunção
// Last Modified On : 02-03-2023
// ***********************************************************************
// <copyright file="CollaboratorBaseDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>CollaboratorBaseDto</summary>
    public class CollaboratorBaseDto
    {
        public int Id { get; set; }
        public Guid Uid { get; set; }

        public bool? Active { get; set; }
        public string UserInterfaceLanguage { get; set; }
        public string FullName => this.FirstName + (!string.IsNullOrEmpty(this.LastNames) ? " " + this.LastNames : String.Empty);
        public string NameAbbreviation => this.FullName.GetTwoLetterCode();
        public string FirstName { get; set; }
        public string LastNames { get; set; }
        public string Badge { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string CellPhone { get; set; }
        public string PublicEmail { get; set; }
        public string JobTitle { get; set; }
        public bool IsApiDisplayEnabled { get; set; }
        public HoldingBaseDto HoldingBaseDto { get; set; }
        public OrganizationBaseDto OrganizatioBaseDto { get; set; }
        public DateTimeOffset? ImageUploadDate { get; set; }
        public DateTimeOffset CreateDate { get; set; }
        public DateTimeOffset UpdateDate { get; set; }
        public DateTimeOffset? WelcomeEmailSentDate => this.EditionAttendeeCollaborator?.WelcomeEmailSendDate;
        public DateTimeOffset? CurrentEditionOnboardingFinishDate => this.EditionAttendeeCollaborator?.OnboardingFinishDate;
        public DateTimeOffset? SpeakerCurrentEditionOnboardingFinishDate => this.EditionAttendeeCollaborator?.SpeakerTermsAcceptanceDate;
        public bool IsInCurrentEdition => this.EditionAttendeeCollaborator != null;
        public bool IsInOtherEdition { get; set; }

        public UserBaseDto UserBaseDto { get; set; }

        public IEnumerable<AttendeeOrganizationBaseDto> AttendeeOrganizationBasesDtos { get; set; }
        public IEnumerable<CollaboratorJobTitleBaseDto> JobTitlesDtos { get; set; }
        public IEnumerable<Role> Roles { get; set; }
        public IEnumerable<AttendeeCollaboratorTypeDto> AttendeeCollaboratorTypeDtos { get; set; }
        public IEnumerable<InnovationOrganizationTrackOptionGroupDto> InnovationOrganizationTrackOptionGroupDtos { get; set; }

        public List<string> TranslatedCollaboratorTypes
        {
            get
            {
                this.Translate();

                var collaboratorTypes = new List<string>();

                // Check if the collaborator has admin full role
                var adminFullDescription = this.Roles?
                    .FirstOrDefault(r => r.Name == Constants.Role.Admin)?.Description;
                if (!string.IsNullOrEmpty(adminFullDescription))
                {
                    collaboratorTypes.Add(adminFullDescription);
                }

                // Check if the collaborator has other roles
                var collaboratorTypesDescriptions = this.AttendeeCollaboratorTypeDtos?
                    .Select(act => act.CollaboratorType?.Description)?
                    .OrderBy(ctd => ctd)
                    .ToList();
                if (collaboratorTypesDescriptions?.Any() == true)
                {
                    collaboratorTypes.AddRange(collaboratorTypesDescriptions);
                }

                return collaboratorTypes;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is admin full.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is admin full; otherwise, <c>false</c>.
        /// </value>
        public bool IsAdminFull
        {
            get
            {
                return this.Roles?.Any(r => r.Name == Constants.Role.Admin) ?? false;
            }
        }

        /// <summary>
        /// Translates this instance.
        /// </summary>
        public void Translate()
        {
            if (string.IsNullOrEmpty(this.UserInterfaceLanguage))
            {
                return;
            }

            this.Roles?
                .ToList()?
                .ForEach(r => r.GetSeparatorTranslation(rt => rt.Description, this.UserInterfaceLanguage, '|'));
            this.Roles = this.Roles?.OrderBy(r => r.Description);

            this.AttendeeCollaboratorTypeDtos?
                                .ToList()?
                                .ForEach(act => act.CollaboratorType?.GetSeparatorTranslation(ct => ct.Description, this.UserInterfaceLanguage, '|'));
            this.AttendeeCollaboratorTypeDtos = this.AttendeeCollaboratorTypeDtos?.OrderBy(act => act.CollaboratorType.Description);
        }

        #region Json ignored properties 

        [ScriptIgnore]
        public AttendeeCollaborator EditionAttendeeCollaborator { get; set; }
        
        #endregion

        /// <summary>Gets the collaborator job title base dto by language code.</summary>
        /// <param name="languageCode">The language code.</param>
        /// <returns></returns>
        public CollaboratorJobTitleBaseDto GetCollaboratorJobTitleBaseDtoByLanguageCode(string languageCode)
        {
            if (string.IsNullOrEmpty(languageCode))
            {
                languageCode = "pt-br";
            }

            return this.JobTitlesDtos?.FirstOrDefault(jtd => jtd.LanguageDto.Code == languageCode) ??
                   this.JobTitlesDtos?.FirstOrDefault(jtd => jtd.LanguageDto.Code == "pt-br");
        }

        /// <summary>Initializes a new instance of the <see cref="CollaboratorBaseDto"/> class.</summary>
        public CollaboratorBaseDto()
        {
        }
    }
}