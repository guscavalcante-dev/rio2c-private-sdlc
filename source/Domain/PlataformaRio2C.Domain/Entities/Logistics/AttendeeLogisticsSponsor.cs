// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Arthur Souza
// Created          : 01-20-2020
//
// Last Modified By : Arthur Souza
// Last Modified On : 01-20-2020
// ***********************************************************************
// <copyright file="AttendeeLogisticSponsor.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using PlataformaRio2C.Domain.Validation;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>AttendeeLogisticSponsor</summary>
    public class AttendeeLogisticSponsor : Entity
    {
        protected AttendeeLogisticSponsor()
        {
        }
        
        public AttendeeLogisticSponsor(Edition edition, string name, int userId)
        {
            this.Edition = edition;
            this.IsOther = true;
            this.CreateUserId = this.UpdateUserId = userId;
            this.CreateDate = this.UpdateDate = DateTime.Now;
            this.LogisticSponsor = new LogisticSponsor(name, userId);
        }

        public AttendeeLogisticSponsor(Edition edition, LogisticSponsor logisticSponsor, int userId)
        {
            this.Edition = edition;
            this.LogisticSponsor = logisticSponsor;
            this.CreateUserId = this.UpdateUserId = userId;
            this.CreateDate = this.UpdateDate = DateTime.Now;
        }
        
        public void Update(Edition edition, LogisticSponsor logisticSponsor, int userId)
        {
            this.Edition = edition;
            this.LogisticSponsor = logisticSponsor;
            this.UpdateUserId = userId;
            this.UpdateDate = DateTime.Now;
        }

        public void Delete(int userId)
        {            
            this.IsDeleted = true;
            this.UpdateDate = DateTime.Now;
            this.UpdateUserId = userId;
        }

        public int EditionId { get; private set; }
        public int LogisticSponsorId { get; private set; }
        public bool IsOther { get; private set; }
        public virtual Edition Edition { get; private set; }
        public virtual LogisticSponsor LogisticSponsor { get; private set; }

        public virtual ICollection<Logistics> AccommodationSponsors { get; private set; }
        public virtual ICollection<Logistics> AirfareSponsors { get; private set; }
        public virtual ICollection<Logistics> AirportTransferSponsors { get; private set; }

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