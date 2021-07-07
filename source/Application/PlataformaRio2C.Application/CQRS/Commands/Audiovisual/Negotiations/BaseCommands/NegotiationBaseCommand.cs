// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 06-04-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 06-04-2021
// ***********************************************************************
// <copyright file="NegotiationBaseCommand.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>
    /// NegotiationBaseCommand
    /// </summary>
    /// <seealso cref="PlataformaRio2C.Application.CQRS.Commands.BaseCommand" />
    public class NegotiationBaseCommand : BaseCommand
    {
        [Display(Name = nameof(Labels.Player), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public Guid? BuyerOrganizationUid { get; set; }

        [Display(Name = nameof(Labels.Project), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public Guid? ProjectUid { get; set; }

        [Display(Name = nameof(Labels.Date), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public Guid? NegotiationConfigUid { get; set; }

        [Display(Name = nameof(Labels.Room), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public Guid? NegotiationRoomConfigUid { get; set; }

        [Display(Name = nameof(Labels.StartTime), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public string StartTime { get; set; }

        public int? RoundNumber { get; set; }
        public Guid? SellerOrganizationUid { get; set; }

        // Properties used to set initial selection at select2.selectedOption
        public Guid? InitialBuyerOrganizationUid { get; set; }
        public string InitialBuyerOrganizationName { get; set; }

        public Guid? InitialProjectUid { get; set; }
        public string InitialProjectName { get; set; }

        public ProjectBuyerEvaluationDto ProjectBuyerEvaluationDto { get; set; }

        /// <summary>
        /// Updates the base properties.
        /// </summary>
        /// <param name="projectBuyerEvaluationDto">The negotiation dto.</param>
        public void UpdateBaseProperties(ProjectBuyerEvaluationDto projectBuyerEvaluationDto, string userInterfaceLanguage)
        {
            if (projectBuyerEvaluationDto == null)
                return;

            this.ProjectBuyerEvaluationDto = projectBuyerEvaluationDto;

            this.InitialBuyerOrganizationUid = projectBuyerEvaluationDto?.BuyerAttendeeOrganizationDto?.Organization?.Uid;
            this.InitialBuyerOrganizationName = projectBuyerEvaluationDto?.BuyerAttendeeOrganizationDto?.Organization?.TradeName;
            this.BuyerOrganizationUid = this.InitialBuyerOrganizationUid;

            this.InitialProjectUid = projectBuyerEvaluationDto?.ProjectDto?.Project?.Uid;
            this.InitialProjectName = projectBuyerEvaluationDto?.ProjectDto?.Project?.ProjectTitles?.FirstOrDefault(pt => pt.Language.Code == userInterfaceLanguage)?.Value;
            this.ProjectUid = this.InitialProjectUid;

            this.SellerOrganizationUid = projectBuyerEvaluationDto?.ProjectDto?.SellerAttendeeOrganizationDto?.Organization?.Uid;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NegotiationBaseCommand"/> class.
        /// </summary>
        public NegotiationBaseCommand()
        {

        }
    }
}
