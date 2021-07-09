// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 04-24-2021
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 07-09-2021
// ***********************************************************************
// <copyright file="DeleteAdministrator.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>DeleteAdministrator</summary>
    public class DeleteAdministrator : BaseCommand
    {
        public Guid CollaboratorUid { get; set; }
        public bool IsDeletingFromCurrentEdition { get; set; }

        /// <summary>Initializes a new instance of the <see cref="DeleteAdministrator"/> class.</summary>
        public DeleteAdministrator()
        {
        }
    }
}