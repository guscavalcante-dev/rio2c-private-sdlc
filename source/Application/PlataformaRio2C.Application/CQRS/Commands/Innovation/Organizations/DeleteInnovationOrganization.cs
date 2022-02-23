// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 07-27-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 02-23-2022
// ***********************************************************************
// <copyright file="DeleteInnovationOrganization.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>DeleteInnovationOrganization</summary>
    public class DeleteInnovationOrganization : BaseCommand
    {
        public Guid InnovationOrganizationUid { get; set; }

        /// <summary>Initializes a new instance of the <see cref="DeleteInnovationOrganization"/> class.</summary>
        public DeleteInnovationOrganization()
        {
        }
    }
}