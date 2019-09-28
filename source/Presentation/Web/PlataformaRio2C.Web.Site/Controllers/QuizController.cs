// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 06-28-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-28-2019
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
using System.Threading.Tasks;
using System.Web.Mvc;
using MediatR;
using PlataformaRio2C.Application.CQRS.Queries;
using PlataformaRio2C.Application.Interfaces.Services;
using PlataformaRio2C.Infra.CrossCutting.Identity.Service;

namespace PlataformaRio2C.Web.Site.Controllers
{
    /// <summary>QuizController</summary>
    [Authorize(Order = 1)]
    public class QuizController : BaseController
    {
        private readonly IMediator commandBus;
        private readonly IQuizRepository _quizRepository;
        private readonly IQuizAnswerRepository _answerRepository;
        private readonly IQuizQuestionRepository _questionRepository;
        private readonly IQuizOptionRepository _optionRepository;
        private readonly IQuizAnswerAppService _answerService;

        //private readonly IQuizRepository _quizRepository;
        //private readonly IQuizRepository _quizRepository;

        /// <summary>Initializes a new instance of the <see cref="QuizController"/> class.</summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="identityController">The identity controller.</param>
        /// <param name="repositoryFactory">The repository factory.</param>
        /// <param name="answerService">The answer service.</param>
        public QuizController(IMediator commandBus, IdentityAutenticationService identityController, IRepositoryFactory repositoryFactory, IQuizAnswerAppService answerService)
            : base(commandBus, identityController)
        {
            this.commandBus = commandBus;
            _quizRepository = repositoryFactory.QuizRepository;
            _answerRepository = repositoryFactory.QuizAnswerRepository;
            _questionRepository = repositoryFactory.QuizQuestionRepository;
            _optionRepository = repositoryFactory.QuizOptionRepository;
            _answerService = answerService;
        }

        // GET: Quiz
        public async Task<ActionResult> Index()
        {
            //int userId = User.Identity.GetUserId<int>();

            var quiz = await this.commandBus.Send(new FindActiveQuiz());
            if (quiz == null)
            {
                return RedirectToAction("Index", "Home", new { Area = "" });
            }

            if (this.AnsweredQuiz(this.UserAccessControlDto.User.Id, 1))
            {
                //return RedirectToAction("ProfileEdit", "Collaborator", new { area = "" });
                return RedirectToAction("Index", "Home", new { Area = "" });
            }

            return RedirectToAction("Edition2018");

            //return View();
        }

        /// <summary>Answereds the quiz.</summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="quizId">The quiz identifier.</param>
        /// <returns></returns>
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