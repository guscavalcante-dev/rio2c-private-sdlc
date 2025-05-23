﻿// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 01-04-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 06-26-2021
// ***********************************************************************
// <copyright file="EditionEvent.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>EditionEvent</summary>
    public class EditionEvent : Entity
    {
        public static readonly int NameMinLength = 1;
        public static readonly int NameMaxLength = 200;

        public int EditionId { get; private set; }
        public string Name { get; private set; }
        public DateTimeOffset StartDate { get; private set; }
        public DateTimeOffset EndDate { get; private set; }

        public virtual Edition Edition { get; private set; }

        public virtual ICollection<Conference> Conferences { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="EditionEvent"/> class.</summary>
        /// <param name="editionEventUid">The edition event uid.</param>
        /// <param name="edition">The edition.</param>
        /// <param name="name">The name.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="userId">The user identifier.</param>
        public EditionEvent(
            Guid editionEventUid,
            Edition edition,
            string name,
            DateTime startDate,
            DateTime endDate,
            int userId)
        {
            //this.Uid = editionEventUid;
            this.EditionId = edition?.Id ?? 0;
            this.Edition = edition;
            this.Name = name?.Trim();
            this.StartDate = startDate.ToUtcTimeZone();
            this.EndDate = endDate.ToEndDateTime().ToUtcTimeZone();

            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.UtcNow;
            this.CreateUserId = this.UpdateUserId = userId;
        }

        /// <summary>Initializes a new instance of the <see cref="EditionEvent"/> class.</summary>
        protected EditionEvent()
        {
        }

        /// <summary>Updates the main information.</summary>
        /// <param name="name">The name.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="userId">The user identifier.</param>
        public void UpdateMainInformation(
            string name,
            DateTime startDate,
            DateTime endDate,
            int userId)
        {
            this.Name = name?.Trim();
            this.StartDate = startDate.ToUtcTimeZone();
            this.EndDate = endDate.ToEndDateTime().ToUtcTimeZone();

            this.IsDeleted = false;
            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
        }

        /// <summary>Deletes the specified user identifier.</summary>
        /// <param name="userId">The user identifier.</param>
        public void Delete(int userId)
        {
            this.IsDeleted = true;
            this.DeleteConferences(userId);

            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
        }

        #region Conferences

        /// <summary>Deletes the conferences.</summary>
        /// <param name="userId">The user identifier.</param>
        private void DeleteConferences(int userId)
        {
            if (this.Conferences?.Any() != true)
            {
                return;
            }

            foreach (var conference in this.Conferences.Where(c => !c.IsDeleted))
            {
                conference.Delete(userId);
            }
        }

        #endregion

        #region Validations

        /// <summary>Returns true if ... is valid.</summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.</returns>
        public override bool IsValid()
        {
            this.ValidationResult = new ValidationResult();

            this.ValidateEdition();
            this.ValidateName();
            this.ValidateDates();

            return this.ValidationResult.IsValid;
        }

        /// <summary>Validates the edition.</summary>
        public void ValidateEdition()
        {
            if (this.Edition == null)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.Edition), new string[] { "Edition" }));
            }
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

        /// <summary>Validates the dates.</summary>
        public void ValidateDates()
        {
            if (this.StartDate.Date < this.Edition.StartDate.Date || this.StartDate.Date > this.Edition.EndDate.Date)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenDates, Labels.StartDate, this.Edition.EndDate.ToBrazilTimeZone().ToShortDateString(), this.Edition.StartDate.ToBrazilTimeZone().ToShortDateString()), new string[] { "StartDate" }));
            }

            if (this.EndDate.Date < this.Edition.StartDate.Date || this.EndDate.Date > this.Edition.EndDate.Date)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenDates, Labels.EndDate, this.Edition.EndDate.ToBrazilTimeZone().ToShortDateString(), this.Edition.StartDate.ToBrazilTimeZone().ToShortDateString()), new string[] { "EndDate" }));
            }

            if (this.StartDate.Date > this.EndDate.Date)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyGreaterThanProperty, Labels.EndDate, Labels.StartDate), new string[] { "EndDate" }));
            }
        }

        #endregion
    }
}