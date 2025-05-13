// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 15-04-2024
//
// Last Modified By : Renan Valentim
// Last Modified On : 15-04-2024
// ***********************************************************************
// <copyright file="AvailabilityBaseCommand.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Foolproof;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System;
using System.ComponentModel.DataAnnotations;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>AvailabilityBaseCommand</summary>
    public class AvailabilityBaseCommand : BaseCommand
    {
        [Display(Name = "Participant", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public Guid? AttendeeCollaboratorUid { get; set; }

        [Display(Name = nameof(Labels.ArrivalDate), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public DateTimeOffset? AvailabilityBeginDate { get; set; }

        [Display(Name = nameof(Labels.DepartureDate), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [GreaterThanOrEqualTo(nameof(AvailabilityBeginDate), ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyGreaterThanProperty")]
        public DateTimeOffset? AvailabilityEndDate { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AvailabilityBaseCommand" /> class.
        /// </summary>
        public AvailabilityBaseCommand()
        {
        }
    }
}