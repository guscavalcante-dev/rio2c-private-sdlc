// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : William Sergio Almado Junior
// Created          : 12-27-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 05-01-2025
// ***********************************************************************
// <copyright file="ProjectDocumentTemplate.cs" company="Softo">
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

namespace PlataformaRio2C.Application.TemplateDocuments
{
    /// <summary>ProjectDocumentTemplate</summary>
    public class ProjectDocumentTemplate : TemplateBase
    {
        public ProjectDto Project { get; private set; }
        private Font _font;
        private Font _fontLabel;
        private Font _fontChip;

        /// <summary>Initializes a new instance of the <see cref="ProjectDocumentTemplate"/> class.</summary>
        public ProjectDocumentTemplate(ProjectDto project)
        {
            this.Project = project;
            _font = new Font(BaseFont.CreateFont("Helvetica", BaseFont.WINANSI, true));
            _font.Color = BaseColor.DARK_GRAY;

            ShowDefaultBackground = true;
            ShowDefaultFooter = false;

            _fontLabel = new Font(Font.FontFamily.HELVETICA, DefaultFontSize + 4f, Font.BOLD, BaseColor.DARK_GRAY);
            _fontChip = new Font(Font.FontFamily.HELVETICA, DefaultFontSize + 4f, Font.NORMAL, BaseColor.DARK_GRAY);
        }

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
            var paragraph = GetFirstPageInfo(ref document);

            document.Add(Chunk.NEXTPAGE);
            paragraph = GetSummaryInfo(ref document, paragraph);
            paragraph = GetLoglineInfo(ref document, paragraph);
            paragraph = GetProductionPlanInfo(ref document, paragraph);

            //document.Add(Chunk.NEXTPAGE);
            paragraph = GetFormatPlatformInfo(ref document, paragraph);
            paragraph = GetProjectValuesInfo(ref document, paragraph);
            GetOtherInfos(ref document, paragraph);
        }

        /// <summary>Gets the first page information.</summary>
        /// <param name="document">The document.</param>
        /// <returns></returns>
        private Paragraph GetFirstPageInfo(ref PlataformaRio2CDocument document)
        {
            var paragraph = new Paragraph();

            paragraph.Add(GetChunk(this.Project.GetTitleDtoByLanguageCode(Language.Portuguese.Code).ProjectTitle.Value.ToUpper(), DefaultFontSize + 12f, Font.BOLD));
            paragraph.IndentationLeft = 170;
            paragraph.SpacingBefore = 85;
            paragraph.SetLeading(1.0f, 2.5f);
            paragraph.Alignment = Element.ALIGN_CENTER;
            document.Add(paragraph);
            paragraph.Clear();

            paragraph.Add(GetChunk(this.Project.GetTitleDtoByLanguageCode(Language.English.Code).ProjectTitle.Value.ToUpper(), DefaultFontSize + 10f, Font.ITALIC));
            paragraph.IndentationLeft = 170;
            paragraph.SpacingBefore = 10;
            paragraph.Alignment = Element.ALIGN_CENTER;
            document.Add(paragraph);
            paragraph.Clear();

            paragraph.Add(GetChunk("Produtora/Empresa: ", DefaultFontSize + 6f, Font.NORMAL));
            paragraph.Add(GetChunk(this.Project.SellerAttendeeOrganizationDto.Organization.TradeName, DefaultFontSize + 6f, Font.NORMAL));
            paragraph.Alignment = Element.ALIGN_LEFT;
            paragraph.IndentationLeft = 170;
            paragraph.SpacingBefore = 20;
            paragraph.SetLeading(1.0f, 1.0f);
            document.Add(paragraph);
            paragraph.Clear();


            var fontLabelGenre = new Font(Font.FontFamily.HELVETICA, DefaultFontSize + 6f, Font.BOLD, BaseColor.DARK_GRAY);
            var fontChipGenre = new Font(Font.FontFamily.HELVETICA, DefaultFontSize + 4f, Font.BOLD, BaseColor.DARK_GRAY);

            var tableGenre = new PdfPTable(new float[] { 100 });
            tableGenre.WidthPercentage = 100;
            tableGenre.HorizontalAlignment = Element.ALIGN_LEFT;
            tableGenre.DefaultCell.VerticalAlignment = Element.ALIGN_TOP;

            var chips = new List<Chunk>();
            var projectInterestsDtos = this.Project.GetAllInterestsByInterestGroupUid(InterestGroup.AudiovisualGenre.Uid);
            if (projectInterestsDtos?.Any() == true)
            {
                foreach (var projectInterestDto in projectInterestsDtos)
                {
                    chips.Add(new Chunk(projectInterestDto.Interest.Name.GetSeparatorTranslation(Language.English.Code, Language.Separator) +
                        " | " +
                       projectInterestDto.Interest.Name.GetSeparatorTranslation(Language.Portuguese.Code, Language.Separator), fontChipGenre)
                       );
                }

                paragraph.Add(GetChips("Gênero: ", chips, fontLabelGenre, tableGenre));
                paragraph.IndentationLeft = 170;
                paragraph.SpacingBefore = 10;
                document.Add(paragraph);
                paragraph.Clear();

            }

            return paragraph;
        }

