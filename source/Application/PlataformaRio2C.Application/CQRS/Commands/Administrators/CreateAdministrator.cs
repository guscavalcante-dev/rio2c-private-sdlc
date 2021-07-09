// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 04-24-2021
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 07-09-2021
// ***********************************************************************
// <copyright file="CreateAdministrator.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>CreateAdministrator</summary>
    public class CreateAdministrator : AdministratorBaseCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateAdministrator"/> class.
        /// </summary>
        /// <param name="roles">The roles.</param>
        /// <param name="collaboratorTypes">The collaborator types.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        public CreateAdministrator(
            List<Role> roles,
            List<CollaboratorType> collaboratorTypes,
            string userInterfaceLanguage)
        {
            base.IsCreatingNewManager = true;

            this.UpdateBaseProperties(
                null,
                roles,
                collaboratorTypes,
                userInterfaceLanguage);
        }

        /// <summary>Initializes a new instance of the <see cref="CreateAdministrator"/> class.</summary>
        public CreateAdministrator()
        {
        }
    }
}