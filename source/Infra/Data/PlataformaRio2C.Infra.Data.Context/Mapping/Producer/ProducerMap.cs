//using PlataformaRio2C.Domain.Entities;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.Data.Entity.Infrastructure.Annotations;
//using System.Data.Entity.ModelConfiguration;

//namespace PlataformaRio2C.Infra.Data.Context.Mapping
//{
//    public class ProducerMap : EntityTypeConfiguration<Producer>
//    {
//        public ProducerMap()
//        {
//            this.Ignore(t => t.LintCnpj);


//            this.Property(t => t.Name)
//                .HasMaxLength(Player.NameMaxLength)
//                .IsOptional();


//            this.Property(t => t.CNPJ)             
//              .IsOptional();


//            Property(u => u.Website)
//               .HasMaxLength(Player.WebSiteMaxLength);

//            Property(u => u.SocialMedia)
//               .HasMaxLength(Player.SocialMediaMaxLength);

//            Property(u => u.TradeName)
//               .HasMaxLength(Player.TradeNameMaxLength);

//            //Relationships        

//            this.HasOptional(t => t.Image)
//                .WithMany()
//                .HasForeignKey(d => d.ImageId);

//            this.HasOptional(t => t.Address)
//              .WithMany()
//              .HasForeignKey(d => d.AddressId);


//            this.ToTable("Producer");
//        }
//    }
//}
