// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Gilson Oliveira
// Created          : 10-23-2024
//
// Last Modified By : Gilson Oliveira
// Last Modified On : 10-23-2024
// ***********************************************************************
// <copyright file="IProjectModalityRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlataformaRio2C.Domain.Interfaces
{
    /// <summary>IProjectModalityRepository</summary>
    public interface IProjectModalityRepository : IRepository<ProjectModality>
    {
        Task<List<ProjectModalityDto>> FindAllAsync();
    }
}