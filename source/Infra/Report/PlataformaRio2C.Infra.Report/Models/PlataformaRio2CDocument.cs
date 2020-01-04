// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : William Sergio Almado Junior
// Created          : 12-27-2019
//
// Last Modified By : William Sergio Almado Junior
// Last Modified On : 12-27-2019
// ***********************************************************************
// <copyright file="PlataformaRio2CDocument.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;

namespace PlataformaRio2C.Infra.Report
{
    public class PlataformaRio2CDocument : Document
    {

        /// <summary>
        /// Objeto "escritor" do documento utilizado para gerar esta instância de documento 
        /// </summary>
        public PdfWriter Writer { get; set; }
        private MemoryStream DocumentStream { get; set; }

        private TemplateBase _template;
        /// <summary>
        /// Template tido como base para configurações básicas e adição de novos elementos no documento
        /// </summary>
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

        /// <summary>
        /// Constrói uma instância de documento PDF com base no template especificado e pronto para receber elementos de conteúdo
        /// </summary>
        /// <param name="template">Template que implementa </param>
        /// <param name="marginLeft">Margem esquerda do documento</param>
        /// <param name="marginRight">Margem direita do documento</param>
        /// <param name="marginTop">Margem superior do documento</param>
        /// <param name="marginBottom">Margem inferior do documento</param>
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

        /// <summary>
        /// Adiciona um parágrafo ao documento
        /// </summary>
        /// <param name="Text">Texto para o conteúdo </param>
        /// <param name="Alignment">Alinhamento do parágrafo (e.g. Element.ALIGN_LEFT)</param>
        /// <param name="FirstLineIndent">Recuo da primeira linha do parágrafo</param>
        /// <param name="LinesLeading">Espaço entrelinhas do parágrafo</param>
        public void AddParagraph(string Text, int Alignment = Element.ALIGN_JUSTIFIED, float FirstLineIndent = 0, float LinesLeading = 1.5f)
        {
            if (Template != null)
                Add(Template.GetParagraph(Text));
            else
            {
                var parag = new Paragraph();
                parag.Alignment = Alignment;
                parag.FirstLineIndent = 0;
                parag.MultipliedLeading = LinesLeading;
            }
        }

