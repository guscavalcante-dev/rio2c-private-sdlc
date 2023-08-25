// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 08-16-2023
//
// Last Modified By : Renan Valentim
// Last Modified On : 08-16-2023
// ***********************************************************************
// <copyright file="ISocialMediaPlatformRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Threading.Tasks;
using PlataformaRio2C.Domain.Dtos;

namespace PlataformaRio2C.Domain.Interfaces
{
    /// <summary>ISocialMediaPlatformRepository</summary>
    public interface ISocialMediaPlatformRepository : IRepository<SocialMediaPlatform>
    {
        Task<SocialMediaPlatform> FindByNameAsync(string name);
        Task<SocialMediaPlatformDto> FindDtoByNameAsync(string name);
    }    
}