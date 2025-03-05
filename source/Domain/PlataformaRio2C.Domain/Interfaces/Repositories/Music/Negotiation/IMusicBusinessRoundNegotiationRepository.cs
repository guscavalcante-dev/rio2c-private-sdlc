using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlataformaRio2C.Domain.Interfaces.Repositories
{
    public interface IMusicBusinessRoundNegotiationRepository : IRepository<MusicBusinessRoundNegotiation>
    {
        Task<int> CountNegotiationNotScheduledAsync(int editionId, bool showAllEditions = false);
        Task<List<MusicBusinessRoundProjectBuyerEvaluationDto>> FindUnscheduledWidgetDtoAsync(int editionId);
        Task<MusicBusinessRoundNegotiation> FindByIdAsync(int negotiationId);
        Task<MusicBusinessRoundNegotiation> FindByUidAsync(Guid negotiationUid);
        Task<MusicBusinessRoundNegotiationDto> FindDtoAsync(Guid negotiationUid);
        Task<MusicBusinessRoundNegotiationDto> FindMainInformationWidgetDtoAsync(Guid negotiationUid);
        Task<MusicBusinessRoundNegotiationDto> FindVirtualMeetingWidgetDtoAsync(Guid negotiationUid);
        Task<List<MusicBusinessRoundNegotiationGroupedByDateDto>> FindScheduledWidgetDtoAsync(int editionId, Guid? buyerOrganizationUid, Guid? sellerOrganizationUid, string projectKeywords, DateTime? negotiationDate, Guid? roomUid, bool showParticipants);
        Task<List<MusicBusinessRoundNegotiation>> FindNegotiationsByEditionIdAsync(int editionId);
        Task<List<MusicBusinessRoundNegotiation>> FindManualScheduledNegotiationsByRoomIdAsync(int roomId, bool showAllRooms = false);
        Task<List<MusicBusinessRoundNegotiation>> FindAutomaticScheduledNegotiationsByRoomIdAsync(int roomId, bool showAllRooms = false);
        Task<List<MusicBusinessRoundNegotiation>> FindScheduledNegotiationsByRoomIdAsync(int roomId, bool showAllRooms = false);
        Task<List<MusicBusinessRoundNegotiationDto>> FindAllScheduledNegotiationsDtosAsync(int editionId, int? attendeeCollaboratorId, DateTimeOffset startDate, DateTimeOffset endDate);
        Task<List<MusicBusinessRoundNegotiationReportGroupedByDateDto>> FindReportWidgetDtoAsync(int editionId, Guid? buyerOrganizationUid, Guid? sellerOrganizationUid, string projectKeywords, DateTime? negotiationDate, Guid? roomUid, bool showParticipants);
        Task<List<MusicBusinessRoundNegotiationGroupedByDateDto>> FindCollaboratorScheduledWidgetDtoAsync(int editionId, Guid? buyerOrganizationUid, Guid? sellerOrganizationUid, string projectKeywords, DateTime? negotiationDate, Guid? attendeeCollaboratorUid);

    }
}
