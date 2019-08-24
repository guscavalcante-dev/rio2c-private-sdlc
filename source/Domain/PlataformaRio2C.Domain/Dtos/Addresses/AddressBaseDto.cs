// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-24-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-24-2019
// ***********************************************************************
// <copyright file="AddressBaseDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>AddressBaseDto</summary>
    public class AddressBaseDto
    {
        public int? Id { get; set; }
        public Guid? Uid { get; set; }
        public string AddressNumber { get; set; }
        public string AddressComplement { get; set; }
        public Guid? StreetUid { get; set; }
        public string StreetName { get; set; } //TODO: remove after dropdown implementation
        public string StreetZipCode { get; set; }
        public Guid? NeighborhoodUid { get; set; }
        public string NeighborhoodName { get; set; } //TODO: remove after dropdown implementation
        public Guid? CityUid { get; set; }
        public Guid? StateUid { get; set; }
        public Guid? CountryUid { get; set; }

        /// <summary>Initializes a new instance of the <see cref="AddressBaseDto"/> class.</summary>
        public AddressBaseDto()
        {
        }
    }
}