using PlataformaRio2C.Application.Common;
using PlataformaRio2C.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaRio2C.Application.ViewModels
{
    public class MailAppViewModel : EntityViewModel<MailAppViewModel, Mail>, IEntityViewModel<Mail>
    {
        public string Message { get; set; }
        public string Subject { get; set; }
        public int Id { get; set; }
        public Guid Uid { get; set; }
        public DateTime CreationDate { get; set; }

        public MailAppViewModel()
            : base()
        {

        }

        public MailAppViewModel(Mail entity)
            : base(entity)
        {
            Message = entity.Message;
            Subject = entity.Subject;
            Id = entity.Id;
            Uid = entity.Uid;
            CreationDate = entity.CreationDate;
        }

        public Mail MapReverse()
        {
            //DateTime creationDate = DateTime.Now;

            Mail entity = new Mail();
            entity.setMessage(Message);
            entity.setSubject(Subject);
            entity.SetId(Id);
            entity.SetUid(Uid);
            entity.SetCreationDate(CreationDate);

            return entity;
        }


        public Mail MapReverse(Mail entity)
        {
            entity.setMessage(entity.Message);
            entity.setSubject(entity.Subject);
            entity.SetId(entity.Id);
            entity.SetUid(entity.Uid);
            entity.SetCreationDate(entity.CreationDate);
            return entity;
        }

        public static IEnumerable<MailAppViewModel> MapList(IEnumerable<Mail> entities)
        {
            foreach (var entity in entities)
            {
                yield return new MailAppViewModel(entity);
            }
        }
    }
}
