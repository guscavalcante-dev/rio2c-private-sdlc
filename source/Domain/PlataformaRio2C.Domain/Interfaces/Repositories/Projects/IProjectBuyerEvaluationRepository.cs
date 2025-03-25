// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 12-10-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-08-2020
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
        Task<ProjectBuyerEvaluationDto> FindDtoAsync(Guid projectBuyerEvaluationUid);
        Task<List<ProjectBuyerEvaluationEmailDto>> FindAllBuyerEmailDtosAsync(int editionId, DateTimeOffset editionProjectEvaluationStartDate, DateTimeOffset editionProjectEvaluationEndDate);
        Task<List<ProjectBuyerEvaluation>> FindAllForGenerateNegotiationsAsync(int editionId);
        Task<List<ProjectBuyerEvaluationDto>> FindUnscheduledWidgetDtoAsync(int editionId);
        Task<int> CountNegotiationScheduledAsync(int editionId, bool showAllEditions = false);
        Task<int> CountNegotiationNotScheduledAsync(int editionId, bool showAllEditions = false);
        Task<int> CountAcceptedProjectBuyerEvaluationsByBuyerAttendeeOrganizationUidAsync(Guid buyerAttendeeOrganizationUid);
        Task<int> CountAcceptedProjectBuyerEvaluationsByEditionIdAsync(int editionId);
    }
}