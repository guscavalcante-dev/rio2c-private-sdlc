// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-26-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 10-16-2023
// ***********************************************************************
// <copyright file="CollaboratorDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>
    /// CollaboratorDto
    /// </summary>
    public class CollaboratorDto
    {
        public int Id { get; set; }
        public Guid Uid { get; set; }
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
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Document { get; set; }

        public int? CollaboratorGenderId { get; set; }
        public string CollaboratorGenderAdditionalInfo { get; set; }
        public int? CollaboratorRoleId { get; set; }
        public string CollaboratorRoleAdditionalInfo { get; set; }
        public int? CollaboratorIndustryId { get; set; }
        public string CollaboratorIndustryAdditionalInfo { get; set; }
        public bool? HasAnySpecialNeeds { get; set; }
        public string SpecialNeedsDescription { get; set; }
        public bool IsApiDisplayEnabled { get; set; }
        public int? ApiHighlightPosition { get; set; }

        public DateTimeOffset? ImageUploadDate { get; set; }
        public DateTimeOffset CreateDate { get; set; }
        public DateTimeOffset UpdateDate { get; set; }
        public DateTime? BirthDate { get; set; }

        public bool? Active { get; set; }
        public bool IsInOtherEdition { get; set; }

        public int CreateUserId { get; set; }
        public int UpdateUserId { get; set; }

        public List<Guid> EditionsUids { get; set; }

        public HoldingBaseDto HoldingBaseDto { get; set; }
        public UserBaseDto UserBaseDto { get; set; }
        public UserBaseDto CreatorBaseDto { get; set; }
        public UserBaseDto UpdaterBaseDto { get; set; }
        public CollaboratorGender Gender { get; set; }
        public CollaboratorRole CollaboratorRole { get; set; }
        public CollaboratorIndustry Industry { get; set; }
        public IEnumerable<AttendeeOrganizationBaseDto> AttendeeOrganizationBasesDtos { get; set; }
        public IEnumerable<CollaboratorJobTitleBaseDto> JobTitleBaseDtos { get; set; }
        public IEnumerable<CollaboratorMiniBioBaseDto> MiniBioBaseDtos { get; set; }
        public IEnumerable<Role> Roles { get; set; }
        public IEnumerable<AttendeeCollaboratorTypeDto> AttendeeCollaboratorTypeDtos { get; set; }
        public IEnumerable<InnovationOrganizationTrackOptionGroupDto> InnovationOrganizationTrackOptionGroupDtos { get; set; }
        public IEnumerable<ConferenceDto> ConferencesDtos { get; set; }
        public IEnumerable<AttendeeCollaboratorActivityDto> AttendeeCollaboratorActivityDtos { get; set; }
        public IEnumerable<AttendeeCollaboratorInterestDto> AttendeeCollaboratorInterestDtos { get; set; }
        public IEnumerable<AttendeeCollaboratorInnovationOrganizationTrackDto> AttendeeCollaboratorInnovationOrganizationTrackDtos { get; set; }
        public IEnumerable<AttendeeCollaboratorTargetAudiencesDto> AttendeeCollaboratorTargetAudiencesDtos { get; set; }
        public IEnumerable<NegotiationBaseDto> NegotiationBaseDtos { get; set; }
        public IEnumerable<NegotiationBaseDto> ProducerNegotiationBaseDtos { get; set; }

        [ScriptIgnore]
        public AttendeeCollaborator EditionAttendeeCollaborator { get; set; }
        public AttendeeCollaboratorBaseDto EditionAttendeeCollaboratorBaseDto { get; set; }

        #region Extension Methods and Extension Properties

        public DateTimeOffset? SpeakerCurrentEditionOnboardingFinishDate => this.EditionAttendeeCollaboratorBaseDto?.SpeakerTermsAcceptanceDate;
        public DateTimeOffset? WelcomeEmailSentDate => this.EditionAttendeeCollaboratorBaseDto?.WelcomeEmailSendDate;
        public DateTimeOffset? CurrentEditionOnboardingFinishDate => this.EditionAttendeeCollaboratorBaseDto?.OnboardingFinishDate;
        public bool IsInCurrentEdition => this.EditionAttendeeCollaboratorBaseDto != null;
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
                var adminFullDescription = this.Roles?.FirstOrDefault(r => r.Name == Constants.Role.Admin)?.Description;
                if (!string.IsNullOrEmpty(adminFullDescription))
                {
                    collaboratorTypes.Add(adminFullDescription);
                }

                // Check if the collaborator has other roles
                var collaboratorTypesDescriptions = this.AttendeeCollaboratorTypeDtos?
                    .Select(act => act.CollaboratorType?.Description ?? act.CollaboratorTypeDescription)?
                    .OrderBy(ctd => ctd)?
                    .ToList();
                if (collaboratorTypesDescriptions?.Any(ctd => !string.IsNullOrEmpty(ctd)) == true)
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
                .ForEach(r => r.GetSeparatorTranslation(rt => rt.Description, this.UserInterfaceLanguage));
            this.Roles = this.Roles?.OrderBy(r => r.Description);

            foreach(var attendeeCollaboratorTypeDto in this.AttendeeCollaboratorTypeDtos)
            {
                if(attendeeCollaboratorTypeDto.CollaboratorType != null)
                {
                    attendeeCollaboratorTypeDto.CollaboratorType = attendeeCollaboratorTypeDto.CollaboratorType.GetSeparatorTranslation(ct => ct.Description, this.UserInterfaceLanguage);
                    this.AttendeeCollaboratorTypeDtos = this.AttendeeCollaboratorTypeDtos?.OrderBy(act => act.CollaboratorType.Description);
                }
                else if(!string.IsNullOrEmpty(attendeeCollaboratorTypeDto.CollaboratorTypeDescription))
                {
                    attendeeCollaboratorTypeDto.CollaboratorTypeDescription = attendeeCollaboratorTypeDto.CollaboratorTypeDescription.GetSeparatorTranslation(this.UserInterfaceLanguage);
                }
            }
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

        /// <summary>
        /// Gets the conferences titles.
        /// </summary>
        /// <param name="culture">The culture.</param>
        /// <returns></returns>
        public List<ConferenceTitleDto> GetConferencesTitles(string culture)
        {
            return this.ConferencesDtos?.Select(c => c.GetConferenceTitleDtoByLanguageCode(culture)).ToList();
        }

        /// <summary>
        /// Gets the conferences titles with room and date string.
        /// </summary>
        /// <param name="culture">The culture.</param>
        /// <returns></returns>
        public string GetConferencesTitlesWithRoomAndDateString(string culture)
        {
            return this.ConferencesDtos?.Select(c => string.Format(@"{0} - {1}/{2} | {3} | {4}",
                                                                        c.StartDate.ToStringDateBrazilTimeZone(),
                                                                        c.StartDate.ToStringHourBrazilTimeZone(),
                                                                        c.EndDate.ToStringHourBrazilTimeZone(),
                                                                        c.GetRoomNameDtoByLanguageCode(culture)?.RoomName?.Value ?? "-",
                                                                        c.GetConferenceTitleDtoByLanguageCode(culture).ConferenceTitle?.Value ?? "-"                                                                        
                                                                    )).ToString("; ");

        }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="CollaboratorDto"/> class.
        /// </summary>
        public CollaboratorDto()
        {
        }
    }
}