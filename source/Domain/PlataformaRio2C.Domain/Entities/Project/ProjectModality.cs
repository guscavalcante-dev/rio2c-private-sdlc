// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Gilson Oliveira
// Created          : 10-23-2024
//
// Last Modified By : Gilson Oliveira
// Last Modified On : 10-23-2024
// ***********************************************************************
// <copyright file="ProjectModality.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>ProjectModality</summary>
    public class ProjectModality : Entity
    {
        public static readonly int NameMinLength = 1;
        public static readonly int NameMaxLength = 100;

        #region Configurations

        public static ProjectModality Both = new ProjectModality(1, new Guid("18F04FA4-E0CB-42E5-903A-00790F31F1D5"), "Labels.Both");
        public static ProjectModality BusinessRound = new ProjectModality(2, new Guid("64CD8C96-E6BA-4BEC-9332-17E6EC5A9C62"), Labels.BusinessRound);
        public static ProjectModality Pitching = new ProjectModality(3, new Guid("88382AD8-D192-424D-BACF-1D946E15C471"), Labels.Pitching);

        #endregion

        public string Name { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="ProjectModality"/> class.</summary>
        /// <param name="uid">The uid.</param>
        /// <param name="name">The name.</param>
        public ProjectModality(int id, Guid uid, string name)
        {
            this.Id = id;
            this.Uid = uid;
            this.Name = name;
        }

        /// <summary>Initializes a new instance of the <see cref="ProjectModality"/> class.</summary>
        protected ProjectModality()
        {
            //
        }

        /// <summary>Returns true if ... is valid.</summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.</returns>
        public override bool IsValid()
        {
            if (this.ValidationResult == null)
            {
                this.ValidationResult = new ValidationResult();
            }

            this.ValidateName();

            return this.ValidationResult.IsValid;
        }

        /// <summary>
        /// Validates the curriculum.
        /// </summary>
        private void ValidateName()
        {
            if (this.Name?.Trim()?.Length > NameMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, nameof(Name), NameMaxLength, 1), new string[] { nameof(Name) }));
            }
        }

        /// <summary>
        /// Gets the name of the translated.
        /// </summary>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        /// <returns></returns>
        public string GetTranslatedName(string userInterfaceLanguage)
        {
            return this.Name?.GetSeparatorTranslation(userInterfaceLanguage, '|');
        }
    }
}
