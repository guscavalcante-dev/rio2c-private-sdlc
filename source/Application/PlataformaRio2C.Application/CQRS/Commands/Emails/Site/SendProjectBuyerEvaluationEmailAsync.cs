// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 12-10-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-11-2019
// ***********************************************************************
// <copyright file="SendProjectBuyerEvaluationEmailAsync.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>SendProjectBuyerEvaluationEmailAsync</summary>
    public class SendProjectBuyerEvaluationEmailAsync : EmailBaseCommand
    {
        public ProjectBuyerEvaluationEmailDto ProjectBuyerEvaluationEmailDto { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="SendProjectBuyerEvaluationEmailAsync"/> class.</summary>
        /// <param name="projectBuyerEvaluationEmailDto">The project buyer evaluation email dto.</param>
        /// <param name="emailRecipientDto">The email recipient dto.</param>
        /// <param name="edition">The edition.</param>
        public SendProjectBuyerEvaluationEmailAsync(
            ProjectBuyerEvaluationEmailDto projectBuyerEvaluationEmailDto,
            EmailRecipientDto emailRecipientDto,
            Edition edition)
            : base(
                emailRecipientDto.RecipientUser.Id,
                emailRecipientDto.RecipientUser.Uid,
                emailRecipientDto.RecipientUser.Name.GetFirstWord(),
                emailRecipientDto.RecipientUser.Name,
                emailRecipientDto.RecipientUser.Email,
                edition,
                emailRecipientDto.RecipientLanguage?.Code ?? "pt-br")
        {
            this.ProjectBuyerEvaluationEmailDto = projectBuyerEvaluationEmailDto;
        }

        /// <summary>Initializes a new instance of the <see cref="SendProjectBuyerEvaluationEmailAsync"/> class.</summary>
        public SendProjectBuyerEvaluationEmailAsync()
        {
        }
    }
}