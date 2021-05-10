// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-06-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-06-2020
// ***********************************************************************
// <copyright file="CreateRoom.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>CreateRoom</summary>
    public class CreateRoom : BaseCommand
    {
        public List<RoomNameBaseCommand> Names { get; set; }

        [Display(Name = "AcceptsVirtualMeeting", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public bool IsVirtualMeeting { get; set; }

        /// <summary>Initializes a new instance of the <see cref="CreateRoom"/> class.</summary>
        /// <param name="roomDto">The room dto.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        public CreateRoom(
            RoomDto roomDto,
            List<LanguageDto> languagesDtos)
        {
            this.UpdateNames(roomDto, languagesDtos);
        }

        /// <summary>Initializes a new instance of the <see cref="CreateRoom"/> class.</summary>
        public CreateRoom()
        {
        }

        #region Private Methods

        /// <summary>Updates the names.</summary>
        /// <param name="roomDto">The room dto.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        private void UpdateNames(RoomDto roomDto, List<LanguageDto> languagesDtos)
        {
            this.Names = new List<RoomNameBaseCommand>();
            foreach (var languageDto in languagesDtos)
            {       
                var roomName = roomDto?.GetRoomNameByLanguageCode(languageDto.Code);
                this.Names.Add(roomName != null ? new RoomNameBaseCommand(roomName) :
                                                  new RoomNameBaseCommand(languageDto));
            }
        }

        #endregion
    }
}