// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 02-26-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 02-28-2020
// ***********************************************************************
// <copyright file="MusicProject.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;

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
        public static readonly int Clipping1UrlMinLength = 1;
        public static readonly int Clipping1UrlMaxLength = 300;
        public static readonly int Clipping2UrlMinLength = 1;
        public static readonly int Clipping2UrlMaxLength = 300;
        public static readonly int Clipping3UrlMinLength = 1;
        public static readonly int Clipping3UrlMaxLength = 300;
        public static readonly int ReleaseMinLength = 1;
        public static readonly int ReleaseMaxLength = 12000;
        public static readonly int ReasonMinLength = 1;
        public static readonly int ReasonMaxLength = 500;

        public int AttendeeMusicBandId { get; private set; }
        public string VideoUrl { get; private set; }
        public string Music1Url { get; private set; }
        public string Music2Url { get; private set; }
        public string Release { get; private set; }
        public string Clipping1 { get; private set; }
        public string Clipping2 { get; private set; }
        public string Clipping3 { get; private set; }
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
        /// <param name="release">The release.</param>
        /// <param name="clipping1">The clipping1.</param>
        /// <param name="clipping2">The clipping2.</param>
        /// <param name="clipping3">The clipping3.</param>
        /// <param name="userId">The user identifier.</param>
        public MusicProject(
            AttendeeMusicBand attendeeMusicBand,
            string videoUrl,
            string music1Url,
            string music2Url,
            string release,
            string clipping1,
            string clipping2,
            string clipping3,
            int userId)
        {
            this.AttendeeMusicBandId = attendeeMusicBand?.Id ?? 0;
            this.AttendeeMusicBand = attendeeMusicBand;
            this.VideoUrl = videoUrl?.Trim();
            this.Music1Url = music1Url?.Trim();
            this.Music2Url = music2Url?.Trim();
            this.Release = release?.Trim();
            this.Clipping1 = clipping1?.Trim();
            this.Clipping2 = clipping2?.Trim();
            this.Clipping3 = clipping3?.Trim();

            this.IsDeleted = false;
            this.CreateUserId = this.UpdateUserId = userId;
            this.CreateDate = this.UpdateDate = DateTime.UtcNow;
        }

        /// <summary>Initializes a new instance of the <see cref="MusicProject"/> class.</summary>
        protected MusicProject()
        {
        }

        //TODO: Implement validations

        #region Validations

        /// <summary>Returns true if ... is valid.</summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.</returns>
        public override bool IsValid()
        {
            this.ValidationResult = new ValidationResult();

            this.ValidateRelease();

            return this.ValidationResult.IsValid;
        }

        /// <summary>Validates the release.</summary>
        public void ValidateRelease()
        {
            if (!string.IsNullOrEmpty(this.Release) && this.Release?.Trim().Length > ReleaseMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.Release, ReleaseMaxLength, 1), new string[] { "Release" }));
            }
        }

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

