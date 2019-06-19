using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    public class LecturerRepository : Repository<PlataformaRio2CContext, Lecturer>, ILecturerRepository
    {
        public LecturerRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }

        public override void Delete(Lecturer entity)
        {
            if (entity.Image != null)
            {
                _context.Entry(entity.Image).State = EntityState.Deleted;
            }

            if (entity.JobTitles != null && entity.JobTitles.Any())
            {
                foreach (var item in entity.JobTitles.ToList())
                {
                    _context.Entry(item).State = EntityState.Deleted;
                }

                entity.JobTitles.Clear();
            }

            base.Delete(entity);
        }
    }
}
