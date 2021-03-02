// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Repository
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 07-22-2019
// ***********************************************************************
// <copyright file="Repository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PlataformaRio2C.Infra.Data.Repository
{
    /// <summary>Repository</summary>
    /// <typeparam name="TContext">The type of the context.</typeparam>
    /// <typeparam name="T"></typeparam>
    public abstract class Repository<TContext, T> : IRepository<T>
         where TContext : DbContext
         where T : class, IEntity
    {
        //Utilizo o GetAll como partida para praticamente todos os métodos do repositório, e portanto ao realizar override do mesmo a alterção valerá para todos os metodos.

        protected TContext _context;
        protected DbSet<T> dbSet;

        /// <summary>Initializes a new instance of the <see cref="Repository{TContext, T}"/> class.</summary>
        /// <param name="context">The context.</param>
        public Repository(TContext context)
        {
            this._context = context;
            this.dbSet = context.Set<T>();
        }

        /// <summary>Gets the specified identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public virtual T Get(object id)
        {
            return this.dbSet.Find(id);
        }

        /// <summary>Gets the specified uid.</summary>
        /// <param name="uid">The uid.</param>
        /// <returns></returns>
        public virtual T Get(Guid uid)
        {
            return this.dbSet.FirstOrDefault(e => e.Uid == uid);
        }

        /// <summary>Gets the asynchronous.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public virtual async Task<T> GetAsync(object id)
        {
            return await this.dbSet.FindAsync(id);
        }

        /// <summary>Gets the asynchronous.</summary>
        /// <param name="uid">The uid.</param>
        /// <returns></returns>
        public virtual async Task<T> GetAsync(Guid uid)
        {
            return await this.dbSet.FirstOrDefaultAsync(e => e.Uid == uid);
        }

        /// <summary>
        /// Método que traz o primeiro registro a satisfazer a condição de filtro
        /// </summary>
        /// <param name="filter">Expressão de filtro</param>
        /// <example>Get(e => e.Name == "João")</example>
        /// <returns></returns>
        public virtual T Get(Expression<Func<T, bool>> filter)
        {
            return this.GetAll().FirstOrDefault(filter);
        }

        /// <summary>
        /// Método que traz o primeiro registro a satisfazer a condição de filtro, assincronamente!
        /// </summary>
        /// <param name="filter">Expressão de filtro</param>
        /// <example>await GetAsync(e => e.Name == "João")</example>
        /// <returns></returns>
        public virtual async Task<T> GetAsync(Expression<Func<T, bool>> filter)
        {
            return await this.GetAll().FirstOrDefaultAsync(filter);
        }

        /// <summary>
        /// Método que traz todos os registros
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<T> GetAll(bool @readonly = false)
        {
            return @readonly
               ? this.dbSet.AsNoTracking()
               : this.dbSet;
        }

        /// <summary>
        /// Gets all simple.
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<T> GetAllSimple()
        {
            return
                this.dbSet;
              
        }

        /// <summary>
        /// Gets all simple.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public virtual IQueryable<T> GetAllSimple(Expression<Func<T, bool>> filter)
        {
            return
                this.dbSet.Where(filter);

        }

        /// <summary>
        /// Metodo que traz todos os registros que satisfaçam a condição do filtro
        /// </summary>
        /// <param name="filter">Expressão de filtro</param>
        /// <example>GetAll(e => e.Name.Contains("João"))</example>
        /// <returns></returns>
        public virtual IQueryable<T> GetAll(Expression<Func<T, bool>> filter)
        {
            return this.GetAll().Where(filter);
        }

        /// <summary>
        /// Metodo que traz todos os registros que satisfaçam a condição do filtro.
        /// Ordenados pela expressão de ordenação.
        /// </summary>
        /// <param name="filter">Expressão de filtro</param>
        /// <param name="orderBy">Expressão de ordenação</param>
        /// <example>GetAll(e => e.Name.Contains("João"), o => o.OrderBy(e => e.Name))</example>
        /// <returns></returns>
        public virtual IQueryable<T> GetAll(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy)
        {
            var query = this.GetAll().Where(filter);
            return orderBy(query);
        }

        /// <summary>
        /// Metodo que traz todos os registros que satisfaçam a condição do filtro e pela paginação.
        /// Ordenados pela expressão de ordenação.
        /// </summary>
        /// <param name="filter">Expressão de filtro</param>
        /// <param name="orderBy">Expressão de ordenação</param>
        /// <param name="page">Número da página</param>
        /// <param name="pagesize">Números de registros por página</param>
        /// <example>GetAll(e => e.Name.Contains("João"), o => o.OrderBy(e => e.Name), 2, 10)</example>
        /// <returns></returns>
        public virtual IQueryable<T> GetAll(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, int page, int pagesize)
        {
            var query = this.GetAll().Where(filter);
            if (page > 0 && pagesize > 0)
            {
                query = query.Skip((page - 1) * pagesize).Take(pagesize);

            }
            return orderBy(query);
        }

        /// <summary>
        /// Metodo que traz todos os registros que satisfaçam a condição do filtro e pela paginação.
        /// </summary>
        /// <param name="filter">Expressão de filtro</param>
        /// <param name="page">Número da página</param>
        /// <param name="pagesize">Números de registros por página</param>
        /// <example>GetAll(e => e.Name.Contains("João"), 2, 10)</example>
        /// <returns></returns>
        public virtual IQueryable<T> GetAll(Expression<Func<T, bool>> filter, int page, int pagesize)
        {
            var query = this.GetAll().Where(filter);
            if (page > 0 && pagesize > 0)
            {
                query = query.Skip((page - 1) * pagesize).Take(pagesize);

            }
            return query;
        }

        /// <summary>
        /// Método que retorna uma página somente do set de entitades
        /// </summary>
        /// <param name="page">A página</param>
        /// <param name="pagesize">Quantidade máxima de itens na página</param>
        /// <returns></returns>
        public virtual IQueryable<T> GetAll(int page, int pagesize)
        {
            var query = this.GetAll();
            if (page > 0 && pagesize > 0)
            {
                query = query.Skip((page - 1) * pagesize).Take(pagesize);

            }
            return query;
        }

        /// <summary>
        /// Método que atualiza o estado da entidade no Contexto
        /// </summary>
        /// <param name="entity">Entidade</param>
        public virtual void Update(T entity)
        {
            this._context.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// Método que atualiza o estado das entidades no Contexto
        /// </summary>
        /// <param name="entities">Entidades</param>
        public virtual void UpdateAll(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                this.Update(entity);
            }
        }

        /// <summary>
        /// Método que adiciona a entidade ao Contexto
        /// </summary>
        /// <param name="entity">Entidade</param>
        public virtual void Create(T entity)
        {
            this.dbSet.Add(entity);
        }

        /// <summary>
        /// Método que adiciona as entidades ao Contexto
        /// </summary>
        /// <param name="entities">Entidades</param>
        public virtual void CreateAll(IEnumerable<T> entities)
        {
            this.dbSet.AddRange(entities);
        }

        /// <summary>
        /// Método que remove a entidade do Contexto
        /// </summary>
        /// <param name="entity">Entidade</param>
        public virtual void Delete(T entity)
        {
            this.dbSet.Remove(entity);
        }

        /// <summary>
        /// Método que remove as entidades do Contexto
        /// </summary>
        /// <param name="entities">Entidades</param>
        public virtual void DeleteAll(IEnumerable<T> entities)
        {
            this.dbSet.RemoveRange(entities);
        }

        /// <summary>
        /// Método que traz a quantidade da entidade no Contexto,
        /// Pode usar critério de filtro.
        /// </summary>
        /// <param name="filter">Expressão de filtro</param>
        /// <example>Count() ou Count(e => e.Nome.Contains("João"))</example>
        /// <returns></returns>
        public virtual int Count(Expression<Func<T, bool>> filter = null)
        {
            if (filter == null)
            {
                return this.GetAll().Count();
            }
            return this.GetAll().Count(filter);
        }

        /// <summary>
        /// Método que traz a quantidade da entidade no Contexto,
        /// Pode usar critério de filtro. 
        /// Assíncrono!!
        /// </summary>
        /// <param name="filter">Expressão de filtro</param>
        /// <example>await CountAsync() ou await CountAsync(e => e.Nome.Contains("João"))</example>
        /// <returns></returns>
        public virtual async Task<int> CountAsync(Expression<Func<T, bool>> filter = null)
        {
            if (filter == null)
            {
                return await this.GetAll().CountAsync();
            }
            return await this.GetAll().CountAsync(filter);
        }

        /// <summary>
        /// Método que verifica a existencia de registro(s) baseado na expressão de filtro
        /// </summary>
        /// <param name="filter">Filtro a ser aplicado</param>
        /// <returns>true or false</returns>
        public virtual bool EntityExists(Expression<Func<T, bool>> filter)
        {
            var result = this.GetAll(filter);
            return result != null && result.Any();
        }
    }
}
