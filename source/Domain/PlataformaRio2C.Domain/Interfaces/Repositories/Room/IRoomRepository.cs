// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-06-2020
// ***********************************************************************
// <copyright file="IRoomRepository.cs" company="Softo">
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
    /// <summary>IRoomRepository</summary>
    public interface IRoomRepository : IRepository<Room>
    {
        Task<RoomDto> FindDtoAsync(Guid roomUid, int editionId);
        Task<RoomDto> FindConferenceWidgetDtoAsync(Guid roomUid, int editionId);
        Task<Room> FindByUidAsync(Guid roomUid);
        Task<List<RoomDto>> FindAllDtoByEditionIdAsync(int editionId);
        Task<IPagedList<RoomJsonDto>> FindAllByDataTable(int page, int pageSize, string keywords, List<Tuple<string, string>> sortColumns, List<Guid> roomUids, int editionId, int languageId);
        Task<int> CountAllByDataTable(bool showAllEditions, int editionId);
    }
}