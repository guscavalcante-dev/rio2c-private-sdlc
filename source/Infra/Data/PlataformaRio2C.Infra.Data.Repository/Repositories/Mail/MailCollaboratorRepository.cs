using PlataformaRio2C.Infra.Data.Context;
using PlataformaRio2C.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Validation;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    public class MailCollaboratorRepository : Repository<PlataformaRio2CContext, MailCollaborator>, IMailCollaboratorRepository
    {
        private PlataformaRio2CContext _context;

        public MailCollaboratorRepository(PlataformaRio2CContext context)
            : base(context)
        {
            _context = context;
        }

        //public IEnumerable<MailCollaborator> AllMailCollaborator(IEnumerable<Collaborator> collaborator, string subject)
        //{
        //    throw new NotImplementedException();
        //}

        //public void Create(Mail mail, Collaborator collaborator)
        //{
        //    DateTime sendDate = DateTime.Now;

        //    MailCollaborator entity = new MailCollaborator(mail, collaborator, sendDate);

        //    base.Create(entity);
        //}

        //public void Create(MailCollaborator entity)
        //{

        //    base.Create(entity);
        //}

        public MailCollaborator GetLast(string subject, string search)
        {
            return _context.MailCollaborators
                .Where(a => a.Mail.Subject == subject
                && (a.Collaborator.User.Email == search || a.Collaborator.Name == search))
                .FirstOrDefault();
        }

        public MailCollaborator GetMailCollaborator(Mail mail, Collaborator collaborator)
        {
            return base.Get(a => a.IdMail == mail.Id && a.IdCollaborator == collaborator.Id);
        }


    }
}
