// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 01-04-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-07-2020
// ***********************************************************************
// <copyright file="IPresentationFormatRepository.cs" company="Softo">
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
    /// <summary>IPresentationFormatRepository</summary>
    public interface IPresentationFormatRepository : IRepository<PresentationFormat>
    {
        Task<PresentationFormatDto> FindDtoAsync(Guid presentationFormatUid, int editionId);
        Task<PresentationFormatDto> FindConferenceWidgetDtoAsync(Guid presentationFormatUid, int editionId);
        Task<List<PresentationFormat>> FindAllAsync(int editionId);
        Task<List<PresentationFormat>> FindAllByUidsAsync(List<Guid> presentationFormatUids);
        Task<IPagedList<PresentationFormatJsonDto>> FindAllByDataTable(int page, int pageSize, string keywords, List<Tuple<string, string>> sortColumns, List<Guid> presentationFormatUids, int editionId, int languageId);
        Task<int> CountAllByDataTable(bool showAllEditions, int editionId);
    }    
}