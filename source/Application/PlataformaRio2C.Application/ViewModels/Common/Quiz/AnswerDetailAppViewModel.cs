using PlataformaRio2C.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaRio2C.Application.ViewModels
{
    public class AnswerDetailAppViewModel : AnswerBasicAppViewModel
    {
        public AnswerDetailAppViewModel()
            : base()
        {
        }

        public AnswerDetailAppViewModel(QuizAnswer entity)
            : base(entity)
        {

        }
    }
}
