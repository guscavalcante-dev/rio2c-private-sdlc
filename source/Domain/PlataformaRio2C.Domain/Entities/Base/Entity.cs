// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 02-15-2020
// ***********************************************************************
// <copyright file="Entity.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Validation;
using System;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>Entity</summary>
    public abstract class Entity : IEntity
    {
        public int Id { get; protected set; }
        public Guid Uid { get; set; }
        public bool IsDeleted { get; protected set; }
        public DateTimeOffset CreateDate { get; protected set; }
        public int CreateUserId { get; protected set; }
        public DateTimeOffset UpdateDate { get; protected set; }
        public int UpdateUserId { get; protected set; }

        public virtual ValidationResult ValidationResult { get; set; }

        public abstract bool IsValid();

        /// <summary>Sets the uid.</summary>
        /// <param name="uid">The uid.</param>
        public void SetUid(Guid uid)
        {
            this.Uid = uid;
        }

        /// <summary>
        /// Creates the specified user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        public void SetCreateDate(int userId)
        {
            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.UtcNow;
            this.CreateUserId = this.UpdateUserId = userId;
        }

        /// <summary>
        /// Updates the specified user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        public void SetUpdateDate(int userId)
        {
            this.IsDeleted = false;
            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
        }

        /// <summary>
        /// Deletes the specified user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        public void Delete(int userId)
        {
            this.IsDeleted = true;
            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
        }

        /// <summary>
        /// Restores the specified user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        public void Restore(int userId)
        {
            this.IsDeleted = false;
            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
        }

        ///// <summary>
        ///// Creates the specified user identifier.
        ///// </summary>
        ///// <param name="userId">The user identifier.</param>
        //public virtual void Create(int userId)
        //{
        //    this.IsDeleted = false;
        //    this.CreateDate = this.UpdateDate = DateTime.UtcNow;
        //    this.CreateUserId = this.UpdateUserId = userId;
        //}

        ///// <summary>
        ///// Updates the specified user identifier.
        ///// </summary>
        ///// <param name="userId">The user identifier.</param>
        //public virtual void Update(int userId)
        //{
        //    this.IsDeleted = false;
        //    this.UpdateDate = DateTime.UtcNow;
        //    this.UpdateUserId = userId;
        //}

        ///// <summary>
        ///// Deletes the specified user identifier.
        ///// </summary>
        ///// <param name="userId">The user identifier.</param>
        //public virtual void Delete(int userId)
        //{
        //    this.IsDeleted = true;
        //    this.UpdateDate = DateTime.UtcNow;
        //    this.UpdateUserId = userId;
        //}
    }
}
