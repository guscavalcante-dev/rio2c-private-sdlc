// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 01-18-2025
//
// Last Modified By : Daniel Giese Rodrigues
// Last Modified On : 01-20-2025
// ***********************************************************************
// <copyright file="MusicBusinessRoundProjectPlayerCategory.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Validation;

namespace PlataformaRio2C.Domain.Entities
{
    public class MusicBusinessRoundProjectPlayerCategory : Entity
    {
        public static readonly int AdditionalInfoMaxLength = 200;
        public int MusicBusinessRoundProjectId { get; private set; }
        public virtual MusicBusinessRoundProject MusicBusinessRoundProject { get; private set; }
        public int PlayerCategoryId { get; private set; }
        public virtual PlayerCategory PlayerCategory { get; private set; }
        public string AdditionalInfo { get; private set; }

        public override bool IsValid()
        {
            this.ValidationResult = new ValidationResult();

            //TODO: Implement validations here

            return this.ValidationResult.IsValid;
        }
    }
}