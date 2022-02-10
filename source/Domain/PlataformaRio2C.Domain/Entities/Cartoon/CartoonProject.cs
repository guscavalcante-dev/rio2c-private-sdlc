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
        public static readonly int TitleMinLength = 1;
        public static readonly int TitleMaxLength = 300;

        public static readonly int LogLineMinLength = 1;
        public static readonly int LogLineMaxLength = 3000;

        public static readonly int SummaryMinLength = 1;
        public static readonly int SummaryMaxLength = 3000;

        public static readonly int MotivationMinLength = 1;
        public static readonly int MotivationMaxLength = 3000;

        public static readonly int EachEpisodePlayingTimeMinLength = 1;
        public static readonly int EachEpisodePlayingTimeMaxLength = 10;

        public static readonly int TotalValueOfProjectMinLength = 0;
        public static readonly int TotalValueOfProjectMaxLength = 50;

        public string Title { get; private set; }
        public string LogLine { get; private set; }
        public string Summary { get; private set; }
        public string Motivation { get; private set; }
        public int NumberOfEpisodes { get; private set; }
        public string EachEpisodePlayingTime { get; private set; }
        public string TotalValueOfProject { get; private set; }
        public int CartoonProjectFormatId { get; private set; }

        public virtual CartoonProjectFormat CartoonProjectFormat { get; private set; }
        public virtual ICollection<CartoonProjectOrganization> CartoonProjectOrganizations { get; private set; }
        public virtual ICollection<AttendeeCartoonProject> AttendeeCartoonProjects { get; private set; }
        public virtual ICollection<CartoonProjectCreator> CartoonProjectCreators { get; private set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="CartoonProject"/> class.
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
        /// <param name="cartoonProjectCompanyApiDto">The cartoon project company API dto.</param>
        /// <param name="cartoonProjectCreatorApiDtos">The cartoon project creator API dtos.</param>
        /// <param name="userId">The user identifier.</param>
        public CartoonProject(
            Edition edition,
            string title,
            string logLine,
            string summary,
            string motivation,
            int numberOfEpisodes,
            string eachEpisodePlayingTime,
            string totalValueOfProject,
            CartoonProjectFormat cartoonProjectFormat,
            CartoonProjectCompanyApiDto cartoonProjectCompanyApiDto,
            List<CartoonProjectCreatorApiDto> cartoonProjectCreatorApiDtos,
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

            this.AddCartoonProjectOrganization(
                cartoonProjectCompanyApiDto,
                userId);

            this.AddCartoonProjectCreators(
                cartoonProjectCreatorApiDtos,
                userId);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CartoonProject" /> class.
        /// </summary>
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
        /// <param name="cartoonProjectCompanyApiDto">The cartoon project company API dto.</param>
        /// <param name="cartoonProjectCreatorApiDtos">The cartoon project creator API dtos.</param>
        /// <param name="userId">The user identifier.</param>
        public void Update(
            Edition edition,
            string title,
            string logLine,
            string summary,
            string motivation,
            int numberOfEpisodes,
            string eachEpisodePlayingTime,
            string totalValueOfProject,
            CartoonProjectFormat cartoonProjectFormat,
            CartoonProjectCompanyApiDto cartoonProjectCompanyApiDto,
            List<CartoonProjectCreatorApiDto> cartoonProjectCreatorApiDtos,
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

            this.AddCartoonProjectOrganization(
                cartoonProjectCompanyApiDto,
                userId);

            this.AddCartoonProjectCreators(
                cartoonProjectCreatorApiDtos,
                userId);
        }

        /// <summary>
        /// Evaluates the specified edition.
        /// </summary>
        /// <param name="edition">The edition.</param>
        /// <param name="evaluatorUser">The evaluator user.</param>
        /// <param name="grade">The grade.</param>
        public void Evaluate(Edition edition, User evaluatorUser, decimal grade)
        {
            var attendeeCartoonProject = this.GetAttendeeCartoonProjectByEditionId(edition.Id);
            attendeeCartoonProject?.Evaluate(evaluatorUser, grade);
        }

        /// <summary>
        /// Gets the attendee innovation organization by edition identifier.
        /// </summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public AttendeeCartoonProject GetAttendeeCartoonProjectByEditionId(int editionId)
        {
            return this.AttendeeCartoonProjects?.FirstOrDefault(aio => aio.Edition.Id == editionId);
        }

        /// <summary>
        /// Deletes the specified user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="isAdmin">if set to <c>true</c> [is admin].</param>
        public void Delete(int userId)
        {
            //this.DeleteProjectBuyerEvaluations(userId);
            base.Delete(userId);
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

        #region Cartoon Project Creators

        /// <summary>
        /// Adds the cartoon project creators.
        /// </summary>
        /// <param name="cartoonProjectCreatorApiDtos">The cartoon project creator API dtos.</param>
        /// <param name="userId">The user identifier.</param>
        private void AddCartoonProjectCreators(List<CartoonProjectCreatorApiDto> cartoonProjectCreatorApiDtos, int userId)
        {
            if (this.CartoonProjectCreators == null)
            {
                this.CartoonProjectCreators = new List<CartoonProjectCreator>();
            }

            foreach (var cartoonProjectCreatorApiDto in cartoonProjectCreatorApiDtos)
            {
                this.CartoonProjectCreators.Add(new CartoonProjectCreator(
                    cartoonProjectCreatorApiDto.FirstName,
                    cartoonProjectCreatorApiDto.LastName,
                    cartoonProjectCreatorApiDto.Document,
                    cartoonProjectCreatorApiDto.Email,
                    cartoonProjectCreatorApiDto.CellPhone,
                    cartoonProjectCreatorApiDto.PhoneNumber,
                    cartoonProjectCreatorApiDto.MiniBio,
                    cartoonProjectCreatorApiDto.IsResponsible,
                    userId,
                    this));
            }
        }

        #endregion

        #region Cartoon Project Organization

        /// <summary>
        /// Updates the cartoon project organization.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="tradeName">Name of the trade.</param>
        /// <param name="document">The document.</param>
        /// <param name="phoneNumber">The phone number.</param>
        /// <param name="reelUrl">The reel URL.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="address">The address.</param>
        private void AddCartoonProjectOrganization(
            CartoonProjectCompanyApiDto cartoonProjectCompanyApiDto,
            int userId)
        {
            if (this.CartoonProjectOrganizations == null)
            {
                this.CartoonProjectOrganizations = new List<CartoonProjectOrganization>();
            }

            this.CartoonProjectOrganizations.Add(new CartoonProjectOrganization(
                cartoonProjectCompanyApiDto.Name,
                cartoonProjectCompanyApiDto.TradeName,
                cartoonProjectCompanyApiDto.Document,
                cartoonProjectCompanyApiDto.PhoneNumber,
                cartoonProjectCompanyApiDto.ReelUrl,
                cartoonProjectCompanyApiDto.Address,
                cartoonProjectCompanyApiDto.ZipCode,
                userId,
                this));
        }

        #endregion

        #region Validations

        /// <summary>Returns true if ... is valid.</summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.</returns>
        public override bool IsValid()
        {
            this.ValidationResult = new ValidationResult();            
            this.ValidateMaxLengths();
            this.ValidateAttendeeCartoonProjects();
            return this.ValidationResult.IsValid;
        }

        /// <summary>
        /// Validates the maximum lengths.
        /// </summary>
        private void ValidateMaxLengths()
        {
            if (!string.IsNullOrEmpty(this.Title) && this.Title?.Trim().Length > TitleMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.Title, TitleMaxLength, TitleMinLength), new string[] { "Title" }));
            }

            if (!string.IsNullOrEmpty(this.LogLine) && this.LogLine?.Trim().Length > LogLineMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.LogLines, LogLineMaxLength, LogLineMinLength), new string[] { "LogLine" }));
            }

            if (!string.IsNullOrEmpty(this.Summary) && this.Summary?.Trim().Length > SummaryMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.Summary, SummaryMaxLength, SummaryMinLength), new string[] { "Summary" }));
            }

            if (!string.IsNullOrEmpty(this.Motivation) && this.Motivation?.Trim().Length > MotivationMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.CreativeMotivation, MotivationMaxLength, MotivationMinLength), new string[] { "Motivation" }));
            }

            if (!string.IsNullOrEmpty(this.EachEpisodePlayingTime) && this.EachEpisodePlayingTime?.Trim().Length > EachEpisodePlayingTimeMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.EachEpisodePlayingTime, EachEpisodePlayingTimeMaxLength, EachEpisodePlayingTimeMinLength), new string[] { "EachEpisodePlayingTime" }));
            }

            if (!string.IsNullOrEmpty(this.TotalValueOfProject) && this.TotalValueOfProject?.Trim().Length > TotalValueOfProjectMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.TotalValueOfProject, TotalValueOfProjectMaxLength, TotalValueOfProjectMinLength), new string[] { "TotalValueOfProject" }));
            }
        }

        /// <summary>
        /// Validates the attendee cartoon projects.
        /// </summary>
        private void ValidateAttendeeCartoonProjects()
        {
            if (this.AttendeeCartoonProjects?.Any() != true)
            {
                return;
            }

            foreach (var attendeeCartoonProject in this.AttendeeCartoonProjects.Where(aiof => !aiof.IsValid()))
            {
                this.ValidationResult.Add(attendeeCartoonProject.ValidationResult);
            }
        }

        #endregion
    }
}

