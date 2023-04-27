// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 04-26-2023
//
// Last Modified By : Renan Valentim
// Last Modified On : 04-26-2023
// ***********************************************************************
// <copyright file="UpdateVirtualMeetingJoinDate.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>UpdateVirtualMeetingJoinDate</summary>
    public class UpdateVirtualMeetingJoinDate : NegotiationBaseCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateVirtualMeetingJoinDate"/> class.
        /// </summary>
        /// <param name="negotiationDto">The negotiation.</param>
        public UpdateVirtualMeetingJoinDate()
        {
        }
    }
}