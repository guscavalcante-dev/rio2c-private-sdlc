//using PlataformaRio2C.Domain.Entities;
//using PlataformaRio2C.Domain.Entities.Validations;
//using PlataformaRio2C.Domain.Interfaces;
//using PlataformaRio2C.Domain.Validation;

//namespace PlataformaRio2C.Domain.Services
//{
//    public class PlayerService : PlayerAdminService
//    {
//        public PlayerService(IPlayerRepository repository, IRepositoryFactory repositoryFactory)
//            : base(repository, repositoryFactory)
//        {
//        }

//        public override ValidationResult Update(Player entity)
//        {
//            _validationResult.Add(new PlayerIsConsistentForPortal().Valid(entity));

//            _validationResult.Add(new ImageIsConsistent().Valid(entity.Image));

//            //_validationResult.Add(new AddressIsConsistent().Valid(entity.Address));            

//            _validationResult.Add(new PhoneNumberIsConsistent("PhoneNumber").Valid(entity.PhoneNumber));

//            return base.Update(entity);
//        }
//    }
//}
