// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 01-04-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-07-2020
// ***********************************************************************
// <copyright file="IHorizontalTrackRepository.cs" company="Softo">
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
    /// <summary>IHorizontalTrackRepository</summary>
    public interface IHorizontalTrackRepository : IRepository<HorizontalTrack>
    {
        Task<HorizontalTrackDto> FindDtoAsync(Guid horizontalTrackUid, int editionId);
        Task<HorizontalTrackDto> FindConferenceWidgetDtoAsync(Guid horizontalTrackUid, int editionId);
        Task<List<HorizontalTrack>> FindAllAsync();
        Task<List<HorizontalTrack>> FindAllByUidsAsync(List<Guid> horizontalTrackUids);
        Task<IPagedList<HorizontalTrackJsonDto>> FindAllByDataTable(int page, int pageSize, string keywords, List<Tuple<string, string>> sortColumns, List<Guid> horizontalTrackUids, int editionId, int languageId);
        Task<int> CountAllByDataTable(bool showAllEditions, int editionId);
    }    
}