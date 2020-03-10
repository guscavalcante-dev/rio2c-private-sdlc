//using HtmlAgilityPack;
//using OfficeOpenXml;
//using OfficeOpenXml.Style;
//using PlataformaRio2C.Application.Interfaces.Services;
//using PlataformaRio2C.Application.ViewModels;
//using PlataformaRio2C.Domain.Entities;
//using PlataformaRio2C.Domain.Enums;
//using PlataformaRio2C.Domain.Interfaces;
//using PlataformaRio2C.Infra.CrossCutting.Resources;
//using PlataformaRio2C.Infra.CrossCutting.SystemParameter;
//using PlataformaRio2C.Infra.Data.Context.Interfaces;
//using System;
//using System.Collections.Generic;
//using System.Drawing;
//using System.Dynamic;
//using System.Globalization;
//using System.Linq;
//using System.Threading;

//namespace PlataformaRio2C.Application.Services
//{
//    public class NegotiationAppService : AppService<Infra.Data.Context.PlataformaRio2CContext, Negotiation, NegotiationAppViewModel, NegotiationAppViewModel, NegotiationAppViewModel, NegotiationAppViewModel>, INegotiationAppService
//    {
//        private readonly INegotiationConfigRepository _negotiationConfigRepository;
//        //private readonly IProjectPlayerRepository _projectPlayerRepository;
//        private readonly IConferenceRepository _conferenceRepository;
//        private readonly IPlayerRepository _playerRepository;
//        private readonly IProducerRepository _producerRepository;
//        private readonly IProjectService _projectService;
//        private readonly IRoomRepository _roomRepository;
//        private readonly IEmailAppService _emailAppService;

//        private readonly INegotiationRepository _negotiationRepository;

//        private readonly ILogisticsRepository _logisticsRepository;
//        private IList<Negotiation> _negociations = new List<Negotiation>();
//        private IList<NegotiationConfig> _datesConfigs;
//        //private IList<ProjectPlayer> _projectSubmissions = new List<ProjectPlayer>();
//        private IList<Logistics> _logistics;
//        private IList<Conference> _conferences;

//        //private IList<ProjectPlayer> _projectSubmissionsError = new List<ProjectPlayer>();

//        #region ctor
//        public NegotiationAppService(INegotiationService service, IUnitOfWork unitOfWork, IRepositoryFactory repositoryFactory, IProjectService projectService, IEmailAppService emailAppService)
//           : base(unitOfWork, service)
//        {
//            _negotiationConfigRepository = repositoryFactory.NegotiationConfigRepository;
//            //_projectPlayerRepository = repositoryFactory.ProjectPlayerRepository;
//            _conferenceRepository = repositoryFactory.ConferenceRepository;
//            _logisticsRepository = repositoryFactory.LogisticRepository;
//            _playerRepository = repositoryFactory.PlayerRepository;
//            _projectService = projectService;
//            _roomRepository = repositoryFactory.RoomRepository;
//            _negotiationRepository = repositoryFactory.NegotiationRepository;
//            _emailAppService = emailAppService;
//            _producerRepository = repositoryFactory.ProducerRepository;
//        }
//        #endregion

//        #region public methods

//        public AppValidationResult ProcessScheduleOneToOneMeetings(int userId)
//        {
//            BeginTransaction();

//            GenerateScheduleOneToOneMeetings(userId);

//            ValidationResult.Add(service.CreateAll(_negociations));

//            if (ValidationResult.IsValid)
//            {
//                Commit();
//            }

//            //ValidationResult.Data = new NegotiationResultProcessAppViewModel(_negociations, _projectSubmissionsError);

//            return ValidationResult;
//        }

//        public NegotiationResultProcessAppViewModel ResultProcessScheduleOneToOneMeetings(int userId)
//        {
//            var _negociations = service.GetAll().ToList();
//            //if (_negociations != null && _negociations.Any())
//            //{
//            //    _projectSubmissionsError = GetProjectSubmissionsError(_negociations).ToList();
//            //    return new NegotiationResultProcessAppViewModel(_negociations, _projectSubmissionsError);
//            //}

//            return null;
//        }

//        //private IEnumerable<ProjectPlayer> GetProjectSubmissionsError(IEnumerable<Negotiation> negotiations)
//        //{
//        //    var result = new List<ProjectPlayer>();

//        //    _projectSubmissions = _projectPlayerRepository.GetAllForProccessSchedule().ToList();

//        //    if (_projectSubmissions != null && _projectSubmissions.Any())
//        //    {
//        //        foreach (var projectSub in _projectSubmissions)
//        //        {
//        //            if (!negotiations.Any(n => n.PlayerId == projectSub.PlayerId && n.ProjectId == projectSub.ProjectId))
//        //            {
//        //                result.Add(projectSub);
//        //            }
//        //        }
//        //    }


//        //    return result;
//        //}

//        public GroupDateNegotiationAppViewModel GetNegotiations(int userId)
//        {
//            var _negociations = service.GetAll().ToList();
//            if (_negociations != null && _negociations.Any())
//            {
//                return new GroupDateNegotiationAppViewModel(NegotiationAppViewModel.MapList(_negociations));
//            }

//            return null;
//        }

//        public AppValidationResult GenerateTemp(int userId)
//        {
//            GenerateScheduleOneToOneMeetings(userId);

//            //var viewModelResult = new NegotiationResultProcessAppViewModel(_negociations, _projectSubmissionsError);

//            dynamic result = new ExpandoObject();
//            //result.NumberScheduledNegotiations = viewModelResult.NumberScheduledNegotiations;
//            //result.NumberUnscheduledNegotiations = viewModelResult.NumberUnscheduledNegotiations;
//            //result.DateProcess = viewModelResult.DateProcess;
//            //result.UnscheduledNegotiations = viewModelResult.UnscheduledNegotiations;
//            result.Dates = new GroupDateNegotiationAppViewModel(NegotiationAppViewModel.MapList(_negociations));

//            ValidationResult.Data = result;

//            return ValidationResult;
//        }

