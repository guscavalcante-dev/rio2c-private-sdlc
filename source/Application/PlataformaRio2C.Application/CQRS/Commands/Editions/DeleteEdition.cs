// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 03-03-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 03-03-2021
// ***********************************************************************
// <copyright file="DeleteEdition.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>DeleteEdition</summary>
    public class DeleteEdition : BaseCommand
    {
        //public Guid EditionUid { get; set; }

        /// <summary>Initializes a new instance of the <see cref="DeleteEdition"/> class.</summary>
        public DeleteEdition()
        {
        }
    }
}