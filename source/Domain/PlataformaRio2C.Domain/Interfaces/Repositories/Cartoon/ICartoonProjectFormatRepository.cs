// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 01-25-2022
//
// Last Modified By : Renan Valentim
// Last Modified On : 01-25-2022
// ***********************************************************************
// <copyright file="ICartoonProjectFormatRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlataformaRio2C.Domain.Interfaces
{
    /// <summary>ICartoonProjectFormatRepository</summary>
    public interface ICartoonProjectFormatRepository : IRepository<CartoonProjectFormat>
    {
        CartoonProjectFormat FindById(int id);
        CartoonProjectFormat FindByUid(Guid uid);
        Task<CartoonProjectFormat> FindByIdAsync(int id);
        Task<CartoonProjectFormat> FindByUidAsync(Guid uid);
        Task<List<CartoonProjectFormat>> FindAllAsync();
        Task<List<CartoonProjectFormat>> FindAllByIdsAsync(List<int?> ids);
        Task<List<CartoonProjectFormat>> FindAllByUidsAsync(List<Guid?> uids);
    }
}