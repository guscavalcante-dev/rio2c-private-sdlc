using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

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
