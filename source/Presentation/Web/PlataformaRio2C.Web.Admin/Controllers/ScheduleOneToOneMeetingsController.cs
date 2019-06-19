using OfficeOpenXml;
using PlataformaRio2C.Application.Interfaces.Services;
using PlataformaRio2C.Application.ViewModels;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using System;
using System.IO;
using System.Web.Mvc;

namespace PlataformaRio2C.Web.Admin.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class ScheduleOneToOneMeetingsController : BaseController
    {
        private readonly INegotiationAppService _negotiationAppService;
        public ScheduleOneToOneMeetingsController(INegotiationAppService negotiationAppService)
        {
            _negotiationAppService = negotiationAppService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Process()
        {
            return View();
        }

        public ActionResult UnscheduledNegotiations()
        {
            return View();
        }

        [Authorize(Users = "projeto.rio2c@marlin.com.br")]
        public ActionResult Test()
        {
            return View();
        }

        public ActionResult RegisterNegotiationManual()
        {
            return View(new ManualNegotiationRegisterAppViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterNegotiationManual(ManualNegotiationRegisterAppViewModel viewModel)
        {
            var result = _negotiationAppService.RegisterNegotiationManual(viewModel);

            if (result.IsValid)
            {
                this.StatusMessage("Negociação agendada com sucesso!", Infra.CrossCutting.Tools.Enums.StatusMessageType.Success);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Erro ao agendar negociação! Verifique o preenchimento dos campos!");

                foreach (var error in result.Errors)
                {
                    var target = error.Target ?? "";
                    ModelState.AddModelError(target, error.Message);
                }
            }

            return View(viewModel);
        }

        public ActionResult SendEmailToPlayers()
        {
            return View();
        }

        public ActionResult SendEmailToProducers()
        {
            return View();
        }

        public ActionResult Report()
        {
            return View();
        }

        public ActionResult ExportExcel(string date, string roomName)
        {
            using (ExcelPackage excelFile = _negotiationAppService.ExportExcel(date, roomName))
            {
                var stream = new MemoryStream();
                excelFile.SaveAs(stream);

                string dateName = date.Replace('/', '-');

                string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                string fileName = string.Format("Relatório agenda - {0} - {1}", dateName, roomName) + ".xlsx";

                stream.Position = 0;

                return File(stream, contentType, fileName);
            }
        }

    }
}