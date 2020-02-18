//using PlataformaRio2C.Domain.Interfaces;
//using PlataformaRio2C.Domain.Validation;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Linq.Expressions;

//namespace PlataformaRio2C.Domain.Services
//{
//    public class Service<TEntity> : IService<TEntity>
//         where TEntity : class, IEntity
//    {
//        protected readonly IRepository<TEntity> _repository;
//        protected readonly ValidationResult _validationResult;

//        public Service(IRepository<TEntity> repository)
//        {
//            _repository = repository;
//            _validationResult = new ValidationResult();
//        }

//        protected IRepository<TEntity> Repository
//        {
//            get { return _repository; }
//        }

//        protected ValidationResult ValidationResult
//        {
//            get { return _validationResult; }
//        }

//        public virtual ValidationResult Create(TEntity entity)
//        {
//            var ValidationEntity = entity as IEntity;
//            if (ValidationEntity != null && !ValidationEntity.IsValid())
//                ValidationResult.Add(ValidationEntity.ValidationResult);

//            if (!ValidationResult.IsValid)
//                return ValidationResult;

//            _repository.Create(entity);
//            return _validationResult;
//        }

//        public virtual ValidationResult CreateAll(IEnumerable<TEntity> entities)
//        {
//            foreach (var entity in entities)
//            {
//                var ValidationEntity = entity as IEntity;
//                if (ValidationEntity != null && !ValidationEntity.IsValid())
//                    ValidationResult.Add(ValidationEntity.ValidationResult);
//            }

//            if (!ValidationResult.IsValid)
//                return ValidationResult;

//            _repository.CreateAll(entities);

//            return _validationResult;
//        }

//        public virtual IEnumerable<TEntity> GetAll(bool @readonly = false)
//        {
//            return _repository.GetAll(@readonly);
//        }

//        public virtual IEnumerable<TEntity> GetAllSimple()
//        {
//            return _repository.GetAllSimple();
//        }

//        public virtual IEnumerable<TEntity> GetAllSimple(Expression<Func<TEntity, bool>> filter)
//        {
//            return _repository.GetAllSimple().Where(filter);
//        }

//        public virtual IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter)
//        {
//            return _repository.GetAll(filter);
//        }

//        public virtual TEntity Get(int id)
//        {
//            return _repository.Get(id);
//        }

//        public virtual TEntity Get(Guid uid)
//        {
//            return _repository.Get(uid);
//        }

//        public virtual TEntity Get(Expression<Func<TEntity, bool>> filter)
//        {
//            return _repository.GetAll().FirstOrDefault(filter);
//        }

//        public virtual ValidationResult Update(TEntity entity)
//        {
//            var ValidationEntity = entity as IEntity;
//            if (ValidationEntity != null && !ValidationEntity.IsValid())
//                ValidationResult.Add(ValidationEntity.ValidationResult);

//            if (!ValidationResult.IsValid)
//                return ValidationResult;

//            _repository.Update(entity);
//            return _validationResult;
//        }

//        public virtual ValidationResult UpdateAll(IEnumerable<TEntity> entities)
//        {
//            foreach (var entity in entities)
//            {
//                var ValidationEntity = entity as IEntity;
//                if (ValidationEntity != null && !ValidationEntity.IsValid())
//                    ValidationResult.Add(ValidationEntity.ValidationResult);
//            }

//            if (!ValidationResult.IsValid)
//                return ValidationResult;

//            _repository.UpdateAll(entities);

//            return _validationResult;
//        }

//        public virtual ValidationResult Delete(TEntity entity)
//        {
//            _repository.Delete(entity);
//            return _validationResult;
//        }

//        public virtual ValidationResult DeleteAll(IEnumerable<TEntity> entities)
//        {
//            //IEntity ValidationEntity;

//            //foreach (var entity in entities)
//            //{
//            //    ValidationEntity = entity as IEntity;
//            //    if (ValidationEntity != null && !ValidationEntity.IsValid())
//            //        ValidationResult.Add(ValidationEntity.ValidationResult);
//            //}

//            //if (!ValidationResult.IsValid)
//            //    return ValidationResult;

//            _repository.DeleteAll(entities);
//            return _validationResult;
//        }
//    }
//}
