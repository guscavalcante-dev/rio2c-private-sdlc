// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Report
// Author           : William Sergio Almado Junior
// Created          : 12-27-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-05-2020
// ***********************************************************************
// <copyright file="PlataformaRio2CDocument.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace PlataformaRio2C.Infra.Report.Models
{
    /// <summary>PlataformaRio2CDocument</summary>
    public class PlataformaRio2CDocument : Document
    {
        /// <summary>Writer object of the document used to generate this document instance.</summary>
        public PdfWriter Writer { get; set; }

        private MemoryStream DocumentStream { get; set; }

        private TemplateBase _template;

        /// <summary>Template for basic configurations and additions of new elments to the document.</summary>
        /// <value>The template.</value>
        public TemplateBase Template
        {
            get
            {
                return _template;
            }
            set
            {
                if (value != null)
                    _template = value;
            }
        }

        /// <summary>Initializes a new instance of the <see cref="PlataformaRio2CDocument"/> class. Initialize a new PDF document based on the specified template and ready to receive new content elements.</summary>
        /// <param name="template">The template.</param>
        /// <param name="marginLeft">The margin left.</param>
        /// <param name="marginRight">The margin right.</param>
        /// <param name="marginTop">The margin top.</param>
        /// <param name="marginBottom">The margin bottom.</param>
        public PlataformaRio2CDocument(TemplateBase template, float marginLeft = 25, float marginRight = 25, float marginTop = 10, float marginBottom = 50)
            : base(iTextSharp.text.PageSize.A4, marginLeft, marginRight, marginTop, marginBottom)
        {
            DocumentStream = new MemoryStream();
            Writer = PdfWriter.GetInstance(this, DocumentStream);

            Writer.CloseStream = false;

            if ((Template = template) != null)
            {
                if (Template.LandscapeOrientation)
                    SetPageSize(iTextSharp.text.PageSize.A4.Rotate());

                Writer.PageEvent = new PDFDocumentEvent(Template);

                Open();
                Template.Prepare(this);
            }
            else
                Open();

            AddTitle(Template?.Title);
            AddAuthor("Nome da sua Empresa");
            AddSubject(Template?.Subtitle);
            AddCreationDate();
        }

        /// <summary>Adds a paragraph to do document.</summary>
        /// <param name="text">The text.</param>
        /// <param name="alignment">The alignment.</param>
        /// <param name="firstLineIndent">The first line indent.</param>
        /// <param name="linesLeading">The lines leading.</param>
        public void AddParagraph(string text, int alignment = Element.ALIGN_JUSTIFIED, float firstLineIndent = 0, float linesLeading = 1.5f)
        {
            if (Template != null)
                Add(Template.GetParagraph(text));
            else
            {
                var parag = new Paragraph();
                parag.Alignment = alignment;
                parag.FirstLineIndent = firstLineIndent;
                parag.MultipliedLeading = linesLeading;
            }
        }

        /// <summary>Creates the table.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items">The items.</param>
        /// <param name="columns">The columns.</param>
        /// <param name="style">The style.</param>
        /// <param name="headerRows">The header rows.</param>
        /// <param name="title">The title.</param>
        /// <param name="groupingMap">The grouping map.</param>
        /// <param name="emptyText">The empty text.</param>
        /// <returns></returns>
        public PdfPTable CreateTable<T>(List<T> items, ColumnMap[] columns, TableStyles style, int headerRows = 0, string title = null, GroupingMap groupingMap = null, string emptyText = null)
        {
            if (columns == null)
                throw new Exception("O mapeamento das colunas não pode ser nulo");

            var columnWidths = new float[columns.Length];

            for (int i = 0; i < columns.Length; i++)
                columnWidths[i] = columns[i].WidthPercentage;

            var table = new PdfPTable(columnWidths);
            table.WidthPercentage = 100;
            table.HeaderRows = headerRows;

            if (!string.IsNullOrEmpty(title))
            {
                table.DefaultCell.BorderWidth = 0;
                table.DefaultCell.Colspan = columns.Length;
                table.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                table.DefaultCell.Padding = 7;

                var paragTitle = Template.GetChunk(title);
                paragTitle.Font = Template.GetFont();
                paragTitle.Font.SetStyle(Font.BOLD);
                paragTitle.Font.Size = Template.DefaultFontSize + (Template.DefaultFontSize * 0.5f);
                table.AddCell(new Phrase(paragTitle));
                table.DefaultCell.Colspan = 0;
            }

            var hasFooter = false;
            foreach (var column in columns)
            {
                table.DefaultCell.HorizontalAlignment = column.Alignment;
                table.AddCell(Table.GetHeaderCell(column.ColumnHeader, Template, column.Alignment, style));
                hasFooter = (hasFooter || column.ColumnFooter != null);
            }

            var j = 0;
            if (items != null && items.Count > 0)
            {
                var groupProperty = items[0].GetType().GetProperty(groupingMap?.GroupPropertyPath ?? "-");

                if (groupingMap != null && groupProperty == null)
                    throw new Exception($"A propriedade '{ groupingMap.GroupPropertyPath }' não existe nos elementos da coleção");

                // Carregar propriedades de reflexão de agrupamento
                for (uint i = 0; i < columns.Length; i++)
                    if (groupingMap != null && groupingMap.SubTotals.ContainsKey(i))
                        groupingMap.SubTotals[i].ColumnProperty = items[0].GetType().GetProperty(columns[i].PropertyPath);

                var currentGroupKey = "!Hey,do_you_know_that@#$Nothing_in_the_world_can_be_like_this_initializer_text?!";
                bool firstGroup = true, lastGroup = false;
                foreach (var item in items)
                {
                    if (groupingMap != null)
                    {

                        // Se o valor da propriedade chave do agrupamento mudou, criar linha de subtotal e próximo header
                        if (!currentGroupKey.Equals(groupProperty?.GetValue(item)?.ToString()))
                        {
                            AddGroupStrip(table, groupProperty?.GetValue(item)?.ToString(), firstGroup, lastGroup, groupingMap, columns, Template, style);
                            currentGroupKey = groupProperty?.GetValue(item)?.ToString();
                        }

                        // Inclusão de header para grupo corrente
                        if (table.HeadersInEvent)
                        {
                            table.DefaultCell.Colspan = table.NumberOfColumns;
                            table.AddCell(currentGroupKey);
                            table.DefaultCell.Colspan = 0;
                        }

                        // Incrementar subtotais
                        for (uint i = 0; i < columns.Length; i++)
                        {
                            if (groupingMap.SubTotals.ContainsKey(i))
                            {
                                try
                                {
                                    groupingMap.SubTotals[i].Subtotal += (decimal)groupingMap.SubTotals[i].ColumnProperty.GetValue(item);
                                }
                                catch { /* falhar graciosamente */ }
                            }
                        }

                        firstGroup = false;
                    }

                    foreach (var column in columns)
                    {
                        table.DefaultCell.HorizontalAlignment = column.Alignment;
                        table.AddCell(
                                Table.GetCell(GetValue(item, column.PropertyPath, column.StringFormat),
                                                      Template,
                                                      column.Alignment,
                                    style,
                                                      (j % 2 == 0)));
                    }
                    j++;
                }

                //Adicionar último group strip se houver
                if (groupingMap != null)
                    AddGroupStrip(table, "", false, true, groupingMap, columns, Template, style);
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(emptyText))
                {
                    table.DefaultCell.BorderWidth = 0;
                    table.DefaultCell.Colspan = columns.Length;
                    table.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.DefaultCell.Padding = 7;
                    table.AddCell(Template.GetPhrase(emptyText, Font.NORMAL, Template.DefaultFontSize));
                }
            }

            if (hasFooter)
            {
                for (int i = 0; i < columns.Length; i++)
                    table.AddCell(Table.GetHeaderCell(columns[i].ColumnFooter, Template, columns[i].Alignment, style));

                table.DefaultCell.BorderWidth = 0;
                table.CompleteRow();
            }

            return table;
        }

        /// <summary>Adds a table to the document. Users as default font the font specifiend in the template.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items">The items.</param>
        /// <param name="columns">The columns.</param>
        /// <param name="style">The style.</param>
        /// <param name="headerRows">The header rows.</param>
        /// <param name="title">The title.</param>
        /// <param name="groupingMap">The grouping map.</param>
        /// <param name="emptyText">The empty text.</param>
        public void AddTable<T>(List<T> items, ColumnMap[] columns, TableStyles style, int headerRows = 0, string title = null, GroupingMap groupingMap = null, string emptyText = null)
        {
            Add(CreateTable(items, columns, style, headerRows, title, groupingMap, emptyText));
        }

        /// <summary>Adds a group strip to the specidifed table that represents an suumary and a header - last group and current group respectively.</summary>
        /// <param name="table">The table.</param>
        /// <param name="currentGroupKey">The current group key.</param>
        /// <param name="firstGroup">if set to <c>true</c> [first group].</param>
        /// <param name="lastGroup">if set to <c>true</c> [last group].</param>
        /// <param name="groupingMap">The grouping map.</param>
        /// <param name="columns">The columns.</param>
        /// <param name="template">The template.</param>
        /// <param name="style">The style.</param>
        private void AddGroupStrip(PdfPTable table, string currentGroupKey, bool firstGroup, bool lastGroup, GroupingMap groupingMap, ColumnMap[] columns, TemplateBase template, TableStyles style)
        {
            if (string.IsNullOrWhiteSpace(currentGroupKey))
                currentGroupKey = "-";

            // Subtotal (O primeiro group strip não é precedido por um sumário)
            if (!firstGroup)
                for (uint i = 0; i < columns.Length; i++)
                {
                    table.DefaultCell.HorizontalAlignment = columns[i].Alignment;
                    if (groupingMap.SubTotals.ContainsKey(i))
                        table.AddCell(Table.GetHeaderCell(
                                                    string.Format(columns[i].StringFormat, groupingMap.SubTotals[i].Subtotal),
                                                    template,
                                                    columns[i].Alignment,
                            style));
                    else
                        table.AddCell(Table.GetHeaderCell(i == 0 ? "Subtotal" : " ", template, columns[i].Alignment, style));
                }

            // Cabeçalho (o último group strip não é seguido por um sumário)
            if (!lastGroup)
            {
                var cell = Table.GetHeaderCell(currentGroupKey, template, Element.ALIGN_LEFT, style);
                cell.BackgroundColor = BaseColor.WHITE;
                cell.Colspan = columns.Length;
                table.AddCell(cell);
            }

            if (!groupingMap.GlobalSubtotals)
                groupingMap.ResetSubTotals();
        }

        /// <summary>Gets the value.</summary>
        /// <param name="obj">The object.</param>
        /// <param name="path">The path.</param>
        /// <param name="stringFormat">The string format.</param>
        /// <returns></returns>
        private string GetValue(object obj, string path, string stringFormat)
        {
            if (obj == null)
                return "";

            string[] local_path = path.Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);

            if (local_path.Length == 1)
            {
                var prop = obj.GetType().GetProperty(local_path[0]);

                if (prop == null)
                    return "";

                var local_value = prop.GetValue(obj);

                if (local_value == null)
                    return "";
                else
                    return string.Format((stringFormat == null ? "{0}" : stringFormat), local_value);
            }
            else if (local_path.Length >= 2)
            {
                //Prepare next call
                var next_path = path.Substring(path.IndexOf('.', 0) + 1);
                var next_prop = obj.GetType().GetProperty(local_path[0]);

                if (next_prop != null)
                    return GetValue(next_prop.GetValue(obj), next_path, stringFormat);
                else
                    return "";
            }
            else
                return "";
        }

        /// <summary>Close the document and recover the stream of bytes that represents it.</summary>
        /// <returns></returns>
        public MemoryStream GetStream()
        {
            if (PageNumber == 0 && IsOpen())
                Add(new Chunk(""));

            Close();
            DocumentStream.Position = 0;

            return DocumentStream;
        }

        /// <summary>Closed the document and saves the PDF to the specified filepath.</summary>
        /// <param name="filepath">The filepath.</param>
        /// <param name="filemode">The filemode.</param>
        public void Save(string filepath, FileMode filemode)
        {
            if (PageNumber == 0 && IsOpen())
                Add(new Chunk(""));

            Close();
            using (var fs = new FileStream(filepath, filemode))
            {
                var bytes = DocumentStream.ToArray();
                fs.Write(bytes, 0, bytes.Length);
            }
        }
    }
}