// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 07-12-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 07-19-2019
// ***********************************************************************
// <copyright file="ISalesPlatformRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Threading.Tasks;

namespace PlataformaRio2C.Domain.Interfaces
{
    /// <summary>ISalesPlatformRepository</summary>
    public interface ISalesPlatformRepository : IRepository<SalesPlatform>
    {
        Task<SalesPlatform> GetByNameAsync(string name);
    }    
}
