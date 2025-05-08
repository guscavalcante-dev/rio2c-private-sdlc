using PlataformaRio2C.Application.Interfaces;
using PlataformaRio2C.Application.Interfaces.Common;
using PlataformaRio2C.Application.Services.Common;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
public class NegotiationValidationService : INegotiationValidationService
{
    private readonly INegotiationRepository negotiationRepository;

    public NegotiationValidationService(INegotiationRepository negotiationRepository)
    {
        this.negotiationRepository = negotiationRepository;
    }

    public async Task<ValidationResult> ValidateOverbookingAsync(
        int editionId,
        DateTimeOffset startDate,
        DateTimeOffset endDate,
        Guid buyerOrganizationId,
        Guid sellerOrganizationId)
    {
        var validationResult = new ValidationResult();

        var scheduledNegotiations = await this.negotiationRepository
            .FindAllScheduledNegotiationsDtosAsync(editionId, null, startDate, endDate);

        bool hasPlayerOverbooking = scheduledNegotiations.Any(ndto =>
            ndto.ProjectBuyerEvaluationDto?.BuyerAttendeeOrganizationDto?.AttendeeOrganization?.Organization.Uid == buyerOrganizationId);

        if (hasPlayerOverbooking)
        {
            validationResult.Add(new ValidationError(string.Format(
                Messages.HasBusinessRoundScheduled,
                Labels.TheM,
                Labels.Player,
                $"{startDate.ToBrazilTimeZone().ToStringHourMinute()} - {endDate.ToBrazilTimeZone().ToShortTimeString()}"),
                new[] { "ToastrError" }));
        }

        bool hasProducerOverbooking = scheduledNegotiations.Any(ndto =>
            ndto.ProjectBuyerEvaluationDto?.ProjectDto?.SellerAttendeeOrganizationDto?.AttendeeOrganization?.Organization.Uid == sellerOrganizationId);

        if (hasProducerOverbooking)
        {
            validationResult.Add(new ValidationError(string.Format(
                Messages.HasBusinessRoundScheduled,
                Labels.TheF,
                Labels.Producer,
                $"{startDate.ToBrazilTimeZone().ToStringHourMinute()} - {endDate.ToBrazilTimeZone().ToShortTimeString()}"),
                new[] { "ToastrError" }));
        }

        return validationResult;
    }
}

