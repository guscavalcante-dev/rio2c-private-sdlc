// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : William Almado
// Created          : 10-09-2019
//
// Last Modified By : William Almado
// Last Modified On : 10-09-2019
// ***********************************************************************
// <copyright file="UpdateCollaboratorInterfaceLanguage.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaRio2C.Application.CQRS.Commands.User
{

    public class UpdateUserInterfaceLanguage : BaseCommand
    {
        public Guid Userid { get; set; }
        public string LanguageCode { get; set; }

        public UpdateUserInterfaceLanguage(Guid userId, string languageCode)
        {
            this.Userid = userId;
            this.LanguageCode = languageCode;
        }
        public UpdateUserInterfaceLanguage()
        {

        }
    }
}
