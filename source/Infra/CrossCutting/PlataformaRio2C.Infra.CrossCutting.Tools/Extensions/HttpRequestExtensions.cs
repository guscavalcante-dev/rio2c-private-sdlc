// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.CrossCutting.Tools
// Author           : Rafael Dantas Ruiz
// Created          : 07-12-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 07-12-2019
// ***********************************************************************
// <copyright file="HttpRequestExtension.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Linq;
using System.Web;

namespace PlataformaRio2C.Infra.CrossCutting.Tools.Extensions
{
    /// <summary>HttpRequestExtension</summary>
    public static class HttpRequestExtension
    {
        /// <summary>Gets the ip address.</summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public static string GetIpAddress(this HttpRequest request)
        {
            string ip;
            try
            {
                ip = request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (!string.IsNullOrEmpty(ip))
                {
                    if (ip.Contains(","))
                    {
                        ip = ip.Split(',').Last().Trim();
                    }
                }
                else
                {
                    ip = request.UserHostAddress;
                }
            }
            catch
            {
                ip = request.UserHostAddress;
            }

            return ip;
        }
    }
}

