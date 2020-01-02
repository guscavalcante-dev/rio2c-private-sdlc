// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-02-2020
// ***********************************************************************
// <copyright file="Conference.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>Conference</summary>
    public class Conference : Entity
    {
        //public static readonly int LocalMinLength = 2;
        //public static readonly int LocalMaxLength = 1000;
        //public static readonly int InfoMaxLength = 3000;

        public int EditionId { get; private set; }
        public int? RoomId { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }

        //public string Info { get; private set; }

        public virtual Edition Edition { get; private set; }
        public virtual Room Room { get; private set; }

        public virtual ICollection<ConferenceTitle> ConferenceTitles { get; private set; }
        public virtual ICollection<ConferenceSynopsis> ConferenceSynopses { get; private set; }
        //public virtual ICollection<ConferenceLecturer> Lecturers { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="Conference"/> class.</summary>
        /// <param name="conferenceUid">The conference uid.</param>
        /// <param name="edition">The edition.</param>
        /// <param name="date">The date.</param>
        /// <param name="startTime">The start time.</param>
        /// <param name="endTime">The end time.</param>
        /// <param name="conferenceTitles">The conference titles.</param>
        /// <param name="userId">The user identifier.</param>
        public Conference(
            Guid conferenceUid,
            Edition edition,
            DateTime date,
            string startTime,
            string endTime,
            List<ConferenceTitle> conferenceTitles,
            int userId)
        {
            //this.Uid = conferenceUid;
            this.EditionId = edition?.Id ?? 0;
            this.Edition = edition;
            this.StartDate = date.JoinDateAndTime(startTime, true);
            this.EndDate = date.JoinDateAndTime(endTime, true);
            this.SynchronizeConferenceTitles(conferenceTitles, userId);

            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.Now;
            this.CreateUserId = this.UpdateUserId = userId;
        }

        /// <summary>Initializes a new instance of the <see cref="Conference"/> class.</summary>
        protected Conference()
        {
        }

        #region Conference Titles

        /// <summary>Synchronizes the conference titles.</summary>
        /// <param name="conferenceTitles">The conference titles.</param>
        /// <param name="userId">The user identifier.</param>
        private void SynchronizeConferenceTitles(List<ConferenceTitle> conferenceTitles, int userId)
        {
            if (this.ConferenceTitles == null)
            {
                this.ConferenceTitles = new List<ConferenceTitle>();
            }

            this.DeleteConferenceTitles(conferenceTitles, userId);

            if (conferenceTitles?.Any() != true)
            {
                return;
            }

            // Create or update job titles
            foreach (var conferenceTitle in conferenceTitles)
            {
                var conferenceTitleDb = this.ConferenceTitles.FirstOrDefault(d => d.Language.Code == conferenceTitle.Language.Code);
                if (conferenceTitleDb != null)
                {
                    conferenceTitleDb.Update(conferenceTitle);
                }
                else
                {
                    this.CreateConferenceTitle(conferenceTitle);
                }
            }
        }

        /// <summary>Deletes the conference titles.</summary>
        /// <param name="newConferenceTitles">The new conference titles.</param>
        /// <param name="userId">The user identifier.</param>
        private void DeleteConferenceTitles(List<ConferenceTitle> newConferenceTitles, int userId)
        {
            var conferenceTitlesToDelete = this.ConferenceTitles.Where(db => newConferenceTitles?.Select(d => d.Language.Code)?.Contains(db.Language.Code) == false && !db.IsDeleted).ToList();
            foreach (var conferenceTitleToDelete in conferenceTitlesToDelete)
            {
                conferenceTitleToDelete.Delete(userId);
            }
        }

        /// <summary>Creates the conference title.</summary>
        /// <param name="conferenceTitle">The conference title.</param>
        private void CreateConferenceTitle(ConferenceTitle conferenceTitle)
        {
            this.ConferenceTitles.Add(conferenceTitle);
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

        /// <summary>Validates the dates.</summary>
        public void ValidateDates()
        {
            if (this.StartDate < this.Edition.StartDate || this.StartDate > this.Edition.EndDate
                || this.EndDate < this.Edition.StartDate || this.EndDate > this.Edition.EndDate)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenDates, Labels.Date, this.Edition.EndDate.ToShortDateString(), this.Edition.StartDate.ToShortDateString()), new string[] { "Date" }));
            }

            if (this.StartDate > this.EndDate)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyGreaterThanProperty, Labels.EndTime, Labels.StartTime), new string[] { "EndTime" }));
            }
        }

        #endregion
    }
}