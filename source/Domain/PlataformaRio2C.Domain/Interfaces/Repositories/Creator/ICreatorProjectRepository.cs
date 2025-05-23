﻿// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 02-27-2024
//
// Last Modified By : Renan Valentim
// Last Modified On : 02-27-2024
// ***********************************************************************
// <copyright file="ICreatorProjectRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlataformaRio2C.Domain.Interfaces
{
    /// <summary>ICreatorProjectRepository</summary>
    public interface ICreatorProjectRepository : IRepository<CreatorProject>
    {
        CreatorProjectDto FindDtoById(int id);
        CreatorProjectDto FindDtoByUid(Guid uid);
        Task<CreatorProjectDto> FindDtoByIdAsync(int id);
        Task<CreatorProjectDto> FindDtoByUidAsync(Guid uid);
        Task<List<CreatorProjectDto>> FindAllAsync();
        Task<List<CreatorProjectDto>> FindAllByIdsAsync(List<int?> ids);
        Task<List<CreatorProjectDto>> FindAllByUidsAsync(List<Guid?> uids);
        Task<CreatorProjectDto> FindDtoByTitleAsync(string title);
    }
}