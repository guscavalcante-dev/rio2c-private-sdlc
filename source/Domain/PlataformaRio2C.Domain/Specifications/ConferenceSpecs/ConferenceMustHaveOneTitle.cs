﻿//using PlataformaRio2C.Domain.Enums;
//using PlataformaRio2C.Domain.Interfaces;
//using System.Linq;

//namespace PlataformaRio2C.Domain.Entities.Specifications
//{
//    public class ConferenceMustHaveOneTitle : ISpecification<Conference>
//    {
//        public string Target { get { return "Titles"; } }
//        public ErrorCodes Code { get { return ErrorCodes.IsRequired; } }

//        public bool IsSatisfiedBy(Conference entity)
//        {
//            return entity.Titles != null && entity.Titles.Any();
//        }
//    }
//}
