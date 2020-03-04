// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-04-2020
// ***********************************************************************
// <copyright file="INegotiationConfigRepository.cs" company="Softo">
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
    /// <summary>INegotiationConfigRepository</summary>
    public interface INegotiationConfigRepository : IRepository<NegotiationConfig>
    {
        Task<IPagedList<NegotiationConfigJsonDto>> FindAllJsonDtosPagedAsync(int page, int pageSize, List<Tuple<string, string>> sortColumns, string keywords, Guid? musicGenreUid, Guid? evaluationStatusUid, string languageCode, int editionId);
        Task<int> CountAsync(int editionId, bool showAllEditions = false);
    }    
}
