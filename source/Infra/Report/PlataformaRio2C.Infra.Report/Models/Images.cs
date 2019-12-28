// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : William Sergio Almado Junior
// Created          : 12-27-2019
//
// Last Modified By : William Sergio Almado Junior
// Last Modified On : 12-27-2019
// ***********************************************************************
// <copyright file="Images" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using iTextSharp.text;
using PlataformaRio2C.Infra.Report.Properties;
using System.IO;

namespace PlataformaRio2C.Infra.Report
{
    public static class Images
    {
        private static Image _background;

        private static Image _backgroundFooter;

        /// <summary>
        /// B
        /// </summary>
        public static Image BackgroundFirstPage
        {
            get
            {
                if (_background == null)
                {
                    MemoryStream img = new MemoryStream();
                    Resources.bg_template_document_project.Save(img, System.Drawing.Imaging.ImageFormat.Bmp);

                    _background = Image.GetInstance(img.ToArray());
                    _background.Alignment = Element.ALIGN_CENTER;
                
                }

                return _background;
            }
        }

        public static Image BackgroundFooter
        {
            get
            {
                if (_backgroundFooter == null)
                {
                    MemoryStream img = new MemoryStream();
                    Resources.bg_footer_template_document_project.Save(img, System.Drawing.Imaging.ImageFormat.Bmp);

                    _backgroundFooter = Image.GetInstance(img.ToArray());
                    _backgroundFooter.Alignment = Element.ALIGN_CENTER;
                    //_bg.ScaleToFit(50, 50); aqui comentei pq vc pode setar o scale da imagem
                }
                return _backgroundFooter;
            }
        }
    }
}