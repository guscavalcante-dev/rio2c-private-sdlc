// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Arthur Souza
// Created          : 01-20-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-12-2020
// ***********************************************************************
// <copyright file="ILogisticSponsorRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PlataformaRio2C.Domain.Dtos;
using X.PagedList;

namespace PlataformaRio2C.Domain.Interfaces
{
    /// <summary>ILogisticSponsorRepository</summary>
    public interface ILogisticSponsorRepository : IRepository<LogisticSponsor>
    {
        Task<IPagedList<AttendeeLogisticSponsorBaseDto>> FindAllByDataTable(int page, int pageSize, string keywords, List<Tuple<string, string>> sortColumns, bool showAllEditions, int? editionId);
        Task<AttendeeLogisticSponsorBaseDto> FindLogisticSponsorDtoByUid(Guid sponsorUid);
        Task<List<AttendeeLogisticSponsorBaseDto>> FindAllDtosByEditionUidAsync(int editionDtoId);
        Task<List<AttendeeLogisticSponsorBaseDto>> FindAllDtosByIsOther(int editionDtoId);
        Task<Guid> GetByIsOthersRequired();
    }    
}