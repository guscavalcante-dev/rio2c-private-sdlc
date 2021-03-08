// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-08-2021
// ***********************************************************************
// <copyright file="QuizMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>
    /// QuizMap
    /// </summary>
    public class QuizMap : EntityTypeConfiguration<Quiz>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QuizMap"/> class.
        /// </summary>
        public QuizMap()
        {
            this.ToTable("Quizzes");

            //this.HasRequired(t => t.Edition)
            //        .WithRequiredPrincipal(e => e.Quiz);
            //        //.HasForeignKey(d => d.EventId);
        }
    }
}