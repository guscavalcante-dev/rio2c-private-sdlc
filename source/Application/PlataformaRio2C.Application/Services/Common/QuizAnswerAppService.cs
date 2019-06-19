using System;
using System.Collections.Generic;
using PlataformaRio2C.Application.Interfaces;
using PlataformaRio2C.Application.Interfaces.Services;
using PlataformaRio2C.Application.ViewModels;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context;
using PlataformaRio2C.Infra.Data.Context.Interfaces;

namespace PlataformaRio2C.Application.Services
{
    public class QuizAnswerAppService : AppService<PlataformaRio2CContext, QuizAnswer, AnswerBasicAppViewModel, AnswerDetailAppViewModel, AnswerEditAppViewModel, AnswerItemListAppViewModel>, IQuizAnswerAppService
    {
        private readonly IQuizAnswerRepository _answerRepository;

        public QuizAnswerAppService(IQuizAnswerService service, IUnitOfWork unitOfWork, IRepositoryFactory repositoryFactory)
        : base(unitOfWork, service)
        {
            _answerRepository = repositoryFactory.QuizAnswerRepository;
        }

        public AppValidationResult Create(AnswerBasicAppViewModel viewModel)
        {
            throw new NotImplementedException();
        }

        public AppValidationResult CreateAll(AnswerBasicAppViewModel viewModel, int userId)
        {
            BeginTransaction();

            var entities = viewModel.MapReverse(userId);
            foreach (var entity in entities)
            {
                ValidationResult.Add(service.Create(entity));

            }

            if (ValidationResult.IsValid)
                Commit();


            return ValidationResult;

        }


        public IEnumerable<AnswerBasicAppViewModel> GetAllSimple(AnswerBasicAppViewModel filter)
        {
            throw new NotImplementedException();
        }

        public AppValidationResult Update(AnswerBasicAppViewModel viewModel)
        {
            throw new NotImplementedException();
        }

        IEnumerable<AnswerBasicAppViewModel> IAppService<AnswerBasicAppViewModel, AnswerBasicAppViewModel, AnswerBasicAppViewModel, AnswerBasicAppViewModel>.All(bool @readonly)
        {
            throw new NotImplementedException();
        }

        IEnumerable<AnswerBasicAppViewModel> IAppService<AnswerBasicAppViewModel, AnswerBasicAppViewModel, AnswerBasicAppViewModel, AnswerBasicAppViewModel>.GetAllSimple()
        {
            throw new NotImplementedException();
        }

        AnswerBasicAppViewModel IAppService<AnswerBasicAppViewModel, AnswerBasicAppViewModel, AnswerBasicAppViewModel, AnswerBasicAppViewModel>.GetByDetails(Guid uid)
        {
            throw new NotImplementedException();
        }

        AnswerBasicAppViewModel IAppService<AnswerBasicAppViewModel, AnswerBasicAppViewModel, AnswerBasicAppViewModel, AnswerBasicAppViewModel>.GetByEdit(Guid uid)
        {
            throw new NotImplementedException();
        }

        AnswerBasicAppViewModel IAppService<AnswerBasicAppViewModel, AnswerBasicAppViewModel, AnswerBasicAppViewModel, AnswerBasicAppViewModel>.GetEditViewModel()
        {
            throw new NotImplementedException();
        }
    }
}