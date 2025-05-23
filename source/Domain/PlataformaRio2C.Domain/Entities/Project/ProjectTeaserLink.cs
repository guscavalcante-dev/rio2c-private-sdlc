﻿// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 02-15-2020
// ***********************************************************************
// <copyright file="ProjectTeaserLink.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>ProjectTeaserLink</summary>
    public class ProjectTeaserLink : Entity
    {
        public static readonly int ValueMinLength = 1;
        public static readonly int ValueMaxLength = 3000;

        public int ProjectId { get; private set; }
        public string Value { get; private set; }

        public virtual Project Project { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="ProjectTeaserLink"/> class.</summary>
        /// <param name="value">The value.</param>
        /// <param name="userId">The user identifier.</param>
        public ProjectTeaserLink(string value, int userId)
        {
            this.Value = value?.Trim();

            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.UtcNow;
            this.CreateUserId = this.UpdateUserId = userId;
        }

        /// <summary>Initializes a new instance of the <see cref="ProjectTeaserLink"/> class.</summary>
        protected ProjectTeaserLink()
        {
        }

        /// <summary>Updates the specified value.</summary>
        /// <param name="value">The value.</param>
        /// <param name="userId">The user identifier.</param>
        public void Update(string value, int userId)
        {
            this.Value = value?.Trim();

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

            this.ValidateValue();

            return this.ValidationResult.IsValid;
        }

        /// <summary>Validates the value.</summary>
        public void ValidateValue()
        {
            //if (string.IsNullOrEmpty(this.Value?.Trim()))
            //{
            //    this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.Descriptions), new string[] { "Descriptions" }));
            //}

            if (this.Value?.Trim().Length < ValueMinLength || this.Value?.Trim().Length > ValueMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.TeaserLinks, ValueMaxLength, ValueMinLength), new string[] { "TeaserLinks" }));
            }
        }

        #endregion

        #region Old methods

        //public ProjectTeaserLink(string value, Project project)
        //{
        //    Value = value;
        //    SetProject(project);
        //}

        //public ProjectTeaserLink(string value)
        //{
        //    Value = value;            
        //}

        //public void SetProject(Project project)
        //{
        //    Project = project;

        //    if (Project != null)
        //    {
        //        ProjectId = project.Id;
        //    }
        //}

        //public override bool IsValid()
        //{
        //    return true;
        //}

        #endregion
    }
}