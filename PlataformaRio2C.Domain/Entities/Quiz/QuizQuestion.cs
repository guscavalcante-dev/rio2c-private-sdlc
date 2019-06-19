using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaRio2C.Domain.Entities
{
    public class QuizQuestion : Entity
    {
        public int QuizId { get; private set; }
        public string Question { get; private set; }

        public virtual Quiz Quiz { get; private set; }
        public virtual ICollection<QuizOption> Option { get; set; }

        protected QuizQuestion() { }

        public QuizQuestion(int QuizId, string question)
        {
            this.QuizId = QuizId;
            this.Question = question;
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
