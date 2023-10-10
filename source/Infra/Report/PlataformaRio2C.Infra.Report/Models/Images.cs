// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Report
// Author           : William Sergio Almado Junior
// Created          : 12-27-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-05-2020
// ***********************************************************************
// <copyright file="Images.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.IO;
using iTextSharp.text;

namespace PlataformaRio2C.Infra.Report.Models
{
    /// <summary>Images</summary>
    public static class Images
    {
        private static Image _background;
        private static Image _backgroundFooter;

        /// <summary>Gets the background first page.</summary>
        /// <value>The background first page.</value>
        public static Image BackgroundFirstPage
        {
            get
            {
                if (_background == null)
                {
                    MemoryStream img = new MemoryStream();
                    string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets\\img\\bg-template-document-project.png");
                    var fileName = System.Drawing.Image.FromFile(path);
                    fileName.Save(img, System.Drawing.Imaging.ImageFormat.Bmp);
                    _background = Image.GetInstance(img.ToArray());
                    _background.Alignment = Element.ALIGN_CENTER;
                
                }

                return _background;
            }
        }

        /// <summary>Gets the background footer.</summary>
        /// <value>The background footer.</value>
        public static Image BackgroundFooter
        {
            get
            {
                if (_backgroundFooter == null)
                {
                    MemoryStream img = new MemoryStream();
                    string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets\\img\\bg-footer-template-document-project.png");
                    var fileName = System.Drawing.Image.FromFile(path);
                    fileName.Save(img, System.Drawing.Imaging.ImageFormat.Bmp);

                    _backgroundFooter = Image.GetInstance(img.ToArray());
                    _backgroundFooter.Alignment = Element.ALIGN_CENTER;
                }
                return _backgroundFooter;
            }
        }
    }
}