// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-22-2019
// ***********************************************************************
// <copyright file="FindAllOrganizationsBaseDtosAsync.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using PlataformaRio2C.Domain.Dtos;
using System;
using System.Collections.Generic;
using X.PagedList;

namespace PlataformaRio2C.Application.CQRS.Queries
{
    /// <summary>FindAllOrganizationsBaseDtosAsync</summary>
    public class FindAllOrganizationsBaseDtosAsync : BaseUserRequest, IRequest<IPagedList<OrganizationBaseDto>>
    {
        public int Page { get; private set; }
        public int PageSize { get; private set; }
        public string Keywords { get; private set; }
        public List<Tuple<string, string>> SortColumns { get; private set; }
        public Guid OrganizationTypeId { get; private set; }
        public bool ShowAllEditions { get; private set; }
        public bool ShowAllOrganizations { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="FindAllOrganizationsBaseDtosAsync"/> class.</summary>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="keywords">The keywords.</param>
        /// <param name="sortColumns">The sort columns.</param>
        /// <param name="organizationTypeId">The organization type identifier.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <param name="showAllOrganizations">if set to <c>true</c> [show all organizations].</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="userUid">The user uid.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="editionUid">The edition uid.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        public FindAllOrganizationsBaseDtosAsync(
            int page,
            int pageSize,
            string keywords,
            List<Tuple<string, string>> sortColumns,
            Guid organizationTypeId,
            bool showAllEditions,
            bool showAllOrganizations,
            int userId,
            Guid userUid,
            int? editionId,
            Guid? editionUid,
            string userInterfaceLanguage)
            : base(userId, userUid, editionId, editionUid, userInterfaceLanguage)
        {
            this.Page = page;
            this.PageSize = pageSize;
            this.Keywords = keywords;
            this.SortColumns = sortColumns;
            this.OrganizationTypeId = organizationTypeId;
            this.ShowAllEditions = showAllEditions;
            this.ShowAllOrganizations = showAllOrganizations;
        }
    }
}