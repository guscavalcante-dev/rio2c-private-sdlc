// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 09-25-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-25-2019
// ***********************************************************************
// <copyright file="ApiBaseResponse.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;

namespace PlataformaRio2C.Web.Site.Areas.WebApi.Models
{
    /// <summary>ApiBaseResponse</summary>
    public class ApiBaseResponse
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("error")]
        public ApiError Error { get; set; }
    }

    /// <summary>ApiStatus</summary>
    public class ApiStatus
    {
        public const string Success = "success";
        public const string Error = "error";
    }

    /// <summary>ApiError</summary>
    public class ApiError
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}