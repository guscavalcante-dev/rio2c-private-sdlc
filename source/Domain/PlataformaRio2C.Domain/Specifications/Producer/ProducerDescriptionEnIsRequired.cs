//using PlataformaRio2C.Domain.Enums;
//using PlataformaRio2C.Domain.Interfaces;
//using System.Linq;

//namespace PlataformaRio2C.Domain.Entities.Specifications
//{
//    public class ProducerDescriptionEnIsRequired : ISpecification<Producer>
//    {
//        public string Target { get { return "Descriptions"; } }
//        public ErrorCodes Code { get { return ErrorCodes.IsRequired; } }

//        public bool IsSatisfiedBy(Producer entity)
//        {           
//            return entity.Descriptions != null && entity.Descriptions.Any( e => e.LanguageCode == LanguageCodes.En.ToString() && !string.IsNullOrWhiteSpace(e.Value));
//        }
//    }
//}
