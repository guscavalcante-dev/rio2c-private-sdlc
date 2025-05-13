// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 07-01-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-01-2021
// ***********************************************************************
// <copyright file="IWorkDedicationRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlataformaRio2C.Domain.Interfaces
{
    /// <summary>IWorkDedicationRepository</summary>
    public interface IWorkDedicationRepository : IRepository<WorkDedication>
    {
        WorkDedication FindById(int workDedicationId);
        WorkDedication FindByUid(Guid workDedicationUid);
        Task<WorkDedication> FindByIdAsync(int workDedicationId);
        Task<WorkDedication> FindByUidAsync(Guid workDedicationUid);
        Task<List<WorkDedication>> FindAllAsync();
        Task<List<WorkDedication>> FindAllByIdsAsync(List<int?> workDedicationIds);
        Task<List<WorkDedication>> FindAllByUidsAsync(List<Guid?> workDedicationUids);
    }
}