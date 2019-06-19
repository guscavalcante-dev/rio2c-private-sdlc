using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    public class ImageFileMap : EntityTypeConfiguration<ImageFile>
    {
        public ImageFileMap()
        {
            this.Property(t => t.Uid).IsRequired();
            this.Property(t => t.CreationDate).IsRequired();
            this.Property(t => t.File).IsRequired();
            this.Property(t => t.FileName).HasMaxLength(ImageFile.FileNameMaxLength).IsRequired();
            this.Property(t => t.ContentType).HasMaxLength(ImageFile.ContentTypeLength).IsRequired();
            this.Property(t => t.ContentLength).IsRequired();

            this.ToTable("ImageFile");
        }
    }
}
