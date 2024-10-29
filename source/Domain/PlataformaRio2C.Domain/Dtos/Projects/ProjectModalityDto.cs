// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Gilson Oliveira
// Created          : 10-23-2024
//
// Last Modified By : Gilson Oliveira
// Last Modified On : 10-24-2024
// ***********************************************************************
// <copyright file="ProjectModalityDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>ProjectModalityDto</summary>
    public class ProjectModalityDto
    {
        public int Id { get; set; }

        public Guid Uid { get; set; }

        public string Name { get; set; }

        /// <summary>Initializes a new instance of the <see cref="ProjectModalityDto"/> class.</summary>
        public ProjectModalityDto()
        {
            //
        }

        /// <summary>
        /// Gets the name of the translated.
        /// </summary>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        /// <returns></returns>
        public string GetTranslatedName(string userInterfaceLanguage)
        {
            return this.Name?.GetSeparatorTranslation(userInterfaceLanguage, '|');
        }

        /// <summary>
        /// Translates this instance.
        /// </summary>
        public void Translate(string userInterfaceLanguage)
        {
            this.Name = this.Name?.GetSeparatorTranslation(userInterfaceLanguage, '|');
        }
    }
}