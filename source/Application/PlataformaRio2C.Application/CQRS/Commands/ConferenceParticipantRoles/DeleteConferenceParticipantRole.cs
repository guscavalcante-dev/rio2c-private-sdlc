// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-07-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-07-2020
// ***********************************************************************
// <copyright file="DeleteConference.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>DeleteConferenceParticipantRole</summary>
    public class DeleteConferenceParticipantRole : BaseCommand
    {
        public Guid ConferenceParticipantRoleUid { get; set; }

        /// <summary>Initializes a new instance of the <see cref="DeleteConferenceParticipantRole"/> class.</summary>
        public DeleteConferenceParticipantRole()
        {
        }
    }
}