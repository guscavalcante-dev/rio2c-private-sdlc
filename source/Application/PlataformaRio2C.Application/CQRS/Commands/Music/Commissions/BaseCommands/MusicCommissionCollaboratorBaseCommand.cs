// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 11-19-2024
//
// Last Modified By : Renan Valentim
// Last Modified On : 11-19-2024
// ***********************************************************************
// <copyright file="MusicCommissionCollaboratorBaseCommand.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using System.Collections.Generic;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    public class MusicCommissionCollaboratorBaseCommand : CollaboratorBaseCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MusicCommissionCollaboratorBaseCommand" /> class.
        /// </summary>
        public MusicCommissionCollaboratorBaseCommand()
        {
        }

        /// <summary>
        /// Updates the base properties.
        /// </summary>
        /// <param name="collaboratorTypes">The collaborator types.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        public void UpdateBaseProperties(
            CollaboratorDto entity,
            List<CollaboratorType> collaboratorTypes,
            string userInterfaceLanguage)
        {
            base.UpdateBaseProperties(entity);
            this.UpdateDropdownProperties(collaboratorTypes, userInterfaceLanguage);
        }

        /// <summary>
        /// Updates the dropdown properties.
        /// </summary>
        /// <param name="MusicOrganizationTrackOptionDtos">The Music organization track option dtos.</param>
        public void UpdateDropdownProperties(
            List<CollaboratorType> collaboratorTypes,
            string userInterfaceLanguage)
        {
            base.UpdateCollaboratorTypes(collaboratorTypes, userInterfaceLanguage);
        }
    }
}