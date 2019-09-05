// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 09-04-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-05-2019
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

        public IEnumerable<AttendeeOrganization> EditionAttendeeOrganizations { get; set; }
        public IEnumerable<AttendeeCollaboratorTicket> EditionAttendeeCollaboratorTickets { get; set; }
        public IEnumerable<TicketType> EditionUserTicketTypes { get; set; }

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

        /// <summary>Determines whether [is onboarding pending].</summary>
        /// <returns>
        ///   <c>true</c> if [is onboarding pending]; otherwise, <c>false</c>.</returns>
        public bool IsOnboardingPending()
        {
            return this.IsUser() && (!this.IsAttendeeCollaboratorOnboardingFinished() || !this.IsAttendeeOrganizationsOnboardingFinished());
        }

        /// <summary>Determines whether [is user onboarding finished].</summary>
        /// <returns>
        ///   <c>true</c> if [is user onboarding finished]; otherwise, <c>false</c>.</returns>
        public bool IsUserOnboardingFinished()
        {
            return !string.IsNullOrEmpty(this.User?.PasswordHash);
        }

        /// <summary>Determines whether [is attendee collaborator onboarding finished].</summary>
        /// <returns>
        ///   <c>true</c> if [is attendee collaborator onboarding finished]; otherwise, <c>false</c>.</returns>
        public bool IsAttendeeCollaboratorOnboardingFinished()
        {
            return this.EditionAttendeeCollaborator?.OnboardingFinishDate != null;
        }

        /// <summary>Determines whether [is attendee organizations onboarding finished].</summary>
        /// <returns>
        ///   <c>true</c> if [is attendee organizations onboarding finished]; otherwise, <c>false</c>.</returns>
        public bool IsAttendeeOrganizationsOnboardingFinished()
        {
            return this.EditionAttendeeOrganizations?.Any() == true                                                // Has at least one organization linked
                   && this.EditionAttendeeOrganizations?.All(eao => eao.OnboardingFinishDate.HasValue) == true;    // and all organizations onboarding are finished
        }

        #endregion
    }
}