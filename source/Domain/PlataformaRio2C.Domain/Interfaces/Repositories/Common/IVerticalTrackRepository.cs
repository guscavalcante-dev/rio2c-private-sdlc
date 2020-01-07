// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 01-04-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-06-2020
// ***********************************************************************
// <copyright file="IVerticalTrackRepository.cs" company="Softo">
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
    /// <summary>IVerticalTrackRepository</summary>
    public interface IVerticalTrackRepository : IRepository<VerticalTrack>
    {
        Task<VerticalTrackDto> FindDtoAsync(Guid verticalTrackUid, int editionId);
        Task<List<VerticalTrack>> FindAllAsync();
        Task<List<VerticalTrack>> FindAllByUidsAsync(List<Guid> verticalTrackUids);
        Task<IPagedList<VerticalTrackJsonDto>> FindAllByDataTable(int page, int pageSize, string keywords, List<Tuple<string, string>> sortColumns, List<Guid> verticalTrackUids, int editionId, int languageId);
        Task<int> CountAllByDataTable(bool showAllEditions, int editionId);
    }    
}