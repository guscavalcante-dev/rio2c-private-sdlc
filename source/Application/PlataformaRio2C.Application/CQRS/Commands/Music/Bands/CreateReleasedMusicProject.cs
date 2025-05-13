// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 03-24-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 03-24-2021
// ***********************************************************************
// <copyright file="CreateReleasedMusicProject.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Dtos;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>CreateReleasedMusicProject</summary>
    public class CreateReleasedMusicProject : ReleasedMusicProjectBaseCommand
    {
        /// <summary>Initializes a new instance of the <see cref="CreateMusicBandTeamMember"/> class.</summary>
        public CreateReleasedMusicProject(ReleasedMusicProjectApiDto releasedMusicProjectApiDto)
        {
            this.UpdateBaseProperties(releasedMusicProjectApiDto);
        }
    }
}