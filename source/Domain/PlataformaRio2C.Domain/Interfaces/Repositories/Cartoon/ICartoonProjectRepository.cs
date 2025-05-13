// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 01-25-2022
//
// Last Modified By : Rafael Franco
// Last Modified On : 02-08-2022
// ***********************************************************************
// <copyright file="ICartoonProjectRepository.cs" company="Softo">
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
    /// <summary>ICartoonProjectRepository</summary>
    public interface ICartoonProjectRepository : IRepository<CartoonProject>
    {
        CartoonProject FindById(int id);
        CartoonProject FindByUid(Guid uid);
        Task<CartoonProject> FindByIdAsync(int id);
        Task<CartoonProject> FindByUidAsync(Guid uid);
        Task<List<CartoonProject>> FindAllAsync();
        Task<List<CartoonProject>> FindAllByIdsAsync(List<int?> ids);
        Task<List<CartoonProject>> FindAllByUidsAsync(List<Guid?> uids);
        Task<CartoonProject> FindByTitleAsync(string title);
    }
}