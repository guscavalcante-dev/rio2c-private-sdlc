// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.CrossCutting.SalesPlatforms
// Author           : Rafael Dantas Ruiz
// Created          : 07-12-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 07-12-2019
// ***********************************************************************
// <copyright file="SalesPlatformServiceFactory.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Infra.CrossCutting.SalesPlatforms.Services;

namespace PlataformaRio2C.Infra.CrossCutting.SalesPlatforms
{
    /// <summary>SalesPlatformServiceFactory</summary>
    public class SalesPlatformServiceFactory : ISalesPlatformServiceFactory
    {
        /// <summary>Initializes a new instance of the <see cref="SalesPlatformServiceFactory"/> class.</summary>
        public SalesPlatformServiceFactory()
        {
        }

        /// <summary>Gets this instance.</summary>
        /// <returns></returns>
        public ISalesPlatformService Get()
        {
            return new EventbriteSalesPlatformService();
        }
    }
}