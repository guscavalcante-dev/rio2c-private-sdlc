// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 06-29-2023
//
// Last Modified By : Renan Valentim
// Last Modified On : 05-01-2025
// ***********************************************************************
// <copyright file="PlayerAudiovisualMeetingsDocumentTemplate.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using PlataformaRio2C.Infra.Report.Models;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace PlataformaRio2C.Application.TemplateDocuments
{
    /// <summary>PlayerAudiovisualMeetingsDocumentTemplate</summary>
    public class PlayerAudiovisualMeetingsDocumentTemplate : TemplateBase
    {
        public List<NegotiationDto> NegotiationsDtos { get; private set; }
        private Font _font;
        private Font _fontText;
        private Font _fontLabel;
        private string _languageCode;
        private CultureInfo _cultureInfo;
        private BaseColor _grayBackground;
        private Font _fontProjectName;
        private float[] _columnsWidths = new float[] { 5, 5, 30, 30, 30 };

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerAudiovisualMeetingsDocumentTemplate"/> class.
        /// </summary>
        /// <param name="project">The project.</param>
        public PlayerAudiovisualMeetingsDocumentTemplate(List<NegotiationDto> negotiationsDtos, string languageCode)
        {
            this.NegotiationsDtos = negotiationsDtos;

            var baseFont = BaseFont.CreateFont("Helvetica", BaseFont.WINANSI, true);
            var grayBaseColor = new BaseColor(72, 71, 91);
            var redBaseColor = new BaseColor(244, 0, 105);
            _grayBackground = new BaseColor(248, 248, 251);

            _font = new Font(baseFont, DefaultFontSize + 3f, Font.BOLD, grayBaseColor);

            _fontLabel = new Font(baseFont, DefaultFontSize + 3f, Font.NORMAL, grayBaseColor);
            _fontText = new Font(baseFont, DefaultFontSize + 3f, Font.BOLD, grayBaseColor);
            _fontProjectName = new Font(baseFont, DefaultFontSize + 3f, Font.BOLD, redBaseColor);

            ShowDefaultBackground = true;
            ShowDefaultFooter = false;

            _languageCode = languageCode;
            _cultureInfo = CultureInfo.CreateSpecificCulture(languageCode);
        }

        #region Overrides

        /// <summary>Retorna a fonte padrão do template</summary>
        /// <param name="fontSize">Tamanho da fonte</param>
        /// <param name="fontStyle">Estilo da fonte</param>
        /// <returns></returns>
        public override Font GetFont(float fontSize, int fontStyle)
        {
            _font.Size = fontSize;
            _font.SetStyle(fontStyle);

            return _font;
        }

        /// <summary>Retorna uma instância de parágrafo com o formato do template</summary>
        /// <param name="Text">Conteúdo do parágafo</param>
        /// <returns></returns>
        public override Paragraph GetParagraph(string Text = null)
        {
            var paragraph = new Paragraph();
            paragraph.Alignment = Element.ALIGN_CENTER;
            paragraph.SpacingBefore = 5f;
            paragraph.Font = GetFont();
            paragraph.Add(Text);

            return paragraph;
        }

        /// <summary>Prepares the specified document.</summary>
        /// <param name="document">The document.</param>
        public override void Prepare(PlataformaRio2CDocument document)
        {
            this.GetFirstPageInfo(ref document);
            document.SetMargins(36, 36, 36, 36);
            document.NewPage();

            var table = new PdfPTable(_columnsWidths);
            table.WidthPercentage = 100;
            table.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
            table.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            table.DefaultCell.Border = 1;
            table.DefaultCell.BorderColor = new BaseColor(System.Drawing.Color.Transparent);

            //Add table Rows
            if (this.NegotiationsDtos?.Any() == true)
            {
                var negotiationsDtosGroupedByDate = this.NegotiationsDtos.GroupBy(ndto => ndto.Negotiation.StartDate.ToBrazilTimeZone().Date);
                foreach (var groupedNegotiationDtos in negotiationsDtosGroupedByDate)
                {
                    table.DefaultCell.Colspan = _columnsWidths.Length;
                    table.DefaultCell.BackgroundColor = _grayBackground;

                    //Add a middle-align cell to the new table
                    var tableHeader = new PdfPTable(1);
                    //tableHeader.DefaultCell.FixedHeight = 50;
                    var tableHeaderCell = new PdfPCell(new Phrase(new Chunk($" {_cultureInfo.TextInfo.ToTitleCase(groupedNegotiationDtos.Key.ToString("dddd, dd MMMM yyyy"))}", new Font(_fontText))));
                    tableHeaderCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    tableHeaderCell.FixedHeight = 25;
                    tableHeader.AddCell(tableHeaderCell);

                    table.AddCell(tableHeader);
                    table.DefaultCell.BackgroundColor = null;

                    foreach (var negotiationDto in groupedNegotiationDtos)
                    {
                        table.AddCell(GetDetailsRow(negotiationDto, negotiationDto == groupedNegotiationDtos.Last()));
                    }
                }
            }

            document.Add(table);
        }

        #endregion

        /// <summary>
        /// Gets the details row.
        /// </summary>
        /// <param name="negotiationDto">The negotiation dto.</param>
        /// <param name="isLastItem">if set to <c>true</c> [is last item].</param>
        /// <returns>PdfPTable.</returns>
        private PdfPTable GetDetailsRow(NegotiationDto negotiationDto, bool isLastItem)
        {
            var innerTable = new PdfPTable(_columnsWidths);
            innerTable.WidthPercentage = 100;
            innerTable.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
            innerTable.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            innerTable.DefaultCell.Border = 1;
            innerTable.DefaultCell.BorderColor = new BaseColor(System.Drawing.Color.Transparent);
            innerTable.DefaultCell.FixedHeight = 25;

            this.AddEmptyRow(ref innerTable);
            this.AddDetailsRow(ref innerTable, Labels.Project, negotiationDto.ProjectBuyerEvaluationDto?.ProjectDto.GetTitleDtoByLanguageCode(_languageCode).ProjectTitle.Value.ToUpper(), _fontProjectName);
            this.AddDetailsRow(ref innerTable, Labels.Producer, negotiationDto.ProjectBuyerEvaluationDto?.ProjectDto.SellerAttendeeOrganizationDto.Organization.TradeName, _fontText);
            this.AddDetailsRow(ref innerTable, Labels.Hour, $"{negotiationDto.Negotiation?.StartDate.ToBrazilTimeZone().ToShortTimeString()} - {negotiationDto.Negotiation?.EndDate.ToBrazilTimeZone().ToShortTimeString()}", _fontText);
            this.AddDetailsRow(ref innerTable, Labels.Room, negotiationDto.RoomDto?.GetRoomNameByLanguageCode(_languageCode).RoomName.Value, _fontText);
            this.AddDetailsRow(ref innerTable, Labels.Table, negotiationDto.Negotiation.TableNumber.ToString(), _fontText);
            this.AddEmptyRow(ref innerTable);

            if (!isLastItem)
            {
                Paragraph p = new Paragraph(new Chunk(new LineSeparator(0.0F, 100.0F, new BaseColor(232, 234, 240), Element.ALIGN_LEFT, 1)));
                innerTable.AddCell(p);
            }

            return innerTable;
        }

        /// <summary>
        /// Adds the empty row.
        /// </summary>
        /// <param name="table">The table.</param>
        private void AddEmptyRow(ref PdfPTable table)
        {
            var defaultFixedHeight = table.DefaultCell.FixedHeight;

            table.DefaultCell.FixedHeight = 2;
            table.DefaultCell.Colspan = 5;
            table.AddCell(" ");

            table.DefaultCell.FixedHeight = defaultFixedHeight;
        }

        /// <summary>
        /// Adds the row.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="label">The label.</param>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        private void AddDetailsRow(ref PdfPTable table, string label, string text, Font textFont)
        {
            table.DefaultCell.Colspan = 5;

            var labelChunk = new Chunk($"   {label}: ", _fontLabel);
            var textChunk = new Chunk($" {text}", textFont);
            var phrase = new Phrase(labelChunk);
            phrase.Add(textChunk);
            table.AddCell(phrase);
        }

        /// <summary>Gets the first page information.</summary>
        /// <param name="document">The document.</param>
        /// <returns></returns>
        private Paragraph GetFirstPageInfo(ref PlataformaRio2CDocument document)
        {
            var paragraph = new Paragraph();

            paragraph.Add(GetChunk($"{Labels.MyRio2C} - {Labels.ScheduledNegotiations}", DefaultFontSize + 12f, Font.BOLD));
            paragraph.IndentationLeft = 170;
            paragraph.SpacingBefore = 150;
            paragraph.SetLeading(1.0f, 2.5f);
            paragraph.Alignment = Element.ALIGN_RIGHT;
            document.Add(paragraph);
            paragraph.Clear();

            paragraph.Add(GetChunk(this.NegotiationsDtos.FirstOrDefault()?.ProjectBuyerEvaluationDto?.BuyerAttendeeOrganizationDto?.Organization?.TradeName, DefaultFontSize + 6f, Font.NORMAL));
            paragraph.Alignment = Element.ALIGN_RIGHT;
            paragraph.IndentationLeft = 170;
            paragraph.SpacingBefore = 20;
            paragraph.SetLeading(1.0f, 1.0f);
            document.Add(paragraph);
            paragraph.Clear();

            return paragraph;
        }
    }
}
