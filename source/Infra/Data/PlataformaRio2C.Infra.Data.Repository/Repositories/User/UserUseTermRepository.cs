// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Repository
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-06-2019
// ***********************************************************************
// <copyright file="UserUseTermRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Linq;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context;
using System.Data.Entity;
using System;
using System.Linq.Expressions;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    /// <summary>UserUseTermRepository</summary>
    public class UserUseTermRepository : Repository<PlataformaRio2CContext, UserUseTerm>, IUserUseTermRepository
    {
        /// <summary>Initializes a new instance of the <see cref="UserUseTermRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public UserUseTermRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }

        /// <summary>Gets all.</summary>
        /// <returns></returns>
        public IQueryable<UserUseTerm> GetAll()
        {

            return this.dbSet
                        .Include(q => q.Role)
                        .Include(i => i.User)
                        .Include(i => i.Edition)
                        .AsNoTracking();
        }

        /// <summary>Metodo que traz todos os registros que satisfaçam a condição do filtro</summary>
        /// <param name="filter">Expressão de filtro</param>
        /// <returns></returns>
        /// <example>GetAll(e =&gt; e.Name.Contains("João"))</example>
        public override IQueryable<UserUseTerm> GetAll(Expression<Func<UserUseTerm, bool>> filter)
        {
            return this.GetAll().Where(filter);
        }
    }
}