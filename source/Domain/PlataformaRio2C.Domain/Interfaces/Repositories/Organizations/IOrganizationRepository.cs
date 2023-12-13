// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 08-19-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 12-13-2023
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
        Task<IPagedList<OrganizationBaseDto>> FindAllByDataTable(int page, int pageSize, string keywords, List<Tuple<string, string>> sortColumns, Guid organizationTypeUid, bool showAllEditions, bool showAllOrganizations, int? editionId);
        Task<int> CountAllByDataTable(Guid organizationTypeId, bool showAllEditions, int? editionId);
        Task<IPagedList<OrganizationApiListDto>> FindAllDropdownApiListDtoPaged(int editionId, string keywords, string customFilter, Guid? organizationTypeUid, int page, int pageSize);

        #region Players

        Task<IPagedList<OrganizationDto>> FindAllPlayersByDataTable(int page, int pageSize, string keywords, List<Tuple<string, string>> sortColumns, bool showAllEditions, bool showAllOrganizations, int? editionId, bool exportToExcel = false);

        #endregion

        #region Api

        Task<IPagedList<OrganizationApiListDto>> FindAllPublicApiPaged(int editionId, string keywords, Guid organizationTypeUid, List<Guid> activitiesUids, List<Guid> targetAudiencesUids, List<Guid> interestsUids, DateTime? modifiedAfterDate, int page, int pageSize);
        Task<IPagedList<OrganizationApiListDto>> FindAllOrganizationsApiPaged(int? editionId, string companyName, string tradeName, string document, Guid organizationTypeUid, int? collaboratorId, bool showAllEditions, bool showAllOrganizations, int page, int pageSize);
        Task<OrganizationDto> FindApiDtoByUidAsync(Guid organizationUid, int editionId, Guid organizationTypeUid);
        Task<List<Organization>> FindAllByHightlightPosition(int editionId, Guid organizationTypeUid, int apiHighlightPosition, Guid? organizationUid);

        #endregion

        #region Old

        Task<List<Organization>> GetAllAsync();

        #endregion
    }
}