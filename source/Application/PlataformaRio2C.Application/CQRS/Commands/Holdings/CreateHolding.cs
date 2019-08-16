// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-15-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-15-2019
// ***********************************************************************
// <copyright file="CreateHolding.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Web;
using MediatR;
using PlataformaRio2C.Application.ViewModels;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>CreateHolding</summary>
    public class CreateHolding : BaseUserRequest, IRequest<Guid?>
    {
        public Guid HoldingUid { get; private set; }
        public string Name { get; private set; }
        public IEnumerable<HoldingDescriptionAppViewModel> Descriptions { get; private set; }
        public HttpPostedFileBase ImageFile { get; private set; }
        public decimal CropperImageDataX { get; private set; }
        public decimal CropperImageDataY { get; private set; }
        public decimal CropperImageDataWidth { get; private set; }
        public decimal CropperImageDataHeight { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="CreateHolding"/> class.</summary>
        /// <param name="holdingUid">The holding uid.</param>
        /// <param name="name">The name.</param>
        /// <param name="descriptions">The descriptions.</param>
        /// <param name="imageFile">The image file.</param>
        /// <param name="cropperImageDataX">The cropper image data x.</param>
        /// <param name="cropperImageDataY">The cropper image data y.</param>
        /// <param name="cropperImageDataWidth">Width of the cropper image data.</param>
        /// <param name="cropperImageDataHeight">Height of the cropper image data.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="userUid">The user uid.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="editionUid">The edition uid.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        public CreateHolding(
            Guid holdingUid, 
            string name,
            IEnumerable<HoldingDescriptionAppViewModel> descriptions,
            HttpPostedFileBase imageFile, 
            decimal cropperImageDataX, 
            decimal cropperImageDataY, 
            decimal cropperImageDataWidth, 
            decimal cropperImageDataHeight,
            int userId,
            Guid userUid,
            int? editionId,
            Guid? editionUid,
            string userInterfaceLanguage)
            : base(userId, userUid, editionId, editionUid, userInterfaceLanguage)
        {
            this.HoldingUid = holdingUid;
            this.Name = name;
            this.Descriptions = descriptions;
            this.ImageFile = imageFile;
            this.CropperImageDataX = cropperImageDataX;
            this.CropperImageDataY = cropperImageDataY;
            this.CropperImageDataWidth = cropperImageDataWidth;
            this.CropperImageDataHeight = cropperImageDataHeight;
        }
    }
}