// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Arthur Souza
// Created          : 01-27-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-17-2020
// ***********************************************************************
// <copyright file="UpdateLogisticSponsorMainInformation.cs" company="Softo">
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
    /// <summary>UpdateLogisticSponsorMainInformation</summary>
    public class UpdateLogisticSponsorMainInformation : BaseCommand
    {
        public Guid? LogisticSponsorUid { get; set; }
        public List<LogisticSponsorsNameBaseCommand> Names { get; set; }

        [Display(Name = "Type", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public bool? IsOther { get; set; }

        [Display(Name = "IsAirfareTicketRequired", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public bool? IsAirfareTicketRequired { get; set; }

        [Display(Name = "OpenOthersDropdown", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public bool? IsOtherRequired { get; set; }

        [Display(Name = "DisplayedInitialLogisticList", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public bool? IsLogisticListDisplayed { get; set; }

        /// <summary>Initializes a new instance of the <see cref="UpdateLogisticSponsorMainInformation"/> class.</summary>
        /// <param name="logisticSponsorDto">The logistic sponsor dto.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        public UpdateLogisticSponsorMainInformation(LogisticSponsorDto logisticSponsorDto, List<LanguageDto> languagesDtos)
        {
            this.LogisticSponsorUid = logisticSponsorDto?.LogisticSponsor?.Uid;
            this.IsOther = logisticSponsorDto?.AttendeeLogisticSponsor?.IsOther;
            this.IsAirfareTicketRequired = logisticSponsorDto?.LogisticSponsor?.IsAirfareTicketRequired;
            this.IsOtherRequired = logisticSponsorDto?.LogisticSponsor?.IsOtherRequired;
            this.IsLogisticListDisplayed = logisticSponsorDto?.AttendeeLogisticSponsor?.IsLogisticListDisplayed;

            this.UpdateNames(logisticSponsorDto, languagesDtos);
        }

        /// <summary>Initializes a new instance of the <see cref="UpdateLogisticSponsorMainInformation"/> class.</summary>
        public UpdateLogisticSponsorMainInformation()
        {
        }

        #region Private Methods

        /// <summary>Updates the names.</summary>
        /// <param name="logisticSponsorDto">The logistic sponsor dto.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        private void UpdateNames(LogisticSponsorDto logisticSponsorDto, List<LanguageDto> languagesDtos)
        {
            this.Names = new List<LogisticSponsorsNameBaseCommand>();
            foreach (var languageDto in languagesDtos)
            {
                this.Names.Add(new LogisticSponsorsNameBaseCommand(logisticSponsorDto, languageDto));
            }
        }

        #endregion
    }
}