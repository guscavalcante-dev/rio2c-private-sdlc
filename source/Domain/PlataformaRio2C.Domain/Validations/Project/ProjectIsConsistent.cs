//using PlataformaRio2C.Domain.Entities.Specifications;
//using PlataformaRio2C.Domain.Validation;
//using PlataformaRio2C.Infra.CrossCutting.Resources;
//using System.Globalization;
//using System.Threading;

//namespace PlataformaRio2C.Domain.Entities.Validations
//{
//    public class ProjectIsConsistent : Validation<Project>
//    {
//        public ProjectIsConsistent()
//        {

//            CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;


//            base.AddRule(new ValidationRule<Project>(new ProjectTitleEnIsRequired(), Messages.ProjectTitleEnIsRequired));
//            base.AddRule(new ValidationRule<Project>(new ProjectLogLineEnIsRequired(), Messages.ProjectLogLineEnIsRequired));
//            base.AddRule(new ValidationRule<Project>(new ProjectSummaryEnIsRequired(), Messages.ProjectSummaryEnIsRequired));

//            if (currentCulture != null && currentCulture.Name == "pt-BR")
//            {
//                base.AddRule(new ValidationRule<Project>(new ProjectTitlePtBrIsRequired(), Messages.ProjectTitlePtBrIsRequired));
//                base.AddRule(new ValidationRule<Project>(new ProjectLogLinePtBrIsRequired(), Messages.ProjectLogLinePtBrIsRequired));
//                base.AddRule(new ValidationRule<Project>(new ProjectSummaryPtBrIsRequired(), Messages.ProjectSummaryPtBrIsRequired));
//                base.AddRule(new ValidationRule<Project>(new ProductionPlanPtBrIsRequired(), Messages.ProductionPlanPtBrIsRequired));
//            }


//            base.AddRule(new ValidationRule<Project>(new ProductionPlanEnIsRequired(), Messages.ProductionPlanEnIsRequired));


//            base.AddRule(new ValidationRule<Project>(new ProjectMustHaveAPlatform(), Messages.ProjectMustHaveAPlatform));
//            base.AddRule(new ValidationRule<Project>(new ProjectMustHaveAStatus(), Messages.ProjectMustHaveAStatus));
//            //base.AddRule(new ValidationRule<Project>(new ProjectMustHaveASeeking(), Messages.ProjectMustHaveASeeking));
//            base.AddRule(new ValidationRule<Project>(new ProjectMustHaveAFormat(), Messages.ProjectMustHaveAFormat));

//            base.AddRule(new ValidationRule<Project>(new ProjectMustHaveNumberOfEpisodes(), Messages.ProjectMustHaveNumberOfEpisodes));
//            base.AddRule(new ValidationRule<Project>(new ProjectMustHaveEachEpisodePlayingTime(), Messages.ProjectMustHaveEachEpisodePlayingTime));

//            base.AddRule(new ValidationRule<Project>(new ProjectMustHaveAGenre(), Messages.ProjectMustHaveAGenre));
//            base.AddRule(new ValidationRule<Project>(new ProjectMustHaveTarget(), Messages.ProjectMustHaveTarget));
//            base.AddRule(new ValidationRule<Project>(new ProjectLookingFor(), Messages.ProjectLookingFor));
//            base.AddRule(new ValidationRule<Project>(new ProjectMustHaveASubGenre(), Messages.ProjectMustHaveASubGenre));

//            base.AddRule(new ValidationRule<Project>(new ProjectMustHavePitching(), Messages.ProjectMustHavePitching));
//        }
//    }
//}
