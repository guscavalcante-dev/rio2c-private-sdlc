// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 08-19-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 09-16-2021
// ***********************************************************************
// <copyright file="IOrganizationTypeRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Threading.Tasks;
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Domain.Interfaces
{
    /// <summary>IOrganizationTypeRepository</summary>
    public interface IOrganizationTypeRepository : IRepository<OrganizationType>
    {
        Task<OrganizationType> FindByUidAsync(Guid organizationTypeUid);
        Task<OrganizationType> FindByNameAsync(string organizationTypeName);
    }
}