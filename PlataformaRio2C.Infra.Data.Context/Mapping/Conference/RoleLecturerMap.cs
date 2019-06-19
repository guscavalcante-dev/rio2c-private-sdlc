using PlataformaRio2C.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    public class RoleLecturerMap : EntityTypeConfiguration<RoleLecturer>
    {
        public RoleLecturerMap()
        {
            //this.Property(t => t.Name)
            //    .HasMaxLength(Room.NameMaxLength)
            //    .HasColumnAnnotation(IndexAnnotation.AnnotationName,
            //                            new IndexAnnotation(
            //                                                    new IndexAttribute("IX_Name", 1)
            //                                                    {
            //                                                        IsUnique = true
            //                                                    }
            //                                                )
            //                                        )
            //    .IsRequired();
          

            this.ToTable("RoleLecturer");
        }
    }
}
