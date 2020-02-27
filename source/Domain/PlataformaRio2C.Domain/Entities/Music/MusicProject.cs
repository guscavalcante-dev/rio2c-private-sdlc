// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 02-26-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 02-26-2020
// ***********************************************************************
// <copyright file="MusicProject.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>MusicProject</summary>
    public class MusicProject : Entity
    {
        public static readonly int VideoUrlMinLength = 1;
        public static readonly int VideoUrlMaxLength = 300;
        public static readonly int Music1UrlMinLength = 1;
        public static readonly int Music1UrlMaxLength = 300;
        public static readonly int Music2UrlMinLength = 1;
        public static readonly int Music2UrlMaxLength = 300;
        public static readonly int ReasonMinLength = 1;
        public static readonly int ReasonMaxLength = 500;

        public int AttendeeMusicBandId { get; private set; }
        public string VideoUrl { get; private set; }
        public string Music1Url { get; private set; }
        public string Music2Url { get; private set; }
        public DateTimeOffset? ClippingUploadDate { get; private set; }
        public int ProjectEvaluationStatusId { get; private set; }
        public int? ProjectEvaluationRefuseReasonId { get; private set; }
        public string Reason { get; private set; }
        public int? EvaluationUserId { get; private set; }
        public DateTimeOffset? EvaluationEmailSendDate { get; private set; }

        public virtual AttendeeMusicBand AttendeeMusicBand { get; private set; }
        public virtual ProjectEvaluationStatus ProjectEvaluationStatus { get; private set; }
        public virtual ProjectEvaluationRefuseReason ProjectEvaluationRefuseReason { get; private set; }
        public virtual User EvaluationUser { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="MusicProject"/> class.</summary>
        /// <param name="attendeeMusicBand">The attendee music band.</param>
        /// <param name="videoUrl">The video URL.</param>
        /// <param name="music1Url">The music1 URL.</param>
        /// <param name="music2Url">The music2 URL.</param>
        /// <param name="isClippingUploaded">if set to <c>true</c> [is clipping uploaded].</param>
        /// <param name="userId">The user identifier.</param>
        public MusicProject(
            AttendeeMusicBand attendeeMusicBand,
            string videoUrl,
            string music1Url,
            string music2Url,
            bool isClippingUploaded,
            int userId)
        {
            this.AttendeeMusicBandId = attendeeMusicBand?.Id ?? 0;
            this.AttendeeMusicBand = attendeeMusicBand;
            this.VideoUrl = videoUrl?.Trim();
            this.Music1Url = music1Url?.Trim();
            this.Music2Url = music2Url?.Trim();
            this.UpdateClippingUploadDate(isClippingUploaded, false);

            this.IsDeleted = false;
            this.CreateUserId = this.UpdateUserId = userId;
            this.CreateDate = this.UpdateDate = DateTime.UtcNow;
        }

        /// <summary>Initializes a new instance of the <see cref="MusicProject"/> class.</summary>
        protected MusicProject()
        {
        }

        /// <summary>Updates the clipping upload date.</summary>
        /// <param name="isImageUploaded">if set to <c>true</c> [is image uploaded].</param>
        /// <param name="isImageDeleted">if set to <c>true</c> [is image deleted].</param>
        private void UpdateClippingUploadDate(bool isImageUploaded, bool isImageDeleted)
        {
            if (isImageUploaded)
            {
                this.ClippingUploadDate = DateTime.UtcNow;
            }
            else if (isImageDeleted)
            {
                this.ClippingUploadDate = null;
            }
        }

        #region Validations

        /// <summary>Returns true if ... is valid.</summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.</returns>
        public override bool IsValid()
        {
            return true;
        }

        // Create validations

        ///// <summary>Determines whether [is create project valid].</summary>
        ///// <returns>
        /////   <c>true</c> if [is create project valid]; otherwise, <c>false</c>.</returns>
        //public bool IsCreateProjectValid()
        //{
        //    if (this.ValidationResult == null)
        //    {
        //        this.ValidationResult = new ValidationResult();
        //    }

        //    this.ValidateProjectsLimits();
        //    this.ValidateProjects();

        //    return this.ValidationResult.IsValid;
        //}

        ///// <summary>Validates the projects limits.</summary>
        //public void ValidateProjectsLimits()
        //{
        //    if (this.SellProjectsCount > this.GetMaxSellProjectsCount())
        //    {
        //        this.ValidationResult.Add(new ValidationError(Messages.IsNotPossibleCreateProjectLimit, new string[] { "ToastrError" }));
        //    }
        //}

        ///// <summary>Validates the projects.</summary>
        //public void ValidateProjects()
        //{
        //    if (this.SellProjects?.Any() != true)
        //    {
        //        return;
        //    }

        //    foreach (var project in this.SellProjects?.Where(d => !d.IsDeleted && !d.IsCreateValid())?.ToList())
        //    {
        //        this.ValidationResult.Add(project.ValidationResult);
        //    }
        //}

        #endregion
    }
}

