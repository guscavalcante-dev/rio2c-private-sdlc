using PlataformaRio2C.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    public class HoldingMap : EntityTypeConfiguration<Holding>
    {
        public HoldingMap()
        {

            this.Property(t => t.Name)
                .HasMaxLength(Holding.NameMaxLength)                
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                                        new IndexAnnotation(
                                                                new IndexAttribute("IX_Name", 2) {
                                                                    IsUnique = true
                                                                }
                                                            )
                                                    )
                .IsRequired();

            //Relationships
            this.HasOptional(t => t.Image)
                .WithMany()
                .HasForeignKey(d => d.ImageId);

            this.ToTable("Holding");
        }
    }
}
