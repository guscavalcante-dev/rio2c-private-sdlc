// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : William Sergio Almado Junior
// Created          : 12-27-2019
//
// Last Modified By : William Sergio Almado Junior
// Last Modified On : 12-27-2019
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
using PlataformaRio2C.Infra.Report;
using System.Linq;

namespace PlataformaRio2C.Application.TemplateDocuments
{
    /// <summary>ProjectDocumentTemplate</summary>
    public class ProjectDocumentTemplate : TemplateBase
    {

        public ProjectDto Project { get; private set; }
        private Font _font;

        /// <summary>Initializes a new instance of the <see cref="ProjectDocumentTemplate"/> class.</summary>
        public ProjectDocumentTemplate(ProjectDto project)
        {
            this.Project = project;
            _font = new Font(BaseFont.CreateFont("Helvetica", BaseFont.WINANSI, true));
            _font.Color = new BaseColor(64, 64, 64);

            ShowDefaultBackground = true;
            ShowDefaultFooter = false;
        }

        public override Font GetFont(float fontSize, int fontStyle)
        {
            _font.Size = fontSize;
            _font.SetStyle(fontStyle);

            return _font;
        }

        public override Paragraph GetParagraph(string Text = null)
        {
            var paragraph = new Paragraph();
            paragraph.Alignment = Element.ALIGN_CENTER;
            paragraph.SpacingBefore = 5f;
            paragraph.Font = GetFont();
            paragraph.Add(Text);

            return paragraph;
        }

        public override void Prepare(PlataformaRio2CDocument document)
        {
           
            var paragraph = GetFirstPageInfo(ref document);


            document.Add(Chunk.NEXTPAGE);
            

            paragraph = GetSummaryInfo(ref document, paragraph);

            paragraph = GetLoglineInfo(ref document, paragraph);

            paragraph = GetProductionPlanInfo(ref document, paragraph);

            document.Add(Chunk.NEXTPAGE);           

            paragraph = GetFormatPlatformInfo(ref document, paragraph);

            paragraph = GetProjectValuesInfo(ref document, paragraph);

            GetOtherInfos(ref document, paragraph);
            
        }

        private Paragraph GetFirstPageInfo(ref PlataformaRio2CDocument document)
        {
            var paragraph = new Paragraph();

            paragraph.Add(GetChunk(this.Project.GetTitleDtoByLanguageCode("pt-br").ProjectTitle.Value.ToUpper(), DefaultFontSize + 12f, Font.BOLD));
            paragraph.IndentationLeft = 170;
            paragraph.SpacingBefore = 85;
            paragraph.SetLeading(1.0f, 2.5f);
            paragraph.Alignment = Element.ALIGN_CENTER;
            document.Add(paragraph);
            paragraph.Clear();

            paragraph.Add(GetChunk(this.Project.GetTitleDtoByLanguageCode("en-us").ProjectTitle.Value.ToUpper(), DefaultFontSize + 10f, Font.ITALIC));
            paragraph.IndentationLeft = 170;
            paragraph.SpacingBefore = 10;
            paragraph.Alignment = Element.ALIGN_CENTER;
            document.Add(paragraph);
            paragraph.Clear();


            paragraph.Add(GetChunk("Produtora/Empresa: ", DefaultFontSize + 6f, Font.NORMAL));
            paragraph.Add(GetChunk(this.Project.SellerAttendeeOrganizationDto.Organization.Name, DefaultFontSize + 6f, Font.NORMAL));
            paragraph.Alignment = Element.ALIGN_LEFT;
            paragraph.IndentationLeft = 170;
            paragraph.SpacingBefore = 20;
            paragraph.SetLeading(1.0f, 1.0f);
            document.Add(paragraph);
            paragraph.Clear();

            paragraph.Add(GetChunk("Gênero: ", DefaultFontSize + 6f, Font.BOLD));
            var projectInterestsDtos = this.Project.GetAllInterestsByInterestGroupUid(InterestGroup.Genre.Uid);
            if (projectInterestsDtos?.Any() == true)
            {
                foreach (var projectInterestDto in projectInterestsDtos)
                {
                    paragraph.Add(GetChunk(projectInterestDto.Interest.Name.GetSeparatorTranslation("en-us", '|'), DefaultFontSize + 4f, Font.BOLD));
                    paragraph.Add(GetChunk(" | ", DefaultFontSize + 4f, Font.BOLD));
                    paragraph.Add(GetChunk(projectInterestDto.Interest.Name.GetSeparatorTranslation("pt-br", '|'), DefaultFontSize + 4f, Font.ITALIC | Font.BOLD));
                }

            };

            paragraph.Alignment = Element.ALIGN_LEFT;
            paragraph.IndentationLeft = 170;
            paragraph.SpacingBefore = 10;
            document.Add(paragraph);
            paragraph.Clear();

            return paragraph;
        }

