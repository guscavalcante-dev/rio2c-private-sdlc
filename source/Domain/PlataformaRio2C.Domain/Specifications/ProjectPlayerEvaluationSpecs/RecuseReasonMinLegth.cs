//using PlataformaRio2C.Domain.Enums;
//using PlataformaRio2C.Domain.Interfaces;

//namespace PlataformaRio2C.Domain.Entities.Specifications
//{
//    public class RecuseReasonMinLength : ISpecification<ProjectPlayerEvaluation>
//    {
//        public string Target { get { return "Reason"; } }
//        public ErrorCodes Code { get { return ErrorCodes.IsRequired; } }

//        public bool IsSatisfiedBy(ProjectPlayerEvaluation entity)
//        {
//            if (entity == null) return false;

//            //return !string.IsNullOrWhiteSpace(entity.Reason) && entity.Reason.Length >= ProjectPlayerEvaluation.ReasonMinLength;
//            return string.IsNullOrWhiteSpace(entity.Reason) || entity.Reason.Length >= ProjectPlayerEvaluation.ReasonMinLength;
//        }
//    }
//}
