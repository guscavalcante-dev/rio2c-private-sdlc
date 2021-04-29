// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 09-26-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-26-2019
// ***********************************************************************
// <copyright file="ICollaboratorTypeRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlataformaRio2C.Domain.Interfaces
{
    /// <summary>ICollaboratorTypeRepository</summary>
    public interface ICollaboratorTypeRepository : IRepository<CollaboratorType>
    {
        Task<CollaboratorType> FindByNameAsync(string collaboratorTypeName);

        Task<List<CollaboratorType>> FindByNamesAsync(string[] collaboratorTypeNames);

        Task<List<CollaboratorType>> FindAllAdminCollaboratorTypesAsync();
    }    
}