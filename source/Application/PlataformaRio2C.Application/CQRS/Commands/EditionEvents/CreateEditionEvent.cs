// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-06-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 02-16-2020
// ***********************************************************************
// <copyright file="CreateEditionEvent.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.ComponentModel.DataAnnotations;
using Foolproof;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>CreateEditionEvent</summary>
    public class CreateEditionEvent : BaseCommand
    {
        [Display(Name = "Name", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public string Name { get; set; }

        [Display(Name = "StartDate", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public DateTime? StartDate { get; set; }

        [Display(Name = "EndDate", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [GreaterThanOrEqualTo("StartDate", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyGreaterThanProperty")]
        public DateTime? EndDate { get; set; }

        /// <summary>Initializes a new instance of the <see cref="CreateEditionEvent"/> class.</summary>
        /// <param name="editionEventDto">The edition event dto.</param>
        public CreateEditionEvent(EditionEventDto editionEventDto)
        {
            this.Name = editionEventDto?.EditionEvent?.Name;
            this.StartDate = editionEventDto?.EditionEvent?.StartDate.ToUserTimeZone();
            this.EndDate = editionEventDto?.EditionEvent?.EndDate.ToUserTimeZone();
        }

        /// <summary>Initializes a new instance of the <see cref="CreateEditionEvent"/> class.</summary>
        public CreateEditionEvent()
        {
        }
    }
}