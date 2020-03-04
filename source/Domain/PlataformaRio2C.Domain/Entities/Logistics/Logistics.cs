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

        /// <summary>
        /// Initializes a new instance of the <see cref="Logistics"/> class.
        /// </summary>
        protected Logistics()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Logistics" /> class.
        /// </summary>
        /// <param name="attendeeCollaborator"></param>
        /// <param name="isAirfareSponsored">if set to <c>true</c> [is airfare sponsored].</param>
        /// <param name="airfareAttendeeLogisticSponsorId">The airfare attendee logistic sponsor identifier.</param>
        /// <param name="isAccommodationSponsored">if set to <c>true</c> [is accommodation sponsored].</param>
        /// <param name="accommodationAttendeeLogisticSponsorId">The accommodation attendee logistic sponsor identifier.</param>
        /// <param name="isAirportTransferSponsored">if set to <c>true</c> [is airport transfer sponsored].</param>
        /// <param name="airportTransferAttendeeLogisticSponsorId">The airport transfer attendee logistic sponsor identifier.</param>
        /// <param name="isCityTransferRequired">if set to <c>true</c> [is city transfer required].</param>
        /// <param name="isVehicleDisposalRequired">if set to <c>true</c> [is vehicle disposal required].</param>
        /// <param name="additionalInfo">The additional information.</param>
        /// <param name="userId"></param>
        public Logistics(AttendeeCollaborator attendeeCollaborator, bool isAirfareSponsored, int? airfareAttendeeLogisticSponsorId, bool isAccommodationSponsored, int? accommodationAttendeeLogisticSponsorId, bool isAirportTransferSponsored, int? airportTransferAttendeeLogisticSponsorId, bool isCityTransferRequired, bool isVehicleDisposalRequired, string additionalInfo, int userId)
        {
            this.AttendeeCollaborator = attendeeCollaborator;

            this.IsAirfareSponsored = isAirfareSponsored;
            this.AirfareAttendeeLogisticSponsorId = airfareAttendeeLogisticSponsorId;
            this.IsAccommodationSponsored = isAccommodationSponsored;
            this.AccommodationAttendeeLogisticSponsorId = accommodationAttendeeLogisticSponsorId;
            this.IsAirportTransferSponsored = isAirportTransferSponsored;
            this.AirportTransferAttendeeLogisticSponsorId = airportTransferAttendeeLogisticSponsorId;
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
