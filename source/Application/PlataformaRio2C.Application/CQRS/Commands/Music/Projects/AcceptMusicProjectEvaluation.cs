// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 02-28-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 02-28-2020
// ***********************************************************************
// <copyright file="AcceptMusicProjectEvaluation.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.ComponentModel.DataAnnotations;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>AcceptMusicProjectEvaluation</summary>
    public class AcceptMusicProjectEvaluation : BaseCommand
    {
        [Display(Name = "Project", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public Guid? ProjectUid { get; set; }

        public MusicProjectDto MusicProjectDto { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="AcceptMusicProjectEvaluation"/> class.</summary>
        /// <param name="musicProjectDto">The music project dto.</param>
        public AcceptMusicProjectEvaluation(MusicProjectDto musicProjectDto)
        {
            this.UpdateModelsAndLists(musicProjectDto);
        }

        /// <summary>Initializes a new instance of the <see cref="AcceptMusicProjectEvaluation"/> class.</summary>
        public AcceptMusicProjectEvaluation()
        {
        }

        /// <summary>Updates the models and lists.</summary>
        /// <param name="musicProjectDto">The music project dto.</param>
        public void UpdateModelsAndLists(MusicProjectDto musicProjectDto)
        {
            this.ProjectUid = musicProjectDto?.MusicProject?.Uid;
            this.MusicProjectDto = musicProjectDto;
        }
    }
}