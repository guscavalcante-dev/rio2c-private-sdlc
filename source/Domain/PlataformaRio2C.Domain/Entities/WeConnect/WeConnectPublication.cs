// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 08-16-2023
//
// Last Modified By : Renan Valentim
// Last Modified On : 08-16-2023
// ***********************************************************************
// <copyright file="WeConnectPublication.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>
    /// Class WeConnectPublication.
    /// Implements the <see cref="PlataformaRio2C.Domain.Entities.AggregateRoot" />
    /// </summary>
    /// <seealso cref="PlataformaRio2C.Domain.Entities.AggregateRoot" />
    public class WeConnectPublication : Entity
    {
        public static readonly int SocialMediaPlatformPublicationIdMaxLenght = 20;
        public static readonly int PublicationTextMaxLenght = 3000;

        public int? SocialMediaPlatformId { get; private set; }
        public string SocialMediaPlatformPublicationId { get; private set; }
        public string PublicationText { get; private set; }
        public DateTimeOffset? ImageUploadDate { get; private set; }
        public bool IsVideo { get; private set; }
        public bool IsFixedOnTop { get; private set; }

        public virtual SocialMediaPlatform SocialMediaPlatform { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="WeConnectPublication"/> class.
        /// </summary>
        public WeConnectPublication()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WeConnectPublication" /> class.
        /// </summary>
        /// <param name="socialMediaPlatformPublicationId">The social media platform publication identifier.</param>
        /// <param name="publicationText">The publication text.</param>
        /// <param name="isVideo">if set to <c>true</c> [is video].</param>
        /// <param name="isFixedOnTop">if set to <c>true</c> [is fixed on top].</param>
        /// <param name="isImageUploaded">if set to <c>true</c> [is image uploaded].</param>
        /// <param name="socialMediaPlatform">The social media platform.</param>
        /// <param name="userId">The user identifier.</param>
        public WeConnectPublication(
            string socialMediaPlatformPublicationId,
            string publicationText,
            bool isVideo,
            bool isFixedOnTop,
            bool isImageUploaded,
            SocialMediaPlatform socialMediaPlatform,
            int userId)
        {
            this.SocialMediaPlatformPublicationId = socialMediaPlatformPublicationId;
            this.PublicationText = publicationText;
            this.IsVideo = isVideo;
            this.IsFixedOnTop = isFixedOnTop;

            this.UpdateImageUploadDate(isImageUploaded, false);

            this.SocialMediaPlatform = socialMediaPlatform;

            base.SetCreateDate(userId);
        }

        /// <summary>
        /// Updates the image upload date.
        /// </summary>
        /// <param name="isImageUploaded">if set to <c>true</c> [is image uploaded].</param>
        /// <param name="isImageDeleted">if set to <c>true</c> [is image deleted].</param>
        private void UpdateImageUploadDate(bool isImageUploaded, bool isImageDeleted)
        {
            if (isImageUploaded)
            {
                this.ImageUploadDate = DateTime.UtcNow;
            }
            else if (isImageDeleted)
            {
                this.ImageUploadDate = null;
            }
        }

        #region Validations

        /// <summary>Returns true if ... is valid.</summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.</returns>
        public override bool IsValid()
        {
            this.ValidationResult = new ValidationResult();

            this.ValidatePublicationText();
            this.ValidateSocialMediaPlatformPublicationId();

            return this.ValidationResult.IsValid;
        }

        /// <summary>
        /// Validates the publication text.
        /// </summary>
        private void ValidatePublicationText()
        {
            if (this.PublicationText.Length > PublicationTextMaxLenght)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, nameof(PublicationText), PublicationTextMaxLenght, 1), new string[] { nameof(PublicationText) }));
            }
        }

        /// <summary>
        /// Validates the social media platform publication identifier.
        /// </summary>
        private void ValidateSocialMediaPlatformPublicationId()
        {
            if (this.SocialMediaPlatformPublicationId.Length > SocialMediaPlatformPublicationIdMaxLenght)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, nameof(SocialMediaPlatformPublicationId), SocialMediaPlatformPublicationIdMaxLenght, 1), new string[] { nameof(SocialMediaPlatformPublicationId) }));
            }
        }

        #endregion
    }
}
