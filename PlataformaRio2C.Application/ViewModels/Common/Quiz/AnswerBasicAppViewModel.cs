using PlataformaRio2C.Application.Common;
using PlataformaRio2C.Domain.Entities;
using System;
using System.Collections.Generic;

namespace PlataformaRio2C.Application.ViewModels
{
    public class AnswerBasicAppViewModel : EntityViewModel<AnswerBasicAppViewModel, QuizAnswer>, IEntityViewModel<QuizAnswer>
    {
        public int userId { get; set; }
        public int OptionSelected { get; set; }
        public string Value { get; set; }

        public List<QuizAnswer> Answer { get; set; }

        public AnswerBasicAppViewModel()
            : base()
        {

        }

        public AnswerBasicAppViewModel(QuizAnswer entity)
            : base(entity)
        {

        }

        public IEnumerable<QuizAnswer> MapReverse(int userid)
        {
            List<QuizAnswer> response = new List<QuizAnswer>();

            foreach(QuizAnswer objAnswer in Answer)
            {
                QuizAnswer oneAnswer = new QuizAnswer();

                oneAnswer.OptionId = objAnswer.OptionId;
                oneAnswer.Value = objAnswer.Value;
                oneAnswer.UserId = userid;
                //oneAnswer.Uid = Guid.NewGuid();
                //oneAnswer.CreationDate = DateTime.Now;

                response.Add(oneAnswer);
            }

            return response;
        }

        public QuizAnswer MapReverse()
        {
            throw new NotImplementedException();
        }

        public QuizAnswer MapReverse(QuizAnswer entity)
        {
            throw new NotImplementedException();
        }
    }
}
