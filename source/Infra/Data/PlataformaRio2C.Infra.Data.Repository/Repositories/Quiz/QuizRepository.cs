// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Repository
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-06-2019
// ***********************************************************************
// <copyright file="QuizRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    /// <summary>QuizRepository</summary>
    public class QuizRepository : Repository<PlataformaRio2CContext, Quiz>, IQuizRepository
    {
        private PlataformaRio2CContext _context;

        public QuizRepository(PlataformaRio2CContext context)
            : base(context)
        {
            _context = context;
        }

        /// <summary>Método que traz todos os registros</summary>
        /// <param name="readonly"></param>
        /// <returns></returns>
        public override IQueryable<Quiz> GetAll(bool @readonly = false)
        {
            var consult = this.dbSet;

            return @readonly
                        ? consult.AsNoTracking()
                        : consult;
        }

        /// <summary>Finds the active asynchronous.</summary>
        /// <returns></returns>
        public async Task<Quiz> FindActiveAsync()
        {
            var query = this.GetAll()
                                .Where(q => q.IsActive);

            return await query.FirstOrDefaultAsync();
        }
    }
}
