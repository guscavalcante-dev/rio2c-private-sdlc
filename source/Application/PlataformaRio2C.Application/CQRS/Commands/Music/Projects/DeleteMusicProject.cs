// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 03-01-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-01-2020
// ***********************************************************************
// <copyright file="DeleteMusicProject.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>DeleteMusicProject</summary>
    public class DeleteMusicProject : BaseCommand
    {
        public Guid MusicProjectUid { get; set; }

        /// <summary>Initializes a new instance of the <see cref="DeleteMusicProject"/> class.</summary>
        public DeleteMusicProject()
        {
        }
    }
}