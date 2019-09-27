// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 09-04-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-26-2019
// ***********************************************************************
// <copyright file="UserAccessControlDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
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
        public Collaborator Collaborator { get; set; }
        public Language Language { get; set; }
        public AttendeeCollaborator EditionAttendeeCollaborator { get; set; }

        public IEnumerable<CollaboratorJobTitleBaseDto> JobTitlesDtos { get; set; }
        public IEnumerable<AttendeeOrganization> EditionAttendeeOrganizations { get; set; }
        public IEnumerable<AttendeeCollaboratorTicket> EditionAttendeeCollaboratorTickets { get; set; }
        public IEnumerable<CollaboratorType> EditionCollaboratorTypes { get; set; }

        /// <summary>Initializes a new instance of the <see cref="UserAccessControlDto"/> class.</summary>
        public UserAccessControlDto()
        {
        }

        #region Properties manipulation 

        /// <summary>Gets the full name.</summary>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        /// <returns></returns>
        public string GetFullName(string userInterfaceLanguage)
        {
            return this.User?.Name?.UppercaseFirstOfEachWord(userInterfaceLanguage);
        }

        /// <summary>Gets the first name.</summary>
        /// <returns></returns>
        public string GetFirstName()
        {
            return this.User?.Name?.GetFirstWord();
        }

        /// <summary>Gets the name first character.</summary>
        /// <returns></returns>
        public string GetNameFirstCharacter()
        {
            return this.User?.Name?.GetFirstChar();
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

        /// <summary>Determines whether [has player organization].</summary>
        /// <returns>
        ///   <c>true</c> if [has player organization]; otherwise, <c>false</c>.</returns>
        public bool HasPlayerOrganization()
        {
            return this.EditionAttendeeOrganizations?.Any(eao => eao.AttendeeOrganizationTypes.Any(aot => !aot.IsDeleted
                                                                                                          && !aot.OrganizationType.IsDeleted
                                                                                                          && aot.OrganizationType.Name == "Player")) == true;
        }

        #endregion

        #region Permissions

        /// <summary>Determines whether this instance is admin.</summary>
        /// <returns>
        ///   <c>true</c> if this instance is admin; otherwise, <c>false</c>.</returns>
        public bool IsAdmin()
        {
            return this.Roles?.Any(r => r.Name == "Admin") == true;
        }

        /// <summary>Determines whether this instance is user.</summary>
        /// <returns>
        ///   <c>true</c> if this instance is user; otherwise, <c>false</c>.</returns>
        public bool IsUser()
        {
            return this.Roles?.Any(r => r.Name == "User") == true;
        }

        #endregion

        #region Onboarding

        //TODO: Check tickets

        /// <summary>Determines whether [is onboarding pending].</summary>
        /// <returns>
        ///   <c>true</c> if [is onboarding pending]; otherwise, <c>false</c>.</returns>
        public bool IsOnboardingPending()
        {
            return this.IsUser() && (!this.IsAttendeeCollaboratorOnboardingFinished() || !this.IsAttendeeOrganizationsOnboardingFinished());
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
            return !this.HasPlayerOrganization()
                   || this.EditionAttendeeCollaborator?.PlayerTermsAcceptanceDate != null;
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

        /// <summary>Determines whether [is attendee organizations onboarding finished].</summary>
        /// <returns>
        ///   <c>true</c> if [is attendee organizations onboarding finished]; otherwise, <c>false</c>.</returns>
        public bool IsAttendeeOrganizationsOnboardingFinished()
        {
            return this.IsOrganizatiosnOnboardingFinished() && this.IsOrganizationsInterestsOnboardingFinished();
        }

        /// <summary>Determines whether [is organizatiosn onboarding finished].</summary>
        /// <returns>
        ///   <c>true</c> if [is organizatiosn onboarding finished]; otherwise, <c>false</c>.</returns>
        public bool IsOrganizatiosnOnboardingFinished()
        {
            return this.EditionAttendeeOrganizations?.Any() == false                                                        // No organization related
                   || (this.EditionAttendeeOrganizations?.Any() == true                                                     // or has at least one organization linked
                       && this.EditionAttendeeOrganizations?.All(eao => eao.OnboardingOrganizationDate.HasValue) == true);  // and all organizations interests onboarding are finished
        }

        /// <summary>Determines whether [has organization interests onboarding pending].</summary>
        /// <returns>
        ///   <c>true</c> if [has organization interests onboarding pending]; otherwise, <c>false</c>.</returns>
        public bool HasOrganizationInterestsOnboardingPending()
        {
            return this.EditionAttendeeOrganizations?.Any() == true                                                         // Has at least one organization linked
                   && this.EditionAttendeeOrganizations?.Any(eao => eao.OnboardingOrganizationDate.HasValue                 // and at least one organization onboarded
                                                                    && !eao.OnboardingInterestsDate.HasValue) == true;      // and this  organization interests area not onboarded
        }

        /// <summary>Determines whether [is organizations interests onboarding finished].</summary>
        /// <returns>
        ///   <c>true</c> if [is organizations interests onboarding finished]; otherwise, <c>false</c>.</returns>
        public bool IsOrganizationsInterestsOnboardingFinished()
        {
            return this.EditionAttendeeOrganizations?.Any() == false                                                        // No organization related
                   || (this.EditionAttendeeOrganizations?.Any() == true                                                     // or has at least one organization linked
                       && this.EditionAttendeeOrganizations?.All(eao => eao.OnboardingInterestsDate.HasValue) == true);     // and all organizations interests onboarding are finished
        }

        #endregion

        #endregion
    }
}