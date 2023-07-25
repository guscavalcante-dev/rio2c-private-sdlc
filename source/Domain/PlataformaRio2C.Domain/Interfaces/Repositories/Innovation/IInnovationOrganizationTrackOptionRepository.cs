// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 07-13-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-25-2023
// ***********************************************************************
// <copyright file="IInnovationOrganizationTrackOptionRepository.cs" company="Softo">
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
    /// <summary>IInnovationOrganizationTrackOptionRepository</summary>
    public interface IInnovationOrganizationTrackOptionRepository : IRepository<InnovationOrganizationTrackOption>
    {
        Task<InnovationOrganizationTrackOptionDto> FindMainInformationWidgetDtoAsync(Guid innovationOrganizationTrackOptionUid);
        InnovationOrganizationTrackOption FindById(int innovationOrganizationTrackOptionId);
        InnovationOrganizationTrackOption FindByUid(Guid innovationOrganizationTrackOptionUid);
        Task<InnovationOrganizationTrackOption> FindByIdAsync(int innovationOrganizationTrackOptionId);
        Task<InnovationOrganizationTrackOption> FindByUidAsync(Guid innovationOrganizationTrackOptionUid);
        Task<List<InnovationOrganizationTrackOption>> FindAllAsync();
        Task<List<InnovationOrganizationTrackOptionDto>> FindAllDtoAsync();
        Task<List<InnovationOrganizationTrackOption>> FindAllByIdsAsync(List<int?> innovationOrganizationTrackOptionIds);
        Task<List<InnovationOrganizationTrackOption>> FindAllByUidsAsync(List<Guid?> innovationOrganizationTrackOptionUids);
        Task<List<InnovationOrganizationTrackOption>> FindAllByAttendeeCollaboratorIdAsync(int attendeeCollaboratorId);
        Task<List<InnovationOrganizationTrackOption>> FindAllByGroupsUidsAsync(IEnumerable<Guid?> innovationOrganizationTrackOptionGroupsUids);
        Task<IPagedList<InnovationOrganizationTrackOptionDto>> FindAllByDataTable(int page, int pageSize, string keywords, List<Tuple<string, string>> sortColumns);
        Task<int> CountAllByDataTable(bool showAllEditions, int editionId);
    }
}