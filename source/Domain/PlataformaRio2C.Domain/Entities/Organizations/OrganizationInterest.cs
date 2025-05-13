// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 09-09-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 01-17-2024
// ***********************************************************************
// <copyright file="OrganizationInterest.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>OrganizationInterest</summary>
    public class OrganizationInterest : Entity
    {
        public static readonly int AdditionalInfoMinLength = 1;
        public static readonly int AdditionalInfoMaxLength = 200;

        public int OrganizationId { get; private set; }
        public int InterestId { get; private set; }
        public string AdditionalInfo { get; private set; }

        public virtual Organization Organization { get; private set; }
        public virtual Interest Interest { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="OrganizationInterest"/> class.</summary>
        /// <param name="organization">The organization.</param>
        /// <param name="interest">The interest.</param>
        /// <param name="userId">The user identifier.</param>
        public OrganizationInterest(Organization organization, Interest interest, int userId)
        {
            this.Organization = organization;
            this.OrganizationId = organization?.Id ?? 0;
            this.Interest = interest;
            this.InterestId = interest?.Id ?? 0;

            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.UtcNow;
            this.CreateUserId = this.UpdateUserId = userId;
        }

        /// <summary>Initializes a new instance of the <see cref="OrganizationInterest"/> class.</summary>
        /// <param name="interest">The interest.</param>
        /// <param name="additionalInfo">The additional information.</param>
        /// <param name="userId">The user identifier.</param>
        public OrganizationInterest(Interest interest, string additionalInfo, int userId)
        {
            this.InterestId = interest?.Id ?? 0;
            this.Interest = interest;
            this.AdditionalInfo = additionalInfo?.Trim();

            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.UtcNow;
            this.CreateUserId = this.UpdateUserId = userId;
        }

        /// <summary>Initializes a new instance of the <see cref="OrganizationInterest"/> class.</summary>
        protected OrganizationInterest()
        {
        }

        /// <summary>
        /// Updates the specified additional information.
        /// </summary>
        /// <param name="additionalInfo">The additional information.</param>
        /// <param name="userId">The user identifier.</param>
        public void Update(string additionalInfo, int userId)
        {
            this.AdditionalInfo = additionalInfo?.Trim();

            this.SetUpdateDate(userId);
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

            this.ValidateAdditionalInfo();

            return this.ValidationResult.IsValid;
        }

        /// <summary>Validates the additional information.</summary>
        public void ValidateAdditionalInfo()
        {
            if (!string.IsNullOrEmpty(this.AdditionalInfo) && this.AdditionalInfo?.Trim().Length < AdditionalInfoMinLength && this.AdditionalInfo?.Trim().Length > AdditionalInfoMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, "Additional Info"/*Labels.LastNames*/, AdditionalInfoMaxLength, AdditionalInfoMinLength), new string[] { "AdditionalInfo" }));
            }
        }

        #endregion
    }
}