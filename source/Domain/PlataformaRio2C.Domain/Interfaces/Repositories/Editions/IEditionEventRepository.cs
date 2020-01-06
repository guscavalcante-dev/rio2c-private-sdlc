// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 01-04-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-06-2020
// ***********************************************************************
// <copyright file="IEditionEventRepository.cs" company="Softo">
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
    /// <summary>IEditionEventRepository</summary>
    public interface IEditionEventRepository : IRepository<EditionEvent>
    {
        Task<EditionEventDto> FindDtoAsync(Guid editionEventUid, int editionId);
        Task<EditionEventDto> FindConferenceWidgetDtoAsync(Guid editionEventUid, int editionId);
        Task<EditionEvent> FindByUidAsync(Guid editionEventUid);
        Task<List<EditionEvent>> FindAllByEditionIdAsync(int editionId);
        Task<IPagedList<EditionEventJsonDto>> FindAllByDataTable(int page, int pageSize, string keywords, List<Tuple<string, string>> sortColumns, List<Guid> editionEventUids, int editionId, int languageId);
        Task<int> CountAllByDataTable(bool showAllEditions, int editionId);
    }    
}