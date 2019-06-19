using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Validation;
using System.Linq;

namespace PlataformaRio2C.Domain.Services
{
    public class HoldingService : Service<Holding>, IHoldingService
    {
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly IImageFileRepository _imageFileRepository;
        private readonly ILanguageRepository _languageRepository;
        private readonly IHoldingDescriptionRepository _holdingDescriptionRepository;
        private readonly IPlayerRepository _playerRepository;

        public HoldingService(IHoldingRepository repository, IRepositoryFactory repositoryFactory)
            : base(repository)
        {
            _repositoryFactory = repositoryFactory;
            _imageFileRepository = _repositoryFactory.ImageFileRepository;
            _languageRepository = _repositoryFactory.LanguageRepository;
            _holdingDescriptionRepository = _repositoryFactory.HoldingDescriptionRepository;
            _playerRepository = _repositoryFactory.PlayerRepository;
        }

        public override ValidationResult Create(Holding holding)
        {
            if (!string.IsNullOrWhiteSpace(holding.Name))
            {
                var existHoldingByName = _repository.Get(e => e.Name == holding.Name);
                if (existHoldingByName != null)
                {
                    var error = new ValidationError(string.Format("Já existe um holding com o nome '{0}'.", holding.Name), new string[] { "Name" });
                    _validationResult.Add(error);
                }
            }

            if (holding.Descriptions != null && holding.Descriptions.Any())
            {
                foreach (var description in holding.Descriptions)
                {
                    var language = _languageRepository.Get(e => e.Code == description.LanguageCode);
                    description.SetLanguage(language);
                    description.SetHolding(holding);
                    _holdingDescriptionRepository.Create(description);
                }
            }

            return base.Create(holding);
        }

        public override ValidationResult Update(Holding holding)
        {
            if (!string.IsNullOrWhiteSpace(holding.Name))
            {
                var existHoldingByName = _repository.Get(e => e.Name == holding.Name && e.Id != holding.Id);
                if (existHoldingByName != null)
                {
                    var error = new ValidationError(string.Format("Já existe um holding com o nome '{0}'.", holding.Name), new string[] { "Name" });
                    _validationResult.Add(error);
                }
            }

            var oldsDescriptions = _holdingDescriptionRepository.GetAll(e => e.HoldingId == holding.Id);
            if (oldsDescriptions != null && oldsDescriptions.Any())
            {
                _holdingDescriptionRepository.DeleteAll(oldsDescriptions);
            }

            if (holding.Descriptions != null && holding.Descriptions.Any())
            {
                foreach (var description in holding.Descriptions)
                {
                    var language = _languageRepository.GetAll(e => e.Code == description.LanguageCode).FirstOrDefault();
                    description.SetLanguage(language);
                    description.SetHolding(holding);

                    _holdingDescriptionRepository.Create(description);
                }
            }

            return base.Update(holding);
        }

        public override ValidationResult Delete(Holding entity)
        {
            var imageFile = _imageFileRepository.Get(entity.ImageId);
            if (imageFile != null)
            {
                _imageFileRepository.Delete(imageFile);
            }

            if (entity.Descriptions != null && entity.Descriptions.Any())
            {
                _holdingDescriptionRepository.DeleteAll(entity.Descriptions);
            }

            var countPlayersAssociated = _playerRepository.Count(e => e.HoldingId == entity.Id);

            if (countPlayersAssociated > 0)
            {
                var error = new ValidationError(string.Format("Existem '{0}' player(s) associado(s) ao holding '{1}'.", countPlayersAssociated, entity.Name), new string[] { "" });
                _validationResult.Add(error);
            }

            return base.Delete(entity);
        }
    }
}
