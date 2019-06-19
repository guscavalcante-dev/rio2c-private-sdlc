using PlataformaRio2C.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    class QuizAnswerMap : EntityTypeConfiguration<QuizAnswer>
    {
        public QuizAnswerMap()
        {
            this.HasRequired(t => t.Option)
                    .WithRequiredPrincipal(e => e.Answer);
                    //.HasForeignKey(d => d.OptionIdId);

            this.ToTable("QuizAnswer");
        }
    }
}
