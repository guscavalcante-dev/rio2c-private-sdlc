// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Report
// Author           : William Sergio Almado Junior
// Created          : 12-27-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-05-2020
// ***********************************************************************
// <copyright file="TemplateBlanks.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using iTextSharp.text;
using System;

namespace PlataformaRio2C.Infra.Report.Models
{
    /// <summary>TemplateBlank</summary>
    public class TemplateBlank : TemplateBase
    {
        public override Font GetFont(float fontSize, int fontStyle)
        {
            throw new NotImplementedException();
        }

        /// <summary>Gets the paragraph.</summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public override Paragraph GetParagraph(string text = null)
        {
            throw new NotImplementedException();
        }

        /// <summary>Prepares the specified document.</summary>
        /// <param name="document">The document.</param>
        public override void Prepare(PlataformaRio2CDocument document)
        {
            throw new NotImplementedException();
        }
    }
}