        private Paragraph GetSummaryInfo(ref PlataformaRio2CDocument document, Paragraph paragraph) 
        {
            paragraph.Add(GetChunk("Resumo", DefaultFontSize + 8f, Font.BOLD));
            paragraph.IndentationLeft = 15;
            paragraph.IndentationRight = 15;
            paragraph.SpacingBefore = 50;
            paragraph.SetLeading(1, 1);
            document.Add(paragraph);
            paragraph.Clear();

            var ls = new LineSeparator();
            paragraph.Add(new Chunk(ls));
            paragraph.SpacingBefore = -5;
            document.Add(paragraph);
            paragraph.Clear();

            paragraph.Add(GetChunk("Inglês", DefaultFontSize + 6f, Font.BOLD));
            paragraph.IndentationLeft = 15;
            paragraph.IndentationRight = 15;
            paragraph.SpacingBefore = 10;
            paragraph.SetLeading(1.0f, 1.5f);
            document.Add(paragraph);
            paragraph.Clear();

            paragraph.Add(GetChunk(this.Project.GetSummaryDtoByLanguageCode("en-us").ProjectSummary.Value, DefaultFontSize + 6f, Font.NORMAL));
            paragraph.IndentationLeft = 15;
            paragraph.IndentationRight = 15;
            paragraph.SpacingBefore = 15;
            document.Add(paragraph);
            paragraph.Clear();

            paragraph.Add(GetChunk("Português", DefaultFontSize + 6f, Font.BOLD));
            paragraph.IndentationLeft = 15;
            paragraph.IndentationRight = 15;
            paragraph.SpacingBefore = 20;
            document.Add(paragraph);
            paragraph.Clear();

            paragraph.Add(GetChunk(this.Project.GetSummaryDtoByLanguageCode("pt-br").ProjectSummary.Value, DefaultFontSize + 6f, Font.NORMAL));
            paragraph.IndentationLeft = 15;
            paragraph.IndentationRight = 15;
            paragraph.SpacingBefore = 15;
            document.Add(paragraph);
            paragraph.Clear();

            return paragraph;
        }

        private Paragraph GetLoglineInfo(ref PlataformaRio2CDocument document, Paragraph paragraph)
        {
            paragraph.Add(GetChunk("Logline", DefaultFontSize + 8f, Font.BOLD));
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

            paragraph.Add(GetChunk(this.Project.GetLogLineDtoByLanguageCode("en-us").ProjectLogLine.Value, DefaultFontSize + 6f, Font.NORMAL));
            paragraph.Add(GetChunk(" "));
            paragraph.Add(GetChunk(this.Project.GetLogLineDtoByLanguageCode("pt-br").ProjectLogLine.Value, DefaultFontSize + 6f, Font.NORMAL));
            paragraph.IndentationLeft = 15;
            paragraph.IndentationRight = 15;
            paragraph.SpacingBefore = 15;
            document.Add(paragraph);
            paragraph.Clear();

            return paragraph;
        }

