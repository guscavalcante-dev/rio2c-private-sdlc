// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.CrossCutting.SalesPlatforms
// Author           : Rafael Dantas Ruiz
// Created          : 07-12-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 11-24-2022
// ***********************************************************************
// <copyright file="ISalesPlatformService.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using PlataformaRio2C.Infra.CrossCutting.SalesPlatforms.Dtos;

namespace PlataformaRio2C.Infra.CrossCutting.SalesPlatforms.Services
{
    /// <summary>ISalesPlatformService</summary>
    public interface ISalesPlatformService
    {
        Tuple<string, List<SalesPlatformAttendeeDto>> ExecuteRequest();
        List<SalesPlatformAttendeeDto> GetAttendeesByEventId(string eventId);
    }
}