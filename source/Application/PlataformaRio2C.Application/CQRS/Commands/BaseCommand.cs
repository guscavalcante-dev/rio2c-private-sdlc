// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-16-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 10-22-2024
// ***********************************************************************
// <copyright file="BaseCommand.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using MediatR;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>BaseCommand</summary>
    public class BaseCommand : IRequest<AppValidationResult>
    {
        public int UserId { get; private set; }
        public Guid UserUid { get; private set; }
        public int? EditionId { get; private set; }
        public Guid? EditionUid { get; private set; }
        public string UserInterfaceLanguage { get; private set; }
        public bool IsAdmin { get; private set; }

        /// <summary>
        /// Updates the pre send properties.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="userUid">The user uid.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="editionUid">The edition uid.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        /// <param name="isAdmin">The is admin.</param>
        public void UpdatePreSendProperties(
            int userId,
            Guid userUid,
            int? editionId,
            Guid? editionUid,
            string userInterfaceLanguage,
            bool? isAdmin = false)
        {
            this.UserId = userId;
            this.UserUid = userUid;
            this.EditionId = editionId;
            this.EditionUid = editionUid;
            this.UserInterfaceLanguage = userInterfaceLanguage;
            this.IsAdmin = isAdmin.Value;
        }

        /// <summary>
        /// Updates the pre send properties.
        /// </summary>
        /// <param name="baseCmd">The base command.</param>
        public void UpdatePreSendProperties(BaseCommand baseCmd)
        {
            this.UserId = baseCmd.UserId;
            this.UserUid = baseCmd.UserUid;
            this.EditionId = baseCmd.EditionId;
            this.EditionUid = baseCmd.EditionUid;
            this.UserInterfaceLanguage = baseCmd.UserInterfaceLanguage;
            this.IsAdmin = baseCmd.IsAdmin;
        }
    }
}