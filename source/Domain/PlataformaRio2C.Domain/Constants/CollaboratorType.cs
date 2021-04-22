// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 09-26-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-18-2020
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
        public const string CuratorshipAudiovisual = "Curatorship | Audiovisual";
        public const string CuratorshipMusic = "Curatorship | Music";
        public const string CuratorshipInnovation = "Curatorship | Innovation";
        public const string ExecutiveAudiovisual = "Executive | Audiovisual";
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
            AdminAudiovisual, AdminLogistic
        };

        public static readonly string[] TicketBuyers =
        {
            Speaker, Industry, Creator, Summit, Festvalia
        };

        public static readonly string[] Executives =
        {
            ExecutiveAudiovisual, ExecutiveMusic, ExecutiveInnovation
        };

        #region Networks

        public static readonly string[] NetworksArray =
        {
            ExecutiveAudiovisual, Speaker, Industry, Summit, Creator
        };

        public const string NetworksString = ExecutiveAudiovisual + "," + Speaker + "," + Industry + "," + Summit + "," + Creator;

        public static readonly string[] NetworksFullVisibilityArray =
        {
            ExecutiveAudiovisual, Speaker, Industry, Summit
        };

        #endregion

        #region Speakers

        // Read
        public const string SpeakersReadString = AdminAudiovisual + "," + CuratorshipAudiovisual + "," + AdminLogistic;
        public static readonly string[] SpeakersReadArray =
        {
            AdminAudiovisual, CuratorshipAudiovisual, AdminLogistic
        };

        // Write
        public const string SpeakersWriteString = AdminAudiovisual + "," + CuratorshipAudiovisual;
        public static readonly string[] SpeakersWriteArray =
        {
            AdminAudiovisual, CuratorshipAudiovisual
        };

        #endregion
    }
}