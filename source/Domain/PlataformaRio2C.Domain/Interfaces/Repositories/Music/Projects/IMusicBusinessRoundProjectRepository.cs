using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using X.PagedList;

namespace PlataformaRio2C.Domain.Interfaces.Repositories.Music.Projects
{
    public interface IMusicBusinessRoundProjectRepository : IRepository<MusicBusinessRoundProject>
    {
        Task<List<MusicBusinessRoundProjectDto>> FindAllMusicBusinessRoundProjectDtosToSellAsync(Guid attendeeOrganizationUid);
        Task<IPagedList<MusicBusinessRoundProjectDto>> FindAllDtosToEvaluateAsync(Guid attendeeCollaboratorUid, string searchKeywords, Guid? evaluationStatusUid, Guid? targetAudienceUid, Guid? interestAreaInterestUid, Guid? businessRoundObjetiveInterestsUid, int page, int pageSize);
        Task<MusicBusinessRoundProjectDto> FindSiteDetailsDtoByProjectUidAsync(Guid projectUid, int editionId);
    }
}
