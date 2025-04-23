// Assembly         : PlataformaRio2C.Web.Site
// Author           : Daniel Giese Rodrigues
// Created          : 04-24-2025
//
// Last Modified By : Daniel Giese Rodrigues
// Last Modified On : 04-24-2025
// ***********************************************************************
// <copyright file="AudiovisualApiController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Microsoft.IdentityModel.Tokens;
using PlataformaRio2C.Domain.ApiModels.Auth.Request;
using PlataformaRio2C.Domain.ApiModels.Auth.Response;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.CrossCutting.Identity.Service;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace PlataformaRio2C.Web.Site.Areas.WebApi.Controllers
{
    namespace PlataformaRio2C.Web.Site.Areas.WebApi.Controllers
    {
        [RoutePrefix("api/v1.0/auth")]
        public class AuthApiController : BaseApiController
        {
            private readonly IdentityAutenticationService identityService;
            private readonly IUserRepository userRepo;

            public AuthApiController(IdentityAutenticationService identityService, IUserRepository userRepo)
            {
                this.identityService = identityService;
                this.userRepo = userRepo;
            }

            [HttpPost, Route("login")]
            [AllowAnonymous]
            [SwaggerResponse(System.Net.HttpStatusCode.OK)]
            [SwaggerResponse(System.Net.HttpStatusCode.Unauthorized)]
            public async Task<IHttpActionResult> Login(LoginApiRequest request)
            {
                var result = await identityService.PasswordSignInAsync(request.Username, request.Password, false, false);

                if (result != IdentitySignInStatus.Success)
                    return StatusCode(HttpStatusCode.Unauthorized);


                var user = await userRepo.FindByUserNameAsync(request.Username);

                if (user == null)
                    return StatusCode(HttpStatusCode.Unauthorized);


                var key = Encoding.ASCII.GetBytes(ConfigurationManager.AppSettings["JwtSecret"]);
                var expirationDate = DateTime.UtcNow.AddHours(2);

                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                    new Claim("id", user.Id.ToString()),
                    new Claim("name", user.Name ?? user.UserName),
                    new Claim("email", user.Email ?? "")
                }),
                    Expires = expirationDate,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                var jwt = tokenHandler.WriteToken(token);

                return Ok(new LoginApiResponse
                {
                    Token = jwt,
                    ExpirationDate = expirationDate
                });
            }
        }
    }
}
