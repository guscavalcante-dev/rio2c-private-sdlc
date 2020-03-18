// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-17-2020
// ***********************************************************************
// <copyright file="Edition.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>Edition</summary>
    public class Edition : Entity
    {
        public static readonly int NameMinLength = 2;
        public static readonly int NameMaxLength = 50;

        public string Name { get; private set; }
        public int UrlCode { get; private set; }
        public bool IsCurrent { get; private set; }
        public bool IsActive { get; private set; }
        public DateTimeOffset StartDate { get; private set; }
        public DateTimeOffset EndDate { get; private set; }
        public DateTimeOffset SellStartDate { get; private set; }
        public DateTimeOffset SellEndDate { get; private set; }
        public DateTimeOffset ProjectSubmitStartDate { get; private set; }
        public DateTimeOffset ProjectSubmitEndDate { get; private set; }
        public DateTimeOffset ProjectEvaluationStartDate { get; private set; }
        public DateTimeOffset ProjectEvaluationEndDate { get; private set; }
        public DateTimeOffset OneToOneMeetingsScheduleDate { get; private set; }
        public DateTimeOffset NegotiationStartDate { get; private set; }
        public DateTimeOffset NegotiationEndDate { get; private set; }
        public int AttendeeOrganizationMaxSellProjectsCount { get; private set; }
        public int ProjectMaxBuyerEvaluationsCount { get; private set; }
        public DateTimeOffset MusicProjectSubmitStartDate { get; private set; }
        public DateTimeOffset MusicProjectSubmitEndDate { get; private set; }
        public DateTimeOffset MusicProjectEvaluationStartDate { get; private set; }
        public DateTimeOffset MusicProjectEvaluationEndDate { get; private set; }
        public DateTimeOffset InnovationProjectSubmitStartDate { get; private set; }
        public DateTimeOffset InnovationProjectSubmitEndDate { get; private set; }
        public DateTimeOffset InnovationProjectEvaluationStartDate { get; private set; }
        public DateTimeOffset InnovationProjectEvaluationEndDate { get; private set; }
        public DateTimeOffset? AudiovisualNegotiationsCreateStartDate { get; private set; }
        public DateTimeOffset? AudiovisualNegotiationsCreateEndDate { get; private set; }

        public virtual Quiz Quiz { get; private set; }

        public virtual ICollection<AttendeeOrganization> AttendeeOrganizations { get; private set; }
        public virtual ICollection<AttendeeCollaborator> AttendeeCollaborators { get; private set; }
        public virtual ICollection<AttendeeSalesPlatform> AttendeeSalesPlatforms { get; private set; }
        
        /// <summary>Initializes a new instance of the <see cref="Edition"/> class.</summary>
        protected Edition()
        {
        }

        /// <summary>Starts the audiovisual negotiations creation.</summary>
        /// <param name="userId">The user identifier.</param>
        public void StartAudiovisualNegotiationsCreation(int userId)
        {
            this.AudiovisualNegotiationsCreateStartDate = DateTime.UtcNow;
            this.AudiovisualNegotiationsCreateEndDate = null;

            this.IsDeleted = false;
            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
        }

        /// <summary>Finishes the audiovisual negotiations creation.</summary>
        /// <param name="userId">The user identifier.</param>
        public void FinishAudiovisualNegotiationsCreation(int userId)
        {
            this.AudiovisualNegotiationsCreateEndDate = DateTime.UtcNow;

            this.IsDeleted = false;
            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
        }

        #region Validations

        /// <summary>Returns true if ... is valid.</summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.</returns>
        public override bool IsValid()
        {
            this.ValidationResult = new ValidationResult();

            this.ValidateName();
            this.ValidateUrlCode();

            return this.ValidationResult.IsValid;
        }

        /// <summary>Validates the name.</summary>
        public void ValidateName()
        {
            if (string.IsNullOrEmpty(this.Name?.Trim()))
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.Name), new string[] { "Name" }));
            }

            if (this.Name?.Trim().Length < NameMinLength || this.Name?.Trim().Length > NameMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.Name, NameMaxLength, NameMinLength), new string[] { "Name" }));
            }
        }

        /// <summary>Validates the URL code.</summary>
        public void ValidateUrlCode()
        {
            if (this.UrlCode < 0)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyGreaterThanValue, Labels.Code, 0), new string[] { "UrlCode" }));
            }
        }

        ///// <summary>Validates the dates.</summary>
        //public void ValidateDates()
        //{
        //    if (this.StartDate < this.Edition.StartDate || this.StartDate > this.Edition.EndDate
        //        || this.EndDate < this.Edition.StartDate || this.EndDate > this.Edition.EndDate)
        //    {
        //        this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenDates, Labels.Date, this.Edition.EndDate.ToUserTimeZone().ToShortDateString(), this.Edition.StartDate.ToUserTimeZone().ToShortDateString()), new string[] { "Date" }));
        //    }

        //    if (this.StartDate > this.EndDate)
        //    {
        //        this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyGreaterThanProperty, Labels.EndTime, Labels.StartTime), new string[] { "EndTime" }));
        //    }
        //}

        #endregion

        #region Old Methods

        ///// <summary>Initializes a new instance of the <see cref="Edition"/> class.</summary>
        ///// <param name="name">The name.</param>
        //public Edition(string name)
        //{
        //    Name = name;
        //}

        ///// <summary>Sets the start date.</summary>
        ///// <param name="startDate">The start date.</param>
        //public void SetStartDate(DateTime startDate)
        //{
        //    StartDate = startDate;
        //}

        ///// <summary>Sets the end date.</summary>
        ///// <param name="endDate">The end date.</param>
        //public void SetEndDate(DateTime endDate)
        //{
        //    EndDate = endDate;
        //}

        ///// <summary>Returns true if ... is valid.</summary>
        ///// <returns>
        /////   <c>true</c> if this instance is valid; otherwise, <c>false</c>.</returns>
        //public override bool IsValid()
        //{
        //    return true;
        //}

        #endregion
    }
}