using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Validation;
using System.Linq;

namespace PlataformaRio2C.Domain.Services
{
    public class RoleLecturerService : Service<RoleLecturer>, IRoleLecturerService
    {
        private readonly IConferenceLecturerRepository _conferenceLecturerRepository;

        public RoleLecturerService(IRoleLecturerRepository repository, IRepositoryFactory repositoryFactory)
            :base(repository)
        {
            _conferenceLecturerRepository = repositoryFactory.ConferenceLecturerRepository;
        }

        public override ValidationResult Delete(RoleLecturer entity)
        {
            var entitiesConferencesLecturer = _conferenceLecturerRepository.GetAll(e => e.RoleLecturerId == entity.Id);
            if (entitiesConferencesLecturer != null && entitiesConferencesLecturer.Any())
            {
                var error = new ValidationError(string.Format("Existem '{0}' palestrante(s) associados(s) a função '{1}'.", entitiesConferencesLecturer.Count(), entity.Titles.FirstOrDefault(e => e.Language.Code == "PtBr").Value), new string[] { "" });
                _validationResult.Add(error);
            }


            return base.Delete(entity);
        }
    }
}