        private Paragraph GetProductionPlanInfo(ref PlataformaRio2CDocument document, Paragraph paragraph)
        {
            paragraph.Add(GetChunk("Planos de Produção", DefaultFontSize + 8f, Font.BOLD));
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

            paragraph.Add(GetChunk("Inglês", DefaultFontSize + 4f, Font.BOLD));
            paragraph.IndentationLeft = 15;
            paragraph.IndentationRight = 15;
            paragraph.SpacingBefore = 10;
            document.Add(paragraph);
            paragraph.Clear();

            paragraph.Add(GetChunk(this.Project.GetProductionPlanDtoByLanguageCode("en-us").ProjectProductionPlan.Value ?? "N/A", DefaultFontSize + 6f, Font.NORMAL));
            paragraph.IndentationLeft = 15;
            paragraph.IndentationRight = 15;
            paragraph.SpacingBefore = 15;
            document.Add(paragraph);
            paragraph.Clear();

            paragraph.Add(GetChunk("Português", DefaultFontSize + 4f, Font.BOLD));
            paragraph.IndentationLeft = 15;
            paragraph.IndentationRight = 15;
            paragraph.SpacingBefore = 20;
            document.Add(paragraph);
            paragraph.Clear();

            paragraph.Add(GetChunk(this.Project.GetProductionPlanDtoByLanguageCode("pt-br").ProjectProductionPlan.Value ?? "N/A", DefaultFontSize + 6f, Font.NORMAL));
            paragraph.IndentationLeft = 15;
            paragraph.IndentationRight = 15;
            paragraph.SpacingBefore = 15;
            document.Add(paragraph);
            paragraph.Clear();

            return paragraph;
        }

        private Paragraph GetFormatPlatformInfo(ref PlataformaRio2CDocument document, Paragraph paragraph)
        {
            paragraph.Add(GetChunk("Formato: ", DefaultFontSize + 6f, Font.BOLD));

            var projectFormatsDtos = this.Project.GetAllInterestsByInterestGroupUid(InterestGroup.Format.Uid);
            if (projectFormatsDtos?.Any() == true)
            {
                foreach (var projectFormatDto in projectFormatsDtos)
                {
                    paragraph.Add(GetChunk(projectFormatDto.Interest.Name.GetSeparatorTranslation("en-us", '|'), DefaultFontSize + 6f, Font.NORMAL));
                    paragraph.Add(GetChunk(" | ", DefaultFontSize + 6f, Font.NORMAL));
                    paragraph.Add(GetChunk(projectFormatDto.Interest.Name.GetSeparatorTranslation("pt-br", '|'), DefaultFontSize + 6f, Font.NORMAL));
                }
            }

            paragraph.IndentationLeft = 15;
            paragraph.IndentationRight = 15;
            paragraph.SpacingBefore = 50;
            paragraph.SetLeading(1.0f, 1.5f);
            document.Add(paragraph);
            paragraph.Clear();


            paragraph.Add(GetChunk("Plataforma: ", DefaultFontSize + 6f, Font.BOLD));

            var projectPlatformsDtos = this.Project.GetAllInterestsByInterestGroupUid(InterestGroup.Platforms.Uid);
            if (projectPlatformsDtos?.Any() == true)
            {
                foreach (var projectPlatformDto in projectPlatformsDtos)
                {
                    paragraph.Add(GetChunk(projectPlatformDto.Interest.Name.GetSeparatorTranslation("en-us", '|'), DefaultFontSize + 6f, Font.NORMAL));
                    paragraph.Add(GetChunk(" | "));
                    paragraph.Add(GetChunk(projectPlatformDto.Interest.Name.GetSeparatorTranslation("pt-br", '|'), DefaultFontSize + 6f, Font.NORMAL));
                }
            }


            paragraph.IndentationLeft = 15;
            paragraph.IndentationRight = 15;
            paragraph.SpacingBefore = 20;
            paragraph.SetLeading(1.0f, 1.0f);
            document.Add(paragraph);
            paragraph.Clear();

            return paragraph;
        }

        private Paragraph GetProjectValuesInfo(ref PlataformaRio2CDocument document, Paragraph paragraph)
        {
            document.Add(paragraph);

            paragraph.Add(GetChunk("Valor total do projeto (USD): ", DefaultFontSize + 6f, Font.BOLD));
            paragraph.Add(GetChunk(this.Project.Project.TotalValueOfProject ?? "N/A", DefaultFontSize + 6f, Font.NORMAL));
            paragraph.IndentationLeft = 15;
            paragraph.IndentationRight = 15;
            paragraph.SpacingBefore = 15;
            paragraph.SetLeading(1.0f, 1.0f);
            document.Add(paragraph);
            paragraph.Clear();


            paragraph.Add(GetChunk("Valor já captado (USD): ", DefaultFontSize + 6f, Font.BOLD));
            paragraph.Add(GetChunk(this.Project.Project.ValueAlreadyRaised ?? "N/A", DefaultFontSize + 6f, Font.NORMAL));
            paragraph.IndentationLeft = 15;
            paragraph.IndentationRight = 15;
            paragraph.SpacingBefore = 15;
            document.Add(paragraph);
            paragraph.Clear();


            paragraph.Add(GetChunk("Valor a captar (USD): ", DefaultFontSize + 6f, Font.BOLD));
            paragraph.Add(GetChunk(this.Project.Project.ValueStillNeeded ?? "N/A", DefaultFontSize + 6f, Font.NORMAL));
            paragraph.IndentationLeft = 15;
            paragraph.IndentationRight = 15;
            paragraph.SpacingBefore = 15;
            document.Add(paragraph);
            paragraph.Clear();

            document.Add(paragraph);

            return paragraph;
        }

