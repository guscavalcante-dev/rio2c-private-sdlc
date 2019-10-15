// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 10-14-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 10-14-2019
// ***********************************************************************
// <copyright file="OrganiationsApiRequest.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;

namespace PlataformaRio2C.Web.Site.Areas.WebApi.Models
{
    /// <summary>PlayersApiRequest</summary>
    public class OrganizationsApiRequest : ApiPageBaseRequest
    {
        [JsonProperty("companyName")]
        public string CompanyName { get; set; }

        [JsonProperty("cradeName")]
        public string TradeName { get; set; }

        [JsonProperty("companyNumber")]
        public string CompanyNumber { get; set; }
    }
}