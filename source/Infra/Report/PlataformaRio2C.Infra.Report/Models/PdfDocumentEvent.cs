// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Report
// Author           : William Sergio Almado Junior
// Created          : 12-27-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-05-2020
// ***********************************************************************
// <copyright file="PDFDocumentEvent.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace PlataformaRio2C.Infra.Report.Models
{
    /// <summary>PDFDocumentEvent</summary>
    public class PDFDocumentEvent : PdfPageEventHelper
    {
        private readonly TemplateBase Template;
        PdfTemplate pdfTemplate;
        BaseFont basefont;

        /// <summary>Initializes a new instance of the <see cref="PDFDocumentEvent"/> class.</summary>
        /// <param name="template">The template.</param>
        public PDFDocumentEvent(TemplateBase template)
        {
            Template = template;
        }

        /// <summary>Called when [start page].</summary>
        /// <param name="writer">The writer.</param>
        /// <param name="document">The document.</param>
        public override void OnStartPage(PdfWriter writer, Document document)
        {
            base.OnStartPage(writer, document);

            if (!Template.ShowDefaultBackground)
                return;

            if (writer.PageNumber == 1)
            {
                var img = Images.BackgroundFirstPage;
                img.ScaleToFit(document.PageSize.Width, document.PageSize.Height + 120);
                img.Alignment = Image.UNDERLYING;
                img.SetAbsolutePosition(0, 0);
                document.Add(img);
            }
            else
            {
                var imgFooter = Images.BackgroundFooter;
                imgFooter.ScaleToFit(document.PageSize.Width, document.PageSize.Height + 120);
                imgFooter.Alignment = Image.UNDERLYING;
                imgFooter.SetAbsolutePosition(0, 0);
                //imgFooter.Transparency = new int[4];
                document.Add(imgFooter);
            }
        }

        /// <summary>Called when [end page].</summary>
        /// <param name="writer"></param>
        /// <param name="document"></param>
        /// Called when a page is finished, just before being written to the document.
        /// @param writer the <CODE>PdfWriter</CODE> for this document
        /// @param document the document
        public override void OnEndPage(PdfWriter writer, Document document)
        {
            base.OnEndPage(writer, document);

            if (!Template.ShowDefaultFooter)
                return;

            if (pdfTemplate == null)
                pdfTemplate = writer.DirectContent.CreateTemplate(50, 50);

            string text = "Página " + writer.PageNumber + " de ";
            basefont = Template.GetFont(6, Font.NORMAL).GetCalculatedBaseFont(true);

            float len = basefont.GetWidthPoint(text, 6);
            Rectangle pageSize = document.PageSize;

            writer.DirectContent.SetRGBColorFill(0, 0, 0);
            writer.DirectContent.BeginText();
            writer.DirectContent.SetFontAndSize(basefont, 6);
            writer.DirectContent.SetTextMatrix(pageSize.GetLeft(40), pageSize.GetBottom(25));
            writer.DirectContent.ShowText(text);
            writer.DirectContent.EndText();
            writer.DirectContent.AddTemplate(pdfTemplate, pageSize.GetLeft(40) + len, pageSize.GetBottom(25));

            writer.DirectContent.BeginText();
            writer.DirectContent.SetFontAndSize(basefont, 6);
            writer.DirectContent.ShowTextAligned(
                PdfContentByte.ALIGN_RIGHT,
                "Impresso em " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
                pageSize.GetRight(40),
                pageSize.GetBottom(25), 0);

            writer.DirectContent.SetFontAndSize(basefont, 6);
            writer.DirectContent.ShowTextAligned(
                PdfContentByte.ALIGN_CENTER,
                (Template.HeaderAddendum == null ? "" : Template.HeaderAddendum),
                pageSize.Width / 2,
                pageSize.GetBottom(25), 0);
            writer.DirectContent.EndText();
        }

        /// <summary>Called when [close document].</summary>
        /// <param name="writer">The writer.</param>
        /// <param name="document">The document.</param>
        public override void OnCloseDocument(PdfWriter writer, Document document)
        {
            base.OnCloseDocument(writer, document);

            if (pdfTemplate != null)
            {
                pdfTemplate.BeginText();
                pdfTemplate.SetFontAndSize(basefont, 6);
                pdfTemplate.SetTextMatrix(0, 0);
                pdfTemplate.ShowText("" + document.PageNumber);
                pdfTemplate.EndText(); 
            }
        }
    }
}