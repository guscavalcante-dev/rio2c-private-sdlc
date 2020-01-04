using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaRio2C.Infra.Report
{
    public class PdfCellChipLayout : IPdfPCellEvent
    {
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
