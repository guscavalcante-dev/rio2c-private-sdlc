//using PlataformaRio2C.Domain.Validation;
//using System;
//using System.Collections.Generic;
//using System.Linq.Expressions;

//namespace PlataformaRio2C.Domain.Interfaces
//{
//    public interface IService<TEntity>
//        where TEntity : class
//    {
//        ValidationResult Create(TEntity entity);
//        ValidationResult CreateAll(IEnumerable<TEntity> entities);

//        IEnumerable<TEntity> GetAll(bool @readonly = false);
//        IEnumerable<TEntity> GetAllSimple();
//        IEnumerable<TEntity> GetAllSimple(Expression<Func<TEntity, bool>> filter);
//        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter);        

//        TEntity Get(Guid uid);        

//        TEntity Get(int id);
//        TEntity Get(Expression<Func<TEntity, bool>> filter);

//        ValidationResult Update(TEntity entity);
//        ValidationResult UpdateAll(IEnumerable<TEntity> entities);

//        ValidationResult Delete(TEntity entity);
//        ValidationResult DeleteAll(IEnumerable<TEntity> entities);
//    }
//}
