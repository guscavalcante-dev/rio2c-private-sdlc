using OfficeOpenXml;
using PlataformaRio2C.Application.Interfaces.Services;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlataformaRio2C.Web.Admin.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class Rio2CNetworkController : BaseController
    {
        public IMessageAppService _messageAppService { get; set; }

        public Rio2CNetworkController(IMessageAppService messageAppService)
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