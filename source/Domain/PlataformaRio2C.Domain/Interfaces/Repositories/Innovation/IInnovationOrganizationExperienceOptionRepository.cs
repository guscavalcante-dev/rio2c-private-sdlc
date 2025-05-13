// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 07-13-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-13-2021
// ***********************************************************************
// <copyright file="IInnovationOrganizationExperienceOptionRepository.cs" company="Softo">
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
    /// <summary>IInnovationOrganizationExperienceOptionRepository</summary>
    public interface IInnovationOrganizationExperienceOptionRepository : IRepository<InnovationOrganizationExperienceOption>
    {
        InnovationOrganizationExperienceOption FindById(int innovationOrganizationExperienceOptionId);
        InnovationOrganizationExperienceOption FindByUid(Guid innovationOrganizationExperienceOptionUid);
        Task<InnovationOrganizationExperienceOption> FindByIdAsync(int innovationOrganizationExperienceOptionId);
        Task<InnovationOrganizationExperienceOption> FindByUidAsync(Guid innovationOrganizationExperienceOptionUid);
        Task<List<InnovationOrganizationExperienceOption>> FindAllAsync();
        Task<List<InnovationOrganizationExperienceOption>> FindAllByIdsAsync(List<int?> innovationOrganizationExperienceOptionIds);
        Task<List<InnovationOrganizationExperienceOption>> FindAllByUidsAsync(List<Guid?> innovationOrganizationExperienceOptionUids);
    }
}