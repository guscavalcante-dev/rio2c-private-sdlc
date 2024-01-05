// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 07-19-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-19-2021
// ***********************************************************************
// <copyright file="CreateAudiovisualCommissionCollaborator.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using PlataformaRio2C.Domain.Dtos;
using System.Collections.Generic;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    public class CreateAudiovisualCommissionCollaborator : AudiovisualCommissionCollaboratorBaseCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateAudiovisualCommissionCollaborator"/> class.
        /// </summary>
        /// <param name="innovationOptions">The innovation options.</param>
        public CreateAudiovisualCommissionCollaborator(List<InterestDto> interestsDtos)
        {
            this.UpdateBaseProperties(null, interestsDtos);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateAudiovisualCommissionCollaborator" /> class.
        /// </summary>
        public CreateAudiovisualCommissionCollaborator()
        {
        }
    }
}