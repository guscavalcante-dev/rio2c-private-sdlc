using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context;
using System.Data.Entity;
using System.Linq;
using System;
using System.Collections.Generic;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    public class MessageRepository : Repository<PlataformaRio2CContext, Message>, IMessageRepository
    {
        public MessageRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }

        public override IQueryable<Message> GetAll(bool @readonly = false)
        {
            var consult = this.dbSet
                                .Include(i => i.Sender)
                                .Include(i => i.Recipient);

            return @readonly
              ? consult.AsNoTracking()
              : consult;
        }
        
    }
}
