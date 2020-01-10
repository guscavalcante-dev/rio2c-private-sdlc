// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.CrossCutting.IOC
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-27-2019
// ***********************************************************************
// <copyright file="AdminIoCBootStrapper.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Services;
using SimpleInjector;

namespace PlataformaRio2C.Infra.CrossCutting.IOC
{
    /// <summary>AdminIoCBootStrapper</summary>
    public static class AdminIoCBootStrapper
    {
        /// <summary>Registers the services.</summary>
        /// <param name="container">The container.</param>
        public static void RegisterServices(Container container)
        {
            //container.Register<IPlayerService, PlayerAdminService>(Lifestyle.Scoped);
            //container.Register<Application.Interfaces.Services.INegotiationConfigService, Application.Services.NegotiationConfigService>(Lifestyle.Scoped);
            container.Register<Domain.Interfaces.INegotiationConfigService, Domain.Services.NegotiationConfigService>(Lifestyle.Scoped);

            //container.Register<IMailAppService, MailAppService>(Lifestyle.Scoped);
            //container.Register<IMailService, MailService>(Lifestyle.Scoped);
        }       
    }    
}