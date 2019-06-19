using PlataformaRio2C.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    public class QuizMap : EntityTypeConfiguration<Quiz>
    {
        public QuizMap()
        {

            this.HasRequired(t => t.Event)
                    .WithRequiredPrincipal(e => e.Quiz);
                //.HasForeignKey(d => d.EventId);

            this.ToTable("Quiz");
        }

    }
}
