// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 08-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 06-19-2021
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
        //Task<OrganizationDto> FindDtoByUidAsync(Guid holdingUid);
        //Task<List<Organization>> GetAllAsync();
    }
}