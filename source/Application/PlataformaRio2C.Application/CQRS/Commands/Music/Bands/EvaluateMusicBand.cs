// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 03-30-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 03-31-2021
// ***********************************************************************
// <copyright file="EvaluateMusicBand.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System.ComponentModel.DataAnnotations;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>EvaluateMusicBand</summary>
    public class EvaluateMusicBand : BaseCommand
    {
        [Display(Name = "MusicBand", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public int? MusicBandId { get; set; }

        [Display(Name = "Grade", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public decimal? Grade { get; set; }

        public MusicBand MusicBand { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="EvaluateMusicBand"/> class.</summary>
        /// <param name="musicBand">The music project dto.</param>
        public EvaluateMusicBand(MusicBand musicBand, decimal? grade)
        {
            this.Grade = grade;
            this.UpdateModelsAndLists(musicBand);
        }

        /// <summary>Initializes a new instance of the <see cref="EvaluateMusicBand"/> class.</summary>
        public EvaluateMusicBand()
        {
        }

        /// <summary>Updates the models and lists.</summary>
        /// <param name="musicBand">The music project dto.</param>
        public void UpdateModelsAndLists(MusicBand musicBand)
        {
            this.MusicBandId = musicBand.Id;
            this.MusicBand = musicBand;
        }
    }
}