//        public IEnumerable<NegotiationAppViewModel> GetTempNegociations(int userId)
//        {
//            GenerateScheduleOneToOneMeetings(userId);

//            return NegotiationAppViewModel.MapList(_negociations);
//        }

//        public GroupDateNegotiationAppViewModel GetGroupTempNegotiations(int userId)
//        {
//            return new GroupDateNegotiationAppViewModel(GetTempNegociations(userId));
//        }

//        public AppValidationResult GenerateScheduleOneToOneMeetings(int userId)
//        {
//            _datesConfigs = _negotiationConfigRepository.GetAll().ToList();
//            IList<Negotiation> reservationForNegotiation = new List<Negotiation>();
//            IList<Negotiation> negociations = new List<Negotiation>();

//            if (_datesConfigs != null && _datesConfigs.Any())
//            {
//                reservationForNegotiation = ProcessConfiguration(_datesConfigs);

//                //_projectSubmissions = _projectPlayerRepository.GetAllForProccessSchedule().ToList();

//                //if (_projectSubmissions != null && _projectSubmissions.Any())
//                //{
//                //    _logistics = _logisticsRepository.GetAll().ToList();
//                //    _conferences = _conferenceRepository.GetAllBySchedule().ToList();

//                //    ProcessProjectSubmissions(reservationForNegotiation, _projectSubmissions);
//                //}
//            }

//            //var t = reservationForNegotiation.Where(e => e.Evaluation != null).ToList();

//            //_negociations = reservationForNegotiation.Where(e => e.Evaluation != null).ToList();

//            return ValidationResult;
//        }

//        public AppValidationResult Delete(int userId, Guid uid)
//        {
//            BeginTransaction();
//            var entity = service.Get(uid);
//            if (entity != null)
//            {
//                ValidationResult.Add(service.Delete(entity));
//            }

//            if (ValidationResult.IsValid)
//            {
//                Commit();
//            }

//            return ValidationResult;
//        }



//        #endregion

//        #region private methods
//        private IList<Negotiation> ProcessConfiguration(IEnumerable<NegotiationConfig> datesConfigs)
//        {
//            IList<Negotiation> negociations = new List<Negotiation>();
//            int numberSlot = 1;
//            //cada dia
//            foreach (var dateConfig in datesConfigs)
//            {

//                TimeSpan currentTime = dateConfig.StartTime;

//                //cada slot
//                for (int iSlot = 0; iSlot < dateConfig.RoudsFirstTurn; iSlot++)
//                {
//                    if (currentTime.Add(dateConfig.TimeOfEachRound) <= dateConfig.EndTime)
//                    {
//                        //cada sala
//                        foreach (var roomConfig in dateConfig.Rooms)
//                        {
//                            //cada mesa automatica
//                            for (int iTable = 0; iTable < roomConfig.CountAutomaticTables; iTable++)
//                            {
//                                negociations.Add(CreateNegotiation(dateConfig, roomConfig, numberSlot, iTable, currentTime, NegotiationTypeCodes.Automatic));
//                            }
//                        }
//                    }

//                    currentTime = currentTime.Add(dateConfig.TimeOfEachRound.Add(dateConfig.TimeIntervalBetweenRound));
//                    numberSlot++;
//                }

//                currentTime = currentTime.Add(dateConfig.TimeIntervalBetweenTurn);

//                //cada slot
//                for (int iSlot = 0; iSlot < dateConfig.RoundsSecondTurn; iSlot++)
//                {

//                    if (currentTime.Add(dateConfig.TimeOfEachRound) <= dateConfig.EndTime)
//                    {
//                        //cada sala
//                        foreach (var roomConfig in dateConfig.Rooms)
//                        {
//                            //cada mesa automatica
//                            for (int iTable = 0; iTable < roomConfig.CountAutomaticTables; iTable++)
//                            {
//                                negociations.Add(CreateNegotiation(dateConfig, roomConfig, numberSlot, iTable, currentTime, NegotiationTypeCodes.Automatic));
//                            }
//                        }
//                    }

//                    currentTime = currentTime.Add(dateConfig.TimeOfEachRound.Add(dateConfig.TimeIntervalBetweenRound));
//                    numberSlot++;
//                }
//            }

//            //var tt = negociations.LastOrDefault();
//            return negociations;
//        }
//        //private void ProcessProjectSubmissions(IEnumerable<Negotiation> reservationForNegotiation, IEnumerable<ProjectPlayer> projectSubmissions)
//        //{
//        //    projectSubmissions = ProcessProjectSubmissionsByAvailability(reservationForNegotiation, projectSubmissions);
//        //    var submissionGroupPlayer = projectSubmissions.GroupBy(e => e.PlayerId);
//        //    var reservationsGroupSlot = reservationForNegotiation.GroupBy(e => e.RoundNumber);


//        //    foreach (var submissionPlayerItem in submissionGroupPlayer)
//        //    {
//        //        int currentSlot = 1;



//        //        foreach (var itemSub in submissionPlayerItem.ToList())
//        //        {
//        //            var slotsExpection = GetSlotsExpection(reservationForNegotiation, itemSub);
//        //            //var negociation = reservationForNegotiation.FirstOrDefault(e => e.Evaluation == null && e.ProjectId != itemSub.ProjectId && e.PlayerId != itemSub.PlayerId && !slotsExpection.Contains(e.RoundNumber));
//        //            var possiblesNegociation = reservationForNegotiation.Where(e => e.Evaluation == null && e.ProjectId != itemSub.ProjectId && e.PlayerId != itemSub.PlayerId && !slotsExpection.Contains(e.RoundNumber));

//        //            if (possiblesNegociation != null && possiblesNegociation.Any())
//        //            {
//        //                var dateTeste = possiblesNegociation.Select(e => e.Date).FirstOrDefault();

//        //                var negotiationsInDate = reservationForNegotiation.FirstOrDefault(e => e.Date == dateTeste && e.PlayerId == itemSub.PlayerId);
//        //                if (negotiationsInDate != null)
//        //                {
//        //                    var negotiation = possiblesNegociation.FirstOrDefault(e => e.TableNumber == negotiationsInDate.TableNumber && e.RoomId == negotiationsInDate.RoomId);

