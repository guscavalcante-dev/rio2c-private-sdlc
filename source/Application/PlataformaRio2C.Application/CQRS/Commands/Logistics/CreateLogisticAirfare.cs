// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Arthur Souza
// Created          : 01-27-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-17-2020
// ***********************************************************************
// <copyright file="CreateLogisticAirfare.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.ComponentModel.DataAnnotations;
using System.Web;
using Foolproof;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Attributes;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>CreateLogisticAirfare</summary>
    public class CreateLogisticAirfare : BaseCommand
    {
        public Guid LogisticsUid { get; set; }
        public Guid LogisticAirfareUid { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [Display(Name = "Type", ResourceType = typeof(Labels))]
        public bool? IsNational { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [Display(Name = "Direction", ResourceType = typeof(Labels))]
        public bool? IsArrival { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [StringLength(100, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        [Display(Name = "FromPlace", ResourceType = typeof(Labels))]
        public string From { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [StringLength(100, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        [NotEqualTo("From", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyDifferentFromProperty")]
        [Display(Name = "ToPlace", ResourceType = typeof(Labels))]
        public string To { get; set; }

        [Display(Name = "DepartureDate", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public string Departure { get; set; }

        [Display(Name = "ArrivalDate", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [NotEqualTo("Departure", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyDifferentFromProperty")]
        public string Arrival { get; set; }
        
        [Display(Name = "ReservationCode", ResourceType = typeof(Labels))]
        [StringLength(20, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string TicketNumber { get; set; }
        
        [Display(Name = "AdditionalInfo", ResourceType = typeof(Labels))]
        [StringLength(1000, MinimumLength = 0, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string AdditionalInfo { get; set; }

        [Display(Name = "AirfareTicket", ResourceType = typeof(Labels))]
        [HttpPostedFileExtensions(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "InvalidPdf", Extensions = "pdf")]
        public HttpPostedFileBase Ticket { get; set; }

        public bool IsTicketFileDeleted { get; set; }

        public DateTimeOffset? TicketUploadDate { get; set; }

        /// <summary>Initializes a new instance of the <see cref="CreateLogisticSponsor"/> class.</summary>
        public CreateLogisticAirfare(Guid logisticsUid)
        {
            this.LogisticsUid = logisticsUid;
            this.IsTicketFileDeleted = false;
        }

        /// <summary>Initializes a new instance of the <see cref="CreateLogisticAirfare"/> class.</summary>
        public CreateLogisticAirfare()
        {
        }
    }
}