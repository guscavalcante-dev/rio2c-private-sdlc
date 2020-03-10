// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Arthur Souza
// Created          : 01-27-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-10-2020
// ***********************************************************************
// <copyright file="CreateLogisticAccomodation.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>CreateLogisticAccomodation</summary>
    public class CreateLogisticAccomodation : BaseCommand
    {
        public Guid LogisticsUid { get; set; }

        [Display(Name = "Place", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public int? PlaceId { get; set; }

        [Display(Name = "CheckInDate", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public DateTime? CheckInDate { get; set; }

        [Display(Name = "CheckOutDate", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public DateTime? CheckOutDate { get; set; }

        [Display(Name = "AdditionalInfo", ResourceType = typeof(Labels))]
        public string AdditionalInfo { get; set; }

        public List<AttendeePlaceDto> Places { get; set; }

        /// <summary>Initializes a new instance of the <see cref="CreateLogisticAccomodation"/> class.</summary>
        /// <param name="logisticsUid">The logistics uid.</param>
        /// <param name="attendeePlaces">The attendee places.</param>
        public CreateLogisticAccomodation(Guid logisticsUid, List<AttendeePlaceDto> attendeePlaces)
        {
            this.LogisticsUid = logisticsUid;
            this.Places = attendeePlaces;
        }

        /// <summary>Initializes a new instance of the <see cref="CreateLogisticAccomodation"/> class.</summary>
        public CreateLogisticAccomodation()
        {
        }

        /// <summary>Updates the lists.</summary>
        /// <param name="attendeePlaceDtos">The attendee place dtos.</param>
        public void UpdateLists(List<AttendeePlaceDto> attendeePlaceDtos)
        {
            this.Places = attendeePlaceDtos;
        }
    }
}