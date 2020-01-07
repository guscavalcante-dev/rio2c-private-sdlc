// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-06-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-06-2020
// ***********************************************************************
// <copyright file="DeleteVerticalTrack.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>DeleteVerticalTrack</summary>
    public class DeleteVerticalTrack : BaseCommand
    {
        public Guid VerticalTrackUid { get; set; }

        /// <summary>Initializes a new instance of the <see cref="DeleteVerticalTrack"/> class.</summary>
        public DeleteVerticalTrack()
        {
        }
    }
}