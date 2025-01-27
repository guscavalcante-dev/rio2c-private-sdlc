// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 03-23-2021
//
// Last Modified By : Gilson Oliveira
// Last Modified On : 11-19-2024
// ***********************************************************************
// <copyright file="IAttendeeMusicBandRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PlataformaRio2C.Domain.Dtos;
using X.PagedList;

namespace PlataformaRio2C.Domain.Interfaces
{
    /// <summary>IAttendeeMusicBandRepository</summary>
    public interface IAttendeeMusicBandRepository : IRepository<AttendeeMusicBand>
    {
        Task<List<AttendeeMusicBand>> FindAllByEditionIdAsync(int editionId);
        Task<int> CountByResponsibleAsync(int editionId, string document);
        Task<int> CountByEditionIdAsync(int editionId);
        Task<int> CountByEvaluatorUserIdAsync(int editionId, int evaluatorUserId);
        Task<int> CountByEvaluatorUsersAsync(int editionId);
        Task<AttendeeMusicBand> FindByMusicBandIdAsync(int editionId, int musicBandId);
    }
}