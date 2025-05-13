// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.CrossCutting.Tools
// Author           : Rafael Dantas Ruiz
// Created          : 11-18-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 11-18-2019
// ***********************************************************************
// <copyright file="ExcelResult.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using ClosedXML.Excel;
using System.IO;
using System.Web.Mvc;

namespace PlataformaRio2C.Infra.CrossCutting.Tools.CustomActionResults
{
    /// <summary>ExcelResult</summary>
    public class ExcelResult : ActionResult
    {
        private readonly XLWorkbook workbook;
        private readonly string fileName;

        /// <summary>Initializes a new instance of the <see cref="ExcelResult"/> class.</summary>
        /// <param name="workbook">The workbook.</param>
        /// <param name="fileName">Name of the file.</param>
        public ExcelResult(XLWorkbook workbook, string fileName)
        {
            this.workbook = workbook;
            this.fileName = fileName;
        }

        /// <summary>Enables processing of the result of an action method by a custom type that inherits from the <see cref="T:System.Web.Mvc.ActionResult"/> class.</summary>
        /// <param name="context">The context in which the result is executed. The context information includes the controller, HTTP content, request context, and route data.</param>
        public override void ExecuteResult(ControllerContext context)
        {
            var response = context.HttpContext.Response;
            response.Clear();
            response.ContentType = "application/vnd.openxmlformats-officedocument." + "spreadsheetml.sheet";
            response.AddHeader("content-disposition", "attachment;filename=" + this.fileName + ".xlsx");

            using (var memoryStream = new MemoryStream())
            {
                this.workbook.SaveAs(memoryStream);
                memoryStream.WriteTo(response.OutputStream);
            }
            response.End();
        }
    }
}