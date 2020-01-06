// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 12-27-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-06-2020
// ***********************************************************************
// <copyright file="RoomDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using System.Linq;
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>RoomDto</summary>
    public class RoomDto
    {
        public Room Room { get; set; }
        public IEnumerable<RoomNameDto> RoomNameBaseDtos { get; set; }

        /// <summary>Initializes a new instance of the <see cref="RoomDto"/> class.</summary>
        public RoomDto()
        {
        }

        /// <summary>Gets the room name by language code.</summary>
        /// <param name="languageCode">The language code.</param>
        /// <returns></returns>
        public RoomNameDto GetRoomNameByLanguageCode(string languageCode)
        {
            if (string.IsNullOrEmpty(languageCode))
            {
                languageCode = "pt-br";
            }

            return this.RoomNameBaseDtos?.FirstOrDefault(rn => rn.LanguageDto.Code == languageCode) ??
                   this.RoomNameBaseDtos?.FirstOrDefault(rn => rn.LanguageDto.Code == "pt-br");
        }
    }
}