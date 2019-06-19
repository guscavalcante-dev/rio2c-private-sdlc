using PlataformaRio2C.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    public class RoomMap : EntityTypeConfiguration<Room>
    {
        public RoomMap()
        {
            //this.Ignore(t => t.Name);

            this.ToTable("Room");
        }
    }
}