        /// <summary>Gets the summary information.</summary>
        /// <param name="document">The document.</param>
        /// <param name="paragraph">The paragraph.</param>
        /// <returns></returns>
        private Paragraph GetSummaryInfo(ref PlataformaRio2CDocument document, Paragraph paragraph)
        {
            paragraph.Add(GetChunk("Resumo", DefaultFontSize + 4f, Font.BOLD));
            paragraph.IndentationLeft = 15;
            paragraph.IndentationRight = 15;
            paragraph.SpacingBefore = 30;
            paragraph.SetLeading(1, 1);
            document.Add(paragraph);
            paragraph.Clear();

            var ls = new LineSeparator();
            paragraph.Add(new Chunk(ls));
            paragraph.SpacingBefore = -5;
            document.Add(paragraph);
            paragraph.Clear();

            paragraph.Add(GetChunk("English", DefaultFontSize + 3f, Font.BOLD));
            paragraph.IndentationLeft = 15;
            paragraph.IndentationRight = 15;
            paragraph.SpacingBefore = 10;
            paragraph.SetLeading(1.0f, 1.0f);
            document.Add(paragraph);
            paragraph.Clear();

            paragraph.Add(GetChunk(this.Project.GetSummaryDtoByLanguageCode(Language.English.Code).ProjectSummary.Value, DefaultFontSize + 3f, Font.NORMAL));
            paragraph.IndentationLeft = 15;
            paragraph.IndentationRight = 15;
            paragraph.SpacingBefore = 0;
            document.Add(paragraph);
            paragraph.Clear();

            paragraph.Add(GetChunk("Português", DefaultFontSize + 3f, Font.BOLD));
            paragraph.IndentationLeft = 15;
            paragraph.IndentationRight = 15;
            paragraph.SpacingBefore = 10;
            document.Add(paragraph);
            paragraph.Clear();

            paragraph.Add(GetChunk(this.Project.GetSummaryDtoByLanguageCode(Language.Portuguese.Code).ProjectSummary.Value, DefaultFontSize + 3f, Font.NORMAL));
            paragraph.IndentationLeft = 15;
            paragraph.IndentationRight = 15;
            paragraph.SpacingBefore = 0;
            document.Add(paragraph);
            paragraph.Clear();

            return paragraph;
        }

