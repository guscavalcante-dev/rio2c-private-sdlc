// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 08-31-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-01-2019
// ***********************************************************************
// <copyright file="TicketType.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>TicketType</summary>
    public class TicketType : Entity
    {
        public static readonly int NameMinLength = 1;
        public static readonly int NameMaxLength = 200;
        public static readonly int CodeMinLength = 1;
        public static readonly int CodeMaxLength = 50;

        public string Name { get; private set; }
        public string Code { get; private set; }
        public int RoleId { get; private set; }

        public virtual Role Role { get; private set; }
        public virtual ICollection<AttendeeSalesPlatformTicketType> AttendeeSalesPlatformTicketTypes { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="TicketType"/> class.</summary>
        private TicketType()
        {
        }

        #region Validations

        /// <summary>Returns true if ... is valid.</summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.</returns>
        public override bool IsValid()
        {
            this.ValidationResult = new ValidationResult();

            this.ValidateName();
            this.ValidateCode();

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

        /// <summary>Validates the code.</summary>
        public void ValidateCode()
        {
            if (string.IsNullOrEmpty(this.Code?.Trim()))
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.Code), new string[] { "Code" }));
            }

            if (this.Code?.Trim().Length < CodeMinLength || this.Code?.Trim().Length > CodeMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.Code, CodeMaxLength, CodeMinLength), new string[] { "Code" }));
            }
        }

        #endregion
    }
}