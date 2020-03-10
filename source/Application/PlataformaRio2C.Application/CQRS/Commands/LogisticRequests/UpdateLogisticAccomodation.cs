// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Arthur Souza
// Created          : 01-27-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-10-2020
// ***********************************************************************
// <copyright file="UpdateLogisticAccomodation.cs" company="Softo">
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
    /// <summary>UpdateLogisticAccomodation</summary>
    public class UpdateLogisticAccomodation : CreateLogisticAccomodation
    {
        public Guid Uid { get; set; }

        /// <summary>Initializes a new instance of the <see cref="UpdateLogisticAccomodation"/> class.</summary>
        /// <param name="uid">The uid.</param>
        /// <param name="checkInDate">The check in date.</param>
        /// <param name="checkOutDate">The check out date.</param>
        /// <param name="additionalInfo">The additional information.</param>
        /// <param name="placeId">The place identifier.</param>
        /// <param name="dtos">The dtos.</param>
        public UpdateLogisticAccomodation(Guid uid, DateTimeOffset? checkInDate, DateTimeOffset? checkOutDate, string additionalInfo, int placeId, List<AttendeePlaceDto> dtos)
        {
            this.Uid = uid;
            this.CheckInDate = checkInDate?.ToUserTimeZone();
            this.CheckOutDate = checkOutDate?.ToUserTimeZone();
            this.AdditionalInfo = additionalInfo;
            this.PlaceId = placeId;
            this.Places = dtos;
        }

        /// <summary>Initializes a new instance of the <see cref="UpdateLogisticAccomodation"/> class.</summary>
        public UpdateLogisticAccomodation()
        {
        }
    }
}