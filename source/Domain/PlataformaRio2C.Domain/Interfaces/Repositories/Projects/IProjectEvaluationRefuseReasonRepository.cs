// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 12-10-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 02-28-2020
// ***********************************************************************
// <copyright file="IProjectEvaluationRefuseReasonRepository.cs" company="Softo">
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
    /// <summary>IProjectEvaluationRefuseReasonRepository</summary>
    public interface IProjectEvaluationRefuseReasonRepository : IRepository<ProjectEvaluationRefuseReason>
    {
        Task<ProjectEvaluationRefuseReason> FindByUidAsync(Guid projectEvaluationRefuseReasonUid);
        Task<List<ProjectEvaluationRefuseReason>> FindAllByProjectTypeUidAsync(Guid projectTypeUid);
    }
}