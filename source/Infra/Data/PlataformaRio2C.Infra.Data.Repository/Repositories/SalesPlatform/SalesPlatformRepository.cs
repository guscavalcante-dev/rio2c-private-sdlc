// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Repository
// Author           : Rafael Dantas Ruiz
// Created          : 07-12-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-06-2019
// ***********************************************************************
// <copyright file="SalesPlatformRepository.cs" company="Softo">
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
    /// <summary>SalesPlatformRepository</summary>
    public class SalesPlatformRepository : Repository<PlataformaRio2CContext, SalesPlatform>, ISalesPlatformRepository
    {
        /// <summary>Initializes a new instance of the <see cref="SalesPlatformRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public SalesPlatformRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }

        /// <summary>Método que traz todos os registros</summary>
        /// <param name="readonly"></param>
        /// <returns></returns>
        public override IQueryable<SalesPlatform> GetAll(bool @readonly = false)
        {
            var consult = this.dbSet;

            return @readonly
                        ? consult.AsNoTracking()
                        : consult;
        }

        /// <summary>Gets the by name asynchronous.</summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public async Task<SalesPlatform> GetByNameAsync(string name)
        {
            return await this.GetAll()
                                   .FirstOrDefaultAsync(m => m.Name == name);
        }
    }
}