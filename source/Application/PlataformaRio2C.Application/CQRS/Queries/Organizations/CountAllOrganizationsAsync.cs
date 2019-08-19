// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-19-2019
// ***********************************************************************
// <copyright file="CountAllOrganizatiosAsync.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using MediatR;

namespace PlataformaRio2C.Application.CQRS.Queries
{
    /// <summary>CountAllOrganizatiosAsync</summary>
    public class CountAllOrganizationsAsync : BaseUserRequest, IRequest<int>
    {
        public Guid OrganizationTypeId { get; private set; }
        public bool ShowAllEditions { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="CountAllOrganizationsAsync"/> class.</summary>
        /// <param name="organizationTypeId">The organization type identifier.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="userUid">The user uid.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="editionUid">The edition uid.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        public CountAllOrganizationsAsync(
            Guid organizationTypeId,
            bool showAllEditions,
            int userId, 
            Guid userUid, 
            int? editionId, 
            Guid? editionUid, 
            string userInterfaceLanguage)
            : base(userId, userUid, editionId, editionUid, userInterfaceLanguage)
        {
            this.OrganizationTypeId = organizationTypeId;
            this.ShowAllEditions = showAllEditions;
        }
    }
}