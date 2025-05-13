// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 10-14-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 09-16-2021
// ***********************************************************************
// <copyright file="OrganiationsApiRequest.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;
using System.Net;

namespace PlataformaRio2C.Domain.ApiModels
{
    /// <summary>PlayersApiRequest</summary>
    public class OrganizationsApiRequest : ApiPageBaseRequest
    {
        [JsonProperty("companyName")]
        public string CompanyName { get; set; }

        [JsonProperty("tradeName")]
        public string TradeName { get; set; }

        [JsonProperty("companyNumber")]
        public string CompanyNumber { get; set; }

        [JsonProperty("organizationTypeName")]
        public string OrganizationTypeName { get; set; }

        /// <summary>Gets the company number.</summary>
        /// <returns></returns>
        public string GetCompanyNumber()
        {
            return WebUtility.UrlDecode(this.CompanyNumber);
        }
    }
}