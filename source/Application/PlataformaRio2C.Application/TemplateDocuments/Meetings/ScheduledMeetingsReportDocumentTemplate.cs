// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 06-26-2023
//
// Last Modified By : Renan Valentim
// Last Modified On : 06-26-2023
// ***********************************************************************
// <copyright file="ScheduledMeetingsReportDocumentTemplate.cs" company="Softo">
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
using System.Collections.Generic;
using System.Linq;
using PlataformaRio2C.Infra.Report.Models;
using System.Globalization;
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Application.TemplateDocuments
{
    /// <summary>ScheduledMeetingsReportDocumentTemplate</summary>
    public class ScheduledMeetingsReportDocumentTemplate : TemplateBase
    {
        public readonly List<NegotiationReportGroupedByDateDto> _negotiationReportGroupedByDateDtos;
        private readonly string _languageCode;
        private readonly CultureInfo _cultureInfo;
        private readonly EditionDto _editionDto;

        private readonly BaseColor _grayBackground;
        private readonly BaseColor _lightGrayBackground;
        private readonly BaseColor _whiteSmokeBackground;
        private readonly BaseColor _ligthYellowBackground;

        private readonly Font _font;
        private readonly Font _fontText;
        private readonly Font _fontLabel;
        private readonly Font _fontTextWhite;

        private readonly float[] _columnsWidths = new float[] { 5, 5, 30, 30, 30 };

        /// <summary>
        /// Initializes a new instance of the <see cref="ScheduledMeetingsReportDocumentTemplate"/> class.
        /// </summary>
        /// <param name="negotiationReportGroupedByDateDtos">The negotiation report grouped by date dtos.</param>
        /// <param name="languageCode">The language code.</param>
        /// <param name="editionDto">The edition dto.</param>
        public ScheduledMeetingsReportDocumentTemplate(List<NegotiationReportGroupedByDateDto> negotiationReportGroupedByDateDtos, string languageCode, EditionDto editionDto)
        {
            _negotiationReportGroupedByDateDtos = negotiationReportGroupedByDateDtos;
            _languageCode = languageCode;
            _cultureInfo = CultureInfo.CreateSpecificCulture(languageCode);
            _editionDto = editionDto;

            _grayBackground = new BaseColor(128, 128, 128);
            _lightGrayBackground = new BaseColor(209, 209, 209);
            _whiteSmokeBackground = new BaseColor(System.Drawing.Color.WhiteSmoke);
            _ligthYellowBackground = new BaseColor(255, 255, 224);

            #region Fonts

            var grayBaseColor = new BaseColor(72, 71, 91);
            var whiteBaseColor = new BaseColor(System.Drawing.Color.White);
            var redBaseColor = new BaseColor(244, 0, 105);
            var baseFontName = "Calibri";
            var baseFontSize = DefaultFontSize + 3f;
            var baseHeaderFontSize = DefaultFontSize + 5f;
            var baseSubHeaderFontSize = DefaultFontSize + 4f;

            _font = FontFactory.GetFont(baseFontName, baseFontSize, Font.BOLD, grayBaseColor);
            _fontText = FontFactory.GetFont(baseFontName, baseFontSize, Font.BOLD, grayBaseColor);
            _fontLabel = FontFactory.GetFont(baseFontName, baseFontSize, Font.NORMAL, grayBaseColor);
            _fontTextWhite = FontFactory.GetFont(baseFontName, baseHeaderFontSize, Font.BOLD, whiteBaseColor);

            #endregion

            ShowDefaultBackground = true;
            ShowDefaultFooter = false;
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

            var table = new PdfPTable(_columnsWidths.Length);
            table.WidthPercentage = 100;
            table.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
            table.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            table.DefaultCell.BorderColor = new BaseColor(System.Drawing.Color.Transparent);

            //Add table Rows
            if (this._negotiationReportGroupedByDateDtos?.Any() == true)
            {
                foreach (var negotiationReportGroupedByDateDto in this._negotiationReportGroupedByDateDtos)
                {
                    #region Date Table Header

                    table.DefaultCell.Colspan = _columnsWidths.Length;
                    table.DefaultCell.BackgroundColor = _grayBackground;

                    string negotiationDate = $@"{negotiationReportGroupedByDateDto.Date.ToShortDateString()} - {negotiationReportGroupedByDateDto.Date.ToString("dddd")}";
                    table.AddCell(this.GetTableHeader(new Phrase(new Chunk($" {_cultureInfo.TextInfo.ToTitleCase(negotiationDate)}", _fontTextWhite))));

                    table.DefaultCell.BackgroundColor = null;

                    #endregion

                    foreach (var negotiationReportGroupedByRoomDto in negotiationReportGroupedByDateDto.NegotiationReportGroupedByRoomDtos)
                    {
                        #region Room Table Header

                        table.DefaultCell.Colspan = _columnsWidths.Length;
                        table.DefaultCell.BackgroundColor = _lightGrayBackground;

                        string roomName = negotiationReportGroupedByRoomDto?.GetRoomNameByLanguageCode(_languageCode)?.Value;
                        string roomType = negotiationReportGroupedByRoomDto.Room.IsVirtualMeeting ? Labels.Virtual : Labels.Presential;
                        table.AddCell(this.GetTableHeader(new Phrase(new Chunk($"{roomName} ({roomType})", _fontText))));

                        table.DefaultCell.BackgroundColor = null;

                        #endregion

                        foreach (var negotiationReportGroupedByStartDateDto in negotiationReportGroupedByRoomDto.NegotiationReportGroupedByStartDateDtos)
                        {
                            #region Round Start Date Table Header

                            table.DefaultCell.Colspan = _columnsWidths.Length;
                            table.DefaultCell.BackgroundColor = _whiteSmokeBackground;

                            string roundStartDate = $"{Labels.Round} {negotiationReportGroupedByStartDateDto.Negotiations.FirstOrDefault().RoundNumber} ({negotiationReportGroupedByStartDateDto.StartDate.ToString("HH:mm")} - {negotiationReportGroupedByStartDateDto.EndDate.ToString("HH:mm")})";
                            table.AddCell(this.GetTableHeader(new Phrase(new Chunk(roundStartDate, _fontText))));

                            table.DefaultCell.BackgroundColor = null;

                            #endregion

                            foreach (var negotiation in negotiationReportGroupedByStartDateDto.Negotiations)
                            {
                                table.AddCell(this.GetTableDetails(negotiation, negotiation.Uid == negotiationReportGroupedByStartDateDto.Negotiations.Last().Uid));
                            }
                        }
                    }
                }
            }

            document.Add(table);
        }

        #endregion

        /// <summary>Gets the first page information.</summary>
        /// <param name="document">The document.</param>
        /// <returns></returns>
        private Paragraph GetFirstPageInfo(ref PlataformaRio2CDocument document)
        {
            var paragraph = new Paragraph();

            paragraph.Add(GetChunk($"{Labels.ScheduledNegotiationsReport}", DefaultFontSize + 12f, Font.BOLD));
            paragraph.IndentationLeft = 170;
            paragraph.SpacingBefore = 150;
            paragraph.SetLeading(1.0f, 2.5f);
            paragraph.Alignment = Element.ALIGN_RIGHT;
            document.Add(paragraph);
            paragraph.Clear();

            paragraph.Add(GetChunk(_editionDto.Edition.Name, DefaultFontSize + 6f, Font.NORMAL));
            paragraph.Alignment = Element.ALIGN_RIGHT;
            paragraph.IndentationLeft = 170;
            paragraph.SpacingBefore = 20;
            paragraph.SetLeading(1.0f, 1.0f);
            document.Add(paragraph);
            paragraph.Clear();

            return paragraph;
        }

        /// <summary>
        /// Gets the table header.
        /// </summary>
        /// <param name="phrase">The phrase.</param>
        /// <returns></returns>
        private PdfPTable GetTableHeader(Phrase phrase)
        {
            var tableCell = new PdfPCell(phrase);
            tableCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            tableCell.FixedHeight = 25;

            //Add a middle-align cell to the new table
            var table = new PdfPTable(1);
            table.AddCell(tableCell);

            return table;
        }

        /// <summary>
        /// Gets the details row.
        /// </summary>
        /// <param name="negotiation">The negotiation dto.</param>
        /// <param name="isLastItem">if set to <c>true</c> [is last item].</param>
        /// <returns>PdfPTable.</returns>
        private PdfPTable GetTableDetails(Negotiation negotiation, bool isLastItem)
        {
            var negotiationsTable = new PdfPTable(2);
            negotiationsTable.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
            negotiationsTable.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            negotiationsTable.DefaultCell.FixedHeight = 25;

            this.AddEmptyRow(ref negotiationsTable);

            negotiationsTable.DefaultCell.Colspan = 0;
            this.AddDetailsRow(ref negotiationsTable, Labels.Table, negotiation.TableNumber.ToString(), _fontText);
            this.AddDetailsRow(ref negotiationsTable, Labels.Type, negotiation.IsAutomatic ? Labels.Automatic : Labels.Manual, _fontText);
            this.AddDetailsRow(ref negotiationsTable, Labels.Player, negotiation.ProjectBuyerEvaluation?.BuyerAttendeeOrganization?.Organization?.TradeName, _fontText);
            this.AddDetailsRow(ref negotiationsTable, Labels.Producer, negotiation.ProjectBuyerEvaluation?.Project?.SellerAttendeeOrganization?.Organization?.TradeName, _fontText);

            negotiationsTable.DefaultCell.Colspan = 2;
            this.AddDetailsRow(ref negotiationsTable, Labels.Project, negotiation.ProjectBuyerEvaluation?.Project?.GetTitleByLanguageCode(_languageCode)?.ToUpper(), _fontText);

            negotiationsTable.DefaultCell.Colspan = 0;
            this.AddEmptyRow(ref negotiationsTable);

            if (negotiation.AttendeeNegotiationCollaborators?.Any() == true)
            {
                var participantsTable = new PdfPTable(2);
                participantsTable.WidthPercentage = 100;
                participantsTable.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                participantsTable.DefaultCell.FixedHeight = 25;
                participantsTable.DefaultCell.PaddingLeft = 10;
                participantsTable.DefaultCell.PaddingRight = 10;

                // Participants Header
                participantsTable.DefaultCell.Colspan = 2;
                participantsTable.DefaultCell.BackgroundColor = _ligthYellowBackground;
                participantsTable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                participantsTable.AddCell(new Phrase(new Chunk(Labels.Participants.ToUpper(), _fontText)));

                participantsTable.DefaultCell.Colspan = 0;
                participantsTable.DefaultCell.BackgroundColor = _whiteSmokeBackground;

                // Table Headers
                participantsTable.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
                participantsTable.AddCell(new Phrase(new Chunk(Labels.Name, _fontText)));

                participantsTable.DefaultCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                participantsTable.AddCell(new Phrase(new Chunk(Labels.AudiovisualVirtualMeetingJoinDate, _fontText)));

                // Table Details
                foreach (var attendeeNegotiationCollaborator in negotiation.AttendeeNegotiationCollaborators)
                {
                    participantsTable.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
                    participantsTable.AddCell(new Phrase(new Chunk(attendeeNegotiationCollaborator?.AttendeeCollaborator?.Collaborator?.GetFullName(), _fontLabel)));

                    participantsTable.DefaultCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    participantsTable.AddCell(new Phrase(new Chunk(attendeeNegotiationCollaborator?.CreateDate.ToStringHourMinute(), _fontLabel)));
                }

                // Adds Participants Table to Negotiations Table
                PdfPCell cell = new PdfPCell(participantsTable);
                cell.Colspan = _columnsWidths.Length;
                cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;

                negotiationsTable.DefaultCell.Colspan = _columnsWidths.Length;
                negotiationsTable.AddCell(cell);

                // Reset Background Color
                participantsTable.DefaultCell.BackgroundColor = null;
            }

            if (!isLastItem)
            {
                Paragraph p = new Paragraph(new Chunk(new LineSeparator(0.0F, 100.0F, new BaseColor(232, 234, 240), Element.ALIGN_LEFT, 1)));
                negotiationsTable.AddCell(p);
            }

            return negotiationsTable;
        }

        /// <summary>
        /// Adds the empty row.
        /// </summary>
        /// <param name="table">The table.</param>
        private void AddEmptyRow(ref PdfPTable table)
        {
            var defaultFixedHeight = table.DefaultCell.FixedHeight;

            table.DefaultCell.FixedHeight = 2;
            table.DefaultCell.Colspan = _columnsWidths.Length;
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
            //table.DefaultCell.Colspan = _columnsWidths.Length;

            var labelChunk = new Chunk($"   {label}: ", _fontLabel);
            var textChunk = new Chunk($" {text}", textFont);
            var phrase = new Phrase(labelChunk);
            phrase.Add(textChunk);
            table.AddCell(phrase);
        }
    }
}
