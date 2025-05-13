// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 07-11-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 11-30-2022
// ***********************************************************************
// <copyright file="ISalesPlatformWebhookRequestRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlataformaRio2C.Domain.Interfaces
{
    /// <summary>ISalesPlatformWebhookRequestRepository</summary>
    public interface ISalesPlatformWebhookRequestRepository : IRepository<SalesPlatformWebhookRequest>
    {
        Task<List<SalesPlatformWebhookRequest>> FindAllByPendingAsync();
        Task<List<SalesPlatformWebhookRequestDto>> FindAllDtoByPendingAsync();
        Task<List<SalesPlatformWebhookRequestDto>> FindAllDtoBySalesPlatformIdAsync(int salesPlatformId);
    }
}
