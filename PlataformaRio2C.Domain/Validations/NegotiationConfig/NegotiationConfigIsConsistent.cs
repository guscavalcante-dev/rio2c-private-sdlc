using PlataformaRio2C.Domain.Entities.Specifications;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Domain.Entities.Validations
{
    public class NegotiationConfigIsConsistent : Validation<NegotiationConfig>
    {
        public NegotiationConfigIsConsistent()
        {
            base.AddRule(new ValidationRule<NegotiationConfig>(new NegotiationConfigDateIsRequired(), "Datá é obrigatória."));
            base.AddRule(new ValidationRule<NegotiationConfig>(new NegotiationConfigStartTimeIsRequired(), "Hora de início é obrigatória."));
            base.AddRule(new ValidationRule<NegotiationConfig>(new NegotiationConfigStartTimeIsValid(), "Hora de início é inválida."));

            base.AddRule(new ValidationRule<NegotiationConfig>(new NegotiationConfigEndTimeIsRequired(), "Hora fim é obrigatória."));
            base.AddRule(new ValidationRule<NegotiationConfig>(new NegotiationConfigEndTimeIsValid(), "Hora fim é inválida."));

            base.AddRule(new ValidationRule<NegotiationConfig>(new NegotiationConfigRoundsFirstTurnIsInvalid(), "Qtd de rodadas do 1 turno inválido."));
            base.AddRule(new ValidationRule<NegotiationConfig>(new NegotiationConfigRoundsSecondTurnIsInvalid(), "Qtd de rodadas  do 2 turno inválido."));

            base.AddRule(new ValidationRule<NegotiationConfig>(new NegotiationConfigTimeIntervalBetweenTurnIsValid(), "Tempo de intervalo entre os turnos inválido."));
            base.AddRule(new ValidationRule<NegotiationConfig>(new NegotiationConfigTimeOfEachRoundIsValid(), "Tempo de cada rodada inválido."));
            base.AddRule(new ValidationRule<NegotiationConfig>(new NegotiationConfigTimeIntervalBetweenRoundIsValid(), "Tempo de intervalo entre cada rodada inválido."));

            base.AddRule(new ValidationRule<NegotiationConfig>(new NegotiationConfigRoomIsRequired(), "Sala é obrigatória."));

            base.AddRule(new ValidationRule<NegotiationConfig>(new NegotiationConfigStartTimeSmallerEndTime(), "Hora de início deve ser menor que a hora de término!"));
            
            base.AddRule(new ValidationRule<NegotiationConfig>(new NegotiationConfigSpecsMustHaveOneRound(), "A configuração deve conter a quantidade mínima de 1 rodada."));

            base.AddRule(new ValidationRule<NegotiationConfig>(new NegotiationConfigRoomIsUnique(), "Cada sala é permitido somente 1 vez por data de configuração."));
            


        }

    }
}
