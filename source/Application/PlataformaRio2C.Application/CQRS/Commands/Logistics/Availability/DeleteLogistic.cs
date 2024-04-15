// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 15-04-2024
//
// Last Modified By : Renan Valentim
// Last Modified On : 15-04-2024
// ***********************************************************************
// <copyright file="DeleteAvailability.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>DeleteAvailability</summary>
    public class DeleteAvailability : BaseCommand
    {
        public Guid AttendeeCollaboratorUid { get; set; }

        /// <summary>Initializes a new instance of the <see cref="DeleteAvailability"/> class.</summary>
        public DeleteAvailability()
        {
        }   
    }
}