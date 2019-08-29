// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Repository
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-29-2019
// ***********************************************************************
// <copyright file="CollaboratorRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context;
using System.Data.Entity;
using System.Linq;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    /// <summary>UserRepository</summary>
    public class UserRepository : Repository<PlataformaRio2CContext, User>, IUserRepository
    {
        /// <summary>Initializes a new instance of the <see cref="UserRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public UserRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }

        /// <summary>Método que remove a entidade do Contexto</summary>
        /// <param name="entity">Entidade</param>
        public override void Delete(User entity)
        {
            entity.Roles.Clear();

            if (entity.UserUseTerms != null && entity.UserUseTerms.Any())
            {
                var terms = entity.UserUseTerms.ToList();

                foreach (var term in terms)
                {
                    _context.Entry(term).State = EntityState.Deleted;
                }
            }

            base.Delete(entity);
        }
    }
}