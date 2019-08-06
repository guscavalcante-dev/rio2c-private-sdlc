// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-06-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-06-2019
// ***********************************************************************
// <copyright file="FindActiveQuiz.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using PlataformaRio2C.Application.Dtos;

namespace PlataformaRio2C.Application.CQRS.Queries
{
    /// <summary>FindActiveQuiz</summary>
    public class FindActiveQuiz : IRequest<QuizDto>
    {
        /// <summary>Initializes a new instance of the <see cref="FindActiveQuiz"/> class.</summary>
        public FindActiveQuiz()
        {
        }
    }
}