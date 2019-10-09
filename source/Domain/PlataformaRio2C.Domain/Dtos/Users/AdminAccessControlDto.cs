// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 09-27-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 10-02-2019
// ***********************************************************************
// <copyright file="AdminAccessControlDto.cs" company="Softo">
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
    /// <summary>AdminAccessControlDto</summary>
    public class AdminAccessControlDto
    {
        public User User { get; set; }
        public IEnumerable<Role> Roles { get; set; }
        public Language Language { get; set; }

        public Collaborator Collaborator { get; set; }
        public IEnumerable<CollaboratorType> EditionCollaboratorTypes { get; set; }


        /// <summary>Initializes a new instance of the <see cref="AdminAccessControlDto"/> class.</summary>
        public AdminAccessControlDto()
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

        /// <summary>Gets the name abbreviation code.</summary>
        /// <returns></returns>
        public string GetNameAbbreviationCode()
        {
            return this.User?.Name?.GetTwoLetterCode();
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
            return this.Roles?.Any(r => r.Name == Constants.Role.Admin) == true;
        }

        /// <summary>Determines whether [is admin partial].</summary>
        /// <returns>
        ///   <c>true</c> if [is admin partial]; otherwise, <c>false</c>.</returns>
        public bool IsAdminPartial()
        {
            return this.Roles?.Any(r => r.Name == Constants.Role.AdminPartial) == true;
        }

        /// <summary>Determines whether [has collaborator type] [the specified collaborator type].</summary>
        /// <param name="collaboratorType">Type of the collaborator.</param>
        /// <returns>
        ///   <c>true</c> if [has collaborator type] [the specified collaborator type]; otherwise, <c>false</c>.</returns>
        public bool HasCollaboratorType(string collaboratorType)
        {
            return !string.IsNullOrEmpty(collaboratorType) && this.EditionCollaboratorTypes?.Any(r => r.Name == collaboratorType) == true;
        }

        /// <summary>Determines whether [has any collaborator type] [the specified collaborator types].</summary>
        /// <param name="collaboratorTypes">The collaborator types.</param>
        /// <returns>
        ///   <c>true</c> if [has any collaborator type] [the specified collaborator types]; otherwise, <c>false</c>.</returns>
        public bool HasAnyCollaboratorType(string[] collaboratorTypes)
        {
            if (collaboratorTypes?.Any() != true)
            {
                return false;
            }

            return this.EditionCollaboratorTypes?.Any(ect => collaboratorTypes.Contains(ect.Name)) == true;
        }

        #endregion
    }
}