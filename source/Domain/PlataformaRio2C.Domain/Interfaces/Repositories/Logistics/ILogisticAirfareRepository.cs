// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Arthur Souza
// Created          : 03-08-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-10-2020
// ***********************************************************************
// <copyright file="ILogisticAirfareRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Domain.Interfaces
{
    /// <summary>ILogisticAirfareRepository</summary>
    public interface ILogisticAirfareRepository : IRepository<LogisticAirfare>
    {
        Task<List<LogisticAirfareDto>> FindAllDtosPaged(Guid logisticsUid);
        Task<List<LogisticAirfare>> FindAllForGenerateNegotiationsAsync(Guid editionUid);
    }    
}