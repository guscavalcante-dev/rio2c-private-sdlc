// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 09-26-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-19-2019
// ***********************************************************************
// <copyright file="TicketType.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>CollaboratorType</summary>
    public class CollaboratorRole : Entity
    {
        public static readonly int NameMinLength = 1;
        public static readonly int NameMaxLength = 300;

        public string Name { get; private set; }
        public bool HasAdditionalInfo { get; private set; }

                /// <summary>Initializes a new instance of the <see cref="CollaboratorType"/> class.</summary>
        private CollaboratorRole()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="CollaboratorType"/> class.</summary>
        /// <param name="collaboratorTypeUid">The collaborator type uid.</param>
        /// <param name="name">The name.</param>
        public CollaboratorRole(Guid collaboratorTypeUid, string name)
        {
            this.Uid = collaboratorTypeUid;
            this.Name = name?.Trim();
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