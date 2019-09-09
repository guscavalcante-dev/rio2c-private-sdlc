// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 09-09-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-09-2019
// ***********************************************************************
// <copyright file="OrganizationActivity.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>OrganizationActivity</summary>
    public class OrganizationActivity : Entity
    {
        public int OrganizationId { get; private set; }
        public int ActivityId { get; private set; }

        public virtual Organization Organization { get; private set; }
        public virtual Activity Activity { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="OrganizationActivity"/> class.</summary>
        /// <param name="organization">The organization.</param>
        /// <param name="activity">The activity.</param>
        /// <param name="userId">The user identifier.</param>
        public OrganizationActivity(Organization organization, Activity activity, int userId)
        {
            this.Organization = organization;
            this.OrganizationId = organization?.Id ?? 0;
            this.Activity = activity;
            this.ActivityId = activity?.Id ?? 0;
            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.Now;
            this.CreateUserId = this.UpdateUserId = userId;
        }

        /// <summary>Initializes a new instance of the <see cref="OrganizationDescription"/> class.</summary>
        protected OrganizationActivity()
        {
        }

        /// <summary>Updates the specified user identifier.</summary>
        /// <param name="userId">The user identifier.</param>
        public void Update(int userId)
        {
            this.IsDeleted = false;
            this.UpdateDate = DateTime.Now;
            this.UpdateUserId = userId;
        }

        /// <summary>Deletes the specified user identifier.</summary>
        /// <param name="userId">The user identifier.</param>
        public void Delete(int userId)
        {
            this.IsDeleted = true;
            this.UpdateDate = DateTime.Now;
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
