using PlataformaRio2C.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    public class ProjectPlayerMap : EntityTypeConfiguration<ProjectPlayer>
    {
        public ProjectPlayerMap()
        {
            this.Ignore(p => p.Uid);

            this.HasKey(p => p.Id);

            this.Ignore(d => d.IdNew);

            //this.HasKey(d => new { d.PlayerId, d.ProjectId });

            //this.Property(t => t.IdNew)
            //    .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.PlayerId)
               .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                                       new IndexAnnotation(
                                                               new IndexAttribute("IX_PlayerProject", 3)
                                                               {
                                                                   IsUnique = true
                                                               }
                                                           )
                                                   )
               .IsRequired();

            this.Property(t => t.ProjectId)
               .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                                       new IndexAnnotation(
                                                               new IndexAttribute("IX_PlayerProject", 4)
                                                               {
                                                                   IsUnique = true
                                                               }
                                                           )
                                                   )
               .IsRequired();



            //Relationships
            this.HasOptional(t => t.Evaluation)
                .WithRequired(t => t.ProjectPlayer);

            //this.HasRequired(t => t.Project)
            //    .WithMany(e => e.PlayersRelated)
            //    .HasForeignKey(d => d.ProjectId);

            this.HasRequired(t => t.Player)
               .WithMany()
               .HasForeignKey(d => d.PlayerId);

            this.HasOptional(t => t.SavedUser)
              .WithMany()
              .HasForeignKey(d => d.SavedUserId);

            this.HasOptional(t => t.SendingUser)
              .WithMany()
              .HasForeignKey(d => d.SendingUserId);           

            this.ToTable("ProjectPlayer");
        }
    }
}
