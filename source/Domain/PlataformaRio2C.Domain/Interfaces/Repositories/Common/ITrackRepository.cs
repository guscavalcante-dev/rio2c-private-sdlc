// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 01-04-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-07-2020
// ***********************************************************************
// <copyright file="ITrackRepository.cs" company="Softo">
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
    /// <summary>ITrackRepository</summary>
    public interface ITrackRepository : IRepository<Track>
    {
        Task<TrackDto> FindDtoAsync(Guid trackUid, int editionId);
        Task<TrackDto> FindConferenceWidgetDtoAsync(Guid trackUid, int editionId);
        Task<List<Track>> FindAllAsync(int editionId);
        Task<List<Track>> FindAllByUidsAsync(List<Guid> trackUids);
        Task<IPagedList<TrackJsonDto>> FindAllByDataTable(int page, int pageSize, string keywords, List<Tuple<string, string>> sortColumns, List<Guid> trackUids, int editionId, int languageId);
        Task<int> CountAllByDataTable(bool showAllEditions, int editionId);
    }    
}