// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Arthur Souza
// Created          : 01-27-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-10-2020
// ***********************************************************************
// <copyright file="CreateLogisticTransfer.cs" company="Softo">
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
    /// <summary>CreateLogisticTransfer</summary>
    public class CreateLogisticTransfer : BaseCommand
    {
        public Guid LogisticsUid { get; set; }

        [Display(Name = "FromPlace", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public int? FromAttendeePlaceId { get; set; }

        [Display(Name = "ToPlace", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public int? ToAttendeePlaceId { get; set; }

        [Display(Name = "Departure", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public DateTime? Date { get; set; }
        
        [Display(Name = "LogisticTransferStatus", ResourceType = typeof(Labels))]
        public int? LogisticTransferStatusId { get; set; }

        [Display(Name = "AdditionalInfo", ResourceType = typeof(Labels))]
        public string AdditionalInfo { get; set; }

        public List<AttendeePlaceDto> Places { get; set; }

        /// <summary>Initializes a new instance of the <see cref="CreateLogisticTransfer"/> class.</summary>
        /// <param name="logisticsUid">The logistics uid.</param>
        /// <param name="places">The places.</param>
        public CreateLogisticTransfer(Guid logisticsUid, List<AttendeePlaceDto> places)
        {
            this.LogisticsUid = logisticsUid;
            this.Places = places;
        }

        /// <summary>Initializes a new instance of the <see cref="CreateLogisticTransfer"/> class.</summary>
        public CreateLogisticTransfer()
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