using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace PlataformaRio2C.Domain.Entities
{
    public class Room : Entity
    {       
        public virtual ICollection<RoomName> Names { get; private set; }

        protected Room()
        {

        }       

        public Room(IEnumerable<RoomName> names)
        {
            SetNames(names);
        }       

        public void SetNames(IEnumerable<RoomName> names)
        {
            if (names != null)
            {
                Names = names.ToList();
            }
        }

        public string GetName()
        {
            CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;

            if (currentCulture != null && currentCulture.Name == "pt-BR")
            {
                var t = Names.Where(e => e.Language.Code == "PtBr").Select(e => e.Value).FirstOrDefault();
                return Names.Where(e => e.Language.Code == "PtBr").Select(e => e.Value).FirstOrDefault();
            }
            else
            {
                return Names.Where(e => e.Language.Code == "En").Select(e => e.Value).FirstOrDefault();
            }            
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
