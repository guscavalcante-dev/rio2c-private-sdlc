// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 07-17-2023
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-17-2023
// ***********************************************************************
// <copyright file="DeleteInnovationOrganizationTrackOptionGroup.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>DeleteInnovationOrganizationTrackOptionGroup</summary>
    public class DeleteInnovationOrganizationTrackOptionGroup : BaseCommand
    {
        public Guid InnovationOrganizationTrackOptionGroupUid { get; set; }

        /// <summary>Initializes a new instance of the <see cref="DeleteInnovationOrganizationTrackOptionGroup"/> class.</summary>
        public DeleteInnovationOrganizationTrackOptionGroup()
        {
        }
    }
}