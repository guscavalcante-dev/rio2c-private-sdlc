// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 07-13-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-13-2021
// ***********************************************************************
// <copyright file="IInnovationOrganizationTechnologyOptionRepository.cs" company="Softo">
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
    /// <summary>IInnovationOrganizationTechnologyOptionRepository</summary>
    public interface IInnovationOrganizationTechnologyOptionRepository : IRepository<InnovationOrganizationTechnologyOption>
    {
        InnovationOrganizationTechnologyOption FindById(int innovationOrganizationTechnologyOptionId);
        InnovationOrganizationTechnologyOption FindByUid(Guid innovationOrganizationTechnologyOptionUid);
        Task<InnovationOrganizationTechnologyOption> FindByIdAsync(int innovationOrganizationTechnologyOptionId);
        Task<InnovationOrganizationTechnologyOption> FindByUidAsync(Guid innovationOrganizationTechnologyOptionUid);
        Task<List<InnovationOrganizationTechnologyOption>> FindAllAsync();
        Task<List<InnovationOrganizationTechnologyOption>> FindAllByIdsAsync(List<int?> innovationOrganizationTechnologyOptionIds);
        Task<List<InnovationOrganizationTechnologyOption>> FindAllByUidsAsync(List<Guid?> innovationOrganizationTechnologyOptionUids);
    }
}