//        //                    if (negotiation != null)
//        //                    {
//        //                        negotiation.SetPlayer(itemSub.Player);
//        //                        negotiation.SetProject(itemSub.Project);
//        //                        negotiation.SetSourceEvaluation(itemSub.Evaluation);
//        //                    }
//        //                    else
//        //                    {
//        //                        _projectSubmissionsError.Add(itemSub);
//        //                    }
//        //                }
//        //                else
//        //                {
//        //                    var negotiation = possiblesNegociation.FirstOrDefault();

//        //                    if (negotiation != null)
//        //                    {
//        //                        negotiation.SetPlayer(itemSub.Player);
//        //                        negotiation.SetProject(itemSub.Project);
//        //                        negotiation.SetSourceEvaluation(itemSub.Evaluation);
//        //                    }
//        //                    else
//        //                    {
//        //                        _projectSubmissionsError.Add(itemSub);
//        //                    }
//        //                }
//        //            }
//        //            else
//        //            {
//        //                _projectSubmissionsError.Add(itemSub);
//        //            }





//        //            //if (negociation != null)
//        //            //{
//        //            //    negociation.SetPlayer(itemSub.Player);
//        //            //    negociation.SetProject(itemSub.Project);
//        //            //    negociation.SetSourceEvaluation(itemSub.Evaluation);
//        //            //}
//        //            //else
//        //            //{
//        //            //    _projectSubmissionsError.Add(itemSub);
//        //            //}
//        //            currentSlot++;
//        //        }
//        //    }
//        //}

//        //private int GetTableIndicatedThePlayer(int i, IEnumerable<Negotiation> reservationForNegotiation, ProjectPlayer itemSub, int slot)
//        //{
//        //    int result = i;

//        //    var negotiationSchedule = reservationForNegotiation.FirstOrDefault(e => e.RoundNumber == (slot - 1));
//        //    if (negotiationSchedule != null)
//        //    {
//        //        return negotiationSchedule.TableNumber;
//        //    }

//        //    return result;
//        //}


//        //private IList<ProjectPlayer> ProcessProjectSubmissionsByAvailability(IEnumerable<Negotiation> reservationForNegotiation, IEnumerable<ProjectPlayer> projectSubmissions)
//        //{
//        //    IList<Tuple<ProjectPlayer, int>> listsubmissionAndSlotEx = new List<Tuple<ProjectPlayer, int>>();

//        //    if (projectSubmissions != null && projectSubmissions.Any())
//        //    {
//        //        foreach (var itemSub in projectSubmissions)
//        //        {
//        //            var slotsExpection = GetSlotsExpection(reservationForNegotiation, itemSub);
//        //            listsubmissionAndSlotEx.Add(new Tuple<ProjectPlayer, int>(itemSub, slotsExpection.Count()));
//        //        }

//        //        return listsubmissionAndSlotEx.GroupBy(e => e.Item1.PlayerId).OrderByDescending(e => e.Count()).ThenByDescending(e => e.First().Item2).SelectMany(e => e.ToList()).Select(e => e.Item1).ToList();
//        //    }

//        //    return new List<ProjectPlayer>();
//        //}

//        //private IEnumerable<int> GetSlotsExpection(IEnumerable<Negotiation> reservationForNegotiation, ProjectPlayer submission)
//        //{
//        //    List<int> result = new List<int>();

//        //    //var slotsExpectionByProjectPlayer = reservationForNegotiation.Where(e => e.PlayerId == submission.PlayerId || e.ProjectId == submission.ProjectId || (e.Project != null && e.Project.ProducerId == submission.Project.ProducerId)).Select(e => e.RoundNumber).Distinct().ToList();
//        //    //result.AddRange(slotsExpectionByProjectPlayer);

//        //    result.AddRange(GetSlotsExpectionByLogistc(reservationForNegotiation, submission));
//        //    result.AddRange(GetSlotsExpectionByConference(reservationForNegotiation, submission));

//        //    return result;
//        //}
//        //private IEnumerable<int> GetSlotsExpectionByLogistc(IEnumerable<Negotiation> reservationForNegotiation, ProjectPlayer submission)
//        //{
//        //    List<int> result = new List<int>();

//        //    result.AddRange(GetSlotsExpectionByLogistcPlayer(reservationForNegotiation, submission));
//        //    result.AddRange(GetSlotsExpectionByLogistcProducer(reservationForNegotiation, submission));

//        //    return result;
//        //}
//        //private IEnumerable<int> GetSlotsExpectionByLogistcProducer(IEnumerable<Negotiation> reservationForNegotiation, ProjectPlayer submission)
//        //{
//        //    List<int> result = new List<int>();

//        //    //if (submission.Project != null && submission.Project.Producer != null)
//        //    //{
//        //    //    //logistic
//        //    //    //var logisticsProducers = _logistics.Where(e => (e.Collaborator.ProducersEvents.Any() && e.Collaborator.ProducersEvents.Select(p => p.ProducerId).Any(p => p == submission.Project.ProducerId)));
//        //    //    //if (logisticsProducers != null && logisticsProducers.Any())
//        //    //    //{
//        //    //    //    List<Tuple<DateTime?, TimeSpan, DateTime?, TimeSpan>> dateTimesLogistics = new List<Tuple<DateTime?, TimeSpan, DateTime?, TimeSpan>>();

//        //    //    //    foreach (var logistic in logisticsProducers)
//        //    //    //    {
//        //    //    //        dateTimesLogistics.Add(new Tuple<DateTime?, TimeSpan, DateTime?, TimeSpan>(logistic.ArrivalDate, logistic.ArrivalTime.Value.Add(TimeSpan.FromHours(4)), logistic.DepartureDate, logistic.DepartureTime.Value.Add(TimeSpan.FromHours(-4))));
//        //    //    //    }

