// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 01-03-2022
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-03-2023
// ***********************************************************************
// <copyright file="IInnovationOrganizationTrackOptionGroupRepository.cs" company="Softo">
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
    /// <summary>IInnovationOrganizationTrackOptionGroupRepository</summary>
    public interface IInnovationOrganizationTrackOptionGroupRepository : IRepository<InnovationOrganizationTrackOptionGroup>
    {
        Task<List<InnovationOrganizationTrackOptionGroup>> FindAllAsync();
        Task<List<InnovationOrganizationTrackOptionGroupDto>> FindAllDtoAsync();
        Task<List<InnovationOrganizationTrackOptionGroup>> FindAllByAttendeeCollaboratorIdAsync(int attendeeCollaboratorId);
        Task<IPagedList<InnovationOrganizationTrackOptionGroupDto>> FindAllByDataTable(int page, int pageSize, string keywords, List<Tuple<string, string>> sortColumns);
        Task<int> CountAllByDataTable(bool showAllEditions, int editionId);
    }
}