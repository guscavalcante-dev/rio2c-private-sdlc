using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PlataformaRio2C.Application.ViewModels
{
    public class PlayerDetailWithInterestAppViewModel : PlayerBasicAppViewModel
    {
        #region props

        [Display(Name = "Executives", ResourceType = typeof(Labels))]
        public IEnumerable<CollaboratorBasicDetailAppViewModel> Collaborators { get; set; }
        public IEnumerable<CollaboratorOptionMessageAppViewModel> Executives { get; set; }
        

        public string Description { get; set; }
        public string RestrictionSpecifics { get; set; }

        public IEnumerable<LanguageAppViewModel> LanguagesOptions { get; set; }

        //public IEnumerable<PlayerInterestAppViewModel> Interests { get; set; }


        [Display(Name = "TargetAudience", ResourceType = typeof(Labels))]
        public IEnumerable<string> TargetAudience { get; set; }


        [Display(Name = "Activity", ResourceType = typeof(Labels))]
        public IEnumerable<string> Activitys { get; set; }


        public string[] Platforms { get; set; }

        public string[] Status { get; set; }

        public string[] Seeking { get; set; }

        public string[] Formats { get; set; }

        public string[] Genres { get; set; }

        public string[] SubGenres { get; set; }

        #endregion

        #region ctor

        public PlayerDetailWithInterestAppViewModel()
            :base()
        {

        }

        public PlayerDetailWithInterestAppViewModel(Player entity)
            :base(entity)
        {
            //if (entity.Collaborators != null && entity.Collaborators.Any())
            //{
            //    Collaborators = CollaboratorBasicDetailAppViewModel.MapList(entity.Collaborators);
            //    Executives = CollaboratorOptionMessageAppViewModel.MapList(entity.Collaborators);
            //}

            if (entity.PlayerActivitys != null && entity.PlayerActivitys.Any())
            {
                Activitys = entity.PlayerActivitys.Select(e => e.Activity.Name);
            }

            if (entity.PlayerTargetAudience != null && entity.PlayerTargetAudience.Any())
            {
                TargetAudience = entity.PlayerTargetAudience.Select(e => e.TargetAudience.Name);
            }

            if (entity.Interests != null)
            {
                Platforms = GetInterestsName(entity.Interests, "Platforms");
                Status = GetInterestsName(entity.Interests, "Status");
                Seeking = GetInterestsName(entity.Interests, "Seeking");
                Formats = GetInterestsName(entity.Interests, "Format");
                Genres = GetInterestsName(entity.Interests, "Gênero");
                SubGenres = GetInterestsName(entity.Interests, "Sub-genre");
            }

            Description = entity.GetDescription();
            RestrictionSpecifics = entity.GetRestrictionSpecifics();
        }

        #endregion


        public string[] GetInterestsName(IEnumerable<PlayerInterest> interests, string nameGroup)
        {
            if (interests != null)
            {
                var formats = interests.Where(e => e.Interest.InterestGroup.Name.Contains(nameGroup)).Select(e => e.Interest.Name);

                if (formats != null)
                {
                    return formats.ToArray();
                }
            }

            return new string[] { };
        }
    }
}
