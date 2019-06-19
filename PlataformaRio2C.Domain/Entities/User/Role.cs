using System.Collections.Generic;

namespace PlataformaRio2C.Domain.Entities
{
    public class Role : Entity
    {
        public string Name { get; set; }


        public override bool IsValid()
        {
            return true;
        }
    }
}
