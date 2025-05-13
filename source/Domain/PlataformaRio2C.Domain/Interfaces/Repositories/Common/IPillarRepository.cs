// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 01-04-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-08-2020
// ***********************************************************************
// <copyright file="IPillarRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using X.PagedList;

namespace PlataformaRio2C.Domain.Interfaces
{
    /// <summary>IPillarRepository</summary>
    public interface IPillarRepository : IRepository<Pillar>
    {
        Task<PillarDto> FindDtoAsync(Guid pillarUid, int editionId);
        Task<PillarDto> FindConferenceWidgetDtoAsync(Guid pillarUid, int editionId);
        Task<List<Pillar>> FindAllByEditionIdAsync(int editionId);
        Task<List<Pillar>> FindAllByUidsAsync(List<Guid> pillarUids);
        Task<IPagedList<PillarJsonDto>> FindAllByDataTable(int page, int pageSize, string keywords, List<Tuple<string, string>> sortColumns, List<Guid> pillarUids, int editionId, int languageId);
        Task<int> CountAllByDataTable(bool showAllEditions, int editionId);
    }
}