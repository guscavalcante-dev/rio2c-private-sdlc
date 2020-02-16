// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-16-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 02-15-2020
// ***********************************************************************
// <copyright file="CropperImageBaseCommand.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.ComponentModel.DataAnnotations;
using System.Web;
using PlataformaRio2C.Domain.Statics;
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
        [RequiredImageOptional]
        public HttpPostedFileBase ImageFile { get; set; }

        public string ImageUploadDate { get; set; }
        public Guid? ImageUid { get; set; }
        public bool IsImageDeleted { get; set; }
        public Guid? FileRepositoryPathTypeUid { get; set; }
        public bool IsRequired { get; set; }

        /// <summary>Initializes a new instance of the <see cref="CropperImageBaseCommand"/> class.</summary>
        /// <param name="imageUploadDate">The image upload date.</param>
        /// <param name="imageUid">The image uid.</param>
        /// <param name="fileRepositoryPathType">Type of the file repository path.</param>
        /// <param name="isRequired">if set to <c>true</c> [is required].</param>
        public CropperImageBaseCommand(DateTimeOffset? imageUploadDate, Guid? imageUid, FileRepositoryPathType fileRepositoryPathType, bool isRequired)
        {
            this.ImageUploadDate = imageUploadDate?.ToString("yyyyMMddHHmmss");
            this.ImageUid = imageUid;
            this.IsImageDeleted = false;
            this.FileRepositoryPathTypeUid = fileRepositoryPathType.Uid;
            this.IsRequired = isRequired;
        }

        /// <summary>Initializes a new instance of the <see cref="CropperImageBaseCommand"/> class.</summary>
        public CropperImageBaseCommand()
        {
        }

        /// <summary>Gets the image upload date.</summary>
        /// <returns></returns>
        public DateTime? GetImageUploadDate()
        {
            if (string.IsNullOrEmpty(this.ImageUploadDate))
            {
                return null;
            }

            return DateTime.ParseExact(this.ImageUploadDate, "yyyyMMddHHmmss", null);
        }
    }
}