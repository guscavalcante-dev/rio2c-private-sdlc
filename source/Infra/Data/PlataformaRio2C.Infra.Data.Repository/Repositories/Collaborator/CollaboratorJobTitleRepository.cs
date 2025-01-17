using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;
using System.Data.Entity;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    public class CollaboratorJobTitleRepository : Repository<PlataformaRio2CContext, CollaboratorJobTitle>, ICollaboratorJobTitleRepository
    {
        public CollaboratorJobTitleRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }

        private IQueryable<CollaboratorJobTitle> GetBaseQuery(bool @readonly = false, bool showDeleted = false)
        {
            var consult = this.dbSet.AsQueryable();

            if (!showDeleted)
            {
                consult = consult.IsNotDeleted();
            }

            return @readonly
                    ? consult.AsNoTracking()
                    : consult;
        }

        public async Task<IEnumerable<CollaboratorJobTitleBaseDto>> FindAllJobTitlesDtosByCollaboratorId(int id)
        {
            var query = this.GetBaseQuery().FindByCollaboratorId(id);

            return await query.Select(d => new CollaboratorJobTitleBaseDto
            {
                Id = d.Id,
                Uid = d.Uid,
                Value = d.Value,
                LanguageDto = new LanguageBaseDto
                {
                    Id = d.Language.Id,
                    Uid = d.Language.Uid,
                    Name = d.Language.Name,
                    Code = d.Language.Code
                }
            }).ToListAsync();
        }
    }

    internal static class CollaboratorJobTitleIQueryableExtensions
    {
        internal static IQueryable<CollaboratorJobTitle> IsNotDeleted(this IQueryable<CollaboratorJobTitle> query)
        {
            query = query.Where(c => !c.IsDeleted);
            return query;
        }

        internal static IQueryable<CollaboratorJobTitle> FindByCollaboratorId(this IQueryable<CollaboratorJobTitle> query,int id)
        {
            query = query.Where(c => c.CollaboratorId == id);
            return query;
        }
    }
}
  
