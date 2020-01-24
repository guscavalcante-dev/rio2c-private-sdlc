//using System.Collections.Generic;
//using PlataformaRio2C.Domain.Entities;
//using PlataformaRio2C.Domain.Interfaces;
//using PlataformaRio2C.Domain.Validation;
//using System.Linq;

//namespace PlataformaRio2C.Domain.Services
//{
//    public class NegotiationConfigService : Service<NegotiationConfig>, INegotiationConfigService
//    {
//        public NegotiationConfigService(INegotiationConfigRepository repository)
//            :base(repository)
//        {

//        }

//        public override ValidationResult UpdateAll(IEnumerable<NegotiationConfig> entities)
//        {
//            if (entities != null && entities.Any())
//            {
//                var oldEntities = GetAll();
//                if (oldEntities != null && oldEntities.Any())
//                {
//                    DeleteAll(oldEntities);
//                }

//                if (entities.GroupBy(e => e.Date).Any(e => e.Count() > 1))
//                {
//                    var error = new ValidationError("Cada configuração deve representar um dia único.", new string[] { "Date" });
//                    _validationResult.Add(error);
//                }

//                CreateAll(entities);
//            }

//            return _validationResult;
//        }
//    }
//}
