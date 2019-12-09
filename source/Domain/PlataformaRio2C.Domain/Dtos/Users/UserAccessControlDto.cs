// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 09-04-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-05-2019
// ***********************************************************************
// <copyright file="UserAccessControlDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>UserAccessControlDto</summary>
    public class UserAccessControlDto
    {
        public User User { get; set; }
        public IEnumerable<Role> Roles { get; set; }
        public Language Language { get; set; }
        public bool HasUnreadMessages { get; set; }

        public Collaborator Collaborator { get; set; }
        public AttendeeCollaborator EditionAttendeeCollaborator { get; set; }
        public IEnumerable<CollaboratorType> EditionCollaboratorTypes { get; set; }
        public IEnumerable<AttendeeOrganization> EditionAttendeeOrganizations { get; set; } //TODO: Remove EditionAttendeeOrganizations from UserAccessControlDto
        public IEnumerable<CollaboratorJobTitleBaseDto> JobTitlesDtos { get; set; }  //TODO: Remove EditionAttendeeOrganizations from UserAccessControlDto
        public IEnumerable<AttendeeCollaboratorTicketDto> EditionAttendeeCollaboratorTickets { get; set; }

        /// <summary>Initializes a new instance of the <see cref="UserAccessControlDto"/> class.</summary>
        public UserAccessControlDto()
        {
        }

        #region Properties manipulation 

        /// <summary>Gets the display name.</summary>
        /// <returns></returns>
        public string GetDisplayName()
        {
            return this.Collaborator?.GetDisplayName() ?? this.User.Name;
        }

        /// <summary>Gets the display name abbreviation.</summary>
        /// <returns></returns>
        public string GetDisplayNameAbbreviation()
        {
            return this.GetDisplayName().GetTwoLetterCode();
        }

        /// <summary>Gets the full name.</summary>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        /// <returns></returns>
        public string GetFullName(string userInterfaceLanguage)
        {
            return this.GetDisplayName()?.UppercaseFirstOfEachWord(userInterfaceLanguage);
        }

        /// <summary>Gets the first name.</summary>
        /// <returns></returns>
        public string GetFirstName()
        {
            return this.GetDisplayName()?.GetFirstWord();
        }

        /// <summary>Gets the name abbreviation code.</summary>
        /// <returns></returns>
        public string GetNameAbbreviationCode()
        {
            return this.GetDisplayName()?.GetTwoLetterCode();
        }

        /// <summary>Determines whether this instance has image.</summary>
        /// <returns>
        ///   <c>true</c> if this instance has image; otherwise, <c>false</c>.</returns>
        public bool HasImage()
        {
            return this.Collaborator?.ImageUploadDate != null;
        }

        /// <summary>Gets the job title.</summary>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        /// <returns></returns>
        public string GetJobTitle(string userInterfaceLanguage)
        {
            var cultureJobTitle = this.JobTitlesDtos?.FirstOrDefault(jb => jb.LanguageDto.Code == userInterfaceLanguage);
            if (cultureJobTitle != null)
            {
                return cultureJobTitle.Value;
            }

            return this.JobTitlesDtos?.FirstOrDefault()?.Value;
        }

        /// <summary>Gets the organizations.</summary>
        /// <returns></returns>
        public List<Organization> GetOrganizations()
        {
            return this.EditionAttendeeOrganizations?.Where(eao => !eao.IsDeleted && !eao.Organization.IsDeleted)?.Select(eao => eao.Organization)?.ToList();
        }

        /// <summary>Gets the first attendee organization created.</summary>
        /// <returns></returns>
        public AttendeeOrganization GetFirstAttendeeOrganizationCreated()
        {
            return this.EditionAttendeeOrganizations?.Where(eao => !eao.IsDeleted)?.OrderBy(eao => eao.Id).FirstOrDefault();
        }

        #endregion

        #region Permissions

        /// <summary>Determines whether this instance is admin.</summary>
        /// <returns>
        ///   <c>true</c> if this instance is admin; otherwise, <c>false</c>.</returns>
        public bool IsAdmin()
        {
            return this.Roles?.Any(r => Constants.Role.AnyAdminArray.Contains(r.Name)) == true;
        }

        /// <summary>Determines whether [has collaborator type] [the specified collaborator type].</summary>
        /// <param name="collaboratorType">Type of the collaborator.</param>
        /// <returns>
        ///   <c>true</c> if [has collaborator type] [the specified collaborator type]; otherwise, <c>false</c>.</returns>
        public bool HasCollaboratorType(string collaboratorType)
        {
            if (this.IsAdmin())
            {
                return true;
            }

            if (string.IsNullOrEmpty(collaboratorType))
            {
                return false;
            }

            return this.EditionCollaboratorTypes?.Any(ect => !ect.IsDeleted && ect.Name == collaboratorType) == true;
        }

        /// <summary>Determines whether [has any collaborator type] [the specified collaborator types].</summary>
        /// <param name="collaboratorTypes">The collaborator types.</param>
        /// <returns>
        ///   <c>true</c> if [has any collaborator type] [the specified collaborator types]; otherwise, <c>false</c>.</returns>
        public bool HasAnyCollaboratorType(string[] collaboratorTypes)
        {
            if (this.IsAdmin())
            {
                return true;
            }

            if (collaboratorTypes?.Any() != true)
            {
                return false;
            }

            return this.EditionCollaboratorTypes?.Any(ect => collaboratorTypes.Contains(ect.Name)) == true;
        }

        /// <summary>Determines whether [has edition attendee organization] [the specified attendee organization uid].</summary>
        /// <param name="attendeeOrganizationUid">The attendee organization uid.</param>
        /// <returns>
        ///   <c>true</c> if [has edition attendee organization] [the specified attendee organization uid]; otherwise, <c>false</c>.</returns>
        public bool HasEditionAttendeeOrganization(Guid attendeeOrganizationUid)
        {
            return this.IsAdmin() || this.EditionAttendeeOrganizations?.Any(eao => eao.Uid == attendeeOrganizationUid) == true;
        }

        #endregion

        #region Onboarding

        /// <summary>Determines whether [is onboarding pending].</summary>
        /// <returns>
        ///   <c>true</c> if [is onboarding pending]; otherwise, <c>false</c>.</returns>
        public bool IsOnboardingPending()
        {
            return !this.IsAdmin() 
                   && (!this.IsAttendeeCollaboratorOnboardingFinished() || !this.IsPlayerAttendeeOrganizationsOnboardingFinished() || this.IsTicketBuyerOrganizationOnboardingPending());
        }

        #region Collaborator

        /// <summary>Determines whether [is attendee collaborator onboarding finished].</summary>
        /// <returns>
        ///   <c>true</c> if [is attendee collaborator onboarding finished]; otherwise, <c>false</c>.</returns>
        public bool IsAttendeeCollaboratorOnboardingFinished()
        {
            return this.IsPlayerTermsAcceptanceFinished() && this.IsUserOnboardingFinished() && this.IsCollaboratorOnboardingFinished();
        }

        /// <summary>Determines whether [is player terms acceptance finished].</summary>
        /// <returns>
        ///   <c>true</c> if [is player terms acceptance finished]; otherwise, <c>false</c>.</returns>
        public bool IsPlayerTermsAcceptanceFinished()
        {
            return !this.HasCollaboratorType(Constants.CollaboratorType.ExecutiveAudiovisual)                                              // // Not Player //TODO: Change to check attendee organization type (must be dto)
                   || this.EditionAttendeeCollaborator?.PlayerTermsAcceptanceDate != null;                                                 // Or has accepted the player terms
                   //|| !this.HasPlayerOrganization()
        }

        /// <summary>Determines whether [is user onboarding finished].</summary>
        /// <returns>
        ///   <c>true</c> if [is user onboarding finished]; otherwise, <c>false</c>.</returns>
        public bool IsUserOnboardingFinished()
        {
            return this.EditionAttendeeCollaborator?.OnboardingUserDate != null;
        }

        /// <summary>Determines whether [is collaborator onboarding finished].</summary>
        /// <returns>
        ///   <c>true</c> if [is collaborator onboarding finished]; otherwise, <c>false</c>.</returns>
        public bool IsCollaboratorOnboardingFinished()
        {
            return this.EditionAttendeeCollaborator?.OnboardingCollaboratorDate != null;
        }

        #endregion

        #region Organizations

        /// <summary>Determines whether [is player attendee organizations onboarding finished].</summary>
        /// <returns>
        ///   <c>true</c> if [is player attendee organizations onboarding finished]; otherwise, <c>false</c>.</returns>
        public bool IsPlayerAttendeeOrganizationsOnboardingFinished()
        {
            return this.IsPlayerOrganizatiosnOnboardingFinished() 
                   && this.IsPlayerOrganizationsInterestsOnboardingFinished();
        }

        /// <summary>Determines whether [is player organizatiosn onboarding finished].</summary>
        /// <returns>
        ///   <c>true</c> if [is player organizatiosn onboarding finished]; otherwise, <c>false</c>.</returns>
        public bool IsPlayerOrganizatiosnOnboardingFinished()
        {
            return !this.HasCollaboratorType(Constants.CollaboratorType.ExecutiveAudiovisual)                                              // Not Player //TODO: Change to check attendee organization type (must be dto)
                    || (this.EditionAttendeeOrganizations?.Any() == false                                                                  // No organization related
                        || (this.EditionAttendeeOrganizations?.Any() == true                                                               // or has at least one organization linked
                            && this.EditionAttendeeOrganizations?.All(eao => eao.OnboardingOrganizationDate.HasValue) == true));           // and all organizations interests onboarding are finished
        }

        /// <summary>Determines whether [is player organization interests onboarding pending].</summary>
        /// <returns>
        ///   <c>true</c> if [is player organization interests onboarding pending]; otherwise, <c>false</c>.</returns>
        public bool IsPlayerOrganizationInterestsOnboardingPending()
        {
            return this.HasCollaboratorType(Constants.CollaboratorType.ExecutiveAudiovisual)                                               // Is Player //TODO: Change to check attendee organization type (must be dto)
                   && this.EditionAttendeeOrganizations?.Any() == true                                                                     // Has at least one organization linked
                   && this.EditionAttendeeOrganizations?.Any(eao => eao.OnboardingOrganizationDate.HasValue                                // and at least one organization onboarded
                                                                    && !eao.OnboardingInterestsDate.HasValue) == true;                     // and this organization interests area not onboarded
        }

        /// <summary>Determines whether [is player organizations interests onboarding finished].</summary>
        /// <returns>
        ///   <c>true</c> if [is player organizations interests onboarding finished]; otherwise, <c>false</c>.</returns>
        public bool IsPlayerOrganizationsInterestsOnboardingFinished()
        {
            return !this.HasCollaboratorType(Constants.CollaboratorType.ExecutiveAudiovisual)                                              // Not Player //TODO: Change to check attendee organization type (must be dto)
                   || (this.EditionAttendeeOrganizations?.Any() == false                                                                   // No organization related
                       || (this.EditionAttendeeOrganizations?.Any() == true                                                                // or has at least one organization linked
                           && this.EditionAttendeeOrganizations?.All(eao => eao.OnboardingInterestsDate.HasValue) == true));               // and all organizations interests onboarding are finished
        }

        /// <summary>Determines whether [is ticket buyer organization onboarding pending].</summary>
        /// <returns>
        ///   <c>true</c> if [is ticket buyer organization onboarding pending]; otherwise, <c>false</c>.</returns>
        public bool IsTicketBuyerOrganizationOnboardingPending()
        {
            return !this.HasCollaboratorType(Constants.CollaboratorType.ExecutiveAudiovisual)                                              // Not player //TODO: Change to check attendee organization type (must be dto)
                   && this.HasAnyCollaboratorType(Constants.CollaboratorType.TicketBuyers)                                                 // Is ticket buyer
                   && (!this.EditionAttendeeCollaborator.OnboardingOrganizationDataSkippedDate.HasValue                                    // Not skipped the onboarding of company data
                       && (this.EditionAttendeeOrganizations?.Any() == false                                                               // No organization related
                           || this.EditionAttendeeOrganizations?.All(eao => eao.OnboardingFinishDate.HasValue) == false));                 // or has organizations without onboarding     
        }

        #endregion

        #endregion

        #region Projects required information

        /// <summary>Determines whether [is project submission organization information pending].</summary>
        /// <returns>
        ///   <c>true</c> if [is project submission organization information pending]; otherwise, <c>false</c>.</returns>
        public bool IsProjectSubmissionOrganizationInformationPending()
        {
            return this.HasCollaboratorType(Constants.CollaboratorType.Industry)
                   && (this.EditionAttendeeOrganizations?.Any() == false
                       || this.EditionAttendeeOrganizations?.Any(eao => eao.ProjectSubmissionOrganizationDate.HasValue) == false);
        }

        /// <summary>Determines whether [is project submission terms acceptance pending].</summary>
        /// <returns>
        ///   <c>true</c> if [is project submission terms acceptance pending]; otherwise, <c>false</c>.</returns>
        public bool IsProjectSubmissionTermsAcceptancePending()
        {
            return this.HasCollaboratorType(Constants.CollaboratorType.Industry)
                   && this.EditionAttendeeCollaborator?.ProducerTermsAcceptanceDate.HasValue == false;
        }

        #endregion
    }
}