//        //    //    //    var slotsExpectionByLogistic = reservationForNegotiation
//        //    //    //                                  .Where(reserva => dateTimesLogistics
//        //    //    //                                                  .Any(logistica =>
//        //    //    //                                                  (reserva.Date < logistica.Item1) || (reserva.Date == logistica.Item1 && reserva.StarTime < logistica.Item2)
//        //    //    //                                                  || (reserva.Date > logistica.Item3) || (reserva.Date == logistica.Item3 && reserva.EndTime > logistica.Item4)
//        //    //    //                                                  )
//        //    //    //                                         ).Select(e => e.RoundNumber).Distinct().ToList();

//        //    //    //    result.AddRange(slotsExpectionByLogistic);
//        //    //    //}
//        //    //}

//        //    return result;
//        //}
//        //private IEnumerable<int> GetSlotsExpectionByLogistcPlayer(IEnumerable<Negotiation> reservationForNegotiation, ProjectPlayer submission)
//        //{
//        //    List<int> result = new List<int>();

//        //    //logistic
//        //    //var logisticsPlayers = _logistics.Where(e => (e.Collaborator.Players.Any(p => p.Id == submission.PlayerId)));
//        //    //if (logisticsPlayers != null && logisticsPlayers.Any())
//        //    //{
//        //    //    List<Tuple<DateTime?, TimeSpan, DateTime?, TimeSpan>> dateTimesLogistics = new List<Tuple<DateTime?, TimeSpan, DateTime?, TimeSpan>>();

//        //    //    foreach (var logistic in logisticsPlayers)
//        //    //    {
//        //    //        dateTimesLogistics.Add(new Tuple<DateTime?, TimeSpan, DateTime?, TimeSpan>(logistic.ArrivalDate, logistic.ArrivalTime.Value.Add(TimeSpan.FromHours(4)), logistic.DepartureDate, logistic.DepartureTime.Value.Add(TimeSpan.FromHours(-4))));
//        //    //    }

             

//        //    //    var slotsExpectionByLogistic = reservationForNegotiation
//        //    //                                       .Where(reserva => dateTimesLogistics
//        //    //                                                       .Any(logistica =>
//        //    //                                                       (reserva.Date < logistica.Item1) || (reserva.Date == logistica.Item1 && reserva.StarTime < logistica.Item2)
//        //    //                                                       || (reserva.Date > logistica.Item3) || (reserva.Date == logistica.Item3 && reserva.EndTime > logistica.Item4)
//        //    //                                                       )
//        //    //                                              ).Select(e => e.RoundNumber).Distinct().ToList();

//        //    //    result.AddRange(slotsExpectionByLogistic);
//        //    //}

//        //    return result;
//        //}
//        //private IEnumerable<int> GetSlotsExpectionByConference(IEnumerable<Negotiation> reservationForNegotiation, ProjectPlayer submission)
//        //{
//        //    List<int> result = new List<int>();

//        //    result.AddRange(GetSlotsExpectionByConferencePlayer(reservationForNegotiation, submission));
//        //    result.AddRange(GetSlotsExpectionByConferenceProducer(reservationForNegotiation, submission));

//        //    return result;
//        //}

//        //private IEnumerable<int> GetSlotsExpectionByConferencePlayer(IEnumerable<Negotiation> reservationForNegotiation, ProjectPlayer submission)
//        //{
//        //    List<int> result = new List<int>();

//        //    //var conferencePlayers = _conferences.Where(e => (e.Lecturers.Where(l => l.Collaborator != null).SelectMany(l => l.Collaborator.Players).Any(p => p.Id == submission.PlayerId)));
//        //    //if (conferencePlayers != null && conferencePlayers.Any())
//        //    //{
//        //    //    List<Tuple<DateTime?, TimeSpan, TimeSpan>> dateTimes = new List<Tuple<DateTime?, TimeSpan, TimeSpan>>();

//        //    //    foreach (var conference in conferencePlayers)
//        //    //    {
//        //    //        dateTimes.Add(new Tuple<DateTime?, TimeSpan, TimeSpan>(conference.Date, conference.StartTime.Value.Add(TimeSpan.FromMinutes(-30)), conference.EndTime.Value.Add(TimeSpan.FromMinutes(30))));
//        //    //    }

//        //    //    var slotsExpectionByConference = reservationForNegotiation
//        //    //                                           .Where(r => dateTimes
//        //    //                                                           .Any(c => (
//        //    //                                                                        c.Item1 == r.Date &&
//        //    //                                                                        (
//        //    //                                                                            (r.StarTime > c.Item2 && r.StarTime < c.Item3) ||
//        //    //                                                                            (r.EndTime < c.Item3 && r.EndTime > c.Item2)
//        //    //                                                                         )

//        //    //                                                                      )
//        //    //                                                                )
//        //    //                                                  ).Select(e => e.RoundNumber).Distinct().ToList();

//        //    //    result.AddRange(slotsExpectionByConference);
//        //    //}

//        //    return result;
//        //}

//        //private IEnumerable<int> GetSlotsExpectionByConferenceProducer(IEnumerable<Negotiation> reservationForNegotiation, ProjectPlayer submission)
//        //{
//        //    List<int> result = new List<int>();

//        //    //var conferenceProducer = _conferences.Where(e => (e.Lecturers.Where(l => l.Collaborator != null).SelectMany(l => l.Collaborator.ProducersEvents.Select(p => p.Producer)).Any(p => p.Id == submission.Project.ProducerId)));
//        //    //if (conferenceProducer != null && conferenceProducer.Any())
//        //    //{
//        //    //    List<Tuple<DateTime?, TimeSpan, TimeSpan>> dateTimes = new List<Tuple<DateTime?, TimeSpan, TimeSpan>>();

//        //    //    foreach (var conference in conferenceProducer)
//        //    //    {
//        //    //        dateTimes.Add(new Tuple<DateTime?, TimeSpan, TimeSpan>(conference.Date, conference.StartTime.Value.Add(TimeSpan.FromMinutes(-30)), conference.EndTime.Value.Add(TimeSpan.FromMinutes(30))));
//        //    //    }

//        //    //    var slotsExpectionByConference = reservationForNegotiation
//        //    //                                           .Where(r => dateTimes
//        //    //                                                           .Any(c => (
//        //    //                                                                        c.Item1 == r.Date &&
//        //    //                                                                        (
//        //    //                                                                            (r.StarTime > c.Item2 && r.StarTime < c.Item3) ||
//        //    //                                                                            (r.EndTime < c.Item3 && r.EndTime > c.Item2)
//        //    //                                                                         )

