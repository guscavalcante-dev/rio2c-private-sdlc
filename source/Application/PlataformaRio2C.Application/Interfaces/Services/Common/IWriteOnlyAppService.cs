using System;

namespace PlataformaRio2C.Application.Interfaces
{
    public interface IWriteOnlyAppService<in TEditViewModel>
    where TEditViewModel : class
    {
        AppValidationResult Create(TEditViewModel viewModel);
        AppValidationResult Update(TEditViewModel viewModel);
        AppValidationResult Delete(Guid uid);
    }
}
