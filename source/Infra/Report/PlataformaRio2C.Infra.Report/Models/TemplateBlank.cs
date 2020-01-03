// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : William Sergio Almado Junior
// Created          : 12-27-2019
//
// Last Modified By : William Sergio Almado Junior
// Last Modified On : 12-27-2019
// ***********************************************************************
// <copyright file="TemplateBlanks" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using iTextSharp.text;

namespace PlataformaRio2C.Infra.Report
{
    public class TemplateBlank : TemplateBase
    {
        public override Font GetFont(float fontSize, int fontStyle)
        {
            throw new NotImplementedException();
        }

        public override Paragraph GetParagraph(string Text = null)
        {
            throw new NotImplementedException();
        }

        public override void Prepare(PlataformaRio2CDocument document)
        {
            throw new NotImplementedException();
        }
    }
}
