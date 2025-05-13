// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 06-29-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 06-29-2021
// ***********************************************************************
// <copyright file="IInnovationOrganizationRepository.cs" company="Softo">
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
    /// <summary>IInnovationOrganizationRepository</summary>
    public interface IInnovationOrganizationRepository : IRepository<InnovationOrganization>
    {
        Task<InnovationOrganization> FindByIdAsync(int innovationOrganizationId);
        Task<InnovationOrganization> FindByUidAsync(Guid innovationOrganizationUid);
        Task<List<InnovationOrganization>> FindAllByIdsAsync(List<int?> innovationOrganizationIds);
        Task<List<InnovationOrganization>> FindAllByUidsAsync(List<Guid?> innovationOrganizationUids);
        Task<InnovationOrganization> FindByDocumentAsync(string document);
    }
}