        private void GetOtherInfos(ref PlataformaRio2CDocument document, Paragraph paragraph)
        {
            paragraph.Add(GetChunk("Links de teaser: ", DefaultFontSize + 6f, Font.BOLD));
            if (this.Project.ProjectTeaserLinkDtos?.Any() == true)
            {
                foreach (var projectTeaserLink in this.Project.ProjectTeaserLinkDtos)
                {
                    paragraph.Add(GetChunk(projectTeaserLink.ProjectTeaserLink.Value, DefaultFontSize + 6f, Font.NORMAL));
                    paragraph.Add(GetChunk(" "));
                }
            }
            else
            {
                paragraph.Add(GetChunk("N/A", DefaultFontSize + 6f, Font.NORMAL));
            }


            paragraph.IndentationLeft = 15;
            paragraph.IndentationRight = 15;
            paragraph.SpacingBefore = 20;
            document.Add(paragraph);
            paragraph.Clear();

            document.Add(paragraph);


            paragraph.Add(GetChunk("Participará no processo de seleção das sessões PITCHING no Rio2C / RioContentMarket: ", DefaultFontSize + 6f, Font.BOLD));
            paragraph.IndentationLeft = 15;
            paragraph.IndentationRight = 15;
            paragraph.SpacingBefore = 20;
            paragraph.SetLeading(1.0f, 2.0f);
            document.Add(paragraph);
            paragraph.Clear();

            paragraph.Add(GetChunk(this.Project.Project.IsPitching ? Labels.Yes : Labels.No, DefaultFontSize + 6f, Font.NORMAL));
            paragraph.IndentationLeft = 15;
            paragraph.IndentationRight = 15;
            paragraph.SpacingBefore = 10;
            document.Add(paragraph);
            paragraph.Clear();

            document.Add(paragraph);


            paragraph.Add(GetChunk("Informações Adicionais", DefaultFontSize + 8f, Font.BOLD));
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

            paragraph.Add(GetChunk("Inglês", DefaultFontSize + 6f, Font.BOLD));
            paragraph.IndentationLeft = 15;
            paragraph.IndentationRight = 15;
            paragraph.SpacingBefore = 10;
            paragraph.SetLeading(1.0f, 1.0f);
            document.Add(paragraph);
            paragraph.Clear();

            paragraph.Add(GetChunk(this.Project.GetAdditionalInformationDtoByLanguageCode("en-us").ProjectAdditionalInformation.Value ?? "N/A", DefaultFontSize + 6f, Font.NORMAL));
            paragraph.IndentationLeft = 15;
            paragraph.IndentationRight = 15;
            paragraph.SpacingBefore = 15;
            document.Add(paragraph);
            paragraph.Clear();

            paragraph.Add(GetChunk("Português", DefaultFontSize + 6f, Font.BOLD));
            paragraph.IndentationLeft = 15;
            paragraph.IndentationRight = 15;
            paragraph.SpacingBefore = 20;
            document.Add(paragraph);
            paragraph.Clear();

            paragraph.Add(GetChunk(this.Project.GetAdditionalInformationDtoByLanguageCode("pt-br").ProjectAdditionalInformation.Value ?? "N/A", DefaultFontSize + 6f, Font.NORMAL));
            paragraph.IndentationLeft = 15;
            paragraph.IndentationRight = 15;
            paragraph.SpacingBefore = 15;
            document.Add(paragraph);
            paragraph.Clear();
        }
    }
}
