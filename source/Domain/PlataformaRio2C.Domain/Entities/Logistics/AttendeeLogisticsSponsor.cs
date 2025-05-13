// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Arthur Souza
// Created          : 01-20-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-20-2020
// ***********************************************************************
// <copyright file="AttendeeLogisticSponsor.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public virtual ICollection<Logistic> AirfareLogistics { get; private set; }
        public virtual ICollection<Logistic> AccommodationLogistics { get; private set; }
        public virtual ICollection<Logistic> AirportTransferLogistics { get; private set; }

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

            this.DissociateAirfareLogistics(userId);
            this.DissociateAccommodationLogistics(userId);
            this.DissociateAirportTransferLogistics(userId);

            if (this.FindAllAirfareLogisticsNotDeleted()?.Any() == true || this.FindAllAccommodationLogisticsNotDeleted()?.Any() == true || this.FindAllAirportTransferLogisticsNotDeleted()?.Any() == true)
            {
                return;
            }

            this.IsDeleted = true;
            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
        }

        #region Airfare Logistics

        /// <summary>Dissociates the airfare logistics.</summary>
        /// <param name="userId">The user identifier.</param>
        private void DissociateAirfareLogistics(int userId)
        {
            foreach (var airfareLogistic in this.FindAllAirfareLogisticsNotDeleted())
            {
                airfareLogistic?.DissociateAirfareAttendeeLogisticSponsor(userId);
            }
        }

        /// <summary>Finds all airfare logistics not deleted.</summary>
        /// <returns></returns>
        private List<Logistic> FindAllAirfareLogisticsNotDeleted()
        {
            return this.AirfareLogistics?.Where(al => !al.IsDeleted && al.AirfareAttendeeLogisticSponsorId == this.Id)?.ToList();
        }

        #endregion

        #region Accommodation Logistics

        /// <summary>Dissociates the accommodation logistics.</summary>
        /// <param name="userId">The user identifier.</param>
        private void DissociateAccommodationLogistics(int userId)
        {
            foreach (var accommodationLogistic in this.FindAllAccommodationLogisticsNotDeleted())
            {
                accommodationLogistic?.DissociateAccommodationAttendeeLogisticSponsor(userId);
            }
        }

        /// <summary>Finds all accommodation logistics not deleted.</summary>
        /// <returns></returns>
        private List<Logistic> FindAllAccommodationLogisticsNotDeleted()
        {
            return this.AccommodationLogistics?.Where(al => !al.IsDeleted && al.AccommodationAttendeeLogisticSponsorId == this.Id)?.ToList();
        }

        #endregion

        #region Airport Transfer Logistics

        /// <summary>Dissociates the airport transfer logistics.</summary>
        /// <param name="userId">The user identifier.</param>
        private void DissociateAirportTransferLogistics(int userId)
        {
            foreach (var airportTransferLogistic in this.FindAllAirportTransferLogisticsNotDeleted())
            {
                airportTransferLogistic?.DissociateAirportTransferAttendeeLogisticSponsor(userId);
            }
        }

        /// <summary>Finds all airport transfer logistics not deleted.</summary>
        /// <returns></returns>
        private List<Logistic> FindAllAirportTransferLogisticsNotDeleted()
        {
            return this.AirportTransferLogistics?.Where(atl => !atl.IsDeleted && atl.AirportTransferAttendeeLogisticSponsorId == this.Id)?.ToList();
        }

        #endregion

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