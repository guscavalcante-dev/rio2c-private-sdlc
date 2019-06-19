using LinqKit;
using PlataformaRio2C.Application.Common;
using PlataformaRio2C.Application.Interfaces;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using PlataformaRio2C.Infra.Data.Context.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace PlataformaRio2C.Application
{
    public class AppService<TContext, TEntity, TEntityViewModel, TDetailViewModel, TEditViewModel, TItemListViewModel> : ITransactionAppService<TContext>, IDisposable
        where TEntity : Entity
        where TContext : IDbContext, new()
        where TEntityViewModel : EntityViewModel<TEntityViewModel, TEntity>, IEntityViewModel<TEntity>, new()
        where TDetailViewModel : TEntityViewModel, IEntityViewModel<TEntity>, new()
        where TEditViewModel : TEntityViewModel, IEntityViewModel<TEntity>, new()
        where TItemListViewModel : EntityViewModel<TItemListViewModel, TEntity>, new()
    {
        private readonly IUnitOfWork _unitOfWork;
        protected readonly IService<TEntity> service;

        public AppService(IUnitOfWork uow, IService<TEntity> service)
        {
            _unitOfWork = uow;
            this.service = service;
            ValidationResult = new AppValidationResult();
        }

        protected AppValidationResult ValidationResult { get; private set; }

        public virtual void BeginTransaction()
        {
            _unitOfWork.BeginTransaction();
        }

        public virtual void Commit()
        {
            var result = _unitOfWork.SaveChanges();
            if (!result.Success)
            {
                if (result.ValidationResults.Any())
                {
                    foreach (var error in result.ValidationResults)
                    {
                        ValidationResult.Add(error.ErrorMessage);
                    }
                }
                else
                {
                    ValidationResult.Add(Messages.ErrorWhileSavingInDataBase);
                }
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public virtual TEditViewModel GetEditViewModel()
        {
            Type type = typeof(TEditViewModel);

            TEditViewModel vm = (TEditViewModel)Activator.CreateInstance(type);

            return vm;
        }

        public virtual TEntityViewModel Get(Guid uid)
        {
            TEntityViewModel vm = default(TEntityViewModel);

            var entity = service.Get(uid);

            if (entity != null)
            {
                Type type = typeof(TEntityViewModel);
                ConstructorInfo ctor = type.GetConstructor(new[] { typeof(TEntity) });

                vm = (TEntityViewModel)ctor.Invoke(new TEntity[] { entity });
            }

            return vm;
        }

        public virtual TDetailViewModel GetByDetails(Guid uid)
        {
            TDetailViewModel vm = default(TDetailViewModel);

            var entity = service.Get(uid);

            if (entity != null)
            {
                Type type = typeof(TDetailViewModel);
                ConstructorInfo ctor = type.GetConstructor(new[] { typeof(TEntity) });

                vm = (TDetailViewModel)ctor.Invoke(new TEntity[] { entity });
            }

            return vm;
        }

        public virtual TEditViewModel GetByEdit(Guid uid)
        {
            TEditViewModel vm = default(TEditViewModel);

            var entity = service.Get(uid);

            if (entity != null)
            {
                Type type = typeof(TEditViewModel);
                ConstructorInfo ctor = type.GetConstructor(new[] { typeof(TEntity) });

                vm = (TEditViewModel)ctor.Invoke(new TEntity[] { entity });
            }

            return vm;
        }


        public virtual TEntityViewModel Get(int id)
        {
            TEntityViewModel vm = default(TEntityViewModel);

            var entity = service.Get(id);

            if (entity != null)
            {
                Type type = typeof(TEntityViewModel);
                ConstructorInfo ctor = type.GetConstructor(new[] { typeof(TEntity) });

                vm = (TEntityViewModel)ctor.Invoke(new TEntity[] { entity });
            }

            return vm;
        }


        public virtual IEnumerable<TItemListViewModel> All(bool @readonly = false)
        {
            var entities = service.GetAll(@readonly).ToList();
            if (entities != null && entities.Any())
            {
                Type type = typeof(EntityViewModel<TItemListViewModel, TEntity>);

                IEnumerable<TItemListViewModel> results = type.GetMethod("MapList", BindingFlags.Public | BindingFlags.Static).Invoke(null, new[] { entities }) as IEnumerable<TItemListViewModel>;
                return results.ToList();
            }

            return new List<TItemListViewModel>() { };
        }

        public virtual IEnumerable<TItemListViewModel> GetAllSimple()
        {
            var entities = service.GetAllSimple().ToList();
            if (entities != null && entities.Any())
            {
                Type type = typeof(EntityViewModel<TItemListViewModel, TEntity>);

                IEnumerable<TItemListViewModel> results = type.GetMethod("MapList", BindingFlags.Public | BindingFlags.Static).Invoke(null, new[] { entities }) as IEnumerable<TItemListViewModel>;
                return results.ToList();
            }

            return new List<TItemListViewModel>() { };
        }

        public virtual IEnumerable<TItemListViewModel> GetAllSimple(TItemListViewModel filter)
        {
            var entities = service.GetAllSimple(GetPredicateForAllSimple(filter)).ToList();
            //entities = entities.Where(e => e.CreationDate == filter.CreationDate).ToList();
            if (entities != null && entities.Any())
            {
                Type type = typeof(EntityViewModel<TItemListViewModel, TEntity>);

                IEnumerable<TItemListViewModel> results = type.GetMethod("MapList", BindingFlags.Public | BindingFlags.Static).Invoke(null, new[] { entities }) as IEnumerable<TItemListViewModel>;
                return results.AsQueryable().Where(GetPredicateForAllSimpleViewModel(filter)).ToList();
            }

            return new List<TItemListViewModel>() { };
        }

        private Expression<Func<TEntity, bool>> GetPredicateForAllSimple(TItemListViewModel filter)
        {
            var predicate = PredicateBuilder.New<TEntity>(true);

            Type typeViewModel = typeof(TItemListViewModel);
            Type typeEntityModel = typeof(TEntity);

            var propertiesViewModel = typeViewModel.GetProperties();
            var propertiesEntityModel = typeEntityModel.GetProperties();
            var propertiesNamesCommon = propertiesViewModel.Select(e => e.Name).Intersect(propertiesEntityModel.Select(e => e.Name));
            var propertiesCommonVieModelAndEntity = propertiesViewModel.Where(e => propertiesNamesCommon.Contains(e.Name));

            foreach (var itemProperty in propertiesCommonVieModelAndEntity)
            {
                var typeName = itemProperty.PropertyType.Name;
                var itemFilterValue = itemProperty.GetValue(filter, null);

                if (itemFilterValue != null && typeName == "String")
                {
                    var predicateItem = PredicateBuilder.New<TEntity>(false);

                    var idName = itemProperty.Name;
                    var idValue = itemFilterValue.ToString();

                    var param = Expression.Parameter(typeof(TEntity));
                    var condition =
                        Expression.Lambda<Func<TEntity, bool>>(
                            Expression.Equal(
                                Expression.Property(param, idName),
                                Expression.Constant(idValue, typeof(string))
                            ),
                            param
                        );

                    predicateItem = predicateItem.Or(condition);

                    predicate = PredicateBuilder.And<TEntity>(predicate, predicateItem);
                }
                else if (itemFilterValue != null && typeName == "Guid" && itemFilterValue.ToString() != Guid.Empty.ToString())
                {
                    var predicateItem = PredicateBuilder.New<TEntity>(false);

                    var idName = itemProperty.Name;
                    var idValue = Guid.Parse(itemFilterValue.ToString());

                    var param = Expression.Parameter(typeof(TEntity));
                    var condition =
                        Expression.Lambda<Func<TEntity, bool>>(
                            Expression.Equal(
                                Expression.Property(param, idName),
                                Expression.Constant(idValue, typeof(Guid))
                            ),
                            param
                        );

                    predicateItem = predicateItem.Or(condition);

                    predicate = PredicateBuilder.And<TEntity>(predicate, predicateItem);
                }
                else if (itemFilterValue != null && typeName == "DateTime" && itemFilterValue.ToString() != default(DateTime).ToString())
                {
                    var predicateItem = PredicateBuilder.New<TEntity>(false);

                    var idName = itemProperty.Name;
                    var idValue = Convert.ToDateTime(itemFilterValue);

                    var param = Expression.Parameter(typeof(TEntity));
                    var condition =
                        Expression.Lambda<Func<TEntity, bool>>(
                            Expression.Equal(
                                Expression.Property(param, idName),
                                Expression.Constant(idValue, typeof(DateTime))
                            ),
                            param
                        );

                    predicateItem = predicateItem.Or(condition);

                    predicate = PredicateBuilder.And<TEntity>(predicate, predicateItem);
                }
            }

            return predicate;
        }

        private Expression<Func<TItemListViewModel, bool>> GetPredicateForAllSimpleViewModel(TItemListViewModel filter)
        {
            var predicate = PredicateBuilder.New<TItemListViewModel>(true);

            Type typeViewModel = typeof(TItemListViewModel);

            var propertiesViewModel = typeViewModel.GetProperties();

            foreach (var itemProperty in propertiesViewModel)
            {
                var typeName = itemProperty.PropertyType.Name;
                var itemFilterValue = itemProperty.GetValue(filter, null);

                if (itemFilterValue != null && typeName == "String")
                {
                    var predicateItem = PredicateBuilder.New<TItemListViewModel>(false);

                    var idName = itemProperty.Name;
                    var idValue = itemFilterValue.ToString();

                    var param = Expression.Parameter(typeof(TItemListViewModel));
                    var condition =
                        Expression.Lambda<Func<TItemListViewModel, bool>>(
                            Expression.Equal(
                                Expression.Property(param, idName),
                                Expression.Constant(idValue, typeof(string))
                            ),
                            param
                        );

                    predicateItem = predicateItem.Or(condition);

                    predicate = PredicateBuilder.And<TItemListViewModel>(predicate, predicateItem);
                }
                else if (itemFilterValue != null && typeName == "Guid" && itemFilterValue.ToString() != Guid.Empty.ToString())
                {
                    var predicateItem = PredicateBuilder.New<TItemListViewModel>(false);

                    var idName = itemProperty.Name;
                    var idValue = Guid.Parse(itemFilterValue.ToString());

                    var param = Expression.Parameter(typeof(TItemListViewModel));
                    var condition =
                        Expression.Lambda<Func<TItemListViewModel, bool>>(
                            Expression.Equal(
                                Expression.Property(param, idName),
                                Expression.Constant(idValue, typeof(Guid))
                            ),
                            param
                        );

                    predicateItem = predicateItem.Or(condition);

                    predicate = PredicateBuilder.And<TItemListViewModel>(predicate, predicateItem);
                }
            }

            return predicate;
        }

        public virtual AppValidationResult Create(TEditViewModel viewModel)
        {
            BeginTransaction();
            Type type = typeof(TEditViewModel);

            var entity = viewModel.MapReverse();
            ValidationResult.Add(service.Create(entity));

            if (ValidationResult.IsValid)
                Commit();

            return ValidationResult;
        }


        public virtual IEnumerable<TEntityViewModel> Find(Expression<Func<TEntityViewModel, bool>> predicate, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public virtual AppValidationResult Delete(Guid uid)
        {

            BeginTransaction();
            var entity = service.Get(uid);

            if (entity != null)
            {
                ValidationResult.Add(service.Delete(entity));
            }

            if (ValidationResult.IsValid)
                Commit();

            return ValidationResult;
        }

        public virtual AppValidationResult Update(TEditViewModel viewModel)
        {
            BeginTransaction();

            var entity = service.Get(viewModel.Uid);

            if (entity != null)
            {
                var entityAlter = viewModel.MapReverse(entity);

                ValidationResult.Add(service.Update(entityAlter));
            }


            if (ValidationResult.IsValid)
                Commit();

            return ValidationResult;
        }
    }
}
