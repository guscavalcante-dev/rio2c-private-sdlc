// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 03-17-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-20-2020
// ***********************************************************************
// <copyright file="PlaceDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>PlaceDto</summary>
    public class PlaceDto
    {
        public Place Place { get; set; }
        public AddressBaseDto AddressBaseDto { get; set; }
        public AddressDto AddressDto { get; set; }

        /// <summary>Initializes a new instance of the <see cref="PlaceDto"/> class.</summary>
        public PlaceDto()
        {
        }

        /// <summary>Gets the type of the place.</summary>
        /// <returns></returns>
        public string GetPlaceType()
        {
            if (this.Place.IsHotel)
            {
                return "Hotel";
            }

            if (this.Place.IsAirport)
            {
                return "Airport";
            }

            return "Others";
        }

        /// <summary>Determines whether this instance has information.</summary>
        /// <returns>
        ///   <c>true</c> if this instance has information; otherwise, <c>false</c>.</returns>
        public bool HasInformation()
        {
            return !string.IsNullOrEmpty(this.Place?.Website)
                   || !string.IsNullOrEmpty(this.Place?.AdditionalInfo)
                   || this.AddressDto != null
                   || this.AddressBaseDto != null;
        }
    }
}