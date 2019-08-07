// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 06-28-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-07-2019
// ***********************************************************************
// <copyright file="Rio2CNetworkController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using OfficeOpenXml;
using PlataformaRio2C.Application.Interfaces.Services;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System;
using System.IO;
using System.Web.Mvc;
using MediatR;
using PlataformaRio2C.Infra.CrossCutting.Identity.Service;

namespace PlataformaRio2C.Web.Admin.Controllers
{
    /// <summary>Rio2CNetworkController</summary>
    [Authorize(Roles = "Administrator")]
    public class Rio2CNetworkController : BaseController
    {
        public IMessageAppService _messageAppService { get; set; }

        /// <summary>Initializes a new instance of the <see cref="Rio2CNetworkController"/> class.</summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="identityController">The identity controller.</param>
        /// <param name="messageAppService">The message application service.</param>
        public Rio2CNetworkController(IMediator commandBus, IdentityAutenticationService identityController, IMessageAppService messageAppService)
            : base(commandBus, identityController)
        {
            _messageAppService = messageAppService;
        }

        // GET: Rio2CNetwork
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Download()
        {            
            using (ExcelPackage excelFile = _messageAppService.DownloadNetwork())
            {
                var stream = new MemoryStream();
                excelFile.SaveAs(stream);

                string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                string fileName = string.Format("{0} - {1}", Labels.Rio2cChatTitle, DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss")) + ".xlsx";

                stream.Position = 0;

                return File(stream, contentType, fileName);
            }

            return View();
        }
    }
}