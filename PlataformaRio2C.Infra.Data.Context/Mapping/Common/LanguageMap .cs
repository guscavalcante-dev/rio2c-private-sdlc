using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    public class LanguageMap : EntityTypeConfiguration<Language>
    {
        public LanguageMap()
        {
            this.Property(t => t.Name).IsRequired();
            this.Property(t => t.Code).IsRequired();


            this.ToTable("Language");
        }
    }
}
