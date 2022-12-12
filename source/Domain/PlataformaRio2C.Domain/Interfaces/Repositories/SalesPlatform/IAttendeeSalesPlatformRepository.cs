// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 08-31-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 11-24-2022
// ***********************************************************************
// <copyright file="IAttendeeSalesPlatformRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using PlataformaRio2C.Domain.Entities;
using System.Threading.Tasks;
using PlataformaRio2C.Domain.Dtos;

namespace PlataformaRio2C.Domain.Interfaces
{
    /// <summary>IAttendeeSalesPlatformRepository</summary>
    public interface IAttendeeSalesPlatformRepository : IRepository<AttendeeSalesPlatform>
    {
        Task<List<AttendeeSalesPlatformDto>> FindAllDtoByIsActiveAsync();
        Task<AttendeeSalesPlatformDto> FindDtoByNameAsync(string name);
    }    
}