        /// <summary>Gets the logline information.</summary>
        /// <param name="document">The document.</param>
        /// <param name="paragraph">The paragraph.</param>
        /// <returns></returns>
        private Paragraph GetLoglineInfo(ref PlataformaRio2CDocument document, Paragraph paragraph)
        {
            paragraph.Add(GetChunk("Logline", DefaultFontSize + 4f, Font.BOLD));
            paragraph.IndentationLeft = 15;
            paragraph.IndentationRight = 15;
            paragraph.SpacingBefore = 20;
            document.Add(paragraph);
            paragraph.Clear();

            var ls = new LineSeparator();
            paragraph.Add(new Chunk(ls));
            paragraph.SpacingBefore = -5;
            document.Add(paragraph);
            paragraph.Clear();

            paragraph.Add(GetChunk("English", DefaultFontSize + 3f, Font.BOLD));
            paragraph.IndentationLeft = 15;
            paragraph.IndentationRight = 15;
            paragraph.SpacingBefore = 5;
            document.Add(paragraph);
            paragraph.Clear();

            paragraph.Add(GetChunk(this.Project.GetLogLineDtoByLanguageCode(Language.English.Code).ProjectLogLine.Value, DefaultFontSize + 3f, Font.NORMAL));
            paragraph.IndentationLeft = 15;
            paragraph.IndentationRight = 15;
            paragraph.SpacingBefore = 0;
            document.Add(paragraph);
            paragraph.Clear();

            paragraph.Add(GetChunk("Português", DefaultFontSize + 3f, Font.BOLD));
            paragraph.IndentationLeft = 15;
            paragraph.IndentationRight = 15;
            paragraph.SpacingBefore = 5;
            document.Add(paragraph);
            paragraph.Clear();

            paragraph.Add(GetChunk(this.Project.GetLogLineDtoByLanguageCode(Language.Portuguese.Code).ProjectLogLine.Value, DefaultFontSize + 3f, Font.NORMAL));
            paragraph.IndentationLeft = 15;
            paragraph.IndentationRight = 15;
            paragraph.SpacingBefore = 0;
            document.Add(paragraph);
            paragraph.Clear();

            return paragraph;
        }

        /// <summary>Gets the production plan information.</summary>
        /// <param name="document">The document.</param>
        /// <param name="paragraph">The paragraph.</param>
        /// <returns></returns>
        private Paragraph GetProductionPlanInfo(ref PlataformaRio2CDocument document, Paragraph paragraph)
        {
            paragraph.Add(GetChunk("Planos de Produção", DefaultFontSize + 4f, Font.BOLD));
            paragraph.IndentationLeft = 15;
            paragraph.IndentationRight = 15;
            paragraph.SpacingBefore = 20;
            document.Add(paragraph);
            paragraph.Clear();

            var ls = new LineSeparator();
            paragraph.Add(new Chunk(ls));
            paragraph.SpacingBefore = -5;
            document.Add(paragraph);
            paragraph.Clear();

            paragraph.Add(GetChunk("English", DefaultFontSize + 3f, Font.BOLD));
            paragraph.IndentationLeft = 15;
            paragraph.IndentationRight = 15;
            paragraph.SpacingBefore = 5;
            document.Add(paragraph);
            paragraph.Clear();

            paragraph.Add(GetChunk(this.Project.GetProductionPlanDtoByLanguageCode(Language.English.Code).ProjectProductionPlan.Value ?? "N/A", DefaultFontSize + 3f, Font.NORMAL));
            paragraph.IndentationLeft = 15;
            paragraph.IndentationRight = 15;
            paragraph.SpacingBefore = 0;
            document.Add(paragraph);
            paragraph.Clear();

            paragraph.Add(GetChunk("Português", DefaultFontSize + 3f, Font.BOLD));
            paragraph.IndentationLeft = 15;
            paragraph.IndentationRight = 15;
            paragraph.SpacingBefore = 5;
            document.Add(paragraph);
            paragraph.Clear();

            paragraph.Add(GetChunk(this.Project.GetProductionPlanDtoByLanguageCode(Language.Portuguese.Code).ProjectProductionPlan.Value ?? "N/A", DefaultFontSize + 3f, Font.NORMAL));
            paragraph.IndentationLeft = 15;
            paragraph.IndentationRight = 15;
            paragraph.SpacingBefore = 0;
            document.Add(paragraph);
            paragraph.Clear();

            return paragraph;
        }

