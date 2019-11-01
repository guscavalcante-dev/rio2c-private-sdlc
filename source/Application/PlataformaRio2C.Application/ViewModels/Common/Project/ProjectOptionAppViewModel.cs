using PlataformaRio2C.Domain.Entities;
using System;
using System.Collections.Generic;

namespace PlataformaRio2C.Application.ViewModels
{
    public class ProjectOptionAppViewModel
    {
        public Guid Uid { get; set; }
        public string Title { get; set; }
        public string ProducerName { get; set; }

        public ProjectOptionAppViewModel()
        {

        }

        public ProjectOptionAppViewModel(Project entity)
        {
            Uid = entity.Uid;
            //Title = entity.GetName();

            //if (entity.Producer != null && !string.IsNullOrWhiteSpace(entity.Producer.Name))
            //{
            //    ProducerName = entity.Producer.Name;
            //    Title = string.Format("{0} - {1}", ProducerName, Title);
            //}
        }


        public static IEnumerable<ProjectOptionAppViewModel> MapList(IEnumerable<Project> entities)
        {
            foreach (var item in entities)
            {
                yield return new ProjectOptionAppViewModel(item);
            }
        }
    }
}
