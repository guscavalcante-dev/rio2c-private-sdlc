using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context;
using System;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    public class ConferenceLecturerRepository : Repository<PlataformaRio2CContext, ConferenceLecturer>, IConferenceLecturerRepository
    {
        public ConferenceLecturerRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }

        public ConferenceLecturer GetLecturerImage(Guid uid)
        {
            return this.dbSet
                            
                            .Include(i => i.Lecturer)
                            .Include(i => i.Lecturer.Image)
                            .Include(i => i.Collaborator)
                            .Include(i => i.Collaborator.Image)                          
                            .FirstOrDefault(e => e.Uid == uid);
        }

        public override void Delete(ConferenceLecturer entity)
        {
           
            base.Delete(entity);
        }

        public override void DeleteAll(IEnumerable<ConferenceLecturer> entities)
        {
            foreach (var entity in entities)
            {
                if (entity.Lecturer != null)
                {
                    if (entity.Lecturer.Image != null)
                    {
                        _context.Entry(entity.Lecturer.Image).State = EntityState.Deleted;
                    }

                    if (entity.Lecturer.JobTitles != null && entity.Lecturer.JobTitles.Any())
                    {
                        foreach (var item in entity.Lecturer.JobTitles.ToList())
                        {
                            _context.Entry(item).State = EntityState.Deleted;
                        }

                        entity.Lecturer.JobTitles.Clear();
                    }

                    _context.Entry(entity.Lecturer).State = EntityState.Deleted;
                }
            }

            base.DeleteAll(entities);
        }
    }
}
