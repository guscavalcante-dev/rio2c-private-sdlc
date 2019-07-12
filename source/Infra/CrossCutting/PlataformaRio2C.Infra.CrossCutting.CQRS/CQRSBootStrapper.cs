// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.CrossCutting.CQRS
// Author           : Rafael Dantas Ruiz
// Created          : 07-12-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 07-12-2019
// ***********************************************************************
// <copyright file="CqrsBootStrapper.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Reflection;
using MediatR.SimpleInjector;
using SimpleInjector;

namespace PlataformaRio2C.Infra.CrossCutting.CQRS
{
    /// <summary>MadiatRBootStrapper</summary>
    public static class CqrsBootStrapper
    {
        /// <summary>Registers the services.</summary>
        /// <param name="container">The container.</param>
        /// <param name="assemblies">The assemblies.</param>
        public static void RegisterServices(Container container, Assembly[] assemblies)
        {
            container.BuildMediator(assemblies);
        }
    }
}