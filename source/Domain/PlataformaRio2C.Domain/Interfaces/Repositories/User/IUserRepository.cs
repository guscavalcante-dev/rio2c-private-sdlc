﻿// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 05-13-2020
// ***********************************************************************
// <copyright file="IUserRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace PlataformaRio2C.Domain.Interfaces
{
    /// <summary>IUserRepository</summary>
    public interface IUserRepository : IRepository<User>
    {
        Task<User> FindByIdAsync(int userId);
        Task<User> FindByUidAsync(Guid userUid);
        Task<User> FindByUserNameAsync(string userName);
        Task<User> FindUserByEmailUidAsync(string userEmail, Guid uid);
        Task<User> FindUserByEmailAsync(string userEmail);
        Task<UserDto> FindUserDtoByUserIdAsync(int userId);
        AdminAccessControlDto FindAdminAccessControlDtoByUserIdAndByEditionId(int userId, int editionId);
        UserAccessControlDto FindUserAccessControlDtoByUserIdAndByEditionId(int userId, int editionId);
        UserLanguageDto FindUserLanguageByUserId(int userId);
        Task<UserEmailSettingsDto> FindUserEmailSettingsDtoByUserIdAsync(int userId);
    }
}