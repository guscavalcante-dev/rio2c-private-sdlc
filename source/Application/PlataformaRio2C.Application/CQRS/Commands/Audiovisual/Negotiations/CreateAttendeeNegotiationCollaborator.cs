// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 04-26-2023
//
// Last Modified By : Renan Valentim
// Last Modified On : 04-26-2023
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
    public class CreateAttendeeNegotiationCollaborator : NegotiationBaseCommand
    {
        public Guid NegotiationUid { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateAttendeeNegotiationCollaborator"/> class.
        /// </summary>
        /// <param name="negotiationUid">The negotiation uid.</param>
        public CreateAttendeeNegotiationCollaborator(Guid negotiationUid)
        {
            this.NegotiationUid = negotiationUid;
        }
    }
}