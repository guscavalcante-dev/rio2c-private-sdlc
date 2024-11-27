// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 11-19-2024
//
// Last Modified By : Renan Valentim
// Last Modified On : 11-19-2024
// ***********************************************************************
// <copyright file="CreateMusicCommissionCollaborator.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Collections.Generic;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    public class CreateMusicCommissionCollaborator : MusicCommissionCollaboratorBaseCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateMusicCommissionCollaborator"/> class.
        /// </summary>
        /// <param name="MusicOptions">The Music options.</param>
        public CreateMusicCommissionCollaborator(List<CollaboratorType> collaboratorTypes, string userInterfaceLanguage)
        {
            this.UpdateBaseProperties(
                null, 
                collaboratorTypes, 
                userInterfaceLanguage);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateMusicCommissionCollaborator" /> class.
        /// </summary>
        public CreateMusicCommissionCollaborator()
        {
        }
    }
}