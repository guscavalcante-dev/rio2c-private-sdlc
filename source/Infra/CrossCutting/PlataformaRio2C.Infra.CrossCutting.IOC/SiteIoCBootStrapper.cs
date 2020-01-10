// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.CrossCutting.IOC
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-27-2019
// ***********************************************************************
// <copyright file="SiteIoCBootStrapper.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using SimpleInjector;

namespace PlataformaRio2C.Infra.CrossCutting.IOC
{
    /// <summary>SiteIoCBootStrapper</summary>
    public static class SiteIoCBootStrapper
    {
        /// <summary>Registers the services.</summary>
        /// <param name="container">The container.</param>
        public static void RegisterServices(Container container)
        {
            //container.Register<IPlayerService, PlayerService>(Lifestyle.Scoped);
            //container.Register<ICountryService, CountryService>(Lifestyle.Scoped);
        }
    }    
}