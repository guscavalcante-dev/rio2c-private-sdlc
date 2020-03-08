// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Arthur Souza
// Created          : 01-27-2020
//
// Last Modified By : Arthur Souza
// Last Modified On : 01-27-2020
// ***********************************************************************
// <copyright file="CreateConference.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>CreateLogisticSponsors</summary>
    public class UpdateLogisticAccomodation : CreateLogisticAccomodation
    {
        public Guid Uid { get; set; }
        
        public UpdateLogisticAccomodation()
        {
        }

        public UpdateLogisticAccomodation(Guid uid, DateTimeOffset? checkInDate, DateTimeOffset? checkOutDate, string additionalInfo, int placeId, List<AttendeePlaceDto> dtos)
        {
            this.Uid = uid;
            this.CheckInDate = checkInDate;
            this.CheckOutDate = checkOutDate;
            this.AdditionalInfo = additionalInfo;
            this.PlaceId = placeId;
            this.Places = dtos;
        }

        #region Private Methods


        #endregion
    }
}