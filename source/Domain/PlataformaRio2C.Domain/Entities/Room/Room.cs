// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-27-2019
// ***********************************************************************
// <copyright file="Room.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>Room</summary>
    public class Room : Entity
    {       
        public int EditionId { get; private set; }

        public virtual Edition Edition { get; private set; }

        public virtual ICollection<RoomName> RoomNames { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="Room"/> class.</summary>
        protected Room()
        {
        }       

        //public Room(IEnumerable<RoomName> names)
        //{
        //    SetNames(names);
        //}       

        //public void SetNames(IEnumerable<RoomName> names)
        //{
        //    if (names != null)
        //    {
        //        Names = names.ToList();
        //    }
        //}

        //public string GetName()
        //{
        //    CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;

        //    if (currentCulture != null && currentCulture.Name == "pt-BR")
        //    {
        //        var t = Names.Where(e => e.Language.Code == "PtBr").Select(e => e.Value).FirstOrDefault();
        //        return Names.Where(e => e.Language.Code == "PtBr").Select(e => e.Value).FirstOrDefault();
        //    }
        //    else
        //    {
        //        return Names.Where(e => e.Language.Code == "En").Select(e => e.Value).FirstOrDefault();
        //    }            
        //}

        public override bool IsValid()
        {
            return true;
        }
    }
}