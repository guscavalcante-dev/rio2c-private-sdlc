// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 09-02-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-02-2019
// ***********************************************************************
// <copyright file="EmailBaseCommand.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using MediatR;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>EmailBaseCommand</summary>
    public class EmailBaseCommand : IRequest<AppValidationResult>
    {
        public string FirstName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }

        public string EditionName { get; set; }
        public int EditionUrlCode { get; set; }
        public string UserInterfaceLanguage { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="EmailBaseCommand"/> class.</summary>
        /// <param name="firstName">The first name.</param>
        /// <param name="fullName">The full name.</param>
        /// <param name="email">The email.</param>
        /// <param name="editionName">Name of the edition.</param>
        /// <param name="editionUrlCode">The edition URL code.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        public EmailBaseCommand(string firstName, string fullName, string email, string editionName, int editionUrlCode, string userInterfaceLanguage)
        {
            this.UpdateBaseProperties(firstName, fullName, email, editionName, editionUrlCode, userInterfaceLanguage);
        }

        /// <summary>Initializes a new instance of the <see cref="EmailBaseCommand"/> class.</summary>
        public EmailBaseCommand()
        {
        }

        /// <summary>Updates the base properties.</summary>
        /// <param name="firstName">The first name.</param>
        /// <param name="fullName">The full name.</param>
        /// <param name="email">The email.</param>
        /// <param name="editionName">Name of the edition.</param>
        /// <param name="editionUrlCode">The edition URL code.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        protected void UpdateBaseProperties(string firstName, string fullName, string email, string editionName, int editionUrlCode, string userInterfaceLanguage)
        {
            this.FirstName = firstName?.Trim();
            this.FullName = fullName?.Trim();
            this.Email = email?.Trim();
            this.EditionName = editionName?.Trim();
            this.EditionUrlCode = editionUrlCode;
            this.UserInterfaceLanguage = userInterfaceLanguage?.Trim();
        }
    }
}