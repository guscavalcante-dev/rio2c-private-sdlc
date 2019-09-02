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
namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>EmailBaseCommand</summary>
    public class EmailBaseCommand : BaseCommand
    {
        public string EditionName { get; set; }
        public string FirstName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }

        /// <summary>Initializes a new instance of the <see cref="EmailBaseCommand"/> class.</summary>
        /// <param name="editionName">Name of the edition.</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="fullName">The full name.</param>
        /// <param name="email">The email.</param>
        public EmailBaseCommand(string editionName, string firstName, string fullName, string email)
        {
            this.UpdateBaseProperties(editionName, firstName, fullName, email);
        }

        /// <summary>Initializes a new instance of the <see cref="EmailBaseCommand"/> class.</summary>
        public EmailBaseCommand()
        {
        }

        /// <summary>Updates the base properties.</summary>
        /// <param name="editionName">Name of the edition.</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="fullName">The full name.</param>
        /// <param name="email">The email.</param>
        protected void UpdateBaseProperties(string editionName, string firstName, string fullName, string email)
        {
            this.EditionName = editionName?.Trim();
            this.FirstName = firstName?.Trim();
            this.FullName = fullName?.Trim();
            this.Email = email?.Trim();
        }
    }
}