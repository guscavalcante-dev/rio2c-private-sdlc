using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context;

namespace PlataformaRio2C.Infra.Data.Repository
{
    public class QuizAnswerRepository: Repository<PlataformaRio2CContext, QuizAnswer>, IQuizAnswerRepository
    {
        private PlataformaRio2CContext _context;

        public QuizAnswerRepository(PlataformaRio2CContext context)
            : base(context)
        {
            _context = context;
        }
    }
}