//        //    //                                                                      )
//        //    //                                                                )
//        //    //                                                  ).Select(e => e.RoundNumber).Distinct().ToList();

//        //    //    result.AddRange(slotsExpectionByConference);
//        //    //}

//        //    return result;
//        //}
//        private Negotiation CreateNegotiation(NegotiationConfig dateConfig, NegotiationRoomConfig roomConfig, int numberSlot, int iTable, TimeSpan currentTime, NegotiationTypeCodes type)
//        {
//            var negociation = new Negotiation(dateConfig.Date);
//            negociation.SetRoom(roomConfig.Room);
//            negociation.SetSlotNumber(numberSlot);
//            negociation.SetTable(iTable + 1);
//            negociation.SetStarTime(currentTime);
//            negociation.SetEndTime(currentTime.Add(dateConfig.TimeOfEachRound));
//            return negociation;
//        }


//        #endregion

//        public IEnumerable<string> GetOptionsDates()
//        {
//            var dates = _negotiationConfigRepository.GetAll().ToList().Select(e =>
//            {
//                return e.Date.Value.ToString("dd/MM/yyyy");
//            }).ToList();

//            return dates;
//        }

//        public IEnumerable<RoomAppViewModel> GetOptionsRoomsByDate(string date)
//        {
//            DateTime dt = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
//            var rooms = _negotiationConfigRepository.GetAll(e => e.Date == dt).ToList().Select(e => RoomAppViewModel.MapList(e.Rooms.Select(r => r.Room))).FirstOrDefault();

//            return rooms;
//        }

//        public IEnumerable<string> GetOptionsStartTime(string date)
//        {
//            DateTime dt = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
//            var dateConfig = _negotiationConfigRepository.GetAll(e => e.Date == dt).FirstOrDefault();

//            List<TimeSpan> optionsTimes = new List<TimeSpan>();

//            TimeSpan currentTime = dateConfig.StartTime;
//            int numberSlot = 1;

//            //cada slot
//            for (int iSlot = 0; iSlot < dateConfig.RoudsFirstTurn; iSlot++)
//            {
//                if (currentTime.Add(dateConfig.TimeOfEachRound) <= dateConfig.EndTime)
//                {
//                    optionsTimes.Add(currentTime);
//                }

//                currentTime = currentTime.Add(dateConfig.TimeOfEachRound.Add(dateConfig.TimeIntervalBetweenRound));
//                numberSlot++;
//            }

//            currentTime = currentTime.Add(dateConfig.TimeIntervalBetweenTurn);

//            //cada slot
//            for (int iSlot = 0; iSlot < dateConfig.RoundsSecondTurn; iSlot++)
//            {
//                //cada sala
//                if (currentTime.Add(dateConfig.TimeOfEachRound) <= dateConfig.EndTime)
//                {
//                    optionsTimes.Add(currentTime);
//                }

//                currentTime = currentTime.Add(dateConfig.TimeOfEachRound.Add(dateConfig.TimeIntervalBetweenRound));
//                numberSlot++;
//            }


//            return optionsTimes.Select(e =>
//                 e.ToString("hh':'mm")
//                ).ToList();
//        }

//        public AppValidationResult RegisterNegotiationManual(ManualNegotiationRegisterAppViewModel viewModel)
//        {
//            BeginTransaction();

//            var negotiation = viewModel.MapReverse();

//            negotiation.SetPlayer(_playerRepository.Get(viewModel.Player));
//            negotiation.SetProject(_projectService.Get(viewModel.Project));
//            negotiation.SetRoom(_roomRepository.Get(viewModel.Room));

//            negotiation.SetDate(viewModel.Date);
//            negotiation.SetTable(viewModel.Table);

//            if (viewModel.StarTime != null)
//            {
//                negotiation.SetStarTime(viewModel.StarTime.Value);
//                var tempNegotiation = GetNegotiationExempleByStartTime(viewModel.StarTime.Value, viewModel.Date.Value, negotiation.RoomId);
//                if (tempNegotiation != null)
//                {
//                    negotiation.SetEndTime(tempNegotiation.EndTime);
//                    negotiation.SetSlotNumber(tempNegotiation.RoundNumber);
//                }
//            }

//            var s = service as INegotiationService;
//            ValidationResult.Add(s.CreateManual(negotiation));

//            if (ValidationResult.IsValid)
//            {
//                Commit();
//            }

//            return ValidationResult;
//        }

//        private Negotiation GetNegotiationExempleByStartTime(TimeSpan startTime, DateTime date, int roomId)
//        {
//            var dateConfigs = _negotiationConfigRepository.GetAll();
//            if (dateConfigs != null)
//            {
//                return ProcessConfiguration(dateConfigs).FirstOrDefault(e => e.Date == date && e.StarTime == startTime && e.RoomId == roomId);
//            }

//            return null;
//        }

//        private IList<Negotiation> ProcessConfigurationByDateConfig(NegotiationConfig dateConfig)
//        {
//            IList<Negotiation> negociations = new List<Negotiation>();
//            int numberSlot = 1;

//            TimeSpan currentTime = dateConfig.StartTime;

//            int currentTable = 0;

//            //cada slot
//            for (int iSlot = 0; iSlot < dateConfig.RoudsFirstTurn; iSlot++)
//            {
//                if (currentTime.Add(dateConfig.TimeOfEachRound) <= dateConfig.EndTime)
//                {
//                    //cada sala
//                    foreach (var roomConfig in dateConfig.Rooms)
//                    {
//                        //cada mesa automatica
//                        for (int iTable = 0; iTable < roomConfig.CountAutomaticTables; iTable++)
//                        {
//                            currentTable = iTable;
//                            negociations.Add(CreateNegotiation(dateConfig, roomConfig, numberSlot, iTable, currentTime, NegotiationTypeCodes.Automatic));
//                        }

