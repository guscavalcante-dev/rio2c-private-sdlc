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
    public class UpdateLogisticTransfer : CreateLogisticTransfer
    {
        public Guid Uid { get; set; }
        
        public UpdateLogisticTransfer()
        {
        }

        public UpdateLogisticTransfer(Guid uid, int? fromAttendeePlaceId, int? attendeePlaceId, DateTimeOffset date, string additionalInfo, List<AttendeePlaceDto> places)
        {
            this.Uid = uid;
            this.FromAttendeePlaceId = fromAttendeePlaceId;
            this.ToAttendeePlaceId = attendeePlaceId;
            this.Date = date;
            this.Places = places;
            this.AdditionalInfo = additionalInfo;
        }

        #region Private Methods


        #endregion
    }
}