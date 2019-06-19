using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace PlataformaRio2C.Domain.Interfaces
{
    public interface ICollaboratorService : IService<Collaborator>
    {
        Collaborator GetByUserId(int id);
        Collaborator GetByUserEmail(string email);
        Collaborator GetWithProducerByUserId(int id);
        Collaborator GetWithPlayerAndProducerUserId(int id);
        Collaborator GetStatusRegisterCollaboratorByUserId(int id);        
        Collaborator GetImage(Guid uid);
        IEnumerable<Collaborator> GetOptions(Expression<Func<Collaborator, bool>> filter);
        IEnumerable<Collaborator> GetOptionsChat(int userId);
        Address GetAddress(Guid playerUid);
    }
}
