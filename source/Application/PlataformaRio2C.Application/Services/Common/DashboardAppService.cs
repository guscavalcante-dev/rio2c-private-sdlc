//using PlataformaRio2C.Application.Interfaces.Services;
//using PlataformaRio2C.Domain.Interfaces;
//using System.Linq;
//using PlataformaRio2C.Application.ViewModels;
//using System;
//using System.Collections.Generic;
//using PlataformaRio2C.Domain.Entities;
//using PlataformaRio2C.Application.Dtos;

//namespace PlataformaRio2C.Application.Services
//{
//    public class DashboardAppService : IDashboardAppService
//    {
//        private readonly IHoldingRepository _holdingRepository;
//        private readonly IPlayerRepository _playerRepository;
//        private readonly IProducerRepository _producerRepository;
//        private readonly IProjectService _projectService;
//        private readonly ICollaboratorPlayerService _collaboratorPlayerService;
//        private readonly ICollaboratorProducerService _collaboratorProducerService;
//        //private readonly IProjectPlayerRepository _projectPlayerRepository;

//        public DashboardAppService(IRepositoryFactory repositoryFactory, IProjectService projectService, ICollaboratorPlayerService collaboratorPlayerService, ICollaboratorProducerService collaboratorProducerService)
//        {
//            _holdingRepository = repositoryFactory.HoldingRepository;
//            _playerRepository = repositoryFactory.PlayerRepository;
//            _producerRepository = repositoryFactory.ProducerRepository;
//            _projectService = projectService;
//            _collaboratorPlayerService = collaboratorPlayerService;
//            _collaboratorProducerService = collaboratorProducerService;
//            //_projectPlayerRepository = repositoryFactory.ProjectPlayerRepository;
//        }
//        public int GetTotalHolding()
//        {
//            return _holdingRepository.GetAllSimple().Count();
//        }

//        public int GetTotalPlayer()
//        {
//            return _playerRepository.GetAllSimple().Count();
//        }

//        public int GetTotalProducer()
//        {
//            return _producerRepository.GetAllSimple().Count();
//        }

//        public int GetTotalProjects()
//        {
//            return _projectService.GetAllSimple().Count();
//        }

//        public IEnumerable<DataItemChartAppViewModel> GetProjetcsSubmissions()
//        {
//            var result = new List<DataItemChartAppViewModel>();

//            //result.Add(new DataItemChartAppViewModel("Em avaliação", "evaluation", _projectPlayerRepository.CountOnEvaluation()));
//            //result.Add(new DataItemChartAppViewModel("Aceito", "accepted", _projectPlayerRepository.CountAccepted()));
//            //result.Add(new DataItemChartAppViewModel("Recusado", "refused", _projectPlayerRepository.CountRejected()));

//            return result;
//        }

//        public IEnumerable<DataItemChartAppViewModel> GetProjetcChart()
//        {
//            var result = new List<DataItemChartAppViewModel>();

//            result.Add(new DataItemChartAppViewModel("Enviados", "accepted", _projectService.CountSent()));
//            result.Add(new DataItemChartAppViewModel("Não enviados", "refused", _projectService.CountUnsent()));

//            return result;
//        }

//        public IEnumerable<DataItemChartAppViewModel> GetCountryPlayer()
//        {
//            var result = new List<DataItemChartAppViewModel>();

//            var entities = _playerRepository.GetAllWithAddress().ToList();

//            if (entities != null && entities.Any())
//            {
//                //var entitiesGroupByCountry = entities.GroupBy(e => e.Address.Country).ToList().Select(e => new GroupCountryDto(e.Key, e.Count())).ToList();
//                //result = ProcessByCountry(entitiesGroupByCountry).ToList();
//            }

//            return result.OrderByDescending(e => e.Number);
//        }

//        public IEnumerable<DataItemChartAppViewModel> GetCountryProducer()
//        {
//            var result = new List<DataItemChartAppViewModel>();

//            var entities = _producerRepository.GetAllWithAddress();

//            if (entities != null && entities.Any())
//            {
//                //var entitiesGroupByCountry = entities.GroupBy(e => e.Address.Country).ToList().Select(e => new GroupCountryDto(e.Key, e.Count())).ToList();
//                //result = ProcessByCountry(entitiesGroupByCountry).ToList();
//            }

//            return result.OrderByDescending(e => e.Number);
//        }

//        private IEnumerable<DataItemChartAppViewModel> ProcessByCountry(IEnumerable<GroupCountryDto> entitiesGroupByCountry)
//        {
//            var result = new List<DataItemChartAppViewModel>();

//            var listSearchGroup = new List<GroupCountrySearchDto>() {
//                new GroupCountrySearchDto() {
//                    Label = "Brasil",
//                    Searches = new string[] { "brasil", "brazil", "brasi", "br"}
//                },
//                new GroupCountrySearchDto() {
//                    Label = "United States",
//                    Searches = new string[] { "us", "usa", "united states", "estados unidos"}
//                },
//                new GroupCountrySearchDto() {
//                    Label = "China",
//                    Searches = new string[] { "china"}
//                },
//                new GroupCountrySearchDto() {
//                    Label = "França",
//                    Searches = new string[] { "frança", "france", "franca" }
//                },
//                new GroupCountrySearchDto() {
//                    Label = "Itália",
//                    Searches = new string[] { "italia", "italy" }
//                },
//                new GroupCountrySearchDto() {
//                    Label = "México",
//                    Searches = new string[] { "méxico", "mexico" }
//                },
//                new GroupCountrySearchDto() {
//                    Label = "Reino unido",
//                    Searches = new string[] { "united kingdom", "reino unido", "uk" }
//                }
//            };

//            var listGroup = new List<GroupCountryDto>();

//            var uninformedGroup = entitiesGroupByCountry.Where(e => string.IsNullOrWhiteSpace(e.Key)).ToList();
//            var uninformedGroupNames = string.Join(", ", uninformedGroup.Select(e => e.Key));
//            var uninformedcountGroup = uninformedGroup.Sum(e => e.Count);
//            if (uninformedGroup.Any())
//            {
//                listGroup.AddRange(uninformedGroup);
//                result.Add(new DataItemChartAppViewModel("- Não informado", null, uninformedcountGroup));
//            }

//            foreach (var itemSearch in listSearchGroup)
//            {
//                var groupbySearch = entitiesGroupByCountry.Where(e => !string.IsNullOrWhiteSpace(e.Key) && itemSearch.Searches.Contains(e.Key.Trim().ToLower())).ToList();

//                if (groupbySearch.Any())
//                {
//                    string groupNames = string.Join(", ", groupbySearch.Select(e => e.Key));

//                    listGroup.AddRange(groupbySearch);
//                    result.Add(new DataItemChartAppViewModel(string.Format("{0} - ({1})", itemSearch.Label, groupNames), null, groupbySearch.Sum(e => e.Count)));
//                }
//            }           


//            foreach (var itemGroupPlayer in entitiesGroupByCountry)
//            {
//                if (listGroup.Contains(itemGroupPlayer))
//                {
//                    continue;
//                }               

//                result.Add(new DataItemChartAppViewModel(itemGroupPlayer.Key, itemGroupPlayer.Key, itemGroupPlayer.Count));
//            }

//            return result.OrderBy(e => e.Label);
//        }


//    }
//}
