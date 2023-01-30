// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-26-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 01-10-2023
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
        public string FirstName { get; set; }
        public string LastNames { get; set; }
        public string Badge { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string CellPhone { get; set; }
        public string PublicEmail { get; set; }
        public string JobTitle { get; set; }
        public string Website { get; set; }
        public string Linkedin { get; set; }
        public string Twitter { get; set; }
        public string Instagram { get; set; }
        public string Youtube { get; set; }
        public DateTimeOffset? ImageUploadDate { get; set; }
        public DateTimeOffset CreateDate { get; set; }
        public DateTimeOffset UpdateDate { get; set; }
        public bool IsInOtherEdition { get; set; }

        public HoldingBaseDto HoldingBaseDto { get; set; }
        public OrganizationBaseDto OrganizatioBaseDto { get; set; }
        public UserBaseDto UserBaseDto { get; set; }
        public UserBaseDto CreatorBaseDto { get; set; }
        public UserBaseDto UpdaterBaseDto { get; set; }

        public IEnumerable<AttendeeOrganizationBaseDto> AttendeeOrganizationBasesDtos { get; set; }
        public IEnumerable<CollaboratorJobTitleBaseDto> JobTitleBaseDtos { get; set; }
        public IEnumerable<CollaboratorMiniBioBaseDto> MiniBioBaseDtos { get; set; }

        //TODO: BaseDtos can only have another BaseDtos. These properties shouldn't be here! Refactor this!
        public IEnumerable<Role> Roles { get; set; }
        public IEnumerable<AttendeeCollaboratorTypeDto> AttendeeCollaboratorTypeDtos { get; set; }
        public IEnumerable<InnovationOrganizationTrackOptionGroupDto> InnovationOrganizationTrackOptionGroupDtos { get; set; }
        [ScriptIgnore]
        public AttendeeCollaborator EditionAttendeeCollaborator { get; set; }

        #region Extension Methods and Extension Properties

        public DateTimeOffset? WelcomeEmailSentDate => this.EditionAttendeeCollaborator?.WelcomeEmailSendDate;
        public DateTimeOffset? CurrentEditionOnboardingFinishDate => this.EditionAttendeeCollaborator?.OnboardingFinishDate;
        public DateTimeOffset? SpeakerCurrentEditionOnboardingFinishDate => this.EditionAttendeeCollaborator?.SpeakerTermsAcceptanceDate;
        public bool IsInCurrentEdition => this.EditionAttendeeCollaborator != null;
        public bool IsAdminFull => this.Roles?.Any(r => r.Name == Constants.Role.Admin) ?? false;
        public string FullName => this.FirstName + (!string.IsNullOrEmpty(this.LastNames) ? " " + this.LastNames : String.Empty);
        public string NameAbbreviation => this.FullName.GetTwoLetterCode();

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

        /// <summary>Gets the collaborator job title base dto by language code.</summary>
        /// <param name="culture">The language code.</param>
        /// <returns></returns>
        public CollaboratorJobTitleBaseDto GetCollaboratorJobTitleBaseDtoByLanguageCode(string culture)
        {
            if (string.IsNullOrEmpty(culture))
            {
                culture = "pt-br";
            }

            return this.JobTitleBaseDtos?.FirstOrDefault(jtd => jtd.LanguageDto.Code == culture) ??
                   this.JobTitleBaseDtos?.FirstOrDefault(jtd => jtd.LanguageDto.Code == "pt-br");
        }

        /// <summary>
        /// Gets the mini bio base dto by language code.
        /// </summary>
        /// <param name="culture">The culture.</param>
        /// <returns></returns>
        public CollaboratorMiniBioBaseDto GetMiniBioBaseDtoByLanguageCode(string culture)
        {
            return this.MiniBioBaseDtos?.FirstOrDefault(mbd => mbd.LanguageDto.Code?.ToLowerInvariant() == culture?.ToLowerInvariant());
        }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="CollaboratorBaseDto" /> class.
        /// </summary>
        public CollaboratorBaseDto()
        {
        }
    }
}