        /// <summary>Gets the format platform information.</summary>
        /// <param name="document">The document.</param>
        /// <param name="paragraph">The paragraph.</param>
        /// <returns></returns>
        private Paragraph GetFormatPlatformInfo(ref PlataformaRio2CDocument document, Paragraph paragraph)
        {
            #region Format

            var tableFormat = new PdfPTable(new float[] { 100 });
            tableFormat.WidthPercentage = 100;
            tableFormat.HorizontalAlignment = Element.ALIGN_LEFT;
            tableFormat.DefaultCell.VerticalAlignment = Element.ALIGN_TOP;

            tableFormat.AddCell(new Phrase(new Chunk("Formato: ", _fontLabel)));

            var chips = new List<Chunk>();
            var projectFormatsDtos = this.Project.GetAllInterestsByInterestGroupUid(InterestGroup.AudiovisualFormat.Uid);
            if (projectFormatsDtos?.Any() == true)
            {
                foreach (var projectFormatDto in projectFormatsDtos)
                {
                    chips.Add(new Chunk(projectFormatDto.Interest.Name, _fontChip));
                }

                paragraph.Add(GetChips("Formato: ", chips, _fontLabel, tableFormat));
                paragraph.IndentationLeft = 15;
                paragraph.IndentationRight = 15;
                paragraph.SpacingBefore = 10;
                paragraph.SetLeading(1.0f, 1.5f);
                document.Add(paragraph);
                paragraph.Clear();
            }

            #endregion

            #region Platform

            var tablePltaform = new PdfPTable(new float[] { 100 });
            tablePltaform.WidthPercentage = 100;
            tablePltaform.HorizontalAlignment = Element.ALIGN_LEFT;
            tablePltaform.DefaultCell.VerticalAlignment = Element.ALIGN_TOP;

            chips = new List<Chunk>();
            var projectPlatformsDtos = this.Project.GetAllInterestsByInterestGroupUid(InterestGroup.AudiovisualPlatforms.Uid);
            if (projectPlatformsDtos?.Any() == true)
            {
                foreach (var projectPlatformDto in projectPlatformsDtos)
                {
                    chips.Add(new Chunk(projectPlatformDto.Interest.Name, _fontChip));
                }

                paragraph.Add(GetChips("Plataforma: ", chips, _fontLabel, tablePltaform));
                paragraph.IndentationLeft = 15;
                paragraph.IndentationRight = 15;
                paragraph.SpacingBefore = 0;
                document.Add(paragraph);
                paragraph.Clear();
            }

            #endregion

            #region Project Status

            var tableStatus = new PdfPTable(new float[] { 100 });
            tableStatus.WidthPercentage = 100;
            tableStatus.HorizontalAlignment = Element.ALIGN_LEFT;
            tableStatus.DefaultCell.VerticalAlignment = Element.ALIGN_TOP;

            chips = new List<Chunk>();
            var projectStatusDtos = this.Project.GetAllInterestsByInterestGroupUid(InterestGroup.AudiovisualProjectStatus.Uid);
            if (projectStatusDtos?.Any() == true)
            {
                foreach (var projectStatusDto in projectStatusDtos)
                {
                    chips.Add(new Chunk(projectStatusDto.Interest.Name, _fontChip));
                }

                paragraph.Add(GetChips("Status do projeto:", chips, _fontLabel, tableStatus));
                paragraph.IndentationLeft = 15;
                paragraph.IndentationRight = 15;
                paragraph.SpacingBefore = 0;
                document.Add(paragraph);
                paragraph.Clear();
            }

            #endregion

            #region SubGenre

            var tableSubGenre = new PdfPTable(new float[] { 100 });
            tableSubGenre.WidthPercentage = 100;
            tableSubGenre.HorizontalAlignment = Element.ALIGN_LEFT;
            tableSubGenre.DefaultCell.VerticalAlignment = Element.ALIGN_TOP;

            chips = new List<Chunk>();
            var subgeneroDtos = this.Project.GetAllInterestsByInterestGroupUid(InterestGroup.AudiovisualBusinessRoundSubGenre.Uid);
            if (subgeneroDtos?.Any() == true)
            {
                foreach (var subgeneroDto in subgeneroDtos)
                {
                    chips.Add(new Chunk(subgeneroDto.Interest.Name, _fontChip));
                }

                paragraph.Add(GetChips("Subgênero: ", chips, _fontLabel, tableSubGenre));
                paragraph.IndentationLeft = 15;
                paragraph.IndentationRight = 15;
                paragraph.SpacingBefore = 0;
                document.Add(paragraph);
                paragraph.Clear();
            }

            #endregion

            return paragraph;
        }

