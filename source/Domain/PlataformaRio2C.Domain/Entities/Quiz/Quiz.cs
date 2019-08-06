// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-06-2019
// ***********************************************************************
// <copyright file="Quiz.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>Quiz</summary>
    public class Quiz : Entity
    {
        public int EditionId { get; private set; }
        public string Name { get; private set; }
        public bool IsActive { get; private set; }

        public virtual Edition Edition { get; private set; }
        public virtual ICollection<QuizQuestion> Question { get; set; }

        /// <summary>Initializes a new instance of the <see cref="Quiz"/> class.</summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="name">The name.</param>
        public Quiz(int editionId, string name)
        {
            this.EditionId = editionId;
            this.Name = name;
        }

        /// <summary>Initializes a new instance of the <see cref="Quiz"/> class.</summary>
        protected Quiz()
        {
        }

        /// <summary>Returns true if ... is valid.</summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.</returns>
        public override bool IsValid()
        {
            return true;
        }
    }
}