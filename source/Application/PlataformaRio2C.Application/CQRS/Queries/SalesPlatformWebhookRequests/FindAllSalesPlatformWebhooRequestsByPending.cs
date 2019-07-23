// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 07-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 07-19-2019
// ***********************************************************************
// <copyright file="FindAllSalesPlatformWebhooRequestsByPending.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using MediatR;
using PlataformaRio2C.Application.Dtos;

namespace PlataformaRio2C.Application.CQRS.Queries
{
    /// <summary>FindAllSalesPlatformWebhooRequestsByPending</summary>
    public class FindAllSalesPlatformWebhooRequestsByPending : IRequest<List<SalesPlatformWebhookRequestDto>>
    {
        /// <summary>Initializes a new instance of the <see cref="FindAllSalesPlatformWebhooRequestsByPending"/> class.</summary>
        public FindAllSalesPlatformWebhooRequestsByPending()
        {
        }
    }
}