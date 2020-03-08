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
using System.IO;
using System.Linq;
using System.Web;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>CreateLogisticSponsors</summary>
    public class CreateLogisticAirfare : BaseCommand
    {
        public Guid LogisticsUid { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [Display(Name = "FromPlace", ResourceType = typeof(Labels))]
        public string From { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [Display(Name = "ToPlace", ResourceType = typeof(Labels))]
        public string To { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [Display(Name = "IsNational", ResourceType = typeof(Labels))]
        public bool? IsNational { get; set; }

        [Display(Name = "Departure", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public DateTimeOffset? Departure { get; set; }

        [Display(Name = "Arrival", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public DateTimeOffset? Arrival { get; set; }
        
        [Display(Name = "TicketNumber", ResourceType = typeof(Labels))]
        public string TicketNumber { get; set; }
        
        [Display(Name = "AdditionalInfo", ResourceType = typeof(Labels))]
        [StringLength(1000, MinimumLength = 0, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string AdditionalInfo { get; set; }

        [Display(Name = "AirfareTicket", ResourceType = typeof(Labels))]
        public HttpPostedFile Ticket { get; set; }

        /// <summary>Initializes a new instance of the <see cref="CreateLogisticSponsors"/> class.</summary>
        public CreateLogisticAirfare(Guid logisticsUid)
        {
            this.LogisticsUid = logisticsUid;
        }

        public CreateLogisticAirfare()
        {
        }

        #region Private Methods


        #endregion
    }
}