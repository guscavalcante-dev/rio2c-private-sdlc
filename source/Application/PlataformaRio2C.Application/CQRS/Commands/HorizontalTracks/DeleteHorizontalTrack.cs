// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-07-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-07-2020
// ***********************************************************************
// <copyright file="DeleteHorizontalTrack.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>DeleteHorizontalTrack</summary>
    public class DeleteHorizontalTrack : BaseCommand
    {
        public Guid HorizontalTrackUid { get; set; }

        /// <summary>Initializes a new instance of the <see cref="DeleteHorizontalTrack"/> class.</summary>
        public DeleteHorizontalTrack()
        {
        }
    }
}