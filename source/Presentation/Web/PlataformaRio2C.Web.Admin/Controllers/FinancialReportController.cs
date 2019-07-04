// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 06-28-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 07-04-2019
// ***********************************************************************
// <copyright file="FinancialReportController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Microsoft.AspNet.Identity;
using OfficeOpenXml;
using PlataformaRio2C.Application.Interfaces.Services;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Web.Mvc;
using PlataformaRio2C.Infra.CrossCutting.Identity.Service;

namespace PlataformaRio2C.Web.Admin.Controllers
{
    /// <summary>FinancialReportController</summary>
    [Authorize(Roles = "Administrator")]
    public class FinancialReportController : BaseController
    {
        private readonly IApiSymplaAppService _appService;

        /// <summary>Initializes a new instance of the <see cref="FinancialReportController"/> class.</summary>
        /// <param name="identityController">The identity controller.</param>
        /// <param name="appService">The application service.</param>
        public FinancialReportController(IdentityAutenticationService identityController, IApiSymplaAppService appService)
            : base(identityController)
        {
            _appService = appService;
        }

        // GET: FinancialReport
        public async Task<ActionResult> SalesByCategory()
        {
           
            string userEmail = User.Identity.GetUserName();

            if (!(await _appService.ConfirmUserAllowedFinancialReport(userEmail)))
            {
                this.StatusMessage("Acesso negado!", Infra.CrossCutting.Tools.Enums.StatusMessageType.Danger);
                return RedirectToAction("Index", "Dashboard");
            }

            return View();
        }

        public async Task<ActionResult> ExportReportSalesByCategory()
        {
           
            string userEmail = User.Identity.GetUserName();

            if (!(await _appService.ConfirmUserAllowedFinancialReport(userEmail)))
            {
                this.StatusMessage("Acesso negado!", Infra.CrossCutting.Tools.Enums.StatusMessageType.Danger);
                return RedirectToAction("Index", "Dashboard");
            }

            var result = _appService.ExportReportSalesByCategory().Result;

            if (result != null)
            {
                using (ExcelPackage excelFile = result)
                {
                    var stream = new MemoryStream();
                    excelFile.SaveAs(stream);
                    string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    string fileName = string.Format("Rio2C - Relatório vendas por categoria - {0}.xlsx", DateTime.Now.ToString("dd/MM/yyyy HH:mm"));

                    stream.Position = 0;

                    return File(stream, contentType, fileName);
                }
            }

            this.StatusMessage("Não foi possível fazer download da planilha", Infra.CrossCutting.Tools.Enums.StatusMessageType.Danger);
            return RedirectToAction("SalesByCategory");
        }

        
        public async Task<ActionResult> ExportReportSales()
        {
           
            string userEmail = User.Identity.GetUserName();

            if (!(await _appService.ConfirmUserAllowedFinancialReport(userEmail)))
            {
                this.StatusMessage("Acesso negado!", Infra.CrossCutting.Tools.Enums.StatusMessageType.Danger);
                return RedirectToAction("Index", "Dashboard");
            }

            var result = _appService.ExportReportSales().Result;

            if (result != null)
            {
                using (ExcelPackage excelFile = result)
                {
                    var stream = new MemoryStream();
                    excelFile.SaveAs(stream);
                    string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    string fileName = string.Format("Rio2C - Relatório todos os Dados Sympla - {0}.xlsx", DateTime.Now.ToString("dd/MM/yyyy HH:mm"));

                    stream.Position = 0;

                    return File(stream, contentType, fileName);
                }
            }

            this.StatusMessage("Não foi possível fazer download da planilha", Infra.CrossCutting.Tools.Enums.StatusMessageType.Danger);
            return RedirectToAction("SalesByCategory");
        }

        public async Task<ActionResult> SalesByRegion()
        {
           
            string userEmail = User.Identity.GetUserName();

            if (!(await _appService.ConfirmUserAllowedFinancialReport(userEmail)))
            {
                this.StatusMessage("Acesso negado!", Infra.CrossCutting.Tools.Enums.StatusMessageType.Danger);
                return RedirectToAction("Index", "Dashboard");
            }

            return View();
        }

        public async Task<ActionResult> SalesByPeriod()
        {
           
            string userEmail = User.Identity.GetUserName();

            if (!(await _appService.ConfirmUserAllowedFinancialReport(userEmail)))
            {
                this.StatusMessage("Acesso negado!", Infra.CrossCutting.Tools.Enums.StatusMessageType.Danger);
                return RedirectToAction("Index", "Dashboard");
            }

            return View();
        }

        public async Task<ActionResult> SalesByTypeOfPayment()
        {
           
            string userEmail = User.Identity.GetUserName();

            if (!(await _appService.ConfirmUserAllowedFinancialReport(userEmail)))
            {
                this.StatusMessage("Acesso negado!", Infra.CrossCutting.Tools.Enums.StatusMessageType.Danger);
                return RedirectToAction("Index", "Dashboard");
            }

            return View();
        }


        public async Task<ActionResult> Courtesies()
        {
           
            string userEmail = User.Identity.GetUserName();

            if (!(await _appService.ConfirmUserAllowedFinancialReport(userEmail)))
            {
                this.StatusMessage("Acesso negado!", Infra.CrossCutting.Tools.Enums.StatusMessageType.Danger);
                return RedirectToAction("Index", "Dashboard");
            }

            return View();
        }


        public async Task<ActionResult> ParticipantsByCategory()
        {
           
            string userEmail = User.Identity.GetUserName();

            if (!(await _appService.ConfirmUserAllowedFinancialReport(userEmail)))
            {
                this.StatusMessage("Acesso negado!", Infra.CrossCutting.Tools.Enums.StatusMessageType.Danger);
                return RedirectToAction("Index", "Dashboard");
            }

            return View();
        }


    }
}