// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 06-29-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 06-29-2021
// ***********************************************************************
// <copyright file="IInnovationOptionGroupRepository.cs" company="Softo">
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
    /// <summary>IInnovationOptionGroupRepository</summary>
    public interface IInnovationOptionGroupRepository : IRepository<InnovationOptionGroup>
    {
        Task<InnovationOptionGroup> FindByIdAsync(int innovationOptionGroupId);
        Task<InnovationOptionGroup> FindByUidAsync(Guid innovationOptionGroupUid);
        Task<List<InnovationOptionGroup>> FindAllAsync();
        Task<List<InnovationOptionGroup>> FindAllByIdsAsync(List<int?> innovationOptionGroupIds);
        Task<List<InnovationOptionGroup>> FindAllByUidsAsync(List<Guid?> innovationOptionGroupUids);
    }
}