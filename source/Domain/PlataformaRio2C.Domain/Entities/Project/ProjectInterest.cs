// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 02-15-2020
// ***********************************************************************
// <copyright file="ProjectInterest.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>ProjectInterest</summary>
    public class ProjectInterest : Entity
    {
        public static readonly int AdditionalInfoMinLength = 1;
        public static readonly int AdditionalInfoMaxLength = 200;

        public int ProjectId { get; private set; }
        public int InterestId { get; private set; }
        public string AdditionalInfo { get; private set; }

        public virtual Project Project { get; private set; }
        public virtual Interest Interest { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="ProjectInterest"/> class.</summary>
        /// <param name="project">The project.</param>
        /// <param name="interest">The interest.</param>
        /// <param name="userId">The user identifier.</param>
        public ProjectInterest(Project project, Interest interest, int userId)
        {
            this.ProjectId = project?.Id ?? 0;
            this.Project = project;
            this.InterestId = interest?.Id ?? 0;
            this.Interest = interest;

            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.UtcNow;
            this.CreateUserId = this.UpdateUserId = userId;
        }

        /// <summary>Initializes a new instance of the <see cref="ProjectInterest"/> class.</summary>
        /// <param name="interest">The interest.</param>
        /// <param name="additionalInfo">The additional information.</param>
        /// <param name="userId">The user identifier.</param>
        public ProjectInterest(Interest interest, string additionalInfo, int userId)
        {
            this.InterestId = interest?.Id ?? 0;
            this.Interest = interest;
            this.AdditionalInfo = additionalInfo?.Trim();

            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.UtcNow;
            this.CreateUserId = this.UpdateUserId = userId;
        }

        /// <summary>Initializes a new instance of the <see cref="ProjectInterest"/> class.</summary>
        protected ProjectInterest()
        {
        }

        /// <summary>Updates the specified project interest.</summary>
        /// <param name="projectInterest">The project interest.</param>
        /// <param name="userId">The user identifier.</param>
        public void Update(ProjectInterest projectInterest, int userId)
        {
            this.AdditionalInfo = projectInterest?.AdditionalInfo?.Trim();

            this.IsDeleted = false;
            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
        }

        /// <summary>Deletes the specified user identifier.</summary>
        /// <param name="userId">The user identifier.</param>
        public void Delete(int userId)
        {
            this.IsDeleted = true;
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

            this.ValidateAdditionalInfo();

            return this.ValidationResult.IsValid;
        }

        /// <summary>Validates the additional information.</summary>
        public void ValidateAdditionalInfo()
        {
            if (!string.IsNullOrEmpty(this.AdditionalInfo) && this.AdditionalInfo?.Trim().Length < AdditionalInfoMinLength && this.AdditionalInfo?.Trim().Length > AdditionalInfoMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, "Additional Info"/*Labels.LastNames*/, AdditionalInfoMaxLength, AdditionalInfoMinLength), new string[] { "AdditionalInfo" }));
            }
        }

        #endregion

        #region Old methods

        //public ProjectInterest(Project project, Interest interest)
        //{
        //    SetProject(project);
        //    SetInterest(interest);
        //}

        //public void SetProject(Project project)
        //{
        //    Project = project;
        //    if (project != null)
        //    {
        //        ProjectId = project.Id;
        //        ProjectUid = project.Uid;
        //    }
        //}

        //public void SetInterest(Interest interest)
        //{
        //    Interest = interest;
        //    if (interest != null)
        //    {
        //        InterestId = interest.Id;
        //        InterestUid = interest.Uid;
        //    }
        //}

        //public override bool IsValid()
        //{
        //    return true;
        //}

        #endregion
    }
}