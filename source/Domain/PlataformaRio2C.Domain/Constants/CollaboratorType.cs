// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 09-26-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 01-31-2024
// ***********************************************************************
// <copyright file="CollaboratorType.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace PlataformaRio2C.Domain.Constants
{
    /// <summary>CollaboratorType</summary>
    public class CollaboratorType
    {
        public const string AdminAudiovisual = "Admin | Audiovisual";
        public const string AdminLogistic = "Admin | Logistic";
        public const string AdminMusic = "Admin | Music";
        public const string AdminInnovation = "Admin | Innovation";
        public const string AdminCartoon = "Admin | Cartoon";
        public const string AdminEditorial = "Admin | Editorial";
        public const string AdminConferences = "Admin | Conferences";
        public const string AdminCreator = "Admin | Creator";

        public const string PlayerExecutiveAudiovisual = "Player Executive | Audiovisual";
        public const string PlayerExecutiveMusic = "Player Executive | Music";
        public const string PlayerExecutiveInnovation = "Player Executive | Innovation";
        public const string PlayerExecutiveCartoon = "Player Executive | Cartoon";
        public const string PlayerExecutiveCreator = "Player Executive | Creator";

        public const string CommissionAudiovisual = "Commission | Audiovisual";
        public const string CommissionMusic = "Commission | Music";
        public const string CommissionInnovation = "Commission | Innovation";
        public const string CommissionCartoon = "Commission | Cartoon";
        public const string CommissionCreator = "Commission | Creator";

        public const string Speaker = "Speaker";
        public const string Industry = "Industry";
        public const string Creator = "Creator";
        public const string Summit = "Summit";
        public const string Festvalia = "Festvalia";
        public const string Music = "Music";
        public const string Innovation = "Innovation";
        public const string Cartoon = "Cartoon";

        public static readonly string[] Admins =
        {
            AdminAudiovisual, AdminLogistic, AdminMusic, AdminInnovation, AdminCartoon, AdminEditorial, AdminConferences, AdminCreator
        };

        public static readonly string[] TicketBuyers =
        {
            Industry, Creator, Summit, Festvalia
        };

        public static readonly string[] PlayerExecutives =
        {
            PlayerExecutiveAudiovisual, PlayerExecutiveMusic, PlayerExecutiveInnovation, PlayerExecutiveCartoon, PlayerExecutiveCreator
        };

        public static readonly string[] Audiovisuals =
        {
            AdminAudiovisual, CommissionAudiovisual, PlayerExecutiveAudiovisual, Industry
        };

        public static readonly string[] Musics =
        {
            AdminMusic, CommissionMusic, PlayerExecutiveMusic, Music
        };

        public static readonly string[] Innovations =
        {
            AdminInnovation, CommissionInnovation, PlayerExecutiveInnovation, Innovation
        };

        public static readonly string[] Cartoons =
        {
            AdminCartoon, CommissionCartoon, PlayerExecutiveCartoon, Cartoon
        };

        public static readonly string[] Creators =
        {
            AdminCreator, CommissionCreator, PlayerExecutiveCreator, Creator
        };

        #region Networks

        public static readonly string[] NetworksArray =
        {
            PlayerExecutiveAudiovisual, Speaker, Industry, Summit, Creator
        };

        public const string NetworksString = PlayerExecutiveAudiovisual + "," + Speaker + "," + Industry + "," + Summit + "," + Creator;

        public static readonly string[] NetworksFullVisibilityArray =
        {
            PlayerExecutiveAudiovisual, Speaker, Industry, Summit
        };

        #endregion

        #region Speakers

        // Read
        public const string SpeakersReadString = AdminConferences + "," + AdminAudiovisual + "," + AdminLogistic;
        public static readonly string[] SpeakersReadArray =
        {
            AdminConferences, AdminAudiovisual, AdminLogistic
        };

        // Write
        public const string SpeakersWriteString = AdminConferences;
        public static readonly string[] SpeakersWriteArray =
        {
            AdminConferences
        };

        #endregion
    }
}