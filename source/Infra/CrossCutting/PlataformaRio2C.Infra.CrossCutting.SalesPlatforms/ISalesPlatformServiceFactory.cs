// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.CrossCutting.SalesPlatforms
// Author           : Rafael Dantas Ruiz
// Created          : 07-12-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-31-2019
// ***********************************************************************
// <copyright file="ISalesPlatformServiceFactory.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Infra.CrossCutting.SalesPlatforms.Services;

namespace PlataformaRio2C.Infra.CrossCutting.SalesPlatforms
{
    /// <summary>ISalesPlatformServiceFactory</summary>
    public interface ISalesPlatformServiceFactory
    {
        ISalesPlatformService Get(SalesPlatformWebhookRequestDto salesPlatformWebhookRequestDto);
    }
}