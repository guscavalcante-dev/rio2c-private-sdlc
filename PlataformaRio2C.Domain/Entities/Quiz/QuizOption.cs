using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaRio2C.Domain.Entities
{
    public class QuizOption : Entity
    {
        public int QuestionId { get; private set; }
        public bool Text { get; private set; }
        public string Value { get; private set; }

        public virtual QuizQuestion Question { get; set; }
        public virtual QuizAnswer Answer { get; set; }

        protected QuizOption() { }

        public QuizOption(int questionId, string value, bool text)
        {
            this.QuestionId = questionId;
            this.Value = value;
            this.Text = text;
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
