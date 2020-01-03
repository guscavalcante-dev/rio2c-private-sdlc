// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : William Sergio Almado Junior
// Created          : 12-27-2019
//
// Last Modified By : William Sergio Almado Junior
// Last Modified On : 12-27-2019
// ***********************************************************************
// <copyright file="TemplateBase" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using iTextSharp.text;
using System;

namespace PlataformaRio2C.Infra.Report
{
    public abstract class TemplateBase
    {
        /// <summary>
        /// Título do documento
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Subtítulo do documento
        /// </summary>
        public string Subtitle { get; set; }

        /// <summary>
        /// Primeiro Cabeçalho do documento
        /// </summary>
        public IElement FirstHeader { get; set; }

        /// <summary>
        /// Elemento de rodapé para o documento
        /// </summary>
        public IElement Footer { get; set; }

        /// <summary>
        /// Cabeçalho para a segunda página em diante
        /// </summary>
        public IElement SubsequentialHeaders { get; set; }

        /// <summary>
        /// Texto para compor o cabeçalho e rodapé
        /// </summary>
        public string HeaderAddendum { get; set; }

        /// <summary>
        /// Tamanho padrão da fonte. Estabelece um padrão do qual outros tamanhos de fonte no documento podem derivar percentualmente.
        /// <para>O tamanho inicial é 8</para>
        /// </summary>
        public float DefaultFontSize { get; set; }

        /// <summary>
        /// Especifica se o rodapé padrão deve ser exibido no documento
        /// </summary>
        public bool ShowDefaultFooter { get; set; }

        public bool ShowDefaultBackground { get; set; }

        /// <summary>
        /// Determina se a orientação do documento é em 'paisagem'
        /// </summary>
        public bool LandscapeOrientation { get; set; }

        public abstract void Prepare(PlataformaRio2CDocument document);

        /// <summary>
        /// Retorna uma instância de parágrafo com o formato do template
        /// </summary>
        /// <param name="Text">Conteúdo do parágafo</param>
        public abstract Paragraph GetParagraph(string Text = null);

        /// <summary>
        /// Recupera um "fragmento" de texto 
        /// </summary>
        /// <param name="Text">Conteúdo do chunk</param>
        public Chunk GetChunk(string Text)
        {
            return new Chunk(Text, GetFont());
        }

        /// <summary>
        /// Retorna um "fragmento" de texto
        /// </summary>
        /// <param name="Text">Conteúdo do fragmento</param>
        /// <param name="fontSize">Tamanho da fonte do fragmento</param>
        /// <param name="fontStyle">Estilo da fonte do fragmento (e.g. Font.BOLD)</param>
        public Chunk GetChunk(string Text, float fontSize, int fontStyle = Font.NORMAL)
        {
            return new Chunk(Text, GetFont(fontSize, fontStyle));
        }

        /// <summary>
        /// Retorna uma frase de texto
        /// </summary>
        /// <param name="text">Conteúdo da frase</param>
        /// <param name="fontStyle">Estilo da fonte do fragmento (e.g. Font.BOLD)</param>
        /// <param name="fontSize">Tamanho da fonte. Por padrão é a tamanho especificado pelo template</param>
        public Phrase GetPhrase(string text, int fontStyle = Font.NORMAL, float fontSize = 0)
        {
            if (Convert.ToInt32(fontSize) == 0)
                fontSize = DefaultFontSize;

            return new Phrase(GetChunk(text, fontSize, fontStyle));
        }

        /// <summary>
        /// Retorna a fonte padrão do template
        /// </summary>
        public Font GetFont()
        {
            return GetFont(DefaultFontSize, Font.NORMAL);
        }


        /// <summary>
        /// Retorna a fonte padrão do template
        /// </summary>
        /// <param name="fontSize">Tamanho da fonte</param>
        /// <param name="fontStyle">Estilo da fonte </param>
        abstract public Font GetFont(float fontSize, int fontStyle);

        /// <summary>
        /// Construtor para setar valores iniciais padrão do template (e.g. DefaultFontSize)
        /// </summary>
        public TemplateBase()
        {
            DefaultFontSize = 8;
        }
    }
}

