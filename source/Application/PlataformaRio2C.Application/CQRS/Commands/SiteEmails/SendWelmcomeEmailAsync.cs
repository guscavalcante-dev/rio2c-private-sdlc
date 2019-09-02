// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 09-02-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-02-2019
// ***********************************************************************
// <copyright file="SendWelmcomeEmailAsync.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>SendWelmcomeEmailAsync</summary>
    public class SendWelmcomeEmailAsync : EmailBaseCommand
    {
        /// <summary>Initializes a new instance of the <see cref="SendWelmcomeEmailAsync"/> class.</summary>
        /// <param name="firstName">The first name.</param>
        /// <param name="fullName">The full name.</param>
        /// <param name="email">The email.</param>
        /// <param name="editionName">Name of the edition.</param>
        /// <param name="editionUrlCode">The edition URL code.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        public SendWelmcomeEmailAsync(string firstName, string fullName, string email, string editionName, int editionUrlCode, string userInterfaceLanguage)
            : base(firstName, fullName, email, editionName, editionUrlCode, userInterfaceLanguage)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="SendWelmcomeEmailAsync"/> class.</summary>
        public SendWelmcomeEmailAsync()
        {
        }
    }
}