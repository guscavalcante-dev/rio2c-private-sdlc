// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-07-2019
// ***********************************************************************
// <copyright file="EditionRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using System.Linq;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    /// <summary>EditionRepository</summary>
    public class EditionRepository : Repository<PlataformaRio2CContext, Edition>, IEditionRepository
    {
        /// <summary>Initializes a new instance of the <see cref="EditionRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public EditionRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }

        /// <summary>Método que traz todos os registros</summary>
        /// <param name="readonly"></param>
        /// <returns></returns>
        public override IQueryable<Edition> GetAll(bool @readonly = false)
        {
            var consult = this.dbSet;

            return @readonly
                ? consult.AsNoTracking()
                : consult;
        }

        /// <summary>Finds all by is active.</summary>
        /// <param name="isActive">if set to <c>true</c> [is active].</param>
        /// <returns></returns>
        public List<Edition> FindAllByIsActive(bool isActive)
        {
            var query = this.GetAll()
                                .Where(e => e.IsActive);

            return query.ToList();
        }
    }
}