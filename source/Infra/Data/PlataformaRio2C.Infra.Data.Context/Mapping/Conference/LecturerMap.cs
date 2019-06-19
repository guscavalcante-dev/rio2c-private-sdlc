using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    public class LecturerMap : EntityTypeConfiguration<Lecturer>
    {
        public LecturerMap()
        {
            this.Property(p => p.Name)
                .HasMaxLength(Lecturer.NameMaxLength);            

            //Relationships
            this.HasOptional(t => t.Image)
              .WithMany()
              .HasForeignKey(d => d.ImageId);

            this.ToTable("Lecturer");
        }
    }
}
