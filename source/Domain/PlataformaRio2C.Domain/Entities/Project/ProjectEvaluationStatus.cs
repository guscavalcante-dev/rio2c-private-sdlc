// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 11-11-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-07-2020
// ***********************************************************************
// <copyright file="ProjectEvaluationStatus.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Validation;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>ProjectEvaluationStatus</summary>
    public class ProjectEvaluationStatus : Entity
    {
        public static readonly int NameMinLength = 1;
        public static readonly int NameMaxLength = 50;
        public static readonly int CodeMinLength = 1;
        public static readonly int CodeMaxLength = 50;

        #region Configurations

        public static ProjectEvaluationStatus UnderEvaluation = new ProjectEvaluationStatus("UnderEvaluation");
        public static ProjectEvaluationStatus Accepted = new ProjectEvaluationStatus("Accepted");
        public static ProjectEvaluationStatus Refused = new ProjectEvaluationStatus("Refused");

        #endregion

        public string Name { get; private set; }
        public string Code { get; private set; }
        public bool IsEvaluated { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="ProjectEvaluationStatus"/> class.</summary>
        protected ProjectEvaluationStatus()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="ProjectEvaluationStatus"/> class.</summary>
        /// <param name="code">The code.</param>
        public ProjectEvaluationStatus(string code)
        {
            this.Code = code?.Trim();
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
    }
}
