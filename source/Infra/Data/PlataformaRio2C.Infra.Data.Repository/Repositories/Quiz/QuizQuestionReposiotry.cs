﻿// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Repository
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-06-2019
// ***********************************************************************
// <copyright file="QuizQuestionRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    /// <summary>QuizQuestionRepository</summary>
    public class QuizQuestionRepository : Repository<PlataformaRio2CContext, QuizQuestion>, IQuizQuestionRepository
    {
        private PlataformaRio2CContext _context;

        /// <summary>Initializes a new instance of the <see cref="QuizQuestionRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public QuizQuestionRepository(PlataformaRio2CContext context)
            : base(context)
        {
            _context = context;
        }
    }
}