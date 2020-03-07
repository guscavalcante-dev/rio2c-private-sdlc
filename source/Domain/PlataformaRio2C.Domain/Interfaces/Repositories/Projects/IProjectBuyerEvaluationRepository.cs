// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 12-10-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-07-2020
// ***********************************************************************
// <copyright file="IProjectBuyerEvaluationRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using PlataformaRio2C.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using PlataformaRio2C.Domain.Dtos;

namespace PlataformaRio2C.Domain.Interfaces
{
    /// <summary>IProjectBuyerEvaluationRepository</summary>
    public interface IProjectBuyerEvaluationRepository : IRepository<ProjectBuyerEvaluation>
    {
        Task<List<ProjectBuyerEvaluationEmailDto>> FindAllBuyerEmailDtosAsync(int editionId, DateTimeOffset editionProjectEvaluationStartDate, DateTimeOffset editionProjectEvaluationEndDate);
        Task<List<ProjectBuyerEvaluation>> FindAllByProjectEvaluationStatusUidAsync(int editionId, Guid projectEvaluationStatusUid);
    }
}