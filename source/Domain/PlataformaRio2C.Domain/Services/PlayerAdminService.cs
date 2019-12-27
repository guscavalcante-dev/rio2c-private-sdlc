//using PlataformaRio2C.Domain.Entities;
//using PlataformaRio2C.Domain.Interfaces;
//using PlataformaRio2C.Domain.Validation;
//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace PlataformaRio2C.Domain.Services
//{
//    public class PlayerAdminService : Service<Player>, IPlayerService
//    {
//        private readonly IRepositoryFactory _repositoryFactory;

//        protected readonly IHoldingRepository _holdingRepository;
//        protected readonly ILanguageRepository _languageRepository;
//        protected readonly IImageFileRepository _imageFileRepository;
//        protected readonly IPlayerDescriptionRepository _playerDescriptionRepository;

//        protected readonly ICollaboratorRepository _collaboratorRepository;

//        public PlayerAdminService(IPlayerRepository repository, IRepositoryFactory repositoryFactory)
//            :base(repository)
//        {
//            _repositoryFactory = repositoryFactory;
//            _holdingRepository = repositoryFactory.HoldingRepository;
//            _languageRepository = repositoryFactory.LanguageRepository;
//            _imageFileRepository = repositoryFactory.ImageFileRepository;
//            _playerDescriptionRepository = repositoryFactory.PlayerDescriptionRepository;
//            _collaboratorRepository = repositoryFactory.CollaboratorRepository;
//        }
       

//        public override ValidationResult Create(Player entity)
//        {
//            if (!string.IsNullOrWhiteSpace(entity.Name))
//            {
//                var entityExistByName = _repository.Get(e => e.Name == entity.Name);
//                if (entityExistByName != null)
//                {
//                    var error = new ValidationError(string.Format("Já existe um player com o nome '{0}'.", entity.Name), new string[] { "Name" });
//                    _validationResult.Add(error);
//                }
//            }

//            if (entity.HoldingUid != Guid.Empty)
//            {
//                var holding = _holdingRepository.GetAll(e => e.Uid == entity.HoldingUid).FirstOrDefault();

//                entity.SetHolding(holding);
//            }

//            if (entity.Descriptions != null && entity.Descriptions.Any())
//            {
//                foreach (var description in entity.Descriptions)
//                {
//                    var language = _languageRepository.Get(e => e.Code == description.LanguageCode);
//                    description.SetLanguage(language);
//                    description.SetPlayer(entity);
//                    _playerDescriptionRepository.Create(description);
//                }
//            }

//            return base.Create(entity);
//        }

//        public override ValidationResult Update(Player entity)
//        {
//            if (!string.IsNullOrWhiteSpace(entity.Name))
//            {
//                var entityExistByName = _repository.Get(e => e.Name == entity.Name && e.Id != entity.Id);
//                if (entityExistByName != null)
//                {
//                    var error = new ValidationError(string.Format("Já existe um holding com o nome '{0}'.", entity.Name), new string[] { "Name" });
//                    _validationResult.Add(error);
//                }
//            }

//            var oldsDescriptions = _playerDescriptionRepository.GetAll(e => e.PlayerId == entity.Id);
//            if (oldsDescriptions != null && oldsDescriptions.Any())
//            {
//                _playerDescriptionRepository.DeleteAll(oldsDescriptions);
//            }

//            if (entity.HoldingUid != Guid.Empty)
//            {
//                var holding = _holdingRepository.GetAll(e => e.Uid == entity.HoldingUid).FirstOrDefault();

//                entity.SetHolding(holding);
//            }

//            if (entity.Descriptions != null && entity.Descriptions.Any())
//            {
//                foreach (var description in entity.Descriptions)
//                {
//                    var language = _languageRepository.GetAll(e => e.Code == description.LanguageCode).FirstOrDefault();
//                    description.SetLanguage(language);
//                    description.SetPlayer(entity);

//                    _playerDescriptionRepository.Create(description);
//                }
//            }

//            return base.Update(entity);
//        }


//        public override ValidationResult Delete(Player entity)
//        {                 

//            //var countCollaboratorAssociated = _collaboratorRepository.Count(e => e.PlayerId == entity.Id);

//            //if (countCollaboratorAssociated > 0)
//            //{
//            //    var error = new ValidationError(string.Format("Existem '{0}' colaborador(es) associado(s) ao player '{1}'.", countCollaboratorAssociated, entity.Name), new string[] { "" });
//            //    _validationResult.Add(error);
//            //}

//            return base.Delete(entity);
//        }

//        public Player GetWithInterests(Guid uid)
//        {
//            var r = _repository as IPlayerRepository;
//            return r.GetAllWithInterests(uid);
//        }
//    }
//}
