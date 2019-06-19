using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.CrossCutting.SystemParameter
{
    public class SystemParameterMap : EntityTypeConfiguration<SystemParameter>
    {
        public SystemParameterMap()
        {
            HasKey(t => t.Id);

            this.Property(t => t.Code)
                 .HasColumnAnnotation(
                        IndexAnnotation.AnnotationName,
                        new IndexAnnotation(new IndexAttribute())
                  );

            this.Property(t => t.Description)
                .HasMaxLength(SystemParameter.DescriptionMaxLength);

            this.Property(t => t.Value)
                .HasMaxLength(SystemParameter.ValueMaxLength);

            this.ToTable("SystemParameter");
        }
    }
}
