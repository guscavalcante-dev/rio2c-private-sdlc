// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 07-19-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-19-2021
// ***********************************************************************
// <copyright file="CreateAudiovisualCollaborator.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>CreateAudiovisualCollaborator</summary>
    public class CreateAudiovisualCollaborator : AudiovisualCollaboratorBaseCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateAudiovisualCollaborator"/> class.
        /// </summary>
        /// <param name="innovationOptions">The innovation options.</param>
        public CreateAudiovisualCollaborator(List<Interest> interests)
        {
            this.UpdateBaseProperties(null, interests);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateAudiovisualCollaborator" /> class.
        /// </summary>
        public CreateAudiovisualCollaborator()
        {
        }
    }
}