using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    class QuizOptionMap : EntityTypeConfiguration<QuizOption>
    {
        public QuizOptionMap()
        {
            this.HasRequired(t => t.Question)
                    .WithMany(e => e.Option)
                    .HasForeignKey(d => d.QuestionId);

            this.ToTable("QuizOption");
        }
    }
}
