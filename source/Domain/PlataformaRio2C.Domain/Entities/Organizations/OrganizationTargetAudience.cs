// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 09-09-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 02-15-2020
// ***********************************************************************
// <copyright file="OrganizationTargetAudience.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>OrganizationTargetAudience</summary>
    public class OrganizationTargetAudience : Entity
    {
        public int OrganizationId { get; private set; }
        public int TargetAudienceId { get; private set; }

        public virtual Organization Organization { get; private set; }
        public virtual TargetAudience TargetAudience { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="OrganizationTargetAudience"/> class.</summary>
        /// <param name="organization">The organization.</param>
        /// <param name="targetAudience">The target audience.</param>
        /// <param name="userId">The user identifier.</param>
        public OrganizationTargetAudience(Organization organization, TargetAudience targetAudience, int userId)
        {
            this.Organization = organization;
            this.OrganizationId = organization?.Id ?? 0;
            this.TargetAudience = targetAudience;
            this.TargetAudienceId = targetAudience?.Id ?? 0;

            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.UtcNow;
            this.CreateUserId = this.UpdateUserId = userId;
        }

        /// <summary>Initializes a new instance of the <see cref="OrganizationTargetAudience"/> class.</summary>
        protected OrganizationTargetAudience()
        {
        }

        /// <summary>Updates the specified user identifier.</summary>
        /// <param name="userId">The user identifier.</param>
        public void Update(int userId)
        {
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

            //this.ValidateValue();
            //this.ValidateLanguage();

            return this.ValidationResult.IsValid;
        }

        ///// <summary>Validates the value.</summary>
        //public void ValidateValue()
        //{
        //    //if (string.IsNullOrEmpty(this.Value?.Trim()))
        //    //{
        //    //    this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.Descriptions), new string[] { "Descriptions" }));
        //    //}

        //    if (this.Value?.Trim().Length < ValueMinLength || this.Value?.Trim().Length > ValueMaxLength)
        //    {
        //        this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.Descriptions, ValueMaxLength, ValueMinLength), new string[] { "Descriptions" }));
        //    }
        //}

        ///// <summary>Validates the language.</summary>
        //public void ValidateLanguage()
        //{
        //    if (this.Language == null)
        //    {
        //        this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.Language), new string[] { "Descriptions" }));
        //    }
        //}

        #endregion
    }
}
