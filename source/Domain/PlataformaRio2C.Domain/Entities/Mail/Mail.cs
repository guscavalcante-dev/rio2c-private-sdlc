using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PlataformaRio2C.Domain.Entities
{
    public class Mail : Entity
    {
        public string Message { get; private set; }
        public string Subject { get; private set; }
        public DateTime CreationDate { get; private set; }
        public Guid Uid { get; private set; }

        public Mail()
        {
        }

        public void setMessage(string message)
        {
            Message = message;
        }

        public void setSubject(string subject)
        {
            Subject = subject;
        }

        public override bool IsValid()
        {
            return true;
        }

        public void SetId(int id)
        {
            Id = id;
        }

        public void SetUid(Guid guid) {
            Uid = guid;
        }

        public void SetCreationDate(DateTime date)
        {
            CreationDate = date;
        }
    }
}
