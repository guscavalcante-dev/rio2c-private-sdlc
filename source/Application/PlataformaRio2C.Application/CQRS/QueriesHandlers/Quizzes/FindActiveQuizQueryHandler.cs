// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-06-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-07-2019
// ***********************************************************************
// <copyright file="FindActiveQuizQueryHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PlataformaRio2C.Application.CQRS.Queries;
using PlataformaRio2C.Application.Dtos;
using PlataformaRio2C.Domain.Interfaces;

namespace PlataformaRio2C.Application.CQRS.QueriesHandlers
{
    /// <summary>FindActiveQuizQueryHandler</summary>
    public class FindActiveQuizQueryHandler : IRequestHandler<FindActiveQuiz, QuizDto>
    {
        private readonly IQuizRepository repo;

        /// <summary>Initializes a new instance of the <see cref="FindActiveQuizQueryHandler"/> class.</summary>
        /// <param name="quizRepository">The quiz repository.</param>
        public FindActiveQuizQueryHandler(IQuizRepository quizRepository)
        {
            this.repo = quizRepository;
        }

        /// <summary>Handles the specified command.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<QuizDto> Handle(FindActiveQuiz cmd, CancellationToken cancellationToken)
        {
            var quiz = await this.repo.FindActiveAsync();
            if (quiz == null)
            {
                return null;
            }

            return new QuizDto(quiz);
        }
    }
}