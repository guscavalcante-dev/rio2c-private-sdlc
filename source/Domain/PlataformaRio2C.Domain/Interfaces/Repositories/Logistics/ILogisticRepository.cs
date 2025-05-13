// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Arthur Souza
// Created          : 03-08-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-27-2020
// ***********************************************************************
// <copyright file="ILogisticRepository.cs" company="Softo">
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
    /// <summary>ILogisticRepository</summary>
    public interface ILogisticRepository : IRepository<Logistic>
    {
        Task<IPagedList<LogisticJsonDto>> FindAllByDataTableAsync(int page, int pageSize, string keywords, List<Tuple<string, string>> sortColumns, bool showAllParticipants, bool showAllSponsored);
        Task<LogisticDto> FindDtoAsync(Guid logisticUid, Language language);
        Task<LogisticDto> FindMainInformationWidgetDtoAsync(Guid logisticUid);
    }
}