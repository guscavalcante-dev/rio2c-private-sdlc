// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 07-22-2019
// ***********************************************************************
// <copyright file="IRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PlataformaRio2C.Domain.Interfaces
{
    /// <summary>IRepository</summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> 
        where T : class, IEntity
    {
        int Count(Expression<Func<T, bool>> filter = null);
        void Create(T entity);
        void CreateAll(IEnumerable<T> entities);
        void Delete(T entity);
        void DeleteAll(IEnumerable<T> entities);
        T Get(object id);
        T Get(Guid uid);
        T Get(Expression<Func<T, bool>> filter);
        System.Linq.IQueryable<T> GetAll(bool @readonly = false);
        System.Linq.IQueryable<T> GetAllSimple();
        System.Linq.IQueryable<T> GetAllSimple(Expression<Func<T, bool>> filter);
        System.Linq.IQueryable<T> GetAll(Expression<Func<T, bool>> filter);
        System.Linq.IQueryable<T> GetAll(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy);
        System.Linq.IQueryable<T> GetAll(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, int page, int pagesize);
        System.Linq.IQueryable<T> GetAll(Expression<Func<T, bool>> filter, int page, int pagesize);
        System.Linq.IQueryable<T> GetAll(int page, int pagesize);
        void Update(T entity);
        void UpdateAll(IEnumerable<T> entities);
        bool EntityExists(Expression<Func<T, bool>> filter);

        #region Async methods

        Task<T> GetAsync(object id);
        Task<T> GetAsync(Guid uid);
        Task<T> GetAsync(Expression<Func<T, bool>> filter);
        Task<int> CountAsync(Expression<Func<T, bool>> filter = null);

        #endregion
    }
}
