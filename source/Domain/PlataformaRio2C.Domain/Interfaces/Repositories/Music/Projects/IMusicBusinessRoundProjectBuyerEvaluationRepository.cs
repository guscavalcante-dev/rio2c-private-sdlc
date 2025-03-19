using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlataformaRio2C.Domain.Interfaces.Repositories
{
    public interface IMusicBusinessRoundProjectBuyerEvaluationRepository : IRepository<MusicBusinessRoundProjectBuyerEvaluation>
    {
        Task<List<MusicBusinessRoundProjectBuyerEvaluation>> FindAllForGenerateNegotiationsAsync(int editionId);
        Task<MusicBusinessRoundProjectBuyerEvaluationDto> FindDtoAsync(Guid Uid);
        Task<int> CountNegotiationScheduledAsync(int editionId, bool showAllEditions = false);

        Task<List<MusicBusinessRoundProjectBuyerEvaluationDto>> FindUnscheduledWidgetDtoAsync(int editionId);

    }
}
