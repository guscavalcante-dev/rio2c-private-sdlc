// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 04-24-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 04-24-2021
// ***********************************************************************
// <copyright file="DeleteManager.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>DeleteManager</summary>
    public class DeleteManager : BaseCommand
    {
        public Guid CollaboratorUid { get; set; }
        public bool IsDeletingFromCurrentEdition { get; set; }

        /// <summary>Initializes a new instance of the <see cref="DeleteManager"/> class.</summary>
        public DeleteManager()
        {
        }
    }
}