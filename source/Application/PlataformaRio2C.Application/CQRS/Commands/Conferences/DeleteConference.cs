// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-02-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-02-2020
// ***********************************************************************
// <copyright file="DeleteConference.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>DeleteConference</summary>
    public class DeleteConference : BaseCommand
    {
        public Guid ConferenceUid { get; set; }

        /// <summary>Initializes a new instance of the <see cref="DeleteConference"/> class.</summary>
        public DeleteConference()
        {
        }
    }
}