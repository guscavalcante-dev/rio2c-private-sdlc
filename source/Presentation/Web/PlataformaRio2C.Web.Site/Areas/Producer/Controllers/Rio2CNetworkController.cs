// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 06-28-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 07-01-2019
// ***********************************************************************
// <copyright file="Rio2CNetworkController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Application.Interfaces.Services;

namespace PlataformaRio2C.Web.Site.Areas.Producer.Controllers
{
    /// <summary>Rio2CNetworkController</summary>
    public class Rio2CNetworkController : Site.Controllers.Rio2CNetworkController
    {
        /// <summary>Initializes a new instance of the <see cref="Rio2CNetworkController"/> class.</summary>
        /// <param name="messageAppService">The message application service.</param>
        public Rio2CNetworkController(IMessageAppService messageAppService)
            :base(messageAppService)
        {            
        }
    }
}