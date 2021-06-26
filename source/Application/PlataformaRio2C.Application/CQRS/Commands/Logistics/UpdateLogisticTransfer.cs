// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Arthur Souza
// Created          : 01-27-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 06-26-2021
// ***********************************************************************
// <copyright file="UpdateLogisticTransfer.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>UpdateLogisticTransfer</summary>
    public class UpdateLogisticTransfer : CreateLogisticTransfer
    {
        public Guid Uid { get; set; }

        /// <summary>Initializes a new instance of the <see cref="UpdateLogisticTransfer"/> class.</summary>
        /// <param name="uid">The uid.</param>
        /// <param name="fromAttendeePlaceId">From attendee place identifier.</param>
        /// <param name="attendeePlaceId">The attendee place identifier.</param>
        /// <param name="date">The date.</param>
        /// <param name="additionalInfo">The additional information.</param>
        /// <param name="attendeePlaceDropdownDto">The attendee place dropdown dto.</param>
        public UpdateLogisticTransfer(Guid uid, int? fromAttendeePlaceId, int? attendeePlaceId, DateTimeOffset date, string additionalInfo, List<AttendeePlaceDropdownDto> attendeePlaceDropdownDto)
        {
            this.Uid = uid;
            this.FromAttendeePlaceId = fromAttendeePlaceId;
            this.ToAttendeePlaceId = attendeePlaceId;
            this.UpdateDate(date);
            this.AdditionalInfo = additionalInfo;

            this.UpdateLists(attendeePlaceDropdownDto);
        }

        /// <summary>Initializes a new instance of the <see cref="UpdateLogisticTransfer"/> class.</summary>
        public UpdateLogisticTransfer()
        {
        }

        #region Private Methods

        /// <summary>Updates the date.</summary>
        /// <param name="date">The date.</param>
        private void UpdateDate(DateTimeOffset? date)
        {
            var userDate = date?.ToBrazilTimeZone();
            this.Date = userDate?.ToShortDateString() + " " + userDate?.ToString("HH:mm");
        }

        #endregion
    }
}