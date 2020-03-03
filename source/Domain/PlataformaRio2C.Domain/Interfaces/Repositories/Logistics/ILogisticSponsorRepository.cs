// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Arthur Souza
// Created          : 01-20-2020
//
// Last Modified By : Arthur Souza
// Last Modified On : 01-20-2020
// ***********************************************************************
// <copyright file="ILogisticSponsorRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using PlataformaRio2C.Domain.Dtos;
using X.PagedList;

namespace PlataformaRio2C.Domain.Interfaces
{
    /// <summary>ILogisticSponsorRepository</summary>
    public interface ILogisticSponsorRepository : IRepository<LogisticSponsor>
    {
        Task<IPagedList<LogisticSponsorBaseDto>> FindAllByDataTable(int page, int pageSize, string keywords, List<Tuple<string, string>> sortColumns, bool showAllEditions, int? editionId);
        Task<LogisticSponsorBaseDto> FindLogisticSponsorDtoByUid(Guid sponsorUid);
        Task<List<LogisticSponsorBaseDto>> FindAllDtosByEditionUidAsync(int editionDtoId);
    }    
}