        /// <summary>Gets the project values information.</summary>
        /// <param name="document">The document.</param>
        /// <param name="paragraph">The paragraph.</param>
        /// <returns></returns>
        private Paragraph GetProjectValuesInfo(ref PlataformaRio2CDocument document, Paragraph paragraph)
        {
            document.Add(paragraph);

            double.TryParse(this.Project.Project.TotalValueOfProject, out double totalValueOfProject);
            paragraph.Add(new Chunk("Valor total do projeto (USD): ", _fontLabel));
            paragraph.Add(GetChunk(totalValueOfProject.ToString("N2"), DefaultFontSize + 4f, Font.NORMAL));
            paragraph.IndentationLeft = 15;
            paragraph.IndentationRight = 15;
            paragraph.SpacingBefore = 15;
            paragraph.SetLeading(1.0f, 1.0f);
            document.Add(paragraph);
            paragraph.Clear();

            double.TryParse(this.Project.Project.ValueAlreadyRaised, out double valueAlreadyRaised);
            paragraph.Add(new Chunk("Valor já captado (USD): ", _fontLabel));
            paragraph.Add(GetChunk(valueAlreadyRaised.ToString("N2"), DefaultFontSize + 4f, Font.NORMAL));
            paragraph.IndentationLeft = 15;
            paragraph.IndentationRight = 15;
            paragraph.SpacingBefore = 15;
            document.Add(paragraph);
            paragraph.Clear();

            double.TryParse(this.Project.Project.ValueStillNeeded, out double valueStillNeeded);
            paragraph.Add(new Chunk("Valor a captar (USD): ", _fontLabel));
            paragraph.Add(GetChunk(valueStillNeeded.ToString("N2"), DefaultFontSize + 4f, Font.NORMAL));
            paragraph.IndentationLeft = 15;
            paragraph.IndentationRight = 15;
            paragraph.SpacingBefore = 15;
            document.Add(paragraph);
            paragraph.Clear();

            document.Add(paragraph);

            return paragraph;
        }

