// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Franco
// Created          : 12-01-2022
//
// Last Modified By : Rafael Franco
// Last Modified On : 12-01-2022
// ***********************************************************************
// <copyright file="IInnovationOrganizationSustainableDevelopmentObjectivesOptionRepository.cs" company="Softo">
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
    /// <summary>InnovationOrganizationSustainableDevelopmentObjectivesOptionRepository</summary>
    public interface IInnovationOrganizationSustainableDevelopmentObjectivesOptionRepository : IRepository<InnovationOrganizationSustainableDevelopmentObjectivesOption>
    {
        InnovationOrganizationSustainableDevelopmentObjectivesOption FindById(int id);
        InnovationOrganizationSustainableDevelopmentObjectivesOption FindByUid(Guid uid);
        Task<InnovationOrganizationSustainableDevelopmentObjectivesOption> FindByIdAsync(int id);
        Task<InnovationOrganizationSustainableDevelopmentObjectivesOption> FindByUidAsync(Guid uid);
        Task<List<InnovationOrganizationSustainableDevelopmentObjectivesOption>> FindAllAsync();
        Task<List<InnovationOrganizationSustainableDevelopmentObjectivesOption>> FindAllByIdsAsync(List<int?> ids);
        Task<List<InnovationOrganizationSustainableDevelopmentObjectivesOption>> FindAllByUidsAsync(List<Guid?> uids);
    }
}