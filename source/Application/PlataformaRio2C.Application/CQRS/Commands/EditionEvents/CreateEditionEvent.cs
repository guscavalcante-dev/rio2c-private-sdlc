﻿// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-06-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 06-26-2021
// ***********************************************************************
// <copyright file="CreateEditionEvent.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Foolproof;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using System;
using System.ComponentModel.DataAnnotations;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>CreateEditionEvent</summary>
    public class CreateEditionEvent : BaseCommand
    {
        [Display(Name = nameof(Name), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public string Name { get; set; }

        [Display(Name = nameof(StartDate), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public DateTime? StartDate { get; set; }

        [Display(Name = (nameof(EndDate)), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [GreaterThanOrEqualTo(nameof(StartDate), ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyGreaterThanProperty")]
        public DateTime? EndDate { get; set; }

        /// <summary>Initializes a new instance of the <see cref="CreateEditionEvent"/> class.</summary>
        /// <param name="editionEventDto">The edition event dto.</param>
        public CreateEditionEvent(EditionEventDto editionEventDto)
        {
            this.Name = editionEventDto?.EditionEvent?.Name;
            this.StartDate = editionEventDto?.EditionEvent?.StartDate.ToBrazilTimeZone();
            this.EndDate = editionEventDto?.EditionEvent?.EndDate.ToBrazilTimeZone();
        }

        /// <summary>Initializes a new instance of the <see cref="CreateEditionEvent"/> class.</summary>
        public CreateEditionEvent()
        {
        }
    }
}