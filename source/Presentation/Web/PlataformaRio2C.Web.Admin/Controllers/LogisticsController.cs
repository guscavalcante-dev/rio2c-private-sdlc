// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 06-28-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-07-2019
// ***********************************************************************
// <copyright file="LogisticsController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Application.Interfaces.Services;
using PlataformaRio2C.Application.ViewModels;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using System;
using System.IO;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using MediatR;
using PlataformaRio2C.Infra.CrossCutting.Identity.Service;

namespace PlataformaRio2C.Web.Admin.Controllers
{
    /// <summary>LogisticsController</summary>
    [Authorize(Roles = "Administrator")]
    public class LogisticsController : BaseController
    {
        private readonly ILogisticsAppService _appService;

        /// <summary>Initializes a new instance of the <see cref="LogisticsController"/> class.</summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="identityController">The identity controller.</param>
        /// <param name="appService">The application service.</param>
        public LogisticsController(IMediator commandBus, IdentityAutenticationService identityController, ILogisticsAppService appService)
            : base(commandBus, identityController)
        {
            _appService = appService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            var viewModel = _appService.GetEditViewModel();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LogisticsEditAppViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                /*Guardar o nome do arquivo na viewModel*/

                viewModel.OriginalName = Session["NomeArquivoOriginal"] == null ? "" : Session["NomeArquivoOriginal"].ToString();
                viewModel.ServerName = Session["NomeArquivoServidor"] == null ? "" : Session["NomeArquivoServidor"].ToString();

                var result = _appService.Create(viewModel);

                if (result.IsValid)
                {

                    this.StatusMessage("Logistica criada com sucesso!", Infra.CrossCutting.Tools.Enums.StatusMessageType.Success);

                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Erro ao salvar cadastro! Verifique o preenchimento dos campos!");

                    foreach (var error in result.Errors)
                    {
                        var target = error.Target ?? "";
                        ModelState.AddModelError(target, error.Message);
                    }
                }
            }
            else
            {
                if (viewModel.CollaboratorUid != Guid.Empty)
                {
                    var vm = _appService.GetDefaultOptions(viewModel);
                }

                ModelState.AddModelError("", "Erro ao atualizar cadastro! Verifique o preenchimento dos campos!");
            }

            return View(viewModel);
        }


        [HttpGet]
        public ActionResult Edit(Guid Uid)
        {
            var result = _appService.GetByEdit(Uid);

            ViewData["NomeArquivoServidor"] = result.ServerName;
            Session["NomeArquivoServidor"] = result.ServerName;
            Session["NomeArquivoOriginal"] = result.OriginalName;
            return View(result);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(LogisticsEditAppViewModel viewModel, ImageFileAppViewModel image, HttpPostedFileBase imageUpload)
        {
            if (ModelState.IsValid)
            {

                viewModel.OriginalName = Session["NomeArquivoOriginal"] == null ? "" : Session["NomeArquivoOriginal"].ToString();
                viewModel.ServerName = Session["NomeArquivoServidor"] == null ? "" : Session["NomeArquivoServidor"].ToString();

                var result = _appService.Update(viewModel);

                if (result.IsValid)
                {
                    this.StatusMessage("Logistica atualizada com sucesso!", Infra.CrossCutting.Tools.Enums.StatusMessageType.Success);
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
            }
            else
            {
                if (viewModel.CollaboratorUid != Guid.Empty)
                {
                    var vm = _appService.GetDefaultOptions(viewModel);
                }

                ModelState.AddModelError("", "Erro ao atualizar cadastro! Verifique o preenchimento dos campos!");
            }

            return View(viewModel);
        }


        public ActionResult Delete(Guid Uid)
        {
            var result = _appService.Delete(Uid);

            if (result.IsValid)
            {
                this.StatusMessage("Logistica apagado com sucesso!", Infra.CrossCutting.Tools.Enums.StatusMessageType.Success);
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

        //[HttpPost]
        public ActionResult SaveAndSendEmail(LogisticsEditAppViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var caminhoPath = Server.MapPath("~/UploadsArchives/");

                viewModel.OriginalName = Session["NomeArquivoOriginal"] == null ? "" : Session["NomeArquivoOriginal"].ToString();
                viewModel.ServerName = Session["NomeArquivoServidor"] == null ? "" : Session["NomeArquivoServidor"].ToString();

                if (viewModel.Uid == Guid.Empty)
                {
                    /*Guardar o nome do arquivo na viewModel*/
                    var result = _appService.Create(viewModel);
                    if (viewModel.OriginalName != null)
                    {
                        Attachment attachment = new Attachment(caminhoPath + viewModel.ServerName);
                        attachment.Name = viewModel.OriginalName.ToString();
                        _appService.SendEmailToCollaborators(viewModel.Collaborator.Uid, attachment, viewModel.TextEmail);
                    }

                    if (result.IsValid)
                    {
                        this.StatusMessage("Logistica criada com sucesso!", Infra.CrossCutting.Tools.Enums.StatusMessageType.Success);

                        return RedirectToAction("Index");
                    }
                    else
                    {

                        ModelState.AddModelError("", "Erro ao salvar cadastro! Verifique o preenchimento dos campos!");

                        foreach (var error in result.Errors)
                        {
                            var target = error.Target ?? "";
                            ModelState.AddModelError(target, error.Message);
                        }

                        return RedirectToAction("Create");
                    }


                }
                else
                {
                    var result = _appService.Update(viewModel);

                    if (viewModel.ServerName != null)
                    {
                        Attachment attachment = new Attachment(caminhoPath + viewModel.ServerName);
                        attachment.Name = viewModel.OriginalName;

                        _appService.SendEmailToCollaborators(viewModel.Collaborator.Uid, attachment, viewModel.TextEmail);
                    }

                    if (result.IsValid)
                    {
                        this.StatusMessage("Logistica atualizada com sucesso!", Infra.CrossCutting.Tools.Enums.StatusMessageType.Success);
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

                        //return RedirectToAction("Edit");
                    }
                }
            }
            else
            {
                if (viewModel.CollaboratorUid != Guid.Empty)
                {
                    var vm = _appService.GetDefaultOptions(viewModel);
                }

                ModelState.AddModelError("", "Erro ao atualizar cadastro! Verifique o preenchimento dos campos!");
            }

            return View("Create", viewModel);
        }

        public void FileUpload(HttpPostedFileBase file)
        {
            /*Salvar arquivo com nome alterado pra não correr o risco de arquivos com nomes duplicados*/
            string path = Server.MapPath("~/UploadsArchives/");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            if (file != null && file.ContentLength > 0)
            {

                var fileName = Path.GetFileName(file.FileName);
                var fileNameServer = Guid.NewGuid();
                path = Path.Combine(Server.MapPath("~/UploadsArchives/"), fileNameServer.ToString());
                file.SaveAs(path);

                Session["NomeArquivoOriginal"] = fileName;
                Session["NomeArquivoServidor"] = fileNameServer;
            }
        }

        public void Cancelar()
        {
            if (Session["NomeArquivoservidor"] != null)
            {
                if (System.IO.File.Exists(Server.MapPath("~/UploadsArchives/") + "/" + Session["NomeArquivoServidor"].ToString()))
                {
                    delArquivo(Server.MapPath("~/UploadsArchives/") + Session["NomeArquivServidor"].ToString());
                }
            }
        }
        private void delArquivo(string pCaminho)
        {
            try
            {
                if (System.IO.File.Exists(pCaminho) == true)
                {
                    System.IO.File.Delete(pCaminho);
                }
                return;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void GravarUploadArquivo()
        {
            if (Session["NomeArquivoTemp"] != null)
            {
                var caminhoPath = Server.MapPath("~/UploadsArchives/");

                if (System.IO.File.Exists(caminhoPath + Session["NomeArquivoOriginal"].ToString()))
                {
                    delArquivo(caminhoPath + Session["NomeArquivoOriginal"].ToString());
                }

                System.IO.File.Copy(caminhoPath + Session["NomeArquivoTemp"].ToString(), caminhoPath + Session["NomeArquivoOriginal"].ToString());
                delArquivo(caminhoPath + Session["NomeArquivoTemp"].ToString());
            }
        }


        public ActionResult DownloadArquivo(string id, string name)
        {
            var pathFiles = Server.MapPath("~/UploadsArchives/");
            return File(pathFiles + "\\" + id, "multipart /form-data", name.ToString());
        }
    }
}