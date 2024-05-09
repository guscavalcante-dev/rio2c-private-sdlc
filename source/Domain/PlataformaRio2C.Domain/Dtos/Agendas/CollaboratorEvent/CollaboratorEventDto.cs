using System;

namespace PlataformaRio2C.Domain.Dtos.Agendas
{
    public class CollaboratorEventDto
    {
        public string Local { get; set; }
        public string Horario { get; set; }
        public DateTime? Data { get; set; }
        public string Nome { get; set; }
        public string Descritivo { get; set; }
    }
}
