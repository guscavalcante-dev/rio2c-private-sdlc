// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 03-30-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 03-30-2021
// ***********************************************************************
// <copyright file="EvaluateProject.cs" company="Softo">
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
    /// <summary>EvaluateProject</summary>
    public class EvaluateProject : BaseCommand
    {
        [Display(Name = "Project", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public Guid? ProjectUid { get; set; }

        public MusicProjectDto MusicProjectDto { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="EvaluateProject"/> class.</summary>
        /// <param name="musicProjectDto">The music project dto.</param>
        public EvaluateProject(MusicProjectDto musicProjectDto)
        {
            this.UpdateModelsAndLists(musicProjectDto);
        }

        /// <summary>Initializes a new instance of the <see cref="EvaluateProject"/> class.</summary>
        public EvaluateProject()
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