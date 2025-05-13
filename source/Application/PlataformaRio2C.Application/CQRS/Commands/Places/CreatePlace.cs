// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 03-17-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-17-2020
// ***********************************************************************
// <copyright file="CreatePlace.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System;
using System.ComponentModel.DataAnnotations;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>CreatePlace</summary>
    public class CreatePlace : BaseCommand
    {
        public Guid? PlaceUid { get; set; }

        [Display(Name = "Name", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [StringLength(100, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string Name { get; set; }

        [Display(Name = "Type", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public string Type { get; set; }

        [Display(Name = "Website", ResourceType = typeof(Labels))]
        [StringLength(300, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string Website { get; set; }

        [Display(Name = "AdditionalInfo", ResourceType = typeof(Labels))]
        [StringLength(1000, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string AdditionalInfo { get; set; }

        /// <summary>Initializes a new instance of the <see cref="CreatePlace"/> class.</summary>
        public CreatePlace(PlaceDto placeDto)
        {
            this.PlaceUid = placeDto?.Place?.Uid;
            this.Name = placeDto?.Place?.Name;
            this.Type = placeDto?.GetPlaceType();
            this.Website = placeDto?.Place?.Website;
            this.AdditionalInfo = placeDto?.Place?.AdditionalInfo;
        }

        /// <summary>Initializes a new instance of the <see cref="CreatePlace"/> class.</summary>
        public CreatePlace()
        {
        }
    }
}