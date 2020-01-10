//using PlataformaRio2C.Domain.Entities;
//using PlataformaRio2C.Domain.Interfaces;
//using PlataformaRio2C.Domain.Validation;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Text;

//namespace PlataformaRio2C.Domain.Services
//{
//    class MailCollaboratorService : Service<MailCollaborator>, IMailService
//    {
//        private readonly IMailCollaboratorRepository _repository;

//        public MailCollaboratorService(IMailCollaboratorRepository repository, IRepositoryFactory repositoryFactory)
//            : base(repository)
//        {
//            _repository = repository;
//        }

//        public ValidationResult Create(Mail entity)
//        {
//            throw new NotImplementedException();
//        }

//        public ValidationResult CreateAll(IEnumerable<Mail> entities)
//        {
//            throw new NotImplementedException();
//        }

//        public ValidationResult CreateMailCollaborator(MailCollaborator model)
//        {
//            return base.Create(model);
//        }




//        public ValidationResult Delete(Mail entity)
//        {
//            throw new NotImplementedException();
//        }

//        public ValidationResult DeleteAll(IEnumerable<Mail> entities)
//        {
//            throw new NotImplementedException();
//        }

//        public Mail Get(Expression<Func<Mail, bool>> filter)
//        {
//            throw new NotImplementedException();
//        }

//        public IEnumerable<Mail> GetAll(Expression<Func<Mail, bool>> filter)
//        {
//            throw new NotImplementedException();
//        }

//        public IEnumerable<Mail> GetAllSimple(Expression<Func<Mail, bool>> filter)
//        {
//            throw new NotImplementedException();
//        }

//        public ValidationResult Update(Mail entity)
//        {
//            throw new NotImplementedException();
//        }

//        public ValidationResult UpdateAll(IEnumerable<Mail> entities)
//        {
//            throw new NotImplementedException();
//        }

//        Mail IService<Mail>.Get(Guid uid)
//        {
//            throw new NotImplementedException();
//        }

//        Mail IService<Mail>.Get(int id)
//        {
//            throw new NotImplementedException();
//        }

//        IEnumerable<Mail> IService<Mail>.GetAll(bool @readonly)
//        {
//            throw new NotImplementedException();
//        }

//        IEnumerable<Mail> IService<Mail>.GetAllSimple()
//        {
//            throw new NotImplementedException();
//        }
//    }
//}