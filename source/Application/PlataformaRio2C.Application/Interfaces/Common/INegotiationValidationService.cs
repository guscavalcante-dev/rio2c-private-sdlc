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
