// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Daniel Giese Rodrigues
// Created          : 05-09-2025
//
// Last Modified By : Daniel Giese Rodrigues
// Last Modified On : 05-09-2025
// ***********************************************************************
// <copyright file="INegotiationValidationService.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaRio2C.Application.Interfaces.Common
{
    public interface INegotiationValidationService
    {
        Task<ValidationResult> ValidateOverbookingAsync(
            int editionId,
            DateTimeOffset startDate,
            DateTimeOffset endDate,
            Guid buyerOrganizationId,
            Guid sellerOrganizationId);

        Task<ValidationResult> ValidateOverbookingDatesAsync(
         int editionId,
         DateTimeOffset dayStart,
         DateTimeOffset dayEnd,
         Guid buyerOrganizationId,
         Guid sellerOrganizationId);
    }
}
