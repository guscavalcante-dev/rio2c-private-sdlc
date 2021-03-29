// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 03-23-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 03-23-2021
// ***********************************************************************
// <copyright file="IMusicBandRepository.cs" company="Softo">
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
    /// <summary>IMusicBandRepository</summary>
    public interface IMusicBandRepository : IRepository<MusicBand>
    {
        Task<MusicBand> FindByIdAsync(int musicBandId);
        Task<MusicBand> FindByUidAsync(Guid musicBandUid);
    }
}