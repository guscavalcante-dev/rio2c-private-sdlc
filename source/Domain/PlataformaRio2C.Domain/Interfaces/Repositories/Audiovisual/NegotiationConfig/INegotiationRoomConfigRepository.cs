// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-07-2020
// ***********************************************************************
// <copyright file="INegotiationRoomConfigRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Threading.Tasks;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Domain.Interfaces
{
    /// <summary>INegotiationRoomConfigRepository</summary>
    public interface INegotiationRoomConfigRepository : IRepository<NegotiationRoomConfig>
    {
        Task<NegotiationRoomConfigDto> FindMainInformationWidgetDtoAsync(Guid negotiationRoomConfigUid);
    }    
}
