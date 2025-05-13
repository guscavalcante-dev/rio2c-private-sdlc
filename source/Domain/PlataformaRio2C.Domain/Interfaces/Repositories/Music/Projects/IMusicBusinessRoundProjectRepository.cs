using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using X.PagedList;

namespace PlataformaRio2C.Domain.Interfaces.Repositories.Music.Projects
{
    public interface IMusicBusinessRoundProjectRepository : IRepository<MusicBusinessRoundProject>
    {
        Task<List<MusicBusinessRoundProjectDto>> FindAllMusicBusinessRoundProjectDtosToSellAsync(Guid attendeeOrganizationUid);
        Task<IPagedList<MusicBusinessRoundProjectDto>> FindAllDtosToEvaluateAsync(Guid attendeeCollaboratorUid, string searchKeywords, Guid? evaluationStatusUid, Guid? targetAudienceUid, Guid? interestAreaInterestUid, Guid? businessRoundObjetiveInterestsUid, int page, int pageSize);
        Task<MusicBusinessRoundProjectDto> FindSiteDetailsDtoByProjectUidAsync(Guid projectUid, int? editionId);
        Task<MusicBusinessRoundProjectDto> FindDtoToEvaluateAsync(Guid attendeeCollaboratorUid, Guid projectUid);
        Task<MusicBusinessRoundProjectDto> FindSiteInterestWidgetDtoByProjectUidAsync(Guid guid);
        Task<MusicBusinessRoundProjectDto> FindSiteBuyerCompanyWidgetDtoByProjectUidAsync(Guid projectUid);
        Task<MusicBusinessRoundProjectDto> FindSiteDuplicateDtoByProjectUidAsync(Guid projectUid);
        Task<IPagedList<MusicBusinessRoundProjectDto>> FindAllDropdownDtoPaged(int editionId, string keywords, string customFilter, Guid? buyerOrganizationUid, int page, int pageSize);
    }
}
