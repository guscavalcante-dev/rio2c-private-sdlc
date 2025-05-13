// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 09-26-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-11-2023
// ***********************************************************************
// <copyright file="CollaboratorRole.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using System;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>CollaboratorType</summary>
    public class CollaboratorRole : Entity
    {
        public static readonly int NameMinLength = 1;
        public static readonly int NameMaxLength = 300;

        public string Name { get; private set; }
        public bool HasAdditionalInfo { get; private set; }

        /// <summary>Prevents a default instance of the <see cref="CollaboratorRole"/> class from being created.</summary>
        private CollaboratorRole()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="CollaboratorRole"/> class.</summary>
        /// <param name="collaboratorTypeUid">The collaborator type uid.</param>
        /// <param name="name">The name.</param>
        /// <param name="userId">The user identifier.</param>
        public CollaboratorRole(Guid collaboratorTypeUid, string name, int userId)
        {
            this.Uid = collaboratorTypeUid;
            this.Name = name?.Trim();

            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.UtcNow;
            this.CreateUserId = this.UpdateUserId = userId;
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

        #region Validations

        /// <summary>Returns true if ... is valid.</summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.</returns>
        public override bool IsValid()
        {
            this.ValidationResult = new ValidationResult();

            this.ValidateName();

            return this.ValidationResult.IsValid;
        }

        /// <summary>Validates the name.</summary>
        public void ValidateName()
        {
            if (string.IsNullOrEmpty(this.Name?.Trim()))
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.Name), new string[] { "Name" }));
            }

            if (this.Name?.Trim().Length < NameMinLength || this.Name?.Trim().Length > NameMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.Name, NameMaxLength, NameMinLength), new string[] { "Name" }));
            }
        }

        /// <summary>
        /// Translates this instance.
        /// </summary>
        public void Translate(string userInterfaceLanguage)
        {
            this.Name = this.Name?.GetSeparatorTranslation(userInterfaceLanguage, '|');
        }

        #endregion
    }
}