        /// <summary>Gets the other infos.</summary>
        /// <param name="document">The document.</param>
        /// <param name="paragraph">The paragraph.</param>
        private void GetOtherInfos(ref PlataformaRio2CDocument document, Paragraph paragraph)
        {
            paragraph.Add(new Chunk("Link para imagens ou layout conceituais: ", _fontLabel));
            paragraph.IndentationLeft = 15;
            paragraph.IndentationRight = 15;
            paragraph.SpacingBefore = 0;
            paragraph.SetLeading(1.0f, 2.0f);
            document.Add(paragraph);
            paragraph.Clear();
            if (this.Project.ProjectImageLinkDtos?.Any() == true)
            {
                foreach (var projectImageLink in this.Project.ProjectImageLinkDtos)
                {
                    paragraph.Add(GetChunk(projectImageLink.ProjectImageLink.Value, DefaultFontSize + 4f, Font.NORMAL));
                    paragraph.Add(GetChunk(" "));
                }
            }
            else
            {
                paragraph.Add(GetChunk("N/A", DefaultFontSize + 4f, Font.NORMAL));
            }
            paragraph.IndentationLeft = 15;
            paragraph.IndentationRight = 15;
            paragraph.SpacingBefore = 5;
            paragraph.SetLeading(1.0f, 2.0f);
            document.Add(paragraph);
            paragraph.Clear();

            paragraph.Add(new Chunk("Links de teaser: ", _fontLabel));
            paragraph.IndentationLeft = 15;
            paragraph.IndentationRight = 15;
            paragraph.SpacingBefore = 10;
            paragraph.SetLeading(1.0f, 2.0f);
            document.Add(paragraph);
            paragraph.Clear();

            if (this.Project.ProjectTeaserLinkDtos?.Any() == true)
            {
                foreach (var projectTeaserLink in this.Project.ProjectTeaserLinkDtos)
                {
                    paragraph.Add(GetChunk(projectTeaserLink.ProjectTeaserLink.Value, DefaultFontSize + 4f, Font.NORMAL));
                    paragraph.Add(GetChunk(" "));
                }
            }
            else
            {
                paragraph.Add(GetChunk("N/A", DefaultFontSize + 4f, Font.NORMAL));
            }
            paragraph.IndentationLeft = 15;
            paragraph.IndentationRight = 15;
            paragraph.SpacingBefore = 5;
            paragraph.SetLeading(1.0f, 2.0f);
            document.Add(paragraph);
            paragraph.Clear();

            paragraph.Add(GetChunk("Participará no processo de seleção das sessões PITCHING no Rio2C: ", DefaultFontSize + 4f, Font.BOLD));
            paragraph.IndentationLeft = 15;
            paragraph.IndentationRight = 15;
            paragraph.SpacingBefore = 10;
            paragraph.SetLeading(1.0f, 2.0f);
            document.Add(paragraph);
            paragraph.Clear();

            paragraph.Add(GetChunk(this.Project.IsPitching ? Labels.Yes : Labels.No, DefaultFontSize + 4f, Font.NORMAL));
            paragraph.IndentationLeft = 15;
            paragraph.IndentationRight = 15;
            paragraph.SpacingBefore = 5;
            document.Add(paragraph);
            paragraph.Clear();

            paragraph.Add(GetChunk("Informações Adicionais", DefaultFontSize + 4f, Font.BOLD));
            paragraph.IndentationLeft = 15;
            paragraph.IndentationRight = 15;
            paragraph.SpacingBefore = 10;
            document.Add(paragraph);
            paragraph.Clear();

            var ls = new LineSeparator();
            paragraph.Add(new Chunk(ls));
            paragraph.SpacingBefore = -5;
            document.Add(paragraph);
            paragraph.Clear();

            paragraph.Add(GetChunk("English", DefaultFontSize + 3f, Font.BOLD));
            paragraph.IndentationLeft = 15;
            paragraph.IndentationRight = 15;
            paragraph.SpacingBefore = 5;
            paragraph.SetLeading(1.0f, 1.0f);
            document.Add(paragraph);
            paragraph.Clear();

            paragraph.Add(GetChunk(this.Project.GetAdditionalInformationDtoByLanguageCode(Language.English.Code).ProjectAdditionalInformation.Value ?? "N/A", DefaultFontSize + 3f, Font.NORMAL));
            paragraph.IndentationLeft = 15;
            paragraph.IndentationRight = 15;
            paragraph.SpacingBefore = 0;
            document.Add(paragraph);
            paragraph.Clear();

            paragraph.Add(GetChunk("Português", DefaultFontSize + 3f, Font.BOLD));
            paragraph.IndentationLeft = 15;
            paragraph.IndentationRight = 15;
            paragraph.SpacingBefore = 5;
            document.Add(paragraph);
            paragraph.Clear();

            paragraph.Add(GetChunk(this.Project.GetAdditionalInformationDtoByLanguageCode(Language.Portuguese.Code).ProjectAdditionalInformation.Value ?? "N/A", DefaultFontSize + 3f, Font.NORMAL));
            paragraph.IndentationLeft = 15;
            paragraph.IndentationRight = 15;
            paragraph.SpacingBefore = 0;
            document.Add(paragraph);
            paragraph.Clear();
        }
    }
}
