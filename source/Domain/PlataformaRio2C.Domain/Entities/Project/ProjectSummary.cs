// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 11-01-2019
// ***********************************************************************
// <copyright file="ProjectSummary.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Validation;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>ProjectSummary</summary>
    public class ProjectSummary : Entity
    {
        public static readonly int ValueMinLength = 1;
        public static readonly int ValueMaxLength = 6000;

        public int ProjectId { get; private set; }
        public int LanguageId { get; private set; }
        public string Value { get; private set; }

        public virtual Project Project { get; private set; }
        public virtual Language Language { get; private set; }
        //public virtual string LanguageCode { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="ProjectSummary"/> class.</summary>
        protected ProjectSummary()
        {
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

        //public ProjectSummary(string value, Language language, Project project)
        //{
        //    Value = value;

        //    SetLanguage(language);
        //    SetProject(project);
        //}


        //public ProjectSummary(string value, string languageCode)
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