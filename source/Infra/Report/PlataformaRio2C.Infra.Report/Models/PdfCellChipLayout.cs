// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Report
// Author           : William Sergio Almado Junior
// Created          : 12-27-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-05-2020
// ***********************************************************************
// <copyright file="PdfCellChipLayout.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace PlataformaRio2C.Infra.Report.Models
{
    /// <summary>PdfCellChipLayout</summary>
    public class PdfCellChipLayout : IPdfPCellEvent
    {
        /// <summary>Cells the layout.</summary>
        /// <param name="cell">The cell.</param>
        /// <param name="position">The position.</param>
        /// <param name="canvases">The canvases.</param>
        public void CellLayout(PdfPCell cell, Rectangle position, PdfContentByte[] canvases)
        {
            PdfContentByte cb = canvases[PdfPTable.BACKGROUNDCANVAS];
            cb.RoundRectangle(
              position.Left + 1.5f,
              position.Bottom + 1.5f,
              position.Width - 3,
              position.Height - 3, 4
            );
            cb.SetCMYKColorFill(0x64, 0x49, 0x00, 0x08);
            cb.Fill();
        }
    }
}