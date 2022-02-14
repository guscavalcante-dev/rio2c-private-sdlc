// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 02-12-2022
//
// Last Modified By : Renan Valentim
// Last Modified On : 02-12-2022
// ***********************************************************************
// <copyright file="DeleteCartoonProject.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>DeleteCartoonProject</summary>
    public class DeleteCartoonProject : BaseCommand
    {
        public Guid CartoonProjectUid { get; set; }

        /// <summary>Initializes a new instance of the <see cref="DeleteCartoonProject"/> class.</summary>
        public DeleteCartoonProject()
        {
        }
    }
}