//                        for (int iTable = 0; iTable < roomConfig.CountManualTables; iTable++)
//                        {
//                            currentTable++;
//                            negociations.Add(CreateNegotiation(dateConfig, roomConfig, numberSlot, currentTable, currentTime, NegotiationTypeCodes.Manual));
//                        }
//                    }
//                }

//                currentTime = currentTime.Add(dateConfig.TimeOfEachRound.Add(dateConfig.TimeIntervalBetweenRound));
//                numberSlot++;
//            }

//            currentTime = currentTime.Add(dateConfig.TimeIntervalBetweenTurn);

//            currentTable = 0;

//            //cada slot
//            for (int iSlot = 0; iSlot < dateConfig.RoundsSecondTurn; iSlot++)
//            {

//                if (currentTime.Add(dateConfig.TimeOfEachRound) <= dateConfig.EndTime)
//                {
//                    //cada sala
//                    foreach (var roomConfig in dateConfig.Rooms)
//                    {
//                        //cada mesa automatica
//                        for (int iTable = 0; iTable < roomConfig.CountAutomaticTables; iTable++)
//                        {
//                            currentTable = iTable;
//                            negociations.Add(CreateNegotiation(dateConfig, roomConfig, numberSlot, iTable, currentTime, NegotiationTypeCodes.Automatic));
//                        }


//                        for (int iTable = 0; iTable < roomConfig.CountManualTables; iTable++)
//                        {
//                            currentTable++;
//                            negociations.Add(CreateNegotiation(dateConfig, roomConfig, numberSlot, currentTable, currentTime, NegotiationTypeCodes.Manual));                            
//                        }
//                    }
//                }

//                currentTime = currentTime.Add(dateConfig.TimeOfEachRound.Add(dateConfig.TimeIntervalBetweenRound));
//                numberSlot++;
//            }

           

//            //var tt = negociations.LastOrDefault();
//            return negociations;
//        }

//        public IEnumerable<int> GetOptionsTables(string date, string startTime, Guid room)
//        {
//            List<int> possibilities = new List<int>();

//            DateTime dt = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
//            var dateConfig = _negotiationConfigRepository.GetAll(e => e.Date == dt).FirstOrDefault();

//            if (dateConfig != null)
//            {
//                TimeSpan st = TimeSpan.ParseExact(startTime, "hh':'mm", CultureInfo.InvariantCulture);
//                var reservationsByConfig = ProcessConfigurationByDateConfig(dateConfig).Where(e => e.StarTime == st && e.Room.Uid == room).Select(e => e.TableNumber).Distinct();
//                var negotiationsShedule = service.GetAll(e => e.Room.Uid == room && e.Date == dt && e.StarTime == st).ToList();

//                foreach (var tableNumber in reservationsByConfig)
//                {
//                    if (!negotiationsShedule.Any(e => e.TableNumber == tableNumber))
//                    {
//                        possibilities.Add(tableNumber);
//                    }
//                }
//            }

//            return possibilities;
//        }

//        public IEnumerable<PlayerOptionAppViewModel> GetPlayers()
//        {
//            var players = _negotiationRepository.GetAllPlayers().ToList();
//            return PlayerOptionAppViewModel.MapList(players).OrderBy(e => e.HoldingName).ThenBy(e => e.Name).ToList();
//        }

//        public IEnumerable<ProducerOptionAppViewModel> GetProducers()
//        {
//            var producers = _negotiationRepository.GetAllProducers().ToList();
//            return ProducerOptionAppViewModel.MapList(producers).OrderBy(e => e.Name).ToList();
//        }

//        public AppValidationResult SendEmailToPlayers(Guid[] uidsPlayers)
//        {
//            if (uidsPlayers != null && uidsPlayers.Any())
//            {
//                //var collaborators = _playerRepository.GetAllCollaborators(e => uidsPlayers.Contains(e.Uid)).ToList().Distinct();

//                //if (collaborators != null && collaborators.Any())
//                //{
//                //    IList<Tuple<bool, CollaboratorItemListAppViewModel, string>> tupleresult = new List<Tuple<bool, CollaboratorItemListAppViewModel, string>>();

//                //    foreach (var collaborator in collaborators)
//                //    {
//                //        var result = SendEmailSchedule(collaborator);

//                //        if (result.IsValid)
//                //        {
//                //            tupleresult.Add(new Tuple<bool, CollaboratorItemListAppViewModel, string>(true, new CollaboratorItemListAppViewModel() { Email = collaborator.User.Email }, ""));
//                //        }
//                //        else
//                //        {
//                //            tupleresult.Add(new Tuple<bool, CollaboratorItemListAppViewModel, string>(false, new CollaboratorItemListAppViewModel() { Email = collaborator.User.Email }, string.Join(",", result.Errors.Select(e => e.Message))));
//                //        }
//                //    }

//                //    ValidationResult.Data = new { SendSuccess = tupleresult.Where(e => e.Item1).Select(e => e.Item2.Email), SendError = tupleresult.Where(e => !e.Item1).Select(e => new { Email = e.Item2.Email, Reason = e.Item3 }) };
//                //}
//            }


//            return ValidationResult;
//        }

//        public AppValidationResult SendEmailToProducers(Guid[] uidsProducers)
//        {
//            if (uidsProducers != null && uidsProducers.Any())
//            {
//                var collaborators = _producerRepository.GetAllCollaborators(e => uidsProducers.Contains(e.Uid)).ToList().Distinct();

//                if (collaborators != null && collaborators.Any())
//                {
//                    IList<Tuple<bool, CollaboratorItemListAppViewModel, string>> tupleresult = new List<Tuple<bool, CollaboratorItemListAppViewModel, string>>();

//                    foreach (var collaborator in collaborators)
//                    {
//                        var result = SendEmailScheduleProducer(collaborator);

//                        if (result.IsValid)
//                        {
//                            tupleresult.Add(new Tuple<bool, CollaboratorItemListAppViewModel, string>(true, new CollaboratorItemListAppViewModel() { Email = collaborator.User.Email }, ""));
//                        }
//                        else
//                        {
//                            tupleresult.Add(new Tuple<bool, CollaboratorItemListAppViewModel, string>(false, new CollaboratorItemListAppViewModel() { Email = collaborator.User.Email }, string.Join(",", result.Errors.Select(e => e.Message))));
//                        }
//                    }

