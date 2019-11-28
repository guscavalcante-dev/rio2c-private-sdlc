// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 11-28-2019
// ***********************************************************************
// <copyright file="IUserRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Threading.Tasks;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Domain.Interfaces
{
    /// <summary>IUserRepository</summary>
    public interface IUserRepository : IRepository<User>
    {
        Task<User> FindByIdAsync(int userId);
        Task<UserDto> FindUserDtoByUserIdAsync(int userId);
        AdminAccessControlDto FindAdminAccessControlDtoByUserIdAndByEditionId(int userId, int editionId);
        UserAccessControlDto FindUserAccessControlDtoByUserIdAndByEditionId(int userId, int editionId);
        UserLanguageDto FindUserLanguageByUserId(int userId);
    }    
}