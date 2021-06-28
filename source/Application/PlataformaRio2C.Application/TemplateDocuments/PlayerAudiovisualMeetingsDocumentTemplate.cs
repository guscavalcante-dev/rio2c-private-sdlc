// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 06-25-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 06-25-2021
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
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using System.Collections.Generic;
using System.Linq;
using PlataformaRio2C.Infra.Report.Models;
using iTextSharp.text.html.simpleparser;
using System.IO;
using iTextSharp.tool.xml;
using System.Globalization;

namespace PlataformaRio2C.Application.TemplateDocuments
{
    /// <summary>PlayerAudiovisualMeetingsDocumentTemplate</summary>
    public class PlayerAudiovisualMeetingsDocumentTemplate : TemplateBase
    {
        public List<NegotiationDto> NegotiationsDtos { get; private set; }
        private Font _font;
        private Font _fontLabel;
        private Font _fontChip;
        private string _languageCode;
        private CultureInfo _cultureInfo;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerAudiovisualMeetingsDocumentTemplate"/> class.
        /// </summary>
        /// <param name="project">The project.</param>
        public PlayerAudiovisualMeetingsDocumentTemplate(List<NegotiationDto> negotiationsDtos, string languageCode)
        {
            this.NegotiationsDtos = negotiationsDtos;
            _font = new Font(BaseFont.CreateFont("Helvetica", BaseFont.WINANSI, true));
            _font.Color = BaseColor.DARK_GRAY;

            ShowDefaultBackground = true;
            ShowDefaultFooter = false;

            _fontLabel = new Font(Font.FontFamily.HELVETICA, DefaultFontSize + 4f, Font.BOLD, BaseColor.DARK_GRAY);
            _fontChip = new Font(Font.FontFamily.HELVETICA, DefaultFontSize + 4f, Font.NORMAL, BaseColor.DARK_GRAY);

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
            document.Add(Chunk.NEXTPAGE);
            document.Add(Chunk.NEWLINE);

            float[] columnsWidths = new float[] { 5, 5, 30, 30, 30 };
            var table = new PdfPTable(columnsWidths);
            table.WidthPercentage = 100;
            table.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
            table.DefaultCell.VerticalAlignment = Element.ALIGN_CENTER;
            table.DefaultCell.Border = 1;
            table.DefaultCell.BorderColor = new BaseColor(System.Drawing.Color.Transparent);
            table.DefaultCell.FixedHeight = 25;

            //Add table Rows
            if (this.NegotiationsDtos?.Any() == true)
            {
                var negotiationsDtosGroupedByDate = this.NegotiationsDtos.GroupBy(ndto => ndto.Negotiation.StartDate.ToUserTimeZone().Date);
                foreach (var groupedNegotiationDtos in negotiationsDtosGroupedByDate)
                {
                    table.DefaultCell.Colspan = columnsWidths.Length;
                    table.DefaultCell.BackgroundColor = BaseColor.GRAY;
                    table.AddCell(new Phrase(new Chunk($"{_cultureInfo.TextInfo.ToTitleCase(groupedNegotiationDtos.Key.ToString("dddd, dd MMMM yyyy"))}", new Font(_fontLabel) { Color = BaseColor.WHITE })));

                    foreach (var negotiationDto in groupedNegotiationDtos)
                    {
                        this.AddEmptyCells(ref table, 1);
                        table.DefaultCell.Colspan = columnsWidths.Length - 1;
                        table.DefaultCell.BackgroundColor = BaseColor.LIGHT_GRAY;
                        var labelChunk = new Chunk($"• {Labels.Project}: ", _fontLabel);
                        var textChunk = new Chunk($"{negotiationDto.ProjectBuyerEvaluationDto?.ProjectDto.GetTitleDtoByLanguageCode(_languageCode).ProjectTitle.Value}", _font);
                        var phrase = new Phrase(labelChunk);
                        phrase.Add(textChunk);
                        table.AddCell(phrase);

                        this.AddDetailsRow(ref table, Labels.Producer, negotiationDto.ProjectBuyerEvaluationDto?.ProjectDto.SellerAttendeeOrganizationDto.Organization.TradeName);
                        this.AddDetailsRow(ref table, Labels.Hour, $"{negotiationDto.Negotiation?.StartDate.ToUserTimeZone().ToShortTimeString()} - {negotiationDto.Negotiation?.EndDate.ToUserTimeZone().ToShortTimeString()}");
                        this.AddDetailsRow(ref table, Labels.Room, negotiationDto.RoomDto?.GetRoomNameByLanguageCode(_languageCode).RoomName.Value);
                        this.AddDetailsRow(ref table, Labels.Table, negotiationDto.Negotiation.TableNumber.ToString());
                    }
                }
            }

            document.Add(table);
        }

        #endregion

        /// <summary>
        /// Adds the row.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="label">The label.</param>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        private void AddDetailsRow(ref PdfPTable table, string label, string text)
        {
            this.AddEmptyCells(ref table, 2);
            table.DefaultCell.Colspan = 3;

            table.DefaultCell.BackgroundColor = new BaseColor(System.Drawing.Color.WhiteSmoke);
            var labelChunk = new Chunk($"• {label}: ", _fontLabel);
            var textChunk = new Chunk($"{text}", _font);
            var phrase = new Phrase(labelChunk);
            phrase.Add(textChunk);
            table.AddCell(phrase);
        }

        /// <summary>
        /// Adds the empty cells.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="count">The count.</param>
        private void AddEmptyCells(ref PdfPTable table, int count)
        {
            table.DefaultCell.BackgroundColor = null;
            table.DefaultCell.Colspan = 0;

            for (int i = 0; i < count; i++)
            {
                table.AddCell("");
            }
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

            paragraph.Add(GetChunk(this.NegotiationsDtos.FirstOrDefault()?.ProjectBuyerEvaluationDto?.BuyerAttendeeOrganizationDto?.Organization?.Name, DefaultFontSize + 6f, Font.NORMAL));
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
