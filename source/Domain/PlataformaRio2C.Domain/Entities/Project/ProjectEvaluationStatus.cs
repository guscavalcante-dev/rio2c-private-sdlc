// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 11-11-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-07-2020
// ***********************************************************************
// <copyright file="ProjectEvaluationStatus.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
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

        public static ProjectEvaluationStatus UnderEvaluation = new ProjectEvaluationStatus(new Guid("44368049-923D-41C6-9EAB-A9CECA05C296"), "UnderEvaluation");
        public static ProjectEvaluationStatus Accepted = new ProjectEvaluationStatus(new Guid("3DFA9E93-CAB8-4A5E-83D1-BF945DD7C137"), "Accepted");
        public static ProjectEvaluationStatus Refused = new ProjectEvaluationStatus(new Guid("CA9C8F5D-C368-4A50-B85C-49C7CFD48625"), "Refused");

        #endregion

        public string Name { get; private set; }
        public string Code { get; private set; }
        public bool IsEvaluated { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="ProjectEvaluationStatus"/> class.</summary>
        protected ProjectEvaluationStatus()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="ProjectEvaluationStatus"/> class.</summary>
        /// <param name="projectEvaluationStatusUid">The project evaluation status uid.</param>
        /// <param name="code">The code.</param>
        private ProjectEvaluationStatus(Guid projectEvaluationStatusUid, string code)
        {
            this.Uid = projectEvaluationStatusUid;
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
