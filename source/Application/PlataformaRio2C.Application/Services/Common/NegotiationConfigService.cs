using PlataformaRio2C.Application.Interfaces.Services;
using PlataformaRio2C.Application.ViewModels;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context;
using PlataformaRio2C.Infra.Data.Context.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PlataformaRio2C.Application.Services
{
    public class NegotiationConfigService : AppService<PlataformaRio2CContext, Domain.Entities.NegotiationConfig, NegotiationConfigAppViewModel, NegotiationConfigAppViewModel, NegotiationConfigAppViewModel, NegotiationConfigAppViewModel>, Interfaces.Services.INegotiationConfigService
    {
        private readonly INegotiationRoomConfigRepository _negotiationRoomConfigRepository;
        private readonly IRoomRepository _roomRepository;

        public NegotiationConfigService(Domain.Interfaces.INegotiationConfigService service, IUnitOfWork uow, IRepositoryFactory repositoryFactory)
            :base(uow, service)
        {
            _negotiationRoomConfigRepository = repositoryFactory.NegotiationRoomConfigRepository;
            _roomRepository = repositoryFactory.RoomRepository;
        }

        public IEnumerable<NegotiationConfigAppViewModel> GetByEdit()
        {
            var entities = service.GetAll().ToList();
            if (entities != null && entities.Any())
            {
                return NegotiationConfigAppViewModel.MapList(entities);
            }

            return new List<NegotiationConfigAppViewModel>() { new NegotiationConfigAppViewModel() };
        }

        public AppValidationResult Update(IEnumerable<NegotiationConfigAppViewModel> datesViewModel)
        {
            if (datesViewModel != null && datesViewModel.Any())
            {
                var entities = new List<NegotiationConfig>();

                foreach (var dateViewModel in datesViewModel)
                {
                    var entity = dateViewModel.MapReverse();
                    MapEntity(ref entity, dateViewModel);

                    entities.Add(entity);
                }

                ValidationResult.Add(service.UpdateAll(entities));

                if (ValidationResult.IsValid)
                    Commit();
            }

            return ValidationResult;
        }

        private void MapEntity(ref NegotiationConfig entity, NegotiationConfigAppViewModel viewModel)
        {
            if (entity.Rooms != null && entity.Rooms.Any())
            {
                _negotiationRoomConfigRepository.DeleteAll(entity.Rooms);
            }

            if (viewModel.Rooms != null && viewModel.Rooms.Any())
            {
                var entitiesRoomsConfigs = new List<NegotiationRoomConfig>();
                foreach (var itemViewModel in viewModel.Rooms.Distinct())
                {
                    var entityItem = itemViewModel.MapReverse();
                    var room = _roomRepository.Get(e => e.Uid == itemViewModel.RoomUid);
                    entityItem.SetRoom(room);
                    entitiesRoomsConfigs.Add(entityItem);
                }
                entity.SetRooms(entitiesRoomsConfigs);
            }
        }




    }
}
