// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Gilson Oliveira
// Created          : 11-19-2024
//
// Last Modified By : Gilson Oliveira
// Last Modified On : 11-19-2024
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
        Task<int> CountByCollaboratorIdAsync(int editionId, int collaboratorId, int? musicBandId);
    }
}