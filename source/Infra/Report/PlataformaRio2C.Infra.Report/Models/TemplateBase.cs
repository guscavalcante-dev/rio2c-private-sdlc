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
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Linq;

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
        public PdfPCell GetChips(List<Chunk> texts, int fontStyle = Font.NORMAL, float fontSize = 0, int rowSize = 100)
        {
            var mainCell = new PdfPCell();

            var tableMaster = new PdfPTable(1);
            tableMaster.WidthPercentage = 100;


            for (int i = 0; i < texts.Count; i++)
            {
                var columns = new float[1];
                columns[0] = texts[i].GetWidthPoint() < 10 ? 20 : (texts[i].GetWidthPoint() * 1.8f);

                var table = new PdfPTable(columns);
                table.WidthPercentage = columns[0];
                table.HorizontalAlignment = Element.ALIGN_LEFT;

                var cell = new PdfPCell(GetPhrase(texts[i].Content, fontStyle, fontSize));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Padding = 5;
                cell.CellEvent = new PdfCellChipLayout();

                table.AddCell(cell);
                tableMaster.SetWidths(columns);

                tableMaster.AddCell(table);
                table.DeleteLastRow();
            }

            mainCell.Border = 0;
            mainCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            mainCell.AddElement(tableMaster);
            return mainCell;
        }

        public PdfPCell GetChips(List<string> texts, int fontStyle = Font.NORMAL, float fontSize = 0)
        {
            var mainCell = new PdfPCell();

            var columns = new float[texts.Count];

            var columnSize = 0f;

            for (int i = 0; i < texts.Count; i++)
            {
                columns[i] = texts[i].Length < 10 ? 20 : (texts[i].Length * 1.8f);
                columnSize += columns[i];
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

        /// <summary>
        /// Gets the chips
        /// </summary>
        /// <param name="label"></param>
        /// <param name="chips"></param>
        /// <param name="fontLabel"></param>
        /// <param name="defaultTable"></param>
        /// <returns></returns>
        public PdfPTable GetChips(string label, List<Chunk> chips, Font fontLabel, PdfPTable defaultTable)
        {
            var tagLabel = new Chunk(label, fontLabel);
            var tagLabelSize = tagLabel.GetWidthPoint() <= 60f ? tagLabel.GetWidthPoint() : tagLabel.GetWidthPoint() - 25;

            var rootTable = new PdfPTable(new float[] { tagLabelSize, (PageSize.A4.Width / 2) - (tagLabelSize) });
            rootTable.WidthPercentage = defaultTable.WidthPercentage;
            rootTable.DefaultCell.VerticalAlignment = defaultTable.DefaultCell.VerticalAlignment;
            rootTable.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT; //defaultTable.DefaultCell.HorizontalAlignment;
            rootTable.DefaultCell.Border = 0;

            var availableWidth = (PageSize.A4.Width / 2) - (tagLabelSize);

            var paragLabel = new Paragraph(tagLabel);
            rootTable.DefaultCell.PaddingTop = 10;

            rootTable.AddCell(new Paragraph(paragLabel));

            rootTable.DefaultCell.PaddingTop = 2;

            var rows = new List<List<Chunk>>();
            var row = new List<Chunk>();

            rows.Add(row);

            var padding = 2f;

            for (int i = 0; i < chips.Count; i++)
            {
                var currentChip = chips[i];

                if (row.Sum(x => x.GetWidthPoint() + (padding * 1.0f) + currentChip.GetWidthPoint()/*padding*/) < availableWidth)
                    row.Add(currentChip);
                else
                {
                    row = new List<Chunk>();
                    row.Add(currentChip);
                    rows.Add(row);
                }

            }

            var rowsTable = new PdfPTable(1);
            rowsTable.WidthPercentage = 100;
            rowsTable.DefaultCell.Border = 0;

            foreach (var line in rows)
            {
                var columns = line.Select(x => x.GetWidthPoint() + padding).ToList();
                var columnSize = availableWidth - (line.Sum(x => x.GetWidthPoint()) + (columns.Count * padding));
                if (columnSize > 0)
                {
                    columns.Add(columnSize);
                }

                var chipsTable = new PdfPTable(columns.ToArray());
                chipsTable.DefaultCell.Border = 0;
                chipsTable.WidthPercentage = 100;
                chipsTable.DefaultCell.CellEvent = new PdfCellChipLayout();
                chipsTable.DefaultCell.PaddingLeft = 0;
                chipsTable.DefaultCell.PaddingRight = 0;
                chipsTable.DefaultCell.PaddingTop = 7;
                chipsTable.DefaultCell.PaddingBottom = 10;
                chipsTable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;

                foreach (var chip in line)
                {
                    var parag = new Paragraph(chip);
                    parag.Alignment = Element.ALIGN_CENTER;
                    chipsTable.AddCell(parag);
                }

                chipsTable.DefaultCell.CellEvent = null;
                chipsTable.CompleteRow();
                rowsTable.AddCell(chipsTable);
            }
            rootTable.AddCell(rowsTable);

            return rootTable;
        }
        /// <summary>Initializes a new instance of the <see cref="TemplateBase"/> class. Sets the initial default values of the template (e.g. DefaultFontSize).</summary>
        public TemplateBase()
        {
            DefaultFontSize = 8;
        }
    }
}