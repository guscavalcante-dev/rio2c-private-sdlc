// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-21-2019
// ***********************************************************************
// <copyright file="DeleteOrganization.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>DeleteOrganization</summary>
    public class DeleteOrganization : BaseCommand
    {
        public Guid OrganizationUid { get; set; }
        public OrganizationType OrganizationType { get; private set; }

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

        /// <summary>Updates the base properties.</summary>
        /// <param name="organizationType">Type of the organization.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="userUid">The user uid.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="editionUid">The edition uid.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        public void UpdateBaseProperties(
            OrganizationType organizationType,
            int userId,
            Guid userUid,
            int? editionId,
            Guid? editionUid,
            string userInterfaceLanguage)
        {
            this.OrganizationType = organizationType;
            this.UpdateBaseProperties(userId, userUid, editionId, editionUid, UserInterfaceLanguage);
        }
    }
}
