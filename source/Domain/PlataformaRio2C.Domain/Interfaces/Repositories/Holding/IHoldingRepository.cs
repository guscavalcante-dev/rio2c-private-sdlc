// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-26-2019
// ***********************************************************************
// <copyright file="IHoldingRepository.cs" company="Softo">
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
    /// <summary>IHoldingRepository</summary>
    public interface IHoldingRepository : IRepository<Holding>
    {
        Task<HoldingDto> FindDtoByUidAsync(Guid holdingUid);
        Task<List<HoldingBaseDto>> FindAllBaseDto(string keywords);
        Task<IPagedList<HoldingBaseDto>> FindAllBaseDtoByPage(int page, int pageSize, string keywords, List<Tuple<string, string>> sortColumns, bool showAllEditions, int? editionId);
        Task<int> CountAllByDataTable(bool showAllEditions, int? editionId);

        #region Old

        Task<List<Holding>> GetAllAsync();

        #endregion
    }
}