using PlataformaRio2C.Application.Common;
using PlataformaRio2C.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaRio2C.Application.ViewModels
{
    public class AnswerItemListAppViewModel : EntityViewModel<AnswerItemListAppViewModel, QuizAnswer>
    {
        public AnswerItemListAppViewModel() { }

        public AnswerItemListAppViewModel(QuizAnswer entity)
            : base(entity) { }
    }
}
