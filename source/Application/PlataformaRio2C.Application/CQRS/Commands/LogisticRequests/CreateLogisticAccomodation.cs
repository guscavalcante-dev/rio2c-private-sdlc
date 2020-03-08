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
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>CreateLogisticSponsors</summary>
    public class CreateLogisticAccomodation : BaseCommand
    {
        public Guid LogisticsUid { get; set; }

        [Display(Name = "Place", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public int? PlaceId { get; set; }

        [Display(Name = "CheckInDate", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public DateTimeOffset? CheckInDate { get; set; }

        [Display(Name = "CheckOutDate", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public DateTimeOffset? CheckOutDate { get; set; }

        [Display(Name = "AdditionalInfo", ResourceType = typeof(Labels))]
        public string AdditionalInfo { get; set; }

        public List<AttendeePlaceDto> Places { get; set; }

        /// <summary>Initializes a new instance of the <see cref="CreateLogisticSponsors"/> class.</summary>
        /// <summary>Initializes a new instance of the <see cref="CreateLogisticSponsors"/> class.</summary>
        public CreateLogisticAccomodation(Guid logisticsUid, List<AttendeePlaceDto> attendeePlaces)
        {
            this.LogisticsUid = logisticsUid;
            this.Places = attendeePlaces;
        }

        public CreateLogisticAccomodation()
        {
        }

        #region Private Methods


        #endregion
        
        public void UpdateLists(List<AttendeePlaceDto> attendeePlaceDtos)
        {
            this.Places = attendeePlaceDtos;
        }
    }
}