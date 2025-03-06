// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Ribeiro 
// Created          : 05-03-2025
//
// Last Modified By : Rafael Ribeiro 
// Last Modified On : 05-03-2025
// ***********************************************************************
// <copyright file="CreateAttendeeNegotiationCollaborator.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>CreateAttendeeNegotiationCollaborator</summary>
    public class CreateAttendeeMusicBusinessNegotiationCollaborator : MusicbusinessRoundnegotiationBaseCommand
    {
        internal Guid MusicBusineesNegotiationUid;

        public Guid MusicBusinessNegotiationUid { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateAttendeeNegotiationCollaborator"/> class.
        /// </summary>
        /// <param name="negotiationUid">The negotiation uid.</param>
        public CreateAttendeeMusicBusinessNegotiationCollaborator(Guid negotiationUid)
        {
            this.MusicBusinessNegotiationUid = negotiationUid;
        }
    }
}