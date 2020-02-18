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
    public class CreateLogisticTransport : BaseCommand
    {
        [Display(Name = "FromPlace", ResourceType = typeof(Labels))]
        public string From { get; set; }

        [Display(Name = "ToPlace", ResourceType = typeof(Labels))]
        public string To { get; set; }

        [Display(Name = "Departure", ResourceType = typeof(Labels))]
        public DateTimeOffset? Departure { get; set; }

        [Display(Name = "Arrival", ResourceType = typeof(Labels))]
        public DateTimeOffset? Arrival { get; set; }

        [Display(Name = "AdditionalInfo", ResourceType = typeof(Labels))]
        public string AdditionalInfo { get; set; }

        /// <summary>Initializes a new instance of the <see cref="CreateLogisticSponsors"/> class.</summary>
        public CreateLogisticTransport(List<LogisticSponsorBaseDto> sponsors, List<LanguageDto> languagesDtos, string userInterfaceLanguage)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="CreateLogisticSponsors"/> class.</summary>
        public CreateLogisticTransport()
        {
        }
        
        #region Private Methods


        #endregion
    }
}