using PlataformaRio2C.Application.Interfaces.Services;
using PlataformaRio2C.Application.ViewModels;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.CrossCutting.SystemParameter;
using PlataformaRio2C.Infra.Data.Context.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PlataformaRio2C.Application.Services
{
    public class ScheduleAppService : AppService<Infra.Data.Context.PlataformaRio2CContext, Negotiation, NegotiationAppViewModel, NegotiationAppViewModel, NegotiationAppViewModel, NegotiationAppViewModel>, IScheduleAppService
    {
        private readonly ISystemParameterRepository _systemParameterRepository;
        private readonly INegotiationRepository _negotiationRepository;
        private readonly IConferenceRepository _conferenceRepository;
        private readonly ICollaboratorRepository _collaboratorRepository;
        private readonly IPlayerRepository _playerRepository;

        #region ctor

        public ScheduleAppService(INegotiationService service, IUnitOfWork unitOfWork, IRepositoryFactory repositoryFactory, ISystemParameterRepository systemParameterRepository)
          : base(unitOfWork, service)
        {
            _systemParameterRepository = systemParameterRepository;
            _negotiationRepository = repositoryFactory.NegotiationRepository;
            _conferenceRepository = repositoryFactory.ConferenceRepository;
            _collaboratorRepository = repositoryFactory.CollaboratorRepository;
            _playerRepository = repositoryFactory.PlayerRepository;
        }

        #endregion
        
        #region Public methods
        public bool ScheduleIsEnable()
        {
            return _systemParameterRepository.Get<bool>(SystemParameterCodes.ScheduleShowViewInSystem);
        }

        public IEnumerable<ScheduleDayAppViewModel> GetSchedulePlayer(int userId)
        {
            List<ScheduleDayAppViewModel> result = new List<ScheduleDayAppViewModel>();

            //var collaborator = _collaboratorRepository.GetBySchedule(e => e.UserId == userId);
            //if (collaborator != null && collaborator.Players != null && collaborator.Players.Any())
            //{
            //    result.AddRange(GetConferencesByPlayer(collaborator));

            //    result.AddRange(GetNegotiationsByPlayer(collaborator));

            //    result = result.GroupBy(e => e.Date).Select(e => new ScheduleDayAppViewModel
            //    {
            //        Date = e.Key,
            //        DayOfWeek = e.Select(i => i.DayOfWeek).FirstOrDefault(),
            //        Items = e.SelectMany(i => i.Items)
            //    }).OrderBy(e => e.Date).ToList();                
            //}

            return result;
        }

        public IEnumerable<ScheduleDayAppViewModel> GetScheduleProducer(int userId)
        {

            List<ScheduleDayAppViewModel> result = new List<ScheduleDayAppViewModel>();

            //var collaborator = _collaboratorRepository.GetBySchedule(e => e.UserId == userId);
            //if (collaborator != null && collaborator.ProducersEvents != null && collaborator.ProducersEvents.Any())
            //{
            //    result.AddRange(GetConferencesByProducer(collaborator));

            //    result.AddRange(GetNegotiationsByProducer(collaborator));

            //    result = result.GroupBy(e => e.Date).Select(e => new ScheduleDayAppViewModel
            //    {
            //        Date = e.Key,
            //        DayOfWeek = e.Select(i => i.DayOfWeek).FirstOrDefault(),
            //        Items = e.SelectMany(i => i.Items)
            //    }).OrderBy(e => e.Date).ToList();
            //}


            return result;
        }

        public IEnumerable<ScheduleDayAppViewModel> GetDays()
        {
            var dias = new List<DateTime>() {
                new DateTime(2019, 04, 23),
                new DateTime(2019, 04, 24),
                new DateTime(2019, 04, 25),
                new DateTime(2019, 04, 26),
                new DateTime(2019, 04, 27),
                new DateTime(2019, 04, 28)
            };

            foreach (var item in dias)
            {
                yield return new ScheduleDayAppViewModel(item);
            }
        }

        public IEnumerable<ScheduleDayAppViewModel> GetComplete(Guid uidCollaborator)
        {
            List<ScheduleDayAppViewModel> result = new List<ScheduleDayAppViewModel>();

            var collaborator = _collaboratorRepository.GetAll(e => e.Uid == uidCollaborator).FirstOrDefault();

            if (collaborator != null)
            {
                result.AddRange(GetSchedulePlayer(collaborator.Id));
                result.AddRange(GetScheduleProducer(collaborator.Id));

                result = result.GroupBy(e => e.Date).Select(e => new ScheduleDayAppViewModel
                {
                    Date = e.Key,
                    DayOfWeek = e.Select(i => i.DayOfWeek).FirstOrDefault(),
                    Items = e.SelectMany(i => i.Items)
                }).OrderBy(e => e.Date).ToList();
            }

            return result;
        }

        public IEnumerable<ScheduleDayAppViewModel> GetSchedulePlayer(Guid uidCollaborator)
        {
            List<ScheduleDayAppViewModel> result = new List<ScheduleDayAppViewModel>();

            var collaborator = _collaboratorRepository.GetAll(e => e.Uid == uidCollaborator).FirstOrDefault();

            if (collaborator != null)
            {
                result.AddRange(GetSchedulePlayer(collaborator.Id));              
            }

            return result;
        }

        public IEnumerable<ScheduleDayAppViewModel> GetScheduleProducer(Guid uidCollaborator)
        {
            List<ScheduleDayAppViewModel> result = new List<ScheduleDayAppViewModel>();

            var collaborator = _collaboratorRepository.GetAll(e => e.Uid == uidCollaborator).FirstOrDefault();

            if (collaborator != null)
            {
              
                result.AddRange(GetScheduleProducer(collaborator.Id));               
            }

            return result;
        }
        #endregion

        #region private methods
        private IEnumerable<ScheduleDayAppViewModel> GetConferencesByPlayer(Collaborator collaborator)
        {
            List<ScheduleDayAppViewModel> result = new List<ScheduleDayAppViewModel>();

            //var collaboratorsIds = collaborator.Players.SelectMany(e => e.Collaborators.Select(c => c.Id)).ToList();

            //var conferences = _conferenceRepository.GetAllBySchedule(e => e.Lecturers.Any(l => l.CollaboratorId != null && collaboratorsIds.Contains(l.CollaboratorId.Value))).ToList();

            //result.AddRange(ScheduleDayAppViewModel.GetByConference(conferences.Distinct()));

            return result;
        }

        private IEnumerable<ScheduleDayAppViewModel> GetConferencesByProducer(Collaborator collaborator)
        {
            List<ScheduleDayAppViewModel> result = new List<ScheduleDayAppViewModel>();

            //var collaboratorsIds = collaborator.ProducersEvents.Select(e => e.Producer).SelectMany(e => e.EventsCollaborators.Select(ev => ev.CollaboratorId)).ToList();

            //var conferences = _conferenceRepository.GetAllBySchedule(e => e.Lecturers.Any(l => l.CollaboratorId != null && collaboratorsIds.Contains(l.CollaboratorId.Value))).ToList();

            //result.AddRange(ScheduleDayAppViewModel.GetByConference(conferences.Distinct()));

            return result;
        }

        private IEnumerable<ScheduleDayAppViewModel> GetNegotiationsByPlayer(Collaborator collaborator)
        {
            //var playersIds = collaborator.Players.Select(e => e.Id);

            //var negotiations = _negotiationRepository.GetAllBySchedule(n => playersIds.Contains(n.PlayerId)).OrderBy(e => e.Date).ToList();

            //if (negotiations != null && negotiations.Any())
            //{
            //    return ScheduleDayAppViewModel.GetByNegotiations(negotiations);
            //}

            return new List<ScheduleDayAppViewModel>();
        }

        private IEnumerable<ScheduleDayAppViewModel> GetNegotiationsByProducer(Collaborator collaborator)
        {
            //var producerIds = collaborator.ProducersEvents.Select(e => e.ProducerId).Distinct();

            //var negotiations = _negotiationRepository.GetAllBySchedule(n => producerIds.Contains(n.Project.ProducerId)).OrderBy(e => e.Date).ToList();

            //if (negotiations != null && negotiations.Any())
            //{
            //    return ScheduleDayAppViewModel.GetByNegotiations(negotiations);
            //}

            return new List<ScheduleDayAppViewModel>();
        }

        
        #endregion
    }
}
