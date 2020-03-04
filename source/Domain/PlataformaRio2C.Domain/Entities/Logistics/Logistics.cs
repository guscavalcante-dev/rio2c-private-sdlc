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

        #region Legacy
        /// <summary>
        /// Gets the arrival date.
        /// </summary>
        /// <value>The arrival date.</value>
        public DateTime? ArrivalDate { get; private set; }
        /// <summary>
        /// Gets the arrival time.
        /// </summary>
        /// <value>The arrival time.</value>
        public TimeSpan? ArrivalTime { get; private set; }
        /// <summary>
        /// Gets the departure date.
        /// </summary>
        /// <value>The departure date.</value>
        public DateTime? DepartureDate { get; private set; }
        /// <summary>
        /// Gets the departure time.
        /// </summary>
        /// <value>The departure time.</value>
        public TimeSpan? DepartureTime { get; private set; }
        /// <summary>
        /// Gets the collaborator identifier.
        /// </summary>
        /// <value>The collaborator identifier.</value>
        public int CollaboratorId { get; private set; }
        /// <summary>
        /// Gets the collaborator.
        /// </summary>
        /// <value>The collaborator.</value>
        public virtual Collaborator Collaborator { get; private set; }
        /// <summary>
        /// Gets the event identifier.
        /// </summary>
        /// <value>The event identifier.</value>
        public int EventId { get; private set; }
        /// <summary>
        /// Gets the edition.
        /// </summary>
        /// <value>The edition.</value>
        public virtual Edition Edition { get; private set; }
        /// <summary>
        /// Gets the name of the original.
        /// </summary>
        /// <value>The name of the original.</value>
        public string OriginalName { get; private set; }
        /// <summary>
        /// Gets the name of the server.
        /// </summary>
        /// <value>The name of the server.</value>
        public string ServerName { get; private set; }
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="Logistics"/> class.
        /// </summary>
        protected Logistics()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Logistics" /> class.
        /// </summary>
        /// <param name="attendeeCollaboratorId">The attendee collaborator identifier.</param>
        /// <param name="isAirfareSponsored">if set to <c>true</c> [is airfare sponsored].</param>
        /// <param name="airfareAttendeeLogisticSponsorId">The airfare attendee logistic sponsor identifier.</param>
        /// <param name="isAccommodationSponsored">if set to <c>true</c> [is accommodation sponsored].</param>
        /// <param name="accommodationAttendeeLogisticSponsorId">The accommodation attendee logistic sponsor identifier.</param>
        /// <param name="isAirportTransferSponsored">if set to <c>true</c> [is airport transfer sponsored].</param>
        /// <param name="airportTransferAttendeeLogisticSponsorId">The airport transfer attendee logistic sponsor identifier.</param>
        /// <param name="isCityTransferRequired">if set to <c>true</c> [is city transfer required].</param>
        /// <param name="isVehicleDisposalRequired">if set to <c>true</c> [is vehicle disposal required].</param>
        /// <param name="additionalInfo">The additional information.</param>
        public Logistics(int? attendeeCollaboratorId, bool isAirfareSponsored, int? airfareAttendeeLogisticSponsorId, bool isAccommodationSponsored, int? accommodationAttendeeLogisticSponsorId, bool isAirportTransferSponsored, int? airportTransferAttendeeLogisticSponsorId, bool isCityTransferRequired, bool isVehicleDisposalRequired, string additionalInfo)
        {
            // TODO: Validate
            this.AttendeeCollaboratorId = attendeeCollaboratorId.Value;
            this.IsAirfareSponsored = isAirfareSponsored;
            this.AirfareAttendeeLogisticSponsorId = airfareAttendeeLogisticSponsorId;
            this.IsAccommodationSponsored = isAccommodationSponsored;
            this.AccommodationAttendeeLogisticSponsorId = accommodationAttendeeLogisticSponsorId;
            this.IsAirportTransferSponsored = isAirportTransferSponsored;
            this.AirportTransferAttendeeLogisticSponsorId = airportTransferAttendeeLogisticSponsorId;
            this.IsCityTransferRequired = isCityTransferRequired;
            this.IsVehicleDisposalRequired = isVehicleDisposalRequired;
            this.AdditionalInfo = additionalInfo;
        }

        #region Legacy

        /// <summary>
        /// Initializes a new instance of the <see cref="Logistics"/> class.
        /// </summary>
        /// <param name="arrivalDate">The arrival date.</param>
        /// <param name="departureDate">The departure date.</param>
        public Logistics(DateTime? arrivalDate, DateTime? departureDate)
        {
            SetArrivalDate(arrivalDate);
            SetDepartureDate(departureDate);
        }

        /// <summary>
        /// Sets the arrival time.
        /// </summary>
        /// <param name="arrivalTime">The arrival time.</param>
        public void SetArrivalTime(TimeSpan? arrivalTime)
        {
            ArrivalTime = arrivalTime;
        }

        /// <summary>
        /// Sets the arrival date.
        /// </summary>
        /// <param name="val">The value.</param>
        public void SetArrivalDate(DateTime? val)
        {
            ArrivalDate = val;
        }


        /// <summary>
        /// Sets the departure time.
        /// </summary>
        /// <param name="departureTime">The departure time.</param>
        public void SetDepartureTime(TimeSpan? departureTime)
        {
            DepartureTime = departureTime;
        }

        /// <summary>
        /// Sets the departure date.
        /// </summary>
        /// <param name="val">The value.</param>
        public void SetDepartureDate(DateTime? val)
        {
            DepartureDate = val;
        }

        /// <summary>
        /// Sets the name of the original.
        /// </summary>
        /// <param name="originalName">Name of the original.</param>
        public void SetOriginalName(string originalName)
        {
            OriginalName = originalName;
        }

        /// <summary>
        /// Sets the name of the server.
        /// </summary>
        /// <param name="serverName">Name of the server.</param>
        public void SetServerName(string serverName)
        {
            ServerName = serverName;
        }

        /// <summary>
        /// Sets the collaborator.
        /// </summary>
        /// <param name="collaborator">The collaborator.</param>
        public void SetCollaborator(Collaborator collaborator)
        {
            Collaborator = collaborator;
            if (collaborator != null)
            {
                CollaboratorId = collaborator.Id;
            }
        }

        /// <summary>
        /// Sets the event.
        /// </summary>
        /// <param name="eventEntity">The event entity.</param>
        public void SetEvent(Edition eventEntity)
        {
            Edition = eventEntity;
            if (eventEntity != null)
            {
                EventId = eventEntity.Id;
            }
        }

        #endregion

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
