// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-19-2019
// ***********************************************************************
// <copyright file="DeleteOrganization.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using PlataformaRio2C.Domain.Dtos;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>DeleteOrganization</summary>
    public class DeleteOrganization : BaseCommand
    {
        public Guid OrganizationUid { get; set; }

        /// <summary>Initializes a new instance of the <see cref="DeleteOrganization"/> class.</summary>
        /// <param name="entity">The entity.</param>
        public DeleteOrganization(HoldingDto entity)
        {
            this.OrganizationUid = entity.Uid;
        }

        /// <summary>Initializes a new instance of the <see cref="DeleteOrganization"/> class.</summary>
        public DeleteOrganization()
        {
        }
    }
}
