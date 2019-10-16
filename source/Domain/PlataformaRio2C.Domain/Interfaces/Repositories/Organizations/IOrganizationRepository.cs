// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 08-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 10-14-2019
// ***********************************************************************
// <copyright file="IOrganizationRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using X.PagedList;

namespace PlataformaRio2C.Domain.Interfaces
{
    /// <summary>IOrganizationRepository</summary>
    public interface IOrganizationRepository : IRepository<Organization>
    {
        Task<OrganizationDto> FindDtoByUidAsync(Guid organizationUid, int editionId);
        Task<IPagedList<OrganizationBaseDto>> FindAllByDataTable(int page, int pageSize, string keywords, List<Tuple<string, string>> sortColumns, Guid organizationTypeId, bool showAllEditions, bool showAllOrganizations, int? editionId);
        Task<int> CountAllByDataTable(Guid organizationTypeId, bool showAllEditions, int? editionId);

        #region Api

        Task<IPagedList<OrganizationApiListDto>> FindAllPublicApiPaged(int editionId, string keywords, Guid organizationTypeUid, int page, int pageSize);
        Task<IPagedList<OrganizationApiListDto>> FindAllOrganizationsApiPaged(int editionId, string companyName, string tradeName, string companyNumber, int page, int pageSize);
        Task<OrganizationDto> FindApiDtoByUidAsync(Guid organizationUid, int editionId, Guid organizationTypeUid);
        #endregion

        #region Old

        Task<List<Organization>> GetAllAsync();

        #endregion
    }
}