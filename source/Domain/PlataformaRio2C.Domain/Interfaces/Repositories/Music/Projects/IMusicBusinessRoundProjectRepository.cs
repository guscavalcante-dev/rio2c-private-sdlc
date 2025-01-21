using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using PlataformaRio2C.Domain.Dtos.Music.BusinessRoundProject;

namespace PlataformaRio2C.Domain.Interfaces.Repositories.Music.Projects
{
    public interface IMusicBusinessRoundProjectRepository : IRepository<MusicBusinessRoundProject>
    {
        Task<List<MusicBusinessRoundProjectDto>> FindAllMusicBusinessRoundProjectDtosToSellAsync(Guid attendeeOrganizationUid);
    }
}
