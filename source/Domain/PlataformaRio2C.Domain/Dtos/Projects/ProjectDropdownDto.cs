// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 03-24-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-25-2020
// ***********************************************************************
// <copyright file="ProjectDropdownDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using Newtonsoft.Json;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>ProjectDropdownDto</summary>
    public class ProjectDropdownDto
    {
        [JsonProperty("uid", Order = 100)]
        public Guid Uid { get; set; }

        [JsonProperty("projectTitle", Order = 200)]
        public string ProjectTitle { get; set; }

        [JsonProperty("sellerTradeName", Order = 300)]
        public string SellerTradeName { get; set; }

        [JsonProperty("sellerCompanyName", Order = 300)]
        public string SellerCompanyName { get; set; }

        [JsonProperty("sellerPicture", Order = 400)]
        public string SellerPicture { get; set; }

        [JsonProperty("sellerUid", Order = 500)]
        public Guid SellerUid { get; set; }
    }
}