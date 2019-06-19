using PlataformaRio2C.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace PlataformaRio2C.Domain.Interfaces
{
    public interface ICollaboratorRepository : IRepository<Collaborator>
    {
        Collaborator GetById(int id);
        Collaborator GetStatusRegisterCollaboratorByUserId(int id);
        Collaborator GetWithProducerByUserId(int id);
        Collaborator GetWithPlayerAndProducerUserId(int id);
        Collaborator GetWithPlayerAndProducerUid(Guid id);
        Collaborator GetImage(Guid uid);
        IEnumerable<Collaborator> GetOptions(Expression<Func<Collaborator, bool>> filter);
        IEnumerable<Collaborator> GetCollaboratorProducerOptions(Expression<Func<Collaborator, bool>> filter);
        IEnumerable<Collaborator> GetCollaboratorPlayerOptions(Expression<Func<Collaborator, bool>> filter);
        IEnumerable<Collaborator> GetOptionsChat(int userId);

        Collaborator GetBySchedule(Expression<Func<Collaborator, bool>> filter);        
    }    
}
