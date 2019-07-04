// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 06-28-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 07-04-2019
// ***********************************************************************
// <copyright file="ProjectController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using OfficeOpenXml;
using PlataformaRio2C.Application.Interfaces.Services;
using PlataformaRio2C.Application.ViewModels;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using System;
using System.IO;
using System.Web.Mvc;
using PlataformaRio2C.Infra.CrossCutting.Identity.Service;

namespace PlataformaRio2C.Web.Admin.Controllers
{
    /// <summary>ProjectController</summary>
    [Authorize(Roles = "Administrator")]
    public class ProjectController : BaseController
    {
        private readonly IProjectAppService _appService;

        /// <summary>Initializes a new instance of the <see cref="ProjectController"/> class.</summary>
        /// <param name="identityController">The identity controller.</param>
        /// <param name="appService">The application service.</param>
        public ProjectController(IdentityAutenticationService identityController, IProjectAppService appService)
            : base(identityController)
        {
            _appService = appService;
        }

        // GET: Event
        public ActionResult Index()
        {
            return View();

            //return View("Paginate");
        }

        public ActionResult ProjectPitching()
        {
            return View();

            //return View("Paginate");
        }

        public ActionResult ProjectPitchingPrint(Guid Uid)
        {
            var viewModel = _appService.GetByDetails(Uid);
            return View(viewModel);
        }

        public ActionResult ProjectPitchingEdit(Guid Uid)
        {
            var viewModel = _appService.GetByEdit(Uid);
            return View(viewModel);
        }

        public ActionResult Edit(Guid Uid)
        {
            var viewModel = _appService.GetByEdit(Uid);
            return View(viewModel);
        }

        public ActionResult Print(Guid Uid)
        {
            var viewModel = _appService.GetByDetails(Uid);
            return View(viewModel);
        }

        public ActionResult PlayerSelection(Guid Uid)
        {
            var viewModel = _appService.GetPlayerSelectionByUidProject(Uid);
            return View(viewModel);
        }

        // POST: Player/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProjectEditAppViewModel viewModel)
        {
            var result = _appService.Update(viewModel);

            if (result.IsValid)
            {
                this.StatusMessage("Projeto atualizado com sucesso!", Infra.CrossCutting.Tools.Enums.StatusMessageType.Success);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Erro ao atualizar cadastro! Verifique o preenchimento dos campos!");

                foreach (var error in result.Errors)
                {
                    var target = error.Target ?? "";
                    ModelState.AddModelError(target, error.Message);
                }
            }

            return View(viewModel);
        }

        // GET: Player/Delete
        public ActionResult Delete(Guid Uid)
        {
            var result = _appService.Delete(Uid);

            if (result.IsValid)
            {
                this.StatusMessage("Projeto apagado com sucesso!", Infra.CrossCutting.Tools.Enums.StatusMessageType.Success);
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    this.StatusMessage(error.Message, Infra.CrossCutting.Tools.Enums.StatusMessageType.Danger);
                }
            }

            return RedirectToAction("Index");
        }


        public ActionResult DeleteProjectPlayer(int id, Guid uid)
        {
            var result = _appService.DeleteProjectPlayer(id);

            if (result.IsValid)
            {
                this.StatusMessage("Seleção de player apagada com sucesso!", Infra.CrossCutting.Tools.Enums.StatusMessageType.Success);
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    this.StatusMessage(error.Message, Infra.CrossCutting.Tools.Enums.StatusMessageType.Danger);
                }
            }

            return RedirectToAction("PlayerSelection", new { uid = uid });
        }

        public ActionResult ResetEvaluation(int id, Guid uid)
        {
            var result = _appService.ResetEvaluation(id);

            if (result.IsValid)
            {
                this.StatusMessage("Avaliação apagada com sucesso!", Infra.CrossCutting.Tools.Enums.StatusMessageType.Success);
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    this.StatusMessage(error.Message, Infra.CrossCutting.Tools.Enums.StatusMessageType.Danger);
                }
            }

            return RedirectToAction("PlayerSelection", new { uid = uid });
        }

        public ActionResult Download()
        {

            using (ExcelPackage excelFile = _appService.DownloadExcelProject())
            {
                var stream = new MemoryStream();
                excelFile.SaveAs(stream);

                string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                string fileName = string.Format("{0} - {1}", "Projetos - ", DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss")) + ".xlsx";

                stream.Position = 0;

                return File(stream, contentType, fileName);
            }
        }

        public ActionResult DownloadExcel()
        {

            using (ExcelPackage excelFile = _appService.DownloadExcel())
            {
                var stream = new MemoryStream();
                excelFile.SaveAs(stream);

                string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                string fileName = string.Format("{0} - {1}", "Projetos ", DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss")) + ".xlsx";

                stream.Position = 0;

                return File(stream, contentType, fileName);
            }
        }

        public ActionResult DownloadPitching()
        {

            using (ExcelPackage excelFile = _appService.DownloadExcelProjectPitching())
            {
                var stream = new MemoryStream();
                excelFile.SaveAs(stream);

                string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                string fileName = string.Format("{0} - {1}", "Projetos - ", DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss")) + ".xlsx";

                stream.Position = 0;

                return File(stream, contentType, fileName);
            }
        }
    }
}