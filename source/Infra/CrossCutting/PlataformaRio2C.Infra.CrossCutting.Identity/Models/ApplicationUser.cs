// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 02-15-2020
// ***********************************************************************
// <copyright file="ApplicationUser.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Web;

namespace PlataformaRio2C.Infra.CrossCutting.Identity.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : User
    {
        public Guid Uid { get; set; }
        public bool Active { get; set; }
        public bool IsDeleted { get; set; }
        public DateTimeOffset CreateDate { get; set; }
        public DateTimeOffset UpdateDate { get; set; }

        /// <summary>Initializes a new instance of the <see cref="ApplicationUser"/> class.</summary>
        public ApplicationUser()
        {
            this.Active = true;
            Uid = Guid.NewGuid();
        }

        /// <summary>Gets the ip.</summary>
        /// <param name="GetLan">if set to <c>true</c> [get lan].</param>
        /// <returns></returns>
        private string GetIp(bool GetLan = false)
        {
            string visitorIPAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (string.IsNullOrEmpty(visitorIPAddress))
                visitorIPAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

            if (string.IsNullOrEmpty(visitorIPAddress))
                visitorIPAddress = HttpContext.Current.Request.UserHostAddress;

            if (string.IsNullOrEmpty(visitorIPAddress) || visitorIPAddress.Trim() == "::1")
            {
                GetLan = true;
                visitorIPAddress = string.Empty;
            }

            if (GetLan && string.IsNullOrEmpty(visitorIPAddress))
            {
                //This is for Local(LAN) Connected ID Address
                string stringHostName = Dns.GetHostName();
                //Get Ip Host Entry
                IPHostEntry ipHostEntries = Dns.GetHostEntry(stringHostName);
                //Get Ip Address From The Ip Host Entry Address List
                IPAddress[] arrIpAddress = ipHostEntries.AddressList;

                try
                {
                    visitorIPAddress = arrIpAddress[arrIpAddress.Length - 2].ToString();
                }
                catch
                {
                    try
                    {
                        visitorIPAddress = arrIpAddress[0].ToString();
                    }
                    catch
                    {
                        try
                        {
                            arrIpAddress = Dns.GetHostAddresses(stringHostName);
                            visitorIPAddress = arrIpAddress[0].ToString();
                        }
                        catch
                        {
                            visitorIPAddress = "127.0.0.1";
                        }
                    }
                }

            }

            return visitorIPAddress;
        }

        /// <summary>Método para injetar outros Claims ao user identity corrente
        /// Ao sobrescrever usar a codificação base caso queira que email, nome completo e id do usuário estejam nos claims</summary>
        /// <param name="userIdentity"></param>
        protected override void SetClaims(ClaimsIdentity userIdentity)
        {
            var claims = new List<Claim>();

            claims.Add(new Claim(ClaimCodes.Email.ToString(), this.Email));
            claims.Add(new Claim(ClaimCodes.Name.ToString(), this.Name));
            claims.Add(new Claim(ClaimCodes.UserId.ToString(), this.Id.ToString()));

            userIdentity.AddClaims(claims);
        }
    }
}