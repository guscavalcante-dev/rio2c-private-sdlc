﻿// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 02-15-2020
// ***********************************************************************
// <copyright file="ProjectProductionPlan.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>ProjectProductionPlan</summary>
    public class ProjectProductionPlan : Entity
    {
        public static readonly int ValueMinLength = 1;
        public static readonly int ValueMaxLength = 3000;

        public int ProjectId { get; private set; }
        public int LanguageId { get; private set; }
        public string Value { get; private set; }

        public virtual Project Project { get; private set; }
        public virtual Language Language { get; private set; }
        //public virtual string LanguageCode { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="ProjectProductionPlan"/> class.</summary>
        /// <param name="value">The value.</param>
        /// <param name="language">The language.</param>
        /// <param name="userId">The user identifier.</param>
        public ProjectProductionPlan(string value, Language language, int userId)
        {
            this.Value = value?.Trim();
            this.Language = language;
            this.LanguageId = language?.Id ?? 0;

            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.UtcNow;
            this.CreateUserId = this.UpdateUserId = userId;
        }

        /// <summary>Initializes a new instance of the <see cref="ProjectProductionPlan"/> class.</summary>
        protected ProjectProductionPlan()
        {
        }

        /// <summary>Updates the specified production plan.</summary>
        /// <param name="productionPlan">The production plan.</param>
        public void Update(ProjectProductionPlan productionPlan)
        {
            this.Value = productionPlan.Value?.Trim();

            this.IsDeleted = false;
            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = productionPlan.UpdateUserId;
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
            this.ValidateLanguage();

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
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.ProductionPlans, ValueMaxLength, ValueMinLength), new string[] { "ProductionPlans" }));
            }
        }

        /// <summary>Validates the language.</summary>
        public void ValidateLanguage()
        {
            if (this.Language == null)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.Language), new string[] { "ProductionPlans" }));
            }
        }

        #endregion

        #region Old methods

        //public ProjectProductionPlan(string value, Language language, Project project)
        //{
        //    Value = value;

        //    SetLanguage(language);
        //    SetProject(project);
        //}


        //public ProjectProductionPlan(string value, string languageCode)
        //{
        //    Value = value;
        //    LanguageCode = languageCode;
        //}

        //public void SetLanguage(Language language)
        //{
        //    Language = language;
        //    if (Language != null)
        //    {
        //        LanguageId = language.Id;
        //    }
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