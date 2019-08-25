// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-23-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-24-2019
// ***********************************************************************
// <copyright file="AddressBaseCommand.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using Foolproof;
using PlataformaRio2C.Infra.CrossCutting.Tools.Attributes;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>AddressBaseCommand</summary>
    public class AddressBaseCommand
    {
        [Display(Name = "Number", ResourceType = typeof(Labels))]
        [RequiredIfNotEmpty("CountryUid", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [StringLength(16, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string AddressNumber { get; set; }
        [Display(Name = "AddressComplement", ResourceType = typeof(Labels))]

        [StringLength(40, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string AddressComplement { get; set; }

        [Display(Name = "StreetName", ResourceType = typeof(Labels))]
        [RequiredIfNotEmpty("CountryUid", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public Guid? StreetUid { get; set; }

        [Display(Name = "StreetName", ResourceType = typeof(Labels))]
        [RequiredIfNotEmpty("CountryUid", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [StringLength(100, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string StreetName { get; set; }

        [Display(Name = "ZipCode", ResourceType = typeof(Labels))]
        [RequiredIfNotEmpty("CountryUid", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [StringLength(10, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string StreetZipCode { get; set; }

        // Neighborhood
        [Display(Name = "Neighborhood", ResourceType = typeof(Labels))]
        [RequiredIfOneNotEmptyAndOtherEmpty("CountryUid", "NeighborhoodName")]
        public Guid? NeighborhoodUid { get; set; }

        [Display(Name = "Neighborhood", ResourceType = typeof(Labels))]
        [RequiredIfOneNotEmptyAndOtherEmpty("CountryUid", "NeighborhoodUid")]
        [StringLength(100, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string NeighborhoodName { get; set; }

        // City
        [Display(Name = "City", ResourceType = typeof(Labels))]
        [RequiredIfOneNotEmptyAndOtherEmpty("CountryUid", "CityName")]
        public Guid? CityUid { get; set; }

        [Display(Name = "City", ResourceType = typeof(Labels))]
        [RequiredIfOneNotEmptyAndOtherEmpty("CountryUid", "CityUid")]
        [StringLength(100, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string CityName { get; set; }

        // State
        [Display(Name = "State", ResourceType = typeof(Labels))]
        [RequiredIfOneNotEmptyAndOtherEmpty("CountryUid", "StateName")]
        public Guid? StateUid { get; set; }

        [Display(Name = "State", ResourceType = typeof(Labels))]
        [RequiredIfOneNotEmptyAndOtherEmpty("CountryUid", "StateUid")]
        [StringLength(100, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string StateName { get; set; }

        // Country
        [Display(Name = "Country", ResourceType = typeof(Labels))]
        public Guid? CountryUid { get; set; }

        public List<CountryBaseDto> CountriesBaseDtos { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="AddressBaseCommand"/> class.</summary>
        /// <param name="addressBaseDto">The address base dto.</param>
        /// <param name="countriesBaseDtos">The countries base dtos.</param>
        public AddressBaseCommand(AddressBaseDto addressBaseDto, List<CountryBaseDto> countriesBaseDtos)
        {
            this.UpdateBaseProperties(addressBaseDto, countriesBaseDtos);
        }

        /// <summary>Initializes a new instance of the <see cref="AddressBaseCommand"/> class.</summary>
        public AddressBaseCommand()
        {
        }

        /// <summary>Updates the base properties.</summary>
        /// <param name="addressBaseDto">The address base dto.</param>
        /// <param name="countriesBaseDtos">The countries base dtos.</param>
        private void UpdateBaseProperties(AddressBaseDto addressBaseDto, List<CountryBaseDto> countriesBaseDtos)
        {
            this.AddressNumber = addressBaseDto?.AddressNumber;
            this.AddressComplement = addressBaseDto?.AddressComplement;
            this.StreetUid = addressBaseDto?.StreetUid;
            this.StreetZipCode = addressBaseDto?.StreetZipCode;
            this.StreetName = addressBaseDto?.StreetName; //TODO: Remove after dropdown implementation
            this.NeighborhoodUid = addressBaseDto?.NeighborhoodUid;
            this.NeighborhoodName = addressBaseDto?.NeighborhoodName; //TODO: Remove after dropdown implementation
            this.CityUid = addressBaseDto?.CityUid;
            this.StateUid = addressBaseDto?.StateUid;
            this.CountryUid = addressBaseDto?.CountryUid;
            this.UpdateDropdownProperties(countriesBaseDtos);
        }

        /// <summary>Updates the dropdown properties.</summary>
        /// <param name="countriesBaseDtos">The countries base dtos.</param>
        public void UpdateDropdownProperties(List<CountryBaseDto> countriesBaseDtos)
        {
            this.CountriesBaseDtos = countriesBaseDtos;
        }
    }
}