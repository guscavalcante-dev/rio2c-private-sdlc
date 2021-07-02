// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 06-29-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 06-29-2021
// ***********************************************************************
// <copyright file="IInnovationOrganizationOptionRepository.cs" company="Softo">
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
    /// <summary>IInnovationOrganizationOptionRepository</summary>
    public interface IInnovationOrganizationOptionRepository : IRepository<InnovationOrganizationOption>
    {
        Task<InnovationOrganizationOption> FindByIdAsync(int innovationOrganizationOptionId);
        Task<InnovationOrganizationOption> FindByUidAsync(Guid innovationOrganizationOptionUid);
        Task<List<InnovationOrganizationOption>> FindAllByIdsAsync(List<int?> innovationOrganizationOptionIds);
        Task<List<InnovationOrganizationOption>> FindAllByUidsAsync(List<Guid?> innovationOrganizationOptionUids);
    }
}