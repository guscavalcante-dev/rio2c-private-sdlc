// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 07-11-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 07-22-2019
// ***********************************************************************
// <copyright file="ISalesPlatformWebhookRequestRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using System.Threading.Tasks;
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Domain.Interfaces
{
    /// <summary>ISalesPlatformWebhookRequestRepository</summary>
    public interface ISalesPlatformWebhookRequestRepository : IRepository<SalesPlatformWebhookRequest>
    {
        Task<List<SalesPlatformWebhookRequest>> GetAllByPendingAsync();
    }    
}
