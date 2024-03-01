// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 02-26-2024
//
// Last Modified By : Renan Valentim
// Last Modified On : 02-26-2024
// ***********************************************************************
// <copyright file="CreatorProjectInterest.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Domain.Entities
{
    public class CreatorProjectInterest : Entity
    {
        public static readonly int AdditionalInfoMaxLength = 200;

        public int CreatorProjectId { get; private set; }
        public int InterestId { get; private set; }
        public string AdditionalInfo { get; private set; }

        public virtual CreatorProject CreatorProject { get; private set; }
        public virtual Interest Interest { get; private set; }

        #region Validations

        public override bool IsValid()
        {
            this.ValidationResult = new ValidationResult();

            this.ValidateAdditionalInfo();

            return this.ValidationResult.IsValid;
        }

        private void ValidateAdditionalInfo()
        {
            if (string.IsNullOrEmpty(this.AdditionalInfo) || this.AdditionalInfo.Length > AdditionalInfoMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, nameof(AdditionalInfo), AdditionalInfoMaxLength, 1), new string[] { nameof(AdditionalInfo) }));
            }
        }

        #endregion
    }
}
