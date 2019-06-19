using PlataformaRio2C.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
