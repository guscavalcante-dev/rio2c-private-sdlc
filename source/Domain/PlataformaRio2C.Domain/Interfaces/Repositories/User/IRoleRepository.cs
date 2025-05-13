// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 07-22-2021
// ***********************************************************************
// <copyright file="IRoleRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlataformaRio2C.Domain.Interfaces
{
    /// <summary>
    /// IRoleRepository
    /// </summary>
    public interface IRoleRepository : IRepository<Role>
    {
        Task<Role> FindByNameAsync(string roleName);
        Task<List<Role>> FindAllAdminRolesAsync();
        List<Role> FindAllAdminRoles();
    }
}
