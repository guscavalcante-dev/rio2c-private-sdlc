// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 02-02-2022
//
// Last Modified By : Renan Valentim
// Last Modified On : 02-02-2022
// ***********************************************************************
// <copyright file="CartoonProjectCreatorApiDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>CartoonProjectCreatorApiDto</summary>
    public class CartoonProjectCreatorApiDto
    {
        [JsonRequired]
        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonRequired]
        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonRequired]
        [JsonProperty("document")]
        public string Document { get; set; }

        [JsonRequired]
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonRequired]
        [JsonProperty("cellPhone")]
        public string CellPhone { get; set; }

        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonRequired]
        [JsonProperty("miniBio")]
        public string MiniBio { get; set; }

        [JsonRequired]
        [JsonProperty("isResponsible")]
        public bool IsResponsible { get; set; }

        /// <summary>Initializes a new instance of the <see cref="CartoonProjectCreatorApiDto"/> class.</summary>
        public CartoonProjectCreatorApiDto()
        {
        }
    }
}