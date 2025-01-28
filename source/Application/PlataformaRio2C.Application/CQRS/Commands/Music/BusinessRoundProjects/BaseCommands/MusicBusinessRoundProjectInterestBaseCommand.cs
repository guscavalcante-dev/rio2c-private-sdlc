// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Daniel Giese Rodrigues
// Created          : 23-01-2025
//
// Last Modified By : Daniel Giese Rodrigues
// Last Modified On : 23-01-2025
// ***********************************************************************
// <copyright file="ProjectLogLineBaseCommand.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System.ComponentModel.DataAnnotations;

namespace PlataformaRio2C.Application.CQRS.Commands.Music.BusinessRoundProjects.BaseCommands
{
    public class MusicBusinessRoundProjectInterestBaseCommand : BaseCommand
    {
        public static readonly int AdditionalInfoMaxLength = 200;

        public int MusicBusinessRoundProjectId { get; set; }

        public int InterestId { get; set; }

        [Display(Name = "AdditionalInformation", ResourceType = typeof(Labels))]
        [StringLength(200, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string AdditionalInfo { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MusicBusinessRoundProjectInterestBaseCommand"/> class.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public MusicBusinessRoundProjectInterestBaseCommand(MusicBusinessRoundProjectInterest entity)
        {
            this.MusicBusinessRoundProjectId = entity.MusicBusinessRoundProjectId;
            this.InterestId = entity.InterestId;
            this.AdditionalInfo = entity.AdditionalInfo;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MusicBusinessRoundProjectInterestBaseCommand"/> class.
        /// </summary>
        public MusicBusinessRoundProjectInterestBaseCommand()
        {
        }
    }

}
