//using PlataformaRio2C.Domain.Enums;
//using PlataformaRio2C.Domain.Interfaces;
//using System.Linq;

//namespace PlataformaRio2C.Domain.Entities.Specifications
//{
//    public class CollaboratorMiniBioEnIsRequired : ISpecification<Collaborator>
//    {
//        public string Target { get { return "MiniBios"; } }
//        public ErrorCodes Code { get { return ErrorCodes.IsInvalid; } }

//        public bool IsSatisfiedBy(Collaborator entity)
//        {           
//            return entity.JobTitles != null && entity.MiniBios.Any( e => e.LanguageCode == LanguageCodes.En.ToString() && !string.IsNullOrWhiteSpace(e.Value));
//        }
//    }
//}
