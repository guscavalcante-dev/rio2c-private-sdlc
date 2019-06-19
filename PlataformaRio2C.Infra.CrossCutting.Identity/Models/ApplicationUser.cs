using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PlataformaRio2C.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace PlataformaRio2C.Infra.CrossCutting.Identity.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : User
    {   
        public Guid Uid { get; set; }

        public DateTime CreationDate { get; set; }        
        public bool Active { get; set; }

        public ApplicationUser()
        {
            this.Active = true;
            Uid = Guid.NewGuid();
        }       

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
