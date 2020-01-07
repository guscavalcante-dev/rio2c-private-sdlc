// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-06-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-07-2020
// ***********************************************************************
// <copyright file="DeleteTrack.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>DeleteTrack</summary>
    public class DeleteTrack : BaseCommand
    {
        public Guid TrackUid { get; set; }

        /// <summary>Initializes a new instance of the <see cref="DeleteTrack"/> class.</summary>
        public DeleteTrack()
        {
        }
    }
}