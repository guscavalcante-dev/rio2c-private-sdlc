// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 02-02-2022
//
// Last Modified By : Renan Valentim
// Last Modified On : 02-02-2022
// ***********************************************************************
// <copyright file="CartoonProjectCompanyApiDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>CartoonProjectCompanyApiDto</summary>
    public class CartoonProjectCompanyApiDto
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("tradeName")]
        public string TradeName { get; set; }

        [JsonProperty("document")]
        public string Document { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("zipCode")]
        public string ZipCode { get; set; }

        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonProperty("ReelUrl")]
        public string ReelUrl { get; set; }

        /// <summary>Initializes a new instance of the <see cref="CartoonProjectCompanyApiDto"/> class.</summary>
        public CartoonProjectCompanyApiDto()
        {
        }
    }
}