using Microsoft.AspNet.Identity;
using OfficeOpenXml;
using PlataformaRio2C.Application.Interfaces.Services;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlataformaRio2C.Web.Site.Controllers
{
    [TermFilter(Order = 2)]
    [Authorize(Order = 1, Roles = "Player,Producer")]
    public class Rio2CNetworkController : BaseController
    {
        public IMessageAppService _messageAppService { get; set; }

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