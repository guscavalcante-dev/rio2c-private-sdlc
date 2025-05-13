// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 03-17-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-17-2020
// ***********************************************************************
// <copyright file="UpdatePlaceMainInformation.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>UpdatePlaceMainInformation</summary>
    public class UpdatePlaceMainInformation : BaseCommand
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

        [Display(Name = "Country", ResourceType = typeof(Labels))]
        public Guid? CountryUid { get; set; }

        public AddressBaseCommand Address { get; set; }

        public List<CountryBaseDto> CountriesBaseDtos { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="UpdatePlaceMainInformation"/> class.</summary>
        /// <param name="placeDto">The place dto.</param>
        /// <param name="countriesBaseDtos">The countries base dtos.</param>
        public UpdatePlaceMainInformation(PlaceDto placeDto, List<CountryBaseDto> countriesBaseDtos)
        {
            this.PlaceUid = placeDto?.Place?.Uid;
            this.Name = placeDto?.Place?.Name;
            this.Type = placeDto?.GetPlaceType();
            this.Website = placeDto?.Place?.Website;
            this.AdditionalInfo = placeDto?.Place?.AdditionalInfo;
            this.UpdateAddress(placeDto);

            this.UpdateModelsAndLists(countriesBaseDtos);
        }

        /// <summary>Initializes a new instance of the <see cref="UpdatePlaceMainInformation"/> class.</summary>
        public UpdatePlaceMainInformation()
        {
        }

        /// <summary>Updates the models and lists.</summary>
        /// <param name="countriesBaseDtos">The countries base dtos.</param>
        public void UpdateModelsAndLists(List<CountryBaseDto> countriesBaseDtos)
        {
            this.CountriesBaseDtos = countriesBaseDtos?
                                        .OrderBy(c => c.Ordering)?
                                        .ThenBy(c => c.DisplayName)?
                                        .ToList();
        }


        #region Private Methods

        /// <summary>Updates the address.</summary>
        /// <param name="placeDto">The place dto.</param>
        private void UpdateAddress(PlaceDto placeDto)
        {
            this.CountryUid = placeDto?.AddressBaseDto?.CountryUid;
            this.Address = new AddressBaseCommand(placeDto?.AddressBaseDto, false);
        }

        #endregion
    }
}