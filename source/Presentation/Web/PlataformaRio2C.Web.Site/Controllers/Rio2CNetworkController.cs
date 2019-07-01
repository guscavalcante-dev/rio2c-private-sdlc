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
using Microsoft.AspNet.Identity;
using OfficeOpenXml;
using PlataformaRio2C.Application.Interfaces.Services;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System;
using System.IO;
using System.Web.Mvc;

namespace PlataformaRio2C.Web.Site.Controllers
{
    /// <summary>Rio2CNetworkController</summary>
    //[TermFilter(Order = 2)]
    [Authorize(Order = 1, Roles = "Player,Producer")]
    public class Rio2CNetworkController : BaseController
    {
        public IMessageAppService _messageAppService { get; set; }

        /// <summary>Initializes a new instance of the <see cref="Rio2CNetworkController"/> class.</summary>
        /// <param name="messageAppService">The message application service.</param>
        public Rio2CNetworkController(IMessageAppService messageAppService)
        {
            _messageAppService = messageAppService;
        }

        // GET: Message
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Download()
        {
            int userId = User.Identity.GetUserId<int>();
            using (ExcelPackage excelFile = _messageAppService.DownloadNetwork(userId))
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