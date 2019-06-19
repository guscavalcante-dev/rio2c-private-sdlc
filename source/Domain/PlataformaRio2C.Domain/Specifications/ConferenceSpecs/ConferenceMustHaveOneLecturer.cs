using PlataformaRio2C.Domain.Enums;
using PlataformaRio2C.Domain.Interfaces;
using System;
using System.Linq;

namespace PlataformaRio2C.Domain.Entities.Specifications
{
    public class ConferenceMustHaveOneLecturer : ISpecification<Conference>
    {
        public string Target { get { return "Lecturers"; } }
        public ErrorCodes Code { get { return ErrorCodes.IsRequired; } }

        public bool IsSatisfiedBy(Conference entity)
        {
            return  entity.Lecturers != null && entity.Lecturers.Any(e => (e.IsPreRegistered && e.Collaborator != null) || (!e.IsPreRegistered && e.Lecturer != null &&  !string.IsNullOrWhiteSpace(e.Lecturer.Name)));
        }
    }
}
