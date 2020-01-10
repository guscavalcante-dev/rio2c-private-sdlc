//using PlataformaRio2C.Domain.Entities.Specifications;
//using PlataformaRio2C.Domain.Validation;
//using PlataformaRio2C.Infra.CrossCutting.Resources;

//namespace PlataformaRio2C.Domain.Entities.Validations
//{
//    public class ConferenceIsConsistent : Validation<Conference>
//    {
//        public ConferenceIsConsistent()
//        {            
//            base.AddRule(new ValidationRule<Conference>(new ConferenceDateIsRequired(), "Data é obrigatória!"));
//            base.AddRule(new ValidationRule<Conference>(new ConferenceStartTimeIsRequired(), "Hora de início é obrigatória!"));
//            base.AddRule(new ValidationRule<Conference>(new ConferenceEndTimeIsRequired(), "Hora de término é obrigatória!"));
//            base.AddRule(new ValidationRule<Conference>(new ConferenceRoomIsRequired(), "Sala é obrigatória!"));
//            base.AddRule(new ValidationRule<Conference>(new ConferenceMustHaveOneLecturer(), "Pelo menos 1 palestrante é obrigatório!"));
//            base.AddRule(new ValidationRule<Conference>(new ConferenceMustHaveOneTitle(), "Pelo menos 1 título é obrigatório!"));
//            base.AddRule(new ValidationRule<Conference>(new ConferenceMustHaveOneSynopsis(), "Pelo menos 1 sinopse é obrigatório!"));
//            base.AddRule(new ValidationRule<Conference>(new ConferenceTitleEnIsRequired(), "Título em inglês obrigatório!"));
//            base.AddRule(new ValidationRule<Conference>(new ConferenceTitlePtBrIsRequired(), "Título em português obrigatório!"));
//            base.AddRule(new ValidationRule<Conference>(new ConferenceSynopsisEnIsRequired(), "Ementa em inglês obrigatório!"));
//            base.AddRule(new ValidationRule<Conference>(new ConferenceSynopsisPtBrIsRequired(), "Ementa em português obrigatório!"));

//            base.AddRule(new ValidationRule<Conference>(new ConferenceStartTimeIsValid(), "Hora de início é invalida!"));
//            base.AddRule(new ValidationRule<Conference>(new ConferenceEndTimeIsValid(), "Hora de término é invalida!"));

//            base.AddRule(new ValidationRule<Conference>(new ConferenceStartTimeSmallerEndTime(), "Hora de início deve ser menor que a hora de término!"));
            

//        }
//    }
//}
