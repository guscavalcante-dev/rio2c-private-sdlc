// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 06-29-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 06-29-2021
// ***********************************************************************
// <copyright file="IAttendeeInnovationOrganizationRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PlataformaRio2C.Domain.Dtos;
using X.PagedList;

namespace PlataformaRio2C.Domain.Interfaces
{
    /// <summary>IAttendeeInnovationOrganizationRepository</summary>
    public interface IAttendeeInnovationOrganizationRepository : IRepository<AttendeeInnovationOrganization>
    {
        Task<IPagedList<AttendeeInnovationOrganizationJsonDto>> FindAllJsonDtosPagedAsync(int editionId, string searchKeywords, Guid? innovationOrganizationTrackOptionUid, Guid? evaluationStatusUid, int page, int pageSize, List<Tuple<string, string>> sortColumns);
        Task<AttendeeInnovationOrganization> FindByIdAsync(int attendeeInnovationOrganizationId);
        Task<AttendeeInnovationOrganization> FindByUidAsync(Guid attendeeInnovationOrganizationUid);
        Task<List<AttendeeInnovationOrganization>> FindAllByIdsAsync(List<int?> attendeeInnovationOrganizationIds);
        Task<List<AttendeeInnovationOrganization>> FindAllByUidsAsync(List<Guid?> attendeeInnovationOrganizationUids);
        Task<AttendeeInnovationOrganization> FindByDocumentAndEditionIdAsync(string document, int editionId);
        Task<int[]> FindAllApprovedAttendeeInnovationOrganizationsIdsAsync(int editionId);
        Task<int> CountAsync(int editionId, bool showAllEditions = false);
        Task<int> CountPagedAsync(int editionId, string searchKeywords, Guid? musicGenreUid, Guid? evaluationStatusUid, int page, int pageSize);
    }
}