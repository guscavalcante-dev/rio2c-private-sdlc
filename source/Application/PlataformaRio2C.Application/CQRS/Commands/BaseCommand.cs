// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-16-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-23-2019
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

        /// <summary>Updates the pre send properties.</summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="userUid">The user uid.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="editionUid">The edition uid.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        public void UpdatePreSendProperties(
            int userId,
            Guid userUid,
            int? editionId,
            Guid? editionUid,
            string userInterfaceLanguage)
        {
            this.UserId = userId;
            this.UserUid = userUid;
            this.EditionId = editionId;
            this.EditionUid = editionUid;
            this.UserInterfaceLanguage = userInterfaceLanguage;
        }
    }
}