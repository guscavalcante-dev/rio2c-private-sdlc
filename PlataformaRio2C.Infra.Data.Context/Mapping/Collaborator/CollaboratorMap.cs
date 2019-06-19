using PlataformaRio2C.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    public class CollaboratorMap : EntityTypeConfiguration<Collaborator>
    {

        public CollaboratorMap()
        {
            this.Ignore(p => p.PlayerUid);

            this.Property(t => t.UserId)               
               .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                                       new IndexAnnotation(
                                                               new IndexAttribute("IX_UserId", 2)
                                                               {
                                                                   IsUnique = true
                                                               }
                                                           )
                                                   )
               .IsRequired();

            //Relationships
            this.HasOptional(t => t.Image)
              .WithMany()
              .HasForeignKey(d => d.ImageId);

            this.HasOptional(t => t.Player)                
               .WithMany(p => p.CollaboratorsOld)
               .HasForeignKey(d => d.PlayerId);

            this.HasRequired(t => t.User)
              .WithMany()
              .HasForeignKey(d => d.UserId);

            this.HasOptional(t => t.Address)
                .WithMany()
                .HasForeignKey(d => d.AddressId);

            this.HasMany(t => t.Players)
                .WithMany(p => p.Collaborators)
                .Map(cs =>
                {
                   cs.MapLeftKey("CollaboratorId");
                   cs.MapRightKey("PlayerId");                    
                });

            //this.HasOptional(t => t.Speaker)
            //    .WithMany()
            //    .HasForeignKey(d => d.SpeakerId);


            this.ToTable("Collaborator");
        }
    }
}
