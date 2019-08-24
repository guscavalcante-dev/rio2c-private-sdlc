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

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>AddressBaseCommand</summary>
    public class AddressBaseCommand
    {
        //TODO: Missing data annotations on AddressBaseCommand

        [Display(Name = "Number", ResourceType = typeof(Labels))]
        public string AddressNumber { get; set; }
        [Display(Name = "AddressComplement", ResourceType = typeof(Labels))]
        public string AddressComplement { get; set; }

        [Display(Name = "StreetName", ResourceType = typeof(Labels))]
        public Guid? StreetUid { get; set; }
        public string StreetName { get; set; }
        [Display(Name = "ZipCode", ResourceType = typeof(Labels))]
        public string StreetZipCode { get; set; }

        [Display(Name = "Neighborhood", ResourceType = typeof(Labels))]
        public Guid? NeighborhoodUid { get; set; }
        public string NeighborhoodName { get; set; }

        [Display(Name = "City", ResourceType = typeof(Labels))]
        public Guid? CityUid { get; set; }
        public string CityName { get; set; }

        [Display(Name = "State", ResourceType = typeof(Labels))]
        public Guid? StateUid { get; set; }
        public string StateName { get; set; }

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