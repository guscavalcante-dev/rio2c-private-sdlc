//using PlataformaRio2C.Domain.Entities;
//using PlataformaRio2C.Domain.Interfaces;
//using PlataformaRio2C.Domain.Validation;
//using System;
//using System.Collections.Generic;
//using System.Linq.Expressions;

//namespace PlataformaRio2C.Domain.Services
//{
//    public class QuizAnswerService : Service<QuizAnswer>, IQuizAnswerService
//    {
//        private readonly IQuizAnswerRepository _repository;

//        public QuizAnswerService(IQuizAnswerRepository repository, IRepositoryFactory repositoryFactory)
//            : base(repository)
//        {
//            _repository = repository;
//        }

//        public override ValidationResult CreateAll(IEnumerable<QuizAnswer> entities)
//        {
//            return base.CreateAll(entities);
//        }
//        public ValidationResult Create(QuizAnswer entity)
//        {
//            return base.Create(entity);
//        }

//        public ValidationResult Delete(QuizAnswer entity)
//        {
//            throw new NotImplementedException();
//        }

//        public ValidationResult DeleteAll(IEnumerable<QuizAnswer> entities)
//        {
//            throw new NotImplementedException();
//        }

//        public QuizAnswer Get(Expression<Func<QuizAnswer, bool>> filter)
//        {
//            throw new NotImplementedException();
//        }

//        public IEnumerable<QuizAnswer> GetAll(Expression<Func<QuizAnswer, bool>> filter)
//        {
//            throw new NotImplementedException();
//        }

//        public IEnumerable<QuizAnswer> GetAllSimple(Expression<Func<QuizAnswer, bool>> filter)
//        {
//            throw new NotImplementedException();
//        }

//        public ValidationResult Update(QuizAnswer entity)
//        {
//            throw new NotImplementedException();
//        }

//        public ValidationResult UpdateAll(IEnumerable<QuizAnswer> entities)
//        {
//            throw new NotImplementedException();
//        }

//        QuizAnswer IService<QuizAnswer>.Get(Guid uid)
//        {
//            throw new NotImplementedException();
//        }

//        QuizAnswer IService<QuizAnswer>.Get(int id)
//        {
//            throw new NotImplementedException();
//        }

//        IEnumerable<QuizAnswer> IService<QuizAnswer>.GetAll(bool @readonly)
//        {
//            throw new NotImplementedException();
//        }

//        IEnumerable<QuizAnswer> IService<QuizAnswer>.GetAllSimple()
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
