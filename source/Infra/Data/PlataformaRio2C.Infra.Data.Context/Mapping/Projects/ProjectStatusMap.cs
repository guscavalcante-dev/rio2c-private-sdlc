using PlataformaRio2C.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    public class ProjectStatusMap : EntityTypeConfiguration<ProjectStatus>
    {
        public ProjectStatusMap()
        {
            this.Ignore(p => p.Uid);

            this.Property(t => t.Code)
               .HasMaxLength(ProjectStatus.CodeMaxLength)
               .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                                       new IndexAnnotation(
                                                               new IndexAttribute("IX_Code", 2)
                                                               {
                                                                   IsUnique = true
                                                               }
                                                           )
                                                   )
               .IsRequired();

            this.ToTable("ProjectStatus");
        }
    }
}
