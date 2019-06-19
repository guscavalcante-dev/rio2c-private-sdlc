using PlataformaRio2C.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Validation;

namespace PlataformaRio2C.Domain.Services
{
    public class MailService : Service<Mail>, IMailService
    {
        private readonly IMailRepository _repository;
        private readonly IMailCollaboratorRepository _mailCollaboratorRepository;
        private readonly IRepositoryFactory _repositoryFactory;


        public MailService(IMailRepository repository, IRepositoryFactory repositoryFactory)
            : base(repository)
        {
            _repository = repository;
            _mailCollaboratorRepository = repositoryFactory.MailCollaboratorRepository;
            _repositoryFactory = repositoryFactory;
        }

        public IEnumerable<Mail> GetAll()
        {
            return _repository.GetAll();
        }

        Mail Get(int mailId)
        {
            return _repository.Get( a => a.Id == mailId);
        }

        Mail Get (string subject)
        {
            return _repository.GetAll(a => a.Subject == subject).OrderByDescending(a => a.Id).FirstOrDefault();
        }

        ValidationResult IMailService.CreateMailCollaborator(MailCollaborator model)
        {
            var MCService = new MailCollaboratorService(_mailCollaboratorRepository,_repositoryFactory);
            return MCService.CreateMailCollaborator(model);
        }

        //ValidationResult IMailService.CreateMailCollaborator(MailCollaborator model)
        //{
        //    throw new NotImplementedException();
        //}
    }
}