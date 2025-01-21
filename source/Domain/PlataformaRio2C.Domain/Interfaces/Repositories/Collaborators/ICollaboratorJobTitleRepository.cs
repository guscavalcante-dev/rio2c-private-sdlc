using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlataformaRio2C.Domain.Interfaces
{
    public interface ICollaboratorJobTitleRepository : IRepository<CollaboratorJobTitle>
    {
        Task<IEnumerable<CollaboratorJobTitleBaseDto>> FindAllJobTitlesDtosByCollaboratorId(int id);
    }
}
