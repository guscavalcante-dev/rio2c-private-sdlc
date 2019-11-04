//using PlataformaRio2C.Domain.Entities.Specifications;
//using PlataformaRio2C.Domain.Validation;
//using PlataformaRio2C.Infra.CrossCutting.Resources;

//namespace PlataformaRio2C.Domain.Entities.Validations
//{
//    public class ProjectPlayerEvaluationRecuseIsConsistent : Validation<ProjectPlayerEvaluation>
//    {
//        public ProjectPlayerEvaluationRecuseIsConsistent()
//        {
//            //base.AddRule(new ValidationRule<ProjectPlayerEvaluation>(new RecuseReasonIsRequired(), Messages.ReasonIsRequired));
//            base.AddRule(new ValidationRule<ProjectPlayerEvaluation>(new RecuseReasonMinLength(), string.Format(Messages.ReasonMinLength, ProjectPlayerEvaluation.ReasonMinLength)));
//            base.AddRule(new ValidationRule<ProjectPlayerEvaluation>(new RecuseReasonMaxLength(), string.Format(Messages.ReasonMaxLength, ProjectPlayerEvaluation.ReasonMaxLength)));
//        }
//    }
//}
