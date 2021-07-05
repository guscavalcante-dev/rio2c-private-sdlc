// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 06-29-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 06-29-2021
// ***********************************************************************
// <copyright file="IInnovationOptionRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PlataformaRio2C.Domain.Dtos;
using X.PagedList;

namespace PlataformaRio2C.Domain.Interfaces
{
    /// <summary>IInnovationOptionRepository</summary>
    public interface IInnovationOptionRepository : IRepository<InnovationOption>
    {
        InnovationOption FindById(int innovationOptionId);
        InnovationOption FindByUid(Guid innovationOptionUid);
        Task<InnovationOption> FindByIdAsync(int innovationOptionId);
        Task<InnovationOption> FindByUidAsync(Guid innovationOptionUid);
        Task<List<InnovationOption>> FindAllAsync();
        Task<List<InnovationOption>> FindAllByIdsAsync(List<int?> innovationOptionIds);
        Task<List<InnovationOption>> FindAllByUidsAsync(List<Guid?> innovationOptionUids);
        Task<List<InnovationOption>> FindAllByGroupUidAsync(int innovationOptionGroupId);
    }
}