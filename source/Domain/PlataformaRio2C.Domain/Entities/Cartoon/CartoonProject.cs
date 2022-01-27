// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 01-24-2022
//
// Last Modified By : Renan Valentim
// Last Modified On : 01-24-2022
// ***********************************************************************
// <copyright file="CartoonProject.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using PlataformaRio2C.Domain.Validation;
using System.Collections.Generic;
using System.Linq;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Domain.Dtos;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>CartoonProject</summary>
    public class CartoonProject : Entity
    {
        public static readonly int EachEpisodePlayingTimeMinLength = 1;
        public static readonly int EachEpisodePlayingTimeMaxLength = 10;
        public static readonly int TotalValueOfProjectMinLength = 1;
        public static readonly int TotalValueOfProjectMaxLength = 50;
        
        public string Title { get; private set; }
        public string LogLine { get; private set; }
        public string Summary { get; private set; }
        public string Motivation { get; private set; }
        public int? NumberOfEpisodes { get; private set; }
        public string EachEpisodePlayingTime { get; private set; }
        public string TotalValueOfProject { get; private set; }
        public int CartoonProjectFormatId { get; private set; }

        public virtual ICollection<AttendeeCartoonProject> AttendeeCartoonProjects { get; private set; }
        public virtual CartoonProjectFormat CartoonProjectFormat { get; private set; }

        private bool IsAdmin = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="CartoonProject"/> class.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="logLine">The log line.</param>
        /// <param name="summary">The summary.</param>
        /// <param name="motivation">The motivation.</param>
        /// <param name="numberOfEpisodes">The number of episodes.</param>
        /// <param name="eachEpisodePlayingTime">The each episode playing time.</param>
        /// <param name="totalValueOfProject">The total value of project.</param>
        /// <param name="cartoonProjectFormat">The cartoon project format.</param>
        /// <param name="userId">The user identifier.</param>
        public CartoonProject(
            Edition edition,
            string title,
            string logLine,
            string summary,
            string motivation,
            int? numberOfEpisodes,
            string eachEpisodePlayingTime,
            string totalValueOfProject,
            CartoonProjectFormat cartoonProjectFormat,
            int userId)
        {
            this.Title = title;
            this.LogLine = logLine;
            this.Summary = summary;
            this.Motivation = motivation;
            this.NumberOfEpisodes = numberOfEpisodes;
            this.EachEpisodePlayingTime = eachEpisodePlayingTime;
            this.TotalValueOfProject = totalValueOfProject;
            this.CartoonProjectFormatId = cartoonProjectFormat.Id;
            this.CartoonProjectFormat = cartoonProjectFormat;

            this.SetCreateDate(userId);

            this.SynchronizeAttendeeCartoonProjects(
                edition,
                userId);
        }

        /// <summary>Initializes a new instance of the <see cref="CartoonProject"/> class.</summary>
        protected CartoonProject()
        {
        }

        /// <summary>
        /// Updates the specified edition.
        /// </summary>
        /// <param name="edition">The edition.</param>
        /// <param name="title">The title.</param>
        /// <param name="logLine">The log line.</param>
        /// <param name="summary">The summary.</param>
        /// <param name="motivation">The motivation.</param>
        /// <param name="numberOfEpisodes">The number of episodes.</param>
        /// <param name="eachEpisodePlayingTime">The each episode playing time.</param>
        /// <param name="totalValueOfProject">The total value of project.</param>
        /// <param name="cartoonProjectFormat">The cartoon project format.</param>
        /// <param name="userId">The user identifier.</param>
        public void Update(
            Edition edition,
            string title,
            string logLine,
            string summary,
            string motivation,
            int? numberOfEpisodes,
            string eachEpisodePlayingTime,
            string totalValueOfProject,
            CartoonProjectFormat cartoonProjectFormat,
            int userId)
        {
            this.Title = title;
            this.LogLine = logLine;
            this.Summary = summary;
            this.Motivation = motivation;
            this.NumberOfEpisodes = numberOfEpisodes;
            this.EachEpisodePlayingTime = eachEpisodePlayingTime;
            this.TotalValueOfProject = totalValueOfProject;
            this.CartoonProjectFormatId = cartoonProjectFormat.Id;
            this.CartoonProjectFormat = cartoonProjectFormat;

            base.SetUpdateDate(userId);

            this.SynchronizeAttendeeCartoonProjects(
                edition,
                userId);
        }

        /// <summary>
        /// Deletes the specified user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="isAdmin">if set to <c>true</c> [is admin].</param>
        public void Delete(int userId, bool isAdmin)
        {
            //this.DeleteProjectBuyerEvaluations(userId);
            base.Delete(userId);
            this.IsAdmin = isAdmin;
        }

        #region Attendee Cartoon Projects

        /// <summary>
        /// Synchronizes the attendee cartoon project.
        /// </summary>
        /// <param name="edition">The edition.</param>
        /// <param name="userId">The user identifier.</param>
        private void SynchronizeAttendeeCartoonProjects(
            Edition edition,
            int userId)
        {
            if (edition == null)
            {
                return;
            }

            if (this.AttendeeCartoonProjects == null)
            {
                this.AttendeeCartoonProjects = new List<AttendeeCartoonProject>();
            }

            var attendeeInnovationOrganization = this.AttendeeCartoonProjects.FirstOrDefault(ao => ao.EditionId == edition.Id);
            if (attendeeInnovationOrganization != null)
            {
                attendeeInnovationOrganization.Restore(userId);
            }
            else
            {
                this.AttendeeCartoonProjects.Add(
                    new AttendeeCartoonProject(
                    edition,
                    this,
                    userId));
            }
        }

        /// <summary>
        /// Gets the attendee attendee cartoon project by edition identifier.
        /// </summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public AttendeeCartoonProject GetAttendeeAttendeeCartoonProjectByEditionId(int editionId)
        {
            return this.AttendeeCartoonProjects?.FirstOrDefault(aio => aio.Edition.Id == editionId);
        }

        /// <summary>
        /// Deletes the attendee cartoon projects.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        private void DeleteAttendeeCartoonProjects(int userId)
        {
            if (this.AttendeeCartoonProjects == null)
            {
                return;
            }

            var attendeeCartoonProjectsToDelete = this.AttendeeCartoonProjects.Where(aio => !aio.IsDeleted).ToList();
            foreach (var attendeeCartoonProject in attendeeCartoonProjectsToDelete)
            {
                attendeeCartoonProject.Delete(userId);
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

            this.ValidateEachEpisodePlayingTime();
            this.ValidateTotalValueOfProject();

            return this.ValidationResult.IsValid;
        }

        /// <summary>Validates the each episode playing time.</summary>
        public void ValidateEachEpisodePlayingTime()
        {
            if (!string.IsNullOrEmpty(this.EachEpisodePlayingTime) && this.EachEpisodePlayingTime?.Trim().Length > EachEpisodePlayingTimeMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.EachEpisodePlayingTime, EachEpisodePlayingTimeMaxLength, EachEpisodePlayingTimeMinLength), new string[] { "EachEpisodePlayingTime" }));
            }
        }

        /// <summary>Validates the total value of project.</summary>
        public void ValidateTotalValueOfProject()
        {
            if (!string.IsNullOrEmpty(this.TotalValueOfProject) && this.TotalValueOfProject?.Trim().Length > TotalValueOfProjectMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.TotalValueOfProject, TotalValueOfProjectMaxLength, TotalValueOfProjectMinLength), new string[] { "TotalValueOfProject" }));
            }
        }
        
        #endregion
    }
}

