// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-23-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-17-2019
// ***********************************************************************
// <copyright file="AddressBaseCommand.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
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
        // State
        [Display(Name = "State", ResourceType = typeof(Labels))]
        [RequiredIfOneWithValueAndOtherEmptyAttribute("IsRequired", "True", "StateName")]
        //[RequiredIfOneNotEmptyAndOtherEmpty("CountryUid", "StateName")]
        public Guid? StateUid { get; set; }

        [Display(Name = "State", ResourceType = typeof(Labels))]
        [RequiredIfOneWithValueAndOtherEmptyAttribute("IsRequired", "True", "StateUid")]
        //[RequiredIfOneNotEmptyAndOtherEmpty("CountryUid", "StateUid")]
        [StringLength(100, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string StateName { get; set; }

        // City
        [Display(Name = "City", ResourceType = typeof(Labels))]
        [RequiredIfOneWithValueAndOtherEmptyAttribute("IsRequired", "True", "CityName")]
        //[RequiredIfOneNotEmptyAndOtherEmpty("CountryUid", "CityName")]
        public Guid? CityUid { get; set; }

        [Display(Name = "City", ResourceType = typeof(Labels))]
        [RequiredIfOneWithValueAndOtherEmptyAttribute("IsRequired", "True", "CityUid")]
        //[RequiredIfOneNotEmptyAndOtherEmpty("CountryUid", "CityUid")]
        [StringLength(100, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string CityName { get; set; }

        // Address
        [Display(Name = "Address1", ResourceType = typeof(Labels))]
        [RequiredIf("IsRequired", "True", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [StringLength(200, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string Address1 { get; set; }

        [Display(Name = "ZipCode", ResourceType = typeof(Labels))]
        [RequiredIf("IsRequired", "True", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [StringLength(10, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string AddressZipCode { get; set; }

        public bool IsRequired { get; set; }

        /// <summary>Initializes a new instance of the <see cref="AddressBaseCommand"/> class.</summary>
        /// <param name="addressBaseDto">The address base dto.</param>
        /// <param name="isRequired">if set to <c>true</c> [is required].</param>
        public AddressBaseCommand(AddressBaseDto addressBaseDto, bool isRequired)
        {
            this.UpdateBaseProperties(addressBaseDto, isRequired);
        }

        /// <summary>Initializes a new instance of the <see cref="AddressBaseCommand"/> class.</summary>
        public AddressBaseCommand()
        {
        }

        /// <summary>Updates the base properties.</summary>
        /// <param name="addressBaseDto">The address base dto.</param>
        /// <param name="isRequired">if set to <c>true</c> [is required].</param>
        private void UpdateBaseProperties(AddressBaseDto addressBaseDto, bool isRequired)
        {
            this.StateUid = addressBaseDto?.StateUid;
            this.CityUid = addressBaseDto?.CityUid;
            this.Address1 = addressBaseDto?.Address1;
            this.AddressZipCode = addressBaseDto?.AddressZipCode;
            this.IsRequired = isRequired;
        }
    }
}