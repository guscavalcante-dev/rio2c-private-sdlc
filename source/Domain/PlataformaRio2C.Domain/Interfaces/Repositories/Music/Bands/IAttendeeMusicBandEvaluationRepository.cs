// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Gilson Oliveira
// Created          : 11-19-2024
//
// Last Modified By : Renan Valentim
// Last Modified On : 21-03-2025
// ***********************************************************************
// <copyright file="IAttendeeMusicBandEvaluationRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Threading.Tasks;

namespace PlataformaRio2C.Domain.Interfaces
{
    /// <summary>IAttendeeMusicBandEvaluationRepository</summary>
    public interface IAttendeeMusicBandEvaluationRepository : IRepository<AttendeeMusicBandEvaluation>
    {
        Task<int> CountAcceptedByCollaboratorTypeIdAsync(int editionId, int userId, int collaboratorTypeId);
        Task<int> CountByPopularEvaluationAsync(int editionId, int? musicBandId, int? popularEvaluationStatusId);
    }
}