using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    public class QuizQuestionMap : EntityTypeConfiguration<QuizQuestion>
    {
        public QuizQuestionMap()
        {
            this.HasRequired(t => t.Quiz)
                    .WithMany(e => e.Question)
                    .HasForeignKey(d => d.QuizId);

            this.ToTable("QuizQuestion");
        }
    }
}
