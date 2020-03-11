// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Arthur Souza
// Created          : 01-20-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-11-2020
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
        /// <param name="userId">The user identifier.</param>
        public AttendeeLogisticSponsor(Edition edition, LogisticSponsor logisticSponsor, int userId)
        {
            this.Edition = edition;
            this.LogisticSponsor = logisticSponsor;

            this.IsDeleted = false;
            this.CreateUserId = this.UpdateUserId = userId;
            this.CreateDate = this.UpdateDate = DateTime.Now;
        }

        /// <summary>Initializes a new instance of the <see cref="AttendeeLogisticSponsor"/> class.</summary>
        /// <param name="edition">The edition.</param>
        /// <param name="name">The name.</param>
        /// <param name="userId">The user identifier.</param>
        public AttendeeLogisticSponsor(Edition edition, string name, int userId)
        {
            this.Edition = edition;
            this.LogisticSponsor = new LogisticSponsor(name, userId);
            this.IsOther = true;

            this.IsDeleted = false;
            this.CreateUserId = this.UpdateUserId = userId;
            this.CreateDate = this.UpdateDate = DateTime.Now;
        }

        /// <summary>Initializes a new instance of the <see cref="AttendeeLogisticSponsor"/> class.</summary>
        protected AttendeeLogisticSponsor()
        {
        }

        /// <summary>Updates the specified edition.</summary>
        /// <param name="edition">The edition.</param>
        /// <param name="logisticSponsor">The logistic sponsor.</param>
        /// <param name="userId">The user identifier.</param>
        public void Update(Edition edition, LogisticSponsor logisticSponsor, int userId)
        {
            this.Edition = edition;
            this.LogisticSponsor = logisticSponsor;

            this.IsDeleted = false;
            this.UpdateUserId = userId;
            this.UpdateDate = DateTime.Now;
        }

        /// <summary>Deletes the specified user identifier.</summary>
        /// <param name="userId">The user identifier.</param>
        public void Delete(int userId)
        {            
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
            if (this.ValidationResult == null)
            {
                this.ValidationResult = new ValidationResult();
            }

            return this.ValidationResult.IsValid;
        }

        #endregion
    }
}