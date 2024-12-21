// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Gilson Oliveira
// Created          : 11-19-2024
//
// Last Modified By : Gilson Oliveira
// Last Modified On : 12-02-2024
// ***********************************************************************
// <copyright file="IAttendeeMusicBandEvaluationRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlataformaRio2C.Domain.Interfaces
{
    /// <summary>IAttendeeMusicBandEvaluationRepository</summary>
    public interface IAttendeeMusicBandEvaluationRepository : IRepository<AttendeeMusicBandEvaluation>
    {
        Task<List<AttendeeMusicBandEvaluation>> FindAllByEditionIdAsync(int editionId);
        Task<int> CountByCommissionMemberAsync(int editionId, List<int?> musicBandId, List<int?> collaboratorId);
        Task<int> CountByCuratorAsync(int editionId, List<int?> musicBandId);
        Task<int> CountByRepechageAsync(int editionId, List<int?> musicBandId);
        Task<int> CountByPopularEvaluationAsync(int editionId, List<int?> musicBandId, List<int?> popularEvaluationStatusId);
    }
}