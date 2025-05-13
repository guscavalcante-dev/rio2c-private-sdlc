// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 05-13-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 05-13-2020
// ***********************************************************************
// <copyright file="Connection.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using System;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>Connection</summary>
    public class Connection : Entity
    {
        public static readonly int UserAgentMaxLength = 500;

        public Guid ConnectionId { get; private set; }
        public int UserId { get; private set; }
        public string UserAgent { get; private set; }

        public virtual User User { get; set; }

        /// <summary>Initializes a new instance of the <see cref="Connection"/> class.</summary>
        /// <param name="connectionId">The connection identifier.</param>
        /// <param name="user">The user.</param>
        /// <param name="userAgent">The user agent.</param>
        public Connection(Guid connectionId, User user, string userAgent)
        {
            this.ConnectionId = connectionId;
            this.UserId = user?.Id ?? 0;
            this.User = user;
            this.UserAgent = userAgent?.GetLimit(UserAgentMaxLength);

            this.CreateDate = this.UpdateDate = DateTime.UtcNow;
        }

        /// <summary>Initializes a new instance of the <see cref="Connection"/> class.</summary>
        protected Connection()
        {
        }

        #region Validations

        /// <summary>Returns true if ... is valid.</summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.</returns>
        public override bool IsValid()
        {
            this.ValidationResult = new ValidationResult();

            this.ValidateUser();

            return this.ValidationResult.IsValid;
        }

        /// <summary>Validates the user.</summary>
        public void ValidateUser()
        {
            if (this.User == null)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.User), new string[] { "User" }));
            }
        }

        #endregion
    }
}