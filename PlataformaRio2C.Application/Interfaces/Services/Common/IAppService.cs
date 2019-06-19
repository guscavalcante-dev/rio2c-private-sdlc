using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace PlataformaRio2C.Application.Interfaces
{
    public interface IAppService<TEntityViewModel, TDetailViewModel, TEditViewModel, TItemListViewModel> : IWriteOnlyAppService<TEditViewModel>, IDisposable
        where TEntityViewModel : class
        where TDetailViewModel : class
        where TEditViewModel : class
        where TItemListViewModel : class
    {
        TEntityViewModel Get(int id);
        TEntityViewModel Get(Guid uid);
        TEditViewModel GetByEdit(Guid uid);
        TDetailViewModel GetByDetails(Guid uid);
        TEditViewModel GetEditViewModel();
        IEnumerable<TItemListViewModel> All(bool @readonly = false);
        IEnumerable<TItemListViewModel> GetAllSimple();
        IEnumerable<TItemListViewModel> GetAllSimple(TItemListViewModel filter);
        IEnumerable<TEntityViewModel> Find(Expression<Func<TEntityViewModel, bool>> predicate, bool @readonly = false);
    }
}
