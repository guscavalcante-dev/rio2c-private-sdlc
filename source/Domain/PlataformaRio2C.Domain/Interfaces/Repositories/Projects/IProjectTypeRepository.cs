// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 11-07-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 11-07-2019
// ***********************************************************************
// <copyright file="IProjectTypeRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Domain.Interfaces
{
    /// <summary>IProjectTypeRepository</summary>
    public interface IProjectTypeRepository : IRepository<ProjectType>
    {
        //Task<OrganizationDto> FindDtoByUidAsync(Guid holdingUid);
        //Task<List<Organization>> GetAllAsync();
    }
}