// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Gilson Oliveira
// Created          : 11-22-2024
//
// Last Modified By : Gilson Oliveira
// Last Modified On : 11-22-2024
// ***********************************************************************
// <copyright file="RefuseMusicProjectEvaluation.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.ComponentModel.DataAnnotations;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>RefuseMusicProjectEvaluation</summary>
    public class RefuseMusicPitchingEvaluation : BaseCommand
    {
        [Display(Name = "Project", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public Guid? MusicBandUid { get; set; }

        public MusicBand MusicBand { get; private set; }

        public MusicProjectDto MusicProjectDto { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="RefuseMusicPitchingEvaluation"/> class.</summary>
        /// <param name="musicProjectDto">The music project dto.</param>
        /// <param name="musicBandUid">The music project dto.</param>
        public RefuseMusicPitchingEvaluation(MusicProjectDto musicProjectDto, Guid musicBandUid)
        {
            this.MusicBandUid = musicBandUid;
            this.UpdateModelsAndLists(musicProjectDto);
        }

        /// <summary>Initializes a new instance of the <see cref="AcceptMusicPitchingEvaluation"/> class.</summary>
        public RefuseMusicPitchingEvaluation()
        {
        }

        /// <summary>Updates the models and lists.</summary>
        /// <param name="musicProjectDto">The music project dto.</param>
        public void UpdateModelsAndLists(MusicProjectDto musicProjectDto)
        {
            this.MusicProjectDto = musicProjectDto;
        }
    }
}