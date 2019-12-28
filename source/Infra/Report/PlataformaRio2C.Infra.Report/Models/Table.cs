// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : William Sergio Almado Junior
// Created          : 12-13-2019
//
// Last Modified By : William Sergio Almado Junior
// Last Modified On : 12-13-2019
// ***********************************************************************
// <copyright file="Table.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace PlataformaRio2C.Infra.Report
{
    public static class Table
    {
        internal static PdfPCell GetHeaderCell(string Content, TemplateBase DocTemplate, int Alignment, TableStyles style)
        {
            var cell = new PdfPCell(new Phrase(DocTemplate != null ? DocTemplate.GetChunk(Content, DocTemplate.DefaultFontSize, Font.BOLD) : new Chunk(Content)));

            cell.VerticalAlignment = Element.ALIGN_CENTER;
            cell.HorizontalAlignment = Alignment;
            cell.BorderWidth = 0;
            cell.BorderWidthTop = 0.5f;
            cell.BorderWidthBottom = 0.5f;
            cell.Padding = 3;

            //Caso a complexidade dos estilos aumente, migrar este código para uma abordagem de templates
            if (style == TableStyles.ZebradaCinza)
                cell.BackgroundColor = BaseColor.LIGHT_GRAY;

            return cell;
        }



        internal static PdfPCell GetCell(string Content, TemplateBase DocTemplate, int Alignment, TableStyles style, bool IsOdd)
        {
            var cell = new PdfPCell(new Phrase(DocTemplate != null ? DocTemplate.GetChunk(Content) : new Chunk(Content)));

            cell.VerticalAlignment = Element.ALIGN_CENTER;
            cell.HorizontalAlignment = Alignment;
            cell.BorderWidth = 0;
            cell.Padding = 3;

            //Caso a complexidade dos estilos aumente, migrar este código para uma abordagem de templates
            if (style == TableStyles.ZebradaCinza)
                cell.BackgroundColor = IsOdd ? BaseColor.WHITE : new BaseColor(210, 210, 210);

            return cell;
        }
    }
}
