// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-24-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 10-10-2019
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
        public Guid? CountryUid { get; set; }
        public Guid? StateUid { get; set; }
        public Guid? CityUid { get; set; }
        public string Address1 { get; set; }
        public string AddressZipCode { get; set; }

        /// <summary>Initializes a new instance of the <see cref="AddressBaseDto"/> class.</summary>
        public AddressBaseDto()
        {
        }
    }
}