// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-06-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-06-2020
// ***********************************************************************
// <copyright file="DeleteEditionEvent.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>DeleteEditionEvent</summary>
    public class DeleteEditionEvent : BaseCommand
    {
        public Guid EditionEventUid { get; set; }

        /// <summary>Initializes a new instance of the <see cref="DeleteEditionEvent"/> class.</summary>
        public DeleteEditionEvent()
        {
        }
    }
}