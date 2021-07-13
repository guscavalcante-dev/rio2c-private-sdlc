// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 07-13-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-13-2021
// ***********************************************************************
// <copyright file="IInnovationOrganizationObjectivesOptionRepository.cs" company="Softo">
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
    /// <summary>IInnovationOrganizationObjectivesOptionRepository</summary>
    public interface IInnovationOrganizationObjectivesOptionRepository : IRepository<InnovationOrganizationObjectivesOption>
    {
        InnovationOrganizationObjectivesOption FindById(int innovationOrganizationObjectivesOptionId);
        InnovationOrganizationObjectivesOption FindByUid(Guid innovationOrganizationObjectivesOptionUid);
        Task<InnovationOrganizationObjectivesOption> FindByIdAsync(int innovationOrganizationObjectivesOptionId);
        Task<InnovationOrganizationObjectivesOption> FindByUidAsync(Guid innovationOrganizationObjectivesOptionUid);
        Task<List<InnovationOrganizationObjectivesOption>> FindAllAsync();
        Task<List<InnovationOrganizationObjectivesOption>> FindAllByIdsAsync(List<int?> innovationOrganizationObjectivesOptionIds);
        Task<List<InnovationOrganizationObjectivesOption>> FindAllByUidsAsync(List<Guid?> innovationOrganizationObjectivesOptionUids);
    }
}