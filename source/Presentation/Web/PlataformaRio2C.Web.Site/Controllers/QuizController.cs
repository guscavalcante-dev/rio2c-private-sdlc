// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 06-28-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 07-01-2019
// ***********************************************************************
// <copyright file="QuizController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Application.ViewModels;
using Microsoft.AspNet.Identity;
using PlataformaRio2C.Domain.Interfaces;
using System.Linq;
using System.Web.Mvc;
using PlataformaRio2C.Application.Interfaces.Services;

namespace PlataformaRio2C.Web.Site.Controllers
{
    /// <summary>QuizController</summary>
    public class QuizController : BaseController
    {
        private readonly IQuizRepository _quizRepository;
        private readonly IQuizAnswerRepository _answerRepository;
        private readonly IQuizQuestionRepository _questionRepository;
        private readonly IQuizOptionRepository _optionRepository;
        private readonly IQuizAnswerAppService _answerService;

        //private readonly IQuizRepository _quizRepository;
        //private readonly IQuizRepository _quizRepository;

        /// <summary>Initializes a new instance of the <see cref="QuizController"/> class.</summary>
        /// <param name="repositoryFactory">The repository factory.</param>
        /// <param name="answerService">The answer service.</param>
        public QuizController(IRepositoryFactory repositoryFactory, IQuizAnswerAppService answerService)
        {
            _quizRepository = repositoryFactory.QuizRepository;
            _answerRepository = repositoryFactory.QuizAnswerRepository;
            _questionRepository = repositoryFactory.QuizQuestionRepository;
            _optionRepository = repositoryFactory.QuizOptionRepository;
            _answerService = answerService;
        }

        // GET: Quiz
        public ActionResult Index()
        {
            int userId = User.Identity.GetUserId<int>();

            if (AnsweredQuiz(userId, 1))
            {
                return RedirectToAction("ProfileEdit", "Collaborator", new { area = "" });
                //return RedirectToAction("", "Dashboard");
            }
            else
            {
                return RedirectToAction("Edition2018");
            }


            //return View();
        }

        private bool AnsweredQuiz(int userId, int quizId)
        {
            bool response = false;

            //int countAnswer = _answerRepository.GetAll(e => e.Option.Question.QuizId == quizId && e.UserId == userId).ToList().Count();
            int countAnswer = _answerRepository.GetAll(e => e.UserId == userId).ToList().Count();

            if (countAnswer >= 1)
            {
                response = true;
            }

            return response;
        }

        public ActionResult Edition2018()
        {
            var quiz = _quizRepository.Get(1);
            var questions = _questionRepository.GetAll(e => e.QuizId == quiz.Id).ToList();
            var option = _optionRepository.GetAll().ToList();

            var viewmodel = new QuizBasicAppViewModel();
            viewmodel.Name = quiz.Name;
            viewmodel.Question = questions;
            viewmodel.Option = option;

            return View(viewmodel);
        }

        [HttpPost]
        public ActionResult SubmitQuiz(AnswerBasicAppViewModel viewmodel)
        {
            int userId = User.Identity.GetUserId<int>();

            _answerService.CreateAll(viewmodel, userId);

            return RedirectToAction("ProfileEdit", "Collaborator", new { area = "" });
            //return RedirectToAction("", "Dashboard");
        }
    }
}