        public PdfPTable CreateTable<T>(List<T> Items, ColumnMap[] Columns, TableStyles Style, int HeaderRows = 0, string Title = null, GroupingMap groupingMap = null, string emptyText = null)
        {
            if (Columns == null)
                throw new Exception("O mapeamento das colunas não pode ser nulo");

            var columnWidths = new float[Columns.Length];

            for (int i = 0; i < Columns.Length; i++)
                columnWidths[i] = Columns[i].WidthPercentage;

            var table = new PdfPTable(columnWidths);
            table.WidthPercentage = 100;
            table.HeaderRows = HeaderRows;

            if (!string.IsNullOrEmpty(Title))
            {
                table.DefaultCell.BorderWidth = 0;
                table.DefaultCell.Colspan = Columns.Length;
                table.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                table.DefaultCell.Padding = 7;

                var paragTitle = Template.GetChunk(Title);
                paragTitle.Font = Template.GetFont();
                paragTitle.Font.SetStyle(Font.BOLD);
                paragTitle.Font.Size = Template.DefaultFontSize + (Template.DefaultFontSize * 0.5f);
                table.AddCell(new Phrase(paragTitle));
                table.DefaultCell.Colspan = 0;
            }

            var hasFooter = false;
            foreach (var column in Columns)
            {
                table.DefaultCell.HorizontalAlignment = column.Alignment;
                table.AddCell(Table.GetHeaderCell(column.ColumnHeader, Template, column.Alignment, Style));
                hasFooter = (hasFooter || column.ColumnFooter != null);
            }

            var j = 0;
            if (Items != null && Items.Count > 0)
            {
                var groupProperty = Items[0].GetType().GetProperty(groupingMap?.GroupPropertyPath ?? "-");

                if (groupingMap != null && groupProperty == null)
                    throw new Exception($"A propriedade '{ groupingMap.GroupPropertyPath }' não existe nos elementos da coleção");

                // Carregar propriedades de reflexão de agrupamento
                for (uint i = 0; i < Columns.Length; i++)
                    if (groupingMap != null && groupingMap.SubTotals.ContainsKey(i))
                        groupingMap.SubTotals[i].ColumnProperty = Items[0].GetType().GetProperty(Columns[i].PropertyPath);

                var currentGroupKey = "!Hey,do_you_know_that@#$Nothing_in_the_world_can_be_like_this_initializer_text?!";
                bool firstGroup = true, lastGroup = false;
                foreach (var item in Items)
                {
                    if (groupingMap != null)
                    {

                        // Se o valor da propriedade chave do agrupamento mudou, criar linha de subtotal e próximo header
                        if (!currentGroupKey.Equals(groupProperty?.GetValue(item)?.ToString()))
                        {
                            AddGroupStrip(table, groupProperty?.GetValue(item)?.ToString(), firstGroup, lastGroup, groupingMap, Columns, Template, Style);
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
                        for (uint i = 0; i < Columns.Length; i++)
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

                    foreach (var column in Columns)
                    {
                        table.DefaultCell.HorizontalAlignment = column.Alignment;
                        table.AddCell(
                                Table.GetCell(GetValue(item, column.PropertyPath, column.StringFormat),
                                                      Template,
                                                      column.Alignment,
                                                      Style,
                                                      (j % 2 == 0)));
                    }
                    j++;
                }

                //Adicionar último group strip se houver
                if (groupingMap != null)
                    AddGroupStrip(table, "", false, true, groupingMap, Columns, Template, Style);
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(emptyText))
                {
                    table.DefaultCell.BorderWidth = 0;
                    table.DefaultCell.Colspan = Columns.Length;
                    table.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.DefaultCell.Padding = 7;
                    table.AddCell(Template.GetPhrase(emptyText, Font.NORMAL, Template.DefaultFontSize));
                }
            }

            if (hasFooter)
            {
                for (int i = 0; i < Columns.Length; i++)
                    table.AddCell(Table.GetHeaderCell(Columns[i].ColumnFooter, Template, Columns[i].Alignment, Style));

                table.DefaultCell.BorderWidth = 0;
                table.CompleteRow();
            }

            return table;
        }

        /// <summary>
        /// Adiciona uma tabela ao documento. Utiliza como fonte padrão a fonte especificada no template.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Items">Coleção de objetos para preencher a tabela</param>
        /// <param name="Columns">Colunas da tabela</param>
        /// <param name="Style">Estilo da tabela</param>
        /// <param name="HeaderRows">Linhas que serão repetidas caso a tabela estoure uma página</param>
        /// <param name="Title">Título para a tabela</param>
        /// <param name="groupingMap"></param>
        public void AddTable<T>(List<T> Items, ColumnMap[] Columns, TableStyles Style, int HeaderRows = 0, string Title = null, GroupingMap groupingMap = null, string emptyText = null)
        {
            Add(CreateTable(Items, Columns, Style, HeaderRows, Title, groupingMap, emptyText));
        }

        /// <summary>
        /// Adiciona uma faixa de agrupamento à tabela especificada que representa um sumário e um cabeçalho - grupo anterior e corrente respectivamente
        /// </summary>
        private void AddGroupStrip(PdfPTable table, string currentGroupKey, bool firstGroup, bool lastGroup, GroupingMap groupingMap, ColumnMap[] Columns, TemplateBase Template, TableStyles Style)
        {
            if (string.IsNullOrWhiteSpace(currentGroupKey))
                currentGroupKey = "-";

            // Subtotal (O primeiro group strip não é precedido por um sumário)
            if (!firstGroup)
                for (uint i = 0; i < Columns.Length; i++)
                {
                    table.DefaultCell.HorizontalAlignment = Columns[i].Alignment;
                    if (groupingMap.SubTotals.ContainsKey(i))
                        table.AddCell(Table.GetHeaderCell(
                                                    string.Format(Columns[i].StringFormat, groupingMap.SubTotals[i].Subtotal),
                                                    Template,
                                                    Columns[i].Alignment,
                                                    Style));
                    else
                        table.AddCell(Table.GetHeaderCell(i == 0 ? "Subtotal" : " ", Template, Columns[i].Alignment, Style));
                }

            // Cabeçalho (o último group strip não é seguido por um sumário)
            if (!lastGroup)
            {
                var cell = Table.GetHeaderCell(currentGroupKey, Template, Element.ALIGN_LEFT, Style);
                cell.BackgroundColor = BaseColor.WHITE;
                cell.Colspan = Columns.Length;
                table.AddCell(cell);
            }

            if (!groupingMap.GlobalSubtotals)
                groupingMap.ResetSubTotals();
        }

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

        /// <summary>
        /// Fecha o documento e recupera o streaming de bytes que o representa
        /// </summary>
        public MemoryStream GetStream()
        {
            if (PageNumber == 0 && IsOpen())
                Add(new Chunk(""));

            Close();
            DocumentStream.Position = 0;

            return DocumentStream;
        }

        /// <summary>
        /// Fecha o documento e salva o arquivo PDF no caminho especificado
        /// </summary>
        /// <param name="filepath">Caminho completo para o arquivo PDF gerado</param>
        /// <param name="filemode">Modo como o Windows deve tratar a escrita em disco. Use 'Create' ou 'CreateNew' somente.</param>
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
