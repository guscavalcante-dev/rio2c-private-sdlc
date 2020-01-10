//using PlataformaRio2C.Application.Common;
//using PlataformaRio2C.Domain.Entities;
//using PlataformaRio2C.Infra.CrossCutting.Resources;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Web;

//namespace PlataformaRio2C.Application.ViewModels
//{
//    public class MailCollaboratorAppViewModel : EntityViewModel<MailCollaboratorAppViewModel, MailCollaborator>
//    {
//        #region props
//        public virtual Mail Mail { get; private set; }
//        public virtual Collaborator Collaborator { get; private set; }
//        public int CollaboratorId { get; private set; }
//        public int MailId { get; private set; }
//        public DateTime SendDate { get; private set; }
//        #endregion

//        #region ctor
//        public MailCollaboratorAppViewModel() { }
//        #endregion

//        #region Public Methods

//        public MailCollaboratorAppViewModel(Mail mail, Collaborator collaborator, DateTime sendDate)
//        {
//            SendDate = sendDate;
//            Mail = mail;
//            Collaborator = Collaborator;

//            MailId = mail.Id;
//            CollaboratorId = collaborator.Id;
//        }

//        public MailCollaboratorAppViewModel(MailCollaborator entity)
//        {
//            SendDate = entity.SendDate;
//            Mail = entity.Mail;
//            Collaborator = entity.Collaborator;
//        }

//        public void setSendDate(DateTime sendDate)
//        {
//            SendDate = sendDate;
//        }

//        public MailCollaborator MapReverse()
//        {
//            var MailCollaborator = new Domain.Entities.MailCollaborator(this.Mail, this.Collaborator, this.SendDate);

//            return MailCollaborator;
//        }

//        public MailCollaborator MapReverse(MailCollaborator entity)
//        {
//            entity.setSendDate(SendDate);
//            entity.setIdMail(entity.Mail.Id);
//            entity.setIdCollaborator(entity.Collaborator.Id);

//            return entity;
//        }
//        #endregion
//    }
//}
