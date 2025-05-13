namespace PlataformaRio2C.Domain.Entities
{
    public class QuizAnswer : Entity
    {
        public int UserId { get; set; }
        public int OptionId { get; set; }
        public string Value { get; set; }

        public virtual User User { get; set; }
        public virtual QuizOption Option { get; set; }

        //protected QuizAnswer() { }

        public QuizAnswer(QuizAnswer entity)
        {
            UserId = entity.UserId;
            OptionId = entity.OptionId;
            Value = entity.Value;
        }

        public QuizAnswer()
        {
            //return new QuizAnswer();
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
