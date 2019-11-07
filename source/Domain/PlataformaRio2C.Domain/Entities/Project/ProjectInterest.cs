// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 11-07-2019
// ***********************************************************************
// <copyright file="ProjectInterest.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using PlataformaRio2C.Domain.Validation;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>ProjectInterest</summary>
    public class ProjectInterest : Entity
    {
        public int ProjectId { get; private set; }
        public int InterestId { get; private set; }

        public virtual Project Project { get; private set; }
        public virtual Interest Interest { get; private set; }
        //public virtual Guid ProjectUid { get; private set; }
        //public virtual Guid InterestUid { get; private set; }

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
            this.CreateDate = this.UpdateDate = DateTime.Now;
            this.CreateUserId = this.UpdateUserId = userId;
        }

        /// <summary>Initializes a new instance of the <see cref="ProjectInterest"/> class.</summary>
        protected ProjectInterest()
        {
        }

        /// <summary>Updates the specified user identifier.</summary>
        /// <param name="userId">The user identifier.</param>
        public void Update(int userId)
        {
            this.IsDeleted = false;
            this.UpdateDate = DateTime.Now;
            this.UpdateUserId = userId;
        }

        /// <summary>Deletes the specified user identifier.</summary>
        /// <param name="userId">The user identifier.</param>
        public void Delete(int userId)
        {
            this.IsDeleted = true;
            this.UpdateDate = DateTime.Now;
            this.UpdateUserId = userId;
        }

        #region Validations

        /// <summary>Returns true if ... is valid.</summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.</returns>
        public override bool IsValid()
        {
            this.ValidationResult = new ValidationResult();

            //this.ValidateName();
            //this.ValidateDescriptions();

            return this.ValidationResult.IsValid;
        }

        ///// <summary>Validates the name.</summary>
        //public void ValidateName()
        //{
        //    if (string.IsNullOrEmpty(this.Name?.Trim()))
        //    {
        //        this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.Name), new string[] { "Name" }));
        //    }

        //    if (this.Name?.Trim().Length < NameMinLength || this.Name?.Trim().Length > NameMaxLength)
        //    {
        //        this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.Name, NameMaxLength, NameMinLength), new string[] { "Name" }));
        //    }
        //}

        ///// <summary>Validates the descriptions.</summary>
        //public void ValidateDescriptions()
        //{
        //    foreach (var description in this.Descriptions?.Where(d => !d.IsValid())?.ToList())
        //    {
        //        this.ValidationResult.Add(description.ValidationResult);
        //    }
        //}

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