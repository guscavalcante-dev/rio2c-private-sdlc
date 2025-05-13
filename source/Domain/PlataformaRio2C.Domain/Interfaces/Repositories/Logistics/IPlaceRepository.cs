// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Arthur Souza
// Created          : 01-20-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-17-2020
// ***********************************************************************
// <copyright file="IPlaceRepository.cs" company="Softo">
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
    /// <summary>
    /// IPlaceRepository
    /// </summary>
    public interface IPlaceRepository : IRepository<Place>
    {
        Task<PlaceDto> FindDtoAsync(Guid placeUid);
        Task<PlaceDto> FindMainInformationWidgetDtoAsync(Guid placeUid);
        Task<IPagedList<PlaceJsonDto>> FindAllByDataTableAsync(int page, int pageSize, string keywords, List<Tuple<string, string>> sortColumns, int editionId, bool showAllEditions, int languageId);
        Task<int> CountAllByDataTableAsync(int editionId, bool showAllEditions);
    }
}