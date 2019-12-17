// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 09-26-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-17-2019
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
            ExecutiveAudiovisual, Speaker, Industry, Creator
        };

        public const string NetworksString = ExecutiveAudiovisual + "," + Speaker + "," + Industry + "," + Creator;

        #endregion
    }
}