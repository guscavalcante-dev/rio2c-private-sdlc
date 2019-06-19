using PlataformaRio2C.Application.ViewModels;
using System;
using System.Collections.Generic;

namespace PlataformaRio2C.Application.Interfaces.Services
{
    public interface IProducerAppService : IAppService<ProducerBasicAppViewModel, ProducerDetailAppViewModel, ProducerEditAppViewModel, ProducerItemListAppViewModel>
    {
        AppValidationResult UpdateByPortal(ProducerEditAppViewModel viewModel);
        IEnumerable<ProducerAppViewModel> GetAllByUserId(int id);
        ProducerAppViewModel GetByUserId(int id, Guid producerUid);
        ProducerDetailAppViewModel GetDetailByUserId(int id);
        ProducerDetailAppViewModel GetDetailByUserId(int id, Guid producerUid);
        ProducerEditAppViewModel GetEditByUserId(int id);
        ProducerEditAppViewModel GetEditByUserId(int id, Guid producerUid);
        ImageFileAppViewModel GetThumbImage(Guid uid);
        ImageFileAppViewModel GetImage(Guid uid);
    }
}
