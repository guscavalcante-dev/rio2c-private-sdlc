// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-09-2019
// ***********************************************************************
// <copyright file="ITargetAudienceRepository.cs" company="Softo">
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
    /// <summary>ITargetAudienceRepository</summary>
    public interface ITargetAudienceRepository : IRepository<TargetAudience>
    {
        //Task<List<TargetAudience>> FindAllAsync();
        Task<List<TargetAudience>> FindAllByUidsAsync(List<Guid> targetAudiencesUids);
        Task<List<TargetAudience>> FindAllByProjectTypeIdAsync(int projectTypeId);
        Task<List<TargetAudienceDto>> FindAllDtosByProjectTypeIdAsync(int projectTypeId);
    }
}