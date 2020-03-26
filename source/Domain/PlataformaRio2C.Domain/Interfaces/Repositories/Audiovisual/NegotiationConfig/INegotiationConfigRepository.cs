// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-25-2020
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
        Task<NegotiationConfigDto> FindMainInformationWidgetDtoAsync(Guid negotiationConfigUid);
        Task<NegotiationConfigDto> FindRoomsWidgetDtoAsync(Guid negotiationConfigUid);
        Task<IPagedList<NegotiationConfigJsonDto>> FindAllJsonDtosPagedAsync(int page, int pageSize, List<Tuple<string, string>> sortColumns, string keywords, Guid? musicGenreUid, Guid? evaluationStatusUid, string languageCode, int editionId);
        Task<int> CountAsync(int editionId, bool showAllEditions = false);
        Task<List<NegotiationConfig>> FindAllForGenerateNegotiationsAsync();
        Task<List<NegotiationConfigDto>> FindAllDatesDtosAsync(int editionId, string customFilter);
        Task<List<NegotiationConfigDto>> FindAllRoomsDtosAsync(int editionId, Guid negotiationConfigUid, string customFilter);
        Task<NegotiationConfigDto> FindAllTimesDtosAsync(int editionId, Guid negotiationRoomConfigUid, string customFilter);
    }
}
