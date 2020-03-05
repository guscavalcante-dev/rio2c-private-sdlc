// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Arthur Souza
// Last Modified On : 03-04-2020
// ***********************************************************************
// <copyright file="Logistics.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities.Validations;
using PlataformaRio2C.Domain.Validation;
using System;
using System.Security.AccessControl;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>
    /// Logistics
    /// </summary>
    public class Logistics : Entity
    {
        /// <summary>
        /// Gets the attendee collaborator identifier.
        /// </summary>
        /// <value>The attendee collaborator identifier.</value>
        public int AttendeeCollaboratorId { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this instance is airfare sponsored.
        /// </summary>
        /// <value><c>true</c> if this instance is airfare sponsored; otherwise, <c>false</c>.</value>
        public bool IsAirfareSponsored { get; private set; }

        /// <summary>
        /// Gets the airfare attendee logistic sponsor identifier.
        /// </summary>
        /// <value>The airfare attendee logistic sponsor identifier.</value>
        public int? AirfareAttendeeLogisticSponsorId { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this instance is accommodation sponsored.
        /// </summary>
        /// <value><c>true</c> if this instance is accommodation sponsored; otherwise, <c>false</c>.</value>
        public bool IsAccommodationSponsored { get; private set; }

        /// <summary>
        /// Gets the accommodation attendee logistic sponsor identifier.
        /// </summary>
        /// <value>The accommodation attendee logistic sponsor identifier.</value>
        public int? AccommodationAttendeeLogisticSponsorId { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this instance is airport transfer sponsored.
        /// </summary>
        /// <value><c>true</c> if this instance is airport transfer sponsored; otherwise, <c>false</c>.</value>
        public bool IsAirportTransferSponsored { get; private set; }

        /// <summary>
        /// Gets the airport transfer attendee logistic sponsor identifier.
        /// </summary>
        /// <value>The airport transfer attendee logistic sponsor identifier.</value>
        public int? AirportTransferAttendeeLogisticSponsorId { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this instance is city transfer required.
        /// </summary>
        /// <value><c>true</c> if this instance is city transfer required; otherwise, <c>false</c>.</value>
        public bool IsCityTransferRequired { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this instance is vehicle disposal required.
        /// </summary>
        /// <value><c>true</c> if this instance is vehicle disposal required; otherwise, <c>false</c>.</value>
        public bool IsVehicleDisposalRequired { get; private set; }

        /// <summary>
        /// Gets the additional information.
        /// </summary>
        /// <value>The additional information.</value>
        public string AdditionalInfo { get; private set; }
        
        public virtual AttendeeCollaborator AttendeeCollaborator { get; private set; }
        public virtual LogisticSponsor AirportTransferSponsor { get; private set; }
        public virtual LogisticSponsor AirfareSponsor { get; private set; }
        public virtual LogisticSponsor AccommodationSponsor { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Logistics"/> class.
        /// </summary>
        protected Logistics()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Logistics" /> class.
        /// </summary>
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
        public Logistics(AttendeeCollaborator attendeeCollaborator,
            bool isAirfareSponsored,
            LogisticSponsor airfareAttendeeLogisticSponsor,
            string airfareSponsorName,
            bool isAccommodationSponsored,
            LogisticSponsor accommodationAttendeeLogisticSponsor,
            string accommodationSponsorName,
            bool isAirportTransferSponsored,
            LogisticSponsor airportTransferAttendeeLogisticSponsor,
            string airportTransferSponsorName,
            bool isCityTransferRequired,
            bool isVehicleDisposalRequired,
            string additionalInfo,
            int userId)
        {
            this.AttendeeCollaborator = attendeeCollaborator;
            this.AttendeeCollaboratorId = attendeeCollaborator.Id;
            
            if (!string.IsNullOrEmpty(airfareSponsorName))
                airfareAttendeeLogisticSponsor = new LogisticSponsor(airfareSponsorName, userId);
            else if (airfareAttendeeLogisticSponsor != null)
                this.AirfareAttendeeLogisticSponsorId = airfareAttendeeLogisticSponsor.Id;
            
            if (!string.IsNullOrEmpty(airportTransferSponsorName))
                airportTransferAttendeeLogisticSponsor = new LogisticSponsor(airportTransferSponsorName, userId);
            else if (airportTransferAttendeeLogisticSponsor != null)
                this.AirportTransferAttendeeLogisticSponsorId = airportTransferAttendeeLogisticSponsor.Id;

            if (!string.IsNullOrEmpty(accommodationSponsorName))
                accommodationAttendeeLogisticSponsor = new LogisticSponsor(accommodationSponsorName, userId);
            else if (accommodationAttendeeLogisticSponsor != null)
                this.AccommodationAttendeeLogisticSponsorId = accommodationAttendeeLogisticSponsor.Id;

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
        /// Returns true if ... is valid.
        /// </summary>
        /// <returns><c>true</c> if this instance is valid; otherwise, <c>false</c>.</returns>
        public override bool IsValid()
        {
            ValidationResult = new ValidationResult();

            ValidationResult.Add(new LogisticsIsConsistent().Valid(this));

            return ValidationResult.IsValid;
        }
    }
}
