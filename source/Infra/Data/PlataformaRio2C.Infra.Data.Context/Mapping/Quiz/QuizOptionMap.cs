using PlataformaRio2C.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
