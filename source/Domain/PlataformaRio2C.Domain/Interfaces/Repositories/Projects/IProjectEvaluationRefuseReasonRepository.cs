// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 12-10-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-10-2019
// ***********************************************************************
// <copyright file="IProjectEvaluationRefuseReasonRepository.cs" company="Softo">
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
    /// <summary>IProjectEvaluationRefuseReasonRepository</summary>
    public interface IProjectEvaluationRefuseReasonRepository : IRepository<ProjectEvaluationRefuseReason>
    {
        Task<ProjectEvaluationRefuseReason> FindByUidAsync(Guid projectEvaluationRefuseReasonUid);
        Task<List<ProjectEvaluationRefuseReason>> FindAllAsync();
    }
}