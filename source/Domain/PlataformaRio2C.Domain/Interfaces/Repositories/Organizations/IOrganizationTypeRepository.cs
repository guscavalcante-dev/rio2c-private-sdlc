// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 08-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-19-2019
// ***********************************************************************
// <copyright file="IOrganizationTypeRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Domain.Interfaces
{
    /// <summary>IOrganizationTypeRepository</summary>
    public interface IOrganizationTypeRepository : IRepository<OrganizationType>
    {
        //Task<OrganizationDto> FindDtoByUidAsync(Guid holdingUid);
        //Task<List<Organization>> GetAllAsync();
    }
}