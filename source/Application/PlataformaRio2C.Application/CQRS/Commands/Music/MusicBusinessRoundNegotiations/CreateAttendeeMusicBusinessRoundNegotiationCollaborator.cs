﻿// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Ribeiro 
// Created          : 05-03-2025
//
// Last Modified By : Rafael Ribeiro 
// Last Modified On : 05-03-2025
// ***********************************************************************
// <copyright file="CreateAttendeeMusicBusinessRoundNegotiationCollaborator.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>CreateAttendeeMusicBusinessRoundNegotiationCollaborator</summary>
    public class CreateAttendeeMusicBusinessRoundNegotiationCollaborator : MusicBusinessRoundNegotiationBaseCommand
    {
        internal Guid MusicBusinesRoundNegotiationUid;

        public Guid MusicBusinessRoundNegotiationUid { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateAttendeeMusicBusinessNegotiationCollaborator"/> class.
        /// </summary>
        /// <param name="negotiationUid">The negotiation uid.</param>
        public CreateAttendeeMusicBusinessRoundNegotiationCollaborator(Guid negotiationUid)
        {
            this.MusicBusinesRoundNegotiationUid = negotiationUid;
        }
    }
}