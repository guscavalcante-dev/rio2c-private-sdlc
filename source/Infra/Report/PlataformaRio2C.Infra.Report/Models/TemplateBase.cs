// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Report
// Author           : William Sergio Almado Junior
// Created          : 12-27-2019
//
// Last Modified By : Rafael Dantas RUiz
// Last Modified On : 01-05-2020
// ***********************************************************************
// <copyright file="TemplateBase.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace PlataformaRio2C.Infra.Report.Models
{
    /// <summary>TemplateBase</summary>
    public abstract class TemplateBase
    {
        /// <summary>Document Title.</summary>
        public string Title { get; set; }

        /// <summary>Document Subtitle.</summary>
        public string Subtitle { get; set; }

        /// <summary>First header of the document.</summary>
        public IElement FirstHeader { get; set; }

        /// <summary>Footer of the document.</summary>
        public IElement Footer { get; set; }

        /// <summary>Header for second page and subsequential.</summary>
        public IElement SubsequentialHeaders { get; set; }

        /// <summary>Text to compose the header and footer.</summary>
        public string HeaderAddendum { get; set; }

        /// <summary>Default font size. Sets and standard that other fonte sizes of the document can in percentage.</summary>
        public float DefaultFontSize { get; set; }

        /// <summary>Specify if the default footer must be shown in the document.</summary>
        public bool ShowDefaultFooter { get; set; }

        /// <summary>Specify if the default background must be shown in the document.</summary>
        public bool ShowDefaultBackground { get; set; }

        /// <summary>Specify if the document orientation is landscape.</summary>
        public bool LandscapeOrientation { get; set; }

        public abstract void Prepare(PlataformaRio2CDocument document);

        /// <summary>Gets a paragraph instance with the template format.</summary>
        public abstract Paragraph GetParagraph(string text = null);

        /// <summary>Gets a chunk of the text.</summary>
        /// <returns></returns>
        public Chunk GetChunk(string text)
        {
            return new Chunk(text, GetFont());
        }

        /// <summary>Gets a chunk of the text.</summary>
        /// <param name="text">The text.</param>
        /// <param name="fontSize">Size of the font.</param>
        /// <param name="fontStyle">The font style (e.g. Font.BOLD).</param>
        /// <returns></returns>
        public Chunk GetChunk(string text, float fontSize, int fontStyle = Font.NORMAL)
        {
            return new Chunk(text, GetFont(fontSize, fontStyle));
        }

        /// <summary>Gets a phrase of the text.</summary>
        /// <param name="text">The text.</param>
        /// <param name="fontStyle">The font style (e.g. Font.BOLD).</param>
        /// <param name="fontSize">Size of the font. By default is the size specified in the template.</param>
        /// <returns></returns>
        public Phrase GetPhrase(string text, int fontStyle = Font.NORMAL, float fontSize = 0)
        {
            if (Convert.ToInt32(fontSize) == 0)
                fontSize = DefaultFontSize;

            return new Phrase(GetChunk(text, fontSize, fontStyle));
        }

        /// <summary>Gets the default font of the template.</summary>
        /// <returns></returns>
        public Font GetFont()
        {
            return GetFont(DefaultFontSize, Font.NORMAL);
        }

        /// <summary>Gets the default font of the template.</summary>
        /// <returns></returns>
        public abstract Font GetFont(float fontSize, int fontStyle);

        /// <summary>Gets the chips.</summary>
        /// <param name="texts">The texts.</param>
        /// <param name="fontStyle">The font style.</param>
        /// <param name="fontSize">Size of the font.</param>
        /// <returns></returns>
        public PdfPCell GetChips(List<string> texts, int fontStyle = Font.NORMAL, float fontSize = 0)
        {
            var mainCell = new PdfPCell();

            var columns = new float[texts.Count];

            var columnSize = 0f;

            for (int i = 0; i < texts.Count; i++)
            {
                columns[i] = texts[i].Length + 15;
                columnSize += texts[i].Length + 15;
            }

            var table = new PdfPTable(columns);
            table.WidthPercentage = columnSize;
            table.HorizontalAlignment = Element.ALIGN_LEFT;
            table.DefaultCell.Border = 0;
            foreach (var text in texts)
            {
                var cell = new PdfPCell(GetPhrase(text, fontStyle, fontSize));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Padding = 5;
                cell.CellEvent = new PdfCellChipLayout();
                cell.Border = 0;
                table.AddCell(cell);

            }
            mainCell.Border = 0;
            mainCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            mainCell.AddElement(table);

            return mainCell;
        }

        /// <summary>Initializes a new instance of the <see cref="TemplateBase"/> class. Sets the initial default values of the template (e.g. DefaultFontSize).</summary>
        public TemplateBase()
        {
            DefaultFontSize = 8;
        }
    }
}