// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 07-27-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-27-2021
// ***********************************************************************
// <copyright file="DeleteAttendeeInnovationOrganization.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>DeleteInnovationOrganization</summary>
    public class DeleteAttendeeInnovationOrganization : BaseCommand
    {
        public Guid AttendeeInnovationOrganizationUid { get; set; }

        /// <summary>Initializes a new instance of the <see cref="DeleteAttendeeInnovationOrganization"/> class.</summary>
        public DeleteAttendeeInnovationOrganization()
        {
        }
    }
}