// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 02-28-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 02-28-2020
// ***********************************************************************
// <copyright file="IMusicGenreRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlataformaRio2C.Domain.Interfaces
{
    /// <summary>IMusicGenreRepository</summary>
    public interface IMusicGenreRepository : IRepository<MusicGenre>
    {
        Task<List<MusicGenre>> FindAllAsync();
    }
}