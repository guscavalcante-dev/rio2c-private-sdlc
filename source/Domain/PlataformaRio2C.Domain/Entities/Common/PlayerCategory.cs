﻿// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 01-18-2025
//
// Last Modified By : Renan Valentim
// Last Modified On : 01-18-2025
// ***********************************************************************
// <copyright file="PlayerCategory.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Validation;

namespace PlataformaRio2C.Domain.Entities
{
    public class PlayerCategory : Entity
    {
        public static readonly int NameMaxLength = 500;

        public int ProjectTypeId { get; private set; }
        public string Name { get; private set; }
        public int DisplayOrder { get; private set; }
        public bool HasAdditionalInfo { get; private set; }

        public override bool IsValid()
        {
            this.ValidationResult = new ValidationResult();

            //TODO: Implement validations here

            return this.ValidationResult.IsValid;
        }
    }
}