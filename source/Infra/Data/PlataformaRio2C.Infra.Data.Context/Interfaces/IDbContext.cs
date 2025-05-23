﻿using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace PlataformaRio2C.Infra.Data.Context.Interfaces
{
    public interface IDbContext
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        int SaveChanges();
        void Dispose();
    }
}
