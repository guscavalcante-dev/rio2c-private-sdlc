﻿//using PlataformaRio2C.Domain.Enums;
//using PlataformaRio2C.Domain.Interfaces;
//using System.Linq;

//namespace PlataformaRio2C.Domain.Entities.Specifications
//{
//    public class ProjectMustHaveEachEpisodePlayingTime : ISpecification<Project>
//    {
//        public string Target { get { return "EachEpisodePlayingTime"; } }
//        public ErrorCodes Code { get { return ErrorCodes.IsInvalid; } }

//        public bool IsSatisfiedBy(Project entity)
//        {
//            if (entity.Interests != null && entity.Interests.Any(e => e.Interest.Name.Contains("Serie") || e.Interest.Name.Contains("Miniseries")))
//            {
//                return !string.IsNullOrWhiteSpace(entity.EachEpisodePlayingTime);
//            }

//            return true;
//        }
//    }
//}
