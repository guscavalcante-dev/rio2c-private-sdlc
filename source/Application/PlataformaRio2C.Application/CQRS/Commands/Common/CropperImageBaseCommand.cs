// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-16-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-16-2019
// ***********************************************************************
// <copyright file="CropperImageBaseCommand.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.ComponentModel.DataAnnotations;
using System.Web;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Attributes;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>CropperImageBaseCommand</summary>
    public class CropperImageBaseCommand
    {
        public decimal DataX { get; set; }
        public decimal DataY { get; set; }
        public decimal DataWidth { get; set; }
        public decimal DataHeight { get; set; }
        public decimal DataRotate { get; set; }
        public decimal DataScaleX { get; set; }
        public decimal DataScaleY { get; set; }

        [Display(Name = "Image", ResourceType = typeof(Labels))]
        [HttpPostedFileExtensions(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "InvalidImage")]
        public HttpPostedFileBase ImageFile { get; set; }

        public bool IsImageUploaded { get; set; }
        public Guid? ImageUid { get; set; }

        /// <summary>Initializes a new instance of the <see cref="CropperImageBaseCommand"/> class.</summary>
        /// <param name="isImageUploaded">if set to <c>true</c> [is image uploaded].</param>
        /// <param name="imageUid">The image uid.</param>
        public CropperImageBaseCommand(bool isImageUploaded, Guid? imageUid)
        {
            this.IsImageUploaded = isImageUploaded;
            this.ImageUid = imageUid;
        }

        /// <summary>Initializes a new instance of the <see cref="CropperImageBaseCommand"/> class.</summary>
        public CropperImageBaseCommand()
        {
        }
    }
}