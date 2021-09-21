// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 09-26-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 08-28-2021
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
        public const string AdminEditorial = "Admin | Editorial";
        public const string AdminConferences = "Admin | Conferences";

        public const string AudiovisualPlayerExecutive = "Executive | Audiovisual Player";
        public const string ExecutiveMusic = "Executive | Music";
        public const string ExecutiveInnovation = "Executive | Innovation";

        public const string CommissionAudiovisual = "Commission | Audiovisual";
        public const string CommissionMusic = "Commission | Music";
        public const string CommissionInnovation = "Commission | Innovation";

        public const string Speaker = "Speaker";
        public const string Industry = "Industry";
        public const string Creator = "Creator";
        public const string Summit = "Summit";
        public const string Festvalia = "Festvalia";
        public const string Music = "Music";
        public const string Innovation = "Innovation";

        public static readonly string[] Admins =
        {
            AdminAudiovisual, AdminLogistic, AdminMusic, AdminInnovation, AdminEditorial, AdminConferences
        };

        public static readonly string[] TicketBuyers =
        {
            Speaker, Industry, Creator, Summit, Festvalia
        };

        public static readonly string[] Executives =
        {
            AudiovisualPlayerExecutive, ExecutiveMusic, ExecutiveInnovation
        };

        public static readonly string[] Audiovisuals =
        {
            AdminAudiovisual, CommissionAudiovisual, AudiovisualPlayerExecutive, Industry
        };

        public static readonly string[] Musics =
        {
            AdminMusic, CommissionMusic, ExecutiveMusic, Music
        };

        public static readonly string[] Innovations =
        {
            AdminInnovation, CommissionInnovation, ExecutiveInnovation, Innovation
        };

        #region Networks

        public static readonly string[] NetworksArray =
        {
            AudiovisualPlayerExecutive, Speaker, Industry, Summit, Creator
        };

        public const string NetworksString = AudiovisualPlayerExecutive + "," + Speaker + "," + Industry + "," + Summit + "," + Creator;

        public static readonly string[] NetworksFullVisibilityArray =
        {
            AudiovisualPlayerExecutive, Speaker, Industry, Summit
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