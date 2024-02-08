// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 12-12-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 02-08-2024
// ***********************************************************************
// <copyright file="CreateTinyCollaborator.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>CreateTinyCollaborator</summary>
    public class CreateTinyCollaborator : CollaboratorBaseCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateTinyCollaborator" /> class.
        /// </summary>
        /// <param name="firstName">The first name.</param>
        /// <param name="email">The email.</param>
        /// <param name="cellPhone">The cell phone.</param>
        /// <param name="document">The document.</param>
        public CreateTinyCollaborator(
            string firstName,
            string email,
            string cellPhone,
            string document)
        {
            base.FirstName = firstName;
            base.Email = email;
            base.CellPhone = cellPhone;
            base.Document = document;
        }

        /// <summary>Initializes a new instance of the <see cref="CreateTinyCollaborator"/> class.</summary>
        public CreateTinyCollaborator()
        {
        }
    }
}