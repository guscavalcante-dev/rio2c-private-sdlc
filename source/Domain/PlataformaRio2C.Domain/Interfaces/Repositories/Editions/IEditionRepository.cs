// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-03-2019
// ***********************************************************************
// <copyright file="IEditionRepository.cs" company="Softo">
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
    /// <summary>IEditionRepository</summary>
    public interface IEditionRepository : IRepository<Edition>
    {
        Task<EditionDto> FindDtoAsync(Guid editionUid);
        Task<EditionDto> FindEventsWidgetDtoAsync(Guid editionUid);
        Task<Edition> FindByUidAsync(Guid editionUid, bool showInactive);
        Task<Edition> FindByIsCurrentAsync();
        List<Edition> FindAllByIsActive(bool showInactive);
        Task<IPagedList<EditionJsonDto>> FindAllByDataTable(int page, int pageSize, string keywords, List<Tuple<string, string>> sortColumns, List<Guid> editionUids, int editionId, int languageId);
        Task<int> CountAllByDataTable(bool showAllEditions, Guid editionUid);
    }    
}