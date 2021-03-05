// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 03-03-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 03-03-2021
// ***********************************************************************
// <copyright file="CreateEdition.cs" company="Softo">
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
    /// <summary>CreateEdition</summary>
    public class CreateEdition : BaseCommand
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

        /// <summary>Initializes a new instance of the <see cref="CreateEdition"/> class.</summary>
        /// <param name="editionDto">The edition event dto.</param>
        public CreateEdition(EditionDto editionDto)
        {
            this.Name = editionDto?.Name;
            this.StartDate = editionDto?.StartDate.ToUserTimeZone();
            this.EndDate = editionDto?.EndDate.ToUserTimeZone();
        }

        /// <summary>Initializes a new instance of the <see cref="CreateEdition"/> class.</summary>
        public CreateEdition()
        {
        }
    }
}