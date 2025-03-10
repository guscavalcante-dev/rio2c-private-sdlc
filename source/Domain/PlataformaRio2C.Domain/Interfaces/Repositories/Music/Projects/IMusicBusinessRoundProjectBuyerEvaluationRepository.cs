using PlataformaRio2C.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlataformaRio2C.Domain.Interfaces.Repositories
{
    public interface IMusicBusinessRoundProjectBuyerEvaluationRepository : IRepository<MusicBusinessRoundProjectBuyerEvaluation>
    {
        Task<List<MusicBusinessRoundProjectBuyerEvaluation>> FindAllForGenerateNegotiationsAsync(int editionId);
    }
}
