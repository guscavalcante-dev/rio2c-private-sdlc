// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 09-09-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 12-23-2023
// ***********************************************************************
// <copyright file="OrganizationActivity.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>OrganizationActivity</summary>
    public class OrganizationActivity : Entity
    {
        public static readonly int AdditionalInfoMinLength = 1;
        public static readonly int AdditionalInfoMaxLength = 200;

        public int OrganizationId { get; private set; }
        public int ActivityId { get; private set; }
        public string AdditionalInfo { get; private set; }

        public virtual Organization Organization { get; private set; }
        public virtual Activity Activity { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="OrganizationActivity"/> class.</summary>
        /// <param name="organization">The organization.</param>
        /// <param name="activity">The activity.</param>
        /// <param name="additionalInfo">The additional information.</param>
        /// <param name="userId">The user identifier.</param>
        public OrganizationActivity(Organization organization, Activity activity, string additionalInfo, int userId)
        {
            this.Organization = organization;
            this.OrganizationId = organization?.Id ?? 0;
            this.Activity = activity;
            this.ActivityId = activity?.Id ?? 0;
            this.AdditionalInfo = additionalInfo?.Trim();

            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.UtcNow;
            this.CreateUserId = this.UpdateUserId = userId;
        }

        /// <summary>Initializes a new instance of the <see cref="OrganizationActivity"/> class.</summary>
        /// <param name="activity">The activity.</param>
        /// <param name="additionalInfo">The additional information.</param>
        /// <param name="userId">The user identifier.</param>
        public OrganizationActivity(Activity activity, string additionalInfo, int userId)
        {
            this.Activity = activity;
            this.ActivityId = activity?.Id ?? 0;
            this.AdditionalInfo = additionalInfo?.Trim();

            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.UtcNow;
            this.CreateUserId = this.UpdateUserId = userId;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrganizationDescription" /> class.
        /// </summary>
        protected OrganizationActivity()
        {
        }

        /// <summary>Updates the specified additional information.</summary>
        /// <param name="additionalInfo">The additional information.</param>
        /// <param name="userId">The user identifier.</param>
        public void Update(string additionalInfo, int userId)
        {
            this.AdditionalInfo = additionalInfo?.Trim();

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
