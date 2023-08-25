// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 08-16-2023
//
// Last Modified By : Renan Valentim
// Last Modified On : 08-16-2023
// ***********************************************************************
// <copyright file="IWeConnectPublicationRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Threading.Tasks;
using PlataformaRio2C.Domain.Dtos;
using System.Collections.Generic;
using System;
using X.PagedList;

namespace PlataformaRio2C.Domain.Interfaces
{
    /// <summary>IWeConnectPublicationRepository</summary>
    public interface IWeConnectPublicationRepository : IRepository<WeConnectPublication>
    {
        Task<WeConnectPublication> FindBySocialMediaPlatformPublicationIdAsync(string socialMediaPlatformPublicationId);
        Task<IPagedList<WeConnectPublicationDto>> FindAllDtosPagedAsync(int page, int pageSize);
    }    
}