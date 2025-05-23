﻿// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Arthur Souza
// Created          : 01-27-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-13-2020
// ***********************************************************************
// <copyright file="CreateLogisticAccommodation.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Foolproof;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>CreateLogisticAccommodation</summary>
    public class CreateLogisticAccommodation : BaseCommand
    {
        public Guid LogisticsUid { get; set; }

        [Display(Name = "Hotel", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public int? PlaceId { get; set; }

        [Display(Name = "CheckInDate", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public string CheckInDate { get; set; }

        [Display(Name = "CheckOutDate", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [NotEqualTo("CheckInDate", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyDifferentFromProperty")]
        public string CheckOutDate { get; set; }

        [Display(Name = "AdditionalInfo", ResourceType = typeof(Labels))]
        [StringLength(1000, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string AdditionalInfo { get; set; }

        public List<AttendeePlaceDropdownDto> Places { get; set; }

        /// <summary>Initializes a new instance of the <see cref="CreateLogisticAccommodation"/> class.</summary>
        /// <param name="logisticsUid">The logistics uid.</param>
        /// <param name="attendeePlacesDtos">The attendee places dtos.</param>
        public CreateLogisticAccommodation(Guid logisticsUid, List<AttendeePlaceDropdownDto> attendeePlacesDtos)
        {
            this.LogisticsUid = logisticsUid;
            this.UpdateLists(attendeePlacesDtos);
        }

        /// <summary>Initializes a new instance of the <see cref="CreateLogisticAccommodation"/> class.</summary>
        public CreateLogisticAccommodation()
        {
        }

        /// <summary>Updates the lists.</summary>
        /// <param name="attendeePlaceDtos">The attendee place dtos.</param>
        public void UpdateLists(List<AttendeePlaceDropdownDto> attendeePlaceDtos)
        {
            this.Places = attendeePlaceDtos;
        }
    }
}