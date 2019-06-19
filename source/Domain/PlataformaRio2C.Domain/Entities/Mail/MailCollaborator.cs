using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaRio2C.Domain.Entities
{
    public class MailCollaborator : Entity
    {
        public Guid Uid { get; private set; }
        //public int IdMailCollaborator { get; private set; }
        public virtual Mail Mail { get; private set; }
        public virtual Collaborator Collaborator { get; private set; }
        public int IdMail { get; private set; }
        public int IdCollaborator { get; private set; }
        public DateTime SendDate { get; private set; }
        public DateTime CreationDate { get; private set; }

        public MailCollaborator() { }

        //public MailCollaborator(Collaborator entity)
        //{
        //    Collaborator = entity;
        //}

        public MailCollaborator(MailCollaborator entity)
        {
            Mail = entity.Mail;
            Collaborator = entity.Collaborator;
            SendDate = entity.SendDate;
            CreationDate = entity.CreationDate;
            IdCollaborator = entity.IdCollaborator;
            IdMail = entity.IdMail;
        }

        //public MailCollaborator(Mail mail, Collaborator collaborator, DateTime sendDate)
        public MailCollaborator(Mail mail, Collaborator collaborator, DateTime sendDate)
        {
            Uid = Guid.NewGuid();
            IdCollaborator = collaborator.Id;
            IdMail = mail.Id;
            SendDate = sendDate;
            CreationDate = DateTime.Now;
            Mail = mail;
            Collaborator = Collaborator;
        }

        //public void setId(int id)
        //{
        //    IdMailCollaborator = id;
        //}

        public void setIdMail(int id)
        {
            IdMail = id;
        }

        public void setIdCollaborator(int id)
        {
            IdCollaborator = id;
        }

        public void setSendDate(DateTime sendDate)
        {
            SendDate = sendDate;
        }

        //public void setMail(Mail mail)
        //{
        //    Mail = mail;
        //}

        //public void setCollaborator(Collaborator collaborator)
        //{
        //    Collaborator = collaborator;
        //}

        public override bool IsValid()
        {
            return true;
        }
    }
}
