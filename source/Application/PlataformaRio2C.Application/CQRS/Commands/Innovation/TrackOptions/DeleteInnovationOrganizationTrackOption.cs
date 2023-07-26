// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 07-26-2023
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-26-2023
// ***********************************************************************
// <copyright file="DeleteInnovationOrganizationTrackOption.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>DeleteInnovationOrganizationTrackOption</summary>
    public class DeleteInnovationOrganizationTrackOption : BaseCommand
    {
        public Guid InnovationOrganizationTrackOptionUid { get; set; }

        /// <summary>Initializes a new instance of the <see cref="DeleteInnovationOrganizationTrackOption"/> class.</summary>
        public DeleteInnovationOrganizationTrackOption()
        {
        }
    }
}