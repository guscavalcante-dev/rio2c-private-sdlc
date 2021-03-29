// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 03-23-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 03-23-2021
// ***********************************************************************
// <copyright file="MusicBandBaseCommand.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Foolproof;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    ///// <summary>MusicBandBaseCommand</summary>
    //public class MusicBandBaseCommand : BaseCommand
    //{
    //    #region MusicBand properties

    //    public int? MusicBandTypeId { get; set; }
    //    public string Name { get; set; }
    //    public string ImageUrl { get; set; }
    //    public string FormationDate { get; set; }
    //    public string MainMusicInfluences { get; set; }
    //    public string Facebook { get; set; }
    //    public string Instagram { get; set; }
    //    public string Twitter { get; set; }
    //    public string Youtube { get; set; }

    //    #endregion

    //    #region MusicProject properties

    //    public int? AttendeeMusicBandId { get; set; }
    //    public string VideoUrl { get; set; }
    //    public string Music1Url { get; set; }
    //    public string Music2Url { get; set; }
    //    public string Clipping1 { get; set; }
    //    public string Clipping2 { get; set; }
    //    public string Clipping3 { get; set; }
    //    public string Release { get; set; }

    //    #endregion

    //    /// <summary>Initializes a new instance of the <see cref="MusicBandBaseCommand"/> class.</summary>
    //    public MusicBandBaseCommand()
    //    {
    //    }

    //    /// <summary>
    //    /// Updates the base properties.
    //    /// </summary>
    //    /// <param name="musicBandDto">The music band dto.</param>
    //    public void UpdateBaseProperties(MusicBandApiDto musicBandDto)
    //    {
    //        this.MusicBandTypeId = musicBandDto.MusicBandTypeId;
    //        this.Name = musicBandDto.Name;
    //        this.ImageUrl = musicBandDto.ImageUrl;
    //        this.FormationDate = musicBandDto.FormationDate;
    //        this.MainMusicInfluences = musicBandDto.MainMusicInfluences;
    //        this.Facebook = musicBandDto.Facebook;
    //        this.Instagram = musicBandDto.Instagram;
    //        this.Twitter = musicBandDto.Twitter;
    //        this.Youtube = musicBandDto.Youtube;
    //    }
    //}
}