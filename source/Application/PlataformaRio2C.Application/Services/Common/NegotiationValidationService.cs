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


    public async Task<ValidationResult> ValidateOverbookingDatesAsync(
        int editionId,
        DateTimeOffset dayStart,
        DateTimeOffset dayEnd,
        Guid buyerOrganizationId,
        Guid sellerOrganizationId)
    {
        var validationResult = new ValidationResult();

        // 1. Buscar reuniões agendadas
        var scheduled = await this.negotiationRepository
            .FindAllScheduledNegotiationsDtosAsync(editionId, null, dayStart, dayEnd);

        // 2. Pega só reuniões onde o buyer OU seller estejam envolvidos
        var relevantMeetings = scheduled.Where(ndto =>
            ndto.ProjectBuyerEvaluationDto?.BuyerAttendeeOrganizationDto?.AttendeeOrganization?.Organization.Uid == buyerOrganizationId ||
            ndto.ProjectBuyerEvaluationDto?.ProjectDto?.SellerAttendeeOrganizationDto?.AttendeeOrganization?.Organization.Uid == sellerOrganizationId
        ).OrderBy(n => n.Negotiation.StartDate).ToList();

        // 3. Define duração da reunião (ex: 20 minutos)
        var meetingDuration = TimeSpan.FromMinutes(20);

        // 4. Verifica se há algum espaço disponível
        var current = dayStart;

        foreach (var meeting in relevantMeetings)
        {
            if (meeting.Negotiation.StartDate >= current.Add(meetingDuration))
            {
                // Achou um intervalo livre
                return validationResult; // sem erro
            }

            // Atualiza 'current' pro fim da reunião atual se for depois do que já tinha
            if (meeting.Negotiation.EndDate > current)
            {
                current = meeting.Negotiation.EndDate;
            }
        }

        // Checar se ainda sobra tempo até o final do dia
        if (current.Add(meetingDuration) <= dayEnd)
        {
            return validationResult; // Tem slot no final do dia
        }

        // Se chegou aqui, não tem slot livre
        validationResult.Add(new ValidationError(string.Format(
            Messages.HasBusinessRoundScheduled,
            Labels.TheM,
            Labels.Player, // ou Producer dependendo de quem for
            $"{dayStart.ToBrazilTimeZone().ToStringHourMinute()} - {dayEnd.ToBrazilTimeZone().ToShortTimeString()}"),
            new[] { "ToastrError" }));

        return validationResult;
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

