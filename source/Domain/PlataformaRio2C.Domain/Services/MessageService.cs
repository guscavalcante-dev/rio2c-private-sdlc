//using System;
//using System.Collections.Generic;
//using PlataformaRio2C.Domain.Entities;
//using PlataformaRio2C.Domain.Interfaces;
//using System.Linq.Expressions;
//using System.Linq;
//using PlataformaRio2C.Domain.Validation;

//namespace PlataformaRio2C.Domain.Services
//{
//    public class MessageService : Service<Message>, IMessageService
//    {
//        private readonly ICollaboratorRepository _collaboratorRepository;
//        private readonly IUserRepository _userRepository;

//        public MessageService(IMessageRepository repository, IRepositoryFactory repositoryFactory)
//            :base(repository)
//        {
//            _collaboratorRepository = repositoryFactory.CollaboratorRepository;
//            _userRepository = repositoryFactory.UserRepository;
//        }

//        public IEnumerable<Message> GetAll(int userId)
//        {
//            return _repository.GetAll(e => (e.SenderId == userId || e.RecipientId == userId));
//        }

//        public IEnumerable<Message> GetAllUnread(int userId)
//        {
//            return _repository.GetAll(e => e.RecipientId == userId && !e.ReadDate.HasValue);
//        }

//        public IEnumerable<Message> GetAll(int userId, string email)
//        {            
//            return _repository.GetAll(e => (e.SenderId == userId || e.RecipientId == userId) && (e.Sender.Email == email || e.Recipient.Email == email));            
//        }


//    }
//}
