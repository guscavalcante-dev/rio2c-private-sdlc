// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 04-24-2021
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 07-09-2021
// ***********************************************************************
// <copyright file="UpdateAdministrator.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>UpdateAdministratorStatus</summary>
    public class UpdateAdministratorStatus : AdministratorBaseCommand
    {
        public Guid UserUid { get; private set; }
        public bool Active { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateAdministratorStatus"/> class.
        /// </summary>
        /// <param name="userUid">The user uid.</param>
        /// <param name="active">if set to <c>true</c> [active].</param>
        public UpdateAdministratorStatus (Guid userUid, bool active)
        {
            this.UserUid = userUid;
            this.Active = active;
        }
    }
}