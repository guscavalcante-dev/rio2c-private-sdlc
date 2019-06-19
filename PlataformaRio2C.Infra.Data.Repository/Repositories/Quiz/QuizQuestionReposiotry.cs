using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context;

namespace PlataformaRio2C.Infra.Data.Repository
{
    public class QuizQuestionRepository : Repository<PlataformaRio2CContext, QuizQuestion>, IQuizQuestionRepository
    {
        private PlataformaRio2CContext _context;

        public QuizQuestionRepository(PlataformaRio2CContext context)
            : base(context)
        {
            _context = context;
        }
    }
}
