﻿// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 02-27-2023
// ***********************************************************************
// <copyright file="ICountryRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlataformaRio2C.Domain.Interfaces
{
    /// <summary>ICountryRepository</summary>
    public interface ICountryRepository : IRepository<Country>
    {
        Task<List<CountryBaseDto>> FindAllBaseDtosAsync();
        Task<Country> FindByNameAsync(string name);
    }
}