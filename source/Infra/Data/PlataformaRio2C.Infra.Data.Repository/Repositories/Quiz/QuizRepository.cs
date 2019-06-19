using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaRio2C.Infra.Data.Repository
{
    public class QuizRepository : Repository<PlataformaRio2CContext, Quiz>, IQuizRepository
    {
        private PlataformaRio2CContext _context;

        public QuizRepository(PlataformaRio2CContext context)
            : base(context)
        {
            _context = context;
        }


    }
}
