// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Report
// Author           : William Sergio Almado Junior
// Created          : 12-13-2019
//
// Last Modified By : Rafael Dantas RUiz
// Last Modified On : 01-05-2020
// ***********************************************************************
// <copyright file="Table.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace PlataformaRio2C.Infra.Report.Models
{
    /// <summary>Table</summary>
    public static class Table
    {
        /// <summary>Gets the header cell.</summary>
        /// <param name="content">The content.</param>
        /// <param name="docTemplate">The document template.</param>
        /// <param name="alignment">The alignment.</param>
        /// <param name="style">The style.</param>
        /// <returns></returns>
        internal static PdfPCell GetHeaderCell(string content, TemplateBase docTemplate, int alignment, TableStyles style)
        {
            var cell = new PdfPCell(new Phrase(docTemplate != null ? docTemplate.GetChunk(content, docTemplate.DefaultFontSize, Font.BOLD) : new Chunk(content)));

            cell.VerticalAlignment = Element.ALIGN_CENTER;
            cell.HorizontalAlignment = alignment;
            cell.BorderWidth = 0;
            cell.BorderWidthTop = 0.5f;
            cell.BorderWidthBottom = 0.5f;
            cell.Padding = 3;

            //Caso a complexidade dos estilos aumente, migrar este código para uma abordagem de templates
            if (style == TableStyles.ZebradaCinza)
                cell.BackgroundColor = BaseColor.LIGHT_GRAY;

            return cell;
        }

        /// <summary>Gets the cell.</summary>
        /// <param name="content">The content.</param>
        /// <param name="docTemplate">The document template.</param>
        /// <param name="alignment">The alignment.</param>
        /// <param name="style">The style.</param>
        /// <param name="isOdd">if set to <c>true</c> [is odd].</param>
        /// <returns></returns>
        internal static PdfPCell GetCell(string content, TemplateBase docTemplate, int alignment, TableStyles style, bool isOdd)
        {
            var cell = new PdfPCell(new Phrase(docTemplate != null ? docTemplate.GetChunk(content) : new Chunk(content)));

            cell.VerticalAlignment = Element.ALIGN_CENTER;
            cell.HorizontalAlignment = alignment;
            cell.BorderWidth = 0;
            cell.Padding = 3;

            //Caso a complexidade dos estilos aumente, migrar este código para uma abordagem de templates
            if (style == TableStyles.ZebradaCinza)
            {
                cell.BackgroundColor = isOdd ? BaseColor.WHITE : new BaseColor(210, 210, 210);
            }

            return cell;
        }
    }
}