// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 10-14-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-19-2019
// ***********************************************************************
// <copyright file="OrganizationsApiResponse.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PlataformaRio2C.Domain.ApiModels
{
    /// <summary>OrganizationsApiResponse</summary>
    public class OrganizationsApiResponse : ListBaseModel
    {
        public List<OrganizationsApiListItem> Organizations { get; set; }
    }

    /// <summary>OrganizationsApiListItem</summary>
    public class OrganizationsApiListItem : OrganizationBaseApiResponse
    {
        [JsonProperty("companyNumber", Order = 301)]
        public string CompanyNumber { get; set; }
    }
}