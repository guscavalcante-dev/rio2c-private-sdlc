using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaRio2C.Domain.Interfaces
{
    public interface IMailCollaboratorRepository : IRepository<MailCollaborator>
    {
        MailCollaborator GetLast(string subject, string search);

        MailCollaborator GetMailCollaborator(Mail mail, Collaborator collaborator);

        //IEnumerable<MailCollaborator> AllMailCollaborator(IEnumerable<Collaborator> collaborator, string subject);

    }
}