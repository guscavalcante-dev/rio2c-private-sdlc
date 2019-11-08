// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 11-08-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 11-08-2019
// ***********************************************************************
// <copyright file="ProjectTargetAudience.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using PlataformaRio2C.Domain.Validation;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>ProjectTargetAudience</summary>
    public class ProjectTargetAudience : Entity
    {
        public int ProjectId { get; private set; }
        public int TargetAudienceId { get; private set; }

        public virtual Project Project { get; private set; }
        public virtual TargetAudience TargetAudience { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="ProjectTargetAudience"/> class.</summary>
        /// <param name="project">The project.</param>
        /// <param name="targetAudience">The target audience.</param>
        /// <param name="userId">The user identifier.</param>
        public ProjectTargetAudience(Project project, TargetAudience targetAudience, int userId)
        {
            this.ProjectId = project?.Id ?? 0;
            this.Project = project;
            this.TargetAudienceId = targetAudience?.Id ?? 0;
            this.TargetAudience = targetAudience;

            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.Now;
            this.CreateUserId = this.UpdateUserId = userId;
        }

        /// <summary>Initializes a new instance of the <see cref="ProjectTargetAudience"/> class.</summary>
        protected ProjectTargetAudience()
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

            //this.ValidateValue();
            //this.ValidateLanguage();

            return this.ValidationResult.IsValid;
        }

        ///// <summary>Validates the value.</summary>
        //public void ValidateValue()
        //{
        //    //if (string.IsNullOrEmpty(this.Value?.Trim()))
        //    //{
        //    //    this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.Descriptions), new string[] { "Descriptions" }));
        //    //}

        //    if (this.Value?.Trim().Length < ValueMinLength || this.Value?.Trim().Length > ValueMaxLength)
        //    {
        //        this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.Descriptions, ValueMaxLength, ValueMinLength), new string[] { "Descriptions" }));
        //    }
        //}

        ///// <summary>Validates the language.</summary>
        //public void ValidateLanguage()
        //{
        //    if (this.Language == null)
        //    {
        //        this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.Language), new string[] { "Descriptions" }));
        //    }
        //}

        #endregion
    }
}
