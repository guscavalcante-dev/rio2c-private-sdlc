// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Report
// Author           : William Sergio Almado Junior
// Created          : 12-27-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-05-2020
// ***********************************************************************
// <copyright file="BoxReport.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace PlataformaRio2C.Infra.Report.Models
{
    /// <summary>
    /// Classe responsável por "desenhar" bordas, observação: todas as bordas devem possuir a mesma espessura.
    /// </summary>
    public static class BoxReport
    {
        /// <summary>
        /// Desenha a borda com uma definição para cada lado
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="borderLeft"></param>
        /// <param name="borderTop"></param>
        /// <param name="borderRight"></param>
        /// <param name="borderBottom"></param>
        /// <returns></returns>
        public static PdfPCell Custom(PdfPCell cell, float borderLeft, float borderTop, float borderRight, float borderBottom)
        {
            cell.BorderWidthLeft = borderLeft;
            cell.BorderWidthTop = borderTop;
            cell.BorderWidthRight = borderRight;
            cell.BorderWidthBottom = borderBottom;

            return cell;
        }

        /// <summary>
        /// Desenha a borda de uma imagem com uma definição para cada lado 
        /// </summary>
        /// <param name="image"></param>
        /// <param name="borderLeft"></param>
        /// <param name="borderTop"></param>
        /// <param name="borderRight"></param>
        /// <param name="borderBottom"></param>
        /// <returns></returns>
        public static Image Custom(Image image, float borderLeft, float borderTop, float borderRight, float borderBottom)
        {
            image.BorderWidthLeft = borderLeft;
            image.BorderWidthTop = borderTop;
            image.BorderWidthRight = borderRight;
            image.BorderWidthBottom = borderBottom;

            return image;
        }

        /// <summary>
        /// Desenha a borda da Esquerda e a do Topo
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="borderWidth"></param>
        /// <returns></returns>
        public static PdfPCell LeftTop(PdfPCell cell, float borderWidth)
        {
            return Custom(cell, borderWidth, borderWidth, 0, 0);
        }

        /// <summary>
        /// Desenha a borda da Direita e a do Topo
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="borderWidth"></param>
        /// <returns></returns>
        public static PdfPCell RightTop(PdfPCell cell, float borderWidth)
        {
            return Custom(cell, 0, borderWidth, borderWidth, 0);
        }

        /// <summary>
        /// Desenha a borda da Esquerda e a de Baixo
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="borderWidth"></param>
        /// <returns></returns>
        public static PdfPCell LeftBottom(PdfPCell cell, float borderWidth)
        {
            return Custom(cell, borderWidth, 0, 0, borderWidth);
        }

        /// <summary>
        /// Desenha a borda da Direita e a de Baixo
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="borderWidth"></param>
        /// <returns></returns>
        public static PdfPCell RightBottom(PdfPCell cell, float borderWidth)
        {
            return Custom(cell, 0, 0, borderWidth, borderWidth);
        }

        /// <summary>
        /// Desenha a borda da Esquerda, da Direita e a do Topo
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="borderWidth"></param>
        /// <returns></returns>
        public static PdfPCell LeftRightTop(PdfPCell cell, float borderWidth)
        {
            return Custom(cell, borderWidth, borderWidth, borderWidth, 0);
        }

        /// <summary>
        /// Desenha a borda da Esquerda, da Direita e a de Baixo
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="borderWidth"></param>
        /// <returns></returns>
        public static PdfPCell LeftRightBottom(PdfPCell cell, float borderWidth)
        {
            return Custom(cell, borderWidth, 0, borderWidth, borderWidth);
        }

        /// <summary>
        /// Desenha bordas em todos os lados
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="borderWidth"></param>
        /// <returns></returns>
        public static PdfPCell Closed(PdfPCell cell, float borderWidth)
        {
            return Custom(cell, borderWidth, borderWidth, borderWidth, borderWidth);
        }

        /// <summary>Desenha bordas em todos os lados</summary>
        /// <param name="image">The image.</param>
        /// <param name="borderWidth">Width of the border.</param>
        /// <returns></returns>
        public static Image Closed(Image image, float borderWidth)
        {
            return Custom(image, borderWidth, borderWidth, borderWidth, borderWidth);
        }

        /// <summary>
        /// Desenha bordas na esquerda, no topo e embaixo
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="borderWidth"></param>
        /// <returns></returns>
        public static PdfPCell LeftTopBottom(PdfPCell cell, float borderWidth)
        {
            return Custom(cell, borderWidth, borderWidth, 0, borderWidth);
        }

        /// <summary>
        /// Desenha bordas na direita, no topo e embaixo
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="borderWidth"></param>
        /// <returns></returns>
        public static PdfPCell RightTopBottom(PdfPCell cell, float borderWidth)
        {
            return Custom(cell, 0, borderWidth, borderWidth, borderWidth);
        }

        /// <summary>
        /// Desenha a borda da Esquerda
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="borderWidth"></param>
        /// <returns></returns>
        public static PdfPCell Left(PdfPCell cell, float borderWidth)
        {
            return Custom(cell, borderWidth, 0, 0, 0);
        }

        /// <summary>
        /// Desenha a borda da Direita
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="borderWidth"></param>
        /// <returns></returns>
        public static PdfPCell Right(PdfPCell cell, float borderWidth)
        {
            return Custom(cell, 0, 0, borderWidth, 0);
        }

        /// <summary>
        /// Desenha a borda do topo
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="borderWidth"></param>
        /// <returns></returns>
        public static PdfPCell Top(PdfPCell cell, float borderWidth)
        {
            return Custom(cell, 0, borderWidth, 0, 0);
        }

        /// <summary>
        /// Desenha a borda da Esquerda e a da Direita
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="borderWidth"></param>
        /// <returns></returns>
        public static PdfPCell LeftRight(PdfPCell cell, float borderWidth)
        {
            return Custom(cell, borderWidth, 0, borderWidth, 0);
        }
    }
}