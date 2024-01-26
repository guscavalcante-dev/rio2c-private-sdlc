// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 03-25-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 01-26-2024
// ***********************************************************************
// <copyright file="IMusicBandTypeRepository.cs" company="Softo">
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
    /// <summary>IMusicBandTypeRepository</summary>
    public interface IMusicBandTypeRepository : IRepository<MusicBandType>
    {
        Task<MusicBandType> FindByIdAsync(int musicBandTypeUid);

        Task<MusicBandType> FindByUidAsync(Guid musicBandTypeUid);

        Task<List<MusicBandType>> FindAllAsync();
    }
}