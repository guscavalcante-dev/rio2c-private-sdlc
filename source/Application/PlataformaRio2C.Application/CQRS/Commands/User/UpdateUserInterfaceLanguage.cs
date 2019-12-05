// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : William Almado
// Created          : 10-09-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-05-2019
// ***********************************************************************
// <copyright file="UpdateUserInterfaceLanguage.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>UpdateUserInterfaceLanguage</summary>
    public class UpdateUserInterfaceLanguage : BaseCommand
    {
        public Guid Useruid { get; set; }
        public string LanguageCode { get; set; }

        /// <summary>Initializes a new instance of the <see cref="UpdateUserInterfaceLanguage"/> class.</summary>
        /// <param name="userUid">The user uid.</param>
        /// <param name="languageCode">The language code.</param>
        public UpdateUserInterfaceLanguage(Guid userUid, string languageCode)
        {
            this.Useruid = userUid;
            this.LanguageCode = languageCode;
        }

        /// <summary>Initializes a new instance of the <see cref="UpdateUserInterfaceLanguage"/> class.</summary>
        public UpdateUserInterfaceLanguage()
        {
        }
    }
}