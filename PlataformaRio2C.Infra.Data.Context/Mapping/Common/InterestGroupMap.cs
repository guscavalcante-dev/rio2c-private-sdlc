using PlataformaRio2C.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    public class InterestGroupMap : EntityTypeConfiguration<InterestGroup>
    {
        public InterestGroupMap()
        {
            this.Property(p => p.Type)
                .HasMaxLength(InterestGroup.TypeMaxLength)
                .IsRequired(); 

            this.Property(t => t.Name)
               .HasMaxLength(InterestGroup.NameMaxLength)
               .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                                       new IndexAnnotation(
                                                               new IndexAttribute("IX_Name", 2)
                                                               {
                                                                   IsUnique = true
                                                               }
                                                           )
                                                   )
               .IsRequired();


            this.ToTable("InterestGroup");
        }
    }
}
