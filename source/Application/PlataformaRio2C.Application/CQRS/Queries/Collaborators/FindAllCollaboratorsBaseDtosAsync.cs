// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-26-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-26-2019
// ***********************************************************************
// <copyright file="FindAllCollaboratorsBaseDtosAsync.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using MediatR;
using PlataformaRio2C.Domain.Dtos;
using X.PagedList;

namespace PlataformaRio2C.Application.CQRS.Queries
{
    /// <summary>FindAllCollaboratorsBaseDtosAsync</summary>
    public class FindAllCollaboratorsBaseDtosAsync : BaseUserRequest, IRequest<IPagedList<CollaboratorBaseDto>>
    {
        public int Page { get; private set; }
        public int PageSize { get; private set; }
        public string Keywords { get; private set; }
        public List<Tuple<string, string>> SortColumns { get; private set; }
        public Guid OrganizationTypeId { get; private set; }
        public bool ShowAllEditions { get; private set; }
        public bool ShowAllCollaborators { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="FindAllCollaboratorsBaseDtosAsync"/> class.</summary>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="keywords">The keywords.</param>
        /// <param name="sortColumns">The sort columns.</param>
        /// <param name="organizationTypeId">The organization type identifier.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <param name="showAllCollaborators">if set to <c>true</c> [show all collaborators].</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="userUid">The user uid.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="editionUid">The edition uid.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        public FindAllCollaboratorsBaseDtosAsync(
            int page, 
            int pageSize, 
            string keywords, 
            List<Tuple<string, string>> sortColumns,
            Guid organizationTypeId,
            bool showAllEditions,
            bool showAllCollaborators,
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
            this.ShowAllCollaborators = showAllCollaborators;
        }
    }
}