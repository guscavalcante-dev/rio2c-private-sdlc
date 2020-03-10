// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-10-2020
// ***********************************************************************
// <copyright file="Logistics.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities.Validations;
using PlataformaRio2C.Domain.Validation;
using System;
using System.Collections.Generic;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>
    /// Logistics
    /// </summary>
    public class Logistics : Entity
    {
        public int AttendeeCollaboratorId { get; private set; }
        public bool IsAirfareSponsored { get; private set; }
        public int? AirfareAttendeeLogisticSponsorId { get; private set; }
        public bool IsAccommodationSponsored { get; private set; }
        public int? AccommodationAttendeeLogisticSponsorId { get; private set; }
        public bool IsAirportTransferSponsored { get; private set; }
        public int? AirportTransferAttendeeLogisticSponsorId { get; private set; }
        public bool IsCityTransferRequired { get; private set; }
        public bool IsVehicleDisposalRequired { get; private set; }
        public string AdditionalInfo { get; private set; }

        public virtual AttendeeCollaborator AttendeeCollaborator { get; private set; }
        public virtual AttendeeLogisticSponsor AirportTransferSponsor { get; private set; }
        public virtual AttendeeLogisticSponsor AirfareSponsor { get; private set; }
        public virtual AttendeeLogisticSponsor AccommodationSponsor { get; private set; }
        public virtual List<LogisticAirfare> LogisticAirfare { get; private set; }
        public virtual List<LogisticAccommodation> LogisticAccommodation { get; private set; }
        public virtual List<LogisticTransfer> LogisticTransfer { get; private set; }
        public virtual User CreateUser { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="Logistics"/> class.</summary>
        /// <param name="edition">The edition.</param>
        /// <param name="attendeeCollaborator">The attendee collaborator.</param>
        /// <param name="isAirfareSponsored">if set to <c>true</c> [is airfare sponsored].</param>
        /// <param name="airfareAttendeeLogisticSponsor">The airfare attendee logistic sponsor.</param>
        /// <param name="airfareSponsorName">Name of the airfare sponsor.</param>
        /// <param name="isAccommodationSponsored">if set to <c>true</c> [is accommodation sponsored].</param>
        /// <param name="accommodationAttendeeLogisticSponsor">The accommodation attendee logistic sponsor.</param>
        /// <param name="accommodationSponsorName">Name of the accommodation sponsor.</param>
        /// <param name="isAirportTransferSponsored">if set to <c>true</c> [is airport transfer sponsored].</param>
        /// <param name="airportTransferAttendeeLogisticSponsor">The airport transfer attendee logistic sponsor.</param>
        /// <param name="airportTransferSponsorName">Name of the airport transfer sponsor.</param>
        /// <param name="isCityTransferRequired">if set to <c>true</c> [is city transfer required].</param>
        /// <param name="isVehicleDisposalRequired">if set to <c>true</c> [is vehicle disposal required].</param>
        /// <param name="additionalInfo">The additional information.</param>
        /// <param name="userId">The user identifier.</param>
        public Logistics(
            Edition edition,
            AttendeeCollaborator attendeeCollaborator,
            bool isAirfareSponsored,
            AttendeeLogisticSponsor airfareAttendeeLogisticSponsor,
            string airfareSponsorName,
            bool isAccommodationSponsored,
            AttendeeLogisticSponsor accommodationAttendeeLogisticSponsor,
            string accommodationSponsorName,
            bool isAirportTransferSponsored,
            AttendeeLogisticSponsor airportTransferAttendeeLogisticSponsor,
            string airportTransferSponsorName,
            bool isCityTransferRequired,
            bool isVehicleDisposalRequired,
            string additionalInfo,
            int userId)
        {
            this.AttendeeCollaborator = attendeeCollaborator;
            this.AttendeeCollaboratorId = attendeeCollaborator.Id;

            if (!string.IsNullOrEmpty(airfareSponsorName))
            {
                airfareAttendeeLogisticSponsor = new AttendeeLogisticSponsor(edition, airfareSponsorName, userId);
            }
            else if (airfareAttendeeLogisticSponsor != null)
            {
                this.AirfareAttendeeLogisticSponsorId = airfareAttendeeLogisticSponsor.Id;
            }

            if (!string.IsNullOrEmpty(airportTransferSponsorName))
            {
                airportTransferAttendeeLogisticSponsor = new AttendeeLogisticSponsor(edition, airportTransferSponsorName, userId);
            }
            else if (airportTransferAttendeeLogisticSponsor != null)
            {
                this.AirportTransferAttendeeLogisticSponsorId = airportTransferAttendeeLogisticSponsor.Id;
            }

            if (!string.IsNullOrEmpty(accommodationSponsorName))
            {
                accommodationAttendeeLogisticSponsor = new AttendeeLogisticSponsor(edition, accommodationSponsorName, userId);
            }
            else if (accommodationAttendeeLogisticSponsor != null)
            {
                this.AccommodationAttendeeLogisticSponsorId = accommodationAttendeeLogisticSponsor.Id;
            }

            this.AirfareSponsor = airfareAttendeeLogisticSponsor;
            this.AccommodationSponsor = accommodationAttendeeLogisticSponsor;
            this.AirportTransferSponsor = airportTransferAttendeeLogisticSponsor;
            this.IsAirfareSponsored = isAirfareSponsored;
            this.IsAccommodationSponsored = isAccommodationSponsored;
            this.IsAirportTransferSponsored = isAirportTransferSponsored;
            this.IsCityTransferRequired = isCityTransferRequired;
            this.IsVehicleDisposalRequired = isVehicleDisposalRequired;
            this.AdditionalInfo = additionalInfo;
            this.CreateUserId = this.UpdateUserId = userId;
            this.CreateDate = this.UpdateDate = DateTime.UtcNow;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Logistics"/> class.
        /// </summary>
        protected Logistics()
        {
        }

        /// <summary>Updates the specified edition.</summary>
        /// <param name="edition">The edition.</param>
        /// <param name="attendeeCollaborator">The attendee collaborator.</param>
        /// <param name="isAirfareSponsored">if set to <c>true</c> [is airfare sponsored].</param>
        /// <param name="airfareAttendeeLogisticSponsor">The airfare attendee logistic sponsor.</param>
        /// <param name="airfareSponsorName">Name of the airfare sponsor.</param>
        /// <param name="isAccommodationSponsored">if set to <c>true</c> [is accommodation sponsored].</param>
        /// <param name="accommodationAttendeeLogisticSponsor">The accommodation attendee logistic sponsor.</param>
        /// <param name="accommodationSponsorName">Name of the accommodation sponsor.</param>
        /// <param name="isAirportTransferSponsored">if set to <c>true</c> [is airport transfer sponsored].</param>
        /// <param name="airportTransferAttendeeLogisticSponsor">The airport transfer attendee logistic sponsor.</param>
        /// <param name="airportTransferSponsorName">Name of the airport transfer sponsor.</param>
        /// <param name="isCityTransferRequired">if set to <c>true</c> [is city transfer required].</param>
        /// <param name="isVehicleDisposalRequired">if set to <c>true</c> [is vehicle disposal required].</param>
        /// <param name="additionalInfo">The additional information.</param>
        /// <param name="userId">The user identifier.</param>
        public void Update(
            Edition edition,
            AttendeeCollaborator attendeeCollaborator,
            bool isAirfareSponsored,
            AttendeeLogisticSponsor airfareAttendeeLogisticSponsor,
            string airfareSponsorName,
            bool isAccommodationSponsored,
            AttendeeLogisticSponsor accommodationAttendeeLogisticSponsor,
            string accommodationSponsorName,
            bool isAirportTransferSponsored,
            AttendeeLogisticSponsor airportTransferAttendeeLogisticSponsor,
            string airportTransferSponsorName,
            bool isCityTransferRequired,
            bool isVehicleDisposalRequired,
            string additionalInfo,
            int userId)
        {
            this.AttendeeCollaborator = attendeeCollaborator;
            this.AttendeeCollaboratorId = attendeeCollaborator.Id;

            if (!string.IsNullOrEmpty(airfareSponsorName))
            {
                airfareAttendeeLogisticSponsor = new AttendeeLogisticSponsor(edition, airfareSponsorName, userId);
            }
            else if (airfareAttendeeLogisticSponsor != null)
            {
                this.AirfareAttendeeLogisticSponsorId = airfareAttendeeLogisticSponsor.Id;
            }

            if (!string.IsNullOrEmpty(airportTransferSponsorName))
            {
                airportTransferAttendeeLogisticSponsor = new AttendeeLogisticSponsor(edition, airportTransferSponsorName, userId);
            }
            else if (airportTransferAttendeeLogisticSponsor != null)
            {
                this.AirportTransferAttendeeLogisticSponsorId = airportTransferAttendeeLogisticSponsor.Id;
            }

            if (!string.IsNullOrEmpty(accommodationSponsorName))
            {
                accommodationAttendeeLogisticSponsor = new AttendeeLogisticSponsor(edition, accommodationSponsorName, userId);
            }
            else if (accommodationAttendeeLogisticSponsor != null)
            {
                this.AccommodationAttendeeLogisticSponsorId = accommodationAttendeeLogisticSponsor.Id;
            }

            this.AirfareSponsor = airfareAttendeeLogisticSponsor;
            this.AccommodationSponsor = accommodationAttendeeLogisticSponsor;
            this.AirportTransferSponsor = airportTransferAttendeeLogisticSponsor;
            this.IsAirfareSponsored = isAirfareSponsored;
            this.IsAccommodationSponsored = isAccommodationSponsored;
            this.IsAirportTransferSponsored = isAirportTransferSponsored;
            this.IsCityTransferRequired = isCityTransferRequired;
            this.IsVehicleDisposalRequired = isVehicleDisposalRequired;
            this.AdditionalInfo = additionalInfo;

            this.IsDeleted = false;
            this.UpdateUserId = userId;
            this.UpdateDate = DateTime.UtcNow;
        }

        /// <summary>Deletes the specified user identifier.</summary>
        /// <param name="userId">The user identifier.</param>
        public void Delete(int userId)
        {
            this.IsDeleted = true;
            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
        }

        #region Validations

        /// <summary>
        /// Returns true if ... is valid.
        /// </summary>
        /// <returns><c>true</c> if this instance is valid; otherwise, <c>false</c>.</returns>
        public override bool IsValid()
        {
            ValidationResult = new ValidationResult();

            ValidationResult.Add(new LogisticsIsConsistent().Valid(this));

            return ValidationResult.IsValid;
        }

        #endregion
    }
}
