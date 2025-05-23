﻿// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 02-26-2020
//
// Last Modified By : Renan Valentim
// Last Modified On : 01-26-2024
// ***********************************************************************
// <copyright file="MusicProject.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>MusicProject</summary>
    public class MusicProject : Entity
    {
        public static readonly int VideoUrlMaxLength = 300;
        public static readonly int VideoUrlPasswordMaxLength = 100;
        public static readonly int Music1UrlMaxLength = 300;
        public static readonly int Music2UrlMaxLength = 300;
        public static readonly int Clipping1MaxLength = 5000;
        public static readonly int Clipping2MaxLength = 5000;
        public static readonly int Clipping3MaxLength = 5000;
        public static readonly int ReleaseMaxLength = 8000;//12000;
        public static readonly int ReasonMaxLength = 500;

        public int AttendeeMusicBandId { get; private set; }
        public string VideoUrl { get; private set; }
        public string VideoUrlPassword { get; private set; }
        public string Music1Url { get; private set; }
        public string Music2Url { get; private set; }
        public string Release { get; private set; }
        public string Clipping1 { get; private set; }
        public string Clipping2 { get; private set; }
        public string Clipping3 { get; private set; }

        public virtual AttendeeMusicBand AttendeeMusicBand { get; private set; }

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
            string videoUrlPassword,
            string music1Url,
            string music2Url,
            string clipping,
            string release,
            int userId)
        {
            this.AttendeeMusicBandId = attendeeMusicBand?.Id ?? 0;
            this.AttendeeMusicBand = attendeeMusicBand;
            this.VideoUrl = videoUrl?.Trim();
            this.VideoUrlPassword = videoUrlPassword;
            this.Music1Url = music1Url?.Trim();
            this.Music2Url = music2Url?.Trim();
            this.Clipping1 = clipping?.Trim();
            this.Release = release?.Trim();

            //this.ProjectEvaluationStatusId = ProjectEvaluationStatus.UnderEvaluation.Id;

            this.SetCreateDate(userId);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MusicProject" /> class.
        /// </summary>
        /// <param name="videoUrl">The video URL.</param>
        /// <param name="videoUrlPassword">The video URL password.</param>
        /// <param name="music1Url">The music1 URL.</param>
        /// <param name="music2Url">The music2 URL.</param>
        /// <param name="clipping1">The clipping1.</param>
        /// <param name="clipping2">The clipping2.</param>
        /// <param name="clipping3">The clipping3.</param>
        /// <param name="release">The release.</param>
        /// <param name="userId">The user identifier.</param>
        public MusicProject(
            string videoUrl,
            string videoUrlPassword,
            string music1Url,
            string music2Url,
            string clipping1,
            string clipping2,
            string clipping3,
            string release,
            int userId)
        {
            this.VideoUrl = videoUrl?.Trim();
            this.VideoUrlPassword = videoUrlPassword;
            this.Music1Url = music1Url?.Trim();
            this.Music2Url = music2Url?.Trim();
            this.Clipping1 = clipping1?.Trim();
            this.Clipping2 = clipping2?.Trim();
            this.Clipping3 = clipping3?.Trim();
            this.Release = release?.Trim();

            this.SetCreateDate(userId);
        }

        /// <summary>Initializes a new instance of the <see cref="MusicProject"/> class.</summary>
        protected MusicProject()
        {
        }


        #region MusicProjects

        /// <summary>Accepts the specified project evaluation statuses.</summary>
        /// <param name="projectEvaluationStatuses">The project evaluation statuses.</param>
        /// <param name="userId">The user identifier.</param>
        public void Accept(List<ProjectEvaluationStatus> projectEvaluationStatuses, int userId)
        {
            var projectEvaluationStatus = projectEvaluationStatuses?.FirstOrDefault(pes => pes.Code == ProjectEvaluationStatus.Accepted.Code);
            //this.ProjectEvaluationStatusId = projectEvaluationStatus?.Id ?? 0;
            //this.ProjectEvaluationStatus = projectEvaluationStatus;

            //this.ProjectEvaluationRefuseReasonId = null;
            //this.ProjectEvaluationRefuseReason = null;
            //this.Reason = null;
            //this.EvaluationUserId = userId;
            //this.EvaluationDate = DateTime.UtcNow;

            this.IsDeleted = false;
            this.UpdateUserId = userId;
            this.UpdateDate = DateTime.UtcNow;
        }

        /// <summary>Refuses the specified project evaluation refuse reason.</summary>
        /// <param name="projectEvaluationRefuseReason">The project evaluation refuse reason.</param>
        /// <param name="reason">The reason.</param>
        /// <param name="projectEvaluationStatuses">The project evaluation statuses.</param>
        /// <param name="userId">The user identifier.</param>
        public void Refuse(ProjectEvaluationRefuseReason projectEvaluationRefuseReason, string reason, List<ProjectEvaluationStatus> projectEvaluationStatuses, int userId)
        {
            var projectEvaluationStatus = projectEvaluationStatuses?.FirstOrDefault(pes => pes.Code == ProjectEvaluationStatus.Refused.Code);
            //this.ProjectEvaluationStatusId = projectEvaluationStatus?.Id ?? 0;
            //this.ProjectEvaluationStatus = projectEvaluationStatus;

            //this.ProjectEvaluationRefuseReasonId = projectEvaluationRefuseReason?.Id;
            //this.ProjectEvaluationRefuseReason = projectEvaluationRefuseReason;
            //this.Reason = reason?.Trim();
            //this.EvaluationUserId = userId;
            //this.EvaluationDate = DateTime.UtcNow;

            this.IsDeleted = false;
            this.UpdateUserId = userId;
            this.UpdateDate = DateTime.UtcNow;
        }

        /// <summary>Deletes the specified user identifier.</summary>
        /// <param name="userId">The user identifier.</param>
        public void Delete(int userId)
        {
            this.IsDeleted = true;
            this.DeleteAttendeeMusicBand(userId);

            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
        }

        #endregion

        #region Attendee Music Band

        /// <summary>Synchronizes the attendee music band collaborators.</summary>
        /// <param name="attendeeMusicBands">The attendee music bands.</param>
        /// <param name="shouldDeleteAttendeeMusicBand">if set to <c>true</c> [should delete music bands].</param>
        /// <param name="userId">The user identifier.</param>
        public void SynchronizeAttendeeMusicBandProject(AttendeeMusicBand attendeeMusicBand, bool shouldDeleteAttendeeMusicBand, int userId)
        {
            if (shouldDeleteAttendeeMusicBand)
            {
                this.DeleteAttendeeMusicBand(userId);
            }

            if (attendeeMusicBand == null)
            {
                return;
            }

            this.AttendeeMusicBand = attendeeMusicBand;
        }

        /// <summary>Deletes the attendee music band.</summary>
        /// <param name="userId">The user identifier.</param>
        private void DeleteAttendeeMusicBand(int userId)
        {
            this.AttendeeMusicBand.Delete(userId);
        }

        #endregion

        #region Validations

        /// <summary>Returns true if ... is valid.</summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.</returns>
        public override bool IsValid()
        {
            this.ValidationResult = new ValidationResult();

            this.ValidateMusicBand();
            this.ValidateVideoUrl();
            this.ValidateVideoUrlPassword();
            this.ValidateMusic1Url();
            this.ValidateMusic2Url();
            this.ValidateRelease();
            this.ValidateClipping1();
            this.ValidateClipping2();
            this.ValidateClipping3();

            return this.ValidationResult.IsValid;
        }

        /// <summary>Determines whether [is evaluation valid].</summary>
        /// <returns>
        ///   <c>true</c> if [is evaluation valid]; otherwise, <c>false</c>.</returns>
        public bool IsEvaluationValid()
        {
            this.ValidationResult = new ValidationResult();

            this.ValidateRefuseReason();

            return this.ValidationResult.IsValid;
        }

        /// <summary>Validates the music band.</summary>
        public void ValidateMusicBand()
        {
            if (this.AttendeeMusicBand == null)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, "MusicBandId"), new string[] { "MusicBandId" }));
            }
        }

        /// <summary>Validates the video URL.</summary>
        public void ValidateVideoUrl()
        {
            if (!string.IsNullOrEmpty(this.VideoUrl) && this.VideoUrl?.Trim().Length > VideoUrlMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, nameof(VideoUrl), VideoUrlMaxLength, 1), new string[] { nameof(VideoUrl) }));
            }
        }

        /// <summary>Validates the video URL.</summary>
        public void ValidateVideoUrlPassword()
        {
            if (!string.IsNullOrEmpty(this.VideoUrlPassword) && this.VideoUrlPassword?.Trim().Length > VideoUrlPasswordMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, nameof(VideoUrlPassword), VideoUrlPasswordMaxLength, 1), new string[] { nameof(VideoUrlPassword) }));
            }
        }

        /// <summary>Validates the music1 URL.</summary>
        public void ValidateMusic1Url()
        {
            if (!string.IsNullOrEmpty(this.Music1Url) && this.Music1Url?.Trim().Length > Music1UrlMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.Music + " 1", Music1UrlMaxLength, 1), new string[] { "Music1Url" }));
            }
        }

        /// <summary>Validates the music2 URL.</summary>
        public void ValidateMusic2Url()
        {
            if (!string.IsNullOrEmpty(this.Music2Url) && this.Music2Url?.Trim().Length > Music2UrlMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.Music + " 2", Music2UrlMaxLength, 1), new string[] { "Music2Url" }));
            }
        }

        /// <summary>Validates the release.</summary>
        public void ValidateRelease()
        {
            if (!string.IsNullOrEmpty(this.Release) && this.Release?.Trim().Length > ReleaseMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.Release, ReleaseMaxLength, 1), new string[] { "Release" }));
            }
        }

        /// <summary>Validates the clipping1.</summary>
        public void ValidateClipping1()
        {
            if (!string.IsNullOrEmpty(this.Clipping1) && this.Clipping1?.Trim().Length > Clipping1MaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.Clipping + " 1", Clipping1MaxLength, 1), new string[] { "Clipping1" }));
            }
        }

        /// <summary>Validates the clipping2.</summary>
        public void ValidateClipping2()
        {
            if (!string.IsNullOrEmpty(this.Clipping2) && this.Clipping2?.Trim().Length > Clipping2MaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.Clipping + " 2", Clipping2MaxLength, 1), new string[] { "Clipping2" }));
            }
        }

        /// <summary>Validates the clipping3.</summary>
        public void ValidateClipping3()
        {
            if (!string.IsNullOrEmpty(this.Clipping3) && this.Clipping3?.Trim().Length > Clipping3MaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.Clipping + " 3", Clipping3MaxLength, 1), new string[] { "Clipping3" }));
            }
        }

        /// <summary>Validates the refuse reason.</summary>
        public void ValidateRefuseReason()
        {
            //if (this.ProjectEvaluationRefuseReason?.HasAdditionalInfo == true && string.IsNullOrEmpty(this.Reason?.Trim()))
            //{
            //    this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.Reason), new string[] { "Reason" }));
            //}

            //if (this.Reason?.Trim().Length > ReasonMaxLength)
            //{
            //    this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.Name, ReasonMaxLength, 1), new string[] { "Reason" }));
            //}
        }

        #endregion
    }
}