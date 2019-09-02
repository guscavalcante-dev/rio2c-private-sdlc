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
        /// <param name="editionName">Name of the edition.</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="fullName">The full name.</param>
        /// <param name="email">The email.</param>
        public SendWelmcomeEmailAsync(string editionName, string firstName, string fullName, string email)
            : base(editionName, firstName, fullName, email)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="SendWelmcomeEmailAsync"/> class.</summary>
        public SendWelmcomeEmailAsync()
        {
        }
    }
}