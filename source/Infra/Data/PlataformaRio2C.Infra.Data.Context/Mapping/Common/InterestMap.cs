using PlataformaRio2C.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    public class InterestMap : EntityTypeConfiguration<Interest>
    {
        public InterestMap()
        {
            this.Ignore(p => p.InterestGroupUid);

            this.Property(t => t.Name)
              .HasMaxLength(Interest.NameMaxLength)
              .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                                      new IndexAnnotation(
                                                              new IndexAttribute("IX_Name", 2)
                                                              {
                                                                  IsUnique = true
                                                              }
                                                          )
                                                  )
              .IsRequired();


            //Relationships
            this.HasRequired(t => t.InterestGroup)
                .WithMany()
                .HasForeignKey(d => d.InterestGroupId);


            this.ToTable("Interest");
        }
    }
}