//                    ValidationResult.Data = new { SendSuccess = tupleresult.Where(e => e.Item1).Select(e => e.Item2.Email), SendError = tupleresult.Where(e => !e.Item1).Select(e => new { Email = e.Item2.Email, Reason = e.Item3 }) };
//                }
//            }


//            return ValidationResult;
//        }


//        private AppValidationResult SendEmailScheduleProducer(Collaborator collaborator)
//        {
//            var result = new AppValidationResult();

//            try
//            {
//                var message = CompileHtmlMessageTemplateDefault();
//                message = message.Replace("@{Message}", Texts.EmailProducer);
//                message = message.Replace("@{Name}", collaborator.FirstName);
//                //message = message.Replace("@{urlSistema}", _systemParameterRepository.Get<string>(SystemParameterCodes.SiteUrl));

//                _emailAppService.SeendEmailTemplateDefault(collaborator.User.Email, "Rio2C - Agenda", message);
//            }
//            catch (Exception ex)
//            {
//                result.Add(string.Format("Não foi possível enviar o e-mail. Motivo: {0} ", ex.Message));
//            }

//            return result;
//        }

//        private AppValidationResult SendEmailSchedule(Collaborator collaborator)
//        {
//            var result = new AppValidationResult();

//            try
//            {
//                var message = CompileHtmlMessageTemplateDefault();
//                message = message.Replace("@{Message}", Texts.EmailAgenda);
//                message = message.Replace("@{Name}", collaborator.FirstName);
//                //message = message.Replace("@{urlSistema}", _systemParameterRepository.Get<string>(SystemParameterCodes.SiteUrl));

//                _emailAppService.SeendEmailTemplateDefault(collaborator.User.Email, "Rio2C - Agenda", message);
//            }
//            catch (Exception ex)
//            {
//                result.Add(string.Format("Não foi possível enviar o e-mail. Motivo: {0} ", ex.Message));
//            }

//            return result;
//        }

//        private string CompileHtmlMessageTemplateDefault()
//        {
//            HtmlDocument template = new HtmlDocument();

//            var currentPath = AppDomain.CurrentDomain.BaseDirectory;
//            var pathTemplate = string.Format("{0}/TemplatesEmail/defaultTemplate.html", currentPath);

//            template.Load(pathTemplate);
//            return template.DocumentNode.InnerHtml;
//        }

//        public IEnumerable<NegotiationAppViewModel> GetAllNegotiations()
//        {
//            var _negociations = service.GetAll().ToList();
//            if (_negociations != null && _negociations.Any())
//            {
//                return NegotiationAppViewModel.MapList(_negociations);
//            }

//            return null;
//        }



//        public GroupDateTableNegotiationAppViewModel GetAllNegotiationsGroupTable()
//        {
//            try
//            {
//                var _negociations = service.GetAll().ToList();
//                if (_negociations != null && _negociations.Any())
//                {
//                    return new GroupDateTableNegotiationAppViewModel(NegotiationAppViewModel.MapList(_negociations));
//                }
//            }
//            catch (Exception e)
//            {

//                throw;
//            }
           

//            return null;
//        }

//        public ExcelPackage ExportExcel(string date, string roomName)
//        {
//            CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;

//            ExcelPackage excelFile = new ExcelPackage();

//            ExcelWorksheet worksheet = excelFile.Workbook.Worksheets.Add(roomName);

//            var _negociations = service.GetAll().ToList();
//            if (_negociations != null && _negociations.Any())
//            {
//                var negotiaionsGroupViewModel = new GroupDateTableNegotiationAppViewModel(NegotiationAppViewModel.MapList(_negociations));
//                var negotiationsInDateToReport = negotiaionsGroupViewModel.Dates.Where(e => e.Date == date);
//                var negotiationsInRoomToReport = negotiationsInDateToReport.SelectMany(e => e.Rooms).Where(e => e.Room == roomName);

//                if (negotiationsInRoomToReport != null && negotiationsInRoomToReport.Any())
//                {
//                    var tablesToReport = negotiationsInRoomToReport.SelectMany(e => e.Tables);
//                    var timesToReport = negotiationsInRoomToReport.SelectMany(e => e.Times);

//                    int countTables = tablesToReport.Count();
//                    int countTimes = timesToReport.Count();

//                    int row = 1;
//                    int column = 1;
//                    int offSetRowHeader = 3;

//                    worksheet.Cells[row, column].Value = "Rio2C - Relatório da agenda de rodadas de negócio";
//                    row++;

//                    worksheet.Cells[row, column].Value = string.Format("Data: {0}", date);
//                    row++;

//                    worksheet.Cells[row, column].Value = string.Format("Sala: {0}", roomName);
//                    row++;


//                    column = 1;
//                    worksheet.Cells[row, column++].Value = "Hora";

//                    foreach (var table in tablesToReport)
//                    {
//                        worksheet.Cells[row, column++].Value = string.Format("Mesa {0}", table.Table);
//                    }

//                    row++;
//                    column = 1;

//                    worksheet.Row(row).Height = 130;

//                    foreach (var time in timesToReport)
//                    {
//                        worksheet.Cells[row, column++].Value = string.Format("{0} \r\n {1}", time.Item1, time.Item2);

//                        worksheet.Column(column).Width = 30;

//                        foreach (var table in tablesToReport)
//                        {
//                            foreach (var negotiation in tablesToReport.SelectMany(e => e.Negotiations).Where(e => e.Slot == time.Item3 && e.Table == table.Table))
//                            {
//                                worksheet.Cells[row, column].IsRichText = true;

//                                ExcelRichText richtext = worksheet.Cells[row, column].RichText.Add(string.Format("{0} \n\n", negotiation.Player));
//                                richtext.Bold = true;
//                                richtext.Size = 12;

//                                richtext = worksheet.Cells[row, column].RichText.Add(string.Format("{0} \n\n", negotiation.Project));
//                                richtext.Bold = true;
//                                richtext.Size = 10;

