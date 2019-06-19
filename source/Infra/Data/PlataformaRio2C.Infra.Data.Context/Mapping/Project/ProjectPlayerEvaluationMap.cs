using PlataformaRio2C.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    public class ProjectPlayerEvaluationMap : EntityTypeConfiguration<ProjectPlayerEvaluation>
    {
        public ProjectPlayerEvaluationMap()
        {
            this.Ignore(p => p.Uid);            

            this.HasKey(d => d.Id);

            this.Property(t => t.ProjectPlayerId)
               .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                                       new IndexAnnotation(
                                                               new IndexAttribute("IX_ProjectPlayerEvaluationdUser", 0)
                                                               {
                                                                   IsUnique = true
                                                               }
                                                           )
                                                   )
               .IsRequired();

            this.Property(t => t.EvaluationUserId)
               .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                                       new IndexAnnotation(
                                                               new IndexAttribute("IX_ProjectPlayerEvaluationdUser", 1)
                                                               {
                                                                   IsUnique = true
                                                               }
                                                           )
                                                   )
               .IsRequired();


            Property(u => u.Reason)
            .HasMaxLength(ProjectPlayerEvaluation.ReasonMaxLength);

            //Relationships          

            this.HasRequired(t => t.EvaluationdUser)
               .WithMany()
               .HasForeignKey(d => d.EvaluationUserId);

            this.HasRequired(t => t.Status)
              .WithMany()
              .HasForeignKey(d => d.StatusId);

            this.ToTable("ProjectPlayerEvaluation");
        }
    }
}
