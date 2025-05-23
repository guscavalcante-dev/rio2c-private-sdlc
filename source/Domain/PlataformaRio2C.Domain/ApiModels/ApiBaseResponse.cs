﻿// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 09-25-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 01-29-2023
// ***********************************************************************
// <copyright file="ApiBaseResponse.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;

namespace PlataformaRio2C.Domain.ApiModels
{
    /// <summary>ApiBaseResponse</summary>
    public class ApiBaseResponse
    {
        [JsonProperty("status", Order = 10000)]
        public string Status { get; set; }

        [JsonProperty("message", Order = 10001)]
        public string Message { get; set; }

        [JsonProperty("error", Order = 10002)]
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