// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-06-2020
// ***********************************************************************
// <copyright file="Room.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>Room</summary>
    public class Room : Entity
    {       
        public int EditionId { get; private set; }

        public virtual Edition Edition { get; private set; }

        public virtual ICollection<RoomName> RoomNames { get; private set; }
        public virtual ICollection<Conference> Conferences { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="Room"/> class.</summary>
        /// <param name="roomUid">The room uid.</param>
        /// <param name="edition">The edition.</param>
        /// <param name="roomNames">The room names.</param>
        /// <param name="userId">The user identifier.</param>
        public Room(
            Guid roomUid,
            Edition edition,
            List<RoomName> roomNames,
            int userId)
        {
            //this.Uid = roomUid;
            this.EditionId = edition?.Id ?? 0;
            this.Edition = edition;
            this.SynchronizeRoomNames(roomNames, userId);

            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.Now;
            this.CreateUserId = this.UpdateUserId = userId;
        }

        /// <summary>Initializes a new instance of the <see cref="Room"/> class.</summary>
        protected Room()
        {
        }

        /// <summary>Deletes the specified user identifier.</summary>
        /// <param name="userId">The user identifier.</param>
        public void Delete(int userId)
        {
            this.IsDeleted = true;
            this.SynchronizeRoomNames(new List<RoomName>(), userId);
            this.DeleteConferencesRooms(userId);

            this.UpdateDate = DateTime.Now;
            this.UpdateUserId = userId;
        }

        #region Room Names

        /// <summary>Synchronizes the room names.</summary>
        /// <param name="roomNames">The room names.</param>
        /// <param name="userId">The user identifier.</param>
        private void SynchronizeRoomNames(List<RoomName> roomNames, int userId)
        {
            if (this.RoomNames == null)
            {
                this.RoomNames = new List<RoomName>();
            }

            this.DeleteRoomNames(roomNames, userId);

            if (roomNames?.Any() != true)
            {
                return;
            }

            // Create or update
            foreach (var roomName in roomNames)
            {
                var roomNameDb = this.RoomNames.FirstOrDefault(d => d.Language.Code == roomName.Language.Code);
                if (roomNameDb != null)
                {
                    roomNameDb.Update(roomName);
                }
                else
                {
                    this.CreateRoomName(roomName);
                }
            }
        }

        /// <summary>Deletes the room names.</summary>
        /// <param name="newRoomNames">The new room names.</param>
        /// <param name="userId">The user identifier.</param>
        private void DeleteRoomNames(List<RoomName> newRoomNames, int userId)
        {
            var roomNamesToDelete = this.RoomNames.Where(db => newRoomNames?.Select(d => d.Language.Code)?.Contains(db.Language.Code) == false && !db.IsDeleted).ToList();
            foreach (var roomNameToDelete in roomNamesToDelete)
            {
                roomNameToDelete.Delete(userId);
            }
        }

        /// <summary>Creates the name of the room.</summary>
        /// <param name="roomName">Name of the room.</param>
        private void CreateRoomName(RoomName roomName)
        {
            this.RoomNames.Add(roomName);
        }

        #endregion

        #region Conferences

        /// <summary>Deletes the conferences rooms.</summary>
        /// <param name="userId">The user identifier.</param>
        private void DeleteConferencesRooms(int userId)
        {
            if (this.Conferences?.Any() != true)
            {
                return;
            }

            foreach (var conference in this.Conferences.Where(c => !c.IsDeleted))
            {
                conference.DeleteRoom(userId);
            }
        }

        #endregion

        #region Validations

        /// <summary>Returns true if ... is valid.</summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.</returns>
        public override bool IsValid()
        {
            this.ValidationResult = new ValidationResult();

            this.ValidateEdition();
            this.ValidateRoomNames();

            return this.ValidationResult.IsValid;
        }

        /// <summary>Validates the edition.</summary>
        public void ValidateEdition()
        {
            if (this.Edition == null)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.Edition), new string[] { "Edition" }));
            }
        }

        public void ValidateRoomNames()
        {
            if (this.RoomNames?.Any() != true)
            {
                return;
            }

            foreach (var roomName in this.RoomNames?.Where(d => !d.IsValid())?.ToList())
            {
                this.ValidationResult.Add(roomName.ValidationResult);
            }
        }

        #endregion
    }
}