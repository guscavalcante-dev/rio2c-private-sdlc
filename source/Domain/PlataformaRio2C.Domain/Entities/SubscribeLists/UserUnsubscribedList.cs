// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 12-04-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-05-2019
// ***********************************************************************
// <copyright file="UserUnsubscribedList.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>UserUnsubscribedList</summary>
    public class UserUnsubscribedList : Entity
    {
        public int UserId { get; private set; }
        public int SubscribeListId { get; private set; }

        public virtual User User { get; private set; }
        public virtual SubscribeList SubscribeList { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="UserUnsubscribedList"/> class.</summary>
        /// <param name="user">The user.</param>
        /// <param name="unsubscribedList">The unsubscribed list.</param>
        public UserUnsubscribedList(User user, SubscribeList unsubscribedList)
        {
            this.UserId = user?.Id ?? 0;
            this.User = user;
            this.SubscribeListId = unsubscribedList?.Id ?? 0;
            this.SubscribeList = unsubscribedList;

            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.Now;
            this.CreateUserId = this.UpdateUserId = user?.Id ?? 0;
        }

        /// <summary>Initializes a new instance of the <see cref="UserUnsubscribedList"/> class.</summary>
        protected UserUnsubscribedList()
        {
        }

        /// <summary>Restores the specified user identifier.</summary>
        /// <param name="userId">The user identifier.</param>
        public void Restore(int userId)
        {
            if (!this.IsDeleted)
            {
                return;
            }

            this.IsDeleted = false;
            this.UpdateDate = DateTime.Now;
            this.UpdateUserId = userId;
        }

        /// <summary>Deletes the specified user identifier.</summary>
        /// <param name="userId">The user identifier.</param>
        public void Delete(int userId)
        {
            if (this.IsDeleted)
            {
                return;
            }

            this.IsDeleted = true;
            this.UpdateDate = DateTime.Now;
            this.UpdateUserId = userId;
        }

        #region Validations

        /// <summary>Returns true if ... is valid.</summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.</returns>
        public override bool IsValid()
        {
            this.ValidationResult = new ValidationResult();

            this.ValidateUser();
            this.ValidateSubscribeList();

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

        /// <summary>Validates the subscribe list.</summary>
        public void ValidateSubscribeList()
        {
            if (this.SubscribeList == null)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.SubscribeList), new string[] { "SubscribeList" }));
            }
        }

        #endregion
    }
}