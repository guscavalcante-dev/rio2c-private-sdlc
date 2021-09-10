// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 04-24-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 09-09-2021
// ***********************************************************************
// <copyright file="UpdateUserStatus.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>UpdateUserStatus</summary>
    public class UpdateUserStatus : AdministratorBaseCommand
    {
        public Guid UserUid { get; private set; }
        public bool Active { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateUserStatus"/> class.
        /// </summary>
        /// <param name="userUid">The user uid.</param>
        /// <param name="active">if set to <c>true</c> [active].</param>
        public UpdateUserStatus (Guid userUid, bool active)
        {
            this.UserUid = userUid;
            this.Active = active;
        }
    }
}