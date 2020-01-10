//using PlataformaRio2C.Domain.Entities;
//using PlataformaRio2C.Domain.Interfaces;
//using PlataformaRio2C.Domain.Validation;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Linq.Expressions;

//namespace PlataformaRio2C.Domain.Services
//{
//    public class CollaboratorService : Service<Collaborator>, ICollaboratorService
//    {
//        #region props
        
//        protected readonly IPlayerRepository _playerRepository;
//        protected readonly IUserRepository _userRepository;
//        protected readonly ILanguageRepository _languageRepository;
//        protected readonly IImageFileRepository _imageFileRepository;
//        protected readonly ICollaboratorJobTitleRepository _collaboratorJobTitleRepository;
//        protected readonly ICollaboratorMiniBioRepository _collaboratorMiniBioRepository;
//        protected readonly ICountryRepository _CountryRepository;
//        protected readonly IUserRoleRepository _userRoleRepository;
//        protected readonly IRoleRepository _roleRepository;
//        protected readonly IAddressRepository _addressRepository;


//        #endregion

//        #region ctor

//        public CollaboratorService(ICollaboratorRepository repository, IRepositoryFactory repositoryFactory)
//            : base(repository)
//        {
//            _playerRepository = repositoryFactory.PlayerRepository;
//            _userRepository = repositoryFactory.UserRepository;
//            _languageRepository = repositoryFactory.LanguageRepository;
//            _imageFileRepository = repositoryFactory.ImageFileRepository;
//            _collaboratorJobTitleRepository = repositoryFactory.CollaboratorJobTitleRepository;
//            _collaboratorMiniBioRepository = repositoryFactory.CollaboratorMiniBioRepository;
//            _CountryRepository = repositoryFactory.CountryRepository;
//            _userRoleRepository = repositoryFactory.UserRoleRepository;
//            _roleRepository = repositoryFactory.RoleRepository;
//            _addressRepository = repositoryFactory.AddressRepository;
//        }

//        #endregion

//        #region Public methods   
//        public Collaborator GetById(int id)
//        {
//            return _repository.Get(e => e.Id == id);
//        }

//        public override ValidationResult Create(Collaborator entity)
//        {
//            if (entity.User != null && !string.IsNullOrWhiteSpace(entity.User.Email))
//            {
//                var entityExistByEmail = _userRepository.Get(e => e.Email == entity.User.Email);
//                if (entityExistByEmail != null)
//                {
//                    var error = new ValidationError(string.Format("Já existe um collaborador com o email '{0}'.", entity.User.Email), new string[] { "Email" });
//                    _validationResult.Add(error);
//                }
//            }

//            return base.Create(entity);
//        }

//        public Address GetAddress(Guid playerUid)
//        {
//            int addressId = (int) _playerRepository.GetAll(a => a.Uid == playerUid).FirstOrDefault().AddressId;
//            //var CollaboratorId = _repository.GetAll(a => a.PlayerId == playerId).FirstOrDefault();
//            var address = _addressRepository.Get(addressId);

//            return _addressRepository.Get(addressId);
//        }
            
//        public override ValidationResult Update(Collaborator entity)
//        {
//            if (entity.User != null && !string.IsNullOrWhiteSpace(entity.User.Email))
//            {
//                var entityExistByEmail = _userRepository.Get(e => e.Email == entity.User.Email && e.Id != entity.User.Id);
//                if (entityExistByEmail != null)
//                {
//                    var error = new ValidationError(string.Format("Já existe um collaborador com o email '{0}'.", entity.User.Email), new string[] { "Email" });
//                    _validationResult.Add(error);
//                }
//            }

//            return base.Update(entity);
//        }


//        public Collaborator GetByUserId(int id)
//        {
//            return _repository.Get(e => e.Id == id);
//        }

//        public Collaborator GetByUserEmail(string email)
//        {
//            return _repository.Get(e => e.User.Email == email);
//        }

//        public Collaborator GetStatusRegisterCollaboratorByUserId(int id)
//        {
//            var r = _repository as ICollaboratorRepository;
//            return r.GetStatusRegisterCollaboratorByUserId(id);
//        }

//        public Collaborator GetWithProducerByUserId(int id)
//        {
//            var r = _repository as ICollaboratorRepository;
//            return r.GetStatusRegisterCollaboratorByUserId(id);
//        }      

//        public Collaborator GetImage(Guid uid)
//        {
//            var r = _repository as ICollaboratorRepository;
//            return r.GetImage(uid);
//        }

//        public Collaborator GetWithPlayerAndProducerUserId(int id)
//        {
//            var r = _repository as ICollaboratorRepository;
//            return r.GetWithPlayerAndProducerUserId(id);
//        }

//        public IEnumerable<Collaborator> GetOptions(Expression<Func<Collaborator, bool>> filter)
//        {            
//            var r = _repository as ICollaboratorRepository;
//            return r.GetOptions(filter);
//        }

//        public IEnumerable<Collaborator> GetOptionsChat(int userId)
//        {
//            var r = _repository as ICollaboratorRepository;
//            return r.GetOptionsChat(userId).OrderBy(e => e.FirstName);
//        }

//        #endregion
//    }
//}
