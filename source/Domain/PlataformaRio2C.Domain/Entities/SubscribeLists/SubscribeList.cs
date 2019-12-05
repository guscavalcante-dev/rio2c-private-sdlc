// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 12-04-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-05-2019
// ***********************************************************************
// <copyright file="SubscribeList.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>SubscribeList</summary>
    public class SubscribeList : Entity
    {
        public static readonly int NameMinLength = 1;
        public static readonly int NameMaxLength = 200;
        public static readonly int DescriptionMinLength = 1;
        public static readonly int DescriptionMaxLength = 2000;
        public static readonly int CodeMinLength = 1;
        public static readonly int CodeMaxLength = 50;

        #region Configurations

        public static SubscribeList UnreadConversationEmail = new SubscribeList("UnreadConversationEmail");

        #endregion

        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }

        //public virtual ICollection<SubscribeListUser> SubscribeListUsers  { get; set; }

        /// <summary>Initializes a new instance of the <see cref="SubscribeList"/> class.</summary>
        protected SubscribeList()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="SubscribeList"/> class.</summary>
        /// <param name="code">The code.</param>
        private SubscribeList(string code)
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

            this.ValidateName();
            this.ValidateDescription();
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

        /// <summary>Validates the description.</summary>
        public void ValidateDescription()
        {
            if (string.IsNullOrEmpty(this.Description?.Trim()))
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.Description), new string[] { "Description" }));
            }

            if (this.Description?.Trim().Length < DescriptionMinLength || this.Description?.Trim().Length > DescriptionMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.Description, DescriptionMaxLength, DescriptionMinLength), new string[] { "Description" }));
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