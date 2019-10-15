// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 10-14-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 10-14-2019
// ***********************************************************************
// <copyright file="OrganizationsApiResponse.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PlataformaRio2C.Web.Site.Areas.WebApi.Models
{
    /// <summary>OrganizationsApiResponse</summary>
    public class OrganizationsApiResponse : ListBaseModel
    {
        public List<OrganizationsApiListItem> Organizations { get; set; }
    }

    /// <summary>OrganizationsApiResponseApiListItem</summary>
    public class OrganizationsApiListItem
    {
        [JsonProperty("uid")]
        public Guid Uid { get; set; }

        [JsonProperty("tradeName")]
        public string TradeName { get; set; }

        [JsonProperty("companyName")]
        public string CompanyName { get; set; }

        [JsonProperty("companyNumber")]
        public string CompanyNumber { get; set; }

        [JsonProperty("picture")]
        public string Picture { get; set; }
    }
}