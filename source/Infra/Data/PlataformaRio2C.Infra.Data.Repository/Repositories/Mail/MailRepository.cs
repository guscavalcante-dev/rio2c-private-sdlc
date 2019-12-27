//using PlataformaRio2C.Infra.Data.Context;
//using PlataformaRio2C.Domain.Entities;
//using System;
//using System.Linq.Expressions;
//using System.Linq;
//using PlataformaRio2C.Domain.Interfaces;

//namespace PlataformaRio2C.Infra.Data.Repository.Repositories
//{
//    public class MailRepository : Repository<PlataformaRio2CContext, Mail>, IMailRepository
//    {
//        private PlataformaRio2CContext _context;

//        public MailRepository(PlataformaRio2CContext context)
//            : base(context)
//        {
//            _context = context;
//        }

//        public override IQueryable<Mail> GetAll(Expression<Func<Mail, bool>> filter)
//        {
//            return this.GetAll().Where(filter);
//        }

//        //public override Mail Get(Expression<Func<Mail, bool>> filter)
//        //{
//        //    return this.GetAll().FirstOrDefault(filter);
//        //}

//        public override Mail Get(object id)
//        {
//            return this.dbSet
//                .SingleOrDefault(x => x.Id == (int)id);
//        }

//        public MailCollaborator GetMailCollaborator(Mail mail, Collaborator collaborator)
//        {
//            throw new NotImplementedException();
//        }

//        //public MailCollaborator GetMailCollaborator(Mail mail, Collaborator collaborator)
//        //{
//        //    return _context.MailCollaborators
//        //        .Where(a => a.Mail == mail && a.Collaborator == collaborator).FirstOrDefault();
//        //}
//    }
//}