//                                richtext = worksheet.Cells[row, column].RichText.Add(string.Format("{0} \n", negotiation.SocialName));
//                                richtext.Bold = false;
//                                richtext.Size = 11;

//                                worksheet.Column(column).Width = 30;
                                
//                            }

//                            column++;
//                        }

                        

//                        column = 1;
//                        row++;
//                        worksheet.Row(row).Height = 140;
//                    }


//                    //estilizando celulas de conteudo
//                    worksheet.Cells[4, 2, (countTimes + 5), (countTables + 1)].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
//                    worksheet.Cells[4, 2, (countTimes + 5), (countTables + 1)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
//                    worksheet.Cells[4, 2, (countTimes + 5), (countTables + 1)].Style.WrapText = true;


//                    //estilizando primeira linha de titulo  
//                    for (int i = 1; i < 4; i++)
//                    {
//                        worksheet.Cells[i, 1, i, (countTables + 1)].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
//                        worksheet.Cells[i, 1, i, (countTables + 1)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
//                        worksheet.Cells[i, 1, i, (countTables + 1)].Style.Font.Bold = true;
//                        worksheet.Cells[i, 1, i, (countTables + 1)].Style.Border.Top.Style = ExcelBorderStyle.Thin;
//                        worksheet.Cells[i, 1, i, (countTables + 1)].Style.Border.Top.Color.SetColor(Color.FromArgb(0, 0, 0));
//                        worksheet.Cells[i, 1, i, (countTables + 1)].Style.Border.Left.Style = ExcelBorderStyle.Thin;
//                        worksheet.Cells[i, 1, i, (countTables + 1)].Style.Border.Left.Color.SetColor(Color.FromArgb(0, 0, 0));
//                        worksheet.Cells[i, 1, i, (countTables + 1)].Style.Border.Right.Style = ExcelBorderStyle.Thin;
//                        worksheet.Cells[i, 1, i, (countTables + 1)].Style.Border.Right.Color.SetColor(Color.FromArgb(0, 0, 0));
//                        worksheet.Cells[i, 1, i, (countTables + 1)].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
//                        worksheet.Cells[i, 1, i, (countTables + 1)].Style.Border.Bottom.Color.SetColor(Color.FromArgb(0, 0, 0));
//                        worksheet.Cells[i, 1, i, (countTables + 1)].Merge = true;
//                    }
//                    worksheet.Cells[1, 1, 1, (countTables + 1)].Style.Font.Size = 14;
//                    worksheet.Cells[2, 1, 2, (countTables + 1)].Style.Font.Size = 12;
//                    worksheet.Cells[3, 1, 3, (countTables + 1)].Style.Font.Size = 12;


//                    //estilizando primeira linha de cabeçalho dos dados
//                    worksheet.Row(offSetRowHeader + 1).Height = 50;
//                    worksheet.Row(offSetRowHeader + 1).Style.VerticalAlignment = ExcelVerticalAlignment.Center;
//                    worksheet.Row(offSetRowHeader + 1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
//                    worksheet.Row(offSetRowHeader + 1).Style.Border.Top.Style = ExcelBorderStyle.Thin;
//                    worksheet.Row(offSetRowHeader + 1).Style.Border.Top.Color.SetColor(Color.FromArgb(204, 204, 204));
//                    worksheet.Row(offSetRowHeader + 1).Style.Border.Left.Style = ExcelBorderStyle.Thin;
//                    worksheet.Row(offSetRowHeader + 1).Style.Border.Left.Color.SetColor(Color.FromArgb(204, 204, 204));
//                    worksheet.Row(offSetRowHeader + 1).Style.Border.Right.Style = ExcelBorderStyle.Thin;
//                    worksheet.Row(offSetRowHeader + 1).Style.Border.Right.Color.SetColor(Color.FromArgb(204, 204, 204));
//                    worksheet.Row(offSetRowHeader + 1).Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
//                    worksheet.Row(offSetRowHeader + 1).Style.Border.Bottom.Color.SetColor(Color.FromArgb(204, 204, 204));
//                    worksheet.Row(offSetRowHeader + 1).Style.Fill.PatternType = ExcelFillStyle.Solid;
//                    worksheet.Row(offSetRowHeader + 1).Style.Fill.BackgroundColor.SetColor(Color.FromArgb(156, 231, 231));
//                    worksheet.Row(offSetRowHeader + 1).Style.Font.Bold = true;

//                    //estilizando primeira coluna
//                    worksheet.Cells[4, 1, (countTimes + 5), 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
//                    worksheet.Cells[4, 1, (countTimes + 5), 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
//                    worksheet.Cells[4, 1, (countTimes + 5), 1].Style.WrapText = true;
//                    worksheet.Cells[4, 1, (countTimes + 5), 1].Style.Border.Top.Style = ExcelBorderStyle.Thin;
//                    worksheet.Cells[4, 1, (countTimes + 5), 1].Style.Border.Top.Color.SetColor(Color.FromArgb(204, 204, 204));
//                    worksheet.Cells[4, 1, (countTimes + 5), 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
//                    worksheet.Cells[4, 1, (countTimes + 5), 1].Style.Border.Left.Color.SetColor(Color.FromArgb(204, 204, 204));
//                    worksheet.Cells[4, 1, (countTimes + 5), 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
//                    worksheet.Cells[4, 1, (countTimes + 5), 1].Style.Border.Right.Color.SetColor(Color.FromArgb(204, 204, 204));
//                    worksheet.Cells[4, 1, (countTimes + 5), 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
//                    worksheet.Cells[4, 1, (countTimes + 5), 1].Style.Border.Bottom.Color.SetColor(Color.FromArgb(204, 204, 204));
//                    worksheet.Cells[4, 1, (countTimes + 5), 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
//                    worksheet.Cells[4, 1, (countTimes + 5), 1].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(156, 231, 231));
//                    worksheet.Cells[4, 1, (countTimes + 5), 1].Style.Font.Bold = true;
//                }
//            }

//            return excelFile;
//        }
//    }
//}
