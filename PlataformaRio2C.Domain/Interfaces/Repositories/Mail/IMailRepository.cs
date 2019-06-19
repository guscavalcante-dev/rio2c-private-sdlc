using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace PlataformaRio2C.Domain.Interfaces
{
    public interface IMailRepository: IRepository<Mail>
    {
        IQueryable<Mail> GetAll(Expression<Func<Mail, bool>> filter);

        //Mail Get(Expression<Func<Mail, bool>> filter);

        Mail Get(object id);

        MailCollaborator GetMailCollaborator(Mail mail, Collaborator collaborator);
    }
}