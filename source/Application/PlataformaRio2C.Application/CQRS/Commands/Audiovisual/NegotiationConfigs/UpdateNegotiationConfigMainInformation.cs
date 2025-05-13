// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 03-05-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 06-26-2021
// ***********************************************************************
// <copyright file="UpdateNegotiationConfigMainInformation.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using System;
using System.ComponentModel.DataAnnotations;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>UpdateNegotiationConfigMainInformation</summary>
    public class UpdateNegotiationConfigMainInformation : BaseCommand
    {
        public Guid NegotiationConfigUid { get; set; }

        [Display(Name = "Date", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public DateTime? Date { get; set; }

        [Display(Name = "StartTime", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public string StartTime { get; set; }

        [Display(Name = "EndTime", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public string EndTime { get; set; }

        [Display(Name = "RoundFirstTurn", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public int? RoundFirstTurn { get; set; }

        [Display(Name = "RoundSecondTurn", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public int? RoundSecondTurn { get; set; }

        [Display(Name = "TimeIntervalBetweenTurn", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public string TimeIntervalBetweenTurn { get; set; }

        [Display(Name = "TimeOfEachRound", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public string TimeOfEachRound { get; set; }

        [Display(Name = "TimeIntervalBetweenRound", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public string TimeIntervalBetweenRound { get; set; }

        public DateTimeOffset? StartDate { get; private set; }
        public DateTimeOffset? EndDate { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="UpdateNegotiationConfigMainInformation"/> class.</summary>
        /// <param name="negotiationConfigDto">The negotiation configuration dto.</param>
        public UpdateNegotiationConfigMainInformation(NegotiationConfigDto negotiationConfigDto)
        {
            this.Date = negotiationConfigDto?.NegotiationConfig?.StartDate.ToBrazilTimeZone().Date;
            this.StartTime = negotiationConfigDto?.NegotiationConfig?.StartDate.ToBrazilTimeZone().ToShortTimeString();
            this.EndTime = negotiationConfigDto?.NegotiationConfig?.EndDate.ToBrazilTimeZone().ToShortTimeString();
            this.RoundFirstTurn = negotiationConfigDto?.NegotiationConfig?.RoundFirstTurn;
            this.RoundSecondTurn = negotiationConfigDto?.NegotiationConfig?.RoundSecondTurn;
            this.TimeIntervalBetweenTurn = negotiationConfigDto?.NegotiationConfig?.TimeIntervalBetweenTurn.ToString(@"hh\:mm");
            this.TimeOfEachRound = negotiationConfigDto?.NegotiationConfig?.TimeOfEachRound.ToString(@"hh\:mm");
            this.TimeIntervalBetweenRound = negotiationConfigDto?.NegotiationConfig?.TimeIntervalBetweenRound.ToString(@"hh\:mm");
        }

        /// <summary>Initializes a new instance of the <see cref="UpdateNegotiationConfigMainInformation"/> class.</summary>
        public UpdateNegotiationConfigMainInformation()
        {
        }
    }
}