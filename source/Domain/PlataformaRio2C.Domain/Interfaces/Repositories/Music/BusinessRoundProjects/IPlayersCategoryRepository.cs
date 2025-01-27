using PlataformaRio2C.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlataformaRio2C.Domain.Interfaces.Repositories.Music.BusinessRoundProjects
{
    public interface IPlayersCategoryRepository : IRepository<PlayerCategory>
    {
        Task<List<PlayerCategory>> FindAllByProjectTypeIdAsync(int projectTypeId);
        Task<List<PlayerCategory>> FindAllByUidsAsync(List<Guid> playersCategoryUids);
    }
}
