// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-09-2019
// ***********************************************************************
// <copyright file="IActivityRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Domain.Interfaces
{
    /// <summary>IActivityRepository</summary>
    public interface IActivityRepository : IRepository<Activity>
    {
        Task<List<Activity>> FindAllByProjectTypeIdAsync(int projectTypeId);
        Task<List<Activity>> FindAllByUidsAsync(List<Guid> activitiesUids);
        Task<List<Activity>> FindAllByProjectTypeUidAsync(Guid projectTypeUid);
    }    
}