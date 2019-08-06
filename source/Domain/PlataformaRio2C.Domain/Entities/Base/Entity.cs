// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-06-2019
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
        public DateTime CreateDate { get; set; }
        public int CreateUserId { get; set; }
        public DateTime UpdateDate { get; set; }
        public int UpdateUserId { get; set; }

        public virtual ValidationResult ValidationResult { get; set; }

        public abstract bool IsValid();

        /// <summary>Sets the uid.</summary>
        /// <param name="uid">The uid.</param>
        public void SetUid(Guid uid)
        {
            this.Uid = uid;
        }
    }
}
