using PlataformaRio2C.Application.ViewModels;
using PlataformaRio2C.Domain.Entities;
using System;
using System.Collections.Generic;

namespace PlataformaRio2C.Application.Interfaces.Services
{
    public interface ICollaboratorAppService : IAppService<CollaboratorBasicAppViewModel, CollaboratorDetailAppViewModel, CollaboratorEditAppViewModel, CollaboratorItemListAppViewModel>
    {
        CollaboratorAppViewModel GetByUserId(int id);
        Collaborator GetByUserEmail(string email);
        CollaboratorStatusRegisterAppViewModel GetStatusRegisterCollaboratorByUserId(int id);
        CollaboratorDetailAppViewModel GetDetailByUserId(int id);
        CollaboratorEditAppViewModel GetEditByUserId(int id);
        IEnumerable<Tuple<bool, CollaboratorItemListAppViewModel, string>> SendInvitationCollaboratorsByEmails(string[] emails);
        void MapEntity(ref Collaborator entity, IEnumerable<CollaboratorJobTitleAppViewModel> jobTitles, IEnumerable<CollaboratorMiniBioAppViewModel> miniBios);

        ImageFileAppViewModel GetThumbImage(Guid uid);
        ImageFileAppViewModel GetImage(Guid uid);

        IEnumerable<CollaboratorOptionAppViewModel> GetOptions(string term);

        IEnumerable<CollaboratorOptionMessageAppViewModel> GetOptionsChat(int userId);


        List<Country> listCountries();
        List<City> listCities(string stateCode);
        List<City> listCities(int stateCode);
        List<State> listStates(string countryCode);
        List<State> listStates(int countryCode);
    }
}
