// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 12-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-19-2019
// ***********************************************************************
// <copyright file="DeleteCollaboratorOrganization.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>DeleteCollaboratorOrganization</summary>
    public class DeleteCollaboratorOrganization : BaseCommand
    {
        public Guid CollaboratorUid { get; set; }
        public Guid OrganizationUid { get; set; }

        /// <summary>Initializes a new instance of the <see cref="DeleteCollaboratorOrganization"/> class.</summary>
        public DeleteCollaboratorOrganization()
        {
        }
    }
}