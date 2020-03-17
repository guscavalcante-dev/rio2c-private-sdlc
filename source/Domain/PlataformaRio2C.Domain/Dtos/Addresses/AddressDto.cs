// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 03-17-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-17-2020
// ***********************************************************************
// <copyright file="AddressBaseDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>AddressDto</summary>
    public class AddressDto
    {
        public Address Address { get; set; }
        public City City { get; set; }
        public State State { get; set; }
        public Country Country { get; set; }

        /// <summary>Initializes a new instance of the <see cref="AddressDto"/> class.</summary>
        public AddressDto()
        {
        }

        /// <summary>Gets the location.</summary>
        /// <param name="culture">The culture.</param>
        /// <returns></returns>
        public string GetLocation(string culture)
        {
            if (this.Country == null)
            {
                return string.Empty;
            }

            var location = this.Country?.Name?.GetSeparatorTranslation(culture, '|');

            if (this.State != null)
            {
                location += "/" + this.State.Name;
            }

            return location;
        }

        /// <summary>Gets the address.</summary>
        /// <returns></returns>
        public string GetAddress()
        {
            if (this.Address == null)
            {
                return string.Empty;
            }

            var address = this.Address.Address1;

            if (this.City != null)
            {
                if (!string.IsNullOrEmpty(address))
                {
                    address += " - " + this.City.Name;
                }
                else
                {
                    address += this.City.Name;
                }
            }

            if (!string.IsNullOrEmpty(this.Address.ZipCode))
            {
                if (!string.IsNullOrEmpty(address))
                {
                    address += " (" + this.Address.ZipCode + ")";
                }
                else
                {
                    address += this.Address.ZipCode;
                }
            }

            return address;
        }
    }
}