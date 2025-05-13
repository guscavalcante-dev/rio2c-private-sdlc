// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Arthur Souza
// Created          : 01-27-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 06-26-2021
// ***********************************************************************
// <copyright file="UpdateLogisticAccommodation.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using System;
using System.Collections.Generic;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>UpdateLogisticAccommodation</summary>
    public class UpdateLogisticAccommodation : CreateLogisticAccommodation
    {
        public Guid Uid { get; set; }

        /// <summary>Initializes a new instance of the <see cref="UpdateLogisticAccommodation"/> class.</summary>
        /// <param name="uid">The uid.</param>
        /// <param name="checkInDate">The check in date.</param>
        /// <param name="checkOutDate">The check out date.</param>
        /// <param name="additionalInfo">The additional information.</param>
        /// <param name="placeId">The place identifier.</param>
        /// <param name="attendeePlaceDtos">The attendee place dtos.</param>
        public UpdateLogisticAccommodation(Guid uid, DateTimeOffset? checkInDate, DateTimeOffset? checkOutDate, string additionalInfo, int placeId, List<AttendeePlaceDropdownDto> attendeePlaceDtos)
        {
            this.Uid = uid;
            this.UpdateDates(checkInDate, checkOutDate);
            this.AdditionalInfo = additionalInfo;
            this.PlaceId = placeId;
            this.UpdateLists(attendeePlaceDtos);
        }

        /// <summary>Initializes a new instance of the <see cref="UpdateLogisticAccommodation"/> class.</summary>
        public UpdateLogisticAccommodation()
        {
        }

        #region Private Methods

        /// <summary>Updates the dates.</summary>
        /// <param name="checkInDate">The check in date.</param>
        /// <param name="checkOutDate">The check out date.</param>
        private void UpdateDates(DateTimeOffset? checkInDate, DateTimeOffset? checkOutDate)
        {
            var checkIn = checkInDate?.ToBrazilTimeZone();
            this.CheckInDate = checkIn?.ToShortDateString() + " " + checkIn?.ToString("HH:mm");

            var checkOut = checkOutDate?.ToBrazilTimeZone();
            this.CheckOutDate = checkOut?.ToShortDateString() + " " + checkOut?.ToString("HH:mm");
        }

        #endregion
    }
}