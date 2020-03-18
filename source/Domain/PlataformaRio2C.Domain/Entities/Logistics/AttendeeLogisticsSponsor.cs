// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Arthur Souza
// Created          : 01-20-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-17-2020
// ***********************************************************************
// <copyright file="AttendeeLogisticSponsor.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using PlataformaRio2C.Domain.Validation;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>AttendeeLogisticSponsor</summary>
    public class AttendeeLogisticSponsor : Entity
    {
        public int EditionId { get; private set; }
        public int LogisticSponsorId { get; private set; }
        public bool IsOther { get; private set; }
        public bool IsLogisticListDisplayed { get; private set; }

        public virtual Edition Edition { get; private set; }
        public virtual LogisticSponsor LogisticSponsor { get; private set; }

        //public virtual ICollection<Logistic> AccommodationSponsors { get; private set; }
        //public virtual ICollection<Logistic> AirfareSponsors { get; private set; }
        //public virtual ICollection<Logistic> AirportTransferSponsors { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="AttendeeLogisticSponsor"/> class.</summary>
        /// <param name="edition">The edition.</param>
        /// <param name="logisticSponsor">The logistic sponsor.</param>
        /// <param name="isOther">if set to <c>true</c> [is other].</param>
        /// <param name="isLogisticListDisplayed">if set to <c>true</c> [is logistic list displayed].</param>
        /// <param name="userId">The user identifier.</param>
        public AttendeeLogisticSponsor(Edition edition, LogisticSponsor logisticSponsor, bool isOther, bool isLogisticListDisplayed, int userId)
        {
            this.Edition = edition;
            this.LogisticSponsor = logisticSponsor;
            this.IsOther = isOther;
            this.IsLogisticListDisplayed = isLogisticListDisplayed;

            this.IsDeleted = false;
            this.CreateUserId = this.UpdateUserId = userId;
            this.CreateDate = this.UpdateDate = DateTime.Now;
        }

        /// <summary>Initializes a new instance of the <see cref="AttendeeLogisticSponsor"/> class for others.</summary>
        /// <param name="edition">The edition.</param>
        /// <param name="name">The name.</param>
        /// <param name="userId">The user identifier.</param>
        public AttendeeLogisticSponsor(Edition edition, string name, int userId)
        {
            this.Edition = edition;
            this.LogisticSponsor = new LogisticSponsor(name, userId);
            this.IsOther = true;
            this.IsLogisticListDisplayed = false;

            this.IsDeleted = false;
            this.CreateUserId = this.UpdateUserId = userId;
            this.CreateDate = this.UpdateDate = DateTime.Now;
        }

        /// <summary>Initializes a new instance of the <see cref="AttendeeLogisticSponsor"/> class.</summary>
        protected AttendeeLogisticSponsor()
        {
        }

        /// <summary>Updates the specified is other.</summary>
        /// <param name="isOther">if set to <c>true</c> [is other].</param>
        /// <param name="isLogisticListDisplayed">if set to <c>true</c> [is logistic list displayed].</param>
        /// <param name="userId">The user identifier.</param>
        public void Update(bool isOther, bool isLogisticListDisplayed, int userId)
        {
            this.IsOther = isOther;
            this.IsLogisticListDisplayed = isLogisticListDisplayed;

            this.IsDeleted = false;
            this.UpdateDate = DateTime.UtcNow;
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
            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
        }

        #region Validations

        /// <summary>Returns true if ... is valid.</summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.</returns>
        public override bool IsValid()
        {
            if (this.ValidationResult == null)
            {
                this.ValidationResult = new ValidationResult();
            }

            return this.ValidationResult.IsValid;
        }